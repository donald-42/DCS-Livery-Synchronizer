using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCS_Livery_Synchronizer
{
    public partial class MainWindow : Form
    {
        private Controller controller;
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
            controller.UpdateInstalledLiveries();
        }
    }
}
