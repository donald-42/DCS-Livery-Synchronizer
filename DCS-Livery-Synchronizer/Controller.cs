﻿using DCS_Livery_Synchronizer.helper;
using DCS_Livery_Synchronizer.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace DCS_Livery_Synchronizer
{
    /// <summary>
    /// Does most of the work. Handles the programm settings, provides additional functionality to the form objects, executes the sync and downloading.
    /// </summary>
    public class Controller
    {
        private string roamingPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DCSLiveriesSynchronizer"); //Path to the programm settings file in appdata/roaming
        private string savefileName = "settings.xml"; //Name of the settings file
        private string savefilePath;
        private MainWindow form;
        private Repository dlRepo;
        private string baseUrl; //url the xml is located at, without the filename.
        private List<Livery> installedLiveries;

        private Settings settings;

        public void CreateRepository(string name, List<string> liverypaths, string savepath)
        {
            Repository repo = new Repository(settings.version, name, this);
            repo.createList(liverypaths);
            repo.saveRepo(savepath);
        }

        public void LoadRepository(string path)
        {
            Repository loadedRepo = null;
            //actually, this is currently useless code but might gets helpful for testing anytime.
            if (File.Exists(path))
            {
                loadedRepo = new Repository(File.ReadAllText(path));
            }

            //check if URL is valid
            Uri uriResult;
            bool result = Uri.TryCreate(path, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (result)
            {
                using (var wc = new System.Net.WebClient())
                    loadedRepo = new Repository(wc.DownloadString(path));
            }

            if ((loadedRepo == null) || (loadedRepo.liveries == null))
                return;

            foreach (Repository.RepoLivery rl in loadedRepo.liveries)
            {
                form.AddOnlineRepoItem(rl.aircraft + "\\" + Path.GetFileName(rl.path) + ": " + rl.name);
            }

            dlRepo = loadedRepo;
            baseUrl = path.Substring(0, path.LastIndexOf('/')+1); //cut the filename 
        }
        /// <summary>
        /// Download and installs the selected liveries.
        /// </summary>
        /// <param name="items">Selected Items in the Checkbox</param>
        public void installliveries(string[] items)
        {
            if (dlRepo == null)
                return;
            //find the correct liveries to the selected items.
            List<Repository.RepoLivery> installlist = new List<Repository.RepoLivery>();
            foreach(string item in items)
            {
                //first split the string in the left part
                string label = item.Split(':')[0];
                string itemAC = label.Split('\\')[0];
                string itemDir = label.Split('\\')[1];

                foreach(Repository.RepoLivery rl in dlRepo.liveries)
                {
                    if ((rl.aircraft.Equals(itemAC)) && (Path.GetFileName(rl.path).Equals(itemDir))){
                        installlist.Add(rl);
                    }
                }

            }

            //download zip files to temporary folder
            string tempPath = Path.Combine(Path.GetTempPath(), "DCSLiverys");
            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }

            foreach (var x in installlist)
            {
                using (var client = new WebClient())
                { 
                    if(!Directory.Exists(Path.Combine(tempPath, x.aircraft)))
                    {
                        Directory.CreateDirectory(Path.Combine(tempPath, x.aircraft));
                    }


                    client.DownloadFile(baseUrl + x.downloadurl, Path.Combine(Path.GetTempPath(), "DCSLiverys", x.downloadurl));

                    //start with installation:
                    string installPath = Path.Combine(settings.dcssavedgames, "Liveries", x.path);

                    //if exists ask for owerwrite
                    if (Directory.Exists(installPath))
                    {
                        DialogResult dialogResult = MessageBox.Show("Livery at \n" + installPath + "\nalready Exists.\nDo you want to owerwrite it?" , "Folder Already Exists", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            Directory.Delete(installPath, true);
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            continue;
                        }
                    }
                    //Extract and install to the right folder:
                    ZipFile.ExtractToDirectory(Path.Combine(Path.GetTempPath(), "DCSLiverys", x.downloadurl), installPath);

                }
            }
        }

        public Controller(MainWindow form)
        {
            this.form = form;
            savefilePath = Path.Combine(roamingPath, savefileName);
            installedLiveries = new List<Livery>();
        }

        public void setProgressBar(int percentage)
        {
            form.setProgressBar(percentage);
        }

        public void FormEnabled(bool enabled)
        {
            form.Enabled = enabled;
        }

        public List<Livery> GetInstalledLiveries()
        {
            return installedLiveries;
        }

        /// <summary>
        /// trys to read the settings file, if not available, creates one.
        /// </summary>
        public void Initialize()
        {
            //Check if programm folder exists, if not, create it.
            if (!Directory.Exists(roamingPath))
            {
                //create file
                Directory.CreateDirectory(roamingPath);
                System.Console.WriteLine("Folder does not exist");
            }

            settings = new Settings();

            //Check if settings file exists, if not, create it and write defaults
            if (!File.Exists(savefilePath))
            {
                settings.version = "0.1";
                if (Directory.Exists(Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), "saved games", "DCS")))
                {
                    settings.dcssavedgames = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), "saved games", "DCS");
                }
                else if (Directory.Exists(Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), "saved games", "DCS.openbeta")))
                {
                    settings.dcssavedgames = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), "saved games", "DCS.openbeta");
                }
                else
                {
                    Console.WriteLine("DCS savedgames path not found.");
                }

                var serializer = new XmlSerializer(settings.GetType());
                using (var writer = XmlWriter.Create(savefilePath))
                {
                    serializer.Serialize(writer, settings);
                }
            }
            else //File exists, loading values from xml
            {
                var serializer = new XmlSerializer(typeof(Settings));
                using (var reader = XmlReader.Create(savefilePath))
                {
                    settings = (Settings)serializer.Deserialize(reader);
                    System.Console.WriteLine("Version is {0}", settings.version);
                }
            }

            //Update Controlls in window


        }
        /// <summary>
        /// saves settings into the roaming folder.
        /// </summary>
        public void SaveSettings()
        {
            var serializer = new XmlSerializer(settings.GetType());
            using (var writer = XmlWriter.Create(savefilePath))
            {
                serializer.Serialize(writer, settings);
            }
        }

        public Settings GetSettings()
        {
            return settings;
        }
        /// <summary>
        /// Scans the Liveries folder for any available Livery
        /// </summary>
        public void UpdateInstalledLiveries()
        {
            installedLiveries.Clear();

            //Reads all the existing liverys installed in the Saved game folder
            //only reads liverys with existing description.lua
            if (Directory.Exists(Path.Combine(settings.dcssavedgames, "Liveries")))
            {
                foreach(String subdir in Directory.GetDirectories(Path.Combine(settings.dcssavedgames, "Liveries")))
                {
                    string aircrafttype = Path.GetFileName(subdir);
                    foreach(String livpath in Directory.GetDirectories(subdir))
                    {
                       // System.Console.WriteLine(livpath);
                        if (File.Exists(Path.Combine(livpath, "description.lua")))
                        {
                            string descriptionpath = Path.Combine(livpath, "description.lua");
                            //System.Console.WriteLine(Path.GetFileName(livpath));
                            Livery livery = new Livery();
                            livery.aircraft = aircrafttype;
                            livery.path = livpath;
                            Description dsc = new Description(descriptionpath);
                            livery.name = dsc.GetName();
                            livery.countries = dsc.GetCountries();
                            if(livery.name == null)
                            {
                                livery.name = ""; // no livery name or not readable, might set to unknown
                            }
                            if(livery.countries == null)
                            {
                                livery.countries = ""; // no countries set or not readable, this should stand for all countries
                            }

                            //Console.WriteLine(livery.ToString());
                            installedLiveries.Add(livery);
                        }
                    }
                }
            }
        }
    }
}
