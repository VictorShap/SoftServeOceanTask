
namespace OceanSimulationInConsole
{
    internal interface IOceanViewer
    {
        int RequestValuesAndAssignThem(string valueName);

        void DisplayGameState(GameState gameState);

        void DisplayIteration();

        void DisplayValidationMessage(bool wasFormatException);
    }
}
