namespace Minesweeper
{
    using Engine;
    using GUI;
    using Interfaces;

    public class Minesweeper
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main()
        {
            // TODO: Add instance of the GameRenderingEngine and call that instance when creating the game engine
            Scoreboard scoreboard = Scoreboard.GetInstance;
            GameBoard gameBoard = GameBoard.GetInstance;
            IOInterface userInterractor = new ConsoleInterface();
            CommandProcessor commandProcessor = new CommandProcessor(gameBoard, scoreboard,userInterractor);
            GameEngine engine = new GameEngine(commandProcessor,userInterractor);
            engine.Play();
        }
    }
}