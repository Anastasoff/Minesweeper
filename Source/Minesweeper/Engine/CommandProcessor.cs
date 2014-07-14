namespace Minesweeper.Engine
{
    using System;

    internal class CommandProcessor
    {

        public GameBoard gameboard; // TODO: encapsulate
        private Scoreboard scoreboard;
        internal static int x { get; set; }

        internal static int y { get; set; }

        internal static Command command;

        public CommandProcessor(GameBoard gameboard, Scoreboard scoreboard)
        {
            this.gameboard = gameboard;
            this.scoreboard = scoreboard;
        }

        public void ExecuteCommand(string input)
        {
            // exit top restart
            Command command;
            try
            {
                command = ParseInput(input);
            }
            catch (ArgumentException)
            {
                command = Command.ValidMove;
            }
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

        private Command ParseInput(string input)
        {
            switch (input)
            {
                case "exit": return Command.Exit;
                case "top": return Command.Top;
                case "restart": return Command.Restart;
                default: throw new ArgumentException("Input not a valid command");
            }
        }

        private void ProcessInvalidMove()
        {
            Console.WriteLine("Illegal command!");
        }

        private void ProcessTopCommand()
        {
            scoreboard.ShowHighScores();
            CommandProcessor.Clear();
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

        private static void SetCoordinates(string commandRead) // to private // ************NEW
        {
            string[] point = commandRead.Split(' ');
            if (point.Length != 2)
            {
                command = Command.InvalidMove;
            }
            else
            {
                try
                {
                    x = Convert.ToInt32(point[0]);
                    y = Convert.ToInt32(point[1]);
                    command = Command.ValidMove;
                }
                catch (FormatException)
                {
                    command = Command.InvalidMove;
                }
            }
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

        internal static void Clear()
        {
            x = 0;
            y = 0;
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
    }
}