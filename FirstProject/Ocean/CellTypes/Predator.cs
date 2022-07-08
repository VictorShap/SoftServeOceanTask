
namespace OceanSimulationInConsole
{
    internal class Predator : Prey
    {
        #region Consts
        private const char DefaultPredatorImage = 'S';
        private const int TimeToFeedDefault = 6;
        #endregion

        #region Fields
        private int _timeToFeed; // the number of iterations after which the predator dies
        #endregion

        #region Ctors
        public Predator(Coordinate coordinate, IOcean ocean) : base(coordinate, ocean)
        {
            _image = DefaultPredatorImage;
            this._timeToFeed = TimeToReproduceDefault;
        }
        #endregion

        #region Methods
        public override void Process()
        {
            Coordinate toCoordinate;

            if (--_timeToFeed <= 0)
            {
                _owner.NumPredators = _owner.NumPredators - 1;
                _owner[Offset] = null;
            }
            else
            {
                toCoordinate = _owner.GetNeighborPreyCoord(Offset);

                if (toCoordinate != Offset)
                {
                    _owner.NumPrey = _owner.NumPrey - 1;
                    _timeToFeed = TimeToFeedDefault;
                    _timeToReproduce = _timeToReproduce - 1;

                    _owner.MoveFrom(Offset, toCoordinate);
                }
                else
                {
                    base.Process();
                }
            }
        }

        protected override Cell Reproduce(Coordinate coordinate)
        {
            if (coordinate != Offset)
            {
                _owner.NumPredators = _owner.NumPredators + 1;
            }

            return new Predator(coordinate, _owner);
        }
        #endregion
    }
}
