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

        public AgentAction(EAgentActionType type)
            : this(type, 0)
        {

        }
    }

    public class Agent
    {
        private const int kTurnCost = 5;
        private const int kGoCost = 1;

        private Controller ctrl;
        private Simulator sim;

        private int size;

        private int curY, curX;
        private int dir;

        private bool[, ,] wall;
        private bool[,] visible;

        private int[, ,] canMove; // 4ビットに収まる = 2kB
        private AgentAction[, ,] prev;

        public int LoopCount { get; private set; }

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
            this.canMove = new int[size, size, 4];
            this.prev = new AgentAction[size, size, 4];

            Initialize();
        }

        // 同じマップでリスタートに使える処理はこっちに書く
        public void Initialize()
        {
            // =======
            // 座標関係
            // =======
            curY = sim.maze.StartY;
            curX = sim.maze.StartX;

            // 上左下右で反時計回り
            dir = (int)sim.maze.StartDir;

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

        public AgentAction Adachi()
        {
            if (CheckGoal(curY, curX))
            {
                return new AgentAction(EAgentActionType.NO_OPERATION);
            }

            return Explore(/* adachi = */ true);
        }

        public AgentAction Explore(bool adachi = false)
        {
            var d = new int[size, size];
            var queue = new Queue<Tuple<int, int>>();
            var inQueue = new bool[size, size];

            if (adachi == false)
            {
                // すべての未開の地をゴールと見立てて初期化する
                for (int y = 0; y < size; y++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        if (visible[y, x] == false)
                        {
                            d[y, x] = 0;
                            queue.Enqueue(new Tuple<int, int>(y, x));
                            inQueue[y, x] = true;
                        }
                        else
                        {
                            d[y, x] = int.MaxValue;
                            inQueue[y, x] = false;
                        }
                    }
                }
            }
            else
            {
                // ゴール地点をゴールと見立てて初期化する
                for (int y = 0; y < size; y++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        if (CheckGoal(y, x))
                        {
                            d[y, x] = 0;
                            queue.Enqueue(new Tuple<int, int>(y, x));
                            inQueue[y, x] = true;
                        }
                        else
                        {
                            d[y, x] = int.MaxValue;
                            inQueue[y, x] = false;
                        }
                    }
                }
            }

            LoopCount = 0;

            while (queue.Count > 0)
            {
                var front = queue.Dequeue();
                int y = front.Item1, x = front.Item2;
                inQueue[y, x] = false;
                for (int k = 0; k < 4; k++)
                {
                    LoopCount++;
                    if (wall[y, x, k] == false)
                    {
                        int ny = y + dy[k];
                        int nx = x + dx[k];
                        if (d[ny, nx] > d[y, x] + 1)
                        {
                            d[ny, nx] = d[y, x] + 1;
                            if (inQueue[ny, nx] == false)
                            {
                                queue.Enqueue(new Tuple<int, int>(ny, nx));
                                inQueue[ny, nx] = true;
                            }
                        }
                    }
                }
            }

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
                return new AgentAction(EAgentActionType.TURN_LEFT);
            }
            else if (wall[curY, curX, (dir + 3) % 4] == false && d[curY + dy[(dir + 3) % 4], curX + dx[(dir + 3) % 4]] == d[curY, curX] - 1)
            {
                // TurnRight
                dir = (dir + 3) % 4;
                return new AgentAction(EAgentActionType.TURN_RIGHT);
            }
            else if (wall[curY, curX, (dir + 2) % 4] == false && d[curY + dy[(dir + 2) % 4], curX + dx[(dir + 2) % 4]] == d[curY, curX] - 1)
            {
                // TurnLeft
                // 後ろを向くのが最適解の場合は、とりあえず左を向いておいて、次のステップに任せる
                dir = (dir + 1) % 4;
                return new AgentAction(EAgentActionType.TURN_LEFT);
            }
            else
            {
                // どの向きを向いても未開の地に辿りつけない場合 -> 連結でないマスが存在する場合
                return new AgentAction(EAgentActionType.NO_OPERATION);
            }
        }

        /// <summary>
        /// そのマスの上下左右に何マス移動できるか記録する
        /// </summary>
        private void PrepareGraph()
        {
            // いもす法
            int s;

            for (int y = 0; y < size; y++)
            {
                // →
                s = 0;
                canMove[y, 0, 1] = s;
                for (int x = 1; x < size; x++)
                {
                    if (wall[y, x, 1] == false)
                    {
                        s++;
                    }
                    else
                    {
                        s = 0;
                    }
                    canMove[y, x, 1] = s;
                }

                // ←
                s = 0;
                canMove[y, size - 1, 3] = s;
                for (int x = size - 2; x >= 0; x--)
                {
                    if (wall[y, x, 3] == false)
                    {
                        s++;
                    }
                    else
                    {
                        s = 0;
                    }
                    canMove[y, x, 3] = s;
                }

            }

            for (int x = 0; x < size; x++)
            {
                // ↓
                s = 0;
                canMove[0, x, 0] = s;
                for (int y = 1; y < size; y++)
                {
                    if (wall[y, x, 0] == false)
                    {
                        s++;
                    }
                    else
                    {
                        s = 0;
                    }
                    canMove[y, x, 0] = s;
                }

                // ↑
                s = 0;
                canMove[size - 1, x, 2] = s;
                for (int y = size - 2; y >= 0; y--)
                {
                    if (wall[y, x, 2] == false)
                    {
                        s++;
                    }
                    else
                    {
                        s = 0;
                    }
                    canMove[y, x, 2] = s;
                }
            }
        }

        // 最短経路を計算して、配列か何かに保存
        public void ComputeShortestPath(bool toStartArea = false)
        {
            PrepareGraph();

            var d = new int[size, size, 4];
            var vis = new bool[size, size, 4];

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        d[y, x, k] = int.MaxValue;
                        vis[y, x, k] = false;
                    }
                }
            }

            if (toStartArea == false)
            {
                for (int y = sim.maze.GoalY; y < sim.maze.GoalY + sim.maze.GoalH; y++)
                {
                    for (int x = sim.maze.GoalX; x < sim.maze.GoalX + sim.maze.GoalW; x++)
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            d[y, x, k] = 0;
                            prev[y, x, k] = new AgentAction(EAgentActionType.NO_OPERATION);
                        }
                    }
                }
            }
            else
            {
                d[sim.maze.StartY, sim.maze.StartX, (int)sim.maze.StartDir] = 0;
                prev[sim.maze.StartY, sim.maze.StartX, (int)sim.maze.StartDir] = new AgentAction(EAgentActionType.NO_OPERATION);
            }

            // O(V^2)のダイクストラ開始
            while (true)
            {
                int vy = -1, vx = -1, vk = -1;
                for (int y = 0; y < size; y++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            if (vis[y, x, k] == false && ((vy == -1 && vx == -1 && vk == -1) || d[y, x, k] < d[vy, vx, vk]))
                            {
                                vy = y;
                                vx = x;
                                vk = k;
                            }
                        }
                    }
                }

                if ((vy == -1 && vx == -1 && vk == -1) || d[vy, vx, vk] == int.MaxValue)
                {
                    break;
                }

                vis[vy, vx, vk] = true;

                if (d[vy, vx, (vk + 1) % 4] > d[vy, vx, vk] + kTurnCost)
                {
                    d[vy, vx, (vk + 1) % 4] = d[vy, vx, vk] + kTurnCost;
                    prev[vy, vx, (vk + 1) % 4] = new AgentAction(EAgentActionType.TURN_RIGHT);
                }

                if (d[vy, vx, (vk + 3) % 4] > d[vy, vx, vk] + kTurnCost)
                {
                    d[vy, vx, (vk + 3) % 4] = d[vy, vx, vk] + kTurnCost;
                    prev[vy, vx, (vk + 3) % 4] = new AgentAction(EAgentActionType.TURN_LEFT);
                }

                int ny = vy, nx = vx;

                for (int i = 1; i <= canMove[vy, vx, (vk + 2) % 4]; i++)
                {
                    ny += dy[(vk + 2) % 4];
                    nx += dx[(vk + 2) % 4];
                    if (d[ny, nx, vk] > d[vy, vx, vk] + kGoCost)
                    {
                        d[ny, nx, vk] = d[vy, vx, vk] + kGoCost;
                        prev[ny, nx, vk] = new AgentAction(EAgentActionType.GO_FORWARD, i);
                    }
                }
                ;
            }
        }

        public AgentAction GetNextAction()
        {
            var act = prev[curY, curX, dir];

            switch (act.Type)
            {
                case EAgentActionType.GO_FORWARD:
                    curY += dy[dir] * act.Value;
                    curX += dx[dir] * act.Value;
                    break;
                case EAgentActionType.TURN_LEFT:
                    dir = (dir + 1) % 4;
                    break;
                case EAgentActionType.TURN_RIGHT:
                    dir = (dir + 3) % 4;
                    break;
            }

            return act;
        }

        public void LearnWallInfo()
        {
            // 探索済みのマスはセンサ入力しない
            if (visible[curY, curX])
            {
                return;
            }

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

        /// <summary>
        /// 座標が迷路の内側かどうかチェックする関数
        /// </summary>
        /// <param name="y">Y座標</param>
        /// <param name="x">X座標</param>
        /// <returns>座標が迷路内のときtrue, 迷路外のときfalse</returns>

        private bool CheckPos(int y, int x)
        {
            return 0 <= y && y < size && 0 <= x && x < size;
        }

        /// <summary>
        /// 座標がゴールの中かどうかチェックする関数
        /// </summary>
        /// <param name="y">Y座標</param>
        /// <param name="x">X座標</param>
        /// <returns>座標がゴール内のときtrue, ゴール外のときfalse</returns>
        private bool CheckGoal(int y, int x)
        {
            return sim.maze.GoalY <= y && y < sim.maze.GoalY + sim.maze.GoalH && sim.maze.GoalX <= x && x < sim.maze.GoalX + sim.maze.GoalW;
        }
    }
}
