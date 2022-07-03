using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject
{
    internal class Cell
    {
        public Ocean owner;
        protected char image;
        private Coordinate offset;

        public bool isBeenIterated = false;
        public Coordinate Offset
        {
            get
            {
                return offset;
            }
            protected set
            {
                offset = value;
            }
        }
        public char Image
        {
            get => image;
        }

        public Cell(Coordinate offset, char image = '-')
        {
            this.image = image;
            Offset = offset;
        }

        public void Display()
        {
            Console.Write(Image);
        }

        protected virtual Cell Reproduce(Coordinate coordinate)
        {
            return new Cell(coordinate);
        }

        public virtual void Process()
        {

        }

        private Cell GetNeighborWithImage(char image)
        {
            Cell[] neighbors = new Cell[4];
            int count = 0;
            char north = North().Image;
            char south = South().Image;
            char east = East().Image;
            char west = West().Image;

            if (north == image)
            {
                neighbors[count++] = North();
            }

            if (south == image)
            {
                neighbors[count++] = South();
            }

            if (east == image)
            {
                neighbors[count++] = East();
            }

            if (west == image)
            {
                neighbors[count++] = West();
            }

            if (count == 0)
            {
                return this;
            }
            else
            {
                return neighbors[owner.random.Next(0, count - 1)];
            }
        }

        protected Coordinate GetEmptyNeighborCoord()
        {
            return GetNeighborWithImage('-').Offset;
        }

        protected Coordinate GetNeighborPreyCoord()
        {
            return GetNeighborWithImage('f').Offset;
        }

        protected Cell GetCellAt(Coordinate coordinate)
        {
            return owner.cells[coordinate.X, coordinate.Y];
        }

        protected virtual void AssignCellAt(Coordinate coordinate, Cell cell)
        {
            owner.cells[coordinate.X, coordinate.Y] = cell;
        }

        private Cell East()
        {
            int x;
            x = Offset.X == owner.NumRows - 1 ? Offset.X : (Offset.X + 1);

            return owner.cells[x, Offset.Y];
        }

        private Cell West()
        {
            int x;
            x = Offset.X == 0 ? Offset.X : (Offset.X - 1);

            return owner.cells[x, Offset.Y];
        }

        private Cell North()
        {
            int y;
            y = Offset.Y == 0 ? Offset.Y : (Offset.Y - 1);

            return owner.cells[Offset.X, y];
        }

        private Cell South()
        {
            int y;
            y = Offset.Y == owner.NumColumns - 1 ? Offset.Y : (Offset.Y + 1);

            return owner.cells[Offset.X, y];
        }
    }
}
