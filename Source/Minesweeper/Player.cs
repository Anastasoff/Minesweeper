namespace Minesweeper
{
    using System;
    using Interfaces;

    /// <summary>
    /// Allows creation and manipualtion of a player
    /// </summary>
    public class Player : IPlayer
    {
        private const int MaxNameLength = 10;
        private string name;
        private int score;

        /// <summary>
        /// Constructor for the Player class
        /// </summary>
        /// <param name="name">The name of the player</param>
        /// <param name="score">The player score</param>
        public Player(string name, int score)
        {
            this.Name = name;
            this.Score = score;
        }
        
        /// <summary>
        /// Gets or sets the name of the player
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    this.name = "Unnamed player";
                }
                else if (value.Length > MaxNameLength)
                {
                    // TODO: this exception must be caught
                    string message = string.Format("Name must be no longer than {0} characters.", MaxNameLength);
                    throw new ArgumentOutOfRangeException(message);
                }
                else
                {
                    this.name = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the player score
        /// </summary>
        public int Score
        {
            get
            {
                return this.score;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Score must be non-negative integer.");
                }

                this.score = value;
            }
        }
    }
}