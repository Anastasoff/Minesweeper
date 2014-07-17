namespace Minesweeper.Interfaces
{
    public interface IGameBoard
    {
        int RevealedCellsCount { get; }

        void InitializeBoardForDisplay();

        void AllocateMines(Random generator);
    }
}
