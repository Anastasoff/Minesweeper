﻿namespace Minesweeper.Common
{
    using System;
    using GameObjects;
    using Interfaces;

    /// <summary>
    /// abstract class that is responsible for creating the different cell types -> Implementation of Abstract Factory Pattern
    /// </summary>
    public class CellCreator : CellFactory
    {
        /// <summary>
        /// creates an instance of the class MineCell
        /// </summary>
        /// <param name="row">takes integer parameter for the row of the cell</param>
        /// <param name="col">takes integer parameter for the col of the cell</param>
        /// <returns>returns an instance of the MineCell class</returns>
        public override Cell CreateMineCell(int row, int col)
        {
            var mineCell = new MineCell(row, col);

            return mineCell;
        }

        /// <summary>
        /// creates an instance of the class SafeCell
        /// </summary>
        /// <param name="row">takes integer parameter for the row of the cell</param>
        /// <param name="col">takes integer parameter for the col of the cell</param>
        /// <returns>returns an instance of the SafeCell class</returns>
        public override Cell CreateSafeCell(int row, int col)
        {
            var safeCell = new SafeCell(row, col);

            return safeCell;
        }
    }
}