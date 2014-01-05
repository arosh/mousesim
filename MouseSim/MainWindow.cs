using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseSim
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void menuitem_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menuitem_VersionInfo_Click(object sender, EventArgs e)
        {
            string version = "0.0.1";
            string text = string.Format("MouseSim\nVersion {0}", version);
            MessageBox.Show(this, text, "MouseSim のバージョン情報");
        }

        private void btn_launch_fbd_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                string workdir = dialog.SelectedPath;
                Update_textbox_workdir(workdir);
            }
        }

        private void Update_textbox_workdir(string workdir)
        {
            textbox_workdir.Text = workdir;
        }

        private void btn_exec_Click(object sender, EventArgs e)
        {

        }

        private void btn_stop_Click(object sender, EventArgs e)
        {

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

        private void btn_select_maze_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "迷路ファイル(*.txt)|*.txt|すべてのファイル(*.*)|*.*";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string maze_file = dialog.FileName;
                Update_textbox_maze_file(maze_file);
                Init_pictbox_field(maze_file);
            }
        }

        private void Init_pictbox_field(string maze_file)
        {
            MouseMaze maze = MouseMazeReader.Load(maze_file);

            int width = pictbox_field.Width / maze.Size;

            Clear_pictbox_field();

            using (var g = GetGraphics())
            {
                g.FillRectangle(Brushes.Black, 0, 0, 4, pictbox_field.Height);
                g.FillRectangle(Brushes.Black, pictbox_field.Width - 4, 0, 4, pictbox_field.Height);
                g.FillRectangle(Brushes.Black, 0, 0, pictbox_field.Width, 4);
                g.FillRectangle(Brushes.Black, 0, pictbox_field.Height - 4, pictbox_field.Width, 4);

                for (int x = 0; x < maze.Size; x++)
                {
                    for (int y = 0; y < maze.Size; y++)
                    {
                        Brush brush;

                        brush = Brushes.White;
                        if (maze.HasWall(x, y, Direction.Top))
                        {
                            brush = Brushes.Black;
                        }

                        g.FillRectangle(brush, 2 + width * x + 2, 2 + width * y, width - 4, 2);

                        brush = Brushes.White;
                        if (maze.HasWall(x, y, Direction.Left))
                        {
                            brush = Brushes.Black;
                        }

                        g.FillRectangle(brush, 2 + width * x, 2 + width * y + 2, 2, width - 4);

                        brush = Brushes.White;
                        if (maze.HasWall(x, y, Direction.Bottom))
                        {
                            brush = Brushes.Black;
                        }

                        g.FillRectangle(brush, 2 + width * x + 2, 2 + width * y + (width - 2), width - 4, 2);


                        brush = Brushes.White;
                        if (maze.HasWall(x, y, Direction.Right))
                        {
                            brush = Brushes.Black;
                        }

                        g.FillRectangle(brush, 2 + width * x + (width - 2), 2 + width * y + 2, 2, width - 4);

                        brush = Brushes.White;
                        g.FillRectangle(brush, width * x, width * y, 4, 4);
                    }
                }

                for (int x = 0; x < maze.Size; x++)
                {
                    for (int y = 0; y < maze.Size; y++)
                    {
                        Brush brush = Brushes.Black;
                        if (maze.HasWall(x, y, Direction.Top) || maze.HasWall(x, y, Direction.Left))
                        {
                            g.FillRectangle(brush, width * x, width * y, 4, 4);
                        }

                        if (maze.HasWall(x, y, Direction.Bottom) || maze.HasWall(x, y, Direction.Right))
                        {
                            g.FillRectangle(brush, width * (x + 1), width * (y + 1), 4, 4);
                        }
                    }
                }
            }

            pictbox_field.Invalidate();
        }

        // もしもこの処理が重かったら、Graphicsをキャッシュする
        private Graphics GetGraphics()
        {
            if (pictbox_field.Image == null)
            {
                pictbox_field.Image = new Bitmap(pictbox_field.Width, pictbox_field.Height);
            }

            return Graphics.FromImage(pictbox_field.Image);
        }

        private void Clear_pictbox_field()
        {
            using (var g = GetGraphics())
            {
                g.FillRectangle(Brushes.White, g.VisibleClipBounds);
            }
        }

        private void Update_textbox_maze_file(string fname)
        {
            textbox_maze_file.Text = fname;
        }


    }
}
