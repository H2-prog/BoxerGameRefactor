namespace BoxerGamerRefactor
{
    public static class ConsoleGameFactory<TGame> 
        where TGame : ConsoleGame, new()
    {
        public static TGame Create()
        {
            TGame game = new TGame();
            game.GameLoop();
            return game;
        }
    }
}
