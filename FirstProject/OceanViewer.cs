using System;

namespace FirstProject
{
    internal class OceanViewer : IOceanViewer
    {
        #region CONSTS
        private readonly Ocean _ocean;
        #endregion

        #region CTORS
        public OceanViewer(Ocean ocean)
        {
            _ocean = ocean;
        }
        #endregion

        #region METHODS
        public int RequestValuesAndAssignThem(string s)
        {
            int number;

            Console.WriteLine("Enter the number of {0}", s);
            number = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("The number of {0} accepted " + number, s);

            return number;
        }

        public void DisplayStats(int iteration)
        {
            Console.Write("Iteration number: " + ++iteration);
            Console.Write(" Obstacles:" + _ocean.NumObstacles);
            Console.Write(" Predators:" + _ocean.NumPredators);
            Console.Write(" Prey:" + _ocean.NumPrey);

            DisplayBorder();
        }

        public void DisplayCells(int numRows, int numColumns)
        {
            for (int row = 0; row < numRows; row++)
            {
                for (int column = 0; column < numColumns; column++)
                {
                    if (_ocean[row, column] == null)
                    {
                        Console.Write(Ocean.DefaultCellImage);
                    }
                    else
                    {
                        _ocean[row, column].wasIterated = false;
                        Console.Write(_ocean[row, column].Image);
                    }

                }

                Console.Write("\n");
            }
        }

        public void DisplayBorder()
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

        public void DisplayGameState(GameStates gameState)
        {
            switch (gameState)
            {
                case GameStates.Start:

                    Console.WriteLine("Starting...");

                    break;

                case GameStates.Continue:

                    Console.Write("Press any key to continue");
                    Console.ReadKey();

                    break;

                case GameStates.End:

                    Console.WriteLine("Simulation has been ended");
                    Console.ReadKey();

                    break;
            }
        }

        public void DisplayValidationMessage(bool wasFormatException)
        {
            if (wasFormatException)
            {
                Console.WriteLine("Invalid input, so everything will be set to its default value");
            }
            else
            {
                Console.WriteLine("Invalid value, so it will be set to maximum possible value");
            }
        }
        #endregion
    }
}
