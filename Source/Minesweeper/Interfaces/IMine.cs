namespace Minesweeper.Interfaces
{
    using GameObjects;

    public interface IMine
    {
        char Symbol { get; }

        Position Coordinates { get; }
    }
}