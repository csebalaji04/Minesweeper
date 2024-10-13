﻿using ConsoleAppMinesweeper.Interface;
using System;
using System.Threading;

namespace ConsoleAppMinesweeper
{
    public static class LoadingScreen
    {
        public static void Load(IConsoleWrapper console)
        {
            int dotMovementNum = 30;
            const int REPETITIONS_NUM = 12;

            for (int i = 0; i <= REPETITIONS_NUM; i++)
            {
                console.SetCursorPosition(dotMovementNum, 7);
                console.WriteLine("  .");
                Thread.Sleep(135);
                dotMovementNum++;
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
