namespace Minesweeper.Engine
{
    using GameObjects;

    public class Command
    {
        private CommandType commandType;
        private Position coordinates;

        public Command(CommandType commandType)
            : this(commandType, new Position(0, 0))
        {
        }

        public Command(CommandType commandType, Position coordinates)
        {
            this.commandType = commandType;
            this.coordinates = coordinates;
        }
    }
}