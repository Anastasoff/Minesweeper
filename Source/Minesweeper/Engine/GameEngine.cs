namespace Minesweeper.Engine
{
    using GUI;
    using System;
    using Interfaces;

    public class GameEngine
    {
        private CommandProcessor commandProcessor;
        private IOInterface userIterractor;

        public GameEngine(CommandProcessor processor, IOInterface iterractor)
        {
            this.commandProcessor = processor;
            this.userIterractor = iterractor;
        }

        public void Play()
        {
            userIterractor.showWelcomeScreen();
            this.commandProcessor.GameBoard.Display();
            while (true)
            {
                string input = userIterractor.getUserInput("Enter row and column: ");
                commandProcessor.ExecuteCommand(input);
            }
        }
    }
}
