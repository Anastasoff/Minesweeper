namespace Minesweeper
{
    using System;

    internal class Person
    {
        private string name;
        private int score;

        public Person(string name, int score)
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
                if (String.IsNullOrWhiteSpace(value))
                {
                    this.name = "Unnamed player";
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
                    throw new ArgumentOutOfRangeException("Score must be non-negative");
                }
                this.score = value;
            }
        }
    }
}