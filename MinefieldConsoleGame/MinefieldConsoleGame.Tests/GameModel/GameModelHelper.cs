using Moq;
using System.Collections.Generic;
using System.Drawing;

namespace MinefieldConsoleGame.Tests.GameModelTests
{
    public static class GameModelHelper
    {
        public static Mock<IMineFieldHelper> GetMinefieldMock(List<Point> minePoints, Point startingPoint)
        {
            Mock<IMineFieldHelper> mineFieldHelper = new Mock<IMineFieldHelper>();
            mineFieldHelper.Setup(m => m.CreateMineField(10, 10, 10)).Returns(() => minePoints);
            mineFieldHelper.Setup(m => m.GetStartingPoint()).Returns(() => startingPoint);

            return mineFieldHelper;
        }
    }
}
