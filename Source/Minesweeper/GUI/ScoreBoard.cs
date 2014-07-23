namespace Minesweeper.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;

    public class Scoreboard : IScoreBoard
    {
        private static Scoreboard instance;
        private static IOInterface iface;
        private ICollection<IPlayer> allPlayers;

        private Scoreboard()
        {
            this.allPlayers = new List<IPlayer>();
        }

        public static Scoreboard GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Scoreboard();
                    iface = new ConsoleInterface();
                }

                return instance;
            }
        }

        public void SetIOInterface(IOInterface userInterractor)
        {
            iface = userInterractor;
        }

        public int MinInTop5()
        {
            if (this.allPlayers.Count > 0)
            {
                return this.allPlayers.Last().Score;
            }

            return -1;
        }

        public void AddPlayer(int score)
        {
            string name = iface.GetUserInput("Please enter your name for the top scoreboard: ");
            this.allPlayers.Add(new Player(name, score));
        }

        public void ShowHighScores()
        {
            int counter = 1;
            var sortedPlayers = this.SortPlayersDescendingByScore(this.allPlayers);
            iface.ShowMessage("Scoreboard:");
            foreach (var player in sortedPlayers)
            {
                iface.ShowMessage(counter + ". " + player.Name + " --> " + player.Score + " cells");
                counter++;
            }

            Console.WriteLine();
        }

        public int Count()
        {
            return this.allPlayers.Count();
        }

        private ICollection<IPlayer> SortPlayersDescendingByScore(ICollection<IPlayer> allPlayers)
        {
            var sortedPlayers = allPlayers.OrderByDescending(p => p.Score).ToList();
            return sortedPlayers;
        }

        private ICollection<IPlayer> GetTop5Results()
        {
            return this.allPlayers.Take(5).ToList();
        }
    }
}