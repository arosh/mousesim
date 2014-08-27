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

        private void MenuItem_Close_Clicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MenuItem_Info_Clicked(object sender, EventArgs e)
        {
            string version = "0.0.1";
            string text = string.Format("MouseSim\nVersion {0}", version);
            MessageBox.Show(this, text, "MouseSim のバージョン情報");
        }

        private void Button_FolderSelect_Clicked(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    string workdir = dialog.SelectedPath;
                    TextBox_WorkDir_Update(workdir);
                }
            }
        }

        private void TextBox_WorkDir_Update(string workdir)
        {
            TextBox_WorkDir.Text = workdir;
        }

        public string WorkingDirectory
        {
            get
            {
                return TextBox_WorkDir.Text;
            }
        }

        public string LaunchCommand
        {
            get
            {
                return TextBox_Command.Text;
            }
        }

        private async void Button_Exec_Clicked(object sender, EventArgs e)
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

            Button_Exec.Enabled = false;
            Button_Stop.Enabled = true;

            try
            {
                await ctrl.DebugRun(cts.Token);
                // await ctrl.Run(cts.Token);
            }
            catch (OperationCanceledException)
            {
                // do nothing
            }

            Button_Exec.Enabled = true;
            Button_Stop.Enabled = false;

            cts = null;
        }

        private void Button_Stop_Clicked(object sender, EventArgs e)
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


        private void Button_SelectMaze_Clicked(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "迷路ファイル(*.txt)|*.txt|すべてのファイル(*.*)|*.*";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string maze_file = dialog.FileName;
                    TextBox_MazeFile_Update(maze_file);
                    ctrl.LoadMaze();
                }
            }
        }

        public string MazeFile
        {
            get
            {
                return TextBox_MazeFile.Text;
            }
        }

        private void BeginDraw(Action<Graphics> fn)
        {
            if (PictureBox_Maze.Image == null)
            {
                PictureBox_Maze.Image = new Bitmap(PictureBox_Maze.Width, PictureBox_Maze.Height);
            }

            using (var g = Graphics.FromImage(PictureBox_Maze.Image))
            {
                fn(g);
            }
        }

        public void PictureBox_Maze_Clear()
        {
            BeginDraw(g =>
            {
                g.Clear(Color.White);
            });
        }

        public void PictureBox_Maze_Invalidate()
        {
            PictureBox_Maze.Invalidate();
        }

        private void TextBox_MazeFile_Update(string fname)
        {
            TextBox_MazeFile.Text = fname;
        }

        public void DrawArrow(int from_x, int from_y, int to_x, int to_y)
        {
            BeginDraw(g =>
            {
                Graphicer.DrawArrow(g, PictureBox_Maze.Size, ctrl.Maze, from_x, from_y, to_x, to_y);
            });
        }

        public void DrawMaze(Maze maze)
        {
            BeginDraw(g =>
            {
                Graphicer.DrawGoal(g, PictureBox_Maze.Size, maze);
                Graphicer.DrawMaze(g, PictureBox_Maze.Size, maze);
            });
        }

        public void DrawAgent(Maze maze, Simulator sim)
        {
            BeginDraw(g =>
            {
                Graphicer.DrawAgent(g, PictureBox_Maze.Size, maze, sim);
            });
        }

        public void DrawVisibility(Maze maze, bool[,] visible)
        {
            BeginDraw(g =>
            {
                Graphicer.DrawVisibility(g, PictureBox_Maze.Size, maze, visible);
            });
        }

        public void ShowMessage(string msg)
        {
            MessageBox.Show(this, msg);
        }

        public void TextBox_Transmit_AppendLine(string msg)
        {
            TextBox_Transmit.AppendText(msg + Environment.NewLine);
        }

        public void TextBox_Recieve_AppendLine(string msg)
        {
            TextBox_Recieve.AppendText(msg + Environment.NewLine);
        }

        public void TextBox_SimInfo_AppendLine(string msg)
        {
            TextBox_SimInfo.AppendText(msg + Environment.NewLine);
        }

        public void TextBox_AgentInfo_AppendLine(string msg)
        {
            TextBox_AgentInfo.AppendText(msg + Environment.NewLine);
        }

        public void TextBox_Transmit_Clear()
        {
            TextBox_Transmit.Clear();
        }

        public void TextBox_Recieve_Clear()
        {
            TextBox_Recieve.Clear();
        }

        public void Textbox_SimInfo_Clear()
        {
            TextBox_SimInfo.Clear();
        }

        public void TextBox_AgentInfo_Clear()
        {
            TextBox_AgentInfo.Clear();
        }
    }
}
