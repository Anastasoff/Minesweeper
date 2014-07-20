namespace Minesweeper.Interfaces
{
    using System;

    using GameObjects;

    public interface IVisitor
    {
        void Visit(Cell cellObject);
    }
}
