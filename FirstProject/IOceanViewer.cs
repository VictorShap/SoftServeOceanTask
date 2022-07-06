

namespace FirstProject
{
    internal interface IOceanViewer
    {
        int RequestValuesAndAssignThem(string valueName);

        void DisplayGameState(GameStates gameState);

        void DisplayStats(int iteration);

        void DisplayCells(int numRows, int numColumns);

        void DisplayBorder();

        void DisplayValidationMessage(bool wasFormatException);
    }
}
