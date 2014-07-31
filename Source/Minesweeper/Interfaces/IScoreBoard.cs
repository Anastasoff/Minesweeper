namespace Minesweeper.Interfaces
{
    public interface IScoreBoard
    {
        void SetIOInterface(IOInterface userInterractor);

        void AddPlayer(int score);

        void ShowHighScores();

        int Count();
    }
}
