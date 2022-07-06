using System;
using System.Collections.Generic;

namespace FirstProject
{
    internal class Ocean
    {
        #region CONSTS
        private const int NumRowsDefault = 25;
        private const int NumColumnsDefault = 70;
        private const int NumPreyDefault = 150;
        private const int NumPredatorsDefault = 20;
        private const int NumObstaclesDefault = 75;
        private const int NumIterationsDefault = 1000;
        private const int NumDirections = 4;
        public const char DefaultCellImage = '-';
        #endregion

        #region READONLY
        private readonly Cell[,] _cells;
        private readonly IOceanViewer _supervisor;
        #endregion

        #region PRIVATE FIELDS
        private int _numRows;
        private int _numColumns;
        private int _numPrey;
        private int _numPredators;
        private int _numObstacles;
        private int _numIterations;
        private int _size;
        #endregion

        #region PRIVATE PROPERTIES
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
        private int NumRows
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
        private int NumColumns
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
        #endregion

        #region PUBLIC PROPERTIES
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
        #endregion

        #region CTORS
        public Ocean()
        {
            _numColumns = NumColumnsDefault;
            _numRows = NumRowsDefault;
            _numObstacles = NumObstaclesDefault;
            _numPredators = NumPredatorsDefault;
            _numPrey = NumPreyDefault;
            _numIterations = NumIterationsDefault;

            _size = NumRows * NumColumns;
            _cells = new Cell[NumRows, NumColumns];
            _supervisor = new OceanViewer(this);

            Run();
        }
        #endregion

        #region INDEXERS
        public Cell this[Coordinate coordinate]
        {
            get
            {
                return _cells[coordinate.X, coordinate.Y];
            }
            set
            {
                _cells[coordinate.X, coordinate.Y] = value;
            }
        }

        public Cell this[int x, int y]
        {
            get
            {
                return this[new Coordinate(x, y)];
            }
            set
            {
                this[new Coordinate(x, y)] = value;
            }
        }
        #endregion

        #region METHODS

        #region METHODS FOR CREATING CELLS
        private void InitializeCells()
        {
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
                empty = GetEmptyCellCoord();
                cell = Activator.CreateInstance(type, empty, this) as Cell;

                this[empty] = cell;
            }
        }

        private Coordinate GetEmptyCellCoord()
        {
            int x, y;

            do
            {
                x = RandomNumberGenerator.random.Next(0, _numRows);
                y = RandomNumberGenerator.random.Next(0, _numColumns);
            }
            while (_cells[x, y] != null);

            return new Coordinate(x, y);
        }
        #endregion;

        #region METHODS FOR GETTING NEIGHBORS
        private Coordinate GetNeighborWithImage(CellTypes neighborType, Coordinate currentCoordinate)
        {
            int count = 0;
            Coordinate[] neighbors = new Coordinate[NumDirections];

            switch (neighborType)
            {
                case CellTypes.Prey:

                    foreach (Coordinate coordinate in GetNeighbors(currentCoordinate))
                    {
                        if (this[coordinate]?.Image == Prey.DefaultPreyImage)
                        {
                            neighbors[count++] = coordinate;
                        }
                    }

                    break;

                case CellTypes.Empty:

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

            return neighbors[RandomNumberGenerator.random.Next(0, count)];
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

        #region PUBLIC METHODS
        public Coordinate GetEmptyNeighborCoord(Coordinate currentCoordinate)
        {
            return GetNeighborWithImage(CellTypes.Empty, currentCoordinate);
        }

        public Coordinate GetNeighborPreyCoord(Coordinate currentCoordinate)
        {
            return GetNeighborWithImage(CellTypes.Prey, currentCoordinate);
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

        public void Run()
        {
            try
            {
                NumIterations = _supervisor.RequestValuesAndAssignThem("iterations");
                NumObstacles = _supervisor.RequestValuesAndAssignThem("obstacles");
                NumPredators = _supervisor.RequestValuesAndAssignThem("predators");
                NumPrey = _supervisor.RequestValuesAndAssignThem("prey");
            }
            catch (FormatException)
            {
                _supervisor.DisplayValidationMessage(true);
            }

            _supervisor.DisplayGameState(GameStates.Start);
            InitializeCells();

            for (int iteration = 0; iteration < NumIterations; iteration++)
            {
                if (NumPredators > 0 && NumPrey > 0)
                {
                    for (int row = 0; row < NumRows; row++)
                    {
                        if (iteration == 0)
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

                            _cells[row, column].Process();
                        }
                    }

                    _supervisor.DisplayStats(iteration);
                    _supervisor.DisplayCells(NumRows, NumColumns);
                    _supervisor.DisplayBorder();

                    _supervisor.DisplayGameState(GameStates.Continue);
                }
            }

            _supervisor.DisplayGameState(GameStates.End);
        }
        #endregion

        #endregion
    }
}
