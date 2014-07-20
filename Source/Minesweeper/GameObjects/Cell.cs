namespace Minesweeper.GameObjects
{
    using System;

    using Interfaces;

    public abstract class Cell : IGameObject, IVisitable
    {
        private const char DEFAULT_SYMBOL = '?';

        private char symbol;
        private Position coordinates;
        private bool isCellRevealed;

        protected Cell(int row, int col)
        {
            this.Symbol = DEFAULT_SYMBOL;
            this.Coordinates = new Position(row, col);
            
        }

        public char Symbol
        {
            get { return this.symbol; }

            set
            {
                this.symbol = value; // I think there is no need for checking if value is a valid char since the new symbol would be set only inside the inheriting classes
            }
        }

        public Position Coordinates
        {
            get { return this.coordinates; }

            protected set
            {
                this.coordinates = value;
            }
        }

        public bool IsCellRevealed
        {
            get { return this.isCellRevealed; }

            set
            {
                this.isCellRevealed = value;
            }
        }

        public void RevealCell()                                      
        {                                                             
            if (this.Symbol != DEFAULT_SYMBOL)                        
            {                                                         
                this.IsCellRevealed = true;                           
            }
        }

        public abstract void Accept(IVisitor visitor);
    }
}
