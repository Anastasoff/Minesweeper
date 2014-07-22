﻿namespace Minesweeper.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Interfaces;

    public class Scoreboard : IScoreBoard
    {
        private ICollection<IPlayer> allPlayers;
        private static Scoreboard instance;
        private static IOInterface iface;

        private Scoreboard()
        {
            allPlayers = new List<IPlayer>();
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
        public void SetIOInterface(IOInterface iface)
        {
            iface = iface;
        }

        public int MinInTop5()
        {
            if (allPlayers.Count > 0)
            {
                return allPlayers.Last().Score;
            }

            return -1;
        }

        public void AddPlayer(int score)
        {
            string name = iface.GetUserInput("Please enter your name for the top scoreboard: ");
            allPlayers.Add(new Player(name, score));
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

        public void ShowHighScores()
        {
            int counter = 1;
            var sortedPlayers = SortPlayersDescendingByScore(this.allPlayers);
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
            return allPlayers.Count();
        }
    }
}