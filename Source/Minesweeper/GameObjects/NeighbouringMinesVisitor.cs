namespace Minesweeper.GameObjects
{
    using System;
    using Interfaces;

    /// <summary>
    /// Class implementing the IVisitor interface.
    /// </summary>
    public class NeighbouringMinesVisitor : IVisitor
    {
        private int numberOfMines;

        /// <summary>
        /// Initializes a new instance of the NeighbouringMinesVisitor class.
        /// </summary>
        /// <param name="numberOfMines">Takes one parameter of type integer for the number of neighbouring mines.</param>
        public NeighbouringMinesVisitor(int numberOfMines)
        {
            this.numberOfMines = numberOfMines;
        }

        /// <summary>
        /// Checks if the passed cell is of type Safe and it true, sets its number of neighbouringMines.
        /// </summary>
        /// <param name="regularCell">Takes one parameter of type Cell.</param>
        public void Visit(Cell regularCell)
        {
            if (regularCell is SafeCell)
            {
                var cell = regularCell as SafeCell;
                cell.NumberOfNeighbouringMines = this.numberOfMines;
            }
        }
    }
}
