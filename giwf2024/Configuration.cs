using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace giwf2024
{
    class Configuration
    {
        public static int gridWidth = 35;
        public static int gridHeight = 35;
        public static readonly int cellSize = 20;
        public static readonly int windowPadding = 0;
        public static readonly Point playerStartPosition = new Point(0, 0);
        public static readonly string emptyCell = "...empty";
    }
}
