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
                    List<UML_Base> baseModel = AnalyzeNode(reader);
                    foreach (var item in baseModel)
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

        public List<UML_Base> AnalyzeNode(XmlReader reader)
        {
            //List<string> id = getNodeAttributeValue(this.filepath);

            XDocument doc = XDocument.Load(this.filepath);
            XNamespace ns = doc.Root.GetDefaultNamespace();
            XNamespace yns = "http://www.yworks.com/xml/graphml";

            List<Key> idList = doc.Descendants(ns + "node").Select(x => new Key()
            {
                id = (string)x.Attribute("id"),
            }).ToList();

            List<string> id = doc.Descendants(ns + "node").Select(x => x.Value
            ).ToList();

            List<Key> nameList = doc.Descendants(yns + "NodeLabel").Select(x => new Key()
            {
                name = (string)x.Value,
            }).ToList();

            Dictionary<string, Key> dict = idList.GroupBy(x => x.id, y => y)
                .ToDictionary(x => x.Key, y => y.FirstOrDefault());

            List<UML_Base> baseModelList = getModel(reader, dict.Select(p => p.Value.id));

            return baseModelList;
        }

        public List<string> getNodeAttributeValue(string filepath)
        {
            List<string> id = new List<string>();
            XmlReader xmlReader = XmlReader.Create(filepath);

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

        List<UML_Base> getModel(XmlReader reader, IEnumerable<string> id)//where T : UML_Base
        {
            List<UML_Base> baseModels = new List<UML_Base>();
            while (reader.Read())
            {
                foreach(var item in id)
                {
                    var baseModel = AnalyzeNodeLabel<UML_Base>(reader, item);
                    if (baseModel.GetType() == typeof(UML_Class))
                    {
                        //baseModels.Add((T)Convert.ChangeType(baseModel, typeof(UML_Class)));
                        baseModels.Add(baseModel);
                    }
                    if (baseModel.GetType() == typeof(UML_Interface))
                    {
                        //baseModels.Add((T)Convert.ChangeType(baseModel, typeof(UML_Interface)));
                        baseModels.Add(baseModel);
                    }
                }
            }
            return baseModels;
        }

        bool checkInheritance(XmlReader reader)
        {
            return false;
        }

        string getInheritance(XmlReader reader, UML_Base model)
        {
            XDocument doc = XDocument.Load(this.filepath);
            XNamespace ns = doc.Root.GetDefaultNamespace();
            List<Key> inheritance = doc.Descendants(ns + "edge").Select(x => new Key()
            {
                source = (string)x.Attribute("source"),
                target = (string)x.Attribute("target")
            }).ToList();

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
                    name = reader.Value;
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
                if (readerValueArray.Any(s => s.StartsWith("+")) == true && kvp[1] != null)
                {
                    attribute.accessModifier = modifierPublic;
                    attribute.name = kvp[0].Trim('+', ' ');
                    attribute.type = current;
                }

                if (readerValueArray.Any(s => s.StartsWith("-")) == true && kvp[1] != null)
                {
                    attribute.accessModifier = modifierPrivate;
                    attribute.name = kvp[0].Trim('-', ' ');
                    attribute.type = current;
                }

                if (readerValueArray.Any(s => s.StartsWith("#")) == true && kvp[1] != null)
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
