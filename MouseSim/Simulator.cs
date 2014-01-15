using System.Text;

namespace MouseSim
{
    public class Simulator
    {
        // Loggerがほしいよね

        private Maze maze;
        public int X { get; private set; }
        public int Y { get; private set; }
        public int dir;

        private static int[] dx = { 0, -1, 0, 1 };
        private static int[] dy = { -1, 0, 1, 0 };

        public Simulator(Maze maze)
        {
            this.maze = maze;
            this.X = maze.StartX;
            this.Y = maze.StartY;
            this.dir = (int)maze.StartDir;
        }

        public void Reset()
        {
            this.X = maze.StartX;
            this.Y = maze.StartY;
            this.dir = (int)maze.StartDir;
        }

        public void GoForward(int distance = 1)
        {
            for (int i = 0; i < distance; i++)
            {
                // 壁にあたったら、そこで止まる
                // 外枠に壁がなかったら間違いなくバグる
                if (maze.HasWall(X, Y, DirF))
                {
                    break;
                }

                X += dx[dir];
                Y += dy[dir];
            }
        }

        public void TurnRight()
        {
            dir = (dir + 4 - 1) % 4;
        }

        public void TurnLeft()
        {
            dir = (dir + 1) % 4;
        }

        // "前 左 後 右"
        public string WallInfo()
        {
            int front = maze.HasWall(X, Y, DirF) ? 1 : 0;
            int left = maze.HasWall(X, Y, DirL) ? 1 : 0;
            int back = maze.HasWall(X, Y, DirB) ? 1 : 0;
            int right = maze.HasWall(X, Y, DirR) ? 1 : 0;

            return string.Format("{0} {1} {2} {3}", front, left, back, right);
        }

        public string Header()
        {
            var builder = new StringBuilder();

            builder.AppendLine(string.Format("{0}", maze.Size));
            builder.AppendLine(string.Format("{0} {1} {2}", maze.StartX, maze.StartY, (int)maze.StartDir));
            builder.Append(string.Format("{0} {1} {2} {3}", maze.GoalX, maze.GoalY, maze.GoalW, maze.GoalH));

            return builder.ToString();
        }

        public bool IsGoal()
        {
            return maze.GoalX <= X && X < maze.GoalX + maze.GoalW && maze.GoalY <= Y && Y < maze.GoalH;
        }

        public bool HasWall(Direction dir)
        {
            return maze.HasWall(X, Y, dir);
        }

        public Direction DirF
        {
            get
            {
                return (Direction)dir;
            }
        }

        public Direction DirL
        {
            get
            {
                return (Direction)((dir + 1) % 4);
            }
        }

        public Direction DirB
        {
            get
            {
                return (Direction)((dir + 2) % 4);
            }
        }

        public Direction DirR
        {
            get
            {
                return (Direction)((dir + 3) % 4);
            }
        }
    }
}
