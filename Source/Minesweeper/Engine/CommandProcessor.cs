namespace Minesweeper.Engine
{
    using GUI;
    using Interfaces;
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
        private delegate void CellHandler(int row, int col);

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
            string[] commandsArr = input.Split(' ');
            Command command = this.commandParser.ExtractCommand(commandsArr, this.gameBoard);
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
                case Command.Restart:
                    ProcessRestartCommand();
                    break;
                case Command.Flag:
                    ProcessFlagCommand(commandsArr);
                    break;
                case Command.InvalidInput:
                    userIteractor.ShowMessage("Invalid input! Please try again!");
                    break;
                case Command.System:
                    break;
                default:
                    ProcessCoordinates(commandsArr);
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

        private void ProcessFlagCommand(string[] commandsArr)
        {
            int row = int.Parse(commandsArr[1]);
            int col = int.Parse(commandsArr[2]);

            var cellHandler = new CellHandler(gameBoard.PlaceFlag);
            CheckIfCellIsRevealed(cellHandler, row, col);
        }

        private void ProcessRestartCommand()
        {
            gameBoard.ResetBoard();
            userIteractor.DrawBoard(gameBoard.Board);
        }

        private void ProcessCoordinates(string[] inputCoordinates)
        {
            int row = int.Parse(inputCoordinates[0]);
            int col = int.Parse(inputCoordinates[1]);

            if (gameBoard.CheckIfHasMine(row, col) && !gameBoard.CheckIfFlagCell(row, col))
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
                userIteractor.DrawBoard(gameBoard.Board);
            }
            else if (gameBoard.CheckIfHasMine(row, col) && gameBoard.CheckIfFlagCell(row, col))
            {
                PrintUsedCellMessage("You've already placed flag at these coordinates! Please enter new cell coordinates!");
            }
            else
            {
                var cellHandler = new CellHandler(gameBoard.RevealBlock);
                CheckIfCellIsRevealed(cellHandler, row, col);
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

        private void CheckIfCellIsRevealed(CellHandler cellAction, int row, int col)
        {
            if (gameBoard.IsCellRevealed(row, col))
            {
                PrintUsedCellMessage("This cell has already been revealed! Please enter new cell coordinates!");
            }
            else if (gameBoard.CheckIfFlagCell(row, col))
            {
                PrintUsedCellMessage("You've already placed flag at these coordinates! Please enter new cell coordinates!");
            }
            else
            {
                cellAction(row, col);
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