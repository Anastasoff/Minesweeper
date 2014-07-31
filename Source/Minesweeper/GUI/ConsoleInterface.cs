namespace Minesweeper.GUI
{
    using System;
    using GameObjects;
    using GUI.ConsoleSkins;
    using Interfaces;

    /// <summary>
    /// Handles input/output to the console
    /// </summary>
    public class ConsoleInterface : IOInterface
    {
        private const char DEFAULT_FLAG_SYMBOL = 'F';
        private const char DEFAULT_MINE_CELL_SYMBOL = '*';
        private const char DEFAULT_SAFE_CELL_SYMBOL = '-';
        private const char DEFAULT_UNREVEALED_CELL_SYMBOL = '?';
        private IInputDevice inputDevice = new KeyboardInput();
        private IConsoleSkin skin;

        /// <summary>
        /// Constructor for the ConsoleInterface class
        /// </summary>
        public ConsoleInterface() : this(new AllWhiteSkin())
        {
        }

        /// <summary>
        /// Constructor for the ConsoleInterface class
        /// </summary>
        /// <param name="skin">The skin to be applied to the console ouput</param>
        public ConsoleInterface(IConsoleSkin skin)
        {
            this.skin = skin;
        }

        /// <summary>
        /// Sets a new input device to the current instance
        /// </summary>
        /// <param name="device">The new input device</param>
        public void ChangeInput(IInputDevice device)
        {
            this.inputDevice = device;
        }

        /// <summary>
        /// Gets the input of the user from the current input device
        /// </summary>
        /// <param name="message">The input</param>
        /// <returns>String with the correct value</returns>
        public string GetUserInput(string message)
        {
            Console.Write(message);
            var input = this.inputDevice.GetInput();
            switch (input)
            {
                case "keyboard":
                    this.inputDevice = null;
                    this.inputDevice = new KeyboardInput();
                    Console.WriteLine("Switching to keyboard input");
                    input = "system";
                    break;
                case "voice":
                    this.inputDevice = null;
                    this.inputDevice = new SpeechInput();
                    Console.WriteLine("Switching to voice command");
                    input = "system";
                    break;
                case "hiscore":
                    input = "top";
                    break;
                default:
                    break;
            }

            return input.Trim();
        }

        /// <summary>
        /// Prints a message to the console or an empty line if none.
        /// </summary>
        /// <param name="message">The message to be printed.</param>
        public void ShowMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(message);
            }
        }

        /// <summary>
        /// Prints the welcome screen on the console
        /// </summary>
        public void ShowWelcomeScreen()
        {
            string welcomeMessage = "Welcome to the game “Minesweeper”. Try to reveal all cells without mines.";
            Console.WriteLine(welcomeMessage);
            string instructionMessage = "Use 'top' to view the scoreboard, 'restart' to start a new game and 'exit' to quit the game.";
            Console.WriteLine(instructionMessage);
            Console.WriteLine();
        }

        /// <summary>
        /// Draws the game board on the console.
        /// </summary>
        /// <param name="board">The game board to be drawn.</param>
        public void DrawBoard(IGameObject[,] board)
        {
            this.SetConsole();

            int cols = board.GetLength(1);

            // print first row
            this.PrintIndentationOnTheLeft();
            this.PrintFieldsNumberOfColumns(cols);

            // print second row
            this.PrintIndentationOnTheLeft();
            this.PrintFieldTopAndBottomBorder(cols);

            this.PrintGameField(board);

            // print last row
            this.PrintIndentationOnTheLeft();
            this.PrintFieldTopAndBottomBorder(cols);
        }

        /// <summary>
        /// Clears the console.
        /// </summary>
        private void ClearScreen()
        {
            Console.Clear();
        }

        private void SetConsole()
        {
            this.ClearScreen();
            this.ShowWelcomeScreen();
        }

        private void PrintIndentationOnTheLeft()
        {
            Console.Write(new string(' ', 4));
        }

        private void PrintFieldTopAndBottomBorder(int cols)
        {
            Console.WriteLine(new string('-', 2 * cols));
        }

        private void PrintFieldsNumberOfColumns(int cols)
        {
            for (int i = 0; i < cols; i++)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();
        }

        private void PrintGameField(IGameObject[,] board)
        {
            int rows = board.GetLength(0);
            int cols = board.GetLength(1);
            for (int row = 0; row < rows; row++)
            {
                Console.Write(row + " | ");
                for (int col = 0; col < cols; col++)
                {
                    var currentCell = board[row, col];
                    var symbolToPrint = this.GetCellSymbol(currentCell);
                    if (this.skin.ColorScheme.ContainsKey(symbolToPrint) &&
                        currentCell.IsCellRevealed)
                    {
                        Console.ForegroundColor = this.skin.ColorScheme[symbolToPrint];
                    }

                    Console.Write(symbolToPrint + " ");
                    Console.ResetColor();
                }

                Console.WriteLine("|");
            }
        }

        private char GetCellSymbol(IGameObject currentCell)
        {
            var cellType = currentCell.Type;

            switch (cellType)
            {
                case CellTypes.Safe:
                    return this.GetRegularAndMineCellsSymbol(currentCell);
                case CellTypes.Mine:
                    return this.GetRegularAndMineCellsSymbol(currentCell);
                case CellTypes.Flag:
                    return DEFAULT_FLAG_SYMBOL;
                case CellTypes.Unrevealed_Regular_Cell:
                    return DEFAULT_SAFE_CELL_SYMBOL;
                default:
                    return new char();
            }
        }

        private char GetRegularAndMineCellsSymbol(IGameObject currentCell)
        {
            char cellSymbol;
            if (currentCell.Type == CellTypes.Mine)
            {
                cellSymbol = DEFAULT_MINE_CELL_SYMBOL;
            }
            else
            {
                var cell = currentCell as SafeCell;
                var numberOfNeighbouringMinesToStr = cell.NumberOfNeighbouringMines.ToString();
                cellSymbol = Convert.ToChar(numberOfNeighbouringMinesToStr);
            }

            if (currentCell.IsCellRevealed)
            {
                return cellSymbol;
            }
            else
            {
                return DEFAULT_UNREVEALED_CELL_SYMBOL;
            }
        }
    }
}