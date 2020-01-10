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
 * 
 * **AnalyzeNodeLabel** : Method structure & structure for storing data  [DONE ✔]
 *  --> Interface or Class Detection [DONE ✔]
 *      --> Unit Test for Interface [WORKING ✔]
 *      --> Unit Test for Class     [WORKING ✔]
 *  --> Add new *Id* property for *UML_Class* objects  [NOT STARTED YET ✘]
 *      --> important for checking inheritance !!
 * 
 * **AnalyzeAttributeLabel** : Method structure & structure for storing data [DONE]
 *  --> new Method for **checkAccesmodifier** [IN PROCESS..]
 * 
 * **AnalyzeMethodLabel** : Method structure & structure for storing data [IN PROCESS..]
 *  --> fix Problem: method.type => IndexOutOfRangeException [DONE ✔]
 *  --> parameters: **getParameter()** method refactor the logic & structure [DONE ✔]
 *      --> possible Exceptions: IndexOutOfRangeException => **getMethod()** -> *UML_Method* -> *object*.type  [RESEARCHING..]
 *  --> new Method for **checkAcessmodifier**   [IN PROCESS..]
 * 
 * **AnalyzeInheritance** : <data id="">  [NOT STARTED YET ✘]
 *  --> Every Class object need id [IN PROCESS..]
 *  --> Parents : Class or Interface? [DONE ✔]
 * 
 */

namespace CodeGenerator.Reader
{
    public class Reader : CommonInterfaces.IReader
    {
        #region Members
        public string filepath { get; set; }
        #endregion

        #region Constructor
        public Reader(string filepath)
        {
            this.filepath = filepath;
        }
        #endregion

        //Main method
        public Datamodel.Datamodel ReadGraphml(string filepath)
        {
            XmlTextReader reader = null;

            try
            {
                // Reader Object for read Graphml File
                reader = new XmlTextReader(filepath);
                Datamodel.Datamodel datamodel = null;
                while (reader.Read())
                {
                    datamodel = new Datamodel.Datamodel();
                    List<UML_Base> baseModel = AnalyzeNode(reader, filepath);
                    foreach (UML_Base item in baseModel)
                    {
                        if (baseModel.GetType() == typeof(UML_Class) && baseModel != null)
                        {
                            UML_Class classModel = new UML_Class(item.name, item.id)
                            {
                                umlAttributes = AnalyzeAttributeLabel(reader),
                                umlMethods = AnalyzeMethodLabel(reader)
                            };
                            datamodel.umlClasses.Add(classModel);
                        }

                        if (baseModel.GetType() == typeof(UML_Interface) && baseModel != null)
                        {
                            UML_Interface interfaceModel = new UML_Interface(item.name, item.id)
                            {
                                umlAttributes = AnalyzeAttributeLabel(reader),
                                umlMethods = AnalyzeMethodLabel(reader)
                            };
                            datamodel.umlInterfaces.Add(interfaceModel);
                        }
                    }
                }

                return datamodel;
            }
            finally
            {
            }
        }

        public List<UML_Base> AnalyzeNode (XmlReader reader, string filepath)
        {
        
            List<string> id = getNodeAttributeValue(filepath);
            List<UML_Base> baseModelList = getModel(reader,id);

            return baseModelList;
        }

        List<string> getNodeAttributeValue(string file)
        {
            List<string> id = null;
            XmlTextReader xmlReader = null;

            XmlParserContext context = new XmlParserContext(null, null, "node", XmlSpace.None);
            xmlReader = new XmlTextReader(filepath, XmlNodeType.Element, context);

            while (xmlReader.Read())
            {
                xmlReader.MoveToAttribute("id");
                while (xmlReader.ReadAttributeValue())
                {
                    if (xmlReader.Value.Contains('n'))
                    {
                        id.Add(xmlReader.Value);
                    }
                }
            }
            return id;
        }

        List<UML_Base> getModel(XmlReader reader, List<string> id)
        {
            List<UML_Base> baseModels = new List<UML_Base>();
            while (reader.Read())
            {
                if (id != null)
                {
                    foreach (var item in id)
                    {
                        UML_Base baseModel = AnalyzeNodeLabel<UML_Base>(reader, item);
                        baseModels.Add(baseModel);
                    }
                }
            }
            return baseModels;
        }

   

        //public UML_Class AnalyzeNodeForClass (string className)
        //{
        //    XmlReader xmlReader = new XmlTextReader(filepath);
        //    UML_Class classModel = new UML_Class();
        //    classModel.name = className;

        //    bool canReadNode = xmlReader.Name == "node\"" && xmlReader.NodeType == XmlNodeType.Element;

        //    while (xmlReader.Read())
        //    {
        //        if (canReadNode)
        //        {
        //            classModel.id = xmlReader.GetAttribute("id");
        //        }
        //    }

        //    return classModel;
        //}
        //UML_Interface AnalyzeNodeForInterface(string interfaceName, string filepath)
        //{
        //    UML_Interface interfaceModel = new UML_Interface();
        //    XmlReader xmlReader = new XmlTextReader(filepath);
        //    interfaceModel.name = interfaceName;

        //    bool canReadNode = xmlReader.Name.Contains("node") && xmlReader.NodeType == XmlNodeType.Element;
            
        //    while (xmlReader.Read())
        //    {
        //        if (canReadNode)
        //        {
        //            interfaceModel.id = xmlReader.GetAttribute("id");
        //        }
        //    }
        //    return interfaceModel;
        //}

        bool checkInheritance(XmlReader reader)
        {
            return false;
        }

        string getInheritance(XmlReader reader, UML_Base model)
        {
            return null;
        }

        public T AnalyzeNodeLabel<T> (XmlReader reader, string nodeId) where T : CodeGenerator.Datamodel.UML_Base
        {
            while (reader.Read())
            {
                bool canRead = reader.Name == "y:NodeLabel" && reader.NodeType == XmlNodeType.Element;
                string name = null;
                if (canRead)
                {
                    reader.MoveToContent();
                    name = reader.ReadElementContentAsString();
                    if (name.Contains("&lt;&lt;interface&gt;&gt;") || name.Contains("interface") || name.StartsWith("I") && name.Substring(0, 1).ToUpper().Equals(name))
                    {
                        UML_Interface interfaceModel = new UML_Interface(name, nodeId);
                        return (T)Convert.ChangeType(interfaceModel, typeof(UML_Interface));
                    }
                    if (name != null && !name.Contains("&lt;&lt;interface&gt;&gt;") || !name.Contains("interface") || !name.StartsWith("I") && !name.Substring(0, 1).ToUpper().Equals(name))
                    {
                        UML_Class classModel = new UML_Class(name, nodeId);
                        return (T)Convert.ChangeType(classModel, typeof(UML_Class));
                    }
                }
            }
            return null;
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
            string modifierPublic = "public";
            string modifierPrivate = "private";
            string modifierProtected = "protected";

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
                if (readerValueArray.Any(s => s.StartsWith("+")) == true)
                {
                    attribute.accessModifier = modifierPublic;
                    attribute.name = kvp[0].Trim('+', ' ');
                    attribute.type = current;
                }

                if (readerValueArray.Any(s => s.StartsWith("-")) == true)
                {
                    attribute.accessModifier = modifierPrivate;
                    attribute.name = kvp[0].Trim('-', ' ');
                    attribute.type = current;
                }

                if (readerValueArray.Any(s => s.StartsWith("#")) == true)
                {
                    attribute.accessModifier = modifierProtected;
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
            string modifierPublic = "public";
            string modifierPrivate = "private";
            string modifierProtected = "protected";

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
                if (readerValueList.Any(s => s.StartsWith("+")) == true)
                {
                    method.accessModifier = modifierPublic;
                    method.name = tmp[0].Trim('+');
                    if (stringValue.Contains(':') == true)
                    {
                        method.type = tmp[1].Trim();
                    }
                    method.parameters = getParameter(stringValue);
                }

                if (readerValueList.Any(s => s.StartsWith("-")) == true)
                {
                    method.accessModifier = modifierPrivate;
                    method.name = tmp[0].Trim('-');
                    if (stringValue.Contains(':') == true)
                    {
                        method.type = tmp[1].Trim();
                    }
                    method.parameters = getParameter(stringValue);
                }

                if (readerValueList.Any(s => s.StartsWith("#")) == true)
                {
                    method.accessModifier = modifierProtected;
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
                    method.accessModifier = null;
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
            int sum = firstIndex + lastIndex;
            if (lastIndex - firstIndex > 3)
            {
                var sections = value.Substring(firstIndex, lastIndex).Split(':');
                UML_Parameter parameter = new UML_Parameter()
                {
                    parameterName = sections[0].Trim('('),
                    parameterType = sections[1].Trim(')')
                };
                listParamters.Add(parameter);
            }

            else
            {
                listParamters = null;
            }
            return listParamters;
        }

        public CodeGenerator.Datamodel.Datamodel getDatamodel()
         {
            Datamodel.Datamodel datamodel = ReadGraphml(this.filepath);
            return datamodel;
         }


    }
}
