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

        bool CheckIfMineCanBePlaced(Position pos);

        bool IsInsideBoard(Position pos);

        bool CheckIfHasMine(Position pos);

        void RevealBlock(Position pos);

        void RevealWholeBoard();

        bool IsCellRevealed(Position pos);

        void PlaceFlag(Position pos);

        bool CheckIfGameIsWon();

        bool CheckIfFlagCell(Position pos);

        Memento SaveMemento();

        void RestoreMemento(Memento memento);

    }
}
