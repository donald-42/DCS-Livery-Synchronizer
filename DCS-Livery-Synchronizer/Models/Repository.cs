using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DCS_Livery_Synchronizer
{
    /// <summary>
    /// Data Representation of a repository. Can in theory represent both online and local livery-sets.
    /// </summary>
    public class Repository
    {
        string programmversion; //stored in the xml repositorys for compatibilty reasons.
        string name; //name of the repository
        List<Livery> liverylist; //list that stores all liverys of this repo.
        string path; //path to the livery - either online path or local  

        /// <summary>
        /// Create new empty repository
        /// </summary>
        public Repository()
        {
            liverylist = new List<Livery>();
        }

        /// <summary>
        /// Create new repository
        /// </summary>
        /// <param name="version">Programmversion repository was created with</param>
        /// <param name="name">Name of the repository</param>
        public Repository(string version, string name)
        {
            this.programmversion = version;
            this.name = name;
            liverylist = new List<Livery>();
        }

        /// <summary>
        /// Set the programmversion this repo has been created with. For compatibility reasons if repo changes
        /// </summary>
        /// <param name="version">Programmversion repo was created with</param>
        public void SetProgrammVersion(string version)
        {
            programmversion = version;
        }

        /// <summary>
        /// Set repository name (aka readable name, whatever you fancy)
        /// </summary>
        /// <param name="name">name of repository</param>
        public void SetName(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Sets the path to the livery, either url or local filesystem path.
        /// </summary>
        /// <param name="path">path to the repoxml</param>
        /// <returns>true if path set successfully, false if either url not valid or file not exists.</returns>
        public bool SetPath(string path)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(path, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!result) //no valid online url
            {
                if (!File.Exists(path)) //no valid local path
                {
                    return false;
                }
            }
            this.path = path;
            return true;
        }

        /// <summary>
        /// Adds livery to the liverylist
        /// </summary>
        /// <param name="livery">Livery to add</param>
        public void AddLivery(Livery livery)
        {
            if(!liverylist.Contains(livery)) //do not add items twice
                liverylist.Add(livery);
        }

        /// <summary>
        /// Delets all references in the liverylist. 
        /// </summary>
        public void ClearLiveries()
        {
            liverylist.Clear();
        }

        /// <summary>
        /// Returns the liverylist.
        /// </summary>
        /// <returns></returns>
        public List<Livery> GetLiveries()
        {
            return liverylist;
        }

        public Livery GetLivery(string aircrafttype, string liveryname)
        {
            foreach(Livery lv in liverylist)
            {
                if((lv.aircraft.Equals(aircrafttype)) && (lv.name.Equals(liveryname)))
                {
                    return lv;
                }
            }
            return null;
        }

        public Livery GetLivery(string path)
        {
            foreach(Livery lv in liverylist)
            {
                if (lv.path.Equals(path))
                {
                    return lv;
                }
            }
            return null;
        }

    }
}
