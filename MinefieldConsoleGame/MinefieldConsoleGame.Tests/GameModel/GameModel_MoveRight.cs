using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;

namespace MinefieldConsoleGame.Tests.GameModelTests
{
    public class GameModel_MoveRight : TestSetup
    {
        [Test]
        public void GivenValidPositionMoveRight()
        {
            List<Point> minePoints = new List<Point>();
            Point startingPoint = new Point(0, 0);
            Mock<IMineFieldHelper> mineFieldHelper = GameModelHelper.GetMinefieldMock(minePoints, startingPoint);

            GameModel gameModel = new GameModel(IOHelperMock.Object, mineFieldHelper.Object);
            gameModel.InitModel();
            gameModel.MoveRight();
            Assert.AreEqual($"Move 1 - Player Position: {startingPoint.X + 1},{startingPoint.Y}, Lives Remaining: 10, Mines Remaining: {minePoints.Count}", gameModel.GetGameStatus());
        }

        [Test]
        public void GivenInvalidPositionMoveRight()
        {
            List<Point> minePoints = new List<Point>();
            Point startingPoint = new Point(9, 0);
            Mock<IMineFieldHelper> mineFieldHelper = GameModelHelper.GetMinefieldMock(minePoints, startingPoint);

            GameModel gameModel = new GameModel(IOHelperMock.Object, mineFieldHelper.Object);
            gameModel.InitModel();
            gameModel.MoveRight();
            Assert.AreEqual($"Move 1 - Player Position: {startingPoint.X},{startingPoint.Y}, Lives Remaining: 10, Mines Remaining: {minePoints.Count}", gameModel.GetGameStatus());
        }

        [Test]
        public void GivenValidPositionMoveRightOntoMine()
        {
            List<Point> minePoints = new List<Point>() { new Point(1, 0) };
            Point startingPoint = new Point(0, 0);
            Mock<IMineFieldHelper> mineFieldHelper = GameModelHelper.GetMinefieldMock(minePoints, startingPoint);

            GameModel gameModel = new GameModel(IOHelperMock.Object, mineFieldHelper.Object);
            gameModel.InitModel();
            gameModel.MoveRight();
            Assert.AreEqual($"Move 1 - Player Position: {startingPoint.X + 1},{startingPoint.Y}, Lives Remaining: 9, Mines Remaining: 0", gameModel.GetGameStatus());
        }
    }
}