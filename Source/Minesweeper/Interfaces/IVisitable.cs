namespace Minesweeper.Interfaces
{
    using System;

    public interface IVisitable
    {
        void Accept(IVisitor visitor);
    }
}
