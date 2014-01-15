using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MouseSim
{
    class Controller
    {
        private View view;
        private Maze maze;
        private Simulator sim;

        private static int kDelayMs = 200;

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

            view.DrawMaze(maze);
            view.DrawAgent(maze, sim);
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

            view.DrawMaze(maze);
            view.DrawArrow(fromX, fromY, toX, toY);

            await Task.Delay(kDelayMs, ct);

            view.DrawMaze(maze);
            view.DrawAgent(maze, sim);

            await Task.Delay(kDelayMs, ct);
        }

        public async Task TurnLeft(CancellationToken ct)
        {
            sim.TurnLeft();
            view.DrawMaze(maze);
            view.DrawAgent(maze, sim);

            await Task.Delay(kDelayMs, ct);
        }

        public async Task TurnRight(CancellationToken ct)
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
                if (sim.HasWall(sim.DirL) == false)
                {
                    await TurnLeft(ct);
                    await GoForward(1, ct);
                }
                else if (sim.HasWall(sim.DirF) == false)
                {
                    await GoForward(1, ct);
                }
                else
                {
                    await TurnRight(ct);
                }

                ct.ThrowIfCancellationRequested();
            }
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
                    view.Add_listbox_transmit(header);
                    program.Transmit(header);

                    while (true)
                    {
                        string stdin = sim.WallInfo();
                        program.Transmit(stdin);
                        view.Add_listbox_transmit(stdin);

                        string stdout;
                        while (true)
                        {
                            string recv = await program.RecieveAsync();

                            if (recv.StartsWith("#") == false)
                            {
                                stdout = recv;
                                break;
                            }

                            view.Add_listbox_agentinfo(recv);
                        }

                        if (stdout == null)
                        {
                            view.Add_listbox_siminfo("出力が読み取れませんでした");
                            break;
                        }

                        view.Add_listbox_recieve(stdout);

                        char cmd;
                        IList<string> args;

                        if (TryParse(stdout, out cmd, out args) == false)
                        {
                            view.Add_listbox_siminfo("出力が不正です　(" + stdout + ")");
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
                                view.Add_listbox_siminfo("リスタートします。");
                                sim.Reset();
                                break;
                            case 'X':
                                view.Add_listbox_siminfo("終了しました。");
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
