namespace Minesweeper
{
    using System;

    public class Player
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
                if (value.Length <= MaxNameLength)
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        this.name = "Unnamed player";
                    }
                    else
                    {
                        this.name = value;
                    }
                }
                else
                {
                    string message = string.Format("Name must be no longer than {0} characters.", MaxNameLength);
                    throw new ArgumentOutOfRangeException(message);
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