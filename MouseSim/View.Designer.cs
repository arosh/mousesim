namespace MouseSim
{
    partial class View
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.menustrip = new System.Windows.Forms.MenuStrip();
            this.ファイルFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.終了XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ヘルプHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mouseSimのバージョン情報ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.picture_maze = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textbox_workdir = new System.Windows.Forms.TextBox();
            this.btn_launch_fbd = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textbox_command = new System.Windows.Forms.TextBox();
            this.btn_exec = new System.Windows.Forms.Button();
            this.btn_stop = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textbox_maze_file = new System.Windows.Forms.TextBox();
            this.btn_select_maze = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textbox_transmit = new System.Windows.Forms.TextBox();
            this.textbox_recieve = new System.Windows.Forms.TextBox();
            this.textbox_siminfo = new System.Windows.Forms.TextBox();
            this.menustrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picture_maze)).BeginInit();
            this.SuspendLayout();
            // 
            // menustrip
            // 
            this.menustrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルFToolStripMenuItem,
            this.ヘルプHToolStripMenuItem});
            this.menustrip.Location = new System.Drawing.Point(0, 0);
            this.menustrip.Name = "menustrip";
            this.menustrip.Size = new System.Drawing.Size(749, 24);
            this.menustrip.TabIndex = 1;
            this.menustrip.Text = "menuStrip1";
            // 
            // ファイルFToolStripMenuItem
            // 
            this.ファイルFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.終了XToolStripMenuItem});
            this.ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem";
            this.ファイルFToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.ファイルFToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // 終了XToolStripMenuItem
            // 
            this.終了XToolStripMenuItem.Name = "終了XToolStripMenuItem";
            this.終了XToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.終了XToolStripMenuItem.Text = "終了(&X)";
            this.終了XToolStripMenuItem.Click += new System.EventHandler(this.menuitem_close_Clicked);
            // 
            // ヘルプHToolStripMenuItem
            // 
            this.ヘルプHToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mouseSimのバージョン情報ToolStripMenuItem});
            this.ヘルプHToolStripMenuItem.Name = "ヘルプHToolStripMenuItem";
            this.ヘルプHToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.ヘルプHToolStripMenuItem.Text = "ヘルプ(&H)";
            // 
            // mouseSimのバージョン情報ToolStripMenuItem
            // 
            this.mouseSimのバージョン情報ToolStripMenuItem.Name = "mouseSimのバージョン情報ToolStripMenuItem";
            this.mouseSimのバージョン情報ToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.mouseSimのバージョン情報ToolStripMenuItem.Text = "MouseSim のバージョン情報(&A)";
            this.mouseSimのバージョン情報ToolStripMenuItem.Click += new System.EventHandler(this.menuitem_info_Clicked);
            // 
            // picture_maze
            // 
            this.picture_maze.Location = new System.Drawing.Point(12, 27);
            this.picture_maze.Name = "picture_maze";
            this.picture_maze.Size = new System.Drawing.Size(404, 404);
            this.picture_maze.TabIndex = 2;
            this.picture_maze.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(422, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "作業ディレクトリ";
            // 
            // textbox_workdir
            // 
            this.textbox_workdir.Location = new System.Drawing.Point(503, 58);
            this.textbox_workdir.Name = "textbox_workdir";
            this.textbox_workdir.Size = new System.Drawing.Size(153, 19);
            this.textbox_workdir.TabIndex = 3;
            // 
            // btn_launch_fbd
            // 
            this.btn_launch_fbd.Location = new System.Drawing.Point(662, 56);
            this.btn_launch_fbd.Name = "btn_launch_fbd";
            this.btn_launch_fbd.Size = new System.Drawing.Size(75, 23);
            this.btn_launch_fbd.TabIndex = 4;
            this.btn_launch_fbd.Text = "選択";
            this.btn_launch_fbd.UseVisualStyleBackColor = true;
            this.btn_launch_fbd.Click += new System.EventHandler(this.btn_launch_fbd_Clicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(422, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "実行ファイル名";
            // 
            // textbox_command
            // 
            this.textbox_command.Location = new System.Drawing.Point(503, 85);
            this.textbox_command.Name = "textbox_command";
            this.textbox_command.Size = new System.Drawing.Size(234, 19);
            this.textbox_command.TabIndex = 5;
            // 
            // btn_exec
            // 
            this.btn_exec.Location = new System.Drawing.Point(431, 110);
            this.btn_exec.Name = "btn_exec";
            this.btn_exec.Size = new System.Drawing.Size(150, 59);
            this.btn_exec.TabIndex = 6;
            this.btn_exec.Text = "実行";
            this.btn_exec.UseVisualStyleBackColor = true;
            this.btn_exec.Click += new System.EventHandler(this.btn_exec_Clicked);
            // 
            // btn_stop
            // 
            this.btn_stop.Enabled = false;
            this.btn_stop.Location = new System.Drawing.Point(587, 110);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(150, 59);
            this.btn_stop.TabIndex = 7;
            this.btn_stop.Text = "停止";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Clicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(462, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "シミュレータ → AI";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(618, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "AI → シミュレータ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(434, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "迷路ファイル";
            // 
            // textbox_maze_file
            // 
            this.textbox_maze_file.Location = new System.Drawing.Point(503, 29);
            this.textbox_maze_file.Name = "textbox_maze_file";
            this.textbox_maze_file.Size = new System.Drawing.Size(153, 19);
            this.textbox_maze_file.TabIndex = 1;
            // 
            // btn_select_maze
            // 
            this.btn_select_maze.Location = new System.Drawing.Point(662, 27);
            this.btn_select_maze.Name = "btn_select_maze";
            this.btn_select_maze.Size = new System.Drawing.Size(75, 23);
            this.btn_select_maze.TabIndex = 2;
            this.btn_select_maze.Text = "選択";
            this.btn_select_maze.UseVisualStyleBackColor = true;
            this.btn_select_maze.Click += new System.EventHandler(this.btn_select_maze_Clicked);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(452, 316);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "シミュレータからの情報";
            // 
            // textbox_transmit
            // 
            this.textbox_transmit.Location = new System.Drawing.Point(431, 202);
            this.textbox_transmit.Multiline = true;
            this.textbox_transmit.Name = "textbox_transmit";
            this.textbox_transmit.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textbox_transmit.Size = new System.Drawing.Size(150, 100);
            this.textbox_transmit.TabIndex = 18;
            // 
            // textbox_recieve
            // 
            this.textbox_recieve.Location = new System.Drawing.Point(588, 202);
            this.textbox_recieve.Multiline = true;
            this.textbox_recieve.Name = "textbox_recieve";
            this.textbox_recieve.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textbox_recieve.Size = new System.Drawing.Size(149, 100);
            this.textbox_recieve.TabIndex = 19;
            // 
            // textbox_siminfo
            // 
            this.textbox_siminfo.Location = new System.Drawing.Point(431, 331);
            this.textbox_siminfo.Multiline = true;
            this.textbox_siminfo.Name = "textbox_siminfo";
            this.textbox_siminfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textbox_siminfo.Size = new System.Drawing.Size(150, 100);
            this.textbox_siminfo.TabIndex = 20;
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 443);
            this.Controls.Add(this.textbox_siminfo);
            this.Controls.Add(this.textbox_recieve);
            this.Controls.Add(this.textbox_transmit);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btn_select_maze);
            this.Controls.Add(this.textbox_maze_file);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_stop);
            this.Controls.Add(this.btn_exec);
            this.Controls.Add(this.textbox_command);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_launch_fbd);
            this.Controls.Add(this.textbox_workdir);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picture_maze);
            this.Controls.Add(this.menustrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menustrip;
            this.MaximizeBox = false;
            this.Name = "View";
            this.Text = "MouseSim";
            this.menustrip.ResumeLayout(false);
            this.menustrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picture_maze)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menustrip;
        private System.Windows.Forms.ToolStripMenuItem ファイルFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 終了XToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ヘルプHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mouseSimのバージョン情報ToolStripMenuItem;
        private System.Windows.Forms.PictureBox picture_maze;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textbox_workdir;
        private System.Windows.Forms.Button btn_launch_fbd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textbox_command;
        private System.Windows.Forms.Button btn_exec;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textbox_maze_file;
        private System.Windows.Forms.Button btn_select_maze;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textbox_transmit;
        private System.Windows.Forms.TextBox textbox_recieve;
        private System.Windows.Forms.TextBox textbox_siminfo;
    }
}

