using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DCS_Livery_Synchronizer.Models;
using System.Security.Cryptography;
using System.IO.Compression;

namespace DCS_Livery_Synchronizer
{
    class ControllerLocal
    {
        NewController parent;
        int repoCreationProgress = 0;
        public ControllerLocal(NewController parent)
        {
            this.parent = parent;
        }

        /// <summary>
        /// Current Progress of repo creation
        /// </summary>
        /// <returns>progress in percent, always between 0 and 100</returns>
        public int getRepoCreationProgress()
        {
            return Math.Max(0,Math.Min(100,repoCreationProgress));
        }


        /// <summary>
        /// Used to calculate checksums of complete folder. only looks at the .dds files
        /// </summary>
        /// <param name="livery"></param>
        /// <returns></returns>
        private string CalculateChecksum(Livery livery)
        {
            var path = Path.Combine(parent.GetModel().GetSettings().dcssavedgames, "Liveries", livery.path);
            // assuming you want to include nested folders
            var files = Directory.GetFiles(path, "*.dds", SearchOption.TopDirectoryOnly)
                                    .OrderBy(p => p).ToList();

            MD5 md5 = MD5.Create();

            for (int i = 0; i < files.Count; i++)
            {
                string file = files[i];

                // hash path
                string relativePath = file.Substring(path.Length + 1);
                byte[] pathBytes = Encoding.UTF8.GetBytes(relativePath.ToLower());
                md5.TransformBlock(pathBytes, 0, pathBytes.Length, pathBytes, 0);

                // hash contents
                byte[] contentBytes = File.ReadAllBytes(file);
                if (i == files.Count - 1)
                    md5.TransformFinalBlock(contentBytes, 0, contentBytes.Length);
                else
                    md5.TransformBlock(contentBytes, 0, contentBytes.Length, contentBytes, 0);
            }
            return BitConverter.ToString(md5.Hash).Replace("-", "").ToLower();
        }

        /// <summary>
        /// Loads local repository according to the Path in Settings Model into the Model. 
        /// </summary>
        public void LoadLocalRepository()
        {
            var path = parent.GetModel().GetSettings().dcssavedgames;

            if (!Directory.Exists(path))
            {
                parent.SetCurrentErrorMessage("Path to savedgames DCS folder does not exist.");
                return;
            }

            Repository localRepo = parent.GetModel().GetLocalRepository();
            localRepo.GetLiveries().Clear();

            //Reads all the existing liverys installed in the Saved game folder
            //only reads liverys with existing description.lua
            if (Directory.Exists(Path.Combine(path, "Liveries")))
            {
                foreach (String subdir in Directory.GetDirectories(Path.Combine(path, "Liveries")))
                {
                    string aircrafttype = Path.GetFileName(subdir);
                    foreach (String livpath in Directory.GetDirectories(subdir))
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
                            if (livery.name == null)
                            {
                                livery.name = ""; // no livery name or not readable, might set to unknown
                            }
                            if (livery.countries == null)
                            {
                                livery.countries = ""; // no countries set or not readable, this should stand for all countries
                            }
                            livery.checksum = CalculateChecksum(livery);
                            livery.url = livery.aircraft + "/" + livery.name + ".zip";
                            localRepo.AddLivery(livery);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Creates a new Repository at the given Path.
        /// </summary>
        /// <param name="liveries"></param>
        /// <param name="path">Path where repository gets saved. Needs to be empty</param>
        /// <param name="name">Name of the repository. Also used as filename for the xml.</param>
        public async void CreateLocalRepositoryAsync(List<Livery> liveries, string path, string name)
        {
            repoCreationProgress = 1;
            parent.onRepoCreationProgressChanged(this, EventArgs.Empty);
            //check if directory is empty. If not, abort.
            if (Directory.GetFileSystemEntries(path).Length != 0)
            {
                parent.SetCurrentErrorMessage("Target directory not empty! No files created");
                return;
            }

            //starting creating and saving the xml.

            var repoFileContent = new StringBuilder();
            repoFileContent.AppendLine("<repository>"); //start of repo information

            repoFileContent.AppendLine(AddAttributeToRepoFile("programmversion", parent.GetModel().GetSettings().version));
            repoFileContent.AppendLine(AddAttributeToRepoFile("name", name));

            foreach (Livery livery in liveries)
            {
                repoFileContent.AppendLine("<livery>");
                repoFileContent.AppendLine(AddAttributeToRepoFile("liveryname", livery.name));
                repoFileContent.AppendLine(AddAttributeToRepoFile("liverypath", livery.path));
                repoFileContent.AppendLine(AddAttributeToRepoFile("liveryurl", livery.url));
                repoFileContent.AppendLine(AddAttributeToRepoFile("liveryaircraft", livery.aircraft));
                repoFileContent.AppendLine(AddAttributeToRepoFile("liverychecksum", livery.checksum));

                repoFileContent.AppendLine("</livery>");
            }

            repoFileContent.AppendLine("</repository>");

            try
            {
                File.WriteAllText(Path.Combine(path,name + ".xml"), repoFileContent.ToString());
            }
            catch (Exception e)
            {
                parent.SetCurrentErrorMessage("Error: Writing Repositoryfile failed.");
                Console.WriteLine(e.ToString());
                return;
            }

            Console.WriteLine("starting download");
            //starting compression of zip files.
            var i = 1;
            foreach (Livery livery in liveries)
            {
                if (!Directory.Exists(Path.Combine(path, livery.aircraft)))
                {
                    Directory.CreateDirectory(Path.Combine(path, livery.aircraft));
                }
                if (File.Exists(Path.Combine(path, livery.path)))
                {
                    File.Delete(Path.Combine(path, livery.path));
                }
                await Task.Run(() => ZipFile.CreateFromDirectory(Path.Combine(parent.GetModel().GetSettings().dcssavedgames, "Liveries", livery.path), Path.Combine(path, livery.aircraft, Path.GetFileName(livery.path) + ".zip")));
                var percentage =  (double)(i) / liveries.Count * 100;
                repoCreationProgress = (int)percentage;
                parent.onRepoCreationProgressChanged(this, EventArgs.Empty);
                i++;
            }
            parent.OnRepoCreationCompleted(this, EventArgs.Empty);
        }
        
        /// <summary>
        /// Returns a string that has attribute as opening and closing tag in DOM format. Value in between
        /// </summary>
        /// <param name="attribute">Attribute for the opening and closing tags</param>
        /// <param name="value">Value in between the tags</param>
        /// <returns>String in DOM format. </returns>
        public static string AddAttributeToRepoFile(string attribute, string value)
        {
            var sb = new StringBuilder();
            sb.Append("<" + attribute + ">");
            sb.Append(value);
            sb.Append("</" + attribute + ">");
            return sb.ToString();
        }

    }
}
