namespace Minesweeper.Engine
{
    using System;
    
    /// <summary>
    /// Contains the partial logic behind the Memento design pattern
    /// </summary>
    internal class GameBoardMemory
    {
        /// <summary>
        /// Gets or sets the value of the Memento instance
        /// </summary>
        public Memento Memento { get; set; }
    }
}