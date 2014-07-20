namespace Minesweeper.GameObjects
{
    using System;

    using Interfaces;

    public class FlagVisitor : IVisitor
    {
        public void Visit(Cell regularCell)
        {
            if (!regularCell.IsCellRevealed)
            {
                regularCell.Type = CellTypes.Flag;
            }
        }
    }
}
