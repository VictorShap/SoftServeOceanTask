
namespace OceanSimulationInConsole
{
    internal interface IOceanViewer
    {
        int RequestValueAndAssign(string valueName, int defaultValue);

        void DisplayGameState(GameState gameState);

        void DisplayIteration();

        void DisplayValidationMessage(bool wasFormatException);
    }
}
