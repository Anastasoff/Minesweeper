﻿using System;

namespace MineSweeper
{
    internal class GameBoard
    {
        private const int SizeX = 5;
        private const int SizeY = 10;
        private const int numberOfMines = 15;

        private char[,] display;
        private bool[,] mineMap;
        private bool[,] revealed;
        private int[,] numberOfNeighbourMines;

        private static GameBoard board; // get one and only instance of board

        internal int RevealedCellsCount { get; set; }

        private GameBoard() // private constructor
        {
            display = new char[SizeX, SizeY];
            mineMap = new bool[SizeX, SizeY];
            revealed = new bool[SizeX, SizeY];
            numberOfNeighbourMines = new int[SizeX, SizeY];
            InitializeBoardForDisplay();
            PutMines();
        }

        public static GameBoard GetBoard // property to access singleton instance of board.
        {
            get
            {
                if (board == null)
                {
                    board = new GameBoard();
                }
                return board;
            }
        }

        private void PutMines()
        {
            Random generator = new Random();

            int actualNumberOfMines = 0;
            while (actualNumberOfMines < numberOfMines)
            {
                if (PlaceMine(generator.Next(SizeX), generator.Next(SizeY)))
                    actualNumberOfMines++;
            }
        }

        internal bool InBoard(int x, int y)
        {
            return 0 <= x && x < SizeX && 0 <= y && y < SizeY;
        }

        private bool PlaceMine(int x, int y)
        {
            if (!InBoard(x, y) || mineMap[x, y])
            {
                return false;
            }

            mineMap[x, y] = true;
            for (int xx = -1; xx <= 1; xx++)
                for (int yy = -1; yy <= 1; yy++)
                {
                    if (((xx != 0) || (yy != 0)) && InBoard(x + xx, y + yy))
                        numberOfNeighbourMines[x + xx, y + yy]++;
                }
            return true;
        }

        private void InitializeBoardForDisplay()
        {
            for (int i = 0; i < SizeX; i++)
                for (int j = 0; j < SizeY; j++)
                    display[i, j] = '?';
        }

        internal void Display()
        {
            for (int i = 0; i < 4; i++)
                Console.Write(" ");
            for (int i = 0; i < SizeY; i++)
                Console.Write(i + " ");
            Console.WriteLine();
            for (int i = 0; i < 4; i++)
                Console.Write(" ");
            for (int i = 0; i < 2 * SizeY; i++)
                Console.Write('-');
            Console.WriteLine();
            for (int i = 0; i < SizeX; i++)
            {
                Console.Write(i + " | ");
                for (int j = 0; j < SizeY; j++)
                    Console.Write(display[i, j] + " ");
                Console.WriteLine("|");
            }
            for (int i = 0; i < 4; i++)
                Console.Write(" ");
            for (int i = 0; i < 2 * SizeY; i++)
                Console.Write("-");
            Console.WriteLine();
        }

        internal bool HasMine(int x, int y)
        {
            return mineMap[x, y];
        }

        internal void RevealBlock(int x, int y)
        {
            display[x, y] = Convert.ToChar(numberOfNeighbourMines[x, y].ToString());
            RevealedCellsCount++;
            revealed[x, y] = true;
            if (display[x, y] == '0')
            {
                for (int xx = -1; xx <= 1; xx++)
                    for (int yy = -1; yy <= 1; yy++)
                    {
                        int newX = x + xx;
                        int newY = y + yy;
                        if (InBoard(newX, newY) && revealed[newX, newY] == false)
                            RevealBlock(newX, newY);
                    }
            }
        }

        internal void RevealAllBoard(int x, int y)
        {
            for (int i = 0; i < SizeX; i++)
                for (int j = 0; j < SizeY; j++)
                {
                    if (!revealed[i, j])
                        display[i, j] = '-';
                    if (mineMap[i, j])
                        display[i, j] = '*';
                }
        }

        internal bool CellIsRevealed(int x, int y)
        {
            return revealed[x, y];
        }
    }
}