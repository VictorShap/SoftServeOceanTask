using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject
{
    internal class Predator : Prey
    {
        private int _timeToFeed;

        public Predator(Coordinate coordinate, Ocean ocean, int timeToFeed = 6) : base(coordinate, ocean)
        {
            _image = 'S';
            this._timeToFeed = timeToFeed;
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
                Owner.NumPredators = Owner.NumPredators - 1;

                Owner.AssignCellAt(Offset, null);
            }
            else
            {
                toCoordinate = Owner.GetNeighborPreyCoord(this.Offset);

                if (toCoordinate != Offset)
                {
                    Owner.NumPrey = Owner.NumPrey - 1;
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
            Owner.NumPredators = Owner.NumPredators + 1;
            return new Predator(coordinate, this.Owner);
        }
    }
}
