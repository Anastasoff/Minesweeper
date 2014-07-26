namespace Minesweeper.GameObjects
{
    using System;
    using Interfaces;

    /// <summary>
    /// Class implementing the IVisitor interface.
    /// </summary>
    public class FlagVisitor : IVisitor
    {
        /// <summary>
        /// Checks if the cell has been revealed and if not changes its type to Flag.
        /// </summary>
        /// <param name="regularCell">Takes one parameter of type Cell.</param>
        public void Visit(Cell regularCell)
        {
            if (!regularCell.IsCellRevealed)
            {
                regularCell.Type = CellTypes.Flag;
            }
        }
    }
}
