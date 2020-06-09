namespace DCS_Livery_Synchronizer
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lb_savedgamespath = new System.Windows.Forms.Label();
            this.tbPathLiveries = new System.Windows.Forms.TextBox();
            this.bt_FindLiveriesPath = new System.Windows.Forms.Button();
            this.btScanLiveries = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbRepositoryLink = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btScanRepository = new System.Windows.Forms.Button();
            this.clbRepositoryLiveries = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.clbInstalledLiveries = new System.Windows.Forms.CheckedListBox();
            this.btCreateRepository = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tbRepoName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // lb_savedgamespath
            // 
            this.lb_savedgamespath.AutoSize = true;
            this.lb_savedgamespath.Location = new System.Drawing.Point(13, 13);
            this.lb_savedgamespath.Name = "lb_savedgamespath";
            this.lb_savedgamespath.Size = new System.Drawing.Size(135, 13);
            this.lb_savedgamespath.TabIndex = 0;
            this.lb_savedgamespath.Text = "Path to DCS saved games:";
            // 
            // tbPathLiveries
            // 
            this.tbPathLiveries.Location = new System.Drawing.Point(16, 38);
            this.tbPathLiveries.Name = "tbPathLiveries";
            this.tbPathLiveries.Size = new System.Drawing.Size(295, 20);
            this.tbPathLiveries.TabIndex = 1;
            // 
            // bt_FindLiveriesPath
            // 
            this.bt_FindLiveriesPath.Location = new System.Drawing.Point(317, 38);
            this.bt_FindLiveriesPath.Name = "bt_FindLiveriesPath";
            this.bt_FindLiveriesPath.Size = new System.Drawing.Size(25, 20);
            this.bt_FindLiveriesPath.TabIndex = 2;
            this.bt_FindLiveriesPath.Text = "...";
            this.bt_FindLiveriesPath.UseVisualStyleBackColor = true;
            this.bt_FindLiveriesPath.Click += new System.EventHandler(this.bt_FindLiveriesPath_Click);
            // 
            // btScanLiveries
            // 
            this.btScanLiveries.Location = new System.Drawing.Point(12, 73);
            this.btScanLiveries.Name = "btScanLiveries";
            this.btScanLiveries.Size = new System.Drawing.Size(118, 23);
            this.btScanLiveries.TabIndex = 4;
            this.btScanLiveries.Text = "Scan Liveries";
            this.btScanLiveries.UseVisualStyleBackColor = true;
            this.btScanLiveries.Click += new System.EventHandler(this.btScanLiveries_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Currently installed liveries:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(421, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2, 537);
            this.panel1.TabIndex = 6;
            // 
            // tbRepositoryLink
            // 
            this.tbRepositoryLink.Location = new System.Drawing.Point(437, 38);
            this.tbRepositoryLink.Name = "tbRepositoryLink";
            this.tbRepositoryLink.Size = new System.Drawing.Size(295, 20);
            this.tbRepositoryLink.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(437, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Livery Repository:";
            // 
            // btScanRepository
            // 
            this.btScanRepository.Location = new System.Drawing.Point(440, 73);
            this.btScanRepository.Name = "btScanRepository";
            this.btScanRepository.Size = new System.Drawing.Size(98, 23);
            this.btScanRepository.TabIndex = 9;
            this.btScanRepository.Text = "Scan Repository";
            this.btScanRepository.UseVisualStyleBackColor = true;
            // 
            // clbRepositoryLiveries
            // 
            this.clbRepositoryLiveries.FormattingEnabled = true;
            this.clbRepositoryLiveries.Location = new System.Drawing.Point(440, 131);
            this.clbRepositoryLiveries.Name = "clbRepositoryLiveries";
            this.clbRepositoryLiveries.Size = new System.Drawing.Size(292, 289);
            this.clbRepositoryLiveries.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(437, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(173, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Check all liveries you want to install";
            // 
            // clbInstalledLiveries
            // 
            this.clbInstalledLiveries.FormattingEnabled = true;
            this.clbInstalledLiveries.Location = new System.Drawing.Point(16, 131);
            this.clbInstalledLiveries.Name = "clbInstalledLiveries";
            this.clbInstalledLiveries.Size = new System.Drawing.Size(295, 289);
            this.clbInstalledLiveries.TabIndex = 12;
            // 
            // btCreateRepository
            // 
            this.btCreateRepository.Location = new System.Drawing.Point(212, 453);
            this.btCreateRepository.Name = "btCreateRepository";
            this.btCreateRepository.Size = new System.Drawing.Size(99, 23);
            this.btCreateRepository.TabIndex = 13;
            this.btCreateRepository.Text = "create repository";
            this.btCreateRepository.UseVisualStyleBackColor = true;
            this.btCreateRepository.Click += new System.EventHandler(this.btCreateRepository_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(437, 452);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "Install Liveries";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // tbRepoName
            // 
            this.tbRepoName.Location = new System.Drawing.Point(16, 455);
            this.tbRepoName.Name = "tbRepoName";
            this.tbRepoName.Size = new System.Drawing.Size(171, 20);
            this.tbRepoName.TabIndex = 15;
            this.tbRepoName.Text = "My Liverys";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 439);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Name of your Repository:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 490);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(214, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "*All checked liveries will be part of your repo";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 539);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbRepoName);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btCreateRepository);
            this.Controls.Add(this.clbInstalledLiveries);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.clbRepositoryLiveries);
            this.Controls.Add(this.btScanRepository);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbRepositoryLink);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btScanLiveries);
            this.Controls.Add(this.bt_FindLiveriesPath);
            this.Controls.Add(this.tbPathLiveries);
            this.Controls.Add(this.lb_savedgamespath);
            this.Name = "MainWindow";
            this.Text = "DCS Livery Synchronizer";
            this.Shown += new System.EventHandler(this.MainWindow_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_savedgamespath;
        private System.Windows.Forms.TextBox tbPathLiveries;
        private System.Windows.Forms.Button bt_FindLiveriesPath;
        private System.Windows.Forms.Button btScanLiveries;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbRepositoryLink;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btScanRepository;
        private System.Windows.Forms.CheckedListBox clbRepositoryLiveries;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckedListBox clbInstalledLiveries;
        private System.Windows.Forms.Button btCreateRepository;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tbRepoName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

