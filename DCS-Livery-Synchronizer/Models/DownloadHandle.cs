using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCS_Livery_Synchronizer
{
    public class DownloadHandle
    {
        public readonly Repository Repository;
        public readonly Livery Livery;

        public readonly string DestinationFolder;

        public string Status;

        public DownloadHandle(Repository repository, Livery livery, string destinationFolder)
        {
            this.Repository = repository;
            this.Livery = livery;
            this.DestinationFolder = destinationFolder;
        }
    }

    //public enum DownloadStatus
    //{
    //    Unknown,
    //    NotInstalled,
    //    Downloading,
    //    Unpacking,
    //    Installed,
    //    NotUpToDate,
    //    Error,
    //}
}
