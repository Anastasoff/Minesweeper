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
            Scoreboard scoreboard = Scoreboard.GetInstance;
            GameBoard board = GameBoard.GetInstance; // calling singleton
            CommandProcessor commandProcessor = new CommandProcessor(board, scoreboard);
            GameEngine engine = new GameEngine(commandProcessor);
            engine.Play();
        }
    }
}