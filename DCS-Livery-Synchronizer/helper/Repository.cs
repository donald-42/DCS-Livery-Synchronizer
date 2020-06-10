using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace DCS_Livery_Synchronizer.helper
{
    /// <summary>
    /// Represents a repository, which is basicly an XML representation of downloadlinks and checksums for zip files.
    /// </summary>
    public class Repository
    {
        [XmlAttribute]
        public string programmversion; //programm version, this repo was created with (for compatibility reasions)
        [XmlAttribute]
        public string name; //readable name of the repository
        //[XmlAttribute]
        public List<RepoLivery> liveries;
        private Controller controller;

        public Repository()
        {
            
        }

        public Repository(string programmversion, string name, Controller ctrl)
        {
            this.programmversion = programmversion;
            this.name = name;
            liveries = new List<RepoLivery>();
            controller = ctrl;
        }

        public void createList(List<string> liveryPathes)
        {
            foreach (string liverypath in liveryPathes)
            {
                if(!File.Exists(Path.Combine(liverypath, "description.lua")))
                {
                    continue; //only use pathes that contain description.lua
                }
                RepoLivery liv = new RepoLivery();
                //Get Path after "Liveries" folder
                string[] splitPath = liverypath.Split(Path.DirectorySeparatorChar);
                int usepathafterindex = -1;
                liv.downloadurl = "";
                for(int i = 0; i<splitPath.Length; i++)
                {
                    if (splitPath[i].Contains("Liveries"))
                    {
                        usepathafterindex = i;
                        liv.aircraft = splitPath[i + 1];
                    }
                    if((usepathafterindex < i) && (usepathafterindex != -1))
                    {
                        liv.downloadurl = Path.Combine (liv.downloadurl, splitPath[i]);
                    }
                }
                liv.path = liv.downloadurl;
                liv.downloadurl += ".zip";

                Console.WriteLine(liv.downloadurl);
                Description dsc = new Description(Path.Combine(liverypath, "description.lua"));
                liv.name = dsc.GetName();
                liv.checksum = "0";
                liveries.Add(liv);
            }
        }

        /// <summary>
        /// saves the XML as well as a compressed version of all the skins.
        /// </summary>
        /// <param name="path"></param>
        public void saveRepo(string path)
        {
            var serializer = new XmlSerializer(this.GetType());
            using (var writer = XmlWriter.Create(path))
            {
                serializer.Serialize(writer, this);
            }

            string folderpath = Path.GetDirectoryName(path);
            Console.WriteLine(folderpath);
            int i = 0;
            double percentage = 0;
            controller.setProgressBar(1);
            controller.FormEnabled(false);
            foreach(RepoLivery liv in liveries)
            {
                percentage = (double)(i) /liveries.Count;
                i++;
                controller.setProgressBar((int) (percentage * 100));
                Console.WriteLine("Test:" + liv.path);
                if(!Directory.Exists(Path.Combine(folderpath, liv.aircraft)))
                {
                    Directory.CreateDirectory(Path.Combine(folderpath, liv.aircraft));
                }
                if(File.Exists(Path.Combine(folderpath, liv.downloadurl)))
                {
                    File.Delete(Path.Combine(folderpath, liv.downloadurl));
                }
                ZipFile.CreateFromDirectory(Path.Combine(controller.GetSettings().dcssavedgames, "Liveries", liv.path), Path.Combine(folderpath, liv.downloadurl));
            }
            controller.setProgressBar(100);
            controller.FormEnabled(true);
        }


        public class RepoLivery
        {
            [XmlAttribute]
            public string downloadurl; //url the repository is located at in reference to the repo xml.
            [XmlAttribute]
            public string checksum;  //calculated checksum of the complete livery folder. *TODO*: Implement checksum calculation.
            [XmlAttribute]
            public string path; // path the zip will be uncompressed at (saved games/DCS/liveries/*ACType*/*Liveryname*
            [XmlAttribute]
            public string name; //name of the livery as stated in the description.lua
            [XmlAttribute]
            public string aircraft;  //name of the aircraft this livery is made for.
        }
    }
}
