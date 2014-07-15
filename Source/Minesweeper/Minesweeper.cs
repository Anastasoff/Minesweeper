namespace Minesweeper
{
    using System;
    using Engine;

    public class Minesweeper
    {
        public static void Main()
        {
            Scoreboard scoreboard = Scoreboard.GetTop5;
            GameBoard board = GameBoard.GetBoard; // calling singleton
            GameEngine engine = new GameEngine(board, scoreboard);
            engine.Play();
        }
    }
}