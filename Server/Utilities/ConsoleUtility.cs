using System;
using CitizenFX.Core;

namespace XCore.Server.Utilities
{
    public class ConsoleUtility
    {
        public void Log(string logLevel, string message, ConsoleColor leveColor, ConsoleColor messageColor)
        {
            var colorCode = GetColorCode(leveColor);
            var messageCode = GetColorCode(messageColor);
            Debug.WriteLine($"{colorCode}[{logLevel}]:\x1B[0m {messageCode}{message}\x1B[0m");
        }

        public static string GetColorCode(ConsoleColor color)
        {
            switch (color)
            {
                case ConsoleColor.Black: return "\x1B[30m";
                case ConsoleColor.DarkRed: return "\x1B[31m";
                case ConsoleColor.DarkGreen: return "\x1B[32m";
                case ConsoleColor.DarkYellow: return "\x1B[33m";
                case ConsoleColor.DarkBlue: return "\x1B[34m";
                case ConsoleColor.DarkMagenta: return "\x1B[35m";
                case ConsoleColor.DarkCyan: return "\x1B[36m";
                case ConsoleColor.Gray: return "\x1B[37m";
                case ConsoleColor.DarkGray: return "\x1B[90m";
                case ConsoleColor.Red: return "\x1B[91m";
                case ConsoleColor.Green: return "\x1B[92m";
                case ConsoleColor.Yellow: return "\x1B[93m";
                case ConsoleColor.Blue: return "\x1B[94m";
                case ConsoleColor.Magenta: return "\x1B[95m";
                case ConsoleColor.Cyan: return "\x1B[96m";
                case ConsoleColor.White: return "\x1B[97m";
                default: return "\x1B[0m";
            }
        }
    }
}