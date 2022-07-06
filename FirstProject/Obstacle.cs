

namespace FirstProject
{
    internal class Obstacle : Cell
    {
        #region CONSTS
        private const char DefaultObstacleImage = '#';
        #endregion

        #region CTORS
        public Obstacle(Coordinate coordinate, Ocean ocean) : base(coordinate, ocean)
        {
            _image = DefaultObstacleImage;
        }
        #endregion

        #region METHODS
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
