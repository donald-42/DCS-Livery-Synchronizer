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
            this.tbPathLiveries.Size = new System.Drawing.Size(444, 20);
            this.tbPathLiveries.TabIndex = 1;
            // 
            // bt_FindLiveriesPath
            // 
            this.bt_FindLiveriesPath.Location = new System.Drawing.Point(466, 36);
            this.bt_FindLiveriesPath.Name = "bt_FindLiveriesPath";
            this.bt_FindLiveriesPath.Size = new System.Drawing.Size(25, 23);
            this.bt_FindLiveriesPath.TabIndex = 2;
            this.bt_FindLiveriesPath.Text = "...";
            this.bt_FindLiveriesPath.UseVisualStyleBackColor = true;
            this.bt_FindLiveriesPath.Click += new System.EventHandler(this.bt_FindLiveriesPath_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 539);
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
    }
}

