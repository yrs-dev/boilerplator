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

/* TODO:
 * AnalyzeAttributeLabel : Method structure & structure for storing data
 * 
 * AnalyzeMethodLabel : Method structure & structure for storing data
 * 
 * AnalyzeInheritance : <data id=""> 
 *  --> Every Class object need id
 * 
 */

namespace CodeGenerator.Reader
{
    public class Reader : CommonInterfaces.IReader
    {
        // Main method
        static void Read(string filepath)
        {
            XmlReader reader = null;

            try
            {
                // Reader Object for read Graphml File
                reader = new XmlTextReader(filepath);

                while (reader.Read())
                {
                    UML_Class graphClass = AnalyzeNodeLabel(reader);
                    graphClass.umlAttributes = AnalyzeAttributeLabel(reader);
                    graphClass.umlMethods = AnalyzeMethodLabel(reader);
                }
            }
            finally
            {

            }
        } 

        // Method gets the name of the class
        public static UML_Class AnalyzeNodeLabel(XmlReader reader)
        {
            string className = "";

            // new Class object 
            UML_Class classObject = new UML_Class(className);

            // XMLReader object method 
            while (reader.Read())
            {
                // only need the <y:NodeLabel> Tag InnerText
                if (reader.Name == "y:NodeLabel" && reader.NodeType == XmlNodeType.Element)
                {
                    className = getClassName(reader.ReadSubtree());
                    classObject.name = className;
                }
            }

            // Output UML_Class object
            return classObject;
        }

        // Partial method of AnalyzeNodeLabel
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


        // Method gets the Attributes for each class
        public static List<UML_Attribute> AnalyzeAttributeLabel (XmlReader reader)
        {
            return null;
        }

        // Method gets the Methods for each class
        public static List<UML_Method> AnalyzeMethodLabel (XmlReader reader)
        {
            return null;
        }

         public CodeGenerator.Datamodel.Datamodel getDatamodel(string filePath)
         {
             throw new NotImplementedException();
         }


    }
}
