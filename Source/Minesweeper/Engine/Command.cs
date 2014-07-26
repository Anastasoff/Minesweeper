namespace Minesweeper.Engine
{
    using GameObjects;

    public class Command
    {
        public Command(CommandType commandType)
            : this(commandType, new Position(0, 0))
        {
        }

        public Command(CommandType commandType, Position coordinates)
        {
            this.CommandType = commandType;
            this.Coordinates = coordinates;
        }

        public CommandType CommandType { get; private set; }

        public Position Coordinates { get; private set; }
    }
}