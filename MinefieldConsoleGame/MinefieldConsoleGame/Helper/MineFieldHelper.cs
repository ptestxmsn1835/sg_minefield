using System;
using System.Collections.Generic;
using System.Drawing;

namespace MinefieldConsoleGame
{
    public class MineFieldHelper : IMineFieldHelper
    {
        private int height;
        private int width;
        private Point startingPoint;

        public List<Point> CreateMineField(int height = 10, int width = 10, int mineCount = 10)
        {
            this.height = height;
            this.width = width;

            startingPoint = CreateStartingPoint();
            List<Point> safePathPoints = CreateSafePath(startingPoint);
            List<Point> minePoints = CreateMines(safePathPoints, mineCount);
            return minePoints;
        }

        public Point GetStartingPoint()
        {
            return startingPoint;
        }

        private Point CreateStartingPoint()
        {
            Random rnd = new Random();
            return new Point(0, rnd.Next(0, height));
        }

        private List<Point> CreateSafePath(Point startingPoint)
        {
            Random rnd = new Random();

            List<Point> safePathPoints = new List<Point>
            {
                startingPoint
            };

            int x = startingPoint.X;
            int y = startingPoint.Y;

            while (x < width)
            {
                int moveDirection = rnd.Next(0, 3);

                int tempX = x;
                int tempY = y;

                switch (moveDirection)
                {
                    case 0:
                        tempY++;
                        break;
                    case 2:
                        tempX++;
                        break;
                    case 3:
                        tempY--;
                        break;
                }

                Point newSafePoint = new Point(tempX, tempY);

                if (tempX < 0 || tempX > width || tempY < 0 || tempY > height || safePathPoints.Contains(newSafePoint))
                {
                    //Illegal Move - Try Again
                    continue;
                }

                safePathPoints.Add(newSafePoint);

                x = tempX;
                y = tempY;
            }

            return safePathPoints;
        }

        private List<Point> CreateMines(List<Point> safePathPoints, int mineCount)
        {
            Random rnd = new Random();
            List<Point> minePoints = new List<Point>();

            int maxMineSquares = (height * width) - safePathPoints.Count;
            mineCount = mineCount > maxMineSquares ? maxMineSquares : mineCount;
            int minesCreated = 0;

            while (minesCreated < mineCount)
            {
                Point minePoint = new Point(rnd.Next(0, width), rnd.Next(0, height));
                if (!safePathPoints.Contains(minePoint) && !minePoints.Contains(minePoint))
                {
                    minePoints.Add(minePoint);
                    minesCreated++;
                }
            }

            return minePoints;
        }
    }
}
