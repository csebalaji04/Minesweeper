using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppMinesweeper.Interface
{
    public interface ITextManager
    {
        void PrintGameInstructions();
        void ClearUserInputGameInstructions();
        void PrintUserDifficultyTitle();
        void PrintWhetherRestartGame();
        void ClearUserInputRestartGame();
    }

}
