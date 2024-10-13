using ConsoleAppMinesweeper.Interface;
using ConsoleAppMinesweeper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MinesweeperTest
{
    public class GameManagerTests
    {
        private readonly Mock<ITextManager> _mockTextManager;
        private readonly Mock<IBoardManager> _mockBoardManager;
        private readonly Mock<IConsoleWrapper> _mockConsoleWrapper;
        private readonly Mock<IUserActionsController> _mockUserActionsController;
        private readonly GameManager _gameManager;

        public GameManagerTests()
        {
            _mockTextManager = new Mock<ITextManager>();
            _mockConsoleWrapper = new Mock<IConsoleWrapper>();
            _mockBoardManager = new Mock<IBoardManager>();
            _mockUserActionsController = new Mock<IUserActionsController>();
            _gameManager = new GameManager(_mockTextManager.Object, _mockConsoleWrapper.Object, _mockUserActionsController.Object,_mockBoardManager.Object);
        }

        [Fact]
        public void InitializeGame_Should_Call_PrintUserDifficultyTitle_And_SetBoard()
        {
            // Arrange
            var mockConsole = new ConsoleWrapper(); // You can create a mock or a real console instance if needed.
            DifficultyHandler.UserDifficulty = "Easy"; // Set the necessary state
            _mockConsoleWrapper.Setup(c => c.Clear());

           // ITextManager textManager = new TextManager(mockConsole);
            // Act
            _gameManager.InitializeGame();

            // Assert
            _mockTextManager.Verify(tm => tm.PrintUserDifficultyTitle(), Times.Once);
            _mockBoardManager.Verify(bm => bm.SetBoard(), Times.Once);
            _mockConsoleWrapper.Verify(c => c.Clear(), Times.Once); // Called once in InitializeGame and once after LoadingScreen
            _mockBoardManager.Verify(bm => bm.Print(), Times.Once);

        }

        [Theory]
        [InlineData("yes", false)]
        [InlineData("y", false)]
        [InlineData("no", true)]
        [InlineData("n", true)]
        public void IsAnotherGame_Should_Return_Expected_Result_Based_On_User_Input(string userInput, bool expectedResult)
        {
            // Arrange
            _mockConsoleWrapper.SetupSequence(c => c.ReadLine())
                .Returns(userInput); // Simulate the user input

            // Act
            var result = _gameManager.IsAnotherGame();

            // Assert
            Assert.Equal(expectedResult, result);
            _mockTextManager.Verify(tm => tm.PrintWhetherRestartGame(), Times.Once);
        }

        [Fact]
        public void InitializeInstructions_Should_Loop_Until_User_Enters_Valid_Input()
        {
            // Arrange
            _mockConsoleWrapper.SetupSequence(c => c.ReadLine())
                .Returns("invalid") // First attempt, invalid input
                .Returns("k"); // Second attempt, valid input

            // Act
            _gameManager.InitializeGame();

            // Assert
            _mockTextManager.Verify(tm => tm.PrintGameInstructions(), Times.Exactly(2));
            _mockTextManager.Verify(tm => tm.ClearUserInputGameInstructions(), Times.Once);
        }

        [Fact]
        public void IsAnotherGame_Should_Reset_Console_Correctly_When_User_Exits()
        {
            // Arrange
            _mockConsoleWrapper.SetupSequence(c => c.ReadLine())
                .Returns("no"); // User exits the game

            // Act
            var result = _gameManager.IsAnotherGame();

            // Assert
            Assert.True(result);
            _mockConsoleWrapper.Verify(c => c.SetCursorPosition(23, 10), Times.Once);
            _mockConsoleWrapper.Verify(c => c.WriteLine("Thanks for playing. Goodbye."), Times.Once);
        }
    }
}
