using Microsoft.Extensions.DependencyInjection;

namespace MinefieldConsoleGame
{
    public class Program
    {
        private static void Main(string[] args)
        {
            ServiceProvider serviceProvider = DependencyInjectionSetup.SetupDI();
            IGame game = serviceProvider.GetService<IGame>();
            game.StartGame();
        }
    }
}
