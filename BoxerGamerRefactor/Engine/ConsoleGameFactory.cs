using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace BoxerGamerRefactor
{
    public static class ConsoleGameFactory
    {
        private static ServiceCollection _services = new ServiceCollection();

        public static TGame Create<TGame>()
            where TGame : ConsoleGame, new()
        {
            var serviceProvider = _services.BuildServiceProvider();
            serviceProvider.CreateScope();

            TGame game = new TGame();
            game.ServiceProvider = serviceProvider;
            game.Setup();
            game.GameLoop();

            serviceProvider.Dispose();
            return game;
        }

        public static void AddScoped<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _services.AddScoped<TService, TImplementation>();
        }
    }
}
