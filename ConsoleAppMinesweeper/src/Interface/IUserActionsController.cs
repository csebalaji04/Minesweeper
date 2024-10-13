using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppMinesweeper.Interface
{
    public interface IUserActionsController
    {

        int IsHidden { get; set; }

        int GetMinesNum();

        int GetValidLength(int limit, string scope);

        int GetColumn();

        int GetRow();

        void MovingAlongTheArray(Cell[,] gameBoard, int minesCounter);
    }
}
