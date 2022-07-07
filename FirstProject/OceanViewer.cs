using System;

namespace OceanSimulationInConsole
{
    internal class OceanViewer : IOceanViewer
    {
        #region Consts
        private readonly IOceanView _ocean;
        #endregion

        #region CTORS
        public OceanViewer(IOceanView ocean)
        {
            _ocean = ocean;
        }
        #endregion

        #region Methods

        #region Private methods
        private void DisplayStats()
        {
            Console.Write("Iteration number: " + _ocean.CurrentIteration);
            Console.Write(" Obstacles:" + _ocean.NumObstacles);
            Console.Write(" Predators:" + _ocean.NumPredators);
            Console.Write(" Prey:" + _ocean.NumPrey);

            DisplayBorder();
        }

        private void DisplayCells()
        {
            for (int row = 0; row < _ocean.NumRows; row++)
            {
                for (int column = 0; column < _ocean.NumColumns; column++)
                {
                    if (_ocean[row, column] == null)
                    {
                        Console.Write(Ocean.DefaultCellImage);
                    }
                    else
                    {
                        _ocean[row, column].WasIterated = false;
                        Console.Write(_ocean[row, column].Image);
                    }

                }

                Console.Write("\n");
            }
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
        #endregion

        #region Public Methods
        public int RequestValuesAndAssignThem(string s)
        {
            int number;

            Console.WriteLine("Enter the number of {0}", s);
            number = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("The number of {0} accepted " + number, s);

            return number;
        }

        public void DisplayGameState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Start:

                    Console.WriteLine("Starting...");

                    break;

                case GameState.Continue:

                    Console.Write("Press any key to continue");
                    Console.ReadKey();

                    break;

                case GameState.End:

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

        public void DisplayIteration()
        {
            DisplayStats();
            DisplayCells();
            DisplayBorder();

            DisplayGameState(GameState.Continue);
        }
        #endregion

        #endregion
    }
}
