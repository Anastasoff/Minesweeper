namespace Minesweeper.GameObjects
{
    using System;
    using Interfaces;

    /// <summary>
    /// Inherits the abstract class Cell and creates an instance of type Safe cell.
    /// </summary>
    public class SafeCell : Cell
    {
        private const int INITIAL_MINES_COUNT = 0;

        private int numberOfNeighbouringMines;

        /// <summary>
        /// Initializes a new instance of the SafeCell class.
        /// </summary>
        /// <param name="row">Takes one integer parameter for the row of the cell.</param>
        /// <param name="col">Takes one integer parameter for the col of the cell.</param>
        public SafeCell(int row, int col)
            : base(row, col)
        {
            this.NumberOfNeighbouringMines = INITIAL_MINES_COUNT;
            this.Type = CellTypes.Safe;
        }

        /// <summary>
        /// Gets or sets the value of the neighbouring mine cells.
        /// </summary>
        public int NumberOfNeighbouringMines 
        {
            get 
            {
                return this.numberOfNeighbouringMines; 
            }

            set
            {
                this.numberOfNeighbouringMines = value;
            }
        }

        /// <summary>
        /// Accepts a visitor and allows it to make changes to the properties of the class.
        /// </summary>
        /// <param name="visitor">Takes one parameter of implementing the interface IVisitor.</param>
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
