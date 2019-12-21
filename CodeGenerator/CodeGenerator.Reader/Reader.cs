using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using CodeGenerator.Datamodel;
using System.Xml.Linq;
using System.IO;
using CommonInterfaces;

namespace CodeGenerator.Reader
{
    public class Reader : CommonInterfaces.IReader
    {
        public static UML_Class AnalyzeNodeLabel(XmlReader reader)
        {
            string className = "";
            UML_Class classObject = new UML_Class(className);
            while (reader.Read())
            {
                if (reader.Name == "y:NodeLabel" && reader.NodeType == XmlNodeType.Element)
                {
                    className = getClassName(reader.ReadSubtree());
                    classObject.name = className;
                }
            }
            return classObject;
        }

        public static string getClassName(XmlReader reader)
        {
            string className = "";
            while (reader.Read())
            {
                if (reader.HasValue)
                {
                    className += reader.Value;
                }
            }
            return className;
        }

        
        public static UML_Attribute AnalyzeAttributeLabel (XmlReader reader)
        {
            return null;
        }

        public static UML_Method AnalyzeMethodLabel (XmlReader reader)
        {
            return null;
        }

         public CodeGenerator.Datamodel.Datamodel getDatamodel(string filePath)
         {
             throw new NotImplementedException();
         }


    }
}
