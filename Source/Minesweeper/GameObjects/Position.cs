namespace Minesweeper.GameObjects
{
    public struct Position
    {
        public int Row;
        public int Col;

        public Position(int inputRow, int inputCol)
        {
            this.Row = inputRow;
            this.Col = inputCol;
        }

        public bool IsEqual(Position other)
        {
            return (this.Row == other.Row) && (this.Col == other.Col);
        }
    }
}