using System;
using System.Collections.Generic;
using System.Linq;

namespace MineSweeper
{
    internal class Scoreboard
    {
        private static List<Person> participants;

        private static Scoreboard top5 = null;

        private Scoreboard()
        {
            participants = new List<Person>();
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
        internal int MinInTop5()
        {
            if (participants.Count > 0)
                return participants.Last().Score;
            return -1;
        }

        internal void AddPlayer(int score)
        {
            Console.Write("Please enter your name for the top scoreboard: ");
            string name = Console.ReadLine();
            participants.Add(new Person(name, score));
            participants.Sort(new Comparison<Person>((p1, p2) => p2.Score.CompareTo(p1.Score)));
            participants = participants.Take(5).ToList();
        }

        internal void ShowHighScores()
        {
            Console.WriteLine("Scoreboard:");
            foreach (var p in participants)
            {
                Console.WriteLine(participants.IndexOf(p) + 1 + ". " + p.Name + " --> " + p.Score + " cells");
            }
            Console.WriteLine();
        }

        internal int Count()
        {
            return participants.Count();
        }
    }
}