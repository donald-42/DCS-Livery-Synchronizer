using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCS_Livery_Synchronizer
{
    /// <summary>
    /// Class to read and modify Description.lua from Liveries
    /// </summary>
    class Description
    {
        private string path;
        private string countries;
        private string name;
        public Description()
        {
            
        }

        public Description(string path)
        {
            if (File.Exists(path))
            {
                this.path = path;
                parse();
            } else
            {
                Console.WriteLine("Error: Description.lua does not exist!");
            }
        }

        public string GetName()
        {
            return name;
        }

        public string GetCountries()
        {
            return countries;
        }

        public void parse()
        {
            if (File.Exists(path))
            {
                try
                {
                    string[] lines = File.ReadAllLines(path);

                    int countBraces = 0;
                    foreach (string line in lines)
                    {
                        string linewithoutcomment = line;
                        for (int i = 1; i < line.Length; i++)
                        {
                            if (line[i - 1] == '-' && line[i] == '-') //comment detected in lua line, ignore everything after this.
                            {
                                linewithoutcomment = line.Substring(0, i - 1);
                            }
                        }


                        if (countBraces == 0 && linewithoutcomment.ToLower().Contains("name") && linewithoutcomment.Contains("="))
                        {
                            name = linewithoutcomment.Split('=')[1].Trim(); //read Liveryname from Lua
                        }

                        if (countBraces == 0 && linewithoutcomment.ToLower().Contains("countries") && linewithoutcomment.Contains("="))
                        {
                            countries =  linewithoutcomment.Split('=')[1].Trim(); //read Countries String from lua
                        }

                        foreach (char c in linewithoutcomment)
                        {
                            if (c == '{')
                                countBraces++;
                            else if (c == '}')
                                countBraces--;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
    }
}
