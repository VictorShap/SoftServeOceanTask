
namespace OceanSimulationInConsole
{
    internal class Prey : Cell
    {
        #region Consts
        public const char DefaultPreyImage = 'f';
        protected const int TimeToReproduceDefault = 6;
        #endregion

        #region Fields
        protected int _timeToReproduce;
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

            if (WasIterated)
            {
                return;
            }

            WasIterated = true;

            if (--_timeToReproduce <= 0)
            {
                Cell redproducedCell = Reproduce(toCoord);
                redproducedCell.WasIterated = true;
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
