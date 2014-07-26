namespace Minesweeper.Engine
{
    using GUI;
    using GameObjects;
    using System;
    using System.Collections.Generic;

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

        public Command ExtractCommand(string input, GameBoard gameBoard)
        {
            Command command;
            CommandType cmdType;
            Position coordinates = new Position(0, 0);

            string[] inputCommands = input.Split(' ');

            cmdType = GetCommandType(inputCommands);
            if (cmdType == CommandType.Flag || cmdType == CommandType.ValidMove)
            {
                try
                {
                    coordinates = GetCoordinates(inputCommands, cmdType);
                }
                catch (Exception)
                {
                    cmdType = CommandType.InvalidMove;
                }
            }

            if (!gameBoard.IsInsideBoard(coordinates))
            {
                return new Command(CommandType.InvalidMove);
            }
            command = new Command(cmdType, coordinates);
            return command;
        }

        private Position GetCoordinates(string[] inputCommands, CommandType cmdType)
        {
            int row;
            int col;
            if (cmdType == CommandType.ValidMove)
            {
                row = int.Parse(inputCommands[0]);
                col = int.Parse(inputCommands[1]);
            }
            else
            {
                row = int.Parse(inputCommands[1]);
                col = int.Parse(inputCommands[2]);
            }

            Position coordinates = new Position(row, col);
            return coordinates;
        }

        private CommandType GetCommandType(string[] inputCommands)
        {
            string commandType = inputCommands[0];
            switch (commandType)
            {
                case "flag":
                    return CommandType.Flag;
                case "restart":
                    return CommandType.Restart;
                case "exit":
                    return CommandType.Exit;
                case "top":
                    return CommandType.Top;
                case "system":
                    return CommandType.System;
                default:
                    return ValidateIfCommandIsValid(inputCommands[0]);
            }
        }

        private CommandType ValidateIfCommandIsValid(string input)
        {
            int possibleCoordinate;

            if (int.TryParse(input, out possibleCoordinate))
            {
                return CommandType.ValidMove;
            }
            else if (this.commands.ContainsKey(input))
            {
                return CommandType.ValidMove;
            }
            else
            {
                return CommandType.InvalidInput;
            }
        }
    }
}