namespace Minesweeper.Interfaces
{
    /// <summary>
    /// Defines the methods of a scoreboard
    /// </summary>
    public interface IScoreBoard
    {
        void SetIOInterface(IOInterface userInterractor);

        void AddPlayer(int score);

        void ShowHighScores();

        int Count();
    }
}