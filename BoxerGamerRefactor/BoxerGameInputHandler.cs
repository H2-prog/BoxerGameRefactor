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
        private IBoxerGameRenderer Renderer { get; }

        public BoxerGameInputHandler(IBoxerGameRenderer renderer)
        {
            Renderer = renderer;
        }

        public BoxerAttack ChooseAttack(IEnumerable<BoxerAttack> attacks, out ConsoleKey consoleKey)
        {
            int top = 12;
            Renderer.RenderText("====== Choose an Attack ======", 2, top);
            foreach (var attack in attacks)
            {
                top++;
                Console.SetCursorPosition(2, top);

                Console.Write("Press ");
                Console.ForegroundColor = attack.ForegroundColor;
                Console.Write(attack.Key);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($" for a {attack.Title}");
            }
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
