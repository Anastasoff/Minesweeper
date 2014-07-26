namespace Minesweeper.Interfaces
{
    using System;
    using Engine;
    using GameObjects;

    public interface IGameBoard
    {
        Cell[,] Board { get; }

        int RevealedCellsCount { get; }

        void ResetBoard();

        bool CheckIfMineCanBePlaced(int row, int col);

        bool IsInsideBoard(int row, int col);

        bool CheckIfHasMine(int row, int col);

        void RevealBlock(int row, int col);

        void RevealWholeBoard();

        bool IsCellRevealed(int row, int col);

        void PlaceFlag(int row, int col);

        bool CheckIfGameIsWon();

        bool CheckIfFlagCell(int row, int col);

        Memento SaveMemento();

        void RestoreMemento(Memento memento);

    }
}
