using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GameEngine
    {
        private GameBoard board;
        private Scoreboard scoreboard;

        public GameEngine(GameBoard board, Scoreboard score)// changed both classes' access to internal
        {
            this.board = board;
            this.scoreboard = score;
        }

        public void Play()
        {
            ShowWelcomeMessage();
            board.Display();

            while (true)
            {
                ReadCommand();
                Commands command = CommandProcessor.command; //removed the numerous if-statements and replaced them with switch-statement
                switch (command)
                {
                    case Commands.InvalidMove: ProcessInvalidMove();
                        break;
                    case Commands.Exit: ProcessExitCommand();
                        break;
                    case Commands.Top: ProcessTopCommand();
                        break;
                    case Commands.Restart: ProcessRestartCommand();
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
            board.Display();
        }

        private void ProcessCoordinates() // new function to reduce the mess in the while(true) loop
        {
            int x = CommandProcessor.x;
            int y = CommandProcessor.y;

            if (!board.InBoard(x, y) || board.CellIsRevealed(x, y))
            {
                Console.WriteLine("Illegal move!");
                Console.WriteLine();
            }
            else
            {
                if (board.HasMine(x, y))
                {
                    ShowEndGameMessage(board, scoreboard);
                    scoreboard.ShowHighScores();
                    GameBoard.ResetBoard();
                    ShowWelcomeMessage();
                    board.Display();
                }
                else
                {
                    board.RevealBlock(x, y);
                    board.Display();
                }
            }
        }
    }
}
