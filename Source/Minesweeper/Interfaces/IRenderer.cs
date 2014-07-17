namespace Minesweeper.Interfaces
{
    using System;

    public interface IRenderer
    {
        // TODO: should we create IRenderable interface and then give an instance to the methods here ?!?
        void DisplayGameBoard();

        void ClearBoard();
    }
}
