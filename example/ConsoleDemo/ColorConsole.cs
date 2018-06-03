using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDemo
{
    static class ColorConsole
    {
        public static void WriteLine(string value, ConsoleColor? backgroundColor = null, ConsoleColor? foregroundColor = null)
        {
            Task.Run(() =>
            {
                lock (typeof(Console))
                {
                    Console.ResetColor();
                    if (backgroundColor != null)
                    {
                        Console.BackgroundColor = backgroundColor.Value;
                    }
                    if (foregroundColor != null)
                    {
                        Console.ForegroundColor = foregroundColor.Value;
                    }
                    Console.WriteLine(value);
                }
            });
        }
    }
}
