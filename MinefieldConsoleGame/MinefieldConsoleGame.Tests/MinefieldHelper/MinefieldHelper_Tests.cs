using Moq;
using NUnit.Framework;

namespace MinefieldConsoleGame.Tests.MinefieldHelperTests
{
    public class MinefieldHelper_Tests
    {
        [SetUp]
        public void Setup()
        {
            Mock<IIOHelper> ioHelper = new Mock<IIOHelper>();
            Mock<IGame> game = new Mock<IGame>();
            Mock<IMineFieldHelper> mineFieldHelper = new Mock<IMineFieldHelper>();
            Mock<IGameModel> gameModel = new Mock<IGameModel>();
            ioHelper.Setup(s => s.Write(It.IsAny<string>()));
        }

        [Test]
        public void CreateDefaultMinefield()
        {
            MineFieldHelper mineFieldHelper = new MineFieldHelper();
            System.Collections.Generic.List<System.Drawing.Point> minePoints = mineFieldHelper.CreateMineField(10, 10, 10);
            System.Drawing.Point startingPoint = mineFieldHelper.GetStartingPoint();

            Assert.AreEqual(10, minePoints.Count);
            Assert.AreEqual(0, startingPoint.X);
        }

        [Test]
        public void CreateMinefieldWithMineCountLimitedBySafePath()
        {
            MineFieldHelper mineFieldHelper = new MineFieldHelper();
            System.Collections.Generic.List<System.Drawing.Point> minePoints = mineFieldHelper.CreateMineField(10, 10, 1000);
            System.Drawing.Point startingPoint = mineFieldHelper.GetStartingPoint();

            Assert.True(minePoints.Count < 90);
            Assert.True(minePoints.Count > 0);
            Assert.AreEqual(0, startingPoint.X);
        }

        [Test]
        public void CreateMinefieldWithNoMines()
        {
            MineFieldHelper mineFieldHelper = new MineFieldHelper();
            System.Collections.Generic.List<System.Drawing.Point> minePoints = mineFieldHelper.CreateMineField(10, 10, 0);
            System.Drawing.Point startingPoint = mineFieldHelper.GetStartingPoint();

            Assert.AreEqual(0, minePoints.Count);
            Assert.AreEqual(0, startingPoint.X);
        }
    }
}