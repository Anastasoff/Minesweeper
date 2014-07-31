namespace Minesweeper.Common
{
    using System;
    using GameObjects;

    /// <summary>
    /// Abstract class that is responsible for creating the different cell types -> Implementation of Abstract Factory Pattern
    /// </summary>
    public abstract class CellFactory
    {
        /// <summary>
        /// Creates an instance of the class MineCell
        /// </summary>
        /// <param name="row">An integer for the row of the cell</param>
        /// <param name="col">An integer parameter for the col of the cell</param>
        /// <returns>An instance of the MineCell class</returns>
        public abstract Cell CreateMineCell(Position pos);

        /// <summary>
        /// Creates an instance of the class SafeCell
        /// </summary>
        /// <param name="row">An integer for the row of the cell</param>
        /// <param name="col">An integer for the col of the cell</param>
        /// <returns>An instance of the SafeCell class</returns>
        public abstract Cell CreateSafeCell(Position pos);
    }
}
