using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxerGamerRefactor
{
    public interface IBoxerGameInputHandler
    {
        string ReadString(string text);
        int ReadInt(string text);
    }

    public class BoxerGameInputHandler : IBoxerGameInputHandler
    {
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
