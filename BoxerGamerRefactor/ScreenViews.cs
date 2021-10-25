using System;

namespace BoxerGamerRefactor
{
    public static class ScreenViews
    {
        public static void GetReadyForNextRound(int round)
        {
            Console.WriteLine("");
            Console.WriteLine($"================= Round {round} has ended get ready for the next round =================");
            Console.WriteLine("");
        }

        public static ConsoleKeyInfo ChooseAttack()
        {
            Console.WriteLine();

            Console.WriteLine("Press = W = for a normal Attack");
            Console.WriteLine("Press = D = for a power Attack");
            Console.WriteLine("Which attack do you want to perform");

            return Console.ReadKey();
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
