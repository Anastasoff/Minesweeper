namespace Minesweeper.Interfaces
{
    using GameObjects;

    public interface IGameObject
    {
        char Symbol { get; set; }

        Position Coordinates { get; }

        bool IsCellRevealed { get; set; }
    }
}