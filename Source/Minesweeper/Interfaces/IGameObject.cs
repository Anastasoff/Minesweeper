namespace Minesweeper.Interfaces
{
    using GameObjects;

    public interface IGameObject
    {
        Position Coordinates { get; }

        bool IsCellRevealed { get; set; }

        CellTypes Type { get; set; }

        void RevealCell();
    }
}