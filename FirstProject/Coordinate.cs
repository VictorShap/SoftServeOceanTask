using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject
{
    internal struct Coordinate
    {
        #region FIELDS
        public int X { get; set; }
        public int Y { get; set; }
        #endregion

        #region CTORS
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
        #endregion

        #region OPERATORS
        public static bool operator ==(Coordinate coordinate1, Coordinate coordinate2)
        {
            return (coordinate1.X == coordinate2.X) && (coordinate1.Y == coordinate2.Y);
        }

        public static bool operator !=(Coordinate coordinate1, Coordinate coordinate2)
        {
            return (coordinate1.X != coordinate2.X) || (coordinate1.Y != coordinate2.Y);
        }
        #endregion
    }
}
