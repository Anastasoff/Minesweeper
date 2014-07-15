namespace Minesweeper.GUI
{
    using System;

    public class GameBoard
    {
        private int revealedCellsCount;

        private static readonly int Rows = 5;
        private static readonly int Cols = 10;
        private static readonly int NumberOfMines = 15;
        private static readonly char UnrevealedCellChar = '?';

        private static char[,] display;
        private static bool[,] mineMap;
        private static bool[,] revealed;
        private static int[,] numberOfNeighbourMines;

        private static GameBoard board = null; // one and only instance of board

        private GameBoard() // private constructor
        {
            display = new char[Rows, Cols];
            mineMap = new bool[Rows, Cols];
            revealed = new bool[Rows, Cols];
            numberOfNeighbourMines = new int[Rows, Cols];
            InitializeBoardForDisplay();
            PutMines();
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

        private void PutMines()
        {
            Random generator = new Random();

            int actualNumberOfMines = 0;
            while (actualNumberOfMines < NumberOfMines)
            {
                if (PlaceMine(generator.Next(Rows), generator.Next(Cols)))
                {
                    actualNumberOfMines++;
                }
            }
        }

        public bool InBoard(int row, int col)
        {
            return 0 <= row && row < Rows && 0 <= col && col < Cols;
        }

        private bool PlaceMine(int row, int col)
        {
            if (!InBoard(row, col) || mineMap[row, col])
            {
                return false;
            }

            // TODO: simplify it
            mineMap[row, col] = true;
            for (int previousRow = -1; previousRow <= 1; previousRow++)
            {
                for (int previousCol = -1; previousCol <= 1; previousCol++)
                {
                    int newRow = row + previousRow;
                    int newCol = col + previousCol;
                    if (((previousRow != 0) || (previousCol != 0)) && InBoard(newRow, newCol))
                    {
                        numberOfNeighbourMines[newRow, newCol]++;
                    }
                }
            }

            return true;
        }

        private void InitializeBoardForDisplay()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    display[i, j] = UnrevealedCellChar; // TODO: extract '?' in constant
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
            Console.WriteLine(new string('-', 2 * Cols));
        }

        private void PrintFieldsNumberOfColumns()
        {
            for (int i = 0; i < Cols; i++)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();
        }

        private void PrintGameField()
        {
            for (int i = 0; i < Rows; i++)
            {
                Console.Write(i + " | ");
                for (int j = 0; j < Cols; j++)
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

        public bool HasMine(int row, int col)
        {
            return mineMap[row, col];
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
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    // TODO: extract symbols in constants
                    if (!revealed[i, j])
                    {
                        display[i, j] = '-';
                    }

                    if (mineMap[i, j])
                    {
                        display[i, j] = '*';
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