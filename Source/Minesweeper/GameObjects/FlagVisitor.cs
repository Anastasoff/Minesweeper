namespace Minesweeper.GameObjects
{
    using System;

    using Interfaces;

    public class FlagVisitor : IVisitor
    {
        private const char DEFAULT_FLAG_SYMBOL = 'F';

        public void Visit(RegularCell regularCell)
        {
            if (!regularCell.IsCellRevealed)
            {
                regularCell.Symbol = DEFAULT_FLAG_SYMBOL;
            }
        }

        public void Visit(MineCell mine)
        {
            if (!mine.IsCellRevealed)
            {
                mine.Symbol = DEFAULT_FLAG_SYMBOL;
            }
        }
    }
}
