namespace Minesweeper.GUI
{
    using System;

    public class GameBoard
    {
        private int revealedCellsCount;

        private static readonly int SizeX = 5;
        private static readonly int SizeY = 10;
        private static readonly int NumberOfMines = 15;

        private static char[,] display;
        private static bool[,] mineMap;
        private static bool[,] revealed;
        private static int[,] numberOfNeighbourMines;

        private static GameBoard board = null; // one and only instance of board

        private GameBoard() // private constructor
        {
            display = new char[SizeX, SizeY];
            mineMap = new bool[SizeX, SizeY];
            revealed = new bool[SizeX, SizeY];
            numberOfNeighbourMines = new int[SizeX, SizeY];
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
                if (PlaceMine(generator.Next(SizeX), generator.Next(SizeY)))
                {
                    actualNumberOfMines++;
                }
            }
        }

        public bool InBoard(int x, int y)
        {
            return 0 <= x && x < SizeX && 0 <= y && y < SizeY;
        }

        private bool PlaceMine(int x, int y)
        {
            if (!InBoard(x, y) || mineMap[x, y])
            {
                return false;
            }

            // TODO: simplify it
            mineMap[x, y] = true;
            for (int xx = -1; xx <= 1; xx++)
            {
                for (int yy = -1; yy <= 1; yy++)
                {
                    if (((xx != 0) || (yy != 0)) && InBoard(x + xx, y + yy))
                    {
                        numberOfNeighbourMines[x + xx, y + yy]++;
                    }
                }
            }

            return true;
        }

        private void InitializeBoardForDisplay()
        {
            for (int i = 0; i < SizeX; i++)
            {
                for (int j = 0; j < SizeY; j++)
                {
                    display[i, j] = '?'; // TODO: extract '?' in constant
                }
            }
        }

        public void Display()
        {
            for (int i = 0; i < 4; i++)
            {
                Console.Write(" ");
            }
            for (int i = 0; i < SizeY; i++)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();
            for (int i = 0; i < 4; i++)
            {
                Console.Write(" ");
            }

            for (int i = 0; i < 2 * SizeY; i++)
            {
                Console.Write('-');
            }

            Console.WriteLine();
            for (int i = 0; i < SizeX; i++)
            {
                Console.Write(i + " | ");
                for (int j = 0; j < SizeY; j++)
                {
                    Console.Write(display[i, j] + " ");
                }

                Console.WriteLine("|");
            }

            for (int i = 0; i < 4; i++)
            {
                Console.Write(" ");
            }

            for (int i = 0; i < 2 * SizeY; i++)
            {
                Console.Write("-");
            }

            Console.WriteLine();
        }

        public bool HasMine(int x, int y)
        {
            return mineMap[x, y];
        }

        public void RevealBlock(int x, int y)
        {
            display[x, y] = Convert.ToChar(numberOfNeighbourMines[x, y].ToString());
            RevealedCellsCount++;
            revealed[x, y] = true;
            if (display[x, y] == '0')
            {
                for (int xx = -1; xx <= 1; xx++)
                {
                    for (int yy = -1; yy <= 1; yy++)
                    {
                        int newX = x + xx;
                        int newY = y + yy;
                        if (InBoard(newX, newY) && revealed[newX, newY] == false)
                        {
                            RevealBlock(newX, newY);
                        }
                    }
                }
            }
        }

        public void RevealWholeBoard()
        {
            for (int i = 0; i < SizeX; i++)
            {
                for (int j = 0; j < SizeY; j++)
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

        public bool CellIsRevealed(int x, int y)
        {
            return revealed[x, y];
        }
    }
}