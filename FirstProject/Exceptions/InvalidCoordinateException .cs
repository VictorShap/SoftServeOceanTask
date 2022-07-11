
namespace OceanSimulationInConsole
{
    internal class InvalidCoordinateException : System.ApplicationException
    {
        public readonly (int, int) Values;

        public InvalidCoordinateException(string message, int x, int y) : base(message)
        {
            Values.Item1 = x;
            Values.Item2 = y;
        }
    }
}
