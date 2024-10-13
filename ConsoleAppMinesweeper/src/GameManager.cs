using ConsoleAppMinesweeper.Interface;
using System;
using System.Threading;

namespace ConsoleAppMinesweeper
{
    public class GameManager
    {
        private readonly ITextManager _textManager;
        private readonly IBoardManager _board;
        private readonly IConsoleWrapper _console;
        private IUserActionsController _userActionsController;

        public GameManager(ITextManager textManager, IConsoleWrapper console, IUserActionsController userActionsController, IBoardManager board)
        {
            _textManager = textManager;
            _console = console;
            _userActionsController = userActionsController;
            _board = board;
        }

        public static void SetThreadSleep(int amountInMS) => Thread.Sleep(amountInMS);

        public void InitializeGame()
        {
            InitializeInstructions();
            DifficultyHandler difficulty = new DifficultyHandler();
            _textManager.PrintUserDifficultyTitle();
            InitializeBoard();
            LoadingScreen.Load(_console);
            InitializeCursor();

            _console.Clear();
            _board.Print();
        }

        public void Play()
        {
            UserActionsController userActionsController = new UserActionsController(_textManager, _console);
            userActionsController.MovingAlongTheArray(_board.Game2DArray, _board.MinesCount);
        }

        private void InitializeInstructions()
        {
            try
            {
                bool isApproved = false;

                while (!isApproved)
                {
                    _textManager.PrintGameInstructions();
                    var value = _console.ReadLine();
                    if (value != null)
                    {
                        string userInput = value.ToLower();

                        switch (userInput)
                        {
                            case "ok":
                            case "k":
                                isApproved = true;
                                break;

                            default:
                                _textManager.ClearUserInputGameInstructions();
                                break;
                        }
                    }
                    else
                    {
                        isApproved = true;
                    }
                }
            }catch (Exception ex)
            {

            }
           
        }

        private void InitializeBoard()
        {
            //_board = new BoardManager();
            _board.SetBoard();
        }

        private void InitializeCursor()
        {
            CursorHandler cursorHandler = new CursorHandler();
            cursorHandler.GetCursorOffSet(_board.Game2DArray, DifficultyHandler.UserDifficulty);
        }

        public bool IsAnotherGame()
        {
            bool isUserAnswer = false;
            bool result = false;
            _textManager.PrintWhetherRestartGame();

            while (!isUserAnswer)
            {
                _console.SetCursorPosition(35, 9);
                _console.WriteLine("\t");
                _console.SetCursorPosition(35, 9);

                switch (_console.ReadLine().ToLower())
                {
                    case "yes":
                    case "y":
                        isUserAnswer = true;
                        _console.Clear();
                        break;

                    case "no":
                    case "n":
                        isUserAnswer = true;
                        result = true;
                        _console.SetCursorPosition(23, 10);
                        _console.WriteLine("Thanks for playing. Goodbye.");
                        Thread.Sleep(2000);
                        break;

                    default:
                         _textManager.ClearUserInputRestartGame();
                        break;
                }
            }

            _console.SetForegroundColor(ConsoleColor.White);
            return result;
        }
    }
}
