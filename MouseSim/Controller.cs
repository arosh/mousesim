using System;
using System.Threading;
using System.Threading.Tasks;

namespace MouseSim
{
    class Controller
    {
        private View view;
        private Maze maze;
        private Simulator sim;

        private int kDelayMs = 200;

        public Controller(View view)
        {
            this.view = view;
            this.maze = null;
            this.sim = null;
        }

        public bool IsMazeReady
        {
            get
            {
                return maze != null;
            }
        }

        public Maze Maze
        {
            get
            {
                if (maze == null)
                {
                    throw new InvalidOperationException("まだ迷路ファイルを読み込んでいないのに、迷路の情報を取り出そうとしました");
                }

                return maze;
            }
        }

        public void LoadMaze()
        {
            string maze_file = view.MazeFile;
            maze = MazeReader.Load(maze_file);
            sim = new Simulator(maze);
            view.DrawMaze(maze);
            view.DrawAgent(maze, sim);
        }

        public async Task DebugGoForward(CancellationToken ct)
        {
            int fromX = sim.X;
            int fromY = sim.Y;
            sim.GoForward();
            int toX = sim.X;
            int toY = sim.Y;

            if (fromX == toX && fromY == toY)
            {
                return;
            }

            view.DrawMaze(maze);
            view.DrawArrow(fromX, fromY, toX, toY);

            await Task.Delay(kDelayMs, ct);

            view.DrawMaze(maze);
            view.DrawAgent(maze, sim);

            await Task.Delay(kDelayMs, ct);
        }

        public async Task DebugTurnLeft(CancellationToken ct)
        {
            sim.TurnLeft();
            view.DrawMaze(maze);
            view.DrawAgent(maze, sim);

            await Task.Delay(kDelayMs, ct);
        }

        public async Task DebugTurnRight(CancellationToken ct)
        {
            sim.TurnRight();
            view.DrawMaze(maze);
            view.DrawAgent(maze, sim);

            await Task.Delay(kDelayMs, ct);
        }

        public async Task DebugRun(CancellationToken ct)
        {
            sim.Reset();

            while (true)
            {
                if (sim.HasWall((Direction)(((int)sim.Dir + 1) % 4)) == false)
                {
                    await DebugTurnLeft(ct);
                    await DebugGoForward(ct);
                }
                else if (sim.HasWall(sim.Dir) == false)
                {
                    await DebugGoForward(ct);
                }
                else
                {
                    await DebugTurnRight(ct);
                }
            }
        }
    }
}
