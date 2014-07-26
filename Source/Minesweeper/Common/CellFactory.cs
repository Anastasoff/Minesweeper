namespace Minesweeper.Common
{
    using System;
    using GameObjects;

    /// <summary>
    /// abstract class that is responsible for creating the different cell types -> Implementation of Abstract Factory Pattern
    /// </summary>
    public abstract class CellFactory
    {
        /// <summary>
        /// creates an instance of the class MineCell
        /// </summary>
        /// <param name="row">takes integer parameter for the row of the cell</param>
        /// <param name="col">takes integer parameter for the col of the cell</param>
        /// <returns>returns an instance of the MineCell class</returns>
        public abstract Cell CreateMineCell(int row, int col);

        /// <summary>
        /// creates an instance of the class SafeCell
        /// </summary>
        /// <param name="row">takes integer parameter for the row of the cell</param>
        /// <param name="col">takes integer parameter for the col of the cell</param>
        /// <returns>returns an instance of the SafeCell class</returns>
        public abstract Cell CreateSafeCell(int row, int col);
    }
}
