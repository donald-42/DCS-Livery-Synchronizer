using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlTypes;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace DCS_Livery_Synchronizer
{
    /// <summary>
    /// Represents a repository, which is basicly an XML representation of downloadlinks and checksums for zip files.
    /// </summary>
    public class RepositoryOld
    {
        public string programmversion { get; set; } //programm version, this repo was created with (for compatibility reasions)
        public string name { get; set; } //readable name of the repository
        public List<RepoLivery> liveries;
        private Controller controller;

        /// <summary>
        /// Reads everything from the given repositorystring
        /// </summary>

        public RepositoryOld(string fileContent)
        {
            //using XmlDocument to Read DOM structure
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(fileContent);

            XmlElement root = doc.DocumentElement;
            Console.WriteLine("progrversion = " + root.SelectSingleNode("programmversion").InnerText);
            this.programmversion = root.SelectSingleNode("programmversion").InnerText;

            this.name = root.SelectSingleNode("name").InnerText;
            Console.WriteLine(name);
            XmlNodeList livers;

            this.liveries = new List<RepoLivery>();

            livers = root.SelectNodes("livery");
            foreach(XmlNode livery in livers)
            {
                RepoLivery rl = new RepoLivery();
                rl.aircraft = livery.SelectSingleNode("liveryaircraft").InnerText;
                rl.checksum = livery.SelectSingleNode("liverychecksum").InnerText;
                rl.downloadurl = livery.SelectSingleNode("liveryurl").InnerText;
                rl.name = livery.SelectSingleNode("liveryname").InnerText;
                rl.path = livery.SelectSingleNode("liverypath").InnerText;

                this.liveries.Add(rl);

                Console.WriteLine(rl.aircraft);
                Console.WriteLine(rl.checksum);
                Console.WriteLine(rl.downloadurl);
                Console.WriteLine(rl.name);
                Console.WriteLine(rl.path);
            }
        }

        public RepositoryOld(string programmversion, string name, Controller ctrl)
        {
            this.programmversion = programmversion;
            this.name = name;
            liveries = new List<RepoLivery>();
            controller = ctrl;
        }

        /// <summary>
        /// Creates a list of liveries based on pathes to their folders.
        /// TODO: Move this to controller.
        /// </summary>
        /// <param name="liveryPathes"></param>
        public void createList(List<string> liveryPathes)
        {
            foreach (string liverypath in liveryPathes)
            {
                if (!File.Exists(Path.Combine(liverypath, "description.lua")))
                {
                    continue; //only use pathes that contain description.lua
                }
                RepoLivery liv = new RepoLivery();
                //Get Path after "Liveries" folder
                string[] splitPath = liverypath.Split(Path.DirectorySeparatorChar);
                int usepathafterindex = -1;
                liv.downloadurl = "";
                for (int i = 0; i < splitPath.Length; i++)
                {
                    if (splitPath[i].Contains("Liveries"))
                    {
                        usepathafterindex = i;
                        liv.aircraft = splitPath[i + 1];
                    }
                    if ((usepathafterindex < i) && (usepathafterindex != -1))
                    {
                        liv.downloadurl = Path.Combine(liv.downloadurl, splitPath[i]);
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

        public static void AddAttributeToRepoFile(StringBuilder sb, string attribute, string value)
        {
            sb.Append("<" + attribute + ">");
            sb.Append(value);
            sb.AppendLine("</" + attribute + ">");
        }

        
        /// <summary>
        /// saves the XML as well as a compressed version of all the skins.
        /// TODO: Move to Controller.
        /// </summary>
        /// <param name="path"></param>
        public void saveRepo(string path)
        {
            string folderpath = Path.GetDirectoryName(path);
            Console.WriteLine(folderpath);
            //Creating of the Repo-File starts here. File will include information on programmversion, Repo Name and a list of all the liveries in the repo
            //List will include livery name, aircraft and downloadurl.
            //file will be in a DOM Model
            //seperation by line is recommended for human readability, programm will do it.
            StringBuilder repoFileContent = new StringBuilder();
            repoFileContent.AppendLine("<repository>"); //start of repo information

            AddAttributeToRepoFile(repoFileContent, "programmversion", this.programmversion);
            AddAttributeToRepoFile(repoFileContent, "name", this.name);

            foreach(RepoLivery rl in liveries)
            {
                repoFileContent.AppendLine("<livery>");
                AddAttributeToRepoFile(repoFileContent, "liveryname", rl.name);
                AddAttributeToRepoFile(repoFileContent, "liverypath", rl.path);
                AddAttributeToRepoFile(repoFileContent, "liveryurl", rl.downloadurl);
                AddAttributeToRepoFile(repoFileContent, "liveryaircraft", rl.aircraft);
                AddAttributeToRepoFile(repoFileContent, "liverychecksum", rl.checksum);

                repoFileContent.AppendLine("</livery>");
            }

            repoFileContent.AppendLine("</repository>");

            try
            {
                File.WriteAllText(path, repoFileContent.ToString());
            } catch (Exception e)
            {
                MessageBox.Show("Error: Writing Repositoryfile failed.");
                Console.WriteLine(e.ToString());
                return;
            }

            //zipping of liveries starts here:
            int i = 0;
            double percentage = 0;
            controller.setProgressBar(1);
            controller.FormEnabled(false);
            foreach (RepoLivery liv in liveries)
            {
                percentage = (double)(i) / liveries.Count;
                i++;
                controller.setProgressBar((int)(percentage * 100));
                Console.WriteLine("Test:" + liv.path);
                if (!Directory.Exists(Path.Combine(folderpath, liv.aircraft)))
                {
                    Directory.CreateDirectory(Path.Combine(folderpath, liv.aircraft));
                }
                if (File.Exists(Path.Combine(folderpath, liv.downloadurl)))
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
            public string downloadurl { get; set; } //url the repository is located at in reference to the repo xml.
            public string checksum { get; set; }  //calculated checksum of the complete livery folder. *TODO*: Implement checksum calculation.
            public string path { get; set; } // path the zip will be uncompressed at (saved games/DCS/liveries/*ACType*/*Liveryname* 
            //currently, path is redundant with downloadurl - but I keep it because it might helps for future versions with a different architecture on the download part.
            public string name { get; set; } //name of the livery as stated in the description.lua
            public string aircraft { get; set; }  //name of the aircraft this livery is made for.
        }
    }
}
