using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

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

        public string WorkingDirectory
        {
            get
            {
                return textbox_workdir.Text;
            }
        }

        public string LaunchCommand
        {
            get
            {
                return textbox_command.Text;
            }
        }

        private async void btn_exec_Clicked(object sender, EventArgs e)
        {
            if (ctrl.IsMazeReady == false)
            {
                ShowMessage("まだ迷路ファイルを読み込んでいません。");
                return;
            }

            if (WorkingDirectory.Length == 0)
            {
                ShowMessage("作業ディレクトリが入力されていません。");
                return;
            }

            if (LaunchCommand.Length == 0)
            {
                ShowMessage("実行ファイル名が入力されていません。");
                return;
            }

            // http://msdn.microsoft.com/ja-jp/library/jj155759.aspx
            cts = new CancellationTokenSource();

            btn_exec.Enabled = false;
            btn_stop.Enabled = true;

            try
            {
                await ctrl.DebugRun(cts.Token);
                // await ctrl.Run(cts.Token);
            }
            catch (OperationCanceledException)
            {
                // do nothing
            }

            btn_exec.Enabled = true;
            btn_stop.Enabled = false;

            cts = null;
        }

        private void btn_stop_Clicked(object sender, EventArgs e)
        {
            if (ctrl.IsMazeReady == false)
            {
                ShowMessage("まだ迷路ファイルを読み込んでいません。");
                return;
            }

            if (cts != null)
            {
                cts.Cancel();
            }
        }


        private void btn_select_maze_Clicked(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "迷路ファイル(*.txt)|*.txt|すべてのファイル(*.*)|*.*";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string maze_file = dialog.FileName;
                    Update_textbox_maze_file(maze_file);
                    ctrl.LoadMaze();
                }
            }
        }

        public string MazeFile
        {
            get
            {
                return textbox_maze_file.Text;
            }
        }

        private void BeginDraw(Action<Graphics> fn)
        {
            if (picture_maze.Image == null)
            {
                picture_maze.Image = new Bitmap(picture_maze.Width, picture_maze.Height);
            }

            using (var g = Graphics.FromImage(picture_maze.Image))
            {
                fn(g);
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

        public void ShowMessage(string msg)
        {
            MessageBox.Show(this, msg);
        }


        public void Add_listbox_transmit(string msg)
        {
            textbox_transmit.AppendText(msg + Environment.NewLine);
        }

        public void Add_listbox_recieve(string msg)
        {
            textbox_recieve.AppendText(msg + Environment.NewLine);
        }

        public void Add_listbox_siminfo(string msg)
        {
            textbox_siminfo.AppendText(msg + Environment.NewLine);
        }

        public void Add_listbox_agentinfo(string msg)
        {
            textbox_agentinfo.AppendText(msg + Environment.NewLine);
        }

        public void Clear_listbox_transmit()
        {
            textbox_transmit.Clear();
        }

        public void Clear_listbox_recieve()
        {
            textbox_recieve.Clear();
        }

        public void Clear_listbox_siminfo()
        {
            textbox_siminfo.Clear();
        }

        public void Clear_listbox_agentinfo()
        {
            textbox_agentinfo.Clear();
        }
    }
}
