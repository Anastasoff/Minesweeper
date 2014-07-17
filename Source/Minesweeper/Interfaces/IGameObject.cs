namespace Minesweeper.Interfaces
{
    using GameObjects;

    public interface IGameObject
    {
        char Symbol { get; }

        Position Coordinates { get; }
    }
}