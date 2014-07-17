﻿namespace Minesweeper.GUI
{
    using System;
    using System.Collections.Generic;

    using Common;
    using GameObjects;
    using Interfaces;

    public class GameBoard:IGameBoard
    {
        private int revealedCellsCount;

        private const int ROWS = 5;
        private const int COLS = 10;
        private const int NUMBER_OF_MINES = 15;
        private const char UNREVEALED_CELL_CHAR = '?';
        private const char EMPTY_CELL = '-';
        private const char MINE = '*';

        private char[,] display; // TODO: FIX CLEARING BOARD ON RESET

        //    private bool[,] mineMap;
        private bool[,] revealed;

        private int[,] numberOfNeighbourMines;

        private static GameBoard board = null; // one and only instance of board

        private IList<IGameObject> mineMap;

        private GameBoard() // private constructor
        {
            display = new char[ROWS, COLS];
            //  mineMap = new bool[Rows, Cols];
            this.mineMap = new List<IGameObject>();
            revealed = new bool[ROWS, COLS];
            numberOfNeighbourMines = new int[ROWS, COLS];
            InitializeBoardForDisplay();
            AllocateMines(RandomGenerator.GetInstance);
        }

        public static void ResetBoard() // I don't think that it's a best implementation. If any have better idea ...
        {
            board = new GameBoard();
        }

        public static GameBoard GetInstance // property to access singleton instance of board. Instance
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

        public int RevealedCellsCount
        {
            get
            {
                return this.revealedCellsCount;
            }

            private set
            {
                this.revealedCellsCount = value;
            }
        }

        private void AllocateMines(Random generator)// renamed method - it was easy to mistake it for PlaceMine
        {
            int actualNumberOfMines = 0;
            while (actualNumberOfMines < NUMBER_OF_MINES)
            {
                int currentRow = generator.Next(ROWS);// a few new variables for easier reading
                int currentCol = generator.Next(COLS);
                bool validPlaceForMine = CheckIfMineCanBePlaced(currentRow, currentCol);
                if (validPlaceForMine)
                {
                    PlaceMine(currentRow, currentCol);
                    actualNumberOfMines++;
                }
            }
        }

        public bool CheckIfMineCanBePlaced(int row, int col)// a separate method checking for valid placement of the mine
        {
            if (!InBoard(row, col) || CheckIfHasMine(row, col))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool InBoard(int row, int col)
        {
            bool isInHorizontalLimits = 0 <= row && row < ROWS;
            bool isInVerticalLimits = 0 <= col && col < COLS;
            bool isInField = isInHorizontalLimits && isInVerticalLimits;

            return isInField;
        }

        private void PlaceMine(int row, int col)
        {
            //  mineMap[row, col] = true;
            this.mineMap.Add(new Mine(row, col)); // TODO: SOLID
            for (int neighbouringRow = row - 1; neighbouringRow <= row + 1; neighbouringRow++)
            {
                for (int neighbouringCol = col - 1; neighbouringCol <= col + 1; neighbouringCol++)
                {
                    if (InBoard(neighbouringRow, neighbouringCol) && !(CheckIfHasMine(neighbouringRow, neighbouringCol)))
                    {
                        numberOfNeighbourMines[neighbouringRow, neighbouringCol]++;
                    }
                }
            }
        }

        private void InitializeBoardForDisplay()
        {
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    display[i, j] = UNREVEALED_CELL_CHAR;
                }
            }
        }

        // I've extracted the logic from the Display() into several methods
        private void PrintIndentationOnTheLeft()
        {
            Console.Write(new string(' ', 4));
        }

        private void PrintFieldTopAndBottomBorder()
        {
            Console.WriteLine(new string('-', 2 * COLS));
        }

        private void PrintFieldsNumberOfColumns()
        {
            for (int i = 0; i < COLS; i++)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();
        }

        private void PrintGameField()
        {
            for (int i = 0; i < ROWS; i++)
            {
                Console.Write(i + " | ");
                for (int j = 0; j < COLS; j++)
                {
                    Console.Write(display[i, j] + " ");
                }

                Console.WriteLine("|");
            }
        }

        // now the Display() calls on the private methods to display the gameBoard
        public void Display()
        {
            // print first row
            PrintIndentationOnTheLeft();
            PrintFieldsNumberOfColumns();

            // print second row
            PrintIndentationOnTheLeft();
            PrintFieldTopAndBottomBorder();

            PrintGameField();

            // print last row
            PrintIndentationOnTheLeft();
            PrintFieldTopAndBottomBorder();
        }

        public bool CheckIfHasMine(int row, int col)
        {
            for (int i = 0; i < this.mineMap.Count; i++)
            {
                Mine currentMine = mineMap[i] as Mine;
                int minePositionX = currentMine.Coordinates.row;
                int minePositionY = currentMine.Coordinates.col;

                if (minePositionX == row && minePositionY == col)
                {
                    return true;
                }
            }

            return false;

            //return mineMap[row, col];
        }

        public void RevealBlock(int row, int col)
        {
            string numOfNeighbouringMinesToStr = numberOfNeighbourMines[row, col].ToString();
            display[row, col] = Convert.ToChar(numOfNeighbouringMinesToStr);
            RevealedCellsCount++;
            revealed[row, col] = true;
            if (display[row, col] == '0')
            {
                for (int previousRow = -1; previousRow <= 1; previousRow++)
                {
                    for (int previousCol = -1; previousCol <= 1; previousCol++)
                    {
                        int newRow = row + previousRow;
                        int newCol = col + previousCol;
                        if (InBoard(newRow, newCol) && revealed[newRow, newCol] == false)
                        {
                            RevealBlock(newRow, newCol);
                        }
                    }
                }
            }
        }

        public void RevealWholeBoard()
        {
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    if (!revealed[i, j])
                    {
                        display[i, j] = EMPTY_CELL;
                    }

                    if (CheckIfHasMine(i, j))
                    {
                        display[i, j] = MINE;
                    }
                }
            }
        }

        public bool CellIsRevealed(int row, int col)
        {
            return revealed[row, col];
        }
    }
}