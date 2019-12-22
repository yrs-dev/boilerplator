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
 *  --> Parents : Class or Interface?
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
           // UML_Attribute attribute = new UML_Attribute();
            List<UML_Attribute> classAttributes = new List<UML_Attribute>();
            while (reader.Read())
            {
                if (reader.Name == "y:AttributeLabel" && reader.NodeType == XmlNodeType.Element)
                {
                    string readerValue = reader.ReadInnerXml();
                    UML_Attribute attributte = getAccesmodifier(readerValue);

                    classAttributes.Add(attributte);

                    // +name:string
                    //string[] readerArray = readerValue.Split(' ').Select(x => x.Trim(':')).ToArray();
                    // ["+","name",":","string"]

                    //for (int i = 0; i < readerArray.Length; i++)
                    //{
                    //    UML_Attribute attribute = new UML_Attribute(readerArray.GetValue(i));
                    //    classAttributes.Add(attribute);
                    //}
                }
            }
            return classAttributes;
        }

        static UML_Attribute getAccesmodifier(string attr)
        {
            string modifierPublic = "+";
            string modifierPrivate = "-";
            string modifierProtected = "#";

            UML_Attribute attribute = new UML_Attribute();

            string[] readerValueArray = attr.Split(':').ToArray();

            if (readerValueArray[0].Contains(modifierPublic) == true)
            {
                attribute.accessModifier = '+';
                attribute.name = readerValueArray[0].Trim('+');
                attribute.type = readerValueArray[1];
            }

            if (readerValueArray[0].Contains(modifierPrivate) == true)
            {
                attribute.accessModifier = '-';
                attribute.name = readerValueArray[0].Trim('-');
                attribute.type = readerValueArray[1];
            }

            if (readerValueArray[0].Contains(modifierProtected) == true)
            {
                attribute.accessModifier = '#';
                attribute.name = readerValueArray[0].Trim('#');
                attribute.type = readerValueArray[1];
            }

            else
            {
                attribute.accessModifier = '?';
                attribute.name = readerValueArray[0];
                attribute.type = readerValueArray[1];
            }

            return attribute;
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
