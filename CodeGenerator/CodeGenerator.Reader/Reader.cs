using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonInterfaces;
using System.Xml;
using System.Xml.Linq;

namespace CodeGenerator.Reader
{
    public class Reader : IReader
    {
        public static string getValue(XmlReader reader)
        {
            string t = "";
            while (reader.Read())
            {
                if (reader.HasValue)
                {
                    t += reader.Value;
                }
            }
            return t;
        }

        public Datamodel getDatamodel(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}