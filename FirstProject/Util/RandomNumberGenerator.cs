using System;

namespace OceanSimulationInConsole
{
    internal class RandomNumberGenerator
    {
        #region Readonly
        private readonly Random random; // field for generating random numbers
        private readonly IOceanLength ocean; // field for interacting with the ocean
        #endregion

        #region Properties
        public Random Random { get { return random; } }
        #endregion

        #region Ctors
        public RandomNumberGenerator(IOceanLength ocean)
        {
            random = new Random();
            this.ocean = ocean;
        }
        #endregion

        #region Methods
        public Coordinate GetEmptyCellCoord()
        {
            int x, y;

            do
            {
                x = random.Next(0, ocean.NumRows);
                y = random.Next(0, ocean.NumColumns);
            }
            while (ocean[x, y] != null);

            return new Coordinate(x, y);
        }
        #endregion
    }
}
