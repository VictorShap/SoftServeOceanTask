
namespace OceanSimulationInConsole
{
    internal interface IOceanViewer
    {
        int RequestValueAndAssign(string valueName);

        void DisplayGameState(GameState gameState);

        void DisplayIteration();

        void DisplayValidationMessage(bool wasFormatException);
    }
}
