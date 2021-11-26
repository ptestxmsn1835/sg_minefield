using Moq;
using System;
using System.Collections.Generic;

namespace MinefieldConsoleGame.Tests.GameTests
{
    public static class GameHelper
    {
        public static Mock<IIOHelper> GetIOHelperMock(List<ConsoleKey> keyPresses, List<ConsoleKey> playAnotherGameKeys)
        {
            Queue<ConsoleKey> keyPressesQueue = new Queue<ConsoleKey>(keyPresses);
            Queue<ConsoleKey> playAnotherGameQueue = new Queue<ConsoleKey>(playAnotherGameKeys);

            Mock<IIOHelper> ioHelper = new Mock<IIOHelper>();
            ioHelper.Setup(s => s.Write(It.IsAny<string>()));
            ioHelper.Setup(s => s.GetCharInput(null, It.IsAny<List<ConsoleKey>>())).Returns(keyPressesQueue.Dequeue);
            ioHelper.Setup(s => s.GetCharInput("Play another game? (Y or N):", It.IsAny<List<ConsoleKey>>())).Returns(playAnotherGameQueue.Dequeue);

            return ioHelper;
        }
        public static Mock<IGameModel> GetGameModelMock(int games, int gamesWon)
        {
            int gamesWonAdded = 0;
            Queue<bool> isGameOverQueue = new Queue<bool>();
            Queue<bool> isGameWonQueue = new Queue<bool>();

            for (int i = 0; i < games; i++)
            {
                isGameOverQueue.Enqueue(false);
                isGameOverQueue.Enqueue(true);

                if (gamesWonAdded < gamesWon)
                {
                    isGameWonQueue.Enqueue(true);
                    gamesWonAdded++;
                }
                else
                {
                    isGameWonQueue.Enqueue(false);
                }
            }

            Mock<IGameModel> simpleGameModel = new Mock<IGameModel>();
            simpleGameModel.Setup(s => s.IsGameOver()).Returns(isGameOverQueue.Dequeue);
            simpleGameModel.Setup(s => s.IsGameWon()).Returns(isGameWonQueue.Dequeue);

            return simpleGameModel;
        }

        public static void VerifyIOWriter(Mock<IIOHelper> ioHelper, int games, int gamesWon)
        {
            for (int i = 0; i < games; i++)
            {
                ioHelper.Verify(v => v.Write($"\nGame {i + 1} Started! - Use arrow keys to reach the end of the gameboard"), Times.Once);
            }
            ioHelper.Verify(v => v.Write("Press Arrow Key to move, Press Escape to exit game:"), Times.Exactly(games));
            ioHelper.Verify(v => v.Write($"Thanks for playing! {gamesWon} games won!"), Times.Once);

        }
    }
}
