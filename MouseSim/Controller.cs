using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MouseSim
{
    public class Controller
    {
        private View view;
        private Maze maze;
        private Simulator sim;

        private bool[,] visible;

        // 本走行だけゆっくり見たいので、const解除
        private static int kDelayMs = 30;

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
                    throw new InvalidOperationException("まだ迷路ファイルを読み込んでいないのに、迷路の情報を取り出そうとしました。");
                }

                return maze;
            }
        }

        public void LoadMaze()
        {
            try
            {
                maze = MazeReader.Load(view.MazeFile);
            }
            catch (IOException ex)
            {
                view.ShowMessage(ex.Message);
                maze = null;
            }

            sim = new Simulator(maze);
            InitVisible();

            view.PictureBox_Maze_Clear();
            view.DrawVisibility(maze, visible);
            view.DrawMaze(maze);
            view.DrawAgent(maze, sim);
            view.PictureBox_Maze_Invalidate();
        }

        private void InitVisible()
        {
            int size = maze.Size;

            visible = new bool[size, size];

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    visible[y, x] = false;
                }
            }

            visible[maze.StartY, maze.StartX] = true;
        }

        public async Task GoForward(int numBlocks, CancellationToken ct)
        {
            int fromX = sim.X;
            int fromY = sim.Y;
            sim.GoForward(numBlocks);
            int toX = sim.X;
            int toY = sim.Y;

            if (fromX == toX && fromY == toY)
            {
                return;
            }

            view.PictureBox_Maze_Clear();
            view.DrawVisibility(maze, visible);
            view.DrawMaze(maze);
            view.DrawArrow(fromX, fromY, toX, toY);
            view.PictureBox_Maze_Invalidate();

            await Task.Delay(kDelayMs, ct);

            // なんか同じ処理を書いているようでつらい
            if (fromX == toX)
            {
                for (int y = Math.Min(fromY, toY); y <= Math.Max(fromY, toY); y++)
                {
                    visible[y, fromX] = true;
                }
            }
            else
            {
                for (int x = Math.Min(fromX, toX); x <= Math.Max(fromX, toX); x++)
                {
                    visible[fromY, x] = true;
                }
            }

            view.PictureBox_Maze_Clear();
            view.DrawVisibility(maze, visible);
            view.DrawMaze(maze);
            view.DrawAgent(maze, sim);
            view.PictureBox_Maze_Invalidate();

            await Task.Delay(kDelayMs, ct);
        }

        public async Task TurnLeft(CancellationToken ct)
        {
            sim.TurnLeft();

            view.PictureBox_Maze_Clear();
            view.DrawVisibility(maze, visible);
            view.DrawMaze(maze);
            view.DrawAgent(maze, sim);
            view.PictureBox_Maze_Invalidate();

            await Task.Delay(kDelayMs, ct);
        }

        public async Task TurnRight(CancellationToken ct)
        {
            sim.TurnRight();

            view.PictureBox_Maze_Clear();
            view.DrawVisibility(maze, visible);
            view.DrawMaze(maze);
            view.DrawAgent(maze, sim);
            view.PictureBox_Maze_Invalidate();

            await Task.Delay(kDelayMs, ct);
        }

        public async Task DebugRun(CancellationToken ct)
        {
            InitVisible();

            sim.Reset();
            var agent = new Agent(sim.maze.Size, this, sim);

            var sw = new Stopwatch();
            sw.Start();

            kDelayMs = 30;

            while (true)
            {
                agent.LearnWallInfo();
                var response = agent.Adachi();
                // view.TextBox_AgentInfo_AppendLine(response.Type.ToString());
                view.TextBox_AgentInfo_AppendLine(agent.LoopCount.ToString());

                bool fBreak = false;

                switch (response.Type)
                {
                    case EAgentActionType.GO_FORWARD:
                        await GoForward(response.Value, ct);
                        break;

                    case EAgentActionType.TURN_LEFT:
                        await TurnLeft(ct);
                        break;

                    case EAgentActionType.TURN_RIGHT:
                        await TurnRight(ct);
                        break;

                    case EAgentActionType.NO_OPERATION:
                        fBreak = true;
                        break;
                }

                if (fBreak)
                {
                    break;
                }

                ct.ThrowIfCancellationRequested();
            }

            while (true)
            {
                agent.LearnWallInfo();
                var response = agent.Explore();
                // view.TextBox_AgentInfo_AppendLine(response.Type.ToString());
                view.TextBox_AgentInfo_AppendLine(agent.LoopCount.ToString());

                bool fBreak = false;

                switch (response.Type)
                {
                    case EAgentActionType.GO_FORWARD:
                        await GoForward(response.Value, ct);
                        break;

                    case EAgentActionType.TURN_LEFT:
                        await TurnLeft(ct);
                        break;

                    case EAgentActionType.TURN_RIGHT:
                        await TurnRight(ct);
                        break;

                    case EAgentActionType.NO_OPERATION:
                        fBreak = true;
                        break;
                }

                if (fBreak)
                {
                    break;
                }

                ct.ThrowIfCancellationRequested();
            }

            kDelayMs = 300;

            agent.ComputeShortestPath(true);

            while (true)
            {
                var response = agent.GetNextAction();
                view.TextBox_AgentInfo_AppendLine(response.Type.ToString());

                bool fBreak = false;

                switch (response.Type)
                {
                    case EAgentActionType.GO_FORWARD:
                        await GoForward(response.Value, ct);
                        break;

                    case EAgentActionType.TURN_LEFT:
                        await TurnLeft(ct);
                        break;

                    case EAgentActionType.TURN_RIGHT:
                        await TurnRight(ct);
                        break;

                    case EAgentActionType.NO_OPERATION:
                        fBreak = true;
                        break;
                }

                if (fBreak)
                {
                    break;
                }

                ct.ThrowIfCancellationRequested();
            }

            agent.ComputeShortestPath();

            while (true)
            {
                var response = agent.GetNextAction();
                view.TextBox_AgentInfo_AppendLine(response.Type.ToString());

                bool fBreak = false;

                switch (response.Type)
                {
                    case EAgentActionType.GO_FORWARD:
                        await GoForward(response.Value, ct);
                        break;

                    case EAgentActionType.TURN_LEFT:
                        await TurnLeft(ct);
                        break;

                    case EAgentActionType.TURN_RIGHT:
                        await TurnRight(ct);
                        break;

                    case EAgentActionType.NO_OPERATION:
                        fBreak = true;
                        break;
                }

                if (fBreak)
                {
                    break;
                }

                ct.ThrowIfCancellationRequested();
            }

            sw.Stop();
            view.TextBox_AgentInfo_AppendLine(sw.Elapsed.ToString());
        }

        public bool TryParse(string msg, out char cmd, out IList<string> args)
        {
            char[] delimiter = { ' ' };
            string[] seq = msg.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);

            args = new List<string>();
            cmd = '\0';

            if (seq.Length == 0)
            {
                return false;
            }

            if (seq[0].Length != 1)
            {
                return false;
            }

            // ここにエラー処理を入れる

            cmd = seq[0].ToCharArray()[0];

            for (int i = 1; i < seq.Length; i++)
            {
                args.Add(seq[i]);
            }

            return true;
        }

        public async Task Run(CancellationToken ct)
        {
            string fullpath = Path.Combine(view.WorkingDirectory, view.LaunchCommand);
            if (File.Exists(fullpath) == false)
            {
                view.ShowMessage(fullpath + " が見つかりません。");
                return;
            }

            sim.Reset();

            using (var program = new AgentCommunicator(view.WorkingDirectory, fullpath))
            {
                program.Launch();

                try
                {
                    string header = sim.Header();
                    view.TextBox_Transmit_AppendLine(header);
                    program.Transmit(header);

                    while (true)
                    {
                        string stdin = sim.WallInfo();
                        program.Transmit(stdin);
                        view.TextBox_Transmit_AppendLine(stdin);

                        string stdout;
                        while (true)
                        {
                            string recv = await program.RecieveAsync();

                            if (recv.StartsWith("#") == false)
                            {
                                stdout = recv;
                                break;
                            }

                            view.TextBox_AgentInfo_AppendLine(recv);
                        }

                        if (stdout == null)
                        {
                            view.TextBox_SimInfo_AppendLine("出力が読み取れませんでした");
                            break;
                        }

                        view.TextBox_Recieve_AppendLine(stdout);

                        char cmd;
                        IList<string> args;

                        if (TryParse(stdout, out cmd, out args) == false)
                        {
                            view.TextBox_SimInfo_AppendLine("出力が不正です　(" + stdout + ")");
                            break;
                        }

                        bool end = false;

                        switch (cmd)
                        {
                            case 'F':
                                int numBlocks = int.Parse(args[0]);
                                await GoForward(numBlocks, ct);
                                break;
                            case 'L':
                                await TurnLeft(ct);
                                break;
                            case 'R':
                                await TurnRight(ct);
                                break;
                            case 'S':
                                view.TextBox_SimInfo_AppendLine("リスタートします。");
                                sim.Reset();
                                break;
                            case 'X':
                                view.TextBox_SimInfo_AppendLine("終了しました。");
                                end = true;
                                break;
                            default:
                                break;
                        }

                        if (end) break;

                        ct.ThrowIfCancellationRequested();
                    }
                }
                finally
                {
                    program.Kill(); // DisposeしたときにKillしてくれるような気もしなくもない
                }
            }
        }
    }
}
