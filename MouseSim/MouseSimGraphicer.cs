using System.Drawing;
using System.Drawing.Drawing2D;

namespace MouseSim
{
    static class MouseSimGraphicer
    {
        private const int kBorderWidth = 4;

        public static void DrawMaze(Graphics g, Size imageSize, MouseMaze maze)
        {
            int cellLength = (imageSize.Width - kBorderWidth) / maze.Size;

            Brush brush = Brushes.Black;

            for (int y = 0; y < maze.Size; y++)
            {
                for (int x = 0; x < maze.Size; x++)
                {
                    if (maze.HasWall(x, y, Direction.Top))
                    {
                        g.FillRectangle(brush, x * cellLength, y * cellLength, cellLength + kBorderWidth, kBorderWidth);
                    }

                    if (maze.HasWall(x, y, Direction.Left))
                    {
                        g.FillRectangle(brush, x * cellLength, y * cellLength, kBorderWidth, cellLength + kBorderWidth);
                    }

                    if (maze.HasWall(x, y, Direction.Bottom))
                    {
                        g.FillRectangle(brush, x * cellLength, (y + 1) * cellLength, cellLength + kBorderWidth, kBorderWidth);
                    }

                    if (maze.HasWall(x, y, Direction.Right))
                    {
                        g.FillRectangle(brush, (x + 1) * cellLength, y * cellLength, kBorderWidth, cellLength + kBorderWidth);
                    }
                }
            }
        }

        public static void DrawArrow(Graphics g, Size imageSize, MouseMaze maze, int from_x, int from_y, int to_x, int to_y)
        {
            int offset = kBorderWidth / 2;
            int cellLength = (imageSize.Width - kBorderWidth) / maze.Size;

            using (var pen = new Pen(Color.OrangeRed, 5))
            {
                using (pen.CustomEndCap = new AdjustableArrowCap(/* width = */ 3, /* height = */ 3))
                {
                    g.DrawLine(
                        pen,
                        offset + cellLength * from_x + (cellLength / 2),
                        offset + cellLength * from_y + (cellLength / 2),
                        offset + cellLength * to_x + (cellLength / 2),
                        offset + cellLength * to_y + (cellLength / 2));
                }
            }
        }

        public static void DrawGoal(Graphics g, Size imageSize, MouseMaze maze)
        {
            int cellLength = (imageSize.Width - kBorderWidth) / maze.Size;

            Brush brush = Brushes.Tomato;

            int x = maze.GoalX * cellLength;
            int y = maze.GoalY * cellLength;
            int width = maze.GoalW * cellLength;
            int height = maze.GoalH * cellLength;

            g.FillRectangle(brush, x, y, width, height);
        }
    }
}
