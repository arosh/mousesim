using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseSim
{
    class Agent
    {
        private Controller ctrl;
        private Simulator sim;

        private int size;
        private int[,] d;
        private bool[,] vis;
        private bool[, ,] wall;
        private bool[,] open;
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
            this.d = new int[size, size];
            this.vis = new bool[size, size];
            this.wall = new bool[size, size, 4];

            Initialize();
        }

        // 同じマップでリスタートに使える処理はこっちに書く
        public void Initialize() {
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
            curY = sim.maze.StartY;
            curX = sim.maze.StartX;

            // 上左下右で反時計回り
            dir = (int)sim.maze.StartDir;
        }

        // 座標が外に出ていないかチェックする関数
        private bool CheckPos(int y, int x) {
            return 0 <= y && y < size && 0 <= x && x < size;
        }

        private bool CheckGoal(int y, int x)
        {
            return sim.maze.GoalY <= y && y < sim.maze.GoalY + sim.maze.GoalH && sim.maze.GoalX <= x && x < sim.maze.GoalX + sim.maze.GoalW;
        }
    }
}
