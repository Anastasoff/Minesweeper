namespace Minesweeper.GUI
{
    using System;
    using System.Linq;
    using Interfaces;

    public class KeyboardInput: IInputDevice
    {
        public string GetInput()
        {
            return Console.ReadLine().Trim();
        }
    }
}
