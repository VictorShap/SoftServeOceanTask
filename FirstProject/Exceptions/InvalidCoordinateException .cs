
namespace OceanSimulationInConsole
{
    internal class InvalidCoordinateException : System.Exception
    {
        #region Readonly
        private readonly int _x;
        private readonly int _y;
        #endregion

        #region Ctors
        public InvalidCoordinateException()
        {

        }

        public InvalidCoordinateException(string message) : base(message)
        {

        }

        public InvalidCoordinateException(string message, System.Exception innerException) : base(message, innerException)
        {

        }

        public InvalidCoordinateException(string message, int x, int y) : base(message)
        {
            _x = x;
            _y = y;
        }
        #endregion

        #region Properties
        public int X { get => _x; }
        public int Y { get => _y; }
        #endregion

    }
}
