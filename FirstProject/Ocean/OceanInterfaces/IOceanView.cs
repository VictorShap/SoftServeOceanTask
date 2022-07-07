
namespace OceanSimulationInConsole
{
    internal interface IOceanView : IOceanLength, IOceanNumberOfAllObjects
    {
        int CurrentIteration { get; }
    }
}
