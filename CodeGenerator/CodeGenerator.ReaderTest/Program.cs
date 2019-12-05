using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using CodeGenerator.Reader;
using CodeGenerator.Datamodel;

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
                    CodeGenerator.Datamodel.Datamodel data = new CodeGenerator.Datamodel.Datamodel();
                    UML_Class readClass = new UML_Class();
                    UML_Attribute readAttribute = new UML_Attribute();

                    if (reader.Name == "y:NodeLabel" && reader.NodeType == XmlNodeType.Element)
                    {
                        Console.WriteLine("Class Name : {0}", CodeGenerator.Reader.Reader.getValue(reader.ReadSubtree()));
                    }
                    if (reader.Name == "y:AttributeLabel" && reader.NodeType == XmlNodeType.Element)
                    {
                        string name = CodeGenerator.Reader.Reader.getValue(reader.ReadSubtree());
                        Console.WriteLine("Attribute Name : {0}", name);
                        readClass.name = name;
                    }
                    if (reader.Name == "y:MethodLabel" && reader.NodeType == XmlNodeType.Element)
                    {
                        Console.WriteLine("Method Names : {0}", CodeGenerator.Reader.Reader.getValue(reader.ReadSubtree()));
                    }
                }
            }
            finally
            {
            }
        }
    }
}
