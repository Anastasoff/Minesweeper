namespace Minesweeper.Engine
{
    using System;

    public class GameEngine
    {
        private CommandProcessor commandProcessor;

        public GameEngine(GameBoard gameboard, Scoreboard scoreboard)// changed both classes' access to internal
        {           
            this.commandProcessor = new CommandProcessor(gameboard, scoreboard); // ************NEW
        }

        public void Play()
        {
            this.commandProcessor.ShowWelcomeMessage();
            this.commandProcessor.gameboard.Display();

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
