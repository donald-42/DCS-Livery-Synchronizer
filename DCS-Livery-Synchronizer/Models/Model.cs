using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCS_Livery_Synchronizer
{
    /// <summary>
    /// Class that represents all Data Elements (in subclasses) and allows sanitized access to them.
    /// </summary>
    public class Model
    {
        private string onlineRepoBaseAddress; //URL to the directory of online repo. 

        public readonly Repository OnlineRepository;
        public readonly Repository LocalRepository;
        public readonly Settings Settings;

        public Model()
        {
            this.OnlineRepository = new Repository();
            this.LocalRepository = new Repository();
            this.Settings = new Settings();
        }

        /// <summary>
        /// Set the path to the folder in which the onlinerepo xml is located.
        /// </summary>
        /// <param name="url"></param>
        public void SetOnlineRepoBaseAddress(string url)
        {
            onlineRepoBaseAddress = url;
        }

        public string GetOnlineRepoBaseAddress()
        {
            return onlineRepoBaseAddress;
        }

        ///// <summary>
        ///// Returns the currently active online repository. Could be empty if no repo is active.
        ///// </summary>
        ///// <returns></returns>
        //public Repository GetOnlineRepository()
        //{
        //    return OnlineRepo;
        //}

        ///// <summary>
        ///// Returns the local repository. Gets initialized after program start.
        ///// </summary>
        ///// <returns></returns>
        //public Repository GetLocalRepository()
        //{
        //    return LocalRepo;
        //}

        ///// <summary>
        ///// Gets the currently active Settings-Model. Gets initialized at programm start.
        ///// </summary>
        ///// <returns></returns>
        //public Settings GetSettings()
        //{
        //    return Settings;
        //}

        /// <summary>
        /// Sets a reference to a new Settings Model
        /// </summary>
        /// <param name="settings">Reference to settingsobject</param>
        public void SetSettings(Settings settings)
        {
            typeof(Model).GetField(nameof(Settings)).SetValue(this, settings);
        }
    }
}
