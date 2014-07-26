namespace Minesweeper.Common
{
    using System;
    using Interfaces;

    public abstract class CellFactory
    {
        public abstract IGameObject CreateMineCell(int row, int col);

        public abstract IGameObject CreateSafeCell(int row, int col);
    }
}
