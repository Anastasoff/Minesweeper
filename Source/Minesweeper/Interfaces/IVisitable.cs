namespace Minesweeper.Interfaces
{
    public interface IVisitable
    {
        void Accept(IVisitor visitor);
    }
}