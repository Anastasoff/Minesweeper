namespace Minesweeper.Interfaces
{
    /// <summary>
    /// Defines methods to provide functionality for the Visitor pattern
    /// </summary>
    public interface IVisitable
    {
        void Accept(IVisitor visitor);
    }
}