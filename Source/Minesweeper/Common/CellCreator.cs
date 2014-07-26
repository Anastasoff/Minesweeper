namespace Minesweeper.Common
{
    using System;
    using GameObjects;
    using Interfaces;

    public class CellCreator : CellFactory
    {
        public override IGameObject CreateMineCell(int row, int col)
        {
            var mineCell = new MineCell(row, col);

            return mineCell;
        }

        public override IGameObject CreateSafeCell(int row, int col)
        {
            var safeCell = new SafeCell(row, col);

            return safeCell;
        }
    }
}