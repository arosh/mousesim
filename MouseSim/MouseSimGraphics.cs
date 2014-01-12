using System.Drawing;
using System.Drawing.Drawing2D;

namespace MouseSim
{
    static class MouseSimGraphicer
    {
        public static void DrawMaze(Graphics g, Size imageSize, MouseMaze maze)
        {
            // 枠は2pxで、両端分で2*2px
            int borderWidth = 2;
            int offset = borderWidth;
            int width = (imageSize.Width - 2*offset) / maze.Size;

            // 枠を塗る
            // 本当は2*borderWidthじゃなくてborderWidthでいいはずなんだけど、
            // 隅を塗るのがうまくいかないので
            // 左
            g.FillRectangle(Brushes.Black, 0, 0, 2 * borderWidth, imageSize.Height);
            // 右
            g.FillRectangle(Brushes.Black, imageSize.Width - borderWidth, 0, 2 * borderWidth, imageSize.Height);
            // 上
            g.FillRectangle(Brushes.Black, 0, 0, imageSize.Width, 2 * borderWidth);
            // 下
            g.FillRectangle(Brushes.Black, 0, imageSize.Height - borderWidth, imageSize.Width, 2 * borderWidth);

            Brush brush = Brushes.Black;

            for (int x = 0; x < maze.Size; x++)
            {
                for (int y = 0; y < maze.Size; y++)
                {
                    if (maze.HasWall(x, y, Direction.Top))
                    {
                        g.FillRectangle(brush, offset + width * x + 2, offset + width * y, width - 4, 2);
                    }

                    if (maze.HasWall(x, y, Direction.Left))
                    {
                        g.FillRectangle(brush, offset + width * x, offset + width * y + 2, 2, width - 4);
                    }

                    if (maze.HasWall(x, y, Direction.Bottom))
                    {
                        g.FillRectangle(brush, offset + width * x + 2, offset + width * y + (width - 2), width - 4, 2);
                    }

                    if (maze.HasWall(x, y, Direction.Right))
                    {
                        g.FillRectangle(brush, offset + width * x + (width - 2), offset + width * y + 2, 2, width - 4);
                    }
                }
            }

            for (int x = 0; x < maze.Size; x++)
            {
                for (int y = 0; y < maze.Size; y++)
                {
                    if (maze.HasWall(x, y, Direction.Top) || maze.HasWall(x, y, Direction.Left))
                    {
                        g.FillRectangle(brush, width * x, width * y, 4, 4);
                    }

                    if (maze.HasWall(x, y, Direction.Bottom) || maze.HasWall(x, y, Direction.Right))
                    {
                        g.FillRectangle(brush, width * (x + 1), width * (y + 1), 4, 4);
                    }
                }
            }
        }

        public static void DrawArrow(Graphics g, Size imageSize, int from_x, int from_y, int to_x, int to_y)
        {
            int size = 16;
            int width = (imageSize.Width - 4) / size;

            using (var pen = new Pen(Color.OrangeRed, 5))
            {
                using (pen.CustomEndCap = new AdjustableArrowCap(/* width = */ 3, /* height = */ 3))
                {
                        g.DrawLine(pen, 2 + width * from_x + (width / 2), 2 + width * from_y + (width / 2), 2 + width * to_x + (width / 2), 2 + width * to_y + (width / 2));
                }
            }
        }
    }
}
