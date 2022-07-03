﻿using System;

namespace FirstProject
{
    internal class Ocean
    {
        private int _numRows;
        private int _numColumns;
        private int _numPrey;
        private int _numPredators;
        private int _numObstacles;
        private int _numIterations;
        private int size;
        private Cell[,] cells;

        private int NumObstacles
        {
            get
            {
                return _numObstacles;
            }
            set
            {
                if (value > size - 2 || value < 0)
                {
                    Console.WriteLine("Invalid value, so it will be set to maximum possible value");
                    _numObstacles = size - 2;
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
                if (value > size - NumObstacles - NumPredators || value < 0)
                {
                    Console.WriteLine("Invalid value, so it will be set to maximum possible value");
                    _numPrey = size - NumObstacles - NumPredators;
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
                if (value > size - NumObstacles - 1 || value < 0)
                {
                    Console.WriteLine("Invalid value, so it will be set to maximum possible value");
                    _numPredators = size - NumObstacles - 1;
                }
                else
                {
                    _numPredators = value;
                }
            }
        }

        public Ocean(int numRows = 5, int numColumns = 5, int numPrey = 150, int numPredators = 20, int numObstacles = 75)
        {
            NumColumns = numColumns;
            NumRows = numRows;
            size = NumRows * NumColumns;
            cells = new Cell[NumRows, NumColumns];

            Run();
        }

        private void InitCells()
        {
            Fabric(CellType.CellTypes.Obstacle);
            Fabric(CellType.CellTypes.Predator);
            Fabric(CellType.CellTypes.Prey);
        }

        private void Fabric(CellType.CellTypes type)
        {
            Coordinate empty;

            switch (type)
            {
                case CellType.CellTypes.Obstacle:
                    for (int i = 0; i < NumObstacles; i++)
                    {
                        empty = GetEmptyCellCoord();
                        cells[empty.X, empty.Y] = new Obstacle(empty, this);

                    }

                    break;

                case CellType.CellTypes.Predator:
                    for (int i = 0; i < NumPredators; i++)
                    {
                        empty = GetEmptyCellCoord();
                        cells[empty.X, empty.Y] = new Predator(empty, this);
                    }

                    break;

                case CellType.CellTypes.Prey:
                    for (int i = 0; i < NumPrey; i++)
                    {
                        empty = GetEmptyCellCoord();
                        cells[empty.X, empty.Y] = new Prey(empty, this);
                    }

                    break;
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
            while (cells[x, y] != null);

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
                    if (cells[row, column] == null)
                    {
                        Console.Write('-');
                    }
                    else
                    {
                        cells[row, column].isBeenIterated = false;
                        cells[row, column].Display();
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

        private Coordinate GetNeighborWithImage(CellType.CellTypes neighborType, Coordinate currentCoordinate)
        {
            int count = 0;
            Coordinate[] neighbors = new Coordinate[4];

            switch (neighborType)
            {
                case CellType.CellTypes.Prey:

                    foreach (Coordinate coordinate in GetNeighbors(currentCoordinate))
                    {
                        if (GetCellAt(coordinate)?.Image == 'f')
                        {
                            neighbors[count++] = coordinate;
                        }
                    }

                    break;

                case CellType.CellTypes.Empty:

                    foreach (Coordinate coordinate in GetNeighbors(currentCoordinate))
                    {
                        if (GetCellAt(coordinate) == null)
                        {
                            neighbors[count++] = coordinate;
                        }
                    }

                    break;
            }

            if (neighbors.Length == 0)
            {
                neighbors[count++] = currentCoordinate;
            }

            return neighbors[RandomNumberGenerator.random.Next(0, count)];
        }

        private Coordinate[] GetNeighbors(Coordinate currentCoordinate)
        {
            int count = 0;
            Coordinate[] neighbors = new Coordinate[4];
            Coordinate north = North(currentCoordinate);
            Coordinate south = South(currentCoordinate);
            Coordinate east = East(currentCoordinate);
            Coordinate west = West(currentCoordinate);

            neighbors[count++] = north;
            neighbors[count++] = south;
            neighbors[count++] = east;
            neighbors[count++] = west;

            return neighbors;
        }

        private Cell GetCellAt(Coordinate coordinate)
        {
            return cells[coordinate.X, coordinate.Y];
        }

        private Coordinate East(Coordinate currentCoordinate)
        {
            int x;
            x = currentCoordinate.X == NumRows - 1 ? currentCoordinate.X : (currentCoordinate.X + 1);

            return new Coordinate(x, currentCoordinate.Y);
        }

        private Coordinate West(Coordinate currentCoordinate)
        {
            int x;
            x = currentCoordinate.X == 0 ? currentCoordinate.X : (currentCoordinate.X - 1);

            return new Coordinate(x, currentCoordinate.Y);
        }

        private Coordinate North(Coordinate currentCoordinate)
        {
            int y;
            y = currentCoordinate.Y == 0 ? currentCoordinate.Y : (currentCoordinate.Y - 1);

            return new Coordinate(currentCoordinate.X, y);
        }

        private Coordinate South(Coordinate currentCoordinate)
        {
            int y;
            y = currentCoordinate.Y == NumColumns - 1 ? currentCoordinate.Y : (currentCoordinate.Y + 1);

            return new Coordinate(currentCoordinate.X, y);
        }

        public Coordinate GetEmptyNeighborCoord(Coordinate currentCoordinate)
        {
            return GetNeighborWithImage(CellType.CellTypes.Empty, currentCoordinate);
        }

        public Coordinate GetNeighborPreyCoord(Coordinate currentCoordinate)
        {
            return GetNeighborWithImage(CellType.CellTypes.Prey, currentCoordinate);
        }

        public void MoveFrom(Coordinate from, Coordinate to)
        {
            if (from != to)
            {
                Cell cell = GetCellAt(from);

                AssignCellAt(from, null);
                AssignCellAt(to, cell);
            }
        }

        public void AssignCellAt(Coordinate coordinate, Cell cell)
        {
            cells[coordinate.X, coordinate.Y] = cell;
        }

        public void Run()
        {
            Console.WriteLine("Enter the number of iterations (default=1000)");
            NumIterations = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("The number of iterations accepted " + NumIterations);


            Console.WriteLine("Enter the number of obstacles (default=75)");
            NumObstacles = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("The number of obstacles accepted " + NumObstacles);

            Console.WriteLine("Enter the number of predators (default=20)");
            NumPredators = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("The number of predators accepted " + NumPredators);

            Console.WriteLine("Enter the number of prey (default=150)");
            NumPrey = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("The number of prey accepted " + NumPrey);

            Console.WriteLine("Starting...");

            InitCells();

            for (int iteration = 0; iteration < NumIterations; iteration++)
            {
                if (NumPredators > 0 && NumPrey > 0)
                {
                    for (int row = 0; row < NumRows; row++)
                    {
                        for (int column = 0; column < NumColumns; column++)
                        {
                            Cell сell = cells[row, column];

                            if (сell == null || iteration == 0)
                            {
                                continue;
                            }

                            cells[row, column].Owner = this;

                            cells[row, column].Process();
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
