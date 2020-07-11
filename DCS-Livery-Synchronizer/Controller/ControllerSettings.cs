using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace DCS_Livery_Synchronizer
{
    /// <summary>
    /// Class controlling all settings of the programm. Including validity Checks etc.
    /// </summary>
    class ControllerSettings
    {
        NewController parent;
        string settingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DCSLiveriesSynchronizer","settings.xml"); //Path to the programm settings file in appdata/roaming

        /// <summary>
        /// Creates settingscontroller. Settings are loaded by default in the constructor.
        /// </summary>
        /// <param name="parent"></param>
        public ControllerSettings(NewController parent)
        {
            this.parent = parent;
            LoadSettings();
        }

        /// <summary>
        /// Method that takes the settings path and either loads Settings or loads default if settings do not exist.
        /// </summary>
        public void LoadSettings()
        {
            //Check if programm folder exists, if not, create it.
            var roamingPath = Path.GetDirectoryName(settingsPath);
            if (!Directory.Exists(roamingPath))
            {
                //create file
                Directory.CreateDirectory(roamingPath);
                System.Console.WriteLine("Folder does not exist");
            }

            var settings = parent.GetModel().GetSettings();

            //Check if settings file exists, if not, create it and write defaults
            if (!File.Exists(settingsPath))
            {
                settings.version = NewController.programmversion;
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
                    parent.SetCurrentErrorMessage("Path to saved games\\DCS or saved games\\DCS.openbeta not found. Please specifiy in Settings.");
                    Console.WriteLine("DCS savedgames path not found.");
                }
                SaveSettings();
            }
            else //File exists, loading values from xml
            {
                var serializer = new XmlSerializer(typeof(Settings));
                using (var reader = XmlReader.Create(settingsPath))
                {
                    settings = (Settings)serializer.Deserialize(reader);
                    parent.GetModel().SetSettings(settings);
                    //System.Console.WriteLine("Version is {0}", settings.version);
                    //System.Console.WriteLine("Path is {0}", parent.GetModel().GetSettings().dcssavedgames);
                }
            }
        }

        /// <summary>
        /// Sets path to the DCS savegames folder
        /// </summary>
        /// <param name="path">path to DCS Savegames Folder</param>
        /// <returns>true if path exists and valid, false if not</returns>
        public bool SetPathToSavegame(string path)
        {
            if (!Directory.Exists(path))
            {
                return false;
            } 

            parent.GetModel().GetSettings().dcssavedgames = path;
            return true;
        }

        /// <summary>
        /// Saves current model.settings to the settings.xml in roaming
        /// </summary>
        public void SaveSettings()
        {
            var settings = parent.GetModel().GetSettings();
            if(settings == null)
            {
                return;
            }
            var serializer = new XmlSerializer(settings.GetType());
            using (var writer = XmlWriter.Create(settingsPath)) //save settings to the settings.xml in roaming
            {
                serializer.Serialize(writer, settings);
            }
        }
    }
}
