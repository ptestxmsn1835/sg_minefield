using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;

namespace MinefieldConsoleGame.Tests.GameModelTests
{
    public class GameModel_MoveUp : TestSetup
    {
        [Test]
        public void GivenValidPositionMoveUp()
        {
            List<Point> minePoints = new List<Point>();
            Point startingPoint = new Point(0, 0);
            Mock<IMineFieldHelper> mineFieldHelper = GameModelHelper.GetMinefieldMock(minePoints, startingPoint);

            GameModel gameModel = new GameModel(IOHelperMock.Object, mineFieldHelper.Object);
            gameModel.InitModel();
            gameModel.MoveUp();
            Assert.AreEqual($"Move 1 - Player Position: {startingPoint.X},{startingPoint.Y + 1}, Lives Remaining: 10, Mines Remaining: {minePoints.Count}", gameModel.GetGameStatus());
        }

        [Test]
        public void GivenInvalidPositionMoveUp()
        {
            List<Point> minePoints = new List<Point>();
            Point startingPoint = new Point(0, 9);
            Mock<IMineFieldHelper> mineFieldHelper = GameModelHelper.GetMinefieldMock(minePoints, startingPoint);

            GameModel gameModel = new GameModel(IOHelperMock.Object, mineFieldHelper.Object);
            gameModel.InitModel();
            gameModel.MoveUp();
            Assert.AreEqual($"Move 1 - Player Position: {startingPoint.X},{startingPoint.Y}, Lives Remaining: 10, Mines Remaining: {minePoints.Count}", gameModel.GetGameStatus());
        }

        [Test]
        public void GivenValidPositionMoveUpOntoMine()
        {
            List<Point> minePoints = new List<Point>() { new Point(0, 1) };
            Point startingPoint = new Point(0, 0);
            Mock<IMineFieldHelper> mineFieldHelper = GameModelHelper.GetMinefieldMock(minePoints, startingPoint);

            GameModel gameModel = new GameModel(IOHelperMock.Object, mineFieldHelper.Object);
            gameModel.InitModel();
            gameModel.MoveUp();
            Assert.AreEqual($"Move 1 - Player Position: {startingPoint.X},{startingPoint.Y + 1}, Lives Remaining: 9, Mines Remaining: 0", gameModel.GetGameStatus());
        }
    }
}