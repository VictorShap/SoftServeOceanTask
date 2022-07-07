
namespace OceanSimulationInConsole
{
    internal interface IOceanIndexer
    {
        Cell this[int x, int y] { get; }

        Cell this[Coordinate coordinate] { set; }
    }
}
