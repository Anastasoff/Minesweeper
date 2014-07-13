namespace Minesweeper
{
    using System;
    
    internal class Minesweeper
    {
        private static void Main()
        {
            Scoreboard scoreboard = Scoreboard.GetTop5;
            GameBoard board = GameBoard.GetBoard; // calling singleton
            GameEngine engine = new GameEngine(board, scoreboard);
            engine.Play();
        }
    }
}