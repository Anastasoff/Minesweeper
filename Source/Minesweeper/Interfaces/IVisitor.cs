namespace Minesweeper.Interfaces
{
    using GameObjects;

    public interface IVisitor
    {
        void Visit(Cell cellObject);
    }
}