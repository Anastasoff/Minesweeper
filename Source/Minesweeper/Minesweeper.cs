namespace Minesweeper
{
    using Engine;
    using GUI;

    public class Minesweeper
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main()
        {
            Scoreboard scoreboard = Scoreboard.GetTop5;
            GameBoard board = GameBoard.GetInstance; // calling singleton
            GameEngine engine = new GameEngine(board, scoreboard);
            engine.Play();
        }
    }
}