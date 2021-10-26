using System;

namespace BoxerGamerRefactor
{
    public class BoxerAttack
    {
        public string Title { get; set; }
        public ConsoleKey Key { get; set; }
        public ConsoleColor ForegroundColor { get; set; }
        public int PlayerModifier { get; set; }
        public int ComputerModifier { get; set; }

        public BoxerAttack(string title, ConsoleKey key, ConsoleColor color, int playerModifier, int computerModifier)
        {
            Title = title;
            Key = key;
            ForegroundColor = color;
            PlayerModifier = playerModifier;
            ComputerModifier = computerModifier;
        }
    }
}
