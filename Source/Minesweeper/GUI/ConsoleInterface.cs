namespace Minesweeper.GUI
{
    using System;
    using GameObjects;
    using Interfaces;
    using GUI.ConsoleSkins;

    public class ConsoleInterface : IOInterface
    {
        private const char DEFAULT_FLAG_SYMBOL = 'F';
        private const char DEFAULT_MINE_CELL_SYMBOL = '*';
        private const char DEFAULT_SAFE_CELL_SYMBOL = '-';
        private const char DEFAULT_UNREVEALED_CELL_SYMBOL = '?';
        private IInputDevice inputDevice = new KeyboardInput();
        private IConsoleSkin skin;

        public ConsoleInterface()
        {
            // Sets a default skin
            this.skin = new AllWhiteSkin();
        }

        public ConsoleInterface(IConsoleSkin skin)
        {
            this.skin = skin;
        }


        public void ChangeInput(IInputDevice device)
        {
            this.inputDevice = device;
        }

        public string GetUserInput(string message)
        {
            Console.Write(message);
            var input = inputDevice.GetInput();
            switch (input)
            {
                case "keyboard":
                    inputDevice = null;
                    inputDevice = new KeyboardInput();
                    Console.WriteLine("Switching to keyboard input");
                    input = "system";
                    break;
                case "voice":
                    inputDevice = null;
                    inputDevice = new SpeechInput();
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

        public void ShowWelcomeScreen()
        {
            string welcomeMessage = "Welcome to the game “Minesweeper”. Try to reveal all cells without mines.";
            Console.WriteLine(welcomeMessage);
            string instructionMessage = "Use 'top' to view the scoreboard, 'restart' to start a new game and 'exit' to quit the game.";
            Console.WriteLine(instructionMessage);
            Console.WriteLine();
        }

        /// <summary>
        /// Clears the console.
        /// </summary>
        public void ClearScreen()
        {
            Console.Clear();
        }

        /// <summary>
        /// Draws the game board on the console.
        /// </summary>
        /// <param name="board">The game board to be drawn.</param>
        public void DrawBoard(IGameObject[,] board)
        {
            SetConsole();

            int cols = board.GetLength(1);

            // print first row
            PrintIndentationOnTheLeft();
            PrintFieldsNumberOfColumns(cols);

            // print second row
            PrintIndentationOnTheLeft();
            PrintFieldTopAndBottomBorder(cols);

            PrintGameField(board);

            // print last row
            PrintIndentationOnTheLeft();
            PrintFieldTopAndBottomBorder(cols);
        }

        private void SetConsole()
        {
            ClearScreen();
            ShowWelcomeScreen();
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
                    var symbolToPrint = GetCellSymbol(currentCell);
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
                    return GetRegularAndMineCellsSymbol(currentCell);
                case CellTypes.Mine:
                    return GetRegularAndMineCellsSymbol(currentCell);
                case CellTypes.Flag:
                    return DEFAULT_FLAG_SYMBOL;
                case CellTypes.Unrevealed_Regular_Cell:
                    return DEFAULT_SAFE_CELL_SYMBOL;
                default:
                    return new Char();
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