using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCS_Livery_Synchronizer
{
    public partial class MainForm : Form 
    {
        private NewController controller;
        private String savepath;
        public MainForm()
        {
            InitializeComponent();
            controller = new NewController();
            controller.NewError += Controller_NewError;
            controller.ModelChanged += Controller_ModelChanged;
            controller.InstallProgressChanged += Controller_InstallProgressChanged;
            controller.InstallCompleted += Controller_InstallCompleted;
            controller.RepoCreationProgressChanged += Controller_RepoCreationProgressChanged;
            controller.RepoCreationCompleted += Controller_RepoCreationCompleted;
        }

        /// <summary>
        /// Delegate that handles any changes in the Model. Applys change only to current active tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Controller_ModelChanged(object sender, EventArgs e)
        {
            ReloadCurrentTab();
        }

        /// <summary>
        /// Method that handles changes of installation progress. Form will be disabled until install is completed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Controller_InstallProgressChanged(object sender, EventArgs e)
        {
            pbInstallProgress.Value = controller.getDownloadProgress();
        }

        /// <summary>
        /// Handles the completion of the Liveryinstallation. Reenables the form and shows success message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Controller_InstallCompleted(object sender, EventArgs e)
        {
            pbInstallProgress.Value = 100;
            MessageBox.Show("Selected liverys have been installed.", "Finished!");
            this.Enabled = true;
        }

        /// <summary>
        /// Showing any Errors the Controller produces in a messagebox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Controller_NewError(object sender, EventArgs e)
        {
            MessageBox.Show(controller.GetCurrentErrorMessage());
        }

        private void btSubmitRepoURL_Click(object sender, EventArgs e)
        {
            controller.LoadOnlineRepository(tBRepoURL.Text);
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadCurrentTab();
        }

        private void Controller_RepoCreationProgressChanged(object sender, EventArgs e)
        {
            pbCreateRepo.Value = controller.GetRepoCreationProgress();
        }

        private void Controller_RepoCreationCompleted(object sender, EventArgs e)
        {
            pbCreateRepo.Value = 100;
            MessageBox.Show("Repository successfully created");
            this.Enabled = true;
            if (Directory.Exists(savepath))
                Process.Start("explorer.exe", savepath);
            //System.Diagnostics.Process.Start("explorer.exe", string.Format("/select,\"{0}\"", "F:\\test\\test.xml"));
        }

        /// <summary>
        /// Reloads the current active tab of the tabcontrol. Called on every tab and model change
        /// </summary>
        private void ReloadCurrentTab()
        {
            if(tabControl.SelectedIndex < 0) //if no tab is active, activate tab 1
            {
                tabControl.SelectedIndex = 0;
            }

            var model = controller.GetModel();

            switch(tabControl.SelectedIndex)
            {
                case 0: //Install from online repo tab.
                    var onlineRepo = model.GetOnlineRepository();
                    //fill information into the gridview
                    gvInstallRepo.Rows.Clear();
                    foreach (Livery lv in onlineRepo.GetLiveries())
                    {
                        Object[] row = new object[4];
                        row[0] = true;
                        row[1] = lv.aircraft;
                        row[2] = lv.name;
                        //check if livery is already installed
                        switch (controller.CheckLiveryInstalled(lv))
                        {
                            case 0:
                                row[3] = "Not Installed";
                                break;
                            case 1:
                                row[3] = "Different";
                                break;
                            case 2:
                                row[3] = "Installed";
                                row[0] = false;
                                break;
                        }
                        gvInstallRepo.Rows.Add(row);
                    }
                    if(gvInstallRepo.Rows.Count > 0)
                    {
                        btInstallSelected.Enabled = true;
                        btInstallSelected.BackColor = Color.LimeGreen;
                    }
                    else
                    {
                        btInstallSelected.Enabled = false;
                        btInstallSelected.BackColor = Color.LightGray;
                    }
                    break;
                case 1: //Local Repo tab
                    gvLocalLiveries.Rows.Clear();
                    var localRepo = controller.GetModel().GetLocalRepository();
                    
                    foreach(Livery lv in localRepo.GetLiveries())
                    {
                        Object[] row = new object[5];
                        row[0] = true;
                        row[1] = lv.aircraft;
                        row[2] = lv.name;
                        row[3] = lv.path;
                        gvLocalLiveries.Rows.Add(row);
                    }
                    if (gvLocalLiveries.Rows.Count > 0)
                    {
                        btCreateRepository.Enabled = true;
                        btCreateRepository.BackColor = Color.LimeGreen;
                    }
                    else
                    {
                        btCreateRepository.Enabled = false;
                        btCreateRepository.BackColor = Color.LightGray;
                    }

                    break;
                case 3: //Settings tab
                    if(model.GetSettings() == null)
                    {
                        controller.LoadSettings();
                    }
                    if(model.GetSettings() == null)
                    {
                        Console.WriteLine("Fatal Error: Can not load settings");
                        break;
                    }
                    var settings = model.GetSettings();
                    tbPathToDCSSavegame.Text = settings.dcssavedgames;
                    break;
            }
        }

        /// <summary>
        /// Opens file explorer to select dcs savegames folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btFindSavegamesPath_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    tbPathToDCSSavegame.Text = fbd.SelectedPath;
                    if (!controller.SetPathToSavegame(fbd.SelectedPath))
                        MessageBox.Show("Error: Path does not exist.");
                    controller.SaveSettings();
                }
            }
        }


        private void btCreateRepository_Click(object sender, EventArgs e)
        {
            List<Livery> installList = new List<Livery>();
            var path = "";
            var name = tbRepoName.Text;

            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Please select a location to save the repo at. \nFolder must be empty.";
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    path = fbd.SelectedPath;
                } else
                {
                    return;
                }
            }

            savepath = path;

            foreach (DataGridViewRow row in gvLocalLiveries.Rows)
            {
                if (bool.Parse(row.Cells[0].Value.ToString()))
                {
                    var liverypath = row.Cells[3].Value.ToString();

                    var lv = controller.GetModel().GetLocalRepository().GetLivery(liverypath);
                    if(lv != null)
                    {
                        installList.Add(lv);
                    }
                    else
                    {
                        MessageBox.Show("Fatal Error on Livery Selection (local).");
                    }
                }
            }
            this.Enabled = false;
            controller.CreateLocalRepositoryAsync(installList, path, name);
        }

        /// <summary>
        /// Creates a list of liverys to be installed by reading the Rows of the Gridview and matching entrys with livery list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btInstallSelected_Click(object sender, EventArgs e)
        {
            List<Livery> installList = new List<Livery>();
            foreach(DataGridViewRow row in gvInstallRepo.Rows)
            {
                if (bool.Parse(row.Cells[0].Value.ToString()))
                {
                    var aircrafttype = row.Cells[1].Value.ToString();
                    var aircraftname = row.Cells[2].Value.ToString();

                    var lv = controller.GetModel().GetOnlineRepository().GetLivery(aircrafttype, aircraftname);

                    //if livery found, add it to the installlist.
                    if(lv != null)
                    {
                        installList.Add(lv);
                    }
                    else
                    {
                        MessageBox.Show("Fatal Error on livery selection.");
                    }
                }
            }
            controller.InstallLiveriesAsync(installList);
            this.Enabled = false;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            this.Enabled = false;
            controller.LoadLocalRepository();
            pnInitLocalLiveries.Visible = false;
            this.Enabled = true;
        }

        private void btRefreshLocalRepository_Click(object sender, EventArgs e)
        {
            pnInitLocalLiveries.Visible = true;
            this.Enabled = false;
            controller.LoadLocalRepository();
            this.Enabled = true;
            pnInitLocalLiveries.Visible = false;
        }

    }
}
