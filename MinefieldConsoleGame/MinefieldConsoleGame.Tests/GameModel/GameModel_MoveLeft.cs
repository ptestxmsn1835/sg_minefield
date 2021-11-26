using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;

namespace MinefieldConsoleGame.Tests.GameModelTests
{
    public class GameModel_MoveLeft : TestSetup
    {
        [Test]
        public void GivenValidPositionMoveLeft()
        {
            List<Point> minePoints = new List<Point>();
            Point startingPoint = new Point(1, 0);
            Mock<IMineFieldHelper> mineFieldHelper = GameModelHelper.GetMinefieldMock(minePoints, startingPoint);

            GameModel gameModel = new GameModel(IOHelperMock.Object, mineFieldHelper.Object);
            gameModel.InitModel();
            gameModel.MoveLeft();
            Assert.AreEqual($"Move 1 - Player Position: {startingPoint.X - 1},{startingPoint.Y}, Lives Remaining: 10, Mines Remaining: {minePoints.Count}", gameModel.GetGameStatus());
        }

        [Test]
        public void GivenInvalidPositionMoveLeft()
        {
            List<Point> minePoints = new List<Point>();
            Point startingPoint = new Point(0, 0);
            Mock<IMineFieldHelper> mineFieldHelper = GameModelHelper.GetMinefieldMock(minePoints, startingPoint);

            GameModel gameModel = new GameModel(IOHelperMock.Object, mineFieldHelper.Object);
            gameModel.InitModel();
            gameModel.MoveLeft();
            Assert.AreEqual($"Move 1 - Player Position: {startingPoint.X},{startingPoint.Y}, Lives Remaining: 10, Mines Remaining: {minePoints.Count}", gameModel.GetGameStatus());
        }

        [Test]
        public void GivenValidPositionMoveLeftOntoMine()
        {
            List<Point> minePoints = new List<Point>() { new Point(0, 0) };
            Point startingPoint = new Point(1, 0);
            Mock<IMineFieldHelper> mineFieldHelper = GameModelHelper.GetMinefieldMock(minePoints, startingPoint);

            GameModel gameModel = new GameModel(IOHelperMock.Object, mineFieldHelper.Object);
            gameModel.InitModel();
            gameModel.MoveLeft();
            Assert.AreEqual($"Move 1 - Player Position: {startingPoint.X - 1},{startingPoint.Y}, Lives Remaining: 9, Mines Remaining: 0", gameModel.GetGameStatus());
        }
    }
}