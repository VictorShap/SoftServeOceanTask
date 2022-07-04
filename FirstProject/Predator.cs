using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject
{
    internal class Predator : Prey
    {
        const int TimeToReproduceDefault = 6;
        private int _timeToFeed;

        public Predator(Coordinate coordinate, Ocean ocean) : base(coordinate, ocean)
        {
            _image = 'S';
            this._timeToFeed = TimeToReproduceDefault;
        }

        public override void Process()
        {
            Coordinate toCoordinate;

            if (isBeenIterated)
            {
                return;
            }

            if (--_timeToFeed <= 0)
            {
                _owner.NumPredators = _owner.NumPredators - 1;

                _owner.AssignCellAt(Offset, null);
            }
            else
            {
                toCoordinate = _owner.GetNeighborPreyCoord(this.Offset);

                if (toCoordinate != Offset)
                {
                    _owner.NumPrey = _owner.NumPrey - 1;
                    _timeToFeed = 6;
                    _timeToReproduce = _timeToReproduce - 1;

                    _owner.MoveFrom(Offset, toCoordinate);
                }
                else
                {
                    base.Process();
                }
            }

            isBeenIterated = true;
        }

        protected override Cell Reproduce(Coordinate coordinate)
        {
            _owner.NumPredators = _owner.NumPredators + 1;
            return new Predator(coordinate, this._owner);
        }
    }
}
