using System;

namespace BoxerGamerRefactor
{
    public class BoxerGraphics
    {
        public static void RenderCharacterTPose(int left, int top, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(left, top + 0);
            Console.WriteLine(" 0");
            Console.SetCursorPosition(left, top + 1);
            Console.WriteLine("/|\\");
            Console.SetCursorPosition(left, top + 2);
            Console.WriteLine("/ \\");
        }

        public static void RenderAttackLeftCharacter(int left, int top, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(left, top + 0);
            Console.WriteLine("  0");
            Console.SetCursorPosition(left, top + 1);
            Console.WriteLine("--|\\");
            Console.SetCursorPosition(left, top + 2);
            Console.WriteLine(" / \\");
        }

        public static void RenderAttackRightCharacter(int left, int top, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(left, top + 0);
            Console.WriteLine(" 0");
            Console.SetCursorPosition(left, top + 1);
            Console.WriteLine("/|--");
            Console.SetCursorPosition(left, top + 2);
            Console.WriteLine("/ \\");
        }
    }
}
