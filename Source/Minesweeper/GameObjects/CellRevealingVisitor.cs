namespace Minesweeper.GameObjects
{
    using System;
    using Interfaces;

    /// <summary>
    /// Class implementing the IVisitor interface.
    /// </summary>
    public class CellRevealingVisitor : IVisitor
    {
        /// <summary>
        /// The method will visit the cell and will change it's state.
        /// </summary>
        /// <param name="regularCell">Takes one parameter of type Cell.</param>
        public void Visit(Cell regularCell)
        {
            regularCell.RevealCell();
        }
    }
}
