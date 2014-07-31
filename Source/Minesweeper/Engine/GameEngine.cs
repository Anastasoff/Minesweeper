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
            this.userIterractor.ShowWelcomeScreen();
            this.userIterractor.DrawBoard(this.board.Board);
            while (true)
            {
                string input = this.userIterractor.GetUserInput("Enter row and column: ");
                this.commandProcessor.ExecuteCommand(input);
            }
        }
    }
}
