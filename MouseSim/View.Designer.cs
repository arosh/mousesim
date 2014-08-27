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
            this.MenuItem_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.ヘルプHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mouseSimのバージョン情報ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PictureBox_Maze = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TextBox_WorkDir = new System.Windows.Forms.TextBox();
            this.Button_FolderSelect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.TextBox_Command = new System.Windows.Forms.TextBox();
            this.Button_Exec = new System.Windows.Forms.Button();
            this.Button_Stop = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TextBox_MazeFile = new System.Windows.Forms.TextBox();
            this.Button_SelectMaze = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.TextBox_Transmit = new System.Windows.Forms.TextBox();
            this.TextBox_Recieve = new System.Windows.Forms.TextBox();
            this.TextBox_SimInfo = new System.Windows.Forms.TextBox();
            this.TextBox_AgentInfo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.menustrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Maze)).BeginInit();
            this.SuspendLayout();
            // 
            // menustrip
            // 
            this.menustrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルFToolStripMenuItem,
            this.ヘルプHToolStripMenuItem});
            this.menustrip.Location = new System.Drawing.Point(0, 0);
            this.menustrip.Name = "menustrip";
            this.menustrip.Size = new System.Drawing.Size(749, 26);
            this.menustrip.TabIndex = 1;
            this.menustrip.Text = "menuStrip1";
            // 
            // ファイルFToolStripMenuItem
            // 
            this.ファイルFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_Close});
            this.ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem";
            this.ファイルFToolStripMenuItem.Size = new System.Drawing.Size(85, 22);
            this.ファイルFToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // MenuItem_Close
            // 
            this.MenuItem_Close.Name = "MenuItem_Close";
            this.MenuItem_Close.Size = new System.Drawing.Size(118, 22);
            this.MenuItem_Close.Text = "終了(&X)";
            this.MenuItem_Close.Click += new System.EventHandler(this.MenuItem_Close_Clicked);
            // 
            // ヘルプHToolStripMenuItem
            // 
            this.ヘルプHToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mouseSimのバージョン情報ToolStripMenuItem});
            this.ヘルプHToolStripMenuItem.Name = "ヘルプHToolStripMenuItem";
            this.ヘルプHToolStripMenuItem.Size = new System.Drawing.Size(75, 22);
            this.ヘルプHToolStripMenuItem.Text = "ヘルプ(&H)";
            // 
            // mouseSimのバージョン情報ToolStripMenuItem
            // 
            this.mouseSimのバージョン情報ToolStripMenuItem.Name = "mouseSimのバージョン情報ToolStripMenuItem";
            this.mouseSimのバージョン情報ToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.mouseSimのバージョン情報ToolStripMenuItem.Text = "MouseSim のバージョン情報(&A)";
            this.mouseSimのバージョン情報ToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_Info_Clicked);
            // 
            // PictureBox_Maze
            // 
            this.PictureBox_Maze.Location = new System.Drawing.Point(12, 27);
            this.PictureBox_Maze.Name = "PictureBox_Maze";
            this.PictureBox_Maze.Size = new System.Drawing.Size(404, 404);
            this.PictureBox_Maze.TabIndex = 2;
            this.PictureBox_Maze.TabStop = false;
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
            // TextBox_WorkDir
            // 
            this.TextBox_WorkDir.Location = new System.Drawing.Point(503, 58);
            this.TextBox_WorkDir.Name = "TextBox_WorkDir";
            this.TextBox_WorkDir.Size = new System.Drawing.Size(153, 19);
            this.TextBox_WorkDir.TabIndex = 3;
            // 
            // Button_FolderSelect
            // 
            this.Button_FolderSelect.Location = new System.Drawing.Point(662, 56);
            this.Button_FolderSelect.Name = "Button_FolderSelect";
            this.Button_FolderSelect.Size = new System.Drawing.Size(75, 23);
            this.Button_FolderSelect.TabIndex = 4;
            this.Button_FolderSelect.Text = "選択";
            this.Button_FolderSelect.UseVisualStyleBackColor = true;
            this.Button_FolderSelect.Click += new System.EventHandler(this.Button_FolderSelect_Clicked);
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
            // TextBox_Command
            // 
            this.TextBox_Command.Location = new System.Drawing.Point(503, 85);
            this.TextBox_Command.Name = "TextBox_Command";
            this.TextBox_Command.Size = new System.Drawing.Size(234, 19);
            this.TextBox_Command.TabIndex = 5;
            // 
            // Button_Exec
            // 
            this.Button_Exec.Location = new System.Drawing.Point(431, 110);
            this.Button_Exec.Name = "Button_Exec";
            this.Button_Exec.Size = new System.Drawing.Size(150, 59);
            this.Button_Exec.TabIndex = 6;
            this.Button_Exec.Text = "実行";
            this.Button_Exec.UseVisualStyleBackColor = true;
            this.Button_Exec.Click += new System.EventHandler(this.Button_Exec_Clicked);
            // 
            // Button_Stop
            // 
            this.Button_Stop.Enabled = false;
            this.Button_Stop.Location = new System.Drawing.Point(587, 110);
            this.Button_Stop.Name = "Button_Stop";
            this.Button_Stop.Size = new System.Drawing.Size(150, 59);
            this.Button_Stop.TabIndex = 7;
            this.Button_Stop.Text = "停止";
            this.Button_Stop.UseVisualStyleBackColor = true;
            this.Button_Stop.Click += new System.EventHandler(this.Button_Stop_Clicked);
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
            // TextBox_MazeFile
            // 
            this.TextBox_MazeFile.Location = new System.Drawing.Point(503, 29);
            this.TextBox_MazeFile.Name = "TextBox_MazeFile";
            this.TextBox_MazeFile.Size = new System.Drawing.Size(153, 19);
            this.TextBox_MazeFile.TabIndex = 1;
            // 
            // Button_SelectMaze
            // 
            this.Button_SelectMaze.Location = new System.Drawing.Point(662, 27);
            this.Button_SelectMaze.Name = "Button_SelectMaze";
            this.Button_SelectMaze.Size = new System.Drawing.Size(75, 23);
            this.Button_SelectMaze.TabIndex = 2;
            this.Button_SelectMaze.Text = "選択";
            this.Button_SelectMaze.UseVisualStyleBackColor = true;
            this.Button_SelectMaze.Click += new System.EventHandler(this.Button_SelectMaze_Clicked);
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
            // TextBox_Transmit
            // 
            this.TextBox_Transmit.Location = new System.Drawing.Point(431, 202);
            this.TextBox_Transmit.Multiline = true;
            this.TextBox_Transmit.Name = "TextBox_Transmit";
            this.TextBox_Transmit.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBox_Transmit.Size = new System.Drawing.Size(150, 100);
            this.TextBox_Transmit.TabIndex = 18;
            // 
            // TextBox_Recieve
            // 
            this.TextBox_Recieve.Location = new System.Drawing.Point(587, 202);
            this.TextBox_Recieve.Multiline = true;
            this.TextBox_Recieve.Name = "TextBox_Recieve";
            this.TextBox_Recieve.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBox_Recieve.Size = new System.Drawing.Size(150, 100);
            this.TextBox_Recieve.TabIndex = 19;
            // 
            // TextBox_SimInfo
            // 
            this.TextBox_SimInfo.Location = new System.Drawing.Point(431, 331);
            this.TextBox_SimInfo.Multiline = true;
            this.TextBox_SimInfo.Name = "TextBox_SimInfo";
            this.TextBox_SimInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBox_SimInfo.Size = new System.Drawing.Size(150, 100);
            this.TextBox_SimInfo.TabIndex = 20;
            // 
            // TextBox_AgentInfo
            // 
            this.TextBox_AgentInfo.Location = new System.Drawing.Point(587, 331);
            this.TextBox_AgentInfo.Multiline = true;
            this.TextBox_AgentInfo.Name = "TextBox_AgentInfo";
            this.TextBox_AgentInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBox_AgentInfo.Size = new System.Drawing.Size(150, 100);
            this.TextBox_AgentInfo.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(628, 316);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 12);
            this.label7.TabIndex = 22;
            this.label7.Text = "AIからの情報";
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 443);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.TextBox_AgentInfo);
            this.Controls.Add(this.TextBox_SimInfo);
            this.Controls.Add(this.TextBox_Recieve);
            this.Controls.Add(this.TextBox_Transmit);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Button_SelectMaze);
            this.Controls.Add(this.TextBox_MazeFile);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Button_Stop);
            this.Controls.Add(this.Button_Exec);
            this.Controls.Add(this.TextBox_Command);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Button_FolderSelect);
            this.Controls.Add(this.TextBox_WorkDir);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PictureBox_Maze);
            this.Controls.Add(this.menustrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menustrip;
            this.MaximizeBox = false;
            this.Name = "View";
            this.Text = "MouseSim";
            this.menustrip.ResumeLayout(false);
            this.menustrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Maze)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menustrip;
        private System.Windows.Forms.ToolStripMenuItem ファイルFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Close;
        private System.Windows.Forms.ToolStripMenuItem ヘルプHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mouseSimのバージョン情報ToolStripMenuItem;
        private System.Windows.Forms.PictureBox PictureBox_Maze;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextBox_WorkDir;
        private System.Windows.Forms.Button Button_FolderSelect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TextBox_Command;
        private System.Windows.Forms.Button Button_Exec;
        private System.Windows.Forms.Button Button_Stop;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TextBox_MazeFile;
        private System.Windows.Forms.Button Button_SelectMaze;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TextBox_Transmit;
        private System.Windows.Forms.TextBox TextBox_Recieve;
        private System.Windows.Forms.TextBox TextBox_SimInfo;
        private System.Windows.Forms.TextBox TextBox_AgentInfo;
        private System.Windows.Forms.Label label7;
    }
}

