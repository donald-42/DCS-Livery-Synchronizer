namespace DCS_Livery_Synchronizer
{
    partial class MainForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tPInstall = new System.Windows.Forms.TabPage();
            this.gvInstallRepo = new System.Windows.Forms.DataGridView();
            this.btSubmitRepoURL = new System.Windows.Forms.Button();
            this.lbOnlineRepoURL = new System.Windows.Forms.Label();
            this.tBRepoURL = new System.Windows.Forms.TextBox();
            this.tPLocalLiverys = new System.Windows.Forms.TabPage();
            this.tPOnlineRepo = new System.Windows.Forms.TabPage();
            this.tPSettings = new System.Windows.Forms.TabPage();
            this.btInstallSelected = new System.Windows.Forms.Button();
            this.lbSettingsTitle = new System.Windows.Forms.Label();
            this.lbPathToDCSSavegames = new System.Windows.Forms.Label();
            this.tbPathToDCSSavegame = new System.Windows.Forms.TextBox();
            this.btFindSavegamesPath = new System.Windows.Forms.Button();
            this.pbInstallProgress = new System.Windows.Forms.ProgressBar();
            this.CCheckbox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CAircraft = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnInitLocalLiveries = new System.Windows.Forms.Panel();
            this.lbInitializingLocalLiveries = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbNotImplementedOnlineManager = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gvLocalLiveries = new System.Windows.Forms.DataGridView();
            this.btCreateRepository = new System.Windows.Forms.Button();
            this.btRefreshLocalRepository = new System.Windows.Forms.Button();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pbCreateRepo = new System.Windows.Forms.ProgressBar();
            this.tbRepoName = new System.Windows.Forms.TextBox();
            this.lbMyRepoName = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tPInstall.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvInstallRepo)).BeginInit();
            this.tPLocalLiverys.SuspendLayout();
            this.tPOnlineRepo.SuspendLayout();
            this.tPSettings.SuspendLayout();
            this.pnInitLocalLiveries.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvLocalLiveries)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tPInstall);
            this.tabControl.Controls.Add(this.tPLocalLiverys);
            this.tabControl.Controls.Add(this.tPOnlineRepo);
            this.tabControl.Controls.Add(this.tPSettings);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1107, 642);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tPInstall
            // 
            this.tPInstall.Controls.Add(this.pbInstallProgress);
            this.tPInstall.Controls.Add(this.btInstallSelected);
            this.tPInstall.Controls.Add(this.gvInstallRepo);
            this.tPInstall.Controls.Add(this.btSubmitRepoURL);
            this.tPInstall.Controls.Add(this.lbOnlineRepoURL);
            this.tPInstall.Controls.Add(this.tBRepoURL);
            this.tPInstall.Location = new System.Drawing.Point(4, 25);
            this.tPInstall.Name = "tPInstall";
            this.tPInstall.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tPInstall.Size = new System.Drawing.Size(1099, 613);
            this.tPInstall.TabIndex = 0;
            this.tPInstall.Text = "Install Liverys";
            this.tPInstall.UseVisualStyleBackColor = true;
            // 
            // gvInstallRepo
            // 
            this.gvInstallRepo.AllowUserToAddRows = false;
            this.gvInstallRepo.AllowUserToDeleteRows = false;
            this.gvInstallRepo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvInstallRepo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CCheckbox,
            this.CAircraft,
            this.CName,
            this.CStatus});
            this.gvInstallRepo.Location = new System.Drawing.Point(11, 94);
            this.gvInstallRepo.Name = "gvInstallRepo";
            this.gvInstallRepo.RowTemplate.Height = 24;
            this.gvInstallRepo.Size = new System.Drawing.Size(1008, 469);
            this.gvInstallRepo.TabIndex = 3;
            // 
            // btSubmitRepoURL
            // 
            this.btSubmitRepoURL.Location = new System.Drawing.Point(340, 45);
            this.btSubmitRepoURL.Name = "btSubmitRepoURL";
            this.btSubmitRepoURL.Size = new System.Drawing.Size(75, 23);
            this.btSubmitRepoURL.TabIndex = 2;
            this.btSubmitRepoURL.Text = "Search";
            this.btSubmitRepoURL.UseVisualStyleBackColor = true;
            this.btSubmitRepoURL.Click += new System.EventHandler(this.btSubmitRepoURL_Click);
            // 
            // lbOnlineRepoURL
            // 
            this.lbOnlineRepoURL.AutoSize = true;
            this.lbOnlineRepoURL.Location = new System.Drawing.Point(8, 26);
            this.lbOnlineRepoURL.Name = "lbOnlineRepoURL";
            this.lbOnlineRepoURL.Size = new System.Drawing.Size(157, 17);
            this.lbOnlineRepoURL.TabIndex = 1;
            this.lbOnlineRepoURL.Text = "Online Repository URL:";
            // 
            // tBRepoURL
            // 
            this.tBRepoURL.Location = new System.Drawing.Point(8, 46);
            this.tBRepoURL.Name = "tBRepoURL";
            this.tBRepoURL.Size = new System.Drawing.Size(326, 22);
            this.tBRepoURL.TabIndex = 0;
            // 
            // tPLocalLiverys
            // 
            this.tPLocalLiverys.Controls.Add(this.lbMyRepoName);
            this.tPLocalLiverys.Controls.Add(this.tbRepoName);
            this.tPLocalLiverys.Controls.Add(this.pbCreateRepo);
            this.tPLocalLiverys.Controls.Add(this.btRefreshLocalRepository);
            this.tPLocalLiverys.Controls.Add(this.btCreateRepository);
            this.tPLocalLiverys.Controls.Add(this.gvLocalLiveries);
            this.tPLocalLiverys.Location = new System.Drawing.Point(4, 25);
            this.tPLocalLiverys.Name = "tPLocalLiverys";
            this.tPLocalLiverys.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tPLocalLiverys.Size = new System.Drawing.Size(1099, 613);
            this.tPLocalLiverys.TabIndex = 1;
            this.tPLocalLiverys.Text = "Create Repository";
            this.tPLocalLiverys.UseVisualStyleBackColor = true;
            // 
            // tPOnlineRepo
            // 
            this.tPOnlineRepo.Controls.Add(this.label2);
            this.tPOnlineRepo.Controls.Add(this.lbNotImplementedOnlineManager);
            this.tPOnlineRepo.Location = new System.Drawing.Point(4, 25);
            this.tPOnlineRepo.Name = "tPOnlineRepo";
            this.tPOnlineRepo.Size = new System.Drawing.Size(1099, 613);
            this.tPOnlineRepo.TabIndex = 2;
            this.tPOnlineRepo.Text = "Online Repository";
            this.tPOnlineRepo.UseVisualStyleBackColor = true;
            // 
            // tPSettings
            // 
            this.tPSettings.Controls.Add(this.btFindSavegamesPath);
            this.tPSettings.Controls.Add(this.tbPathToDCSSavegame);
            this.tPSettings.Controls.Add(this.lbPathToDCSSavegames);
            this.tPSettings.Controls.Add(this.lbSettingsTitle);
            this.tPSettings.Location = new System.Drawing.Point(4, 25);
            this.tPSettings.Name = "tPSettings";
            this.tPSettings.Size = new System.Drawing.Size(1099, 613);
            this.tPSettings.TabIndex = 3;
            this.tPSettings.Text = "Settings";
            this.tPSettings.UseVisualStyleBackColor = true;
            // 
            // btInstallSelected
            // 
            this.btInstallSelected.BackColor = System.Drawing.Color.LightGray;
            this.btInstallSelected.Enabled = false;
            this.btInstallSelected.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btInstallSelected.Location = new System.Drawing.Point(832, 36);
            this.btInstallSelected.Name = "btInstallSelected";
            this.btInstallSelected.Size = new System.Drawing.Size(187, 32);
            this.btInstallSelected.TabIndex = 4;
            this.btInstallSelected.Text = "Install selected";
            this.btInstallSelected.UseVisualStyleBackColor = false;
            this.btInstallSelected.Click += new System.EventHandler(this.btInstallSelected_Click);
            // 
            // lbSettingsTitle
            // 
            this.lbSettingsTitle.AutoSize = true;
            this.lbSettingsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSettingsTitle.Location = new System.Drawing.Point(8, 11);
            this.lbSettingsTitle.Name = "lbSettingsTitle";
            this.lbSettingsTitle.Size = new System.Drawing.Size(113, 31);
            this.lbSettingsTitle.TabIndex = 0;
            this.lbSettingsTitle.Text = "Settings";
            // 
            // lbPathToDCSSavegames
            // 
            this.lbPathToDCSSavegames.AutoSize = true;
            this.lbPathToDCSSavegames.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPathToDCSSavegames.Location = new System.Drawing.Point(11, 55);
            this.lbPathToDCSSavegames.Name = "lbPathToDCSSavegames";
            this.lbPathToDCSSavegames.Size = new System.Drawing.Size(238, 20);
            this.lbPathToDCSSavegames.TabIndex = 1;
            this.lbPathToDCSSavegames.Text = "Path to DCS Savegame folder:";
            // 
            // tbPathToDCSSavegame
            // 
            this.tbPathToDCSSavegame.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPathToDCSSavegame.Location = new System.Drawing.Point(15, 78);
            this.tbPathToDCSSavegame.Name = "tbPathToDCSSavegame";
            this.tbPathToDCSSavegame.Size = new System.Drawing.Size(437, 26);
            this.tbPathToDCSSavegame.TabIndex = 2;
            // 
            // btFindSavegamesPath
            // 
            this.btFindSavegamesPath.Location = new System.Drawing.Point(458, 78);
            this.btFindSavegamesPath.Name = "btFindSavegamesPath";
            this.btFindSavegamesPath.Size = new System.Drawing.Size(41, 26);
            this.btFindSavegamesPath.TabIndex = 3;
            this.btFindSavegamesPath.Text = "...";
            this.btFindSavegamesPath.UseVisualStyleBackColor = true;
            this.btFindSavegamesPath.Click += new System.EventHandler(this.btFindSavegamesPath_Click);
            // 
            // pbInstallProgress
            // 
            this.pbInstallProgress.Location = new System.Drawing.Point(11, 569);
            this.pbInstallProgress.Name = "pbInstallProgress";
            this.pbInstallProgress.Size = new System.Drawing.Size(1008, 36);
            this.pbInstallProgress.TabIndex = 5;
            // 
            // CCheckbox
            // 
            this.CCheckbox.HeaderText = "Select";
            this.CCheckbox.Name = "CCheckbox";
            this.CCheckbox.Width = 60;
            // 
            // CAircraft
            // 
            this.CAircraft.HeaderText = "Aircraft";
            this.CAircraft.Name = "CAircraft";
            this.CAircraft.ReadOnly = true;
            this.CAircraft.Width = 120;
            // 
            // CName
            // 
            this.CName.HeaderText = "Name";
            this.CName.Name = "CName";
            this.CName.ReadOnly = true;
            this.CName.Width = 150;
            // 
            // CStatus
            // 
            this.CStatus.HeaderText = "Status";
            this.CStatus.Name = "CStatus";
            this.CStatus.ReadOnly = true;
            this.CStatus.Width = 150;
            // 
            // pnInitLocalLiveries
            // 
            this.pnInitLocalLiveries.Controls.Add(this.label1);
            this.pnInitLocalLiveries.Controls.Add(this.lbInitializingLocalLiveries);
            this.pnInitLocalLiveries.Location = new System.Drawing.Point(353, 187);
            this.pnInitLocalLiveries.Name = "pnInitLocalLiveries";
            this.pnInitLocalLiveries.Size = new System.Drawing.Size(393, 199);
            this.pnInitLocalLiveries.TabIndex = 1;
            // 
            // lbInitializingLocalLiveries
            // 
            this.lbInitializingLocalLiveries.AutoSize = true;
            this.lbInitializingLocalLiveries.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbInitializingLocalLiveries.Location = new System.Drawing.Point(53, 55);
            this.lbInitializingLocalLiveries.Name = "lbInitializingLocalLiveries";
            this.lbInitializingLocalLiveries.Size = new System.Drawing.Size(265, 29);
            this.lbInitializingLocalLiveries.TabIndex = 1;
            this.lbInitializingLocalLiveries.Text = "Initializing local liveries.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(61, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "this may take a while....";
            // 
            // lbNotImplementedOnlineManager
            // 
            this.lbNotImplementedOnlineManager.AutoSize = true;
            this.lbNotImplementedOnlineManager.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNotImplementedOnlineManager.Location = new System.Drawing.Point(384, 104);
            this.lbNotImplementedOnlineManager.Name = "lbNotImplementedOnlineManager";
            this.lbNotImplementedOnlineManager.Size = new System.Drawing.Size(355, 39);
            this.lbNotImplementedOnlineManager.TabIndex = 0;
            this.lbNotImplementedOnlineManager.Text = "NOT IMPLEMENTED!";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(267, 198);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(669, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "This tab will alow you to manage your online repository. Not yet implemented";
            // 
            // gvLocalLiveries
            // 
            this.gvLocalLiveries.AllowUserToAddRows = false;
            this.gvLocalLiveries.AllowUserToDeleteRows = false;
            this.gvLocalLiveries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvLocalLiveries.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.Path});
            this.gvLocalLiveries.Location = new System.Drawing.Point(8, 71);
            this.gvLocalLiveries.Name = "gvLocalLiveries";
            this.gvLocalLiveries.RowTemplate.Height = 24;
            this.gvLocalLiveries.Size = new System.Drawing.Size(1014, 469);
            this.gvLocalLiveries.TabIndex = 4;
            // 
            // btCreateRepository
            // 
            this.btCreateRepository.BackColor = System.Drawing.Color.LightGray;
            this.btCreateRepository.Enabled = false;
            this.btCreateRepository.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btCreateRepository.Location = new System.Drawing.Point(815, 33);
            this.btCreateRepository.Name = "btCreateRepository";
            this.btCreateRepository.Size = new System.Drawing.Size(207, 32);
            this.btCreateRepository.TabIndex = 5;
            this.btCreateRepository.Text = "Create Repository (selected)";
            this.btCreateRepository.UseVisualStyleBackColor = false;
            this.btCreateRepository.Click += new System.EventHandler(this.btCreateRepository_Click);
            // 
            // btRefreshLocalRepository
            // 
            this.btRefreshLocalRepository.BackColor = System.Drawing.Color.LightGray;
            this.btRefreshLocalRepository.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btRefreshLocalRepository.Location = new System.Drawing.Point(8, 33);
            this.btRefreshLocalRepository.Name = "btRefreshLocalRepository";
            this.btRefreshLocalRepository.Size = new System.Drawing.Size(94, 32);
            this.btRefreshLocalRepository.TabIndex = 6;
            this.btRefreshLocalRepository.Text = "Refresh";
            this.btRefreshLocalRepository.UseVisualStyleBackColor = false;
            this.btRefreshLocalRepository.Click += new System.EventHandler(this.btRefreshLocalRepository_Click);
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.HeaderText = "Select";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Width = 60;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Aircraft";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 120;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Name";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 150;
            // 
            // Path
            // 
            this.Path.HeaderText = "Path";
            this.Path.Name = "Path";
            this.Path.ReadOnly = true;
            this.Path.Width = 620;
            // 
            // pbCreateRepo
            // 
            this.pbCreateRepo.Location = new System.Drawing.Point(8, 560);
            this.pbCreateRepo.Name = "pbCreateRepo";
            this.pbCreateRepo.Size = new System.Drawing.Size(1014, 36);
            this.pbCreateRepo.TabIndex = 7;
            // 
            // tbRepoName
            // 
            this.tbRepoName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRepoName.Location = new System.Drawing.Point(634, 35);
            this.tbRepoName.Name = "tbRepoName";
            this.tbRepoName.Size = new System.Drawing.Size(161, 30);
            this.tbRepoName.TabIndex = 8;
            this.tbRepoName.Text = "MyRepository";
            // 
            // lbMyRepoName
            // 
            this.lbMyRepoName.AutoSize = true;
            this.lbMyRepoName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMyRepoName.Location = new System.Drawing.Point(437, 38);
            this.lbMyRepoName.Name = "lbMyRepoName";
            this.lbMyRepoName.Size = new System.Drawing.Size(167, 25);
            this.lbMyRepoName.TabIndex = 9;
            this.lbMyRepoName.Text = "Repository Name:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1154, 642);
            this.Controls.Add(this.pnInitLocalLiveries);
            this.Controls.Add(this.tabControl);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.tabControl.ResumeLayout(false);
            this.tPInstall.ResumeLayout(false);
            this.tPInstall.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvInstallRepo)).EndInit();
            this.tPLocalLiverys.ResumeLayout(false);
            this.tPLocalLiverys.PerformLayout();
            this.tPOnlineRepo.ResumeLayout(false);
            this.tPOnlineRepo.PerformLayout();
            this.tPSettings.ResumeLayout(false);
            this.tPSettings.PerformLayout();
            this.pnInitLocalLiveries.ResumeLayout(false);
            this.pnInitLocalLiveries.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvLocalLiveries)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tPInstall;
        private System.Windows.Forms.Button btSubmitRepoURL;
        private System.Windows.Forms.Label lbOnlineRepoURL;
        private System.Windows.Forms.TextBox tBRepoURL;
        private System.Windows.Forms.TabPage tPLocalLiverys;
        private System.Windows.Forms.TabPage tPOnlineRepo;
        private System.Windows.Forms.TabPage tPSettings;
        private System.Windows.Forms.DataGridView gvInstallRepo;
        private System.Windows.Forms.Button btInstallSelected;
        private System.Windows.Forms.Label lbPathToDCSSavegames;
        private System.Windows.Forms.Label lbSettingsTitle;
        private System.Windows.Forms.TextBox tbPathToDCSSavegame;
        private System.Windows.Forms.Button btFindSavegamesPath;
        private System.Windows.Forms.ProgressBar pbInstallProgress;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CCheckbox;
        private System.Windows.Forms.DataGridViewTextBoxColumn CAircraft;
        private System.Windows.Forms.DataGridViewTextBoxColumn CName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CStatus;
        private System.Windows.Forms.Panel pnInitLocalLiveries;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbInitializingLocalLiveries;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbNotImplementedOnlineManager;
        private System.Windows.Forms.Button btRefreshLocalRepository;
        private System.Windows.Forms.Button btCreateRepository;
        private System.Windows.Forms.DataGridView gvLocalLiveries;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Path;
        private System.Windows.Forms.ProgressBar pbCreateRepo;
        private System.Windows.Forms.Label lbMyRepoName;
        private System.Windows.Forms.TextBox tbRepoName;
    }
}