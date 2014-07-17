namespace Minesweeper.GameObjects
{
    using Interfaces;
    using System;

    public class Flag : IGameObject
    {
        private const char defaultSymbol = 'F';

        private char symbol;
        private Position coordinates;

        public Flag(int row, int col)
        {
            this.Symbol = defaultSymbol;
            this.Coordinates = new Position(row, col);
        }

        public char Symbol
        {
            get { return this.symbol; }

            private set
            {
                this.symbol = value;
            }
        }

        public Position Coordinates
        {
            get { return this.coordinates; }

            private set
            {
                this.coordinates = value;
            }
        }
    }
}
