
namespace OceanSimulationInConsole
{
    internal struct Coordinate
    {
        #region Readonly
        private readonly int _x;
        private readonly int _y;
        #endregion

        #region Properties
        public int X { get => _x; }
        public int Y { get => _y; }
        #endregion

        #region Ctors
        public Coordinate(int x, int y)
        {
            _x = x;
            _y = y;
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
