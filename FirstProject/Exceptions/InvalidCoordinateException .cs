
namespace OceanSimulationInConsole
{
    internal class InvalidCoordinateException : System.Exception
    {

        #region Properties
        public int X { get; private set; }
        public int Y { get; private set; }
        #endregion

        #region Ctors
        public InvalidCoordinateException(string message, int x, int y) : base(message)
        {
            X = x;
            Y = y;
        }
        #endregion
    }
}
