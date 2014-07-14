namespace Minesweeper.Engine
{
    using System;

    public class GameEngine
    {
        private GameBoard gameboard;
        private Scoreboard scoreboard;

        public GameEngine(GameBoard gameboard, Scoreboard scoreboard)// changed both classes' access to internal
        {
            this.gameboard = gameboard;
            this.scoreboard = scoreboard;
        }

        public void Play()
        {
            ShowWelcomeMessage();
            gameboard.Display();

            while (true)
            {
                ReadCommand();
                Command command = CommandProcessor.command; //removed the numerous if-statements and replaced them with switch-statement
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
                    default: ProcessCoordinates();
                        break;
                }
            }
        }

        public void ShowWelcomeMessage() //transfered from the main class
        {
            Console.WriteLine("Welcome to the game “Minesweeper”. Try to reveal all cells without mines. Use 'top' to view the scoreboard, 'restart' to start a new game and 'exit' to quit the game.");
            Console.WriteLine();
        }

        public void ShowEndGameMessage(GameBoard board, Scoreboard scoreboard) //transfered from the main class
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

        private void ReadCommand() // new function to reduce the mess in the while(true) loop
        {
            Console.Write("Enter row and column: ");
            CommandProcessor.ReadCommand();
        }

        private void ProcessInvalidMove() // new function to reduce the mess in the while(true) loop
        {
            Console.WriteLine("Illegal command!");
        }

        private void ProcessTopCommand() // new function to reduce the mess in the while(true) loop
        {
            scoreboard.ShowHighScores();
            CommandProcessor.Clear();
        }

        private void ProcessExitCommand() // new function to reduce the mess in the while(true) loop
        {
            Console.WriteLine("Goodbye!");
            Environment.Exit(0);
        }

        private void ProcessRestartCommand() // new function to reduce the mess in the while(true) loop
        {
            GameBoard.ResetBoard();
            ShowWelcomeMessage();
            gameboard.Display();
        }

        private void ProcessCoordinates() // new function to reduce the mess in the while(true) loop
        {
            int x = CommandProcessor.x;
            int y = CommandProcessor.y;

            if (!gameboard.InBoard(x, y) || gameboard.CellIsRevealed(x, y))
            {
                Console.WriteLine("Illegal move!");
                Console.WriteLine();
            }
            else
            {
                if (gameboard.HasMine(x, y))
                {
                    ShowEndGameMessage(gameboard, scoreboard);
                    scoreboard.ShowHighScores();
                    GameBoard.ResetBoard();
                    ShowWelcomeMessage();
                    gameboard.Display();
                }
                else
                {
                    gameboard.RevealBlock(x, y);
                    gameboard.Display();
                }
            }
        }
    }
}
