namespace Minesweeper.Engine
{
    using System;
    using GUI;

    internal class CommandProcessor
    {

        public GameBoard gameboard; // TODO: encapsulate
        private Scoreboard scoreboard;

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
                default: ProcessCoordinates(input);
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

        private int[] SetCoordinates(string commandRead)
        {
            int[] coordinates = new int[2];
            string[] point = commandRead.Split(' ');
            if (point.Length != 2)
            {
                throw new ArgumentException("Invalid coordinates.");
            }
            else
            {
                try
                {
                    coordinates[0] = Convert.ToInt32(point[0]);
                    coordinates[1] = Convert.ToInt32(point[1]);
                }
                catch (FormatException)
                {
                    throw new ArgumentException("Invalid format.");
                }
            }

            return coordinates;
        }

        private void ProcessCoordinates(string input)
        {
            var coords = SetCoordinates(input);
            int x = coords[0];
            int y = coords[1];

            if (!gameboard.InBoard(x, y) || gameboard.CellIsRevealed(x, y))
            {
                Console.WriteLine("Illegal move!");
                Console.WriteLine();
            }
            else
            {
                if (gameboard.CheckIfHasMine(x, y))
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
    }
}