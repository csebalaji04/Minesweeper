using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppMinesweeper.Interface
{
    public interface IBoardManager
    {
        void SetBoard();

        void Print();

        Cell[,] Game2DArray { get; set; }

        int MinesCount { get; set; }
    }
}
