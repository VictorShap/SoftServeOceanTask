
namespace OceanSimulationInConsole
{
    internal struct Coordinate
    {
        #region Fields
        public int X { get; set; }
        public int Y { get; set; }
        #endregion

        #region Ctors
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
        #endregion

        #region Operators
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
