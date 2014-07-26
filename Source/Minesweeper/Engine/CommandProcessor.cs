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
        private CommandParser CommandParser { get; set; }
        private GameBoard GameBoard { get; set; }
        private Scoreboard ScoreBoard { get; set; }
        private IOInterface UserIteractor { get; set; }
        private int RemainingLives { get; set; }
        private GameBoardMemory CurrentBoardState { get; set; }
        private delegate void CellHandler(int row, int col);

        public CommandProcessor(GameBoard board, Scoreboard score, IOInterface userIteractor, CommandParser commandParser)
        {
            this.GameBoard = board;
            this.ScoreBoard = score;
            this.UserIteractor = userIteractor;
            this.CurrentBoardState = new GameBoardMemory();
            this.CurrentBoardState.Memento = board.SaveMemento();
            this.CommandParser = commandParser;
            this.RemainingLives = INITIAL_LIVES;
        }        

        /// <summary>
        /// Handles the execution of all valid commands.
        /// </summary>
        /// <param name="input">The command string.</param>
        public void ExecuteCommand(string input)
        {
            string[] commandsArr = input.Split(' ');
            Command command = this.CommandParser.ExtractCommand(commandsArr, this.GameBoard);
            switch (command)
            {
                case Command.InvalidMove:
                    UserIteractor.ShowMessage("Invalid rows or cols! Try again");
                    break;

                case Command.Exit:
                    UserIteractor.ShowMessage("Goodbye!");
                    Environment.Exit(0);
                    break;

                case Command.Top:
                    ScoreBoard.ShowHighScores();
                    break;

                case Command.Restart: ProcessRestartCommand();
                    break;

                case Command.Flag: ProcessFlagCommand(commandsArr);
                    break;

                case Command.InvalidInput:
                    UserIteractor.ShowMessage("Invalid input! Please try again!");
                    break;

                case Command.System:
                    break;

                default: ProcessCoordinates(commandsArr);
                    break;
            }
        }

        private void ShowMessage(string message)
        {
            this.GameBoard.RevealWholeBoard();

            UserIteractor.DrawBoard(GameBoard.Board);

            UserIteractor.ShowMessage(message);
            UserIteractor.ShowMessage();
        }

        private void ShowEndGameMessage()
        {
            string message = "Booooom! You were killed by a mine. You revealed " + this.GameBoard.RevealedCellsCount + " cells without mines.";
            ShowMessage(message);

            if (this.GameBoard.RevealedCellsCount > this.ScoreBoard.MinInTop5() || this.ScoreBoard.Count() < 5)
            {
                this.ScoreBoard.AddPlayer(this.GameBoard.RevealedCellsCount);
            }
        }

        private void ShowGameWonMessage()
        {
            string message = "Congratulations! You have escaped all the mines and WON the game!";
            ShowMessage(message);

            this.ScoreBoard.AddPlayer(this.GameBoard.RevealedCellsCount);
        }

        private void ProcessFlagCommand(string[] commandsArr)
        {
            int row = int.Parse(commandsArr[1]);
            int col = int.Parse(commandsArr[2]);


            var cellHandler = new CellHandler(GameBoard.PlaceFlag);
            CheckIfCellIsRevealed(cellHandler, row, col);
        }

        private void ProcessRestartCommand()
        {
            GameBoard.ResetBoard();
            UserIteractor.DrawBoard(GameBoard.Board);
        }

        private void ProcessCoordinates(string[] inputCoordinates)
        {
            int row = int.Parse(inputCoordinates[0]);
            int col = int.Parse(inputCoordinates[1]);

            if (GameBoard.CheckIfHasMine(row, col) && !GameBoard.CheckIfFlagCell(row, col))
            {
                //Memento logic
                if (this.RemainingLives > 0)
                {
                    bool reverting = AskUserToRevert();
                    if (reverting == true)
                    {
                        GameBoard.RestoreMemento(CurrentBoardState.Memento);
                        return;
                    }
                }
                ShowEndGameMessage();
                GameBoard.ResetBoard();
                UserIteractor.DrawBoard(GameBoard.Board);
            }
            else if (GameBoard.CheckIfHasMine(row, col) && GameBoard.CheckIfFlagCell(row, col))
            {
                PrintUsedCellMessage("You've already placed flag at these coordinates! Please enter new cell coordinates!");
            }
            else
            {
                var cellHandler = new CellHandler(GameBoard.RevealBlock);
                CheckIfCellIsRevealed(cellHandler, row, col);
                this.CurrentBoardState.Memento = GameBoard.SaveMemento();
            }

            if (GameBoard.CheckIfGameIsWon())
            {
                ShowGameWonMessage();
            }
        }

        private bool AskUserToRevert()
        {
            string userInput = UserIteractor.GetUserInput("You have one more live. Do you want to revert the board to the previous state?[yes/no]");
            while (userInput != "yes" && userInput != "no")
            {

                userInput = UserIteractor.GetUserInput("Invalid input! Please enter [yes/no]! ");
            }

            if (userInput == "yes")
            {
                this.RemainingLives--;
                return true;
            }

            return false;
        }

        private void CheckIfCellIsRevealed(CellHandler cellAction, int row, int col)
        {
            if (GameBoard.IsCellRevealed(row, col))
            {
                PrintUsedCellMessage("This cell has already been revealed! Please enter new cell coordinates!");
            }
            else if (GameBoard.CheckIfFlagCell(row, col))
            {
                PrintUsedCellMessage("You've already placed flag at these coordinates! Please enter new cell coordinates!");
            }
            else
            {
                cellAction(row, col);
                UserIteractor.DrawBoard(GameBoard.Board);
            }
        }

        

        private void PrintUsedCellMessage(string message)
        {
            UserIteractor.DrawBoard(GameBoard.Board);
            UserIteractor.ShowMessage(message);
            UserIteractor.ShowMessage();
        }
    }
}