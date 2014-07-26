namespace Minesweeper.GUI
{
    using System;
    using System.Collections.Generic;
    using Common;
    using Engine;
    using GameObjects;
    using Interfaces;

    /// <summary>
    /// Implements the IGameBoard interface
    /// </summary>
    public class GameBoard : IGameBoard
    {
        private const int ROWS = 5;
        private const int COLS = 10;
        private const int TOTAL_NUMBER_OF_CELLS = ROWS * COLS;
        private const int NUMBER_OF_MINES = 15;
        private const int DEFAULT_NUMBER_OF_NEIGHBOURING_MINES = 0;

        private static GameBoard board = null;

        private Cell[,] cellsMap;
        private IList<Position> minePositions;
        private Dictionary<Position, int> numbersPositions;
        private CellFactory cellCreator;
        private int revealedCellsCount;

        private IVisitor neighbouringMinesVisitor;
        private IVisitor flagVisitor;
        private IVisitor cellRevealingVisitor;

        /// <summary>
        /// Prevents a default instance of the GameBoard class from being created. Implements the Singleton Design Pattern.
        /// </summary>
        private GameBoard()
        {
            this.ResetBoard();
        }

        /// <summary>
        /// Gets an only instance of the GameBoard class.
        /// </summary>
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

        /// <summary>
        /// Gets initialized inside the class the Cell[,]
        /// </summary>
        public Cell[,] Board
        {
            get
            {
                return this.cellsMap;
            }
        }

        /// <summary>
        /// Return one cell from cells map.
        /// </summary>
        /// <param name="pos">Cell position</param>
        /// <returns>Cell</returns>
        public Cell GetCell(Position pos)
        {
            return cellsMap[pos.row, pos.col];
        }

        public int GetMaxRows
        {
            get
            {
                return ROWS;
            }
        }

        public int GetMaxCols
        {
            get
            {
                return COLS;
            }
        }

        /// <summary>
        /// Gets the value of the number of revealed cells
        /// </summary>
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

        /// <summary>
        /// The method resets the GameBoard.
        /// </summary>
        public void ResetBoard()
        {
            this.minePositions = new List<Position>();
            this.numbersPositions = new Dictionary<Position, int>();
            this.cellsMap = new Cell[ROWS, COLS];
            this.RevealedCellsCount = 0;
            this.cellCreator = new CellCreator();
            this.AllocateMines(RandomGenerator.GetInstance);
            this.InitializeBoardForDisplay();
        }

        /// <summary>
        /// Check if the specified position is inside the board and if the cell at that position is of type Mine.
        /// </summary>
        /// <param name="pos">Takes one parameter for the cell position to be checked.</param>
        /// <returns>Return true if is a mine ot that position.</returns>
        public bool CheckIfMineCanBePlaced(Position pos)
        {
            return this.IsInsideBoard(pos) && !this.CheckIfHasMine(pos);
        }

        /// <summary>
        /// Check if the specified position is inside the board.
        /// </summary>
        /// <param name="pos">Takes one Cell parameter - cell to be checked.</param>
        /// <returns>Return boolean value.</returns>
        public bool IsInsideBoard(Position pos)
        {
            bool isInHorizontalLimits = 0 <= pos.row && pos.row < ROWS;
            bool isInVerticalLimits = 0 <= pos.col && pos.col < COLS;
            return isInHorizontalLimits && isInVerticalLimits;
        }

        /// <summary>
        /// Check if the cell at that position is of type Mine.
        /// </summary>
        /// <param name="pos">Takes one Cell parameter - cell to be checked.</param>
        /// <returns>Return boolean value.</returns>
        public bool CheckIfHasMine(Position pos)
        {
            for (int i = 0; i < this.minePositions.Count; i++)
            {
                if (this.minePositions[i].IsEqual(pos))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Changes the state of the cell at the specified position and changes its state. Attempts to reveal a block of cells.
        /// </summary>
        /// <param name="pos">Takes one Cell parameter - cell to be revealed.</param>
        public void RevealBlock(Position pos)
        {
            var currentCell = this.GetCell(pos);
            this.cellRevealingVisitor = new CellRevealingVisitor();
            currentCell.Accept(this.cellRevealingVisitor);
            var regularCell = currentCell as SafeCell;

            this.RevealedCellsCount++;

            if (regularCell.NumberOfNeighbouringMines == DEFAULT_NUMBER_OF_NEIGHBOURING_MINES)
            {
                for (int previousRow = -1; previousRow <= 1; previousRow++)
                {
                    for (int previousCol = -1; previousCol <= 1; previousCol++)
                    {
                        Position newPos = new Position(pos.row + previousRow, pos.col + previousCol);
                        if (this.IsInsideBoard(newPos) && !this.IsCellRevealed(newPos))
                        {
                            this.RevealBlock(newPos);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// At the end of the game reveals the whole board and changes the type of the cells if necessary.
        /// </summary>
        public void RevealWholeBoard()
        {
            for (int row = 0; row < ROWS; row++)
            {
                for (int col = 0; col < COLS; col++)
                {
                    var currentCell = this.cellsMap[row, col];
                    var cellType = currentCell.Type;

                    switch (cellType)
                    {
                        case CellTypes.Safe:
                            if (!currentCell.IsCellRevealed)
                            {
                                currentCell.Type = CellTypes.Unrevealed_Regular_Cell;
                            }

                            break;
                        case CellTypes.Mine:
                            currentCell.IsCellRevealed = true;
                            break;
                        case CellTypes.Flag:
                            if (currentCell is SafeCell)
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

        /// <summary>
        /// Checks if the cell at the specified position has been revealed.
        /// </summary>
        /// <param name="pos">Takes one Cell parameter - cell to be checked.</param>
        /// <returns>Returns a boolean value.</returns>
        public bool IsCellRevealed(Position pos)
        {
            return this.GetCell(pos).IsCellRevealed;
        }

        /// <summary>
        /// Changes the type of the cell at the specified position.
        /// </summary>
        /// <param name="pos">Takes one Cell parameter - cell to where falg be placed.</param>
        public void PlaceFlag(Position pos)
        {
            this.flagVisitor = new FlagVisitor();
            this.cellsMap[pos.row, pos.col].Accept(this.flagVisitor);
        }

        /// <summary>
        /// Checks if all of the safe cells have been revealed.
        /// </summary>
        /// <returns>Return boolean value.</returns>
        public bool CheckIfGameIsWon()
        {
            int numberOfCellsLeft = TOTAL_NUMBER_OF_CELLS - this.revealedCellsCount;
            return numberOfCellsLeft == NUMBER_OF_MINES;
        }

        /// <summary>
        /// Checks if the cell at the specified position is of type Flag.
        /// </summary>
        /// <param name="pos">Takes one Cell parameter - cell to be checked.</param>
        /// <returns>Returns a boolean value.</returns>
        public bool CheckIfFlagCell(Position pos)
        {
            var currentCell = this.GetCell(pos);
            return currentCell.Type == CellTypes.Flag;
        }

        /// <summary>
        /// Creates an instance of the Memento class using the current GameBoard.
        /// </summary>
        /// <returns>Return instance of the Memento class.</returns>
        public Memento SaveMemento()
        {
            return new Memento(this.Board);
        }

        /// <summary>
        /// Sets the current cellsMap with the one saved in the Memento.
        /// </summary>
        /// <param name="memento">Takes one parameter of type Memento</param>
        public void RestoreMemento(Memento memento)
        {
            this.cellsMap = memento.CurrentBoard;
        }

        /// <summary>
        /// Fills the list with the positions at which we should have mines.
        /// </summary>
        /// <param name="generator">As its only parameter takes an instance of the Random class.</param>
        private void AllocateMines(Random generator)
        {
            int actualNumberOfMines = 0;
            while (actualNumberOfMines < NUMBER_OF_MINES)
            {
                Position newMinePos = new Position(generator.Next(ROWS), generator.Next(COLS));
                bool validPlaceForMine = this.CheckIfMineCanBePlaced(newMinePos);
                if (validPlaceForMine)
                {
                    this.minePositions.Add(newMinePos);
                    this.AllocateNeighbouringMines(newMinePos);
                    actualNumberOfMines++;
                }
            }
        }

        /// <summary>
        /// Counts the number of neighbouring mines for the specified position
        /// </summary>
        /// <param name="pos">Takes one Cell parameter - cell to be counted.</param>
        private void AllocateNeighbouringMines(Position pos)
        {
            for (int neighbouringRow = pos.row - 1; neighbouringRow <= pos.row + 1; neighbouringRow++)
            {
                for (int neighbouringCol = pos.col - 1; neighbouringCol <= pos.col + 1; neighbouringCol++)
                {
                    var position = new Position(neighbouringRow, neighbouringCol);
                    if (CheckIfMineCanBePlaced(position))
                    {
                        var numberOfNeighbouringMines = 0;

                        if (this.numbersPositions.ContainsKey(position))
                        {
                            this.numbersPositions[position]++;
                        }
                        else
                        {
                            numberOfNeighbouringMines = 1;
                            this.numbersPositions.Add(position, numberOfNeighbouringMines);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Initializes the GameBoard.
        /// </summary>
        private void InitializeBoardForDisplay()
        {
            for (int row = 0; row < ROWS; row++)
            {
                for (int col = 0; col < COLS; col++)
                {
                    var pos = new Position(row, col);
                    if (this.CheckIfHasMine(pos))
                    {
                        this.cellsMap[row, col] = this.cellCreator.CreateMineCell(pos);
                    }
                    else
                    {
                        this.cellsMap[row, col] = this.cellCreator.CreateSafeCell(pos);
                        var currentCellPosition = this.GetCell(pos).Coordinates;

                        if (this.CheckIfHasNeighbouringMines(currentCellPosition))
                        {
                            var numberOfMines = this.numbersPositions[currentCellPosition];
                            this.neighbouringMinesVisitor = new NeighbouringMinesVisitor(numberOfMines);
                            this.GetCell(pos).Accept(this.neighbouringMinesVisitor);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks if there are mines around the specified position.
        /// </summary>
        /// <param name="currentPosition">Takes one parameter of type Position.</param>
        /// <returns>Return boolean value.</returns>
        private bool CheckIfHasNeighbouringMines(Position currentPosition)
        {
            return this.numbersPositions.ContainsKey(currentPosition);
        }
    }
}