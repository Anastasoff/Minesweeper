namespace Minesweeper.Engine
{
    using GameObjects;
    using GUI;
    using System;
    using System.Linq;

    public class CommandProcessor
    {
        private GameBoard gameboard; 
        private Scoreboard scoreboard;

        public CommandProcessor(GameBoard gameboard, Scoreboard scoreboard)
        {
            this.Gamefield = gameboard;
            this.Score = scoreboard;
        }

        public GameBoard Gamefield 
        { 
            get
            {
                return this.gameboard;
            }

            set 
            {
                this.gameboard = value;
            }
        }

        public Scoreboard Score
        {
            get
            {
                return this.scoreboard;
            }

            set
            {
                this.scoreboard = value;
            }
        }

        private bool CheckIfValidInputCommand(string inputCommand)
        {
            string[] validCommands = new string[] { "exit", "restart", "top", "flag" };
            for (int i = 0; i < validCommands.Length; i++)
            {
                string currentCommand = validCommands[i];
                if (currentCommand == inputCommand.ToLower())
                {
                    return true;
                }
            }

            return false;
        }

        private string[] ConvertInputStringToStringArray(string input)
        {
            string[] inputToArr = input.Split(' ');

            return inputToArr;
        }
        private Command ExtractCommand(string[] inputCommands)
        {
            Command command = Command.ValidMove;

            if (inputCommands.Length == 1 || inputCommands.Length == 3)
            {
                string inputCommand = inputCommands[0];
                if (CheckIfValidInputCommand(inputCommand))
                {
                    command = GetCommandFromInput(inputCommand);
                }
                else
                {
                    command = Command.InvalidInput;
                }
            }

            return command;
        }

        public void ExecuteCommand(string input)
        {
            string[] commandsArr = ConvertInputStringToStringArray(input);
            Command command = ExtractCommand(commandsArr);
            //    try
            //    {
            //        command = ParseInputString(input); //GetCommandFromInput(input);
            //    }
            //    catch (ArgumentException)
            //    {
            //        command = Command.ValidMove;
            //    }
            switch (command)
            {
                case Command.InvalidMove: ProcessInvalidMove();
                    break;
                case Command.Exit: ProcessExitCommand();
                    break;
                case Command.Top: ProcessTopCommand();
                    break;
                case Command.Restart: ProcessRestartCommand();
                    break;
                case Command.Flag: ProcessFlagCommand(commandsArr);
                    break;
                case Command.InvalidInput: ProcessInvalidInputCommand();
                    break;
                default: ProcessCoordinates(commandsArr);
                    break;
            }
        }

        private Command GetCommandFromInput(string input)
        {
            switch (input)
            {
                case "exit": return Command.Exit;
                case "top": return Command.Top;
                case "restart": return Command.Restart;
                case "flag": return Command.Flag;
                default: throw new ArgumentException("Input not a valid command");
            }
        }

        private void ProcessFlagCommand(string[] commandsArr)
        {
            string[] coordsArr = commandsArr.Skip(1).ToArray();
            int[] coordinates = ParseInputCoordinates(coordsArr);
            int row = coordinates[0];
            int col = coordinates[1];

            if (CheckIfValidCoordinates(row, col))
            {
                Console.WriteLine("Illegal move!"); // to be put in the Renderer
                Console.WriteLine();
            }
            else
            {
                gameboard.PlaceFlag(row, col);
                gameboard.Display();
            }
        }

        private void ProcessInvalidInputCommand()
        {
            Console.WriteLine("Invalid input! Please try again!");
        }

        private void ProcessInvalidMove()
        {
            Console.WriteLine("Illegal command!");
        }

        private void ProcessTopCommand()
        {
            scoreboard.ShowHighScores();
        }

        private void ProcessExitCommand()
        {
            Console.WriteLine("Goodbye!");
            Environment.Exit(0);
        }

        private void ProcessRestartCommand()
        {
            GameBoard.ResetBoard();
            ShowWelcomeMessage();
            gameboard.Display();
        }

        private int[] ParseInputCoordinates(string[] inputCoordinates)
        {
            int[] coordinates = new int[2];
            if (inputCoordinates.Length != 2)
            {
                throw new ArgumentException("Invalid coordinates.");
            }
            else
            {
                try
                {
                    coordinates[0] = Convert.ToInt32(inputCoordinates[0]);
                    coordinates[1] = Convert.ToInt32(inputCoordinates[1]);
                }
                catch (FormatException)
                {
                    throw new ArgumentException("Invalid format.");
                }
            }

            return coordinates;
        }

        private bool CheckIfValidCoordinates(int row, int col)
        {
            bool isInsideBoard = !gameboard.IsInsideBoard(row, col);
            bool isCellRevealed = gameboard.IsCellRevealed(row, col);
            return isInsideBoard || isCellRevealed;
        }

        private void ProcessCoordinates(string[] inputCoordinates)
        {
            var coords = ParseInputCoordinates(inputCoordinates);
            int row = coords[0];
            int col = coords[1];

            if (CheckIfValidCoordinates(row, col))
            {
                Console.WriteLine("Illegal move!"); // to be put in the Renderer
                Console.WriteLine();
            }
            else
            {
                if (gameboard.CheckIfHasMine(row, col))
                {
                    ShowEndGameMessage(gameboard, scoreboard);
                    scoreboard.ShowHighScores();
                    GameBoard.ResetBoard(); // should call on the ClearBoard() from the RenderingEngine
                    ShowWelcomeMessage();
                    gameboard.Display();
                }
                else
                {
                    gameboard.RevealBlock(row, col);
                    gameboard.Display();
                }
            }

            if (gameboard.CheckIfGameIsWon())
            {
                ShowGameWonMessage(gameboard, scoreboard);
            }
        }

        public void ShowWelcomeMessage()
        {
            Console.WriteLine("Welcome to the game “Minesweeper”. Try to reveal all cells without mines.");
            Console.WriteLine("Use 'top' to view the scoreboard, 'restart' to start a new game and 'exit' to quit the game.");
            Console.WriteLine();
        }

        public void ShowEndGameMessage(GameBoard board, Scoreboard scoreboard)
        {
            board.RevealWholeBoard();
            board.Display();

            Console.WriteLine("Booooom! You were killed by a mine. You revealed " + board.RevealedCellsCount + " cells without mines.");
            Console.WriteLine();

            if (board.RevealedCellsCount > scoreboard.MinInTop5() || scoreboard.Count() < 5)
            {
                scoreboard.AddPlayer(board.RevealedCellsCount);
            }
        }

        public void ShowGameWonMessage(GameBoard board, Scoreboard scoreboard)
        {
            board.RevealWholeBoard();
            board.Display();

            Console.WriteLine("Congratulations! You have escaped all the mines and WON the game!");
            Console.WriteLine();

            scoreboard.AddPlayer(board.RevealedCellsCount);
        }
    }
}