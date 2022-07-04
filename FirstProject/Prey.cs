using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject
{
    internal class Prey : Cell
    {
        public const char DefaultPreyImage = 'f';
        protected const int TimeToReproduceDefault = 6;
        protected int _timeToReproduce;

        public Prey(Coordinate coordinate, Ocean ocean) : base(coordinate, ocean)
        {
            _image = DefaultPreyImage;
            this._timeToReproduce = TimeToReproduceDefault;
        }

        public override void Process()
        {
            Coordinate toCoord = _owner.GetEmptyNeighborCoord(this.Offset);

            if (isBeenIterated)
            {
                return;
            }

            isBeenIterated = true;

            if (--_timeToReproduce <= 0)
            {
                Cell redproducedCell = Reproduce(toCoord);
                redproducedCell.isBeenIterated = true;
                _timeToReproduce = TimeToReproduceDefault;

                _owner.AssignCellAt(toCoord, redproducedCell);
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
    }
}
