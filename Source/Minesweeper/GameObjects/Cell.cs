namespace Minesweeper.GameObjects
{
    using System;
    using Interfaces;

    /// <summary>
    /// Abstract class for the Cell, it implements both the IGameObject and IVisitable interfaces.
    /// </summary>
    public abstract class Cell : IGameObject, IVisitable
    {
        private Position coordinates;
        private bool isCellRevealed;
        private CellTypes type;

        /// <summary>
        /// The constructor can be used by the inheriting classes.
        /// </summary>
        /// <param name="row">Takes one integer parameter for the row of the cell.</param>
        /// <param name="col">Takes one integer parameter for the col of the cell.</param>
        protected Cell(Position pos)
        {
            this.isCellRevealed = false;
            this.Coordinates = new Position(pos.row, pos.col);
        }

        /// <summary>
        /// Gets or sets a value for the position of the cell.
        /// </summary>
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

                this.coordinates.row = value.row;
                this.coordinates.col = value.col;
            }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether the state of the cell has been changed.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the type of the cell.
        /// </summary>
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

        /// <summary>
        /// The method changes the state of the cell and from unrevealed marks it as revealed.
        /// </summary>
        public void RevealCell()
        { 
            if (!this.IsCellRevealed)                        
            { 
                this.IsCellRevealed = true;                           
            }
        }

        /// <summary>
        /// Accepts a visitor and allows it to make changes to the properties of the class.
        /// </summary>
        /// <param name="visitor">Takes one parameter of implementing the interface IVisitor.</param>
        public abstract void Accept(IVisitor visitor);
    }
}