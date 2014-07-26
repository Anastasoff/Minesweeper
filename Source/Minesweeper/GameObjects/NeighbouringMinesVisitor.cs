namespace Minesweeper.GameObjects
{
    using System;

    using Interfaces;

    public class NeighbouringMinesVisitor : IVisitor
    {
        private int numberOfMines;

        public NeighbouringMinesVisitor(int numberOfMines)
        {
            this.numberOfMines = numberOfMines;
        }

        public void Visit(Cell regularCell)
        {
            if (regularCell is SafeCell)
            {
                var cell = regularCell as SafeCell;
                cell.NumberOfNeighbouringMines = numberOfMines;
            }
        }
    }
}
