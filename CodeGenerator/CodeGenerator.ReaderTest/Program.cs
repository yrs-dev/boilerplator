using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using CodeGenerator.Reader;

namespace CodeGenerator.ReaderTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string filepath = "";
            XmlTextReader reader = null;

            try
            {
                reader = new XmlTextReader(filepath);
                while (reader.Read())
                {
                    if (reader.Name == "y:NodeLabel" && reader.NodeType == XmlNodeType.Element)
                    {
                        Console.WriteLine("Class Name : {0}", CodeGenerator.Reader.Reader.getValue(reader.ReadSubtree()));
                    }
                }
            }
            finally
            {
            }
        }
    }
}
