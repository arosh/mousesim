using System;

namespace MouseSim
{
    class Controller
    {
        private View view;
        private Maze maze;

        public Controller(View view)
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
            view.DrawMaze(maze);
        }
    }
}
