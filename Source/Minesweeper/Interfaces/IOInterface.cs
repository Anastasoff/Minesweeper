namespace Minesweeper.Interfaces
{
    public interface IOInterface
    {
        string GetUserInput(string message);
        void ShowMessage(string message);
        void ShowMessage();
        void ClearScreen();
        void DrawBoard(char[,] board);
        void ShowWelcomeScreen();
    }
}
