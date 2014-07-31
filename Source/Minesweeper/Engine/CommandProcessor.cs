namespace Minesweeper.Engine
{
    using System;
    using GameObjects;
    using GUI;
    using Interfaces;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Contains the main logic behind commands execution 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CommandProcessor
    {
        private const int INITIAL_LIVES = 1;
        private CommandParser commandParser;
        private GameBoard gameBoard;
        private Scoreboard scoreBoard;
        private IOInterface userIteractor;
        private int remainingLives;
        private GameBoardMemory currentBoardState;

        public CommandProcessor(GameBoard board, Scoreboard score, IOInterface userIteractor, CommandParser commandParser)
        {
            this.gameBoard = board;
            this.scoreBoard = score;
            this.userIteractor = userIteractor;
            this.currentBoardState = new GameBoardMemory();
            this.currentBoardState.Memento = board.SaveMemento();
            this.commandParser = commandParser;
            this.remainingLives = INITIAL_LIVES;
        }

        private delegate void CellHandler(Position pos);

        /// <summary>
        /// Handles the execution of all valid commands.
        /// </summary>
        /// <param name="input">The command string.</param>
        public void ExecuteCommand(string input)
        {
            Command cmd = this.commandParser.ExtractCommand(input, this.gameBoard);
            switch (cmd.CommandType)
            {
                case CommandType.InvalidMove:
                    this.userIteractor.ShowMessage("Invalid rows or cols! Try again");
                    break;
                case CommandType.Exit:
                    this.userIteractor.ShowMessage("Goodbye!");
                    Environment.Exit(0);
                    break;
                case CommandType.Top:
                    this.scoreBoard.ShowHighScores();
                    break;
                case CommandType.Restart:
                    this.ProcessRestartCommand();
                    break;
                case CommandType.Flag:
                    this.ProcessFlagCommand(cmd.Coordinates);
                    break;
                case CommandType.InvalidInput:
                    this.userIteractor.ShowMessage("Invalid input! Please try again!");
                    break;
                case CommandType.System:
                    break;
                default:
                    this.ProcessCoordinates(cmd.Coordinates);
                    break;
            }
        }

        private void ShowMessage(string message)
        {
            this.gameBoard.RevealWholeBoard();

            this.userIteractor.DrawBoard(this.gameBoard.Board);

            this.userIteractor.ShowMessage(message);
            this.userIteractor.ShowMessage(string.Empty);
        }

        private void ShowEndGameMessage()
        {
            string message = "Booooom! You were killed by a mine. You revealed " + this.gameBoard.RevealedCellsCount + " cells without mines.";
            this.ShowMessage(message);

            if (this.gameBoard.RevealedCellsCount > this.scoreBoard.MinInTop5() || this.scoreBoard.Count() < 5)
            {
                this.scoreBoard.AddPlayer(this.gameBoard.RevealedCellsCount);
            }
        }

        private void ShowGameWonMessage()
        {
            string message = "Congratulations! You have escaped all the mines and WON the game!";
            this.ShowMessage(message);

            this.scoreBoard.AddPlayer(this.gameBoard.RevealedCellsCount);
        }

        private void ProcessFlagCommand(Position coordinates)
        {
            var cellHandler = new CellHandler(this.gameBoard.PlaceFlag);
            this.CheckIfCellIsRevealed(cellHandler, coordinates);
        }

        private void ProcessRestartCommand()
        {
            this.gameBoard.ResetBoard();
            this.ResetLives();
            this.userIteractor.DrawBoard(this.gameBoard.Board);
        }

        private void ResetLives()
        {
            this.remainingLives = 1;
        }

        private void ProcessCoordinates(Position coordinates)
        {
            if (this.gameBoard.CheckIfHasMine(coordinates) && !this.gameBoard.CheckIfFlagCell(coordinates))
            {
                // Memento logic
                if (this.remainingLives > 0)
                {
                    bool reverting = this.AskUserToRevert();
                    if (reverting == true)
                    {
                        this.gameBoard.RestoreMemento(this.currentBoardState.Memento);
                        return;
                    }
                }

                this.ShowEndGameMessage();
                this.gameBoard.ResetBoard();
                this.ResetLives();
                this.userIteractor.DrawBoard(this.gameBoard.Board);
            }
            else if (this.gameBoard.CheckIfHasMine(coordinates) && this.gameBoard.CheckIfFlagCell(coordinates))
            {
                this.PrintUsedCellMessage("You've already placed flag at these coordinates! Please enter new cell coordinates!");
            }
            else
            {
                var cellHandler = new CellHandler(this.gameBoard.RevealBlock);
                this.CheckIfCellIsRevealed(cellHandler, coordinates);
                this.currentBoardState.Memento = this.gameBoard.SaveMemento();
            }

            if (this.gameBoard.CheckIfGameIsWon())
            {
                this.ShowGameWonMessage();
            }
        }

        private bool AskUserToRevert()
        {
            string userInput = this.userIteractor.GetUserInput("You have one more live. Do you want to revert the board to the previous state?[yes/no]");
            while (userInput != "yes" && userInput != "no")
            {
                userInput = this.userIteractor.GetUserInput("Invalid input! Please enter [yes/no]! ");
            }

            if (userInput == "yes")
            {
                this.remainingLives--;
                return true;
            }

            return false;
        }

        private void CheckIfCellIsRevealed(CellHandler cellAction, Position pos)
        {
            if (this.gameBoard.IsCellRevealed(pos))
            {
                this.PrintUsedCellMessage("This cell has already been revealed! Please enter new cell coordinates!");
            }
            else if (this.gameBoard.CheckIfFlagCell(pos))
            {
                this.PrintUsedCellMessage("You've already placed flag at these coordinates! Please enter new cell coordinates!");
            }
            else
            {
                cellAction(pos);
                this.userIteractor.DrawBoard(this.gameBoard.Board);
            }
        }

        private void PrintUsedCellMessage(string message)
        {
            this.userIteractor.DrawBoard(this.gameBoard.Board);
            this.userIteractor.ShowMessage(message);
            this.userIteractor.ShowMessage(string.Empty);
        }
    }
}