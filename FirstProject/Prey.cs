
namespace FirstProject
{
    internal class Prey : Cell
    {
        #region CONSTS
        public const char DefaultPreyImage = 'f';
        protected const int TimeToReproduceDefault = 6;
        #endregion

        #region FIELDS
        protected int _timeToReproduce;
        #endregion

        #region CTORS
        public Prey(Coordinate coordinate, Ocean ocean) : base(coordinate, ocean)
        {
            _image = DefaultPreyImage;
            this._timeToReproduce = TimeToReproduceDefault;
        }
        #endregion

        #region METHODS
        public override void Process()
        {
            Coordinate toCoord = _owner.GetEmptyNeighborCoord(this.Offset);

            if (wasIterated)
            {
                return;
            }

            wasIterated = true;

            if (--_timeToReproduce <= 0)
            {
                Cell redproducedCell = Reproduce(toCoord);
                redproducedCell.wasIterated = true;
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

            return new Prey(coordinate, this._owner);
        }
        #endregion
    }
}
