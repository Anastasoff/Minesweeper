namespace Minesweeper.Engine
{
    using System;

    using GUI;
    using Interfaces;
        
    /// <summary>
    /// A facade of the game logic
    /// </summary>
    public class GameEngine
    {
        private CommandProcessor commandProcessor;
        private IOInterface userInteractor;
        private GameBoard board;

        /// <summary>
        /// Creates an instance of the GameEngine class
        /// </summary>
        /// <param name="processor">The command processor to be used by the engine</param>
        /// <param name="interactor">The user interactor to be used by the engine</param>
        /// <param name="board">The game board to be used by the engine</param>
        public GameEngine(CommandProcessor processor, IOInterface interactor, GameBoard board)
        {
            this.commandProcessor = processor;
            this.userInteractor = interactor;
            this.board = board;
        }

        /// <summary>
        /// Starts the game
        /// </summary>
        public void Play()
        {
            this.userInteractor.ShowWelcomeScreen();
            this.userInteractor.DrawBoard(this.board.Board);
            while (true)
            {
                string input = this.userInteractor.GetUserInput("Enter row and column: ");
                this.commandProcessor.ExecuteCommand(input);
            }
        }
    }
}
