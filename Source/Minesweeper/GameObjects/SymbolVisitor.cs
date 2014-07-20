namespace Minesweeper.GameObjects
{
    using System;

    using Interfaces;

    public class SymbolVisitor : IVisitor
    {
        public void Visit(RegularCell regularCell)
        {
            if (!regularCell.IsCellRevealed)
            {
                var numberOfMinesStr = regularCell.NumberOfNeighbouringMines.ToString();
                regularCell.Symbol = Convert.ToChar(numberOfMinesStr);   
            }

            regularCell.RevealCell();
        }

        public void Visit(MineCell mine)
        {
            if (!mine.IsCellRevealed)
            {
                mine.Symbol = mine.DEFAULT_MINE_CELL_SYMBOL;
            }

            mine.RevealCell();
        }
    }
}
