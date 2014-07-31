namespace Minesweeper.Engine
{
    using System;
    using GameObjects;
    
    /// <summary>
    /// Contains partial logic of the Memento design pattern
    /// </summary>
    public class Memento
    {
        /// <summary>
        /// Creates an instacne of the Memento class
        /// </summary>
        /// <param name="currentBoard">The cells array to be stored</param>
        public Memento(Cell[,] currentBoard)
        {
            this.CurrentBoard = currentBoard;
        }

        public Cell[,] CurrentBoard { get; set; }
    }
}