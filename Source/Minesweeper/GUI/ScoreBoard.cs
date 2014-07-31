namespace Minesweeper.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;

    /// <summary>
    /// Contains functionality that allows the storage of scores
    /// </summary>
    public class Scoreboard : IScoreBoard
    {
        private static Scoreboard instance;
        private static IOInterface iface;
        private ICollection<IPlayer> allPlayers;

        private Scoreboard()
        {
            this.allPlayers = new List<IPlayer>();
        }

        /// <summary>
        /// Returns the instance of the Scoreboard class
        /// </summary>
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

        /// <summary>
        /// Sets the IO interface
        /// </summary>
        /// <param name="userInterractor">IOInterface instance</param>
        public void SetIOInterface(IOInterface userInterractor)
        {
            iface = userInterractor;
        }

        /// <summary>
        /// Adds a player to the scoreboard
        /// </summary>
        /// <param name="score">A score</param>
        public void AddPlayer(int score)
        {
            string name = iface.GetUserInput("Please enter your name for the top scoreboard: ");
            this.allPlayers.Add(new Player(name, score));
        }

        /// <summary>
        /// Prints the high scores
        /// </summary>
        public void ShowHighScores()
        {
            int counter = 1;
            var sortedPlayers = this.SortPlayersDescendingByScore(this.allPlayers);
            var topFivePlayers = this.GetTop5Results(sortedPlayers);
            
            iface.ShowMessage("Scoreboard:");
            foreach (var player in topFivePlayers)
            {
                iface.ShowMessage(counter + ". " + player.Name + " --> " + player.Score + " cells");
                counter++;
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Returns the count of players in the scoreboard
        /// </summary>
        /// <returns>An int value</returns>
        public int Count()
        {
            return this.allPlayers.Count();
        }

        private ICollection<IPlayer> SortPlayersDescendingByScore(ICollection<IPlayer> allPlayers)
        {
            var sortedPlayers = allPlayers.OrderByDescending(p => p.Score).ToList();
            return sortedPlayers;
        }

        private ICollection<IPlayer> GetTop5Results(ICollection<IPlayer> players)
        {
            return players.Take(5).ToList();
        }
    }
}