namespace Minesweeper.Interfaces
{
    using System;
    using GUI;

    public interface IMine
    {
        char Symbol { get; }

        Position Coordinates { get; }
    }
}
