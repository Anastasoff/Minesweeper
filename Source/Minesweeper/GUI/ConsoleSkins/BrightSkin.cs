using Minesweeper.Interfaces;
using System;
using System.Collections.Generic;

namespace Minesweeper.GUI.ConsoleSkins
{
    /// <summary>
    /// This skin changes some of the colors of the digits and make them appear brighter
    /// </summary>
    public class BrightSkin : IConsoleSkin
    {
        private Dictionary<char, ConsoleColor> colorScheme = new Dictionary<char, ConsoleColor>()
        {
            { '1', ConsoleColor.Green }, 
            { '2', ConsoleColor.Cyan },
            { '3', ConsoleColor.Yellow },
            { '4', ConsoleColor.Red },
        };

        public Dictionary<char, ConsoleColor> ColorScheme
        {
            get
            {
                return this.colorScheme;
            }
        }
    }
}