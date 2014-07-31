namespace Minesweeper.Interfaces
{
    using GameObjects;

    public interface IOInterface
    {
        string GetUserInput(string message);
        void ShowMessage(string message);
        void DrawBoard(IGameObject[,] board);
        void ShowWelcomeScreen();
    }
}
