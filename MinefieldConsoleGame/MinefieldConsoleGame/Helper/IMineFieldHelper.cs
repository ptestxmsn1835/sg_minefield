using System.Collections.Generic;
using System.Drawing;

namespace MinefieldConsoleGame
{
    public interface IMineFieldHelper
    {
        List<Point> CreateMineField(int height = 10, int width = 10, int mineCount = 10);
        Point GetStartingPoint();
    }
}