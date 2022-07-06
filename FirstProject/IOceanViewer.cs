
namespace OceanSimulationInConsole
{
    internal interface IOceanViewer
    {
        int RequestValuesAndAssignThem(string valueName);

        void DisplayGameState(GameState gameState);

        void DisplayStats(int iteration);

        void DisplayCells(int numRows, int numColumns);

        void DisplayBorder();

        void DisplayValidationMessage(bool wasFormatException);
    }
}
