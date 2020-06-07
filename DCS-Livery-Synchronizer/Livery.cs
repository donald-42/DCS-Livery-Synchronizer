﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCS_Livery_Synchronizer
{
    /// <summary>
    /// Represents all Information necessary about an installed or to-be installed livery
    /// </summary>
    class Livery
    {
        //path to the directory of the livery
        public string path;
        //Name as stated in the description.lua
        public string name;
        //Aircraft Type as described by the file path
        public string aircraft;
        //Countries as stated in the description.lua
        public string countries;

        public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("path: " + path);
            sb.AppendLine("name: " + name);
            sb.AppendLine("aircraft: " + aircraft);
            sb.AppendLine("countries: " + countries);

            return sb.ToString();
        }
    }
}
