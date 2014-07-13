namespace Minesweeper
{
    using System;

    // Аз съм българче but everrything in code (icluding comments) must be in english.

    internal class Програма
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