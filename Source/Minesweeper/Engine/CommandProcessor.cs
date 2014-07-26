namespace Minesweeper.Engine
{
    using GUI;
    using Interfaces;
    using GameObjects;
    using System;

    /// <summary>
    /// Contains the main logic behind commands execution 
    /// </summary>
    public class CommandProcessor
    {
        private const int INITIAL_LIVES = 1;
        private CommandParser commandParser;
        private GameBoard gameBoard;
        private Scoreboard scoreBoard;
        private IOInterface userIteractor;
        private int remainingLives;
        private GameBoardMemory currentBoardState;
        private delegate void CellHandler(Position pos);

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
                    userIteractor.ShowMessage("Invalid rows or cols! Try again");
                    break;
                case CommandType.Exit:
                    userIteractor.ShowMessage("Goodbye!");
                    Environment.Exit(0);
                    break;
                case CommandType.Top:
                    scoreBoard.ShowHighScores();
                    break;
                case CommandType.Restart:
                    ProcessRestartCommand();
                    break;
                case CommandType.Flag:
                    ProcessFlagCommand(cmd.Coordinates);
                    break;
                case CommandType.InvalidInput:
                    userIteractor.ShowMessage("Invalid input! Please try again!");
                    break;
                case CommandType.System:
                    break;
                default:
                    ProcessCoordinates(cmd.Coordinates);
                    break;
            }
        }

        private void ShowMessage(string message)
        {
            this.gameBoard.RevealWholeBoard();

            userIteractor.DrawBoard(gameBoard.Board);

            userIteractor.ShowMessage(message);
            userIteractor.ShowMessage(string.Empty);
        }

        private void ShowEndGameMessage()
        {
            string message = "Booooom! You were killed by a mine. You revealed " + this.gameBoard.RevealedCellsCount + " cells without mines.";
            ShowMessage(message);

            if (this.gameBoard.RevealedCellsCount > this.scoreBoard.MinInTop5() || this.scoreBoard.Count() < 5)
            {
                this.scoreBoard.AddPlayer(this.gameBoard.RevealedCellsCount);
            }
        }

        private void ShowGameWonMessage()
        {
            string message = "Congratulations! You have escaped all the mines and WON the game!";
            ShowMessage(message);

            this.scoreBoard.AddPlayer(this.gameBoard.RevealedCellsCount);
        }

        private void ProcessFlagCommand(Position coordinates)
        {
            var cellHandler = new CellHandler(gameBoard.PlaceFlag);
            CheckIfCellIsRevealed(cellHandler, coordinates);
        }

        private void ProcessRestartCommand()
        {
            gameBoard.ResetBoard();
            this.ResetLives();
            userIteractor.DrawBoard(gameBoard.Board);
        }

        private void ResetLives()
        {
            this.remainingLives = 1;
        }

        private void ProcessCoordinates(Position coordinates)
        {
            if (gameBoard.CheckIfHasMine(coordinates) && !gameBoard.CheckIfFlagCell(coordinates))
            {
                //Memento logic
                if (this.remainingLives > 0)
                {
                    bool reverting = AskUserToRevert();
                    if (reverting == true)
                    {
                        gameBoard.RestoreMemento(currentBoardState.Memento);
                        return;
                    }
                }
                ShowEndGameMessage();
                gameBoard.ResetBoard();
                this.ResetLives();
                userIteractor.DrawBoard(gameBoard.Board);
            }
            else if (gameBoard.CheckIfHasMine(coordinates) && gameBoard.CheckIfFlagCell(coordinates))
            {
                PrintUsedCellMessage("You've already placed flag at these coordinates! Please enter new cell coordinates!");
            }
            else
            {
                var cellHandler = new CellHandler(gameBoard.RevealBlock);
                CheckIfCellIsRevealed(cellHandler, coordinates);
                this.currentBoardState.Memento = gameBoard.SaveMemento();
            }

            if (gameBoard.CheckIfGameIsWon())
            {
                ShowGameWonMessage();
            }
        }

        private bool AskUserToRevert()
        {
            string userInput = userIteractor.GetUserInput("You have one more live. Do you want to revert the board to the previous state?[yes/no]");
            while (userInput != "yes" && userInput != "no")
            {
                userInput = userIteractor.GetUserInput("Invalid input! Please enter [yes/no]! ");
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
            if (gameBoard.IsCellRevealed(pos))
            {
                PrintUsedCellMessage("This cell has already been revealed! Please enter new cell coordinates!");
            }
            else if (gameBoard.CheckIfFlagCell(pos))
            {
                PrintUsedCellMessage("You've already placed flag at these coordinates! Please enter new cell coordinates!");
            }
            else
            {
                cellAction(pos);
                userIteractor.DrawBoard(gameBoard.Board);
            }
        }

        private void PrintUsedCellMessage(string message)
        {
            userIteractor.DrawBoard(gameBoard.Board);
            userIteractor.ShowMessage(message);
            userIteractor.ShowMessage(string.Empty);
        }
    }
}