using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DCS_Livery_Synchronizer
{
    /// <summary>
    /// Stores the Programm settings
    /// </summary>
    public class Settings
    {
        [XmlAttribute]
        public string version;
        [XmlAttribute]
        public string dcssavedgames;        
    }


}
