using System;

namespace MineSweeper
{
    // Аз съм българче but everrything in code (icluding comments) must be in english.

    internal class Програма
    {
        private static void ShowWelcomeMessage()
        {
            Console.WriteLine("Welcome to the game “Minesweeper”. Try to reveal all cells without mines. Use 'top' to view the scoreboard, 'restart' to start a new game and 'exit' to quit the game.");
            Console.WriteLine();
        }

        private static void Main()
        {
            Scoreboard scoreboard = new Scoreboard();
            GameBoard board = GameBoard.GetBoard; // calling singleton
            ShowWelcomeMessage();
            board.Display();
            while (true)
            {
                Console.Write("Enter row and column: ");
                CommandProcessor.ReadCommand();
                if (CommandProcessor.InvalidMove)
                {
                    Console.WriteLine("Illegal command!");
                    continue;
                }

                if (CommandProcessor.GetStatistic)
                {
                    scoreboard.Покажи();
                    CommandProcessor.Clear();
                    continue;
                }
                if (CommandProcessor.Exit)
                {
                    Console.WriteLine("Goodbye!");
                    Environment.Exit(0);
                }
                if (CommandProcessor.Restart)
                    GameBoard.ResetBoard();
                    ShowWelcomeMessage();
                    board.Display();
                    continue;
                }

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
                        board.RevealAllBoard(x, y);
                        board.Display();
                        Console.WriteLine("Booooom! You were killed by a mine. You revealed " + board.RevealedCellsCount + " cells without mines.");
                        Console.WriteLine();
                        if (board.RevealedCellsCount > scoreboard.MinInTop5() || scoreboard.Count() < 5)
                        {
                            scoreboard.Dobavi(board.RevealedCellsCount);
                        }
                        scoreboard.Покажи();
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
        //ai na bas che ne mojehs da napishesh pove4e kod v edin metod!
    }
}