<<<<<<< .mine
﻿namespace Minesweeper.Engine
{
    using GUI;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Interfaces;

    public class CommandProcessor
    {
        Dictionary<string, Command> commands = new Dictionary<string, Command>();
        private GameBoard gameBoard;
        private Scoreboard scoreBoard;
        private IOInterface userIteractor;

        public CommandProcessor(GameBoard board, Scoreboard score, IOInterface userIteractor)
        {
            this.GameBoard = board;
            this.Score = score;
            this.userIteractor = userIteractor;

            commands.Add("exit", Command.Exit);
            commands.Add("top", Command.Top);
            commands.Add("restart", Command.Restart);
            commands.Add("flag", Command.Flag);

        }

        public GameBoard GameBoard
        {
            get
            {
                return this.gameBoard;
            }

            private set
            {
                this.gameBoard = value;
            }
        }

        public Scoreboard Score
        {
            get
            {
                return this.scoreBoard;
            }

            private set
            {
                this.scoreBoard = value;
            }
        }

        private bool CheckIfValidInputCommand(string inputCommand)
        {
            return commands.ContainsKey(inputCommand.ToLower());
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
                case Command.InvalidMove:
                    userIteractor.showMessage("Illegal command!");
                    break;
                case Command.Exit:
                    userIteractor.showMessage("Goodbye!");
                    Environment.Exit(0);
                    break;
                case Command.Top: ProcessTopCommand();
                    break;
                case Command.Restart: ProcessRestartCommand();
                    break;
                case Command.Flag: ProcessFlagCommand(commandsArr);
                    break;
                case Command.InvalidInput:
                    userIteractor.showMessage("Invalid input! Please try again!");
                    break;
                default: ProcessCoordinates(commandsArr);
                    break;
            }
        }

        private Command GetCommandFromInput(string input)
        {
            if (!commands.ContainsKey(input))
            {
                throw new ArgumentException("Input not a valid command");
            }
            else
            {
                return commands[input];
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
                userIteractor.showMessage("Illegal move!");
                userIteractor.showMessage();
            }
            else
            {
                gameBoard.PlaceFlag(row, col);
                gameBoard.Display();
            }
        }

        //private void ProcessInvalidInputCommand()
        //{
        //    Console.WriteLine("Invalid input! Please try again!");
        //}

        //private void ProcessInvalidMove()
        //{
        //    Console.WriteLine("Illegal command!");
        //}

        private void ProcessTopCommand()
        {
            scoreBoard.ShowHighScores();
        }

        //private void ProcessExitCommand()
        //{
        //    Console.WriteLine("Goodbye!");
        //    Environment.Exit(0);
        //}

        private void ProcessRestartCommand()
        {
            gameBoard.ResetBoard();
            userIteractor.showWelcomeScreen();
            gameBoard.Display();
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
            // bullshit
            //bool isInsideBoard = !gameboard.IsInsideBoard(row, col);
            //bool isCellRevealed = gameboard.IsCellRevealed(row, col);
            //return isInsideBoard || isCellRevealed;

            // mother of all bullshits
            //if (!gameboard.IsInsideBoard(row, col)) 
            //{
            //    return false;
            //}
            //if (gameboard.IsCellRevealed(row, col))
            //{
            //    return false;                
            //}
            //return true;

            return !gameBoard.IsInsideBoard(row, col) || gameBoard.IsCellRevealed(row, col);
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
                if (gameBoard.CheckIfHasMine(row, col))
                {
                    ShowEndGameMessage(gameBoard, scoreBoard);
                    scoreBoard.ShowHighScores();
                    gameBoard.ResetBoard(); // should call on the ClearBoard() from the RenderingEngine
                    userIteractor.showWelcomeScreen();
                    gameBoard.Display();
                }
                else
                {
                    gameBoard.RevealBlock(row, col);
                    gameBoard.Display();
                }
            }

            if (gameBoard.CheckIfGameIsWon())
            {
                ShowGameWonMessage(gameBoard, scoreBoard);
            }
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
=======
﻿namespace Minesweeper.Engine
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
            gameboard.ResetBoard();
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
            // bullshit
            //bool isInsideBoard = !gameboard.IsInsideBoard(row, col);
            //bool isCellRevealed = gameboard.IsCellRevealed(row, col);
            //return isInsideBoard || isCellRevealed;

            // mother of all bullshits
            //if (!gameboard.IsInsideBoard(row, col)) 
            //{
            //    return false;
            //}
            //if (gameboard.IsCellRevealed(row, col))
            //{
            //    return false;                
            //}
            //return true;

            return !gameboard.IsInsideBoard(row, col) || gameboard.IsCellRevealed(row, col);
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
                    gameboard.ResetBoard(); // should call on the ClearBoard() from the RenderingEngine
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
>>>>>>> .r69
}