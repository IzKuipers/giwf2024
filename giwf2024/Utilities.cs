using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace giwf2024
{
    class Utilities
    {
        public static Point translateGridPosition(Point gridPosition)
        {
            return new Point(
                (gridPosition.X * Configuration.cellSize) + Configuration.windowPadding,
                (gridPosition.Y * Configuration.cellSize) + Configuration.windowPadding
            );
        }
    }
}   
