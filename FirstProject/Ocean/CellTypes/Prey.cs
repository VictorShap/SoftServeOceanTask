
namespace OceanSimulationInConsole
{
    internal class Prey : Cell
    {
        #region Consts
        private const int TimeToReproduceDefault = 6;

        public const char DefaultPreyImage = 'f';
        #endregion

        #region Fields
        protected int _timeToReproduce; // the number of iterations after which the prey reproduces
        #endregion

        #region Ctors
        public Prey(Coordinate coordinate, IOcean ocean) : base(coordinate, ocean)
        {
            _image = DefaultPreyImage;
            this._timeToReproduce = TimeToReproduceDefault;
        }
        #endregion

        #region Methods
        public override void Process()
        {
            Coordinate toCoord = _owner.GetEmptyNeighborCoord(Offset);

            if (--_timeToReproduce <= 0)
            {
                Cell redproducedCell = Reproduce(toCoord);
                _timeToReproduce = TimeToReproduceDefault;

                _owner[toCoord] = redproducedCell;
            }
            else
            {
                _owner.MoveFrom(Offset, toCoord);
            }
        }

        protected override Cell Reproduce(Coordinate coordinate)
        {
            if (coordinate != Offset)
            {
                _owner.NumPrey = _owner.NumPrey + 1;
            }

            return new Prey(coordinate, _owner);
        }
        #endregion
    }
}
