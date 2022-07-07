
namespace OceanSimulationInConsole
{
    internal interface IOcean : IOceanNumberOfAllObjects
    {
        Coordinate GetNeighborPreyCoord(Coordinate currentCoordinate);

        Coordinate GetEmptyNeighborCoord(Coordinate currentCoordinate);

        void MoveFrom(Coordinate from, Coordinate to);
    }
}
