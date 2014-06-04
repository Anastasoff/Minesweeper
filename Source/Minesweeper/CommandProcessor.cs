namespace Minesweeper
{
    using System;

    internal static class CommandProcessor
    {
        internal static int x { get; set; }

        internal static int y { get; set; }

        internal static bool Exit;

        internal static bool GetStatistic;
        internal static bool InvalidMove;
        internal static bool Restart;

        internal static void ReadCommand()
        {
            Clear();
            string command = Console.ReadLine();
            command = command.Trim();
            if (command == "exit")
            {
                Exit = true;
                return;
            }
            if (command == "top")
            {
                GetStatistic = true;
                return;
            }
            if (command == "restart")
            {
                Restart = true;
                return;
            }

            string[] point = command.Split(' ');
            if (point.Length != 2)
                InvalidMove = true;
            else
            {
                try
                {
                    x = Convert.ToInt32(point[0]);
                    y = Convert.ToInt32(point[1]);
                }
                catch (FormatException)
                {
                    InvalidMove = true;
                }
            }
        }

        internal static void Clear()
        {
            x = 0;
            y = 0;
            Exit = false;
            GetStatistic = false;
            InvalidMove = false;
            Restart = false;
        }
    }
}