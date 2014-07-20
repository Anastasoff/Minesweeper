﻿namespace Minesweeper.GUI
{
    using System;

    using GameObjects;
    using Interfaces;

    class ConsoleInterface : IOInterface
    {
        public string GetUserInput(string message)
        {
            Console.Write(message);
            return Console.ReadLine().Trim();
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void ShowMessage()
        {
            Console.WriteLine();
        }
        public void ShowWelcomeScreen()
        {
            Console.WriteLine("Welcome to the game “Minesweeper”. Try to reveal all cells without mines.");
            Console.WriteLine("Use 'top' to view the scoreboard, 'restart' to start a new game and 'exit' to quit the game.");
            Console.WriteLine();
        }

        public void ClearScreen()
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }


        // I've extracted the logic from the Display() into several methods
        private void PrintIndentationOnTheLeft()
        {
            Console.Write(new string(' ', 4));
        }

        private void PrintFieldTopAndBottomBorder(int cols)
        {
            Console.WriteLine(new string('-', 2 * cols));
        }

        private void PrintFieldsNumberOfColumns(int cols)
        {
            for (int i = 0; i < cols; i++)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();
        }

        private void PrintGameField(Cell[,] board)
        {
            int rows = board.GetLength(0);
            int cols = board.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                Console.Write(i + " | ");
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(board[i, j].Symbol + " ");
                }

                Console.WriteLine("|");
            }
        }

        public void DrawBoard(Cell[,] board)
        {
            int rows = board.GetLength(0);
            int cols = board.GetLength(1);
            // print first row
            PrintIndentationOnTheLeft();
            PrintFieldsNumberOfColumns(cols);

            // print second row
            PrintIndentationOnTheLeft();
            PrintFieldTopAndBottomBorder(cols);

            PrintGameField(board);

            // print last row
            PrintIndentationOnTheLeft();
            PrintFieldTopAndBottomBorder(cols);
        }
    }
}
