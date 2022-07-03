using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject
{
    internal class Prey : Cell
    {
        const int TimeToReproduceDefault = 6;
        protected int _timeToReproduce;

        public Prey(Coordinate coordinate, Ocean ocean, int timeToReproduce = 6) : base(coordinate, ocean)
        {
            _image = 'f';
            this._timeToReproduce = timeToReproduce;
        }

        public override void Process()
        {
            Coordinate toCoord = Owner.GetEmptyNeighborCoord(this.Offset);

            if (isBeenIterated)
            {
                return;
            }

            isBeenIterated = true;

            if (--_timeToReproduce <= 0)
            {
                Owner.AssignCellAt(toCoord, Reproduce(toCoord));
                _timeToReproduce = 6;
            }
            else
            {
                _owner.MoveFrom(Offset, toCoord);
            }

        }

        protected override Cell Reproduce(Coordinate coordinate)
        {
            Owner.NumPrey = Owner.NumPrey + 1;
            return new Prey(coordinate, this.Owner);
        }
    }
}
