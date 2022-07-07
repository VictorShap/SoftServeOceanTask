using System;
using System.Threading;
using System.Text;

namespace OceanSimulationInConsole
{
    internal class OceanViewer : IOceanViewer
    {
        #region Consts
        private const int IterationCursorDefaultLeftPosition = 18;
        private const int ObstaclesCursorDefaultLeftPosition = 11;
        private const int PredatorsCursorDefaultLeftPosition = 11;
        private const int PreyCursorDefaultLeftPosition = 6;
        private const int IterationCursorDefaultTopPosition = 0;
        private const int ObstaclesCursorDefaultTopPosition = 1;
        private const int PredatorsCursorDefaultTopPosition = 2;
        private const int PreyCursorDefaultTopPosition = 3;
        private const int CursorDefaultLeftPosition = 0;
        private const int CursorDefaultTopPosition = 6;
        #endregion

        #region Readonly
        private readonly IOceanView _ocean;
        #endregion

        #region Ctors
        public OceanViewer(IOceanView ocean)
        {
            _ocean = ocean;
        }
        #endregion

        #region Methods

        #region Private methods
        private void DisplayStats()
        {
            if (_ocean.CurrentIteration == 1)
            {
                Console.WriteLine("Iteration number: " + _ocean.CurrentIteration);
                Console.WriteLine("Obstacles: " + _ocean.NumObstacles);
                Console.WriteLine("Predators: " + _ocean.NumPredators);
                Console.WriteLine("Prey: " + _ocean.NumPrey);
                DisplayBorder();
            }
            else
            {
                ReplaceOldValues(IterationCursorDefaultLeftPosition, _ocean.CurrentIteration, IterationCursorDefaultTopPosition);
                ReplaceOldValues(ObstaclesCursorDefaultLeftPosition, _ocean.NumObstacles, ObstaclesCursorDefaultTopPosition);
                ReplaceOldValues(PredatorsCursorDefaultLeftPosition, _ocean.NumPredators, PredatorsCursorDefaultTopPosition);
                ReplaceOldValues(PreyCursorDefaultLeftPosition, _ocean.NumPrey, PreyCursorDefaultTopPosition);

                Console.SetCursorPosition(CursorDefaultLeftPosition, CursorDefaultTopPosition);
            }
        }

        private void ReplaceOldValues(int start, int lenght, int line)
        {
            Console.SetCursorPosition(start + lenght.ToString().Length, line);

            for (int i = 0; i < lenght.ToString().Length; i++)
            {
                Console.Write("\b");
            }

            Console.Write(lenght);
        }

        private void DisplayCells()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int row = 0; row < _ocean.NumRows; row++)
            {
                for (int column = 0; column < _ocean.NumColumns; column++)
                {
                    if (_ocean[row, column] == null)
                    {
                        stringBuilder.Append(Ocean.DefaultCellImage);
                    }
                    else
                    {
                        _ocean[row, column].WasIterated = false;
                        stringBuilder.Append(_ocean[row, column].Image);
                    }

                }

                stringBuilder.Append("\n");
            }

            Console.WriteLine(stringBuilder);
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
        public int RequestValueAndAssign(string s)
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
                    Console.Clear();

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

            Thread.Sleep(2000);
        }
        #endregion

        #endregion
    }
}
