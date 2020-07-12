using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Net;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO.Compression;
using RestSharp;

namespace DCS_Livery_Synchronizer
{
    /// <summary>
    /// sub controller manages everything that has to do with installing new Liverys from online repository.
    /// </summary>
    public class InstallController
    {
        private NewController parent;

        private int downloadProgress = 0;
        private int numberOfDownloads = 0; //saves the cound of the download items to calculate the overall progress.

        /// <summary>
        /// Create new controllerinstall instance. Handles everything with downloading and installing liveries.
        /// </summary>
        /// <param name="parent">Parent controller that creates this subcontroller</param>
        public InstallController(NewController parent)
        {
            this.parent = parent;
            //wc = new WebClient();
            //wc.DownloadProgressChanged += DownloadProgressChanged;
            //wc.DownloadFileCompleted += DownloadCompleted;
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
            //var livery = installQueue.Dequeue();

            //if (!Directory.Exists(Path.Combine(parent.GetModel().GetSettings().dcssavedgames, "Liveries")))
            //{
            //    Directory.CreateDirectory(Path.Combine(parent.GetModel().GetSettings().dcssavedgames, "Liveries"));
            //}

            //string installPath = Path.Combine(parent.GetModel().GetSettings().dcssavedgames, "Liveries", livery.path);
            ////if exists ask for owerwrite
            ////TODO: Pass this to View/introduce boolean parameter whether to overwrite or not. UI has to set it to true then.
            //if (Directory.Exists(installPath))
            //{
            //    DialogResult dialogResult = MessageBox.Show("Livery at \n" + installPath + "\nalready Exists.\nDo you want to overwrite it? All files in the directory will be permanently deleted. ", "Folder Already Exists", MessageBoxButtons.YesNo);
            //    if (dialogResult == DialogResult.Yes)
            //    {
            //        Directory.Delete(installPath, true);
            //        //Extract and install to the right folder:
            //        ZipFile.ExtractToDirectory(Path.Combine(tempPath, livery.url), installPath);
            //    }
            //}
            //else
            //{
            //    Directory.CreateDirectory(installPath);
            //    ZipFile.ExtractToDirectory(Path.Combine(tempPath, livery.url), installPath);
            //}
            //DownloadNext();
        }

        /// <summary>
        /// Method that gets triggered when downloadprogress on webclient changes. Calculates Progress then.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            ////divider is needed, so that the progress off e.g. 5 downloads gets reduced to 1/5
            //float divider = (float)(1) / numberOfDownloads;
            ////additor is needed, so that the progress of the second download gets added to the 
            ////completed partprogress of the first download
            //float additor = (float)((float)(numberOfDownloads - installQueue.Count) / numberOfDownloads) * 100;
            //downloadProgress = (int)(e.ProgressPercentage * divider) + (int)additor;
            //parent.setDownloadProgress(downloadProgress);
            
        }


        /// <summary>
        /// Method to install all liverys within liverylist. Method will only install liverys which have a valid url path.
        /// </summary>
        /// <param name="liverylist">Handle to a list of liveries that will be installed</param>
        public void InstallLiveriesAsync(List<Livery> liverylist)
        {
            //installQueue = new Queue<Livery>();
            //foreach(Livery lv in liverylist)    
            //{
            //    installQueue.Enqueue(lv);
            //}
            //if (!Directory.Exists(tempPath))
            //{
            //    Directory.CreateDirectory(tempPath);
            //}
            //numberOfDownloads = installQueue.Count();
            //DownloadNext();

            //RestClient client = new RestClient(this.parent.GetModel().GetOnlineRepoBaseAddress());
            //client.AddDefaultHeader("Accept", "*/*");

            //var tasks = new Task[liverylist.Count];
            //for (int i = 0; i < liverylist.Count; i++)
            //{
            //    tasks[i] = this.DownloadLiveryAsync(client, liverylist[i]);//  new Uri(this.parent.GetModel().GetOnlineRepoBaseAddress() + liverylist[i].url));
            //}

            ////Task.WaitAll(tasks);
            //Task.WhenAll(tasks).Wait();
            //this.downloadProgress = 100;
        }
        public void DownloadAndInstallLiveriesAsync(List<DownloadHandle> downloads)
        {
            RestClient client = new RestClient(this.parent.Model.GetOnlineRepoBaseAddress());
            client.AddDefaultHeader("Accept", "*/*");

            int count = downloads.Count;
            var tasks = new Task[count];
            for (int i = 0; i < count; i++)
            {
                tasks[i] = this.DownloadLiveryAsync(client, downloads[i]);//  new Uri(this.parent.GetModel().GetOnlineRepoBaseAddress() + liverylist[i].url));
            }

            //Task.WaitAll(tasks);
            Task.WhenAll(tasks).Wait();
            this.downloadProgress = 100;

            MessageBox.Show($"Completed installation of {count} liveries!");
        }

        private Task DownloadLiveryAsync(RestClient client, DownloadHandle download)
        {
            return Task.Factory.StartNew(() =>
            {
                // Clear out any existing installation.
                string directory = $"{download.DestinationFolder.TrimEnd('/', '\\')}\\{download.Livery.path}";
                if (Directory.Exists(directory))
                {
                    Directory.Delete(directory, true);
                }

                try
                {
                    // Download the livery.
                    var requestTask = Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            var request = new RestRequest(download.Livery.url);
                            download.Status = "Downloading";
                            return client.Execute(request, Method.GET);
                        }
                        catch (Exception ex)
                        {
                            download.Status = "Download failed";
                            throw;
                        }
                    });
                    requestTask.Wait();
                    var response = requestTask.Result;

                    if (response.StatusCode != HttpStatusCode.OK) throw new Exception($"{(int)response.StatusCode}: {response.StatusCode.ToString()}");

                    // Unzip and write the files.
                    using (var srcStream = new MemoryStream(response.RawBytes))
                    using (var archieve = new ZipArchive(srcStream))
                    {
                        download.Status = "Unpacking";
                        int count = archieve.Entries.Count;
                        Task[] tasks = new Task[count];

                        for (int i = 0; i < count; i++)
                        {
                            var entry = archieve.Entries[i];
                            tasks[i] = Task.Factory.StartNew(() =>
                            {
                                try
                                {
                                    if (entry.FullName[entry.FullName.Length - 1] != '/' && entry.FullName[entry.FullName.Length - 1] != '\\') // Folders end on either / or  \. Don't wanna unzip them.
                                    {
                                        string filepath = $"{download.DestinationFolder.TrimEnd('/', '\\')}\\{download.Livery.path}\\{entry.FullName}";

                                        var dir = Path.GetDirectoryName(filepath);
                                        if (Directory.Exists(dir) == false) Directory.CreateDirectory(dir);

                                        entry.ExtractToFile(filepath, false);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    download.Status = "Unpack failed";
                                }
                            });
                        }

                        Task.WaitAll(tasks);
                    }
                }
                catch (Exception ex)
                {
                    download.Status = "Install failed";
                }
            });
        }

        /// <summary>
        /// Starts Async Download with webclient. Takes first item from installqueue without dequeing it.
        /// </summary>
        private void DownloadNext()
        {
            // Could probably just start a bunch of Tasks instead of doing them one at a time.

            //if(installQueue.Count == 0)
            //{
            //    //Installation completed.
            //    parent.OnInstallationCompleted(this, EventArgs.Empty);
            //    return;
            //}

            //var currentliv = installQueue.Peek();

            //if(Directory.Exists(Path.Combine(tempPath, currentliv.aircraft)) == false)
            //{
            //    Directory.CreateDirectory(Path.Combine(tempPath, currentliv.aircraft));
            //}
            //wc.DownloadFileAsync(new Uri(parent.GetModel().GetOnlineRepoBaseAddress() + currentliv.url), Path.Combine(tempPath, currentliv.url));


        }

        /// <summary>
        /// Loads online repository xml, parses it and stores information in the model.
        /// </summary>
        /// <param name="url"></param>
        public void LoadOnlineRepository(string url)
        {
            var onlineRepository = parent.Model.OnlineRepository;

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
                    parent.Model.SetOnlineRepoBaseAddress(Path.GetDirectoryName(url));
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
                    parent.Model.SetOnlineRepoBaseAddress(url.Substring(0, url.LastIndexOf('/') + 1)); //cut the filename to retrieve base adress
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

                liv.Status = parent.InstallStatusToDisplayString(parent.GetLiveryInstallStatus(liv));
                //liv.Status = parent.CheckLiveryInstalled(liv) ? "Installed" : "Not installed";

                onlineRepository.AddLivery(liv);
            }
            
        }
    }
}