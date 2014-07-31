namespace Minesweeper.Engine
{
    using GameObjects;

    public class Command
    {
        /// <summary>
        /// Creates an instance of the Command type
        /// </summary>
        /// <param name="commandType">The type of the command</param>
        public Command(CommandType commandType)
            : this(commandType, new Position(0, 0))
        {
        }

        /// <summary>
        /// Creates an instance of the Command type
        /// </summary>
        /// <param name="commandType">The type of the command</param>
        /// <param name="coordinates">Position in the 2D plane</param>
        public Command(CommandType commandType, Position coordinates)
        {
            this.CommandType = commandType;
            this.Coordinates = coordinates;
        }

        /// <summary>
        /// Gets the type of the command
        /// </summary>
        public CommandType CommandType { get; private set; }

        /// <summary>
        /// Gets the coordinates of the command
        /// </summary>
        public Position Coordinates { get; private set; }
    }
}