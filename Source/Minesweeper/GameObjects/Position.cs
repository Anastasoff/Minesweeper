namespace Minesweeper.GameObjects
{
    public struct Position
    {
        public int row;
        public int col;

        public Position(int inputRow, int inputCol)
        {
            this.row = inputRow;
            this.col = inputCol;
        }

        public bool IsEqual(Position other)
        {
            return (this.row == other.row) && (this.col == other.col);
        }
    }
}