using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCS_Livery_Synchronizer
{
    //declare Events below:
    public delegate void NewErrorEventHandler(object sender, EventArgs e);
    public delegate void ModelChangedEventHandler(object sender, EventArgs e);
    public delegate void InstallCompletedEventHandler(object sender, EventArgs e);
    public delegate void InstallProgressChangedEventHandler(object sender, EventArgs e);
    public delegate void RepoCreationProgressChangedEventHandler(object sender, EventArgs e);
    public delegate void RepoCreationCompletedEventHandler(object sender, EventArgs e);
    /// <summary>
    /// rewritten Controller component. Controller is in charge of all Changes to database and informs view with 
    /// events of updates in model. 
    /// </summary>
    public class NewController
    {
        public const string repositoryVersion = "0.1";
        public readonly Model Model;
        private InstallController controllerInstall;
        private ControllerSettings controllerSettings;
        private ControllerLocal controllerLocal;
        private string currentErrorMessage; //Last thrown error message.

        public event NewErrorEventHandler NewError;
        public event ModelChangedEventHandler ModelChanged;
        public event InstallCompletedEventHandler InstallCompleted;
        public event InstallProgressChangedEventHandler InstallProgressChanged;
        public event RepoCreationProgressChangedEventHandler RepoCreationProgressChanged;
        public event RepoCreationCompletedEventHandler RepoCreationCompleted;

        public NewController()
        {
            this.Model = new Model();
            controllerInstall = new InstallController(this);
            controllerSettings = new ControllerSettings(this);
            controllerLocal = new ControllerLocal(this);
        }

        /// <summary>
        /// Returns current download progress. 
        /// </summary>
        /// <returns>Download Progress, always between 0 and 100</returns>
        public int getDownloadProgress()
        {
            return controllerInstall.getDownloadProgress();
        }

        public int GetRepoCreationProgress()
        {
            return controllerLocal.getRepoCreationProgress();
        }


        /// <summary>
        /// Creates a new Repository at the given Path Asynchron.
        /// </summary>
        /// <param name="liveries"></param>
        /// <param name="path">Path where repository gets saved. Needs to be empty</param>
        /// <param name="name">Name of the repository. Also used as filename for the xml.</param>
        public void CreateLocalRepositoryAsync(List<Livery> liverylist, string path, string name)
        {
            controllerLocal.CreateLocalRepositoryAsync(liverylist, path, name);
        }



        /// <summary>
        /// Set the progress of the current download and raises an Event.
        /// </summary>
        /// <param name="progress">current progress, should be between 0 and 100</param>
        public void setDownloadProgress(int progress)
        {
            controllerInstall.setDownloadProgress(progress);
            OnInstallationProgressChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Event raised if progress of repo creation is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void onRepoCreationProgressChanged(object sender, EventArgs e)
        {
            RepoCreationProgressChanged?.Invoke(sender, e);
        }

        /// <summary>
        /// Event raised if Repo Creation is completed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnRepoCreationCompleted(object sender, EventArgs e)
        {
            RepoCreationCompleted?.Invoke(sender, e);
        }

        /// <summary>
        /// Raises Event "ModelChanged". Call this whenever you update the model.
        /// </summary>
        /// <param name="sender">Object that raised the event, usually "this"</param>
        /// <param name="e">EventArgument. usually empty</param>
        public virtual void OnModelChanged(object sender, EventArgs e)
        {
            ModelChanged?.Invoke(sender, e);
        }

        /// <summary>
        /// Raises Event "ModelChanged". Call this whenever you update the model
        /// </summary>
        /// <param name="e">EventArgument. Usually empty</param>
        protected virtual void OnModelChanged(EventArgs e)
        {
            ModelChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Raises Event "InstallationProgressChanged". Gets called whenever the progress of the livery installation is updated.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnInstallationProgressChanged(object sender, EventArgs e)
        {
            InstallProgressChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Event raised if an pending installation is finally completed. 
        /// Recommend to block user interaction until installation is completed.
        /// </summary>
        /// <param name="sender">Object raising the event</param>
        /// <param name="e">EventArgs parameter</param>
        public virtual void OnInstallationCompleted(object sender, EventArgs e)
        {
            InstallCompleted?.Invoke(sender, e);
        }

        /// <summary>
        /// Raises event "NewError". Call this whenever you want to inform view about an error. 
        /// You should set Error Message before with "SetCurrentErrorMessage".
        /// </summary>
        /// <param name="sender">Object raising the Event</param>

        public virtual void OnNewError(object sender, EventArgs e)
        {
            NewError?.Invoke(sender, e);
        }

        /// <summary>
        /// Raises event "NewError". Call this whenever you want to inform view about an error. 
        /// You should set Error Message before with "SetCurrentErrorMessage".
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnNewError(EventArgs e)
        {
            NewError?.Invoke(this, e);
        }

        /// <summary>
        /// Get latest Error Message.
        /// </summary>
        /// <returns>Current Error Message</returns>
        public string GetCurrentErrorMessage()
        {
            return currentErrorMessage;
        }

        /// <summary>
        /// Set Error-Message. Also raises the Event "OnNewError" to inform view etc.
        /// </summary>
        /// <param name="message">Errormessage.</param>
        public void SetCurrentErrorMessage(string message)
        {
            //TODO: implement error log.
            currentErrorMessage = message;
            OnNewError(EventArgs.Empty);
        }
        
        /// <summary>
        /// Tries to read online repository xml. 
        /// </summary>
        /// <param name="url">URL to the online repository</param>
        public void LoadOnlineRepository(string url)
        {
            controllerInstall.LoadOnlineRepository(url);
            OnModelChanged(EventArgs.Empty);
        }

        ///// <summary>
        ///// Get the databasis used by this controller.
        ///// </summary>
        ///// <returns>Current Databasis represented with Modelclasses. All accessible via this Object.</returns>
        //public Model Model()
        //{
        //    return Model;
        //}

        /// <summary>
        /// Loads settings from the settings.xml file in roaming. If no file exists, creates one with default values.
        /// </summary>
        public void LoadSettings()
        {
            controllerSettings.LoadSettings();
            OnModelChanged(EventArgs.Empty);
        }

        /// <summary>
        /// Set the path to the dcs savegame folder. False if folder not exists or not valid
        /// </summary>
        /// <param name="path"></param>
        public bool SetPathToSavegame(string path)
        {
            var result = controllerSettings.SetPathToSavegame(path);
            OnModelChanged(EventArgs.Empty);
            return result;
        }

        /// <summary>
        /// Saves the current active settings to the settings file in roaming folder
        /// </summary>
        public void SaveSettings()
        {
            controllerSettings.SaveSettings();
        }

        /// <summary>
        /// Loads local repository according to the Path in Settings Model into the Model. 
        /// </summary>
        public void LoadLocalRepository()
        {
            controllerLocal.LoadLocalRepository();
            OnModelChanged(EventArgs.Empty);
        }

        /// <summary>
        /// Method to install all liverys within liverylist. Method will only install liverys which have a valid url path.
        /// </summary>
        /// <param name="liverylist">Handle to a list of liveries that will be installed</param>
        public void InstallLiveriesAsync(List<Livery> liverylist)
        {
            controllerInstall.InstallLiveriesAsync(liverylist);
        }
        public void DownloadAndInstallLiveries(List<DownloadHandle> downloads)
        {
            controllerInstall.DownloadAndInstallLiveriesAsync(downloads);
        }

        /// <summary>
        /// Function that checks a (online) livery against the local repository set. 
        /// </summary>
        /// <param name="livery"></param>
        /// <returns>0 = not installed, 1 = installed but different, 2 = installed and identical</returns>
        public int  GetLiveryInstallStatus(Livery livery)
        {
            if (Model.LocalRepository.GetLiveries().Count == 0)
            {
                return 0;
            }

            var status = 0;

            foreach(Livery locallivery in Model.LocalRepository.GetLiveries())
            {
                if ((locallivery.aircraft.Equals(livery.aircraft)) && (locallivery.path.Equals(livery.path)))
                {
                    string checksum = locallivery.CalculateChecksum(this.Model.Settings);

                    if (checksum.Equals(livery.checksum))
                    {
                        //Identical, as Checksum are the same
                        return 2;
                    }
                    else
                    {
                        //different, same unique Identifiers but files are different
                        return 1;
                    }
                }
            }
            return status;
        }

        public string InstallStatusToDisplayString(int status)
        {
            switch (status)
            {
                case 0: return "Not installed";
                case 1: return "Update available";
                case 2: return "Installed";
                default:
                    throw new Exception("Unknown status: " + status);
            }
        }
    }
}
