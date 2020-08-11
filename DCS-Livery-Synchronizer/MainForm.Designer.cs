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
            this.pbInstallProgress = new System.Windows.Forms.ProgressBar();
            this.btInstallSelected = new System.Windows.Forms.Button();
            this.gvInstallRepo = new System.Windows.Forms.DataGridView();
            this.btSubmitRepoURL = new System.Windows.Forms.Button();
            this.lbOnlineRepoURL = new System.Windows.Forms.Label();
            this.tBRepoURL = new System.Windows.Forms.TextBox();
            this.tPLocalLiverys = new System.Windows.Forms.TabPage();
            this.lbMyRepoName = new System.Windows.Forms.Label();
            this.tbRepoName = new System.Windows.Forms.TextBox();
            this.pbCreateRepo = new System.Windows.Forms.ProgressBar();
            this.btRefreshLocalRepository = new System.Windows.Forms.Button();
            this.btCreateRepository = new System.Windows.Forms.Button();
            this.gvLocalLiveries = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tPOnlineRepo = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.lbNotImplementedOnlineManager = new System.Windows.Forms.Label();
            this.tPSettings = new System.Windows.Forms.TabPage();
            this.btFindSavegamesPath = new System.Windows.Forms.Button();
            this.tbPathToDCSSavegame = new System.Windows.Forms.TextBox();
            this.lbPathToDCSSavegames = new System.Windows.Forms.Label();
            this.lbSettingsTitle = new System.Windows.Forms.Label();
            this.pnInitLocalLiveries = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lbInitializingLocalLiveries = new System.Windows.Forms.Label();
            this.CCheckbox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CAircraft = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl.SuspendLayout();
            this.tPInstall.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvInstallRepo)).BeginInit();
            this.tPLocalLiverys.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvLocalLiveries)).BeginInit();
            this.tPOnlineRepo.SuspendLayout();
            this.tPSettings.SuspendLayout();
            this.pnInitLocalLiveries.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tPInstall);
            this.tabControl.Controls.Add(this.tPLocalLiverys);
            this.tabControl.Controls.Add(this.tPOnlineRepo);
            this.tabControl.Controls.Add(this.tPSettings);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(830, 522);
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
            this.tPInstall.Location = new System.Drawing.Point(4, 22);
            this.tPInstall.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tPInstall.Name = "tPInstall";
            this.tPInstall.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tPInstall.Size = new System.Drawing.Size(822, 496);
            this.tPInstall.TabIndex = 0;
            this.tPInstall.Text = "Install Liverys";
            this.tPInstall.UseVisualStyleBackColor = true;
            // 
            // pbInstallProgress
            // 
            this.pbInstallProgress.Location = new System.Drawing.Point(8, 462);
            this.pbInstallProgress.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pbInstallProgress.Name = "pbInstallProgress";
            this.pbInstallProgress.Size = new System.Drawing.Size(756, 29);
            this.pbInstallProgress.TabIndex = 5;
            // 
            // btInstallSelected
            // 
            this.btInstallSelected.BackColor = System.Drawing.Color.LightGray;
            this.btInstallSelected.Enabled = false;
            this.btInstallSelected.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btInstallSelected.Location = new System.Drawing.Point(624, 29);
            this.btInstallSelected.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btInstallSelected.Name = "btInstallSelected";
            this.btInstallSelected.Size = new System.Drawing.Size(140, 26);
            this.btInstallSelected.TabIndex = 4;
            this.btInstallSelected.Text = "Install selected";
            this.btInstallSelected.UseVisualStyleBackColor = false;
            this.btInstallSelected.Click += new System.EventHandler(this.btInstallSelected_Click);
            // 
            // gvInstallRepo
            // 
            this.gvInstallRepo.AllowUserToAddRows = false;
            this.gvInstallRepo.AllowUserToDeleteRows = false;
            this.gvInstallRepo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvInstallRepo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CCheckbox,
            this.Category,
            this.CAircraft,
            this.CName,
            this.CStatus});
            this.gvInstallRepo.Location = new System.Drawing.Point(8, 76);
            this.gvInstallRepo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gvInstallRepo.Name = "gvInstallRepo";
            this.gvInstallRepo.RowTemplate.Height = 24;
            this.gvInstallRepo.Size = new System.Drawing.Size(756, 381);
            this.gvInstallRepo.TabIndex = 3;
            // 
            // btSubmitRepoURL
            // 
            this.btSubmitRepoURL.Location = new System.Drawing.Point(255, 37);
            this.btSubmitRepoURL.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btSubmitRepoURL.Name = "btSubmitRepoURL";
            this.btSubmitRepoURL.Size = new System.Drawing.Size(56, 19);
            this.btSubmitRepoURL.TabIndex = 2;
            this.btSubmitRepoURL.Text = "Search";
            this.btSubmitRepoURL.UseVisualStyleBackColor = true;
            this.btSubmitRepoURL.Click += new System.EventHandler(this.btSubmitRepoURL_Click);
            // 
            // lbOnlineRepoURL
            // 
            this.lbOnlineRepoURL.AutoSize = true;
            this.lbOnlineRepoURL.Location = new System.Drawing.Point(6, 21);
            this.lbOnlineRepoURL.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbOnlineRepoURL.Name = "lbOnlineRepoURL";
            this.lbOnlineRepoURL.Size = new System.Drawing.Size(118, 13);
            this.lbOnlineRepoURL.TabIndex = 1;
            this.lbOnlineRepoURL.Text = "Online Repository URL:";
            // 
            // tBRepoURL
            // 
            this.tBRepoURL.Location = new System.Drawing.Point(6, 37);
            this.tBRepoURL.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tBRepoURL.Name = "tBRepoURL";
            this.tBRepoURL.Size = new System.Drawing.Size(246, 20);
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
            this.tPLocalLiverys.Location = new System.Drawing.Point(4, 22);
            this.tPLocalLiverys.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tPLocalLiverys.Name = "tPLocalLiverys";
            this.tPLocalLiverys.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tPLocalLiverys.Size = new System.Drawing.Size(822, 496);
            this.tPLocalLiverys.TabIndex = 1;
            this.tPLocalLiverys.Text = "Create Repository";
            this.tPLocalLiverys.UseVisualStyleBackColor = true;
            // 
            // lbMyRepoName
            // 
            this.lbMyRepoName.AutoSize = true;
            this.lbMyRepoName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMyRepoName.Location = new System.Drawing.Point(328, 31);
            this.lbMyRepoName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbMyRepoName.Name = "lbMyRepoName";
            this.lbMyRepoName.Size = new System.Drawing.Size(135, 20);
            this.lbMyRepoName.TabIndex = 9;
            this.lbMyRepoName.Text = "Repository Name:";
            // 
            // tbRepoName
            // 
            this.tbRepoName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRepoName.Location = new System.Drawing.Point(476, 28);
            this.tbRepoName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbRepoName.Name = "tbRepoName";
            this.tbRepoName.Size = new System.Drawing.Size(122, 26);
            this.tbRepoName.TabIndex = 8;
            this.tbRepoName.Text = "MyRepository";
            // 
            // pbCreateRepo
            // 
            this.pbCreateRepo.Location = new System.Drawing.Point(6, 455);
            this.pbCreateRepo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pbCreateRepo.Name = "pbCreateRepo";
            this.pbCreateRepo.Size = new System.Drawing.Size(760, 29);
            this.pbCreateRepo.TabIndex = 7;
            // 
            // btRefreshLocalRepository
            // 
            this.btRefreshLocalRepository.BackColor = System.Drawing.Color.LightGray;
            this.btRefreshLocalRepository.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btRefreshLocalRepository.Location = new System.Drawing.Point(6, 27);
            this.btRefreshLocalRepository.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btRefreshLocalRepository.Name = "btRefreshLocalRepository";
            this.btRefreshLocalRepository.Size = new System.Drawing.Size(70, 26);
            this.btRefreshLocalRepository.TabIndex = 6;
            this.btRefreshLocalRepository.Text = "Refresh";
            this.btRefreshLocalRepository.UseVisualStyleBackColor = false;
            this.btRefreshLocalRepository.Click += new System.EventHandler(this.btRefreshLocalRepository_Click);
            // 
            // btCreateRepository
            // 
            this.btCreateRepository.BackColor = System.Drawing.Color.LightGray;
            this.btCreateRepository.Enabled = false;
            this.btCreateRepository.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btCreateRepository.Location = new System.Drawing.Point(611, 27);
            this.btCreateRepository.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btCreateRepository.Name = "btCreateRepository";
            this.btCreateRepository.Size = new System.Drawing.Size(155, 26);
            this.btCreateRepository.TabIndex = 5;
            this.btCreateRepository.Text = "Create Repository (selected)";
            this.btCreateRepository.UseVisualStyleBackColor = false;
            this.btCreateRepository.Click += new System.EventHandler(this.btCreateRepository_Click);
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
            this.gvLocalLiveries.Location = new System.Drawing.Point(6, 58);
            this.gvLocalLiveries.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gvLocalLiveries.Name = "gvLocalLiveries";
            this.gvLocalLiveries.RowTemplate.Height = 24;
            this.gvLocalLiveries.Size = new System.Drawing.Size(760, 381);
            this.gvLocalLiveries.TabIndex = 4;
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
            // tPOnlineRepo
            // 
            this.tPOnlineRepo.Controls.Add(this.label2);
            this.tPOnlineRepo.Controls.Add(this.lbNotImplementedOnlineManager);
            this.tPOnlineRepo.Location = new System.Drawing.Point(4, 22);
            this.tPOnlineRepo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tPOnlineRepo.Name = "tPOnlineRepo";
            this.tPOnlineRepo.Size = new System.Drawing.Size(822, 496);
            this.tPOnlineRepo.TabIndex = 2;
            this.tPOnlineRepo.Text = "Online Repository";
            this.tPOnlineRepo.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(200, 161);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(540, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "This tab will alow you to manage your online repository. Not yet implemented";
            // 
            // lbNotImplementedOnlineManager
            // 
            this.lbNotImplementedOnlineManager.AutoSize = true;
            this.lbNotImplementedOnlineManager.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNotImplementedOnlineManager.Location = new System.Drawing.Point(288, 84);
            this.lbNotImplementedOnlineManager.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbNotImplementedOnlineManager.Name = "lbNotImplementedOnlineManager";
            this.lbNotImplementedOnlineManager.Size = new System.Drawing.Size(283, 31);
            this.lbNotImplementedOnlineManager.TabIndex = 0;
            this.lbNotImplementedOnlineManager.Text = "NOT IMPLEMENTED!";
            // 
            // tPSettings
            // 
            this.tPSettings.Controls.Add(this.btFindSavegamesPath);
            this.tPSettings.Controls.Add(this.tbPathToDCSSavegame);
            this.tPSettings.Controls.Add(this.lbPathToDCSSavegames);
            this.tPSettings.Controls.Add(this.lbSettingsTitle);
            this.tPSettings.Location = new System.Drawing.Point(4, 22);
            this.tPSettings.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tPSettings.Name = "tPSettings";
            this.tPSettings.Size = new System.Drawing.Size(822, 496);
            this.tPSettings.TabIndex = 3;
            this.tPSettings.Text = "Settings";
            this.tPSettings.UseVisualStyleBackColor = true;
            // 
            // btFindSavegamesPath
            // 
            this.btFindSavegamesPath.Location = new System.Drawing.Point(344, 63);
            this.btFindSavegamesPath.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btFindSavegamesPath.Name = "btFindSavegamesPath";
            this.btFindSavegamesPath.Size = new System.Drawing.Size(31, 21);
            this.btFindSavegamesPath.TabIndex = 3;
            this.btFindSavegamesPath.Text = "...";
            this.btFindSavegamesPath.UseVisualStyleBackColor = true;
            this.btFindSavegamesPath.Click += new System.EventHandler(this.btFindSavegamesPath_Click);
            // 
            // tbPathToDCSSavegame
            // 
            this.tbPathToDCSSavegame.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPathToDCSSavegame.Location = new System.Drawing.Point(11, 63);
            this.tbPathToDCSSavegame.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbPathToDCSSavegame.Name = "tbPathToDCSSavegame";
            this.tbPathToDCSSavegame.Size = new System.Drawing.Size(329, 23);
            this.tbPathToDCSSavegame.TabIndex = 2;
            // 
            // lbPathToDCSSavegames
            // 
            this.lbPathToDCSSavegames.AutoSize = true;
            this.lbPathToDCSSavegames.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPathToDCSSavegames.Location = new System.Drawing.Point(8, 45);
            this.lbPathToDCSSavegames.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbPathToDCSSavegames.Name = "lbPathToDCSSavegames";
            this.lbPathToDCSSavegames.Size = new System.Drawing.Size(200, 17);
            this.lbPathToDCSSavegames.TabIndex = 1;
            this.lbPathToDCSSavegames.Text = "Path to DCS Savegame folder:";
            // 
            // lbSettingsTitle
            // 
            this.lbSettingsTitle.AutoSize = true;
            this.lbSettingsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSettingsTitle.Location = new System.Drawing.Point(6, 9);
            this.lbSettingsTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbSettingsTitle.Name = "lbSettingsTitle";
            this.lbSettingsTitle.Size = new System.Drawing.Size(91, 26);
            this.lbSettingsTitle.TabIndex = 0;
            this.lbSettingsTitle.Text = "Settings";
            // 
            // pnInitLocalLiveries
            // 
            this.pnInitLocalLiveries.Controls.Add(this.label1);
            this.pnInitLocalLiveries.Controls.Add(this.lbInitializingLocalLiveries);
            this.pnInitLocalLiveries.Location = new System.Drawing.Point(265, 152);
            this.pnInitLocalLiveries.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnInitLocalLiveries.Name = "pnInitLocalLiveries";
            this.pnInitLocalLiveries.Size = new System.Drawing.Size(295, 162);
            this.pnInitLocalLiveries.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(46, 78);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "this may take a while....";
            // 
            // lbInitializingLocalLiveries
            // 
            this.lbInitializingLocalLiveries.AutoSize = true;
            this.lbInitializingLocalLiveries.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbInitializingLocalLiveries.Location = new System.Drawing.Point(40, 45);
            this.lbInitializingLocalLiveries.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbInitializingLocalLiveries.Name = "lbInitializingLocalLiveries";
            this.lbInitializingLocalLiveries.Size = new System.Drawing.Size(202, 24);
            this.lbInitializingLocalLiveries.TabIndex = 1;
            this.lbInitializingLocalLiveries.Text = "Initializing local liveries.";
            // 
            // CCheckbox
            // 
            this.CCheckbox.HeaderText = "Select";
            this.CCheckbox.Name = "CCheckbox";
            this.CCheckbox.Width = 60;
            // 
            // Category
            // 
            this.Category.HeaderText = "Category";
            this.Category.Name = "Category";
            this.Category.ReadOnly = true;
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 522);
            this.Controls.Add(this.pnInitLocalLiveries);
            this.Controls.Add(this.tabControl);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.tabControl.ResumeLayout(false);
            this.tPInstall.ResumeLayout(false);
            this.tPInstall.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvInstallRepo)).EndInit();
            this.tPLocalLiverys.ResumeLayout(false);
            this.tPLocalLiverys.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvLocalLiveries)).EndInit();
            this.tPOnlineRepo.ResumeLayout(false);
            this.tPOnlineRepo.PerformLayout();
            this.tPSettings.ResumeLayout(false);
            this.tPSettings.PerformLayout();
            this.pnInitLocalLiveries.ResumeLayout(false);
            this.pnInitLocalLiveries.PerformLayout();
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
        private System.Windows.Forms.DataGridViewCheckBoxColumn CCheckbox;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category;
        private System.Windows.Forms.DataGridViewTextBoxColumn CAircraft;
        private System.Windows.Forms.DataGridViewTextBoxColumn CName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CStatus;
    }
}