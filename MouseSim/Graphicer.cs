using System.Drawing;
using System.Drawing.Drawing2D;

namespace MouseSim
{
    static class Graphicer
    {
        private const int kBorderWidth = 4;

        public static void DrawMaze(Graphics g, Size imageSize, Maze maze)
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

        public static void DrawArrow(Graphics g, Size imageSize, Maze maze, int from_x, int from_y, int to_x, int to_y)
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

        public static void DrawGoal(Graphics g, Size imageSize, Maze maze)
        {
            int cellLength = (imageSize.Width - kBorderWidth) / maze.Size;

            Brush brush = Brushes.Tomato;

            int x = maze.GoalX * cellLength;
            int y = maze.GoalY * cellLength;
            int width = maze.GoalW * cellLength;
            int height = maze.GoalH * cellLength;

            g.FillRectangle(brush, x, y, width, height);
        }

        public static void DrawAgent(Graphics g, Size imageSize, Maze maze, Simulator sim)
        {
            int x = sim.X;
            int y = sim.Y;
            Direction dir = sim.Dir;

            int cellLength = (imageSize.Width - kBorderWidth) / maze.Size;
            Brush brush = Brushes.Green;

            Point[] points;
            if (dir == Direction.Top)
            {
                points = new Point[] {
                    new Point(cellLength * x + kBorderWidth + (cellLength - kBorderWidth) * 1 / 2, cellLength * y + kBorderWidth + (cellLength - kBorderWidth) * 1 / 8),
                    new Point(cellLength * x + kBorderWidth + (cellLength - kBorderWidth) * 1 / 5, cellLength * y + kBorderWidth + (cellLength - kBorderWidth) * 7 / 8),
                    new Point(cellLength * x + kBorderWidth + (cellLength - kBorderWidth) * 4 / 5, cellLength * y + kBorderWidth + (cellLength - kBorderWidth) * 7 / 8)
                };
            }
            else if (dir == Direction.Left)
            {
                points = new Point[] {
                    new Point(cellLength * x + kBorderWidth + (cellLength - kBorderWidth) * 1 / 8, cellLength * y + kBorderWidth + (cellLength - kBorderWidth) * 1 / 2),
                    new Point(cellLength * x + kBorderWidth + (cellLength - kBorderWidth) * 7 / 8, cellLength * y + kBorderWidth + (cellLength - kBorderWidth) * 1 / 5),
                    new Point(cellLength * x + kBorderWidth + (cellLength - kBorderWidth) * 7 / 8, cellLength * y + kBorderWidth + (cellLength - kBorderWidth) * 4 / 5)
                };
            }
            else if (dir == Direction.Bottom)
            {
                points = new Point[] {
                    new Point(cellLength * x + kBorderWidth + (cellLength - kBorderWidth) * 1 / 2, cellLength * y + kBorderWidth + (cellLength - kBorderWidth) * 7 / 8),
                    new Point(cellLength * x + kBorderWidth + (cellLength - kBorderWidth) * 1 / 5, cellLength * y + kBorderWidth + (cellLength - kBorderWidth) * 1 / 8),
                    new Point(cellLength * x + kBorderWidth + (cellLength - kBorderWidth) * 4 / 5, cellLength * y + kBorderWidth + (cellLength - kBorderWidth) * 1 / 8)
                };
            }
            else if (dir == Direction.Right)
            {
                points = new Point[] {
                    new Point(cellLength * x + kBorderWidth + (cellLength - kBorderWidth) * 7 / 8, cellLength * y + kBorderWidth + (cellLength - kBorderWidth) * 1 / 2),
                    new Point(cellLength * x + kBorderWidth + (cellLength - kBorderWidth) * 1 / 8, cellLength * y + kBorderWidth + (cellLength - kBorderWidth) * 1 / 5),
                    new Point(cellLength * x + kBorderWidth + (cellLength - kBorderWidth) * 1 / 8, cellLength * y + kBorderWidth + (cellLength - kBorderWidth) * 4 / 5)
                };
            }
            else
            {
                points = null;
            }

            g.FillPolygon(brush, points);
        }
    }
}
