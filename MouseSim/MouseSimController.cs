using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void LoadMaze()
        {
            string maze_file = view.MazeFile;
            maze = MouseMazeReader.Load(maze_file);
            view.DrawMaze(maze);
        }
    }
}
