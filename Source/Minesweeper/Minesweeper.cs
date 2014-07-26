namespace Minesweeper
{
    using Engine;
    using GUI;
    using Interfaces;
    using GUI.ConsoleSkins;

    public class Minesweeper
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main()
        {
            // TODO: Add instance of the GameRenderingEngine and call that instance when creating the game engine
            var brightSkin = new BrightSkin();
            IOInterface userInterractor = new ConsoleInterface(brightSkin);
            Scoreboard scoreboard = Scoreboard.GetInstance;
            scoreboard.SetIOInterface(userInterractor);
            GameBoard gameBoard = GameBoard.GetInstance;
            CommandProcessor commandProcessor = new CommandProcessor(gameBoard, scoreboard, userInterractor, new CommandParser());
            GameEngine engine = new GameEngine(commandProcessor, userInterractor, gameBoard);
            engine.Play();
        }
    }
}