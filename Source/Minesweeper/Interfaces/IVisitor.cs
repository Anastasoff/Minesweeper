namespace Minesweeper.Interfaces
{
    using System;

    using GameObjects;

    public interface IVisitor
    {
        void Visit(RegularCell regularCell);

        void Visit(MineCell mine);
    }
}
