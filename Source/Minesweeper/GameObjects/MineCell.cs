namespace Minesweeper.GameObjects
{
    using System;

    using Interfaces;

    public class MineCell : Cell
    {
        public MineCell(int row, int col)
            : base(row, col)
        {
            this.Type = CellTypes.Mine;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
