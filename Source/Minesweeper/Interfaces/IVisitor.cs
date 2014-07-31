namespace Minesweeper.Interfaces
{
    using GameObjects;

    /// <summary>
    /// Defines methods for a Visitor class
    /// </summary>
    public interface IVisitor
    {
        void Visit(Cell cellObject);
    }
}