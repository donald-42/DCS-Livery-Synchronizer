using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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
        //Category, the livery belongs to. 
        public string category;

        public string Status;

        public override string ToString()
        {
            return $"path: {this.path}\nname: {this.name}\naircraft: {this.aircraft}\ncountries: {this.countries}";
        }

        public string CalculateChecksum(Settings settings)
        {
            var path = Path.Combine(settings.dcssavedgames, "Liveries", this.path);
            // assuming you want to include nested folders
            var files = Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly)
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
    }
}
