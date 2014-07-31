using Minesweeper.Interfaces;
using System;
using System.Collections.Generic;

namespace Minesweeper.GUI.ConsoleSkins
{
    /// <summary>
    /// This skin doesn't change any colors
    /// </summary>
    public class AllWhiteSkin : IConsoleSkin
    {
        private Dictionary<char, ConsoleColor> colorScheme = new Dictionary<char, ConsoleColor>();

        public Dictionary<char, ConsoleColor> ColorScheme
        {
            get
            {
                return this.colorScheme;
            }
        }
    }
}
