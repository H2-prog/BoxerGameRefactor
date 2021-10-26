using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxerGamerRefactor
{
    public interface IBoxerGameInputHandler
    {
        BoxerAttack ChooseAttack(IEnumerable<BoxerAttack> attacks);

        string ReadStringWithText(string text);
        int ReadIntWithText(string text);
    }

    public class BoxerGameInputHandler : IBoxerGameInputHandler
    {
        private IBoxerGameRenderer Renderer { get; }

        public BoxerGameInputHandler(IBoxerGameRenderer renderer)
        {
            Renderer = renderer;
        }

        public BoxerAttack ChooseAttack(IEnumerable<BoxerAttack> attacks)
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
            var choosenAttack = (attacks.Where(x => x.Key == consoleInput.Key)).FirstOrDefault();
            return choosenAttack;
        }

        public string ReadStringWithText(string text)
        {
            Console.WriteLine(text);
            return GetInputAsString();
        }

        public int ReadIntWithText(string text)
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
