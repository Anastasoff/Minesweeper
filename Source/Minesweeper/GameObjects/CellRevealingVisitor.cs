namespace Minesweeper.GameObjects
{
    using System;

    using Interfaces;

    public class CellRevealingVisitor : IVisitor
    {
        public void Visit(Cell regularCell)
        {
            regularCell.RevealCell();
        }
    }
}
