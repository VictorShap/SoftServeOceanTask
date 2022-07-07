
namespace OceanSimulationInConsole
{
    internal interface IOceanLength : IOceanIndexer
    {
        int NumColumns { get; }
        int NumRows { get; }
    }
}
