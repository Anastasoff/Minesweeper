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

        public void Visit(RegularCell regularCell)
        {
            regularCell.SetNumberOfNeighbouringMines(numberOfMines);
        }

        public void Visit(MineCell mine)
        {
            throw new NotImplementedException();
        }
    }
}
