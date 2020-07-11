using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Net;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO.Compression;

namespace DCS_Livery_Synchronizer
{
    /// <summary>
    /// sub controller manages everything that has to do with installing new Liverys from online repository.
    /// </summary>
    class ControllerInstall
    {
        private NewController parent;
        private Queue<Livery> installQueue; //list that stores all liverys that get enqueued for installation.
        private WebClient wc; //webclient used to download liveries.
        private string tempPath = Path.Combine(Path.GetTempPath(), "DCSLiverySynchronizer");
        private int downloadProgress = 0; //number between 0 and 100 showing download progress.
        private int numberOfDownloads = 0; //saves the cound of the download items to calculate the overall progress.

        /// <summary>
        /// Create new controllerinstall instance. Handles everything with downloading and installing liveries.
        /// </summary>
        /// <param name="parent">Parent controller that creates this subcontroller</param>
        public ControllerInstall(NewController parent)
        {
            this.parent = parent;
            wc = new WebClient();
            wc.DownloadProgressChanged += DownloadProgressChanged;
            wc.DownloadFileCompleted += DownloadCompleted;
        }

        /// <summary>
        /// Returns current download progress. 
        /// </summary>
        /// <returns>Download Progress, always between 0 and 100</returns>
        public int getDownloadProgress()
        {
            return Math.Max(0, Math.Min(100,downloadProgress));
        }

        public void setDownloadProgress(int progress)
        {
            downloadProgress = Math.Max(0, Math.Min(100, downloadProgress));
        }

        private void DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            var livery = installQueue.Dequeue();

            if (!Directory.Exists(Path.Combine(parent.GetModel().GetSettings().dcssavedgames, "Liveries")))
            {
                Directory.CreateDirectory(Path.Combine(parent.GetModel().GetSettings().dcssavedgames, "Liveries"));
            }

            string installPath = Path.Combine(parent.GetModel().GetSettings().dcssavedgames, "Liveries", livery.path);
            //if exists ask for owerwrite
            //TODO: Pass this to View/introduce boolean parameter whether to overwrite or not. UI has to set it to true then.
            if (Directory.Exists(installPath))
            {
                DialogResult dialogResult = MessageBox.Show("Livery at \n" + installPath + "\nalready Exists.\nDo you want to overwrite it? All files in the directory will be permanently deleted. ", "Folder Already Exists", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        Directory.Delete(installPath, true);
                        //Extract and install to the right folder:
                        ZipFile.ExtractToDirectory(Path.Combine(tempPath, livery.url), installPath);
                    } catch (Exception exc)
                    {
                        parent.SetCurrentErrorMessage("Fatal Error: Could not extract the livery. Livery could not be installed.\n" + exc.ToString());
                    }
                }
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(installPath);
                    ZipFile.ExtractToDirectory(Path.Combine(tempPath, livery.url), installPath);
                } catch (Exception exc)
                {
                    parent.SetCurrentErrorMessage("Fatal Error while extracting the Livery. Livery could not be installed.\n" + exc.ToString());
                }
            }
            DownloadNext();
        }

        /// <summary>
        /// Method that gets triggered when downloadprogress on webclient changes. Calculates Progress then.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            //divider is needed, so that the progress off e.g. 5 downloads gets reduced to 1/5
            float divider = (float)(1) / numberOfDownloads;
            //additor is needed, so that the progress of the second download gets added to the 
            //completed partprogress of the first download
            float additor = (float)((float)(numberOfDownloads - installQueue.Count) / numberOfDownloads) * 100;
            downloadProgress = (int)(e.ProgressPercentage * divider) + (int)additor;
            parent.setDownloadProgress(downloadProgress);
            
        }


        /// <summary>
        /// Method to install all liverys within liverylist. Method will only install liverys which have a valid url path.
        /// </summary>
        /// <param name="liverylist">Handle to a list of liveries that will be installed</param>
        public void InstallLiveriesAsync(List<Livery> liverylist)
        {
            installQueue = new Queue<Livery>();
            foreach(Livery lv in liverylist)    
            {
                installQueue.Enqueue(lv);
            }
            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }
            numberOfDownloads = installQueue.Count();
            DownloadNext();
        }

        /// <summary>
        /// Starts Async Download with webclient. Takes first item from installqueue without dequeing it.
        /// </summary>
        private void DownloadNext()
        {
            if(installQueue.Count < 1)
            {
                //Installation completed.
                parent.OnInstallationCompleted(this, EventArgs.Empty);
                return;
            }

            var currentliv = installQueue.Peek();

            if(!Directory.Exists(Path.Combine(tempPath, currentliv.aircraft)))
            {
                Directory.CreateDirectory(Path.Combine(tempPath, currentliv.aircraft));
            }
            wc.DownloadFileAsync(new Uri(parent.GetModel().GetOnlineRepoBaseAddress() + currentliv.url), Path.Combine(tempPath, currentliv.url));

        }

        /// <summary>
        /// Loads online repository xml, parses it and stores information in the model.
        /// </summary>
        /// <param name="url"></param>
        public void LoadOnlineRepository(string url)
        {
            var onlineRepository = parent.GetModel().GetOnlineRepository();

            //check if URL is valid
            bool result = onlineRepository.SetPath(url); //does validation - if result false path is not valid.
            string repoXmlContent = null;
            if (result)
            {
                if (File.Exists(url)) //local xml repo -- only for testing purpouse. probably.
                {
                    try
                    {
                        repoXmlContent = File.ReadAllText(url);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        repoXmlContent = null; //to make sure next if is true and error is raised
                    }
                    parent.GetModel().SetOnlineRepoBaseAddress(Path.GetDirectoryName(url));
                }
                else // online repo, default case
                {
                    try
                    {
                        using (var wc = new System.Net.WebClient())
                            repoXmlContent = wc.DownloadString(url);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        repoXmlContent = null; //to make sure next if is true and error is raised.
                    }
                    parent.GetModel().SetOnlineRepoBaseAddress(url.Substring(0, url.LastIndexOf('/') + 1)); //cut the filename to retrieve base adress
                }
            }
            if (repoXmlContent == null)
            {
                parent.SetCurrentErrorMessage("Error downloading online repository.");
                return;
            }

            //Starting interpreting of the xml structure of the file.
            var xml = new XmlDocument();
            xml.LoadXml(repoXmlContent);

            XmlElement root = xml.DocumentElement;

            onlineRepository.SetProgrammVersion(root.SelectSingleNode("programmversion").InnerText);
            onlineRepository.SetName(root.SelectSingleNode("name").InnerText);

            //Clears liverylist in repo and adds all liverys as stated in the repo xml file.
            onlineRepository.ClearLiveries();
            var liveries = root.SelectNodes("livery");
            foreach (XmlNode livery in liveries)
            {
                var liv = new Livery();
                liv.aircraft = livery.SelectSingleNode("liveryaircraft").InnerText;
                liv.checksum = livery.SelectSingleNode("liverychecksum").InnerText;
                liv.url = livery.SelectSingleNode("liveryurl").InnerText;
                liv.name = livery.SelectSingleNode("liveryname").InnerText;
                liv.path = livery.SelectSingleNode("liverypath").InnerText;
                onlineRepository.AddLivery(liv);
            }
            
        }
    }
}
