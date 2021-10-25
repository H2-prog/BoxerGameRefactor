using System;
using System.Linq;
using System.Collections.Generic;

namespace BoxerGamerRefactor
{
    public static class ScreenViews
    {
        public static void GetReadyForNextRound(int round)
        {
            AddEmptyLine();
            Console.WriteLine($"================= Round {round} has ended get ready for the next round =================");
            AddEmptyLine();
        }

        public static void ShowBoxerHitMessage(Boxer attacker, Boxer victim, BoxerDamageResult damageResult)
        {
            var outputLine = $"{attacker.Name} Hits {victim.Name} for {damageResult.Damage}, {victim.Name} takes {damageResult.Damage} Damage, {victim.Name} now has {damageResult.NewHealth} health left";
            Console.WriteLine(outputLine);
        }

        public static void AddEmptyLine(uint lines = 1)
        {
            if(lines == uint.MinValue)
            {
                lines = 1;
            }

            for (int i = 0; i < lines; i++)
            {
                Console.WriteLine("");
            }
        }

        public static ConsoleKeyInfo ShowAndChooseAttack(IEnumerable<BoxerAttack> attacks)
        {
            AddEmptyLine();

            foreach (var attack in attacks)
            {
                Console.WriteLine($"Press = {attack.Key} = for a {attack.Title}");
            }
            Console.WriteLine("Which attack do you want to perform");

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
    }
}
