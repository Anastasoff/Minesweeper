using System;
using System.Linq;
using Minesweeper.Interfaces;

namespace Minesweeper.GUI
{
    public class KeyboardInput:IInputDevice
    {
        public string GetInput()
        {
            return Console.ReadLine().Trim();
        }
    }
}
