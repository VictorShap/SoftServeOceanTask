using System;

namespace FirstProject
{
    internal class Ocean
    {
        private int numRows;
        private int numColumns;
        private int numPrey;
        private int numPredators;
        private int numObstacles;
        private int numIterations;
        public Random random = new Random();
        public Cell[,] cells;
        private int size;

        public int NumRows
        {
            get
            {
                return numRows;
            }
            set
            {
                if (value > 25 || value < 0)
                {
                    Console.WriteLine("Invalid value, so it will be set to maximum possible value");
                    numRows = 25;
                }
                else
                {
                    numRows = value;
                }
            }
        }
        public int NumColumns
        {
            get
            {
                return numColumns;
            }
            set
            {
                if (value > 70 || value < 0)
                {
                    Console.WriteLine("Invalid value, so it will be set to maximum possible value");
                    numColumns = 70;
                }
                else
                {
                    numColumns = value;
                }
            }
        }
        public int NumPrey
        {
            get
            {
                return numPrey;
            }
            set
            {
                if (value > size - NumObstacles - NumPredators || value <= 0)
                {
                    Console.WriteLine("Invalid value, so it will be set to maximum possible value");
                    numPrey = size - NumObstacles - NumPredators;
                }
                else
                {
                    numPrey = value;
                }
            }
        }
        public int NumPredators
        {
            get
            {
                return numPredators;
            }
            set
            {
                if (value > size - NumObstacles - 1 || value <= 0)
                {
                    Console.WriteLine("Invalid value, so it will be set to maximum possible value");
                    numPredators = size - NumObstacles - 1;
                }
                else
                {
                    numPredators = value;
                }
            }
        }
        private int NumObstacles
        {
            get
            {
                return numObstacles;
            }
            set
            {
                if (value > size - 2 || value <= 0)
                {
                    Console.WriteLine("Invalid value, so it will be set to maximum possible value");
                    numObstacles = size - 2;
                }
                else
                {
                    numObstacles = value;
                }
            }
        }
        private int NumIterations
        {

            get
            {
                return numIterations;
            }
            set
            {
                if (value > 1000 || value < 0)
                {
                    Console.WriteLine("Invalid value, so it will be set to maximum possible value");
                    numIterations = 1000;
                }
                else
                {
                    numIterations = value;
                }
            }

        }

        public Ocean(int numRows = 4, int numColumns = 4, int numPrey = 150, int numPredators = 20, int numObstacles = 75)
        {
            NumColumns = numColumns;
            NumRows = numRows;
            size = NumRows * NumColumns;
            cells = new Cell[NumRows, NumColumns];

            Run();
        }

        private void InitCells()
        {
            AddEmptyCells();

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

            AddObstacles();
            AddPredators();
            AddPrey();

            DisplayStats(-1);
            DisplayCells();
            DisplayBorder();

            //Console.Write("Press any key to continue");
            //Console.ReadKey();
        }

        private void AddEmptyCells()
        {
            for (int row = 0; row < NumRows; row++)
            {
                for (int column = 0; column < NumColumns; column++)
                {
                    Cell cell = new Cell(new Coordinate(row, column));
                    cells[row, column] = cell;
                }
            }
        }

        private void AddObstacles()
        {
            Coordinate empty;

            for (int i = 0; i < NumObstacles; i++)
            {
                empty = GetEmptyCellCoord();
                cells[empty.X, empty.Y] = new Obstacle(empty);

            }
        }

        private void AddPredators()
        {
            Coordinate empty;

            for (int i = 0; i < NumPredators; i++)
            {
                empty = GetEmptyCellCoord();
                cells[empty.X, empty.Y] = new Predator(empty);
            }
        }

        private void AddPrey()
        {
            Coordinate empty;

            for (int i = 0; i < NumPrey; i++)
            {
                empty = GetEmptyCellCoord();
                cells[empty.X, empty.Y] = new Prey(empty);
            }
        }

        private Coordinate GetEmptyCellCoord()
        {
            int x, y;
            Coordinate empty;

            do
            {
                x = random.Next(0, numRows - 1);
                y = random.Next(0, numColumns - 1);
                empty = cells[x, y].Offset;

            }
            while (cells[x, y] != null && cells[x, y].Image != '-');

            return empty;
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
                    cells[row, column].isBeenIterated = false;
                    cells[row, column].Display();
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

        public void Run()
        {
            Console.WriteLine("Enter the number of iterations (default=1000)");
            NumIterations = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("The number of iterations accepted " + NumIterations);

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
                            сell.owner = this;
                            cells[row, column].Process();
                        }
                    }

                    DisplayStats(iteration);
                    DisplayCells();
                    DisplayBorder();

                    //Console.Write("Press any key to continue");
                    //Console.ReadKey();
                }
            }

            Console.WriteLine("Simulation has been ended");
            Console.ReadKey();
        }
    }
}
