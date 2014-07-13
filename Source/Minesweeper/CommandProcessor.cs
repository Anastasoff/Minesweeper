namespace Minesweeper
{
    using System;

    internal static class CommandProcessor
    {
        internal static int x { get; set; }

        internal static int y { get; set; }

        internal static Commands command;

        internal static void ReadCommand()
        {
            Clear();
            string commandRead = Console.ReadLine();
            commandRead = commandRead.Trim();

            switch (commandRead)
            {
                case "exit": command = Commands.Exit;
                    break;
                case "top": command = Commands.Top;
                    break;
                case "restart": command = Commands.Restart;
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
                command = Commands.InvalidMove;
            }
            else
            {
                try
                {
                    x = Convert.ToInt32(point[0]);
                    y = Convert.ToInt32(point[1]);
                    command = Commands.ValidMove;
                }
                catch (FormatException)
                {
                    command = Commands.InvalidMove;
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