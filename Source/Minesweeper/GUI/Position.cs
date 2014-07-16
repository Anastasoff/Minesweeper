namespace Minesweeper.GUI
{
    using System;

    public struct Position
    {
        public int row;
        public int col;

        public Position(int inputRow, int inputCol)
        {
            this.row = inputRow;
            this.col = inputCol;
        }
    }
}
