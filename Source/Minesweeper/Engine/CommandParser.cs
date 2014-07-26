namespace Minesweeper.Engine
{
    using GUI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CommandParser
    {
        private Dictionary<string, Command> commands = new Dictionary<string, Command>();

        public CommandParser()
        {
            this.commands.Add("exit", Command.Exit);
            this.commands.Add("top", Command.Top);
            this.commands.Add("restart", Command.Restart);
            this.commands.Add("flag", Command.Flag);
            this.commands.Add("system", Command.System);
        }

        public Command ExtractCommand(string[] inputCommands, GameBoard gameBoard)
        {
            if (!IsCommandValid(inputCommands))
            {
                return Command.InvalidInput;
            }

            Command command = Command.ValidMove;

            if (inputCommands.Length == 1 || inputCommands.Length == 3)
            {
                command = commands[inputCommands[0]];
            }
            else
            {
                if (!gameBoard.IsInsideBoard(int.Parse(inputCommands[0]), int.Parse(inputCommands[1])))
                {
                    command = Command.InvalidMove;
                }
            }

            return command;
        }

        private bool IsCommandValid(string[] commandElements)
        {
            bool commandResult;

            switch (commandElements.Length)
            {
                case 1:
                    commandResult = (commands.ContainsKey(commandElements[0].ToLower())) && (commandElements[0].ToLower() != "flag");
                    break;

                case 2:
                    int row = -1;
                    int col = -1;
                    bool isValidCoords = int.TryParse(commandElements[0], out row);
                    commandResult = isValidCoords && int.TryParse(commandElements[1], out col);
                    break;

                case 3:
                    string[] coords = new string[2] { commandElements[1], commandElements[2] };
                    commandResult = (commandElements[0].ToLower() == "flag") && IsCommandValid(coords);
                    break;

                default:
                    commandResult = false;
                    break;
            }

            return commandResult;
        }
    }
}
