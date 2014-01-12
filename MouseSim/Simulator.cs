
namespace MouseSim
{
    public class Simulator
    {
        private Maze maze;
        public int X { get; private set; }
        public int Y { get; private set; }
        public int dir;

        public Direction Dir
        {
            get
            {
                return (Direction)dir;
            }
        }

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

        public void GoForward(int distance = 1) {
            int nx = X;
            int ny = Y;

            for (int i = 0; i < distance; i++)
            {
                // 壁にあたったら、そこで止まる
                if (maze.HasWall(nx, ny, Dir))
                {
                    break;
                }

                nx += dx[dir];
                ny += dy[dir];
            }

            if (0 <= nx && nx < maze.Size && 0 <= ny && ny < maze.Size)
            {
                X = nx;
                Y = ny;
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
            int top = maze.HasWall(X, Y, Direction.Top) ? 1 : 0;
            int left = maze.HasWall(X, Y, Direction.Left) ? 1 : 0;
            int bottom = maze.HasWall(X, Y, Direction.Bottom) ? 1 : 0;
            int right = maze.HasWall(X, Y, Direction.Right) ? 1 : 0;

            return string.Format("{0} {1} {2} {3}", top, left, bottom, right);
        }

        public bool HasWall(Direction dir)
        {
            return maze.HasWall(X, Y, dir);
        }
    }
}
