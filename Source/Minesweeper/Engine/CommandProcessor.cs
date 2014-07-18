namespace Minesweeper.Engine
 {
     using GUI;
     using System;
     using System.Linq;
     using System.Collections.Generic;
     using Interfaces;

     public class CommandProcessor
     {
         Dictionary<string, Command> commands = new Dictionary<string, Command>();
         private GameBoard gameBoard;
         private Scoreboard scoreBoard;
         private IOInterface userIteractor;

         public CommandProcessor(GameBoard board, Scoreboard score, IOInterface userIteractor)
         {
             this.GameBoard = board;
             this.Score = score;
             this.userIteractor = userIteractor;

             this.commands.Add("exit", Command.Exit);
             this.commands.Add("top", Command.Top);
             this.commands.Add("restart", Command.Restart);
             this.commands.Add("flag", Command.Flag);

         }

         public GameBoard GameBoard
         {
             get
             {
                 return this.gameBoard;
             }

             private set
             {
                 this.gameBoard = value;
             }
         }

         public Scoreboard Score
         {
             get
             {
                 return this.scoreBoard;
             }

             private set
             {
                 this.scoreBoard = value;
             }
         }

         private bool CheckIfValidInputCommand(string inputCommand)
         {
             return commands.ContainsKey(inputCommand.ToLower());
         }

         private string[] ConvertInputStringToStringArray(string input)
         {
             return input.Split(' ');
         }

         private Command ExtractCommand(string[] inputCommands)
         {
             Command command = Command.ValidMove;

             if (inputCommands.Length == 1 || inputCommands.Length == 3)
             {
                 string inputCommand = inputCommands[0];
                 if (CheckIfValidInputCommand(inputCommand))
                 {
                     command = GetCommandFromInput(inputCommand);
                 }
                 else
                 {
                     command = Command.InvalidInput;
                 }
             }

             return command;
         }

         public void ExecuteCommand(string input)
         {
             string[] commandsArr = ConvertInputStringToStringArray(input);
             Command command = ExtractCommand(commandsArr);
             //    try
             //    {
             //        command = ParseInputString(input); //GetCommandFromInput(input);
             //    }
             //    catch (ArgumentException)
             //    {
             //        command = Command.ValidMove;
             //    }
             switch (command)
             {
                 case Command.InvalidMove:
                     userIteractor.ShowMessage("Illegal command!");
                     break;
                 case Command.Exit:
                     userIteractor.ShowMessage("Goodbye!");
                     Environment.Exit(0);
                     break;
                 case Command.Top: ProcessTopCommand();
                     break;
                 case Command.Restart: ProcessRestartCommand();
                     break;
                 case Command.Flag: ProcessFlagCommand(commandsArr);
                     break;
                 case Command.InvalidInput:
                     userIteractor.ShowMessage("Invalid input! Please try again!");
                     break;
                 default: ProcessCoordinates(commandsArr);
                     break;
             }
         }

         private Command GetCommandFromInput(string input)
         {
             if (!commands.ContainsKey(input))
             {
                 throw new ArgumentException("Input not a valid command");
             }
             else
             {
                 return commands[input];
             }
         }

         private void ProcessFlagCommand(string[] commandsArr)
         {
             string[] coordsArr = commandsArr.Skip(1).ToArray();
             int[] coordinates = ParseInputCoordinates(coordsArr);
             int row = coordinates[0];
             int col = coordinates[1];

             if (CheckIfValidCoordinates(row, col))
             {
                 userIteractor.ShowMessage("Illegal move!");
                 userIteractor.ShowMessage();
             }
             else
             {
                 gameBoard.PlaceFlag(row, col);
                 userIteractor.DrawBoard(gameBoard.Board);
             }
         }

         //private void ProcessInvalidInputCommand()
         //{
         //    Console.WriteLine("Invalid input! Please try again!");
         //}

         //private void ProcessInvalidMove()
         //{
         //    Console.WriteLine("Illegal command!");
         //}

         private void ProcessTopCommand()
         {
             scoreBoard.ShowHighScores();
         }

         //private void ProcessExitCommand()
         //{
         //    Console.WriteLine("Goodbye!");
         //    Environment.Exit(0);
         //}

         private void ProcessRestartCommand()
         {
             gameBoard.ResetBoard();
             userIteractor.ShowWelcomeScreen();
             userIteractor.DrawBoard(gameBoard.Board);
        }

         private int[] ParseInputCoordinates(string[] inputCoordinates)
         {
             int[] coordinates = new int[2];
             if (inputCoordinates.Length != 2)
             {
                 throw new ArgumentException("Invalid coordinates.");
             }
             else
             {
                 try
                 {
                     coordinates[0] = Convert.ToInt32(inputCoordinates[0]);
                     coordinates[1] = Convert.ToInt32(inputCoordinates[1]);
                 }
                 catch (FormatException)
                 {
                     throw new ArgumentException("Invalid format.");
                 }
             }

             return coordinates;
         }

         private bool CheckIfValidCoordinates(int row, int col)
         {
             // bullshit
             //bool isInsideBoard = !gameboard.IsInsideBoard(row, col);
             //bool isCellRevealed = gameboard.IsCellRevealed(row, col);
             //return isInsideBoard || isCellRevealed;

             // mother of all bullshits
             //if (!gameboard.IsInsideBoard(row, col)) 
             //{
             //    return false;
             //}
             //if (gameboard.IsCellRevealed(row, col))
             //{
             //    return false;                
             //}
             //return true;

             return !gameBoard.IsInsideBoard(row, col) || gameBoard.IsCellRevealed(row, col);
         }

         private void ProcessCoordinates(string[] inputCoordinates)
         {
             var coords = ParseInputCoordinates(inputCoordinates);
             int row = coords[0];
             int col = coords[1];

             if (CheckIfValidCoordinates(row, col))
             {
                 Console.WriteLine("Illegal move!"); // to be put in the Renderer
                 Console.WriteLine();
             }
             else
             {
                 if (gameBoard.CheckIfHasMine(row, col))
                 {
                     ShowEndGameMessage(gameBoard, scoreBoard);
                     scoreBoard.ShowHighScores();
                     gameBoard.ResetBoard(); // should call on the ClearBoard() from the RenderingEngine
                     userIteractor.ShowWelcomeScreen();
                 }
                 else
                 {
                     gameBoard.RevealBlock(row, col);
                 }
                 userIteractor.DrawBoard(gameBoard.Board);
             }

             if (gameBoard.CheckIfGameIsWon())
             {
                 ShowGameWonMessage(gameBoard, scoreBoard);
             }
         }

         public void ShowEndGameMessage(GameBoard board, Scoreboard scoreboard) // this parameters may not be needed
         {
             board.RevealWholeBoard();
             userIteractor.DrawBoard(gameBoard.Board);

             Console.WriteLine("Booooom! You were killed by a mine. You revealed " + board.RevealedCellsCount + " cells without mines.");
             Console.WriteLine();

             if (board.RevealedCellsCount > scoreboard.MinInTop5() || scoreboard.Count() < 5)
             {
                 scoreboard.AddPlayer(board.RevealedCellsCount);
             }
         }

         public void ShowGameWonMessage(GameBoard board, Scoreboard scoreboard) // this parameters may not be needed
         {
             board.RevealWholeBoard();
             userIteractor.DrawBoard(gameBoard.Board);

             Console.WriteLine("Congratulations! You have escaped all the mines and WON the game!");
             Console.WriteLine();

             scoreboard.AddPlayer(board.RevealedCellsCount);
         }
     }
 }