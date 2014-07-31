namespace Minesweeper.GameObjects
{
    using System;
    using Interfaces;

    /// <summary>
    /// Inherits the abstract class Cell and creates an instance of type Mine cell.
    /// </summary>
    public class MineCell : Cell
    {
        /// <summary>
        /// Initializes a new instance of the MineCell class.
        /// </summary>
        /// <param name="row">Takes one integer parameter for the row of the cell.</param>
        /// <param name="col">Takes one integer parameter for the col of the cell.</param>
        public MineCell(Position pos) : base(pos)
        {
            this.Type = CellTypes.Mine;
        }

        /// <summary>
        /// Accepts a visitor and allows it to make changes to the properties of the class.
        /// </summary>
        /// <param name="visitor">Takes one parameter of implementing the interface IVisitor.</param>
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}