namespace Minesweeper.GameObjects
{
    using System;
    using Interfaces;

    public abstract class Cell : IGameObject, IVisitable
    {
        private Position coordinates;
        private bool isCellRevealed;
        private CellTypes type;

        protected Cell(int row, int col)
        {
            this.isCellRevealed = false;
            this.Coordinates = new Position(row, col);
        }

        public Position Coordinates
        {
            get 
            {
                return this.coordinates; 
            }

            protected set
            {
                if (value.row < 0 || value.col < 0)
                {
                    throw new ArgumentOutOfRangeException("The position parameters must be positive values!");
                }

                this.coordinates = value;
            }
        }
        
        public bool IsCellRevealed
        {
            get 
            { 
                return this.isCellRevealed;
            }

            set
            {
                this.isCellRevealed = value;
            }
        }

        public CellTypes Type
        {
            get 
            {
                return this.type; 
            }

            set 
            { 
                this.type = value;
            }
        }

        public void RevealCell()
        { 
            if (!this.IsCellRevealed)                        
            { 
                this.IsCellRevealed = true;                           
            }
        }

        public abstract void Accept(IVisitor visitor);
    }
}