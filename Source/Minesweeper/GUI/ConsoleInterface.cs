namespace Minesweeper.GUI
{
    using System;

    using GameObjects;
    using Interfaces;

    class ConsoleInterface : IOInterface
    {
        private const char DEFAULT_FLAG_SYMBOL = 'F';
        private const char DEFAULT_MINE_CELL_SYMBOL = '*';
        private const char DEFAULT_REGULAR_CELL_SYMBOL = '-';
        private const char DEFAULT_UNREVEALED_CELL_SYMBOL = '?';

        private IConsoleSkin Skin { get; set; }

        public ConsoleInterface()
        {
            
        }

        public ConsoleInterface(IConsoleSkin skin)
        {
            this.Skin = skin;
        }

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
            Console.Clear();
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
            for (int row = 0; row < rows; row++)
            {
                Console.Write(row + " | ");
                for (int col = 0; col < cols; col++)
                {
                    var currentCell = board[row, col];
                    SetCellSymbol(currentCell);
                }

                Console.WriteLine("|");
            }
        }

        private void SetCellSymbol(Cell currentCell)
        {
            var cellType = currentCell.Type;

            switch (cellType)
            {
                case CellTypes.Regular:
                    var cell = currentCell as RegularCell;
                    var numberOfNeighbouringMinesToStr = cell.NumberOfNeighbouringMines.ToString();
                    var cellSymbol = Convert.ToChar(numberOfNeighbouringMinesToStr);
                    SetRegularAndMineCellsSymbol(currentCell, cellSymbol, DEFAULT_UNREVEALED_CELL_SYMBOL);
                    break;
                case CellTypes.Mine:
                    SetRegularAndMineCellsSymbol(currentCell, DEFAULT_MINE_CELL_SYMBOL, DEFAULT_UNREVEALED_CELL_SYMBOL);
                    break;
                case CellTypes.Flag:
                    Console.Write(DEFAULT_FLAG_SYMBOL + " ");
                    break;
                case CellTypes.Unrevealed_Regular_Cell:
                    Console.Write(DEFAULT_REGULAR_CELL_SYMBOL + " ");
                    break;
                default:
                    break;
            }
        }

        private void SetRegularAndMineCellsSymbol(Cell currentCell, char revealedCellSymbol, char unrevealedCellSymbol)
        {
            if (currentCell.IsCellRevealed)
            {
                Console.Write(revealedCellSymbol + " ");
            }
            else
            {
                Console.Write(unrevealedCellSymbol + " ");
            }
        }
    }
}
