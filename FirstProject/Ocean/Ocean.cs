using System;
using System.Collections.Generic;

namespace OceanSimulationInConsole
{
    internal class Ocean : IOceanView, IOcean
    {
        #region Consts
        private const int NumRowsDefault = 25;
        private const int NumColumnsDefault = 70;
        private const int NumPreyDefault = 150;
        private const int NumPredatorsDefault = 20;
        private const int NumObstaclesDefault = 75;
        private const int NumIterationsDefault = 1000;
        private const int NumDirections = 4;

        public const char DefaultCellImage = '-';
        #endregion

        #region Readonly
        private readonly Cell[,] _cells; // array of the cells in the ocean
        private readonly IOceanViewer _supervisor; // field for interacting with UI
        private readonly RandomNumberGenerator _randomNumberGenerator; // field for interacting with random number generator
        #endregion

        #region Private fields
        private int _numRows; // the number of rows in the ocean
        private int _numColumns; // the number of columnss in the ocean
        private int _numPrey; // the number of prey in the ocean
        private int _numPredators; // the number of predators in the ocean
        private int _numObstacles; // the number of obstacles in the ocean
        private int _numIterations; // the number of iterations in the simulation
        private int _size; // the size of the array _cells
        #endregion

        #region Ctors
        public Ocean()
        {
            _numColumns = NumColumnsDefault;
            _numRows = NumRowsDefault;

            _size = NumRows * NumColumns;
            _cells = new Cell[NumRows, NumColumns];
            _supervisor = new OceanViewer(this);
            _randomNumberGenerator = new RandomNumberGenerator(this);

            Run();
        }
        #endregion

        #region Private properties
        private int NumIterations
        {
            get
            {
                return _numIterations;
            }
            set
            {
                if (value > NumIterationsDefault || value < 0)
                {
                    _supervisor.DisplayValidationMessage(false);
                    _numIterations = NumIterationsDefault;
                }
                else
                {
                    _numIterations = value;
                }
            }
        }
        #endregion

        #region Public properties
        public int NumRows
        {
            get
            {
                return _numRows;
            }
            set
            {
                if (value > NumRowsDefault || value < 0)
                {
                    _supervisor.DisplayValidationMessage(false);
                    _numRows = NumRowsDefault;
                }
                else
                {
                    _numRows = value;
                }
            }
        }
        public int NumColumns
        {
            get
            {
                return _numColumns;
            }
            set
            {
                if (value > NumColumnsDefault || value < 0)
                {
                    _supervisor.DisplayValidationMessage(false);
                    _numColumns = NumColumnsDefault;
                }
                else
                {
                    _numColumns = value;
                }
            }
        }
        public int NumPrey
        {
            get
            {
                return _numPrey;
            }
            set
            {
                if (value > _size - NumObstacles - NumPredators || value < 0)
                {
                    _supervisor.DisplayValidationMessage(false);
                    _numPrey = _size - NumObstacles - NumPredators;
                }
                else
                {
                    _numPrey = value;
                }
            }
        }
        public int NumPredators
        {
            get
            {
                return _numPredators;
            }
            set
            {
                if (value > _size - NumObstacles - 1 || value < 0)
                {
                    _supervisor.DisplayValidationMessage(false);
                    _numPredators = _size - NumObstacles - 1;
                }
                else
                {
                    _numPredators = value;
                }
            }
        }
        public int NumObstacles
        {
            get
            {
                return _numObstacles;
            }
            set
            {
                if (value > _size - 2 || value < 0)
                {
                    _supervisor.DisplayValidationMessage(false);
                    _numObstacles = _size - 2;
                }
                else
                {
                    _numObstacles = value;
                }
            }
        }
        public int CurrentIteration { get; set; }
        #endregion

        #region Indexders
        public Cell this[Coordinate coordinate]
        {
            get
            {
                return this[coordinate.X, coordinate.Y];
            }
            set
            {
                this[coordinate.X, coordinate.Y] = value;
            }
        }

        public Cell this[int x, int y]
        {
            get
            {
                CheckCoordinate(x, y);

                return _cells[x, y];
            }
            set
            {
                CheckCoordinate(x, y);

                _cells[x, y] = value;
            }
        }
        #endregion

        #region Methods

        #region Methods for launching
        private void Run()
        {
            InitializeCells();

            for (CurrentIteration = 1; CurrentIteration < NumIterations; CurrentIteration++)
            {
                ICellVisitor cellVisitor = new CellVisitor();

                if (NumPredators > 0 && NumPrey > 0)
                {
                    for (int row = 0; row < NumRows; row++)
                    {
                        if (CurrentIteration == 1)
                        {
                            break;
                        }

                        for (int column = 0; column < NumColumns; column++)
                        {
                            Cell сell = _cells[row, column];

                            if (сell == null)
                            {
                                continue;
                            }

                            _cells[row, column].Accept(cellVisitor);
                        }
                    }

                    _supervisor.DisplayIteration();
                }
            }

            _supervisor.DisplayGameState(GameState.End);
        }
        #endregion

        #region Methods for creating cells
        private void InitializeCells()
        {
            NumIterations = _supervisor.RequestValueAndAssign("iterations", NumIterationsDefault);
            NumObstacles = _supervisor.RequestValueAndAssign("obstacles", NumObstaclesDefault);
            NumPredators = _supervisor.RequestValueAndAssign("predators", NumPredatorsDefault);
            NumPrey = _supervisor.RequestValueAndAssign("prey", NumPreyDefault);

            _supervisor.DisplayGameState(GameState.Start);

            CreateCells(typeof(Obstacle), NumObstacles);
            CreateCells(typeof(Predator), NumPredators);
            CreateCells(typeof(Prey), NumPrey);
        }

        private void CreateCells(Type type, int amount)
        {
            Cell cell;
            Coordinate empty;

            for (int i = 0; i < amount; i++)
            {
                empty = _randomNumberGenerator.GetEmptyCellCoord();
                cell = Activator.CreateInstance(type, empty, this) as Cell;

                this[empty] = cell;
            }
        }
        #endregion;

        #region Methods for getting neighbors
        private Coordinate GetNeighborWithType(CellType neighborType, Coordinate currentCoordinate)
        {
            int count = 0;
            Coordinate[] neighbors = new Coordinate[NumDirections];

            switch (neighborType)
            {
                case CellType.Prey:

                    foreach (Coordinate coordinate in GetNeighbors(currentCoordinate))
                    {
                        if (this[coordinate]?.Image == Prey.DefaultPreyImage)
                        {
                            neighbors[count++] = coordinate;
                        }
                    }

                    break;

                case CellType.Empty:

                    foreach (Coordinate coordinate in GetNeighbors(currentCoordinate))
                    {
                        if (this[coordinate] == null)
                        {
                            neighbors[count++] = coordinate;
                        }
                    }

                    break;
            }

            if (count == 0)
            {
                neighbors[count++] = currentCoordinate;
            }

            return neighbors[_randomNumberGenerator.Random.Next(0, count)];
        }

        private IEnumerable<Coordinate> GetNeighbors(Coordinate currentCoordinate)
        {
            yield return North(currentCoordinate);
            yield return South(currentCoordinate);
            yield return East(currentCoordinate);
            yield return West(currentCoordinate);
        }

        private Coordinate East(Coordinate currentCoordinate)
        {
            int y;
            y = currentCoordinate.Y == NumColumns - 1 ? currentCoordinate.Y : (currentCoordinate.Y + 1);

            return new Coordinate(currentCoordinate.X, y);
        }

        private Coordinate West(Coordinate currentCoordinate)
        {
            int y;
            y = currentCoordinate.Y == 0 ? currentCoordinate.Y : (currentCoordinate.Y - 1);

            return new Coordinate(currentCoordinate.X, y);
        }

        private Coordinate North(Coordinate currentCoordinate)
        {
            int x;
            x = currentCoordinate.X == 0 ? currentCoordinate.X : (currentCoordinate.X - 1);

            return new Coordinate(x, currentCoordinate.Y);
        }

        private Coordinate South(Coordinate currentCoordinate)
        {
            int x;
            x = currentCoordinate.X == NumRows - 1 ? currentCoordinate.X : (currentCoordinate.X + 1);

            return new Coordinate(x, currentCoordinate.Y);
        }
        #endregion

        #region Utility methods
        private void CheckCoordinate(int x, int y)
        {
            if (x > NumRows || y > NumColumns || x < 0 || y < 0)
            {
                throw new InvalidCoordinateException("You tried to access an array by non-existent coordinates");
            }
        }
        #endregion

        #region Public methods
        public Coordinate GetEmptyNeighborCoord(Coordinate currentCoordinate)
        {
            return GetNeighborWithType(CellType.Empty, currentCoordinate);
        }

        public Coordinate GetNeighborPreyCoord(Coordinate currentCoordinate)
        {
            return GetNeighborWithType(CellType.Prey, currentCoordinate);
        }

        public void MoveFrom(Coordinate from, Coordinate to)
        {
            if (from != to)
            {
                Cell cell = this[from];
                cell.Offset = to;

                this[from] = null;
                this[to] = cell;
            }
        }
        #endregion

        #endregion
    }
}
