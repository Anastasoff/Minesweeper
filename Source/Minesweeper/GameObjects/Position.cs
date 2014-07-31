namespace Minesweeper.GameObjects
{
    /// <summary>
    /// Holds a pair of values that represent coordinates in a two-dimensional plane
    /// </summary>
    public struct Position
    {
        public int Row;
        public int Col;

        /// <summary>
        /// Creates an instance of the Position structure
        /// </summary>
        /// <param name="inputRow">The X coordinate</param>
        /// <param name="inputCol">The Y coordinate</param>
        public Position(int inputRow, int inputCol)
        {
            this.Row = inputRow;
            this.Col = inputCol;
        }
    }
}