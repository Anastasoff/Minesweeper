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
        private Dictionary<string, CommandType> commands = new Dictionary<string, CommandType>();

        public CommandParser()
        {
            this.commands.Add("exit", CommandType.Exit);
            this.commands.Add("top", CommandType.Top);
            this.commands.Add("restart", CommandType.Restart);
            this.commands.Add("flag", CommandType.Flag);
            this.commands.Add("system", CommandType.System);
        }

        public CommandType ExtractCommand(string[] inputCommands, GameBoard gameBoard)
        {
            if (!IsCommandValid(inputCommands))
            {
                return CommandType.InvalidInput;
            }

            CommandType command = CommandType.ValidMove;

            if (inputCommands.Length == 1 || inputCommands.Length == 3)
            {
                command = commands[inputCommands[0]];
            }
            else
            {
                if (!gameBoard.IsInsideBoard(int.Parse(inputCommands[0]), int.Parse(inputCommands[1])))
                {
                    command = CommandType.InvalidMove;
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
