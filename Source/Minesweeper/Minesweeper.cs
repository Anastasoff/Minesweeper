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
            IOInterface userInterractor = new ConsoleInterface();
            Scoreboard scoreboard = Scoreboard.GetInstance;
            scoreboard.SetIOInterface(userInterractor);
            GameBoard gameBoard = GameBoard.GetInstance;
            CommandProcessor commandProcessor = new CommandProcessor(gameBoard, scoreboard, userInterractor);
            GameEngine engine = new GameEngine(commandProcessor, userInterractor, gameBoard);
            engine.Play();
        }
    }
}