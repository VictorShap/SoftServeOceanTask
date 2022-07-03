using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject
{
    internal class Predator : Prey
    {
        private int timeToFeed;

        public Predator(Coordinate coordinate, int timeToFeed = 6) : base(coordinate)
        {
            image = 'S';
            this.timeToFeed = timeToFeed;
        }

        public override void Process()
        {
            Coordinate toCoordinate;

            if (isBeenIterated)
            {
                return;
            }

            if (--timeToFeed <= 0)
            {
                owner.NumPredators = owner.NumPredators - 1;

                AssignCellAt(Offset, new Cell(Offset));
            }
            else
            {
                toCoordinate = GetNeighborPreyCoord();

                if (toCoordinate != Offset)
                {
                    owner.NumPrey = owner.NumPrey - 1;
                    timeToFeed = 6;
                    timeToReproduce = timeToReproduce - 1;

                    MoveFrom(Offset, toCoordinate);
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
            owner.NumPredators = owner.NumPredators + 1;
            return new Predator(coordinate);
        }
    }
}
