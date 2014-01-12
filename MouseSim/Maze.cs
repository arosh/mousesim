using System;

namespace MouseSim
{
    public enum Direction
    {
        Top, Left, Bottom, Right
    }

    public class Maze
    {
        public int Size { get; private set; }
        public int StartX { get; private set; }
        public int StartY { get; private set; }
        public Direction StartDir { get; private set; }

        public int GoalX { get; private set; }
        public int GoalY { get; private set; }
        public int GoalW { get; private set; }
        public int GoalH { get; private set; }

        private bool[, ,] has_wall;

        public Maze(int size, int startX, int startY, Direction startDir, int goalX, int goalY, int goalW, int goalH)
        {
            this.Size = size;
            this.StartX = startX;
            this.StartY = startY;
            this.StartDir = startDir;

            this.GoalX = goalX;
            this.GoalY = goalY;
            this.GoalW = goalW;
            this.GoalH = goalH;

            has_wall = new bool[size, size, 4];
            Clear();
        }

        private void Clear()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        has_wall[i, j, k] = false;
                    }
                }
            }

            // 端に壁を建造する
            for (int i = 0; i < Size; i++)
            {
                has_wall[i, 0, (int)Direction.Top] = true;
                has_wall[i, Size - 1, (int)Direction.Bottom] = true;
                has_wall[0, i, (int)Direction.Left] = true;
                has_wall[Size - 1, i, (int)Direction.Right] = true;
            }
        }

        public void AddWall(int x, int y, Direction dir)
        {
            AssertRange(x, y);

            int[] dx = new int[] { 0, -1, 0, 1 };
            int[] dy = new int[] { -1, 0, 1, 0 };

            has_wall[x, y, (int)dir] = true;

            int nx = x + dx[(int)dir];
            int ny = y + dy[(int)dir];

            if (nx < 0 || nx >= Size || ny < 0 || ny >= Size)
            {
                return;
            }

            has_wall[nx, ny, ((int)dir + 2) % 4] = true;
        }

        public void AssertRange(int x, int y)
        {
            if (x < 0 || x >= Size)
            {
                throw new ArgumentOutOfRangeException("x", "x < 0 || x >= size, size = " + Size.ToString());
            }

            if (y < 0 || y >= Size)
            {
                throw new ArgumentOutOfRangeException("y", "y < 0 || y >= size, size = " + Size.ToString());
            }
        }

        public bool HasWall(int x, int y, Direction dir)
        {
            AssertRange(x, y);

            return has_wall[x, y, (int)dir];
        }

        public bool IsGoalArea(int x, int y)
        {
            return GoalX <= x && x < GoalX + GoalW && GoalY <= y && y < GoalY + GoalH;
        }
    }
}
