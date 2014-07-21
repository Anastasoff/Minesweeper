namespace Minesweeper.Engine
{
    using System;

    using GUI;
    using Interfaces;

    public class GameEngine
    {
        private CommandProcessor commandProcessor;
        private IOInterface userIterractor;
        private GameBoard board;

        public GameEngine(CommandProcessor processor, IOInterface iterractor, GameBoard board)
        {
            this.commandProcessor = processor;
            this.userIterractor = iterractor;
            this.board = board;
        }

        public void Play()
        {
            userIterractor.ShowWelcomeScreen();
            userIterractor.DrawBoard(board.Board);
            while (true)
            {
                string input = userIterractor.GetUserInput("Enter row and column: ");
                commandProcessor.ExecuteCommand(input);
            }
        }
    }
}
