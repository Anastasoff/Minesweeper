namespace Minesweeper.Interfaces
{
    using GameObjects;

    /// <summary>
    /// Defines properties and methods of a game object
    /// </summary>
    public interface IGameObject
    {
        Position Coordinates { get; }

        bool IsCellRevealed { get; set; }

        CellTypes Type { get; set; }

        void RevealCell();
    }
}