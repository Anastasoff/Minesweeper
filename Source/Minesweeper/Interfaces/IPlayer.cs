namespace Minesweeper.Interfaces
{
    /// <summary>
    /// Defines the properties of a player
    /// </summary>
    public interface IPlayer
    {
        string Name { get; }

        int Score { get; }
    }
}