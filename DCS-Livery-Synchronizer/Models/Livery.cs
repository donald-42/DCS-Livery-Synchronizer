using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCS_Livery_Synchronizer
{
    /// <summary>
    /// Represents all Information necessary about an installed or to-be installed livery
    /// </summary>
    public class Livery
    {
        //path to the .zip file if online repository. Will be null or empty if local livery.
        public string url;
        //path to the directory of the livery -- This is a unique Identifier
        public string path;
        //Name as stated in the description.lua
        public string name;
        //Aircraft Type as described by the file path
        public string aircraft;
        //Countries as stated in the description.lua
        public string countries;
        //Checksum of all files in the livery-folder. To verify this against any equal-named liverys
        public string checksum;

        public override string ToString()
        {
            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine("path: " + path);
            //sb.AppendLine("name: " + name);
            //sb.AppendLine("aircraft: " + aircraft);
            //sb.AppendLine("countries: " + countries);

            //return sb.ToString();
            return $"path: {this.path}\nname: {this.name}\naircraft: {this.aircraft}\ncountries: {this.countries}";
        }
    }
}
