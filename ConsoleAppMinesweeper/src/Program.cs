using ConsoleAppMinesweeper.Interface;

namespace ConsoleAppMinesweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            IConsoleWrapper consoleWrapper = new ConsoleWrapper();
            ITextManager textManager = new TextManager(); // Create an instance of TextManager
            IUserActionsController userActionsController = new UserActionsController(textManager, consoleWrapper);
            IBoardManager boardManager = new BoardManager(textManager,consoleWrapper,userActionsController);
            GameManager gameManager;
            bool isGameOver = false;

            while (!isGameOver)
            {
                gameManager = new GameManager(textManager, consoleWrapper, userActionsController, boardManager); // Pass both instances
                gameManager.InitializeGame();
                gameManager.Play();

                isGameOver = gameManager.IsAnotherGame();
            }
        }
    }

}