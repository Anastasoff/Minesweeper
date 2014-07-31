namespace Minesweeper.Interfaces
{
    /// <summary>
    /// Defines the properties and methods of an IO Interface
    /// </summary>
    public interface IOInterface
    {
        string GetUserInput(string message);

        void ShowMessage(string message);

        void DrawBoard(IGameObject[,] board);

        void ShowWelcomeScreen();
    }
}