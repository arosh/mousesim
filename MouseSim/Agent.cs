using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseSim
{
    public enum EAgentActionType
    {
        GO_FORWARD, TURN_LEFT, TURN_RIGHT, NO_OPERATION
    }

    public class AgentAction
    {
        public EAgentActionType Type { get; private set; }
        public int Value { get; private set; }
        public AgentAction(EAgentActionType type, int value)
        {
            this.Type = type;
            this.Value = value;
        }
    }

    public class Agent
    {
        private Controller ctrl;
        private Simulator sim;

        private int size;
        private bool[, ,] wall;
        private bool[,] visible;
        private int curY, curX;
        private int dir;

        // 上左下右で反時計回り
        private int[] dy = new int[] { -1, 0, 1, 0 };
        private int[] dx = new int[] { 0, -1, 0, 1 };

        public Agent(int size, Controller ctrl, Simulator sim)
        {
            this.ctrl = ctrl;
            this.sim = sim;

            this.size = size;
            this.wall = new bool[size, size, 4];
            this.visible = new bool[size, size];

            Initialize();
        }

        // 同じマップでリスタートに使える処理はこっちに書く
        public void Initialize()
        {
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

            // =======
            // 座標関係
            // =======
            curY = sim.maze.StartY;
            curX = sim.maze.StartX;

            // 上左下右で反時計回り
            dir = (int)sim.maze.StartDir;

            // =======
            // 探索関係
            // =======
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    visible[y, x] = false;
                }
            }
        }

        public AgentAction Explore()
        {
            var d = new int[size, size];
            var vis = new bool[size, size];

            // すべての未開の地をゴールと見立てて初期化する
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    vis[y, x] = false;
                    if (visible[y, x] == false)
                    {
                        d[y, x] = 0;
                    }
                    else
                    {
                        d[y, x] = int.MaxValue;
                    }
                }
            }

            //ダイクストラよりもSPFAのほうが向いているかも？
            // O(V^2)のダイクストラ開始
            while (true)
            {
                int vy = -1, vx = -1;

                for (int y = 0; y < size; y++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        if (vis[y, x] == false && ((vx == -1 && vy == -1) || d[y, x] < d[vy, vx]))
                        {
                            vy = y;
                            vx = x;
                        }
                    }
                }

                // 未踏の地が無かったら、探索終了
                if ((vx == -1 && vy == -1) || d[vy, vx] == int.MaxValue)
                {
                    break;
                }

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
            // O(V^2)のダイクストラ終了

            // どちらに移動するか決めて、そっちに移動する
            if (wall[curY, curX, (dir + 0) % 4] == false && d[curY + dy[(dir + 0) % 4], curX + dx[(dir + 0) % 4]] == d[curY, curX] - 1)
            {
                // GoForward
                curY += dy[dir];
                curX += dx[dir];
                return new AgentAction(EAgentActionType.GO_FORWARD, 1);
            }
            else if (wall[curY, curX, (dir + 1) % 4] == false && d[curY + dy[(dir + 1) % 4], curX + dx[(dir + 1) % 4]] == d[curY, curX] - 1)
            {
                // TurnLeft
                dir = (dir + 1) % 4;
                return new AgentAction(EAgentActionType.TURN_LEFT, 0);
            }
            else if (wall[curY, curX, (dir + 3) % 4] == false && d[curY + dy[(dir + 3) % 4], curX + dx[(dir + 3) % 4]] == d[curY, curX] - 1)
            {
                // TurnRight
                dir = (dir + 3) % 4;
                return new AgentAction(EAgentActionType.TURN_RIGHT, 0);
            }
            else if (wall[curY, curX, (dir + 2) % 4] == false && d[curY + dy[(dir + 2) % 4], curX + dx[(dir + 2) % 4]] == d[curY, curX] - 1)
            {
                // TurnLeft
                // 後ろを向くのが最適解の場合は、とりあえず左を向いておいて、次のステップに任せる
                dir = (dir + 1) % 4;
                return new AgentAction(EAgentActionType.TURN_LEFT, 0);
            }
            else
            {
                // どの向きを向いても未開の地に辿りつけない場合 -> 連結でないマスが存在する場合
                return new AgentAction(EAgentActionType.NO_OPERATION, 0);
            }
        }

        public void LearnWallInfo()
        {
            // * そのマスの周り4マスがすべてvisibleだったらそのマスもvisibleだから探索しなくてよい、
            //   みたいな処理を入れたら探索走行が高速になるのでは
            // * 「入り口の数」とか「入口の接しているマス」といった概念を使えば、ポケット状の場所の探索を削減できるのでは？
            //   「同じ向きに連続」かつ「入口が1個」とか
            // * いずれにしても「壁を探索済みか？」を保存するメモリは必要
            // * ある程度の時間が過ぎたら、知っているマスだけを使って移動するなど
            visible[curY, curX] = true;
            var dirs = new Direction[] { sim.DirF, sim.DirL, sim.DirB, sim.DirR };
            for (int k = 0; k < 4; k++)
            {
                int ndir = (dir + k) % 4;
                wall[curY, curX, ndir] = sim.HasWall(dirs[k]);
                int ny = curY + dy[ndir];
                int nx = curX + dx[ndir];
                if (CheckPos(ny, nx))
                {
                    wall[ny, nx, (ndir + 2) % 4] = wall[curY, curX, ndir];

                    // とりあえず、1マス限定で枝刈り処理を入れてみる

                    // 周囲の4マスから既に情報が得られているマスには入らない処理
                    // (3辺が壁に囲まれているマスの探索も枝刈りできる)
                    if (visible[ny, nx] == false)
                    {
                        int n = 0;
                        for (int l = 0; l < 4; l++)
                        {
                            int my = ny + dy[l];
                            int mx = nx + dx[l];
                            if (CheckPos(my, mx) == false || visible[my, mx])
                            {
                                n++;
                            }
                        }
                        if (n == 4)
                        {
                            visible[ny, nx] = true;
                        }
                    }
                }
            }
        }

        // 座標が外に出ていないかチェックする関数
        private bool CheckPos(int y, int x)
        {
            return 0 <= y && y < size && 0 <= x && x < size;
        }

        private bool CheckGoal(int y, int x)
        {
            return sim.maze.GoalY <= y && y < sim.maze.GoalY + sim.maze.GoalH && sim.maze.GoalX <= x && x < sim.maze.GoalX + sim.maze.GoalW;
        }
    }
}
