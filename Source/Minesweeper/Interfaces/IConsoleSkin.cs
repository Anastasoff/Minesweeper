namespace Minesweeper.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IConsoleSkin
    {
        Dictionary<char, ConsoleColor> ColorScheme { get; }
    }
}