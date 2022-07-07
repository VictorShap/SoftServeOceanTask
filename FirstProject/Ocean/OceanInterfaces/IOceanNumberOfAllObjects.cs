
namespace OceanSimulationInConsole
{
    internal interface IOceanNumberOfAllObjects : IOceanIndexer
    {
        int NumObstacles { get; set; }
        int NumPredators { get; set; }
        int NumPrey { get; set; }
    }
}
