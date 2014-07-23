using System;
using System.Collections.Generic;

namespace Minesweeper.Interfaces
{
    public interface IConsoleSkin
    {
        Dictionary<char, ConsoleColor> ColorScheme { get; }
    }
}
