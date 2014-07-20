namespace Minesweeper.Interfaces
{
    using GameObjects;

    public interface IOInterface
    {
        string GetUserInput(string message);
        void ShowMessage(string message);
        void ShowMessage();
        void ClearScreen();
        void DrawBoard(Cell[,] board);
        void ShowWelcomeScreen();
    }
}
