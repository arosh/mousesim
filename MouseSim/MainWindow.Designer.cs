namespace MouseSim
{
    partial class MainWindow
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
            this.pictbox_field = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textbox_workdir = new System.Windows.Forms.TextBox();
            this.btn_launch_fbd = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textbox_command = new System.Windows.Forms.TextBox();
            this.btn_exec = new System.Windows.Forms.Button();
            this.btn_stop = new System.Windows.Forms.Button();
            this.listbox_output = new System.Windows.Forms.ListBox();
            this.listbox_input = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.menustrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictbox_field)).BeginInit();
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
            this.終了XToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_Close_Click);
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
            this.mouseSimのバージョン情報ToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_VersionInfo_Click);
            // 
            // pictbox_field
            // 
            this.pictbox_field.Location = new System.Drawing.Point(12, 27);
            this.pictbox_field.Name = "pictbox_field";
            this.pictbox_field.Size = new System.Drawing.Size(400, 400);
            this.pictbox_field.TabIndex = 2;
            this.pictbox_field.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(419, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "作業ディレクトリ";
            // 
            // textbox_workdir
            // 
            this.textbox_workdir.Location = new System.Drawing.Point(503, 55);
            this.textbox_workdir.Name = "textbox_workdir";
            this.textbox_workdir.Size = new System.Drawing.Size(153, 19);
            this.textbox_workdir.TabIndex = 4;
            // 
            // btn_launch_fbd
            // 
            this.btn_launch_fbd.Location = new System.Drawing.Point(662, 53);
            this.btn_launch_fbd.Name = "btn_launch_fbd";
            this.btn_launch_fbd.Size = new System.Drawing.Size(75, 23);
            this.btn_launch_fbd.TabIndex = 5;
            this.btn_launch_fbd.Text = "選択";
            this.btn_launch_fbd.UseVisualStyleBackColor = true;
            this.btn_launch_fbd.Click += new System.EventHandler(this.btn_launch_fbd_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(433, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "実行コマンド";
            // 
            // textbox_command
            // 
            this.textbox_command.Location = new System.Drawing.Point(503, 80);
            this.textbox_command.Name = "textbox_command";
            this.textbox_command.Size = new System.Drawing.Size(234, 19);
            this.textbox_command.TabIndex = 7;
            // 
            // btn_exec
            // 
            this.btn_exec.Location = new System.Drawing.Point(431, 105);
            this.btn_exec.Name = "btn_exec";
            this.btn_exec.Size = new System.Drawing.Size(150, 59);
            this.btn_exec.TabIndex = 8;
            this.btn_exec.Text = "実行";
            this.btn_exec.UseVisualStyleBackColor = true;
            this.btn_exec.Click += new System.EventHandler(this.btn_exec_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.Location = new System.Drawing.Point(587, 105);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(150, 59);
            this.btn_stop.TabIndex = 9;
            this.btn_stop.Text = "停止";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // listbox_output
            // 
            this.listbox_output.FormattingEnabled = true;
            this.listbox_output.ItemHeight = 12;
            this.listbox_output.Location = new System.Drawing.Point(431, 207);
            this.listbox_output.Name = "listbox_output";
            this.listbox_output.Size = new System.Drawing.Size(150, 220);
            this.listbox_output.TabIndex = 10;
            // 
            // listbox_input
            // 
            this.listbox_input.FormattingEnabled = true;
            this.listbox_input.ItemHeight = 12;
            this.listbox_input.Location = new System.Drawing.Point(587, 207);
            this.listbox_input.Name = "listbox_input";
            this.listbox_input.Size = new System.Drawing.Size(150, 220);
            this.listbox_input.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(462, 192);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "シミュレータ → AI";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(618, 192);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "AI → シミュレータ";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 439);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listbox_input);
            this.Controls.Add(this.listbox_output);
            this.Controls.Add(this.btn_stop);
            this.Controls.Add(this.btn_exec);
            this.Controls.Add(this.textbox_command);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_launch_fbd);
            this.Controls.Add(this.textbox_workdir);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictbox_field);
            this.Controls.Add(this.menustrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menustrip;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "MouseSim";
            this.menustrip.ResumeLayout(false);
            this.menustrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictbox_field)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menustrip;
        private System.Windows.Forms.ToolStripMenuItem ファイルFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 終了XToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ヘルプHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mouseSimのバージョン情報ToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictbox_field;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textbox_workdir;
        private System.Windows.Forms.Button btn_launch_fbd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textbox_command;
        private System.Windows.Forms.Button btn_exec;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.ListBox listbox_output;
        private System.Windows.Forms.ListBox listbox_input;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

