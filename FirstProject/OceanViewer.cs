using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject
{
    internal class OceanViewer
    {
        private readonly Ocean _ocean;

        public OceanViewer(Ocean ocean)
        {
            _ocean = ocean;
        }

        public int RequestValuesAndAssignThem(string s)
        {
            int number;

            Console.WriteLine("Enter the number of {0}", s);
            number = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("The number of {0} accepted " + number, s);

            return number;
        }

        public void Start(string message = "Starting...")
        {
            Console.WriteLine(message);
        }

        public void End(string message = "Simulation has been ended")
        {
            Console.WriteLine(message);
            Console.ReadKey();
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
                        _ocean[row, column].isBeenIterated = false;
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

        public void Continue(string message = "Press any key to continue")
        {
            Console.Write(message);
            Console.ReadKey();
        }

        public void ValidateProperties()
        {
            Console.WriteLine("Invalid value, so it will be set to maximum possible value");
        }

        public void ValidateInput()
        {
            Console.WriteLine("Invalid input, so everything will be set to its default value");
        }
    }
}
