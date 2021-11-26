using Microsoft.Extensions.DependencyInjection;

namespace MinefieldConsoleGame
{
    public static class DependencyInjectionSetup
    {
        public static ServiceProvider SetupDI()
        {
            return new ServiceCollection()
                .AddSingleton<IIOHelper, IOHelper>()
                .AddSingleton<IGame, Game>()
                .AddSingleton<IGameModel, GameModel>()
                .AddSingleton<IMineFieldHelper, MineFieldHelper>()
                .BuildServiceProvider();
        }
    }
}
