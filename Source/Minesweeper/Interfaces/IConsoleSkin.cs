namespace Minesweeper.Interfaces
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines properties that allow the extraction of char-color color schemes
    /// </summary>
    public interface IConsoleSkin
    {
        Dictionary<char, ConsoleColor> ColorScheme { get; }
    }
}