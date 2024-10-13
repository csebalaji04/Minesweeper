using ConsoleAppMinesweeper.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppMinesweeper
{
    public class ConsoleWrapper : IConsoleWrapper
    {
        public string ReadLine() => Console.ReadLine();
        public void WriteLine(string message) => Console.WriteLine(message);
        public void Clear() => Console.Clear();
        public void SetCursorPosition(int left, int top) => Console.SetCursorPosition(left, top);
        public void SetForegroundColor(ConsoleColor color) => Console.ForegroundColor = color;
    }

}
