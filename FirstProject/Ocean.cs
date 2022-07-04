using System;
using System.Reflection;
using System.Collections.Generic;

namespace FirstProject
{
    internal class Ocean
    {
        private const char DefaultCellImage = '-';
        private const int NumRowsDefault = 5;
        private const int NumColumnsDefault = 5;
        private const int NumPreyDefault = 150;
        private const int NumPredatorsDefault = 20;
        private const int NumObstaclesDefault = 75;
        private const int NumDirections = 4;

        private int _numRows;
        private int _numColumns;
        private int _numPrey;
        private int _numPredators;
        private int _numObstacles;
        private int _numIterations;
        private int _size;
        private Cell[,] _cells;

        private int NumObstacles
        {
            get
            {
                return _numObstacles;
            }
            set
            {
                if (value > _size - 2 || value < 0)
                {
                    Console.WriteLine("Invalid value, so it will be set to maximum possible value");
                    _numObstacles = _size - 2;
                }
                else
                {
                    _numObstacles = value;
                }
            }
        }
        private int NumIterations
        {

            get
            {
                return _numIterations;
            }
            set
            {
                if (value > 1000 || value < 0)
                {
                    Console.WriteLine("Invalid value, so it will be set to maximum possible value");
                    _numIterations = 1000;
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
                if (value > 25 || value < 0)
                {
                    Console.WriteLine("Invalid value, so it will be set to maximum possible value");
                    _numRows = 25;
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
                if (value > 70 || value < 0)
                {
                    Console.WriteLine("Invalid value, so it will be set to maximum possible value");
                    _numColumns = 70;
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
                    Console.WriteLine("Invalid value, so it will be set to maximum possible value");
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
                    Console.WriteLine("Invalid value, so it will be set to maximum possible value");
                    _numPredators = _size - NumObstacles - 1;
                }
                else
                {
                    _numPredators = value;
                }
            }
        }

        public Ocean()
        {
            _numColumns = NumColumnsDefault;
            _numRows = NumRowsDefault;
            _numObstacles = NumObstaclesDefault;
            _numPredators = NumPredatorsDefault;
            _numPrey = NumPreyDefault;

            _size = NumRows * NumColumns;
            _cells = new Cell[NumRows, NumColumns];

            Run();
        }

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

                AssignCellAt(empty, cell);
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

        private void DisplayBorder()
        {
            for (int column = 0; column < Console.WindowWidth; column++)
            {
                if (column == Console.WindowWidth - 1 || column == 0)
                {
                    Console.Write("\n");
                }
                else
                {
                    Console.Write("*");
                }
            }
        }

        private void DisplayCells()
        {
            for (int row = 0; row < NumRows; row++)
            {
                for (int column = 0; column < NumColumns; column++)
                {
                    if (_cells[row, column] == null)
                    {
                        Console.Write(DefaultCellImage);
                    }
                    else
                    {
                        _cells[row, column].isBeenIterated = false;
                        _cells[row, column].Display();
                    }

                }

                Console.Write("\n");
            }
        }

        private void DisplayStats(int iteration)
        {
            Console.Write("Iteration number: " + ++iteration);
            Console.Write(" Obstacles:" + NumObstacles);
            Console.Write(" Predators:" + NumPredators);
            Console.Write(" Prey:" + NumPrey);

            DisplayBorder();
        }

        private Coordinate GetNeighborWithImage(CellTypes neighborType, Coordinate currentCoordinate)
        {
            int count = 0;
            Coordinate[] neighbors = new Coordinate[NumDirections];

            switch (neighborType)
            {
                case CellTypes.Prey:

                    foreach (Coordinate coordinate in GetNeighbors(currentCoordinate))
                    {
                        if (GetCellAt(coordinate)?.Image == Prey.DefaultPreyImage)
                        {
                            neighbors[count++] = coordinate;
                        }
                    }

                    break;

                case CellTypes.Empty:

                    foreach (Coordinate coordinate in GetNeighbors(currentCoordinate))
                    {
                        if (GetCellAt(coordinate) == null)
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

        public Coordinate GetEmptyNeighborCoord(Coordinate currentCoordinate)
        {
            return GetNeighborWithImage(CellTypes.Empty, currentCoordinate);
        }

        public Coordinate GetNeighborPreyCoord(Coordinate currentCoordinate)
        {
            return GetNeighborWithImage(CellTypes.Prey, currentCoordinate);
        }

        private Cell GetCellAt(Coordinate coordinate)
        {
            return _cells[coordinate.X, coordinate.Y];
        }

        public void MoveFrom(Coordinate from, Coordinate to)
        {
            if (from != to)
            {
                Cell cell = GetCellAt(from);
                cell.Offset = to;

                AssignCellAt(from, null);
                AssignCellAt(to, cell);
            }
        }

        public void AssignCellAt(Coordinate coordinate, Cell cell)
        {
            _cells[coordinate.X, coordinate.Y] = cell;
        }

        public void Run()
        {
            Console.WriteLine("Enter the number of iterations");
            NumIterations = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("The number of iterations accepted " + NumIterations);

            Console.WriteLine("Enter the number of obstacles");
            NumObstacles = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("The number of obstacles accepted " + NumObstacles);

            Console.WriteLine("Enter the number of predators");
            NumPredators = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("The number of predators accepted " + NumPredators);

            Console.WriteLine("Enter the number of prey");
            NumPrey = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("The number of prey accepted " + NumPrey);

            Console.WriteLine("Starting...");

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

                    DisplayStats(iteration);
                    DisplayCells();
                    DisplayBorder();

                    Console.Write("Press any key to continue");
                    Console.ReadKey();
                }
            }

            Console.WriteLine("Simulation has been ended");
            Console.ReadKey();
        }
    }
}
