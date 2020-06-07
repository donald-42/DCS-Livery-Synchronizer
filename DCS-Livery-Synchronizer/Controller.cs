using DCS_Livery_Synchronizer.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace DCS_Livery_Synchronizer
{
    /// <summary>
    /// Does most of the work. Handles the programm settings, provides additional functionality to the form objects, executes the sync and downloading.
    /// </summary>
    class Controller
    {
        private string roamingPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DCSLiveriesSynchronizer"); //Path to the programm settings file in appdata/roaming
        private string savefileName = "settings.xml"; //Name of the settings file
        private string savefilePath;

        private List<Livery> installedLiveries;

        private Settings settings;

        public Controller()
        {
            savefilePath = Path.Combine(roamingPath, savefileName);
            installedLiveries = new List<Livery>();
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
                        System.Console.WriteLine(livpath);
                        if (File.Exists(Path.Combine(livpath, "description.lua")))
                        {
                            string descriptionpath = Path.Combine(livpath, "description.lua");
                            //System.Console.WriteLine(Path.GetFileName(livpath));
                            Livery livery = new Livery();
                            livery.aircraft = aircrafttype;
                            livery.path = livpath;
                            livery.name = GetNameFromLua(descriptionpath);
                            livery.countries = GetCountriesFromLua(descriptionpath);
                            if(livery.name == null)
                            {
                                livery.name = ""; // no livery name or not readable, might set to unknown
                            }
                            if(livery.countries == null)
                            {
                                livery.countries = ""; // no countries set or not readable, this should stand for all countries
                            }

                            Console.WriteLine(livery.ToString());
                            installedLiveries.Add(livery);
                        }
                    }
                }
            }
        }

        public static string GetNameFromLua(string luaFile)
        {
            try
            {
                string[] lines = File.ReadAllLines(luaFile);

                int countBraces = 0;
                foreach (string line in lines)
                {
                    string linewithoutcomment = line;
                    for(int i = 1; i<line.Length; i++)
                    {
                        if (line[i-1] == '-' && line[i] == '-') //comment, ignore everything after this.
                        {
                            linewithoutcomment = line.Substring(0, i - 1);
                        }
                    }

                    if (countBraces == 0 && linewithoutcomment.ToLower().Contains("name") && linewithoutcomment.Contains("="))
                    {
                        return linewithoutcomment.Split('=')[1].Trim(); //return Liveryname from Lua
                    }

                    foreach (char c in linewithoutcomment)
                    {
                        if (c == '{')
                            countBraces++;
                        else if (c == '}')
                            countBraces--;
                    }
                }
                return null;

            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
                return null;
            }
        }

        public static string GetCountriesFromLua(string luaFile)
        {
            try
            {
                string[] lines = File.ReadAllLines(luaFile);

                int countBraces = 0;
                foreach (string line in lines)
                {
                    string linewithoutcomment = line;
                    for (int i = 1; i < line.Length; i++)
                    {
                        if (line[i - 1] == '-' && line[i] == '-') //comment, ignore everything after this.
                        {
                            linewithoutcomment = line.Substring(0, i - 1);
                        }
                    }



                    if (countBraces == 0 && linewithoutcomment.ToLower().Contains("countries") && linewithoutcomment.Contains("="))
                    {
                        return linewithoutcomment.Split('=')[1].Trim(); //return Countries String from lua
                    }

                    foreach (char c in linewithoutcomment)
                    {
                        if (c == '{')
                            countBraces++;
                        else if (c == '}')
                            countBraces--;
                    }
                }
                return null;
            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
