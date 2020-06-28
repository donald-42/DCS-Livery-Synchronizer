using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCS_Livery_Synchronizer
{
    public partial class MainWindow : Form
    {
        public Controller controller;
        public MainWindow()
        {
            InitializeComponent();
        }

     

        private void bt_FindLiveriesPath_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    tbPathLiveries.Text = fbd.SelectedPath;
                    controller.GetSettings().dcssavedgames = fbd.SelectedPath;
                    controller.SaveSettings();
                }
            }
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            controller = new Controller(this);
            controller.Initialize();
            //Update Controls
            tbPathLiveries.Text = controller.GetSettings().dcssavedgames;
            //controller.UpdateInstalledLiveries();
            btScanLiveries_Click(sender, e);
        }

        private void btScanLiveries_Click(object sender, EventArgs e)
        {
            controller.UpdateInstalledLiveries();
            clbInstalledLiveries.Items.Clear();
            foreach (Livery liv in controller.GetInstalledLiveries())
            {
                clbInstalledLiveries.Items.Add(liv.aircraft + "\\" + Path.GetFileName(liv.path) + ": " + liv.name );
            }
        }

        private void btCreateRepository_Click(object sender, EventArgs e)
        {
            List<string> liveryPaths = new List<string>();
            foreach(string livery in clbInstalledLiveries.CheckedItems)
            {
                string liveryPath = Path.Combine(controller.GetSettings().dcssavedgames, "Liveries", livery.Split(':')[0]);
                if (Directory.Exists(liveryPath))
                {
                    liveryPaths.Add(liveryPath);
                }
            }

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                controller.CreateRepository(tbRepoName.Text, liveryPaths, saveFileDialog1.FileName);
            }
        }

        public void setProgressBar(int percentage)
        {
            if (percentage > 100)
                percentage = 100;
            else if (percentage < 0)
                percentage = 0;
            pbStatusBar.Value = percentage;
            pbStatusBar.Refresh();
           // ActiveForm.Refresh();
        }

        public void ClearOnlineRepoItems()
        {
            clbRepositoryLiveries.Items.Clear();
        }

        public void AddOnlineRepoItem(string item)
        {
            clbRepositoryLiveries.Items.Add(item);
        }

        private void btScanRepository_Click(object sender, EventArgs e)
        {
            clbRepositoryLiveries.Items.Clear();
            controller.LoadRepository(tbRepositoryLink.Text);
        }

        private void btInstallLiveries_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            string[] checkeditems = new string[clbRepositoryLiveries.CheckedItems.Count];
            int i = 0;
            foreach (string livery in clbRepositoryLiveries.CheckedItems)
            {
                checkeditems[i] = livery;
                i++;
            }

            controller.installliveries(checkeditems);
        }
    }
}
