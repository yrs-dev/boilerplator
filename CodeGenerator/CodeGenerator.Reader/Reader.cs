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
        public static string getValue(XmlReader xtr)
        {
            string t = "";
            while (xtr.Read())
            {
                if (xtr.HasValue)
                {
                    t += xtr.Value;
                }
            }
            return t;
        }
    }
}