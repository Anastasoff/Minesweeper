namespace Minesweeper.Engine
{
    using System;
    using GameObjects;
    
    public class Memento
    {
        public Memento(Cell[,] currentBoard)
        {
            this.CurrentBoard = currentBoard;
            //this.revealedCellsCount = revealedCellsCount;
        }

        public Cell[,] CurrentBoard { get; set; }
        //public int revealedCellsCount { get; set; }
    }
}