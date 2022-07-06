

namespace OceanSimulationInConsole
{
    internal class Obstacle : Cell
    {
        #region Consts
        private const char DefaultObstacleImage = '#';
        #endregion

        #region CTORS
        public Obstacle(Coordinate coordinate, IOcean ocean) : base(coordinate, ocean)
        {
            _image = DefaultObstacleImage;
        }
        #endregion

        #region Methods
        public override void Process()
        {

        }

        protected override Cell Reproduce(Coordinate coordinate)
        {
            return this;
        }
        #endregion
    }
}
