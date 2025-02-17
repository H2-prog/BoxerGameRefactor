﻿using System;
using System.Diagnostics;
using System.Text;

namespace BoxerGamerRefactor
{
    public interface IBoxerGameRenderer
    {
        void RenderBoxerStats(Boxer boxer, int startLeft, int startTop);
        void RenderBoxerCharacter(int startLeft, int startTop, ConsoleColor foregroundColor = ConsoleColor.Gray);
        void RenderWinnerCharacter(int startLeft, int startTop, ConsoleColor foregroundColor = ConsoleColor.Gray);
        string GenerateBar(double percent, char barCharacter, int barLength);

        void RenderText(string text, int startLeft, int startTop);

        void AddEmptyLine(uint lines = 1);
        void ClearLine(int lineToClear);
        void Clear();
    }

    public class BoxerGameRenderer : IBoxerGameRenderer
    {
        public void RenderBoxerStats(Boxer boxer, int startLeft, int startTop)
        {
            var healthPercent = Utils.CalculatePercent((double)boxer.Health, (double)boxer.StartHealth);
            var staminaPercent = Utils.CalculatePercent((double)boxer.Stamina, (double)boxer.StartStamina);
            var healthBar = GenerateBar(healthPercent, '#', 10);
            var staminaBar = GenerateBar(staminaPercent, '#', 10);

            Console.SetCursorPosition(startLeft, startTop + 0);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(boxer.Name);
            Console.SetCursorPosition(startLeft, startTop + 1);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"HP:{healthBar}");
            Console.SetCursorPosition(startLeft, startTop + 2);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"ST:{staminaBar}");
            Console.SetCursorPosition(startLeft, startTop + 3);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Victories: {boxer.Victories}");
        }

        public void RenderBoxerCharacter(int startLeft, int startTop, ConsoleColor foregroundColor = ConsoleColor.Gray)
        {
            Console.ForegroundColor = foregroundColor;
            Console.SetCursorPosition(startLeft, startTop + 0);
            Console.WriteLine(" 0");
            Console.SetCursorPosition(startLeft, startTop + 1);
            Console.WriteLine("/|\\");
            Console.SetCursorPosition(startLeft, startTop + 2);
            Console.WriteLine("/ \\");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void RenderWinnerCharacter(int startLeft, int startTop, ConsoleColor foregroundColor = ConsoleColor.Gray)
        {
            Console.ForegroundColor = foregroundColor;
            RenderText("\\0/", startLeft, startTop + 0);
            RenderText(" |", startLeft, startTop + 1);
            RenderText("/ \\", startLeft, startTop + 2);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public string GenerateBar(double percent, char barCharacter, int barLength)
        {
            percent = Math.Clamp(percent, 0, 100);
            var sb = new StringBuilder(new string(barCharacter, barLength));
            for (int i = barLength; i > (percent / 10); i--)
            {
                sb[i - 1] = '-';
            }
            return sb.ToString();
        }

        public void RenderText(string text, int startLeft, int startTop)
        {
            ClearLine(startTop);
            Console.SetCursorPosition(startLeft, startTop);
            Console.WriteLine(text);
        }

        public void AddEmptyLine(uint lines = 1)
        {
            if (lines == uint.MinValue)
            {
                lines = 1;
            }

            for (int i = 0; i < lines; i++)
            {
                Console.WriteLine("");
            }
        }

        public void ClearLine(int lineToClear)
        {
            Console.SetCursorPosition(0, lineToClear);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, lineToClear);
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}
