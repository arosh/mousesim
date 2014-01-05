﻿using System;
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

        private void MenuItem_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MenuItem_VersionInfo_Click(object sender, EventArgs e)
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
                RewriteWorkingDirectory(workdir);
            }
        }

        private void RewriteWorkingDirectory(string workdir)
        {
            textbox_workdir.Text = workdir;
        }

        private void btn_exec_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "exec button clicked");
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "stop button clicked");
        }
        

    }
}
