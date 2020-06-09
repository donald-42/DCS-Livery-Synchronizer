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
            
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            controller = new Controller();
            controller.Initialize();
            //Update Controls
            tbPathLiveries.Text = controller.GetSettings().dcssavedgames;
            //controller.UpdateInstalledLiveries();
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
    }
}
