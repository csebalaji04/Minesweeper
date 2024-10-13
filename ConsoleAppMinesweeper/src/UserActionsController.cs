using ConsoleAppMinesweeper.Interface;
using System;

namespace ConsoleAppMinesweeper
{
    public class UserActionsController : IUserActionsController
    {
        private readonly ITextManager _textManager; // Instance variable for the text manager
        private readonly IConsoleWrapper _console; // Instance variable for the console manager

        public UserActionsController(ITextManager textManager,IConsoleWrapper console) // Constructor with dependency injection
        {
            _textManager = textManager;
            _console = console;
        }

        public int IsHidden { get;  set; }

        public int GetMinesNum()
        {
            int result;
            do
            {
                _console.Clear();
                _console.SetCursorPosition(11, 5);
                _console.WriteLine("Enter the number of mines you want in your minesweeper: ");
            } while (!int.TryParse(Console.ReadLine(), out result));
            return result;
        }

        public int GetValidLength(int limit, string scope)
        {
            int result;
            do
            {
                _console.Clear();
                _console.SetCursorPosition(12, 4);
                _console.WriteLine("The number you've entered isn't valid.");
                _console.SetCursorPosition(12, 6);
                _console.WriteLine($"Please enter a different number, which is {limit} or {scope}: ");
            } while (!int.TryParse(Console.ReadLine(), out result));

            return result;
        }

        public int GetColumn()
        {
            int colNum;
            do
            {
                _textManager.PrintUserDifficultyTitle(); // Use the instance variable
                _console.SetCursorPosition(29, 6);
                _console.WriteLine("Enter the board height: ");
            } while (!int.TryParse(_console.ReadLine(), out colNum));
            return colNum;
        }

        public int GetRow()
        {
            int rowNum;
            do
            {
                _textManager.PrintUserDifficultyTitle(); // Use the instance variable
                _console.SetCursorPosition(28, 6);
                _console.WriteLine("Enter the board width: ");
            } while (!int.TryParse(_console.ReadLine(), out rowNum));
            return rowNum;
        }

      

        /// <summary>
        /// The logic of the movement around the field and pressed playable buttons.
        /// </summary>
        /// <param name="gameBoard">The 2D Array the user is playing on.</param>
        /// <localVariable name="upAndDown">The number for the SetCourserPosition. - Top </localVariable>
        /// <localVariable name="sidesCount">Tracking on which cell the user is currently on. - Rows</localVariable>
        /// <localVariable name="upAndDownCount">Tracking on which cell the user is currently on. - Columns</localVariable>
        /// <localVariable name="tempSides">Saved the last number 'sides' used for the SetCourserPosition. - Left</localVariable>
        /// <localVariable name="tempUpAndDown">Saved the last number 'upAndDown' used for the SetCourserPosition. - Top</localVariable>
        /// <localVariable name="tempForSides">Saved the first number 'sides' used for the SetCourserPosition. - Left</localVariable>
        /// <localVariable name="tempForUpAndDown">Saved the first number 'upAndDown' used for the SetCourserPosition. - Top</localVariable>
        /// <localVariable name="redo">True / false if the player - won / lost - the game.</localVariable>
        public void MovingAlongTheArray(Cell[,] gameBoard, int minesCounter)
        
        {
            int upAndDown = 5, sidesCount = 0, upAndDownCount = 0, tempSides = CursorHandler.CursorOffSet + 2, tempUpAndDown = upAndDown + 1,
                tempForSides = CursorHandler.CursorOffSet + 2, tempForUpAndDown = upAndDown + 1;
            bool redo = false;

            ConsoleKeyInfo keyInfo;
            do
            {
                _console.SetCursorPosition(0, 0);
                _console.WriteLine("\t\t\t\t\t\t\t\n\t\t\t\t\t\t\t\n\t\t\t\t\t\t\t");
                _console.SetCursorPosition(CursorHandler.CursorOffSet, upAndDown);
                keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    //Moving the cursor 1 time to the left.
                    case ConsoleKey.LeftArrow:
                        CursorHandler.CursorOffSet -= 2;
                        sidesCount--;
                        //Checks if the user is staying on the field.
                        if (CursorHandler.CursorOffSet < 0)
                        {
                            CursorHandler.CursorOffSet += 2;
                            sidesCount++;
                            _console.SetCursorPosition(0, 0);
                            _console.WriteLine("Trying to run away huh?\nYou'll never get out from here.\nWell.. Unless you'll finish my Minesweeper.");
                            GameManager.SetThreadSleep(2500);
                            _console.SetCursorPosition(CursorHandler.CursorOffSet, upAndDown);
                            break;
                        }
                        _console.SetCursorPosition(CursorHandler.CursorOffSet, upAndDown);
                        break;

                    //Moving the cursor 1 time to the right.
                    case ConsoleKey.RightArrow:
                        CursorHandler.CursorOffSet += 2;
                        sidesCount++;
                        //Checks if the user is staying on the field.
                        if (CursorHandler.CursorOffSet > 79)
                        {
                            CursorHandler.CursorOffSet -= 2;
                            sidesCount--;
                            _console.SetCursorPosition(0, 0);
                            _console.WriteLine("Trying to run away huh?\nYou'll never get out from here.\nWell.. Unless you'll finish my Minesweeper.");
                            GameManager.SetThreadSleep(2500);
                            _console.SetCursorPosition(CursorHandler.CursorOffSet, upAndDown);
                            break;
                        }
                        _console.SetCursorPosition(CursorHandler.CursorOffSet, upAndDown);
                        break;

                    //Moving the cursor 1 time up.
                    case ConsoleKey.UpArrow:
                        upAndDown--;
                        upAndDownCount--;
                        //Checks if the user is staying on the field.
                        if (upAndDown < 0)
                        {
                            upAndDown++;
                            upAndDownCount++;
                            _console.SetCursorPosition(0, 0);
                            _console.WriteLine("Trying to run away huh?\nYou'll never get out from here.\nWell.. Unless you'll finish my Minesweeper.");
                            GameManager.SetThreadSleep(2500);
                            _console.SetCursorPosition(CursorHandler.CursorOffSet, upAndDown);
                            break;
                        }
                        _console.SetCursorPosition(CursorHandler.CursorOffSet, upAndDown);
                        break;

                    //Moving the cursor 1 time down.
                    case ConsoleKey.DownArrow:
                        upAndDown++;
                        upAndDownCount++;
                        //Checks if the user is staying on the field.
                        if (upAndDown > 50)
                        {
                            upAndDown--;
                            upAndDownCount--;
                            _console.SetCursorPosition(0, 47);
                            _console.WriteLine("There's sharks out there.. Trust me, I'm doing you a favor.\nNow go up there and finish my Minesweeper!");
                            _console.SetCursorPosition(CursorHandler.CursorOffSet, upAndDown);
                            break;
                        }
                        _console.SetCursorPosition(CursorHandler.CursorOffSet, upAndDown);
                        break;

                    case ConsoleKey.Enter:
                        _console.SetCursorPosition(0, 0);
                        _console.WriteLine("\t\t\t\t\t\t\t\n\t\t\t\t\t\t\t");
                        _console.SetCursorPosition(CursorHandler.CursorOffSet, upAndDown);

                        //Checks if the Enter was inside the field.
                        if (sidesCount >= 0 && upAndDownCount >= 0 && upAndDownCount < gameBoard.GetLength(0) && sidesCount < gameBoard.GetLength(1))
                        {

                            //Checks if the current location is hidden.
                            if (gameBoard[upAndDownCount, sidesCount].IsHidden)
                            {

                                //Checks if the current location contains exclamation mark.
                                if (gameBoard[upAndDownCount, sidesCount].IsMarked)
                                {
                                    _console.SetCursorPosition(0, 0);
                                    _console.WriteLine("To clear this exclamation mark press - Delete");
                                    GameManager.SetThreadSleep(2500);
                                    _console.SetCursorPosition(CursorHandler.CursorOffSet, upAndDown);
                                }

                                //Checks if the current location contains a mine.
                                else if (gameBoard[upAndDownCount, sidesCount].CellValue == StringUtilities.MINE_SYMBOL)
                                {
                                    
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    gameBoard[upAndDownCount, sidesCount].IsHidden = false;
                                    Console.Write(gameBoard[upAndDownCount, sidesCount].CellValue);
                                    gameBoard[upAndDownCount, sidesCount].CellValue = '0';
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    upAndDown = tempForUpAndDown;

                                    //Prints all the mines on the field. Then waits 2.5 seconds, clears everything and print "Game over".
                                    for (int i = 1; i < gameBoard.GetLength(0) - 1; i++)
                                    {
                                        CursorHandler.CursorOffSet = tempForSides;
                                        for (int j = 1; j < gameBoard.GetLength(1) - 1; j++)
                                        {
                                            if (gameBoard[i, j].CellValue == StringUtilities.MINE_SYMBOL)
                                            {
                                                _console.SetCursorPosition(CursorHandler.CursorOffSet, upAndDown);
                                                (gameBoard[i, j].IsHidden) = false;
                                                Console.Write(gameBoard[i, j].CellValue);
                                            }
                                            CursorHandler.CursorOffSet += 2;
                                        }
                                        upAndDown++;
                                    }
                                    _console.SetForegroundColor( ConsoleColor.Gray);
                                    GameManager.SetThreadSleep(2500);
                                    _console.Clear();
                                    _console.SetCursorPosition(31, 1);
                                    _console.WriteLine("Game over!");
                                    _console.SetCursorPosition(30, 2);
                                    _console.WriteLine("Tough luck..");
                                    _console.SetForegroundColor(ConsoleColor.White);
                                    redo = true;
                                }
                                else
                                {
                                    BoardManager.UnlockSlotsIfEmpty(gameBoard, upAndDown, tempSides = CursorHandler.CursorOffSet, tempUpAndDown = upAndDown, sidesCount, upAndDownCount);
                                    IsHidden = 0;

                                    //Counts the number of hidden values.
                                    for (int i = 1; i < gameBoard.GetLength(0) - 1; i++)
                                    {
                                        for (int j = 1; j < gameBoard.GetLength(1) - 1; j++)
                                        {
                                            if (gameBoard[i, j].IsHidden)
                                            {
                                                IsHidden++;
                                            }
                                        }
                                    }
                                    //Checks if the amount of hidden values is equal to the number of mines.
                                    //If it does, uses a nested loop to print all the mines locations as exclamation marks.
                                    if (IsHidden == minesCounter)
                                    {
                                        upAndDown = tempForUpAndDown;
                                        for (int i = 1; i < gameBoard.GetLength(0) - 1; i++)
                                        {
                                            CursorHandler.CursorOffSet = tempForSides;
                                            for (int j = 1; j < gameBoard.GetLength(1) - 1; j++)
                                            {
                                                if (gameBoard[i, j].CellValue == StringUtilities.MINE_SYMBOL)
                                                {
                                                    Console.SetCursorPosition(CursorHandler.CursorOffSet, upAndDown);
                                                    (gameBoard[i, j].IsMarked) = true;
                                                    _console.SetForegroundColor(ConsoleColor.Yellow);
                                                    Console.Write(gameBoard[i, j].CellValue = StringUtilities.MARK_SYMBOL);
                                                    _console.SetForegroundColor(ConsoleColor.White);
                                                }
                                                CursorHandler.CursorOffSet += 2;
                                            }
                                            upAndDown++;
                                        }
                                        _console.SetForegroundColor(ConsoleColor.Yellow);
                                        GameManager.SetThreadSleep(2500);
                                        _console.Clear();
                                        _console.SetCursorPosition(28, 1);
                                        _console.WriteLine("Congratulations!");
                                        _console.SetCursorPosition(31, 2);
                                        _console.WriteLine("You've won!");
                                        _console.SetForegroundColor(ConsoleColor.White);
                                        redo = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            _console.SetCursorPosition(0, 0);
                            _console.WriteLine("What are you trying to click on?\nYou totally missed the board..");
                            GameManager.SetThreadSleep(2500);
                            _console.SetCursorPosition(CursorHandler.CursorOffSet, upAndDown);
                        }
                        break;

                    case ConsoleKey.Insert:

                        //Checks if the Insert was inside the field.
                        if (sidesCount >= 0 && upAndDownCount >= 0 && upAndDownCount < gameBoard.GetLength(0) && sidesCount < gameBoard.GetLength(1))
                        {
                            //Checks if the current location isn't hidden or marked by exclamation mark.
                            if (!gameBoard[upAndDownCount, sidesCount].IsHidden || gameBoard[upAndDownCount, sidesCount].IsMarked)
                            {
                                break;
                            }
                            gameBoard[upAndDownCount, sidesCount].IsMarked = true;
                            _console.SetForegroundColor(ConsoleColor.Yellow);
                            Console.Write(gameBoard[upAndDownCount, sidesCount].MarkValue = StringUtilities.MARK_SYMBOL);
                            _console.SetForegroundColor (ConsoleColor.White);
                        }
                        else
                        {
                            Console.SetCursorPosition(0, 0);
                            Console.Write("Trying to redecorate my Minesweeper I see..\nMaybe it's better to focus on finishing it.");
                            GameManager.SetThreadSleep(2500);
                            Console.SetCursorPosition(CursorHandler.CursorOffSet, upAndDown);
                        }
                        break;

                    case ConsoleKey.Delete:

                        //Checks if the Delete was inside the field.
                        if (sidesCount >= 0 && upAndDownCount >= 0 && upAndDownCount < gameBoard.GetLength(0) && sidesCount < gameBoard.GetLength(1))
                        {
                            //Checks if the current location is marked by exclamation mark.
                            if (gameBoard[upAndDownCount, sidesCount].IsMarked)
                            {
                                gameBoard[upAndDownCount, sidesCount].IsMarked = false;
                                Console.Write(gameBoard[upAndDownCount, sidesCount].MarkValue = '\0');
                            }
                        }
                        else
                        {
                            _console.SetCursorPosition(0, 0);
                            _console.WriteLine("There is nothing to delete out there..\nMaybe it's better to focus on the Minesweeper.");
                            GameManager.SetThreadSleep(2500);
                            _console.SetCursorPosition(CursorHandler.CursorOffSet, upAndDown);
                        }
                        break;
                }
            } while (!redo);
        }
    }
}
