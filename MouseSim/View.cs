using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace MouseSim
{
    public partial class View : Form
    {
        private Controller ctrl;
        private CancellationTokenSource cts;

        public View()
        {
            InitializeComponent();
            ctrl = new Controller(this);
            cts = null;
        }

        private void menuitem_close_Clicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menuitem_info_Clicked(object sender, EventArgs e)
        {
            string version = "0.0.1";
            string text = string.Format("MouseSim\nVersion {0}", version);
            MessageBox.Show(this, text, "MouseSim のバージョン情報");
        }

        private void btn_launch_fbd_Clicked(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    string workdir = dialog.SelectedPath;
                    Update_textbox_workdir(workdir);
                }
            }
        }

        private void Update_textbox_workdir(string workdir)
        {
            textbox_workdir.Text = workdir;
        }

        private async void btn_exec_Clicked(object sender, EventArgs e)
        {
            if (ctrl.IsMazeReady == false)
            {
                MessageBox.Show(this, "まだ迷路ファイルを読み込んでいません、");
                return;
            }

            // http://msdn.microsoft.com/ja-jp/library/jj155759.aspx
            cts = new CancellationTokenSource();

            try
            {
                await ctrl.DebugRun(cts.Token);
            }
            catch (OperationCanceledException)
            {
                // do nothing
            }

            cts = null;
        }

        private void btn_stop_Clicked(object sender, EventArgs e)
        {
            if (ctrl.IsMazeReady == false)
            {
                MessageBox.Show(this, "まだ迷路ファイルを読み込んでいません、");
                return;
            }

            if (cts != null)
            {
                cts.Cancel();
            }
        }

        private void Add_listbox_output(string msg)
        {
            listbox_output.Items.Add(msg);
        }

        private void Add_listbox_input(string msg)
        {
            listbox_input.Items.Add(msg);
        }

        private void Add_listbox_output(IEnumerable<string> msgs)
        {
            listbox_output.Items.AddRange(msgs.ToArray());
        }

        private void Add_listbox_input(IEnumerable<string> msgs)
        {
            listbox_input.Items.AddRange(msgs.ToArray());
        }

        private void Clear_listbox_output()
        {
            listbox_output.Items.Clear();
        }

        private void Clear_listbox_input()
        {
            listbox_input.Items.Clear();
        }

        private void btn_select_maze_Clicked(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "迷路ファイル(*.txt)|*.txt|すべてのファイル(*.*)|*.*";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string maze_file = dialog.FileName;
                Update_textbox_maze_file(maze_file);
                ctrl.LoadMaze();
            }
        }

        public string MazeFile
        {
            get
            {
                return textbox_maze_file.Text;
            }
        }

        private void BeginDraw(Action<Graphics> f)
        {
            if (picture_maze.Image == null)
            {
                picture_maze.Image = new Bitmap(picture_maze.Width, picture_maze.Height);
            }

            using (var g = Graphics.FromImage(picture_maze.Image))
            {
                f(g);
            }
        }

        private void Clear_picture_maze()
        {
            BeginDraw(g =>
            {
                g.Clear(Color.White);
            });

            picture_maze.Invalidate();
        }

        private void Update_textbox_maze_file(string fname)
        {
            textbox_maze_file.Text = fname;
        }

        public void DrawArrow(int from_x, int from_y, int to_x, int to_y)
        {
            BeginDraw(g =>
            {
                Graphicer.DrawArrow(g, picture_maze.Size, ctrl.Maze, from_x, from_y, to_x, to_y);
            });

            picture_maze.Invalidate();
        }

        public void DrawMaze(Maze maze)
        {
            Clear_picture_maze();

            BeginDraw(g =>
            {
                Graphicer.DrawGoal(g, picture_maze.Size, maze);
                Graphicer.DrawMaze(g, picture_maze.Size, maze);
            });

            picture_maze.Invalidate();
        }

        public void DrawAgent(Maze maze, Simulator sim)
        {
            BeginDraw(g =>
            {
                Graphicer.DrawAgent(g, picture_maze.Size, maze, sim);
            });

            picture_maze.Invalidate();
        }
    }
}
