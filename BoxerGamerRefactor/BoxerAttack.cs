using System;

namespace BoxerGamerRefactor
{
    public class BoxerAttack
    {
        public string Title { get; set; }
        public ConsoleKey Key { get; set; }
        public int Player1Modifier { get; set; }
        public int Player2Modifier { get; set; }

        public BoxerAttack(string title, ConsoleKey key, int player1Modifier, int player2Modifier)
        {
            Title = title;
            Key = key;
            Player1Modifier = player1Modifier;
            Player2Modifier = player2Modifier;
        }
    }
}
