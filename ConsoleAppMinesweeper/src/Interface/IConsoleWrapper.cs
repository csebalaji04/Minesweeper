using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppMinesweeper.Interface
{
    public interface IConsoleWrapper
    {
        string ReadLine();
        void WriteLine(string message);
        void Clear();
        void SetCursorPosition(int left, int top);
        void SetForegroundColor(ConsoleColor color);

    }

}
