namespace Minesweeper.GUI
{
    using System;
    using System.Collections.Generic;

    using Common;
    using GameObjects;
    using Interfaces;

    public class GameBoard
    {
        private int revealedCellsCount;

        private const int ROWS = 5;
        private const int COLS = 10;
        private const int TOTAL_NUMBER_OF_CELLS = ROWS * COLS;
        private const int NUMBER_OF_MINES = 15;
        private const int DEFAULT_NUMBER_OF_NEIGHBOURING_MINES = 0;

        private Cell[,] cellsMap;

        private static GameBoard board = null; // one and only instance of board

        private IList<Position> minePositions;
        private Dictionary<Position, int> numbersPositions;

        private IVisitor neighbouringMinesVisitor;
        private IVisitor flagVisitor;
        private IVisitor cellRevealingVisitor;

        //private GameBoard() // private constructor
        //{
        //    display = new char[ROWS, COLS];
        //    this.mineMap = new List<IGameObject>();
        //    this.flagMap = new List<IGameObject>();
        //    numberOfNeighbourMines = new int[ROWS, COLS];
        //    InitializeBoardForDisplay();
        //    AllocateMines(RandomGenerator.GetInstance);
        //}

        //public static void ResetBoard() // I don't think that it's a best implementation. If any have better idea ...
        //{
        //    board = new GameBoard();
        //}

        private GameBoard() // private constructor
        {
            ResetBoard();
        }

        public void ResetBoard()
        {
            this.minePositions = new List<Position>();
            this.numbersPositions = new Dictionary<Position, int>();
            this.cellsMap = new Cell[ROWS, COLS];
            this.RevealedCellsCount = 0;
            AllocateMines(RandomGenerator.GetInstance);
            InitializeBoardForDisplay();
        }

        public Cell[,] Board
        {
            get 
            { 
                return cellsMap; 
            }
            
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
        
        // Излагаме се ... кой го написа това? 10 лицеви опори веднага! :)
        //public bool CheckIfMineCanBePlaced(int row, int col)// a separate method checking for valid placement of the mine
        //{
        //    if (!IsInsideBoard(row, col) || CheckIfHasMine(row, col))
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}

        public bool CheckIfMineCanBePlaced(int row, int col)
        {
            return IsInsideBoard(row, col) && !CheckIfHasMine(row, col);
        }

        public bool IsInsideBoard(int row, int col)
        {
            bool isInHorizontalLimits = 0 <= row && row < ROWS;
            bool isInVerticalLimits = 0 <= col && col < COLS;
            return isInHorizontalLimits && isInVerticalLimits;
        }

        public bool CheckIfHasMine(int row, int col)
        {
            for (int i = 0; i < this.minePositions.Count; i++)
            {
                Position currentMinePosition = minePositions[i];
                int minePositionX = currentMinePosition.row;
                int minePositionY = currentMinePosition.col;

                if (minePositionX == row && minePositionY == col)
                {
                    return true;
                }
            }

            return false;
        }

        public void RevealBlock(int row, int col)
        {
            var currentCell = cellsMap[row, col];
            this.cellRevealingVisitor = new CellRevealingVisitor();
            currentCell.Accept(cellRevealingVisitor);
            var regularCell = currentCell as RegularCell;

            this.RevealedCellsCount++;

            if (regularCell.NumberOfNeighbouringMines == DEFAULT_NUMBER_OF_NEIGHBOURING_MINES)
            {
                for (int previousRow = -1; previousRow <= 1; previousRow++)
                {
                    for (int previousCol = -1; previousCol <= 1; previousCol++)
                    {
                        int newRow = row + previousRow;
                        int newCol = col + previousCol;
                        if (IsInsideBoard(newRow, newCol) && IsCellRevealed(newRow, newCol) == false)
                        {
                            RevealBlock(newRow, newCol);
                        }
                    }
                }
            }
        }

        public void RevealWholeBoard()
        {
            for (int row = 0; row < ROWS; row++)
            {
                for (int col = 0; col < COLS; col++)
                {
                    var currentCell = cellsMap[row, col];
                    var cellType = currentCell.Type;

                    switch (cellType)
                    {
                        case CellTypes.Regular:
                            if (!currentCell.IsCellRevealed)
                            {
                                currentCell.Type = CellTypes.Unrevealed_Regular_Cell;
                            }
                            break;
                        case CellTypes.Mine:
                            currentCell.IsCellRevealed = true;
                            break;
                        case CellTypes.Flag:
                            if (currentCell is RegularCell)
                            {
                                currentCell.Type = CellTypes.Unrevealed_Regular_Cell;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public bool IsCellRevealed(int row, int col)
        {
            //var cellToCheck = display[row, col];
            //if (cellToCheck == UNREVEALED_CELL_CHAR)
            //{
            //    return false;
            //}

            //return true;

            return cellsMap[row, col].IsCellRevealed;
        
        }

        public void PlaceFlag(int row, int col)
        {
            this.flagVisitor = new FlagVisitor();
            cellsMap[row, col].Accept(flagVisitor);
        }

        public bool CheckIfGameIsWon()
        {
            int numberOfCellsLeft = TOTAL_NUMBER_OF_CELLS - revealedCellsCount;

            //if (numberOfCellsLeft == NUMBER_OF_MINES)
            //{
            //    return true;
            //}

            //return false;
            return numberOfCellsLeft == NUMBER_OF_MINES;
        }

        public bool CheckIfFlagCell(int row, int col)
        {
            var currentCell = cellsMap[row, col];
            return currentCell.Type == CellTypes.Flag;
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

        private void PlaceMine(int row, int col)
        {
            this.minePositions.Add(new Position(row, col)); // TODO: SOLID
            for (int neighbouringRow = row - 1; neighbouringRow <= row + 1; neighbouringRow++)
            {
                for (int neighbouringCol = col - 1; neighbouringCol <= col + 1; neighbouringCol++)
                {
                    if (IsInsideBoard(neighbouringRow, neighbouringCol) && !(CheckIfHasMine(neighbouringRow, neighbouringCol)))
                    {
                        var position = new Position(neighbouringRow, neighbouringCol);
                        var numberOfNeighbouringMines = 0;

                        if (numbersPositions.ContainsKey(position))
                        {
                            numbersPositions[position]++;
                        }
                        else
                        {
                            numberOfNeighbouringMines = 1;
                            numbersPositions.Add(position, numberOfNeighbouringMines);
                        }
                    }
                }
            }
        }

        private void InitializeBoardForDisplay()
        {
            for (int row = 0; row < ROWS; row++)
            {
                for (int col = 0; col < COLS; col++)
                {
                    if (CheckIfHasMine(row, col))
                    {
                        cellsMap[row, col] = new MineCell(row, col);
                    }
                    else
                    {
                        cellsMap[row, col] = new RegularCell(row, col);
                        var currentCellPosition = cellsMap[row, col].Coordinates;

                        if (CheckIfHasNeighbouringMines(currentCellPosition))
                        {
                            var numberOfMines = numbersPositions[currentCellPosition];
                            this.neighbouringMinesVisitor = new NeighbouringMinesVisitor(numberOfMines);
                            cellsMap[row, col].Accept(this.neighbouringMinesVisitor);
                        }
                    }
                }
            }
        }

        private bool CheckIfHasNeighbouringMines(Position currentPosition)
        {
            return numbersPositions.ContainsKey(currentPosition);
        }
    }
}