using System;
using System.Collections.Generic;

namespace MinefieldConsoleGame
{
    public class Game : IGame
    {
        private readonly IIOHelper ioHelper;
        private readonly IGameModel gameModel;
        private int games = 0;
        private int gamesWon = 0;

        public Game(IIOHelper ioHelper, IGameModel gameModel)
        {
            this.ioHelper = ioHelper;
            this.gameModel = gameModel;
        }

        public void StartGame()
        {
            while (true)
            {
                ioHelper.Write($"\nGame {games + 1} Started! - Use arrow keys to reach the end of the gameboard");

                gameModel.InitModel();
                StartGameInstance();

                ConsoleKey restartGameKey = ioHelper.GetCharInput("Play another game? (Y or N):", new List<ConsoleKey>() { ConsoleKey.Y, ConsoleKey.N });
                if (restartGameKey == ConsoleKey.N)
                {
                    ioHelper.Write($"Thanks for playing! {gamesWon} games won!");
                    break;
                }
            }
        }

        private void StartGameInstance()
        {
            ioHelper.Write("Press Arrow Key to move, Press Escape to exit game:");
            while (!gameModel.IsGameOver())
            {
                ConsoleKey key = ioHelper.GetCharInput(null, new List<ConsoleKey>() { ConsoleKey.LeftArrow, ConsoleKey.UpArrow, ConsoleKey.RightArrow, ConsoleKey.DownArrow, ConsoleKey.Escape });

                switch (key)
                {
                    case ConsoleKey.Escape:
                        return;
                    case ConsoleKey.LeftArrow:
                        gameModel.MoveLeft();
                        break;
                    case ConsoleKey.UpArrow:
                        gameModel.MoveUp();
                        break;
                    case ConsoleKey.RightArrow:
                        gameModel.MoveRight();
                        break;
                    case ConsoleKey.DownArrow:
                        gameModel.MoveDown();
                        break;
                    default:
                        //LogError - Impossible ConsoleKey return
                        return;
                }

                ioHelper.Write(gameModel.GetGameStatus());
            }

            if (gameModel.IsGameWon())
            {
                gamesWon++;
            }
            games++;
        }
    }
}
