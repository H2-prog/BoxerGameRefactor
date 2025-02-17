﻿namespace BoxerGamerRefactor
{
    class BoxingGame
    {
        static void Main(string[] args)
        {
            ConsoleGameFactory.AddScoped<IBoxerGameController, BoxerGameController>();
            ConsoleGameFactory.AddScoped<IBoxerGameRenderer, BoxerGameRenderer>();
            ConsoleGameFactory.AddScoped<IBoxerGameInputHandler, BoxerGameInputHandler>();
            ConsoleGameFactory.AddScoped<IBoxerGameAIHandler, BoxerGameAIHandler>();
            ConsoleGameFactory.Create<BoxerGame>();
        }
    }
}
