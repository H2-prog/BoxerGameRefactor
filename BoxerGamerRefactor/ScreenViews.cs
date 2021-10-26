using System;
using System.Linq;
using System.Collections.Generic;

namespace BoxerGamerRefactor
{
    public static class ScreenViews
    {
        public static void GetReadyForNextRound(int round)
        {
            AddText($"================= Round {round} has ended get ready for the next round =================");
        }

        public static void ShowBoxerHitMessage(Boxer attacker, Boxer victim, BoxerDamageResult damageResult)
        {
            var outputLine = $"{attacker.Name} Hits {victim.Name} for {damageResult.Damage}, {victim.Name} takes {damageResult.Damage} Damage, {victim.Name} now has {damageResult.NewHealth} health left";
            AddText(outputLine);
        }

        public static void AddText(string text)
        {
            Console.WriteLine(text);
        }

        public static void AddSeperator(int length = 90)
        {
            Console.WriteLine(new string('#', length));
        }

        public static void AddEmptyLine(uint lines = 1)
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

        public static string ReadString(string text)
        {
            Console.WriteLine(text);
            return GetInputAsString();
        }

        public static int ReadInt(string text)
        {
            Console.WriteLine(text);
            return GetInputAsInt();
        }

        public static ConsoleKeyInfo ShowAndChooseAttack(IEnumerable<BoxerAttack> attacks)
        {
            AddEmptyLine();
            foreach (var attack in attacks)
            {
                AddText($"Press = {attack.Key} = for a {attack.Title}");
            }
            AddText("Which attack do you want to perform");

            return Console.ReadKey(true);
        }

        public static string GetInputAsString()
        {
            return Console.ReadLine();
        }

        public static int GetInputAsInt()
        {
            var consoleInput = Console.ReadLine();
            var success = int.TryParse(consoleInput, out int value);
            return success ? value : 0;
        }

        public static void Clear()
        {
            Console.Clear();
        }
    }
}
