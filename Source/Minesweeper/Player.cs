namespace Minesweeper
{
    using Interfaces;
    using System;

    public class Player : IPlayer
    {
        private const int MaxNameLength = 10;
        private string name;
        private int score;

        public Player(string name, int score)
        {
            this.Name = name;
            this.Score = score;
        }

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