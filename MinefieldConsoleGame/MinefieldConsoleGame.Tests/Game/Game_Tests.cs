using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace MinefieldConsoleGame.Tests.GameTests
{
    public class Game_Tests
    {
        private Mock<IGameModel> gameModel;
        private Mock<IMineFieldHelper> mineFieldHelper;

        [SetUp]
        public void Setup()
        {
            gameModel = new Mock<IGameModel>();

            List<Point> minePoints = new List<Point>();
            Point startingPoint = new Point(0, 0);
            mineFieldHelper = new Mock<IMineFieldHelper>();
            mineFieldHelper.Setup(m => m.CreateMineField(10, 10, 10)).Returns(() => minePoints);
            mineFieldHelper.Setup(m => m.GetStartingPoint()).Returns(() => startingPoint);
        }

        [Test]
        public void Game_ExitGame_EscapeKey()
        {
            List<ConsoleKey> arrowKeys = new List<ConsoleKey>() { ConsoleKey.Escape };
            List<ConsoleKey> playAnotherGameKeys = new List<ConsoleKey>() { ConsoleKey.N };
            Mock<IIOHelper> ioHelper = GameHelper.GetIOHelperMock(arrowKeys, playAnotherGameKeys);

            Game game = new Game(ioHelper.Object, gameModel.Object);
            game.StartGame();

            GameHelper.VerifyIOWriter(ioHelper, 1, 0);
        }

        [Test]
        public void Game_PlayGame_ArrowKeys()
        {
            List<ConsoleKey> arrowKeys = new List<ConsoleKey>() { ConsoleKey.LeftArrow, ConsoleKey.UpArrow, ConsoleKey.RightArrow, ConsoleKey.DownArrow, ConsoleKey.Escape };
            List<ConsoleKey> playAnotherGameKeys = new List<ConsoleKey>() { ConsoleKey.N };
            Mock<IIOHelper> ioHelper = GameHelper.GetIOHelperMock(arrowKeys, playAnotherGameKeys);

            Game game = new Game(ioHelper.Object, gameModel.Object);
            game.StartGame();

            GameHelper.VerifyIOWriter(ioHelper, 1, 0);

            gameModel.Verify(v => v.MoveLeft(), Times.Once);
            gameModel.Verify(v => v.MoveUp(), Times.Once);
            gameModel.Verify(v => v.MoveRight(), Times.Once);
            gameModel.Verify(v => v.MoveDown(), Times.Once);
        }

        [Test]
        public void Game_PlayGame_SingleGame_Win()
        {
            List<ConsoleKey> arrowKeys = new List<ConsoleKey>() { ConsoleKey.RightArrow };
            List<ConsoleKey> playAnotherGameKeys = new List<ConsoleKey>() { ConsoleKey.N };
            Mock<IIOHelper> ioHelper = GameHelper.GetIOHelperMock(arrowKeys, playAnotherGameKeys);
            Mock<IGameModel> simpleGameModel = GameHelper.GetGameModelMock(1, 1);

            Game game = new Game(ioHelper.Object, simpleGameModel.Object);
            game.StartGame();

            GameHelper.VerifyIOWriter(ioHelper, 1, 1);
            simpleGameModel.Verify(v => v.MoveRight(), Times.Exactly(1));
        }

        [Test]
        public void Game_PlayGame_MultipleGames_Win()
        {
            List<ConsoleKey> arrowKeys = new List<ConsoleKey>() { ConsoleKey.RightArrow, ConsoleKey.RightArrow };
            List<ConsoleKey> playAnotherGameKeys = new List<ConsoleKey>() { ConsoleKey.Y, ConsoleKey.N };
            Mock<IIOHelper> ioHelper = GameHelper.GetIOHelperMock(arrowKeys, playAnotherGameKeys);
            Mock<IGameModel> simpleGameModel = GameHelper.GetGameModelMock(2, 2);

            Game game = new Game(ioHelper.Object, simpleGameModel.Object);
            game.StartGame();

            GameHelper.VerifyIOWriter(ioHelper, 2, 2);
            simpleGameModel.Verify(v => v.MoveRight(), Times.Exactly(2));
        }

        [Test]
        public void Game_PlayGame_SingleGame_Lose()
        {
            List<ConsoleKey> arrowKeys = new List<ConsoleKey>() { ConsoleKey.RightArrow };
            List<ConsoleKey> playAnotherGameKeys = new List<ConsoleKey>() { ConsoleKey.N };
            Mock<IIOHelper> ioHelper = GameHelper.GetIOHelperMock(arrowKeys, playAnotherGameKeys);
            Mock<IGameModel> simpleGameModel = GameHelper.GetGameModelMock(1, 0);

            Game game = new Game(ioHelper.Object, simpleGameModel.Object);
            game.StartGame();

            GameHelper.VerifyIOWriter(ioHelper, 1, 0);
            simpleGameModel.Verify(v => v.MoveRight(), Times.Exactly(1));
        }

        [Test]
        public void Game_PlayGame_MultipleGames_Lose()
        {
            List<ConsoleKey> arrowKeys = new List<ConsoleKey>() { ConsoleKey.RightArrow, ConsoleKey.RightArrow };
            List<ConsoleKey> playAnotherGameKeys = new List<ConsoleKey>() { ConsoleKey.Y, ConsoleKey.N };
            Mock<IIOHelper> ioHelper = GameHelper.GetIOHelperMock(arrowKeys, playAnotherGameKeys);
            Mock<IGameModel> simpleGameModel = GameHelper.GetGameModelMock(2, 0);

            Game game = new Game(ioHelper.Object, simpleGameModel.Object);
            game.StartGame();

            GameHelper.VerifyIOWriter(ioHelper, 2, 0);
            simpleGameModel.Verify(v => v.MoveRight(), Times.Exactly(2));
        }
    }
}