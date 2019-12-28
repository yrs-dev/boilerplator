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
 * AnalyzeNodeLabel: Method structure & structure for storing data  [DONE]
 *  --> Add new *Id* property for *UML_Class* objects  [NOT STARTED YET]
 *      --> important for checking inheritance !!
 * 
 * AnalyzeAttributeLabel : Method structure & structure for storing data [DONE]
 *  --> new Method for **checkAccesmodifier** [IN PROCESS..]
 * 
 * AnalyzeMethodLabel : Method structure & structure for storing data [IN PROCESS..]
 *  --> fix Problem: method.type => IndexOutOfRangeException [DONE]
 *  --> parameters: **getParameter()** method refactor the logic & structure [IN PROCESS..]
 *      --> possible Exceptions: IndexOutOfRangeException => **getMethod()** -> *UML_Method* -> *object*.type  [RESEARCHING..]
 *  --> new Method for **checkAcessmodifier**   [NOT STARTED YET]
 * 
 * AnalyzeInheritance : <data id="">  [NOT STARTED YET]
 *  --> Every Class object need id
 *  --> Parents : Class or Interface?
 * 
 * 
 */

namespace CodeGenerator.Reader
{
    public class Reader : CommonInterfaces.IReader
    {

        // Main method
        public void Read(string filepath)
        {
            XmlReader reader = null;

            try
            {
                // Reader Object for read Graphml File
                reader = new XmlTextReader(filepath);

                while (reader.Read())
                {
                    Datamodel.Datamodel datamodel = new Datamodel.Datamodel();
                    UML_Base baseModel = checkInterfaceOrClass<UML_Base>(reader);

                    if (baseModel.GetType() == typeof(UML_Class) && baseModel != null)
                    {
                        UML_Class classModel = new UML_Class(baseModel.name) 
                        {
                            umlAttributes = AnalyzeAttributeLabel(reader),
                            umlMethods = AnalyzeMethodLabel(reader)
                        };
                        datamodel.umlClasses.Add(classModel);
                    }
                        
                    if (baseModel.GetType() == typeof(UML_Interface) && baseModel != null)
                    {
                        UML_Interface interfaceModel = new UML_Interface(baseModel.name)
                        { 
                            umlAttributes = AnalyzeAttributeLabel(reader),
                            umlMethods = AnalyzeMethodLabel(reader)
                        };
                        datamodel.umlInterfaces.Add(interfaceModel);
                    }
                }
            }
            finally
            {

            }
        } 

        public T checkInterfaceOrClass<T> (XmlReader reader) where T : CodeGenerator.Datamodel.UML_Base
        {
            //reader.Settings.IgnoreWhitespace = true;

            if (getName(reader).Contains("<<interface>>") || getName(reader).Contains("interface")
                || getName(reader).StartsWith("I") && getName(reader).Substring(0, 1).ToUpper().Equals(getName(reader)))
            {
                UML_Interface interfaceModel = new UML_Interface(getName(reader));
                return (T)Convert.ChangeType(interfaceModel, typeof(UML_Interface));
            }
            else
            {
                UML_Class classModel = new UML_Class(getName(reader));
                return (T)Convert.ChangeType(classModel, typeof(UML_Class));
            }
            return null;
        }

        // Method gets the name of the class
        public UML_Class AnalyzeNodeLabel(XmlReader reader)
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
                    className = getName(reader.ReadSubtree());
                    classObject.name = className;
                }
            }

            // Output UML_Class object
            return classObject;
        }

        // Partial method of AnalyzeNodeLabel
        string getName(XmlReader reader)
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
        public List<UML_Attribute> AnalyzeAttributeLabel (XmlReader reader)
        {
            List<UML_Attribute> classAttributes = new List<UML_Attribute>();
            while (reader.Read())
            {
                if (reader.Name == "y:AttributeLabel" && reader.NodeType == XmlNodeType.Element)
                {
                    // Storing text of the <y:AttributeLabel>
                    string readerValue = reader.ReadInnerXml();
                    // Method for parsing the attributes
                    classAttributes = getAttribute(readerValue);
                }
            }

            return classAttributes;
        }

        private List<UML_Attribute> getAttribute(string attr)
        {
            // Possible accessmodifiers
            string modifierPublic = "+";
            string modifierPrivate = "-";
            string modifierProtected = "#";

            List<UML_Attribute> listAttributes = new List<UML_Attribute>();
            List<string> readerValueArray = new List<string>();
            // Splitting the string input at whitespaces
            readerValueArray = System.Text.RegularExpressions.Regex.Split(attr, @"\s{2,}").ToList<string>();

            // Looping through all splitted string-elements
            foreach (string stringValue in readerValueArray)
            {
                UML_Attribute attribute = new UML_Attribute();

                // Separating name and type
                var kvp = stringValue.Split(':');
                // Cutting of whitespaces
                string current = kvp[1].Trim();

                // Provisionally checking accesmodifier
                if (readerValueArray.Any(s => s.StartsWith(modifierPublic)) == true)
                {
                    attribute.accessModifier = '+';
                    attribute.name = kvp[0].Trim('+', ' ');
                    attribute.type = current;
                }

                if (readerValueArray.Any(s => s.StartsWith(modifierPrivate)) == true)
                {
                    attribute.accessModifier = '-';
                    attribute.name = kvp[0].Trim('-', ' ');
                    attribute.type = current;
                }

                if (readerValueArray.Any(s => s.StartsWith(modifierProtected)) == true)
                {
                    attribute.accessModifier = '#';
                    attribute.name = kvp[0].Trim('#', ' ');
                    attribute.type = current;
                }
                listAttributes.Add(attribute);
            }

            return listAttributes;
        }

        // Method gets the Methods for each class
        public List<UML_Method> AnalyzeMethodLabel (XmlReader reader)
        {
            List<UML_Method> classMethods = new List<UML_Method>();
            while (reader.Read())
            {
                if (reader.Name == "y:MethodLabel" && reader.NodeType == XmlNodeType.Element)
                {
                    // Storing text of the <y:MethodLabel>
                    string readerValue = reader.ReadInnerXml();
                    // Method for parsing the methods
                    classMethods = getMethod(readerValue);
                }
            }
            return classMethods;
        }

        private List<UML_Method> getMethod(string methods)
        {
            // Possible accessmodifier
            string modifierPublic = "+";
            string modifierPrivate = "-";
            string modifierProtected = "#";

            List<UML_Method> listMethods = new List<UML_Method>();
            // Splitting the string input at whitespaces
            List<string> readerValueList = System.Text.RegularExpressions.Regex.Split(methods, @"\s{2,}").ToList<string>();

            // Looping through all splitted string-elements
            foreach (string stringValue in readerValueList)
            {
                UML_Method method = new UML_Method();
                // Separating name and type
                var tmp = stringValue.Split('(');

                // Provisionally checking accessmodifier
                if (readerValueList.Any(s => s.StartsWith(modifierPublic)) == true)
                {
                    method.accessModifier = '+';
                    method.name = tmp[0].Trim('+');
                    if (stringValue.Contains(':') == true)
                    {
                        method.type = tmp[1].Trim();
                    }
                    method.parameters = getParameter(stringValue);
                }

                if (readerValueList.Any(s => s.StartsWith(modifierPrivate)) == true)
                {
                    method.accessModifier = '-';
                    method.name = tmp[0].Trim('-');
                    if (stringValue.Contains(':') == true)
                    {
                        method.type = tmp[1].Trim();
                    }
                    method.parameters = getParameter(stringValue);
                }

                if (readerValueList.Any(s => s.StartsWith(modifierProtected)) == true)
                {
                    method.accessModifier = '#';
                    method.name = tmp[0].Trim('#');
                    if (stringValue.Contains(':') == true)
                    {
                        method.type = tmp[1].Trim();
                    }
                    method.type = tmp[1].Trim();
                    method.parameters = getParameter(stringValue);
                }

                else
                {
                    method.accessModifier = '\0';
                    method.name = tmp[0].Trim('(');
                    if (stringValue.Contains(':') == true)
                    {
                        var tmp2 = stringValue.Split(')');
                        method.type = tmp2[1].Trim(':');
                    }
                    method.parameters = getParameter(stringValue);
                }
               
                listMethods.Add(method);
            }

            return listMethods;
        }

        private List<UML_Parameter> getParameter(string value)
        {
            List<UML_Parameter> listParamters = new List<UML_Parameter>();
            int firstIndex = value.IndexOf('(');
            int lastIndex = value.IndexOf(')');

            if (lastIndex-firstIndex > 3)
            {
                foreach (var stringValue in value)
                {
                    var sections = value.Substring(firstIndex, lastIndex).Split(':');
                    UML_Parameter parameter = new UML_Parameter()
                    {
                        parameterName = sections[0].Trim('('),
                        parameterType = sections[1].Trim(':' , ')')
                    };
                    listParamters.Add(parameter);
                }
            }

            else
            {
                listParamters = null;
            }
            return listParamters;
        }

        public CodeGenerator.Datamodel.Datamodel getDatamodel(string filePath)
         {
             throw new NotImplementedException();
         }


    }
}
