namespace Minesweeper.Engine
{
    using System;
    using GUI;

    public class GameEngine
    {
        private CommandProcessor commandProcessor;

        public GameEngine(CommandProcessor processor)
        {
            this.commandProcessor = processor;
        }

        public void Play()
        {
            this.commandProcessor.ShowWelcomeMessage();
            this.commandProcessor.Gamefield.Display();

            while (true)
            {
                string input = ReadInput();
                commandProcessor.ExecuteCommand(input);
            }
        }

        private string ReadInput() // new function to reduce the mess in the while(true) loop
        {
            Console.Write("Enter row and column: "); // TODO: "or another command"
            string commandRead = Console.ReadLine();
            commandRead = commandRead.Trim();
            return commandRead;
        }
    }
}
