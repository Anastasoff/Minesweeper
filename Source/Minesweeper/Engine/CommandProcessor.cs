namespace Minesweeper.Engine
{
    using System;

    internal static class CommandProcessor
    {
        internal static int x { get; set; }

        internal static int y { get; set; }

        internal static Command command;

        internal static void ReadCommand()
        {
            Clear();
            string commandRead = Console.ReadLine();
            commandRead = commandRead.Trim();

            switch (commandRead)
            {
                case "exit": command = Command.Exit;
                    break;
                case "top": command = Command.Top;
                    break;
                case "restart": command = Command.Restart;
                    break;
                default: SetCoordinates(commandRead);
                    break;
            }
        }

        public static void SetCoordinates(string commandRead)
        {
            string[] point = commandRead.Split(' ');
            if (point.Length != 2)
            {
                command = Command.InvalidMove;
            }
            else
            {
                try
                {
                    x = Convert.ToInt32(point[0]);
                    y = Convert.ToInt32(point[1]);
                    command = Command.ValidMove;
                }
                catch (FormatException)
                {
                    command = Command.InvalidMove;
                }
            }
        }

        internal static void Clear()
        {
            x = 0;
            y = 0;
        }
    }
}