namespace Minesweeper.GameObjects
{
    using System;

    using Interfaces;

    public class MineCell : Cell
    {
        // had to make the field readonly in order to access it from the visitor classes
        public readonly char DEFAULT_MINE_CELL_SYMBOL = '*'; 

        public MineCell(int row, int col)
            :base(row, col)
        {
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
