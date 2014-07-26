﻿namespace Minesweeper.GUI
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
        /// <param name="row">Takes one integer parameter for the row to be checked.</param>
        /// <param name="col">Takes one integer parameter for the col to be checked.</param> 
        /// <returns>Return boolean value.</returns>
        public bool CheckIfMineCanBePlaced(int row, int col)
        {
            return this.IsInsideBoard(row, col) && !this.CheckIfHasMine(row, col);
        }

        /// <summary>
        /// Check if the specified position is inside the board.
        /// </summary>
        /// <param name="row">Takes one integer parameter for the row to be checked.</param>
        /// <param name="col">Takes one integer parameter for the col to be checked.</param> 
        /// <returns>Return boolean value.</returns>
        public bool IsInsideBoard(int row, int col)
        {
            bool isInHorizontalLimits = 0 <= row && row < ROWS;
            bool isInVerticalLimits = 0 <= col && col < COLS;
            return isInHorizontalLimits && isInVerticalLimits;
        }

        /// <summary>
        /// Check if the cell at that position is of type Mine.
        /// </summary>
        /// <param name="row">Takes one integer parameter for the row to be checked.</param>
        /// <param name="col">Takes one integer parameter for the col to be checked.</param> 
        /// <returns>Return boolean value.</returns>
        public bool CheckIfHasMine(int row, int col)
        {
            for (int i = 0; i < this.minePositions.Count; i++)
            {
                Position currentMinePosition = this.minePositions[i];
                int minePositionX = currentMinePosition.row;
                int minePositionY = currentMinePosition.col;

                if (minePositionX == row && minePositionY == col)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Changes the state of the cell at the specified position and changes its state. Attempts to reveal a block of cells.
        /// </summary>
        /// <param name="row">Takes one integer parameter for the row to be checked.</param>
        /// <param name="col">Takes one integer parameter for the col to be checked.</param> 
        public void RevealBlock(int row, int col)
        {
            var currentCell = this.cellsMap[row, col];
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
                        int newRow = row + previousRow;
                        int newCol = col + previousCol;
                        if (this.IsInsideBoard(newRow, newCol) && this.IsCellRevealed(newRow, newCol) == false)
                        {
                            this.RevealBlock(newRow, newCol);
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
        /// <param name="row">Takes one integer parameter for the row to be checked.</param>
        /// <param name="col">Takes one integer parameter for the col to be checked.</param> 
        /// <returns>Returns a boolean value.</returns>
        public bool IsCellRevealed(int row, int col)
        {
            return this.cellsMap[row, col].IsCellRevealed;
        }

        /// <summary>
        /// Changes the type of the cell at the specified position.
        /// </summary>
        /// <param name="row">Takes one integer parameter for the row for the cell which type would be changed to Flag.</param>
        /// <param name="col">Takes one integer parameter for the col for the cell which type would be changed to Flag.</param> 
        public void PlaceFlag(int row, int col)
        {
            this.flagVisitor = new FlagVisitor();
            this.cellsMap[row, col].Accept(this.flagVisitor);
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
        /// <param name="row">Takes one integer parameter for the row to be checked.</param>
        /// <param name="col">Takes one integer parameter for the col to be checked.</param> 
        /// <returns>Returns a boolean value.</returns>
        public bool CheckIfFlagCell(int row, int col)
        {
            var currentCell = this.cellsMap[row, col];
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
                int currentRow = generator.Next(ROWS);
                int currentCol = generator.Next(COLS);
                bool validPlaceForMine = this.CheckIfMineCanBePlaced(currentRow, currentCol);
                if (validPlaceForMine)
                {
                    this.minePositions.Add(new Position(currentRow, currentCol));
                    this.AllocateNeighbouringMines(currentRow, currentCol);
                    actualNumberOfMines++;
                }
            }
        }

        /// <summary>
        /// Counts the number of neighbouring mines for the specified position
        /// </summary>
        /// <param name="row">Takes one integer parameter for the row to be checked.</param>
        /// <param name="col">Takes one integer parameter for the col to be checked.</param> 
        private void AllocateNeighbouringMines(int row, int col)
        {
            for (int neighbouringRow = row - 1; neighbouringRow <= row + 1; neighbouringRow++)
            {
                for (int neighbouringCol = col - 1; neighbouringCol <= col + 1; neighbouringCol++)
                {
                    if (this.IsInsideBoard(neighbouringRow, neighbouringCol) && !this.CheckIfHasMine(neighbouringRow, neighbouringCol))
                    {
                        var position = new Position(neighbouringRow, neighbouringCol);
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
                    if (this.CheckIfHasMine(row, col))
                    {
                        this.cellsMap[row, col] = this.cellCreator.CreateMineCell(row, col);
                    }
                    else
                    {
                        this.cellsMap[row, col] = this.cellCreator.CreateSafeCell(row, col);
                        var currentCellPosition = this.cellsMap[row, col].Coordinates;

                        if (this.CheckIfHasNeighbouringMines(currentCellPosition))
                        {
                            var numberOfMines = this.numbersPositions[currentCellPosition];
                            this.neighbouringMinesVisitor = new NeighbouringMinesVisitor(numberOfMines);
                            this.cellsMap[row, col].Accept(this.neighbouringMinesVisitor);
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