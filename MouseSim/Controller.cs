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

            int size = sim.maze.Size;

            int[,] d = new int[size, size];
            bool[,] vis = new bool[size, size];
            bool[, ,] wall = new bool[size, size, 4];
            bool[,] open = new bool[size, size]; // 一度でも行ったことがあるか？

            // =========
            // 壁の初期化
            // =========
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    for (int k = 0; k < 4; k++)
                        wall[i, j, k] = false;

            // 端に壁をたてる
            for (int i = 0; i < size; i++)
            {
                // 左
                wall[i, 0, 1] = true;
                // 右
                wall[i, size - 1, 3] = true;
                // 上
                wall[0, i, 0] = true;
                // 下
                wall[size - 1, i, 2] = true;
            }

            // ========
            // 座標関係
            // ========
            int curY = sim.maze.StartY;
            int curX = sim.maze.StartX;

            // 上左下右で反時計回り
            int dir = (int)sim.maze.StartDir;

            int[] dy = new int[] { -1, 0, 1, 0 };
            int[] dx = new int[] { 0, -1, 0, 1 };

            Func<int, int, bool> check_coor = (y, x) => (0 <= y && y < size && 0 <= x && x < size);

            // 探索を打ち切っても良い十分条件
            // 「現在地からゴールまでの仮想最短経路上のマスがすべて到達済み（壁の状態が調査済み）であり（←「通ろうとしている道に壁がないことが明らか」でも良いが、実装が難しい）
            // 現在の座標が「ゴールからスタートまでの最短経路上」に存在すること

            for (int T = 0; T < 3; T++)
            {
                while (!(sim.maze.GoalX <= curX && curX < sim.maze.GoalX + sim.maze.GoalW && sim.maze.GoalY <= curY && curY < sim.maze.GoalY + sim.maze.GoalH))
                {
                    // ===========
                    // 壁情報の入力
                    // ===========
                    Direction[] ds = new Direction[] { sim.DirF, sim.DirL, sim.DirB, sim.DirR };
                    for (int k = 0; k < 4; k++)
                    {
                        wall[curY, curX, (dir + k) % 4] = sim.HasWall(ds[k]);
                        if (check_coor(curY + dy[(dir + k) % 4], curX + dx[(dir + k) % 4]))
                        {
                            wall[curY + dy[(dir + k) % 4], curX + dx[(dir + k) % 4], (dir + k + 2) % 4] = wall[curY, curX, (dir + k) % 4];
                        }
                    }

                    // ============================
                    // ゴール位置からダイクストラ開始
                    // ============================

                    // すべての座標を未踏と設定し、距離を無限大に設定
                    for (int i = 0; i < size; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            d[i, j] = int.MaxValue;
                            vis[i, j] = false;
                        }
                    }

                    // ダイクストラのスタート地点の距離は0
                    for (int i = 0; i < sim.maze.GoalW; i++)
                    {
                        for (int j = 0; j < sim.maze.GoalH; j++)
                        {
                            d[sim.maze.GoalY + j, sim.maze.GoalX + i] = 0;
                        }
                    }

                    while (true)
                    {
                        // 未踏の地の中で距離が最小の地点を探す
                        int vx = -1, vy = -1;

                        for (int i = 0; i < size; i++)
                        {
                            for (int j = 0; j < size; j++)
                            {
                                if (vis[i, j] == false && ((vx == -1 && vy == -1) || d[i, j] < d[vy, vx]))
                                {
                                    vy = i;
                                    vx = j;
                                }
                            }
                        }

                        // 未踏の地が無かったら、探索終了
                        if (vx == -1 && vy == -1) break;

                        vis[vy, vx] = true;

                        for (int k = 0; k < 4; k++)
                        {
                            if (wall[vy, vx, k] == false)
                            {
                                int ny = vy + dy[k];
                                int nx = vx + dx[k];
                                d[ny, nx] = Math.Min(d[ny, nx], d[vy, vx] + 1);
                            }
                        }
                    }
                    // ============================
                    // ゴール位置からダイクストラ終了
                    // ============================

                    // TODO 経路を調べて、既にマッピング済みのマスだけを移動してゴールまで行けるようならば、探索を終了する
                    // マッピング済みの領域かどうかを保存する変数が必要

                    // どちらに移動するか決めて、そっちに移動する
                    if (wall[curY, curX, (dir + 0) % 4] == false && d[curY + dy[(dir + 0) % 4], curX + dx[(dir + 0) % 4]] == d[curY, curX] - 1)
                    {
                        await GoForward(1, ct);
                        curY += dy[dir];
                        curX += dx[dir];
                    }
                    else if (wall[curY, curX, (dir + 1) % 4] == false && d[curY + dy[(dir + 1) % 4], curX + dx[(dir + 1) % 4]] == d[curY, curX] - 1)
                    {
                        await TurnLeft(ct);
                        dir = (dir + 1) % 4;
                    }
                    else if (wall[curY, curX, (dir + 3) % 4] == false && d[curY + dy[(dir + 3) % 4], curX + dx[(dir + 3) % 4]] == d[curY, curX] - 1)
                    {
                        await TurnRight(ct);
                        dir = (dir + 3) % 4;
                    }
                    else
                    {
                        await TurnRight(ct);
                        dir = (dir + 3) % 4;
                    }

                    ct.ThrowIfCancellationRequested();
                }

                while (!(sim.maze.StartX <= curX && curX < sim.maze.StartX + 1 && sim.maze.StartY <= curY && curY < sim.maze.StartY + 1))
                {
                    // ===========
                    // 壁情報の入力
                    // ===========

                    Direction[] ds = new Direction[] { sim.DirF, sim.DirL, sim.DirB, sim.DirR };
                    for (int k = 0; k < 4; k++)
                    {
                        wall[curY, curX, (dir + k) % 4] = sim.HasWall(ds[k]);
                        if (check_coor(curY + dy[(dir + k) % 4], curX + dx[(dir + k) % 4]))
                        {
                            wall[curY + dy[(dir + k) % 4], curX + dx[(dir + k) % 4], (dir + k + 2) % 4] = wall[curY, curX, (dir + k) % 4];
                        }
                    }

                    // ============================
                    // ゴール位置からダイクストラ開始
                    // ============================

                    // すべての座標を未踏と設定し、距離を無限大に設定
                    for (int i = 0; i < size; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            d[i, j] = int.MaxValue;
                            vis[i, j] = false;
                        }
                    }

                    // ダイクストラのスタート地点の距離は0
                    d[sim.maze.StartY, sim.maze.StartX] = 0;

                    while (true)
                    {
                        // 未踏の地の中で距離が最小の地点を探す
                        int vx = -1, vy = -1;

                        for (int i = 0; i < size; i++)
                        {
                            for (int j = 0; j < size; j++)
                            {
                                if (vis[i, j] == false && ((vx == -1 && vy == -1) || d[i, j] < d[vy, vx]))
                                {
                                    vy = i;
                                    vx = j;
                                }
                            }
                        }

                        // 未踏の地が無かったら、探索終了
                        if (vx == -1 && vy == -1) break;

                        vis[vy, vx] = true;

                        for (int k = 0; k < 4; k++)
                        {
                            if (wall[vy, vx, k] == false)
                            {
                                int ny = vy + dy[k];
                                int nx = vx + dx[k];
                                d[ny, nx] = Math.Min(d[ny, nx], d[vy, vx] + 1);
                            }
                        }
                    }
                    // ============================
                    // ゴール位置からダイクストラ終了
                    // ============================

                    // TODO 経路を調べて、既にマッピング済みのマスだけを移動してゴールまで行けるようならば、探索を終了する
                    // マッピング済みの領域かどうかを保存する変数が必要

                    // どちらに移動するか決めて、そっちに移動する
                    if (wall[curY, curX, (dir + 0) % 4] == false && d[curY + dy[(dir + 0) % 4], curX + dx[(dir + 0) % 4]] == d[curY, curX] - 1)
                    {
                        await GoForward(1, ct);
                        curY += dy[dir];
                        curX += dx[dir];
                    }
                    else if (wall[curY, curX, (dir + 1) % 4] == false && d[curY + dy[(dir + 1) % 4], curX + dx[(dir + 1) % 4]] == d[curY, curX] - 1)
                    {
                        await TurnLeft(ct);
                        dir = (dir + 1) % 4;
                    }
                    else if (wall[curY, curX, (dir + 3) % 4] == false && d[curY + dy[(dir + 3) % 4], curX + dx[(dir + 3) % 4]] == d[curY, curX] - 1)
                    {
                        await TurnRight(ct);
                        dir = (dir + 3) % 4;
                    }
                    else
                    {
                        await TurnRight(ct);
                        dir = (dir + 3) % 4;
                    }

                    ct.ThrowIfCancellationRequested();
                }
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
