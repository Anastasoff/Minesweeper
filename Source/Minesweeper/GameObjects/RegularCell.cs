namespace Minesweeper.GameObjects
{
    using System;

    using Interfaces;

    public class RegularCell : Cell
    {
        private const int INITIAL_MINES_COUNT = 0;

        private int numberOfNeighbouringMines;

        public RegularCell(int row, int col)
            : base(row, col)
        {
            this.NumberOfNeighbouringMines = INITIAL_MINES_COUNT;
        }

        public int NumberOfNeighbouringMines 
        {
            get { return this.numberOfNeighbouringMines; }

            private set
            {
                this.numberOfNeighbouringMines = value;
            }
        }

        public void SetNumberOfNeighbouringMines(int numberOfMines)
        {
            this.NumberOfNeighbouringMines = numberOfMines;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
