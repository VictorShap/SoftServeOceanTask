using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject
{
    internal struct Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(Coordinate coordinate1, Coordinate coordinate2)
        {
            return (coordinate1.X == coordinate2.X) && (coordinate1.Y == coordinate2.Y);
        }

        public static bool operator !=(Coordinate coordinate1, Coordinate coordinate2)
        {
            return (coordinate1.X != coordinate2.X) || (coordinate1.Y != coordinate2.Y);
        }
    }
}
