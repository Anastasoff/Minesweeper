namespace Minesweeper.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Scoreboard //changed to public
    {
        private static List<Player> participants;

        private static Scoreboard top5 = null;

        public Scoreboard()
        {
            participants = new List<Player>();
        }

        public static Scoreboard GetTop5
        {
            get
            {
                if (top5 == null)
                {
                    top5 = new Scoreboard();
                }

                return top5;
            }
        }

        public int MinInTop5()
        {
            if (participants.Count > 0)
            {
                return participants.Last().Score;
            }

            return -1;
        }

        public void AddPlayer(int score)
        {
            Console.Write("Please enter your name for the top scoreboard: ");
            string name = Console.ReadLine();
            participants.Add(new Player(name, score));
            participants.Sort(new Comparison<Player>((p1, p2) => p2.Score.CompareTo(p1.Score)));
            participants = participants.Take(5).ToList();
        }

        public void ShowHighScores()
        {
            Console.WriteLine("Scoreboard:");
            foreach (var p in participants)
            {
                Console.WriteLine(participants.IndexOf(p) + 1 + ". " + p.Name + " --> " + p.Score + " cells");
            }

            Console.WriteLine();
        }

        public int Count()
        {
            return participants.Count();
        }
    }
}