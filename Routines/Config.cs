using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Routines
{
    public class Config
    {

        public string ReadValue(string key)
        {
            string filepath = @"C:\Earcu\configfiles\migrationtools.config";

            if (!File.Exists(filepath))
                filepath = filepath.Replace("C:", "E:");

            if (!System.IO.File.Exists(filepath))
            {
                throw new Exception(@"config file C:\Earcu\configfiles\migrationtools.config not found ");

            }

            System.Xml.XmlDocument d = new System.Xml.XmlDocument();
            try
            {
                d.Load(filepath);

                {
                    string xpath = @"/configuration/" + key + "/@value";
                    System.Xml.XmlNode? n = d.SelectSingleNode(xpath);
                    if (n != null)
                    {
                        return n.Value;
                    }
                    else
                    {
                        throw new Exception("key not found " + key);
                    }
                }

            }
            catch { }

            return "";
        }
    }
}

