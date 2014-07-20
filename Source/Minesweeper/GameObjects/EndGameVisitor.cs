namespace Minesweeper.GameObjects
{
    using System;

    using Interfaces;

    public class EndGameVisitor : IVisitor
    {
        private const char DEFAULT_REGULAR_CELL_SYMBOL = '-';

        public void Visit(RegularCell regularCell)
        {
            if (!regularCell.IsCellRevealed)
            {
                regularCell.Symbol = DEFAULT_REGULAR_CELL_SYMBOL;   
            }
        }

        public void Visit(MineCell mine)
        {
            mine.Symbol = MineCell.DEFAULT_MINE_CELL_SYMBOL;
        }
    }
}
