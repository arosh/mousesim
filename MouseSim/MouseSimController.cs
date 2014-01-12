using System;

namespace MouseSim
{
    class MouseSimController
    {
        private MouseSimView view;
        private MouseMaze maze;

        public MouseSimController(MouseSimView view)
        {
            this.view = view;
            this.maze = null;
        }

        public bool IsMazeReady
        {
            get
            {
                return maze != null;
            }
        }

        public MouseMaze Maze
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
            maze = MouseMazeReader.Load(maze_file);
            view.DrawMaze(maze);
        }
    }
}
