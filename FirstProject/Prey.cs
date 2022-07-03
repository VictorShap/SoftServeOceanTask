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
        protected int timeToReproduce;

        public Prey(Coordinate coordinate, int timeToReproduce = 6) : base(coordinate)
        {
            image = 'f';
            this.timeToReproduce = timeToReproduce;
        }

        public override void Process()
        {
            Coordinate toCoord = GetEmptyNeighborCoord();

            if (isBeenIterated)
            {
                return;
            }

            isBeenIterated = true;

            if (--timeToReproduce <= 0)
            {
                AssignCellAt(toCoord, Reproduce(toCoord));
                timeToReproduce = 6;
            }
            else
            {
                MoveFrom(Offset, toCoord);
            }

        }

        protected void MoveFrom(Coordinate from, Coordinate to)
        {
            if (from != to)
            {
                Offset = to;

                AssignCellAt(to, this);
                AssignCellAt(from, new Cell(from));
            }
        }

        protected override Cell Reproduce(Coordinate coordinate)
        {
            owner.NumPrey = owner.NumPrey + 1;
            return new Prey(coordinate);
        }
    }
}
