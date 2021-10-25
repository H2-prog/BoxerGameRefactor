namespace BoxerGamerRefactor
{
    public static class ConsoleGameFactory
    {
        public static TGame Create<TGame>()
            where TGame : ConsoleGame, new()
        {
            TGame game = new TGame();
            game.GameLoop();
            return game;
        }
    }
}
