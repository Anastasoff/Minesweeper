namespace Minesweeper.Engine
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

            this.commands.Add("exit", Command.Exit);
            this.commands.Add("top", Command.Top);
            this.commands.Add("restart", Command.Restart);
            this.commands.Add("flag", Command.Flag);

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

        private Command ExtractCommand(string[] inputCommands)
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
            switch (commandElements.Length)
            {
                case 1:
                    return (commands.ContainsKey(commandElements[0].ToLower())) && (commandElements[0].ToLower() != "flag");
                    break;
                case 2:
                    int row = -1;
                    int col = -1;
                    bool isValidCoords = int.TryParse(commandElements[0], out row);
                    isValidCoords = isValidCoords && int.TryParse(commandElements[1], out col);
                    return isValidCoords;
                    break;
                case 3:
                    string[] coords = new string[2] { commandElements[1], commandElements[2] };
                    return (commandElements[0].ToLower() == "flag") && IsCommandValid(coords);
                    break;
                default:
                    return false;
                    break;
            }
        }

        public void ExecuteCommand(string input)
        {
            string[] commandsArr = input.Split(' ');
            Command command = ExtractCommand(commandsArr);
            switch (command)
            {
                case Command.InvalidMove:
                    userIteractor.ShowMessage("Invalid rows or cols! Try again");
                    break;
                case Command.Exit:
                    userIteractor.ShowMessage("Goodbye!");
                    Environment.Exit(0);
                    break;
                case Command.Top:
                    scoreBoard.ShowHighScores();
                    break;
                case Command.Restart: ProcessRestartCommand();
                    break;
                case Command.Flag: ProcessFlagCommand(commandsArr);
                    break;
                case Command.InvalidInput:
                    userIteractor.ShowMessage("Invalid input! Please try again!");
                    break;
                default: ProcessCoordinates(commandsArr);
                    break;
            }
        }

        private void ProcessFlagCommand(string[] commandsArr)
        {
            int row = int.Parse(commandsArr[1]);
            int col = int.Parse(commandsArr[2]);

            gameBoard.PlaceFlag(row, col);
            userIteractor.DrawBoard(gameBoard.Board);
        }

        private void ProcessRestartCommand()
        {
            gameBoard.ResetBoard();
            userIteractor.ShowWelcomeScreen();
            userIteractor.DrawBoard(gameBoard.Board);
        }


        private void ProcessCoordinates(string[] inputCoordinates)
        {

            int row = int.Parse(inputCoordinates[0]);
            int col = int.Parse(inputCoordinates[1]);

            if (gameBoard.CheckIfHasMine(row, col))
            {
                ShowEndGameMessage(gameBoard, scoreBoard);
                scoreBoard.ShowHighScores();
                gameBoard.ResetBoard(); // should call on the ClearBoard() from the RenderingEngine
                userIteractor.ShowWelcomeScreen();
            }
            else
            {
                gameBoard.RevealBlock(row, col);
            }
            userIteractor.DrawBoard(gameBoard.Board);

            if (gameBoard.CheckIfGameIsWon())
            {
                ShowGameWonMessage(gameBoard, scoreBoard);
            }
        }

        public void ShowEndGameMessage(GameBoard board, Scoreboard scoreboard) // this parameters may not be needed
        {
            board.RevealWholeBoard();
            userIteractor.DrawBoard(gameBoard.Board);
            userIteractor.ShowMessage("Booooom! You were killed by a mine. You revealed " + board.RevealedCellsCount + " cells without mines.");
            userIteractor.ShowMessage();

            if (board.RevealedCellsCount > scoreboard.MinInTop5() || scoreboard.Count() < 5)
            {
                scoreboard.AddPlayer(board.RevealedCellsCount);
            }
        }

        public void ShowGameWonMessage(GameBoard board, Scoreboard scoreboard) // this parameters may not be needed
        {
            board.RevealWholeBoard();
            userIteractor.DrawBoard(gameBoard.Board);

            userIteractor.ShowMessage("Congratulations! You have escaped all the mines and WON the game!");
            userIteractor.ShowMessage();

            scoreboard.AddPlayer(board.RevealedCellsCount);
        }
    }
}