using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxerGamerRefactor
{
    public interface IBoxerGameInputHandler
    {
        BoxerAttack ChooseAttack(IEnumerable<BoxerAttack> attacks, out ConsoleKey consoleKey);

        string ReadString(string text);
        int ReadInt(string text);
    }

    public class BoxerGameInputHandler : IBoxerGameInputHandler
    {
        public BoxerAttack ChooseAttack(IEnumerable<BoxerAttack> attacks, out ConsoleKey consoleKey)
        {
            Console.WriteLine("");
            foreach (var attack in attacks)
            {
                Console.WriteLine($"Press = {attack.Key} = for a {attack.Title}");
            }
            Console.WriteLine("Which attack do you want to perform");
            var consoleInput = Console.ReadKey(true);
            consoleKey = consoleInput.Key;

            var choosenAttack = (attacks.Where(x => x.Key == consoleInput.Key)).FirstOrDefault();
            if (choosenAttack == null)
            {
                Console.WriteLine("It was not the you were told to press!");
            }
            return choosenAttack;
        }

        public string ReadString(string text)
        {
            Console.WriteLine(text);
            return GetInputAsString();
        }

        public int ReadInt(string text)
        {
            Console.WriteLine(text);
            return GetInputAsInt();
        }

        private string GetInputAsString()
        {
            return Console.ReadLine();
        }

        private int GetInputAsInt()
        {
            var consoleInput = GetInputAsString();
            var success = int.TryParse(consoleInput, out int value);
            return success ? value : 0;
        }
    }
}
