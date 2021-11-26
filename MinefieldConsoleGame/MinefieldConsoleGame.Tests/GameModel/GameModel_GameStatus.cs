using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace MinefieldConsoleGame.Tests.GameModelTests
{
    public class GameModel_GameStatus
    {

        [Test]
        public void IsGameOver_GameOngoing()
        {
            List<Point> minePoints = new List<Point>();
            Point startingPoint = new Point(0, 0);
            Mock<IMineFieldHelper> mineFieldHelper = GameModelHelper.GetMinefieldMock(minePoints, startingPoint);

            Mock<IIOHelper> ioHelper = new Mock<IIOHelper>();
            ioHelper.Setup(s => s.Write(It.IsAny<string>()));
            ioHelper.Setup(s => s.GetCharInput(It.IsAny<string>(), It.IsAny<List<ConsoleKey>>())).Returns(ConsoleKey.N);
            ioHelper.Setup(s => s.GetNumericInput(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(10);

            GameModel gameModel = new GameModel(ioHelper.Object, mineFieldHelper.Object);
            gameModel.InitModel();

            Assert.IsFalse(gameModel.IsGameOver());
            Assert.IsFalse(gameModel.IsGameWon());
        }

        [Test]
        public void IsGameOver_NoLives()
        {
            List<Point> minePoints = new List<Point>();
            Point startingPoint = new Point(0, 0);
            Mock<IMineFieldHelper> mineFieldHelper = GameModelHelper.GetMinefieldMock(minePoints, startingPoint);

            Mock<IIOHelper> ioHelper = new Mock<IIOHelper>();
            ioHelper.Setup(s => s.Write(It.IsAny<string>()));
            ioHelper.Setup(s => s.GetCharInput(It.IsAny<string>(), It.IsAny<List<ConsoleKey>>())).Returns(ConsoleKey.N);
            ioHelper.Setup(s => s.GetNumericInput(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(10);
            ioHelper.Setup(s => s.GetNumericInput("Live Count:", It.IsAny<int>(), It.IsAny<int>())).Returns(0);

            GameModel gameModel = new GameModel(ioHelper.Object, mineFieldHelper.Object);
            gameModel.InitModel();

            Assert.IsTrue(gameModel.IsGameOver());
            Assert.IsFalse(gameModel.IsGameWon());
        }

        [Test]
        public void IsGameOver_PlayerAtFinish()
        {
            List<Point> minePoints = new List<Point>();
            Point startingPoint = new Point(9, 0);
            Mock<IMineFieldHelper> mineFieldHelper = GameModelHelper.GetMinefieldMock(minePoints, startingPoint);

            Mock<IIOHelper> ioHelper = new Mock<IIOHelper>();
            ioHelper.Setup(s => s.Write(It.IsAny<string>()));
            ioHelper.Setup(s => s.GetCharInput(It.IsAny<string>(), It.IsAny<List<ConsoleKey>>())).Returns(ConsoleKey.N);
            ioHelper.Setup(s => s.GetNumericInput("Live Count:", It.IsAny<int>(), It.IsAny<int>())).Returns(10);
            ioHelper.Setup(s => s.GetNumericInput(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(10);

            GameModel gameModel = new GameModel(ioHelper.Object, mineFieldHelper.Object);
            gameModel.InitModel();

            Assert.IsTrue(gameModel.IsGameOver());
            Assert.IsTrue(gameModel.IsGameWon());
            ioHelper.Verify(v => v.Write("Game Won! You reached the end in 0 moves"), Times.Exactly(1));
        }

        [Test]
        public void IsGameOver_GameStatus_1Live_1Mines()
        {
            List<Point> minePoints = new List<Point>() { new Point(1, 0) };
            Point startingPoint = new Point(0, 0);
            Mock<IMineFieldHelper> mineFieldHelper = GameModelHelper.GetMinefieldMock(minePoints, startingPoint);

            Mock<IIOHelper> ioHelper = new Mock<IIOHelper>();
            ioHelper.Setup(s => s.Write(It.IsAny<string>()));
            ioHelper.Setup(s => s.GetCharInput(It.IsAny<string>(), It.IsAny<List<ConsoleKey>>())).Returns(ConsoleKey.N);
            ioHelper.Setup(s => s.GetNumericInput(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(10);
            ioHelper.Setup(s => s.GetNumericInput("Live Count:", It.IsAny<int>(), It.IsAny<int>())).Returns(1);

            GameModel gameModel = new GameModel(ioHelper.Object, mineFieldHelper.Object);
            gameModel.InitModel();

            Assert.AreEqual($"Move 0 - Player Position: {startingPoint.X},{startingPoint.Y}, Lives Remaining: 1, Mines Remaining: {minePoints.Count}", gameModel.GetGameStatus());
        }
    }
}