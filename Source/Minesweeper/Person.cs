namespace Minesweeper
{
    using System;

    internal class Person
    {
        private const int MAX_NAME_LENGTH = 10;
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
                if (value.Length <= MAX_NAME_LENGTH)
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
                else
                {
                    string message = string.Format("Name must be no longer than {0} cahracters.",
                                                    MAX_NAME_LENGTH);
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
                    throw new ArgumentOutOfRangeException("Score must be non-negative");
                }

                this.score = value;
            }
        }
    }
}