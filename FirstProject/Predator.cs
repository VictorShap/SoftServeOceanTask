using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject
{
    internal class Predator : Prey
    {
        #region CONSTS
        private const char DefaultPredatorImage = 'S';
        private const int TimeToFeedDefault = 6;
        #endregion

        #region FIELDS
        private int _timeToFeed;
        #endregion

        #region CTORS
        public Predator(Coordinate coordinate, Ocean ocean) : base(coordinate, ocean)
        {
            _image = DefaultPredatorImage;
            this._timeToFeed = TimeToReproduceDefault;
        }
        #endregion

        #region METHODS
        public override void Process()
        {
            Coordinate toCoordinate;

            if (wasIterated)
            {
                return;
            }

            if (--_timeToFeed <= 0)
            {
                _owner.NumPredators = _owner.NumPredators - 1;
                _owner[Offset] = null;
            }
            else
            {
                toCoordinate = _owner.GetNeighborPreyCoord(this.Offset);

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

            wasIterated = true;
        }

        protected override Cell Reproduce(Coordinate coordinate)
        {
            if (coordinate != Offset)
            {
                _owner.NumPredators = _owner.NumPredators + 1;
            }

            return new Predator(coordinate, this._owner);
        }
        #endregion
    }
}
