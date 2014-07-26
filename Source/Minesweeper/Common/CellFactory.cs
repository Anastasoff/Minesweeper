namespace Minesweeper.Common
{
    using System;
    using GameObjects;

    public abstract class CellFactory
    {
        public abstract Cell CreateMineCell(int row, int col);

        public abstract Cell CreateSafeCell(int row, int col);
    }
}
