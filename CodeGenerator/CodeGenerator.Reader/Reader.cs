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

            try
            {
                Datamodel.Datamodel datamodel = null;
                datamodel = new Datamodel.Datamodel();
                List<UML_Base> baseModel = AnalyzeNode();
                foreach (var item in baseModel)
                {
                    if (item.GetType() == typeof(UML_Class) && baseModel != null)
                    {
                        datamodel.umlClasses.Add((UML_Class)item);
                    }

                    if (item.GetType() == typeof(UML_Interface) && baseModel != null)
                    {
                        datamodel.umlInterfaces.Add((UML_Interface)item);
                    }
                }

                return datamodel;
            }
            finally
            {
            }
        }

        public List<UML_Base> AnalyzeNode()
        {

            XDocument doc = XDocument.Load(this.filepath);
            XNamespace ns = doc.Root.GetDefaultNamespace();
            XNamespace yns = "http://www.yworks.com/xml/graphml";

            Dictionary<string, List<string>> keys = new Dictionary<string, List<string>>();
            foreach (var node in doc.Descendants(ns + "node"))
            {
                keys[node.Attribute("id").Value] = new List<string>();
                foreach (var nodelabel in node.Descendants(yns + "NodeLabel"))
                {
                    keys[node.Attribute("id").Value].Add(nodelabel.Value);
                    foreach (var attributeLabel in node.Descendants(yns + "AttributeLabel"))
                    {
                        keys[node.Attribute("id").Value].Add(attributeLabel.Value);
                    }
                    foreach (var methodLabel in node.Descendants(yns + "MethodLabel"))
                    {
                        keys[node.Attribute("id").Value].Add(methodLabel.Value);
                    }
                }
            }
            Dictionary<string, List<string>> inheritance = new Dictionary<string, List<string>>();
            foreach (var source in doc.Descendants(ns + "edge"))
            {
                inheritance[source.Attribute("source").Value] = new List<string>();
                inheritance[source.Attribute("target").Value] = new List<string>();
                foreach (var whiteDelta in doc.Descendants(yns + "Arrows"))
                {
                    inheritance[source.Attribute("target").Value].Add(whiteDelta.Attribute("target").Value);
                }
            }

            List<UML_Base> baseModelList = getModel(keys);
            checkInheritance(baseModelList, inheritance,doc);

            return baseModelList;
        }

        List<UML_Base> getModel(Dictionary<string,List<string>> dict)//where T : UML_Base
        {
            List<UML_Base> baseModels = new List<UML_Base>();
            foreach (var entry in dict)
            {
                var baseModel = AnalyzeNodeLabel<UML_Base>(entry.Key, entry.Value[0], entry.Value[1], entry.Value[2]);
                if (baseModel.GetType() == typeof(UML_Class))
                {
                    baseModels.Add(baseModel);
                }
                if (baseModel.GetType() == typeof(UML_Interface))
                {
                    baseModels.Add(baseModel);
                }
            }
            return baseModels;
        }

        bool checkInheritance(List<UML_Base> baseList, Dictionary<string, List<string>> inheritanceDict, XDocument doc)
        {
            bool inheritanceChecker = false;
            foreach (var item in inheritanceDict)
            {
                foreach (var check in item.Value)
                {
                    if (check == "white_delta")
                    {
                        inheritanceChecker = true;
                        getInheritance(baseList,inheritanceDict,doc);
                    }
                }
            }
            return inheritanceChecker;
        }

        void getInheritance(List<UML_Base> baseModelList, Dictionary<string,List<string>> inheritanceDict, XDocument doc)
        {
            foreach (var inheritance in doc.Descendants(doc.Root.GetDefaultNamespace() + "edge"))
            {
                var sourceId = inheritance.Attribute("source").Value;
                var targetId = inheritance.Attribute("target").Value;

                if (baseModelList.Find(x => x.id == targetId).GetType() == typeof(UML_Interface))
                {
                    UML_Class classParent = (UML_Class)baseModelList.Find(y => y.id == sourceId);
                    UML_Interface implementedInterface = (UML_Interface)baseModelList.Find(y => y.id == targetId);
                    List<UML_Interface> implementedInterfaceList = new List<UML_Interface>();
                    implementedInterfaceList.Add(implementedInterface);
                    classParent.implementedInterfaces = implementedInterfaceList;
                }
                if (baseModelList.Find(x => x.id == targetId).GetType() == typeof(UML_Class))
                {
                    UML_Class classParent = (UML_Class)baseModelList.Find(y => y.id == sourceId);
                    UML_Class implementedClass = (UML_Class)baseModelList.Find(y => y.id == targetId);
                    classParent.parent = implementedClass;
                }
            }
        }

        public T AnalyzeNodeLabel<T> (string nodeId, string name, string attributes, string methods) where T : CodeGenerator.Datamodel.UML_Base
        {
            string modifierPublic = "public";
            string modifierPrivate = "private";
            string modifierProtected = "protected";

            if (name.Contains("<<interface>>") || name.Contains("&lt;&lt;interface&gt;&gt;") || name.Contains("interface") || name.StartsWith("I") && name.Substring(0, 1).ToUpper().Equals(name))
            {
                string interfaceName = name.Replace("<<interface>>", "").Replace("\n\t\t","");

                if (name.StartsWith("+"))
                {
                    UML_Interface interfaceModel = new UML_Interface(interfaceName, nodeId);
                    interfaceModel.accessModifier = modifierPublic;
                    interfaceModel.umlAttributes = AnalyzeAttributeLabel(attributes);
                    return (T)Convert.ChangeType(interfaceModel, typeof(UML_Interface));
                }
                if (name.StartsWith("-"))
                {
                    UML_Interface interfaceModel = new UML_Interface(interfaceName, nodeId);
                    interfaceModel.accessModifier = modifierPrivate;
                    interfaceModel.umlAttributes = AnalyzeAttributeLabel(attributes);
                    return (T)Convert.ChangeType(interfaceModel, typeof(UML_Interface));
                }
                if (name.StartsWith("#"))
                {
                    UML_Interface interfaceModel = new UML_Interface(interfaceName, nodeId);
                    interfaceModel.accessModifier = modifierProtected;
                    interfaceModel.umlAttributes = AnalyzeAttributeLabel(attributes);
                    return (T)Convert.ChangeType(interfaceModel, typeof(UML_Interface));
                }
                else
                {
                    UML_Interface interfaceModel = new UML_Interface(interfaceName, nodeId);
                    interfaceModel.umlAttributes = AnalyzeAttributeLabel(attributes);
                    interfaceModel.umlMethods = AnalyzeMethodLabel(methods);
                    return (T)Convert.ChangeType(interfaceModel, typeof(UML_Interface));
                }
            }
            if (name != null && !name.Contains("&lt;&lt;interface&gt;&gt;") || !name.Contains("interface") || !name.StartsWith("I") && !name.Substring(0, 1).ToUpper().Equals(name))
            {
                if (name.StartsWith("+"))
                {
                    UML_Class classModel = new UML_Class(name, nodeId);
                    classModel.accessModifier = modifierPublic;
                    classModel.umlAttributes = AnalyzeAttributeLabel(attributes);
                    return (T)Convert.ChangeType(classModel, typeof(UML_Class));
                }
                if (name.StartsWith("-"))
                {
                    UML_Class classModel = new UML_Class(name, nodeId);
                    classModel.accessModifier = modifierPrivate;
                    classModel.umlAttributes = AnalyzeAttributeLabel(attributes);
                    return (T)Convert.ChangeType(classModel, typeof(UML_Class));
                }
                if (name.StartsWith("#"))
                {
                    UML_Class classModel = new UML_Class(name, nodeId);
                    classModel.accessModifier = modifierProtected;
                    classModel.umlAttributes = AnalyzeAttributeLabel(attributes);
                    return (T)Convert.ChangeType(classModel, typeof(UML_Class));
                }
                else
                {
                    UML_Class classModel = new UML_Class(name, nodeId);
                    classModel.umlAttributes = AnalyzeAttributeLabel(attributes);
                    classModel.umlMethods = AnalyzeMethodLabel(methods);
                    return (T)Convert.ChangeType(classModel, typeof(UML_Class));
                }
            }
            return null;
        }

        // Method gets the Attributes for each class
        public List<UML_Attribute> AnalyzeAttributeLabel (string attr)
        {
            List<UML_Attribute> classAttributes = new List<UML_Attribute>();
            XDocument doc = XDocument.Load(this.filepath);
            XNamespace yns = "http://www.yworks.com/xml/graphml";
            Dictionary<string, List<string>> attributes = new Dictionary<string, List<string>>();
            classAttributes = getAttribute(attr);

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
            readerValueArray = System.Text.RegularExpressions.Regex.Split(attr, "\\n").ToList<string>();

            // Looping through all splitted string-elements
            foreach (string stringValue in readerValueArray)
            {
                UML_Attribute attribute = new UML_Attribute();

                // Separating name and type
                var kvp = stringValue.Split(':');
                // Cutting of whitespaces
                string current = kvp[1].Trim();

                // Provisionally checking accesmodifier
                if (stringValue.StartsWith("+") == true && kvp[1] != null)
                {
                    attribute.accessModifier = modifierPublic;
                    attribute.name = kvp[0].Trim('+', ' ');
                    attribute.type = current;
                }

                if (stringValue.StartsWith("-") == true && kvp[1] != null)
                {
                    attribute.accessModifier = modifierPrivate;
                    attribute.name = kvp[0].Trim('-', ' ');
                    attribute.type = current;
                }

                if (stringValue.StartsWith("#") == true && kvp[1] != null)
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
        public List<UML_Method> AnalyzeMethodLabel (string methods)
        {
            List<UML_Method> classMethods = new List<UML_Method>();
            // Storing text of the <y:MethodLabel>
            // Method for parsing the methods
            classMethods = getMethod(methods);
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
            List<string> readerValueList = System.Text.RegularExpressions.Regex.Split(methods, "\\n").ToList<string>();

            // Looping through all splitted string-elements
            foreach (string stringValue in readerValueList)
            {
                UML_Method method = new UML_Method();
                // Separating name and type
                var tmp = stringValue.Split('(');

                // Provisionally checking accessmodifier
                if (stringValue.StartsWith("+") == true)
                {
                    method.accessModifier = modifierPublic;
                    method.name = tmp[0].Trim('+');
                    if (stringValue.Contains(':') == true)
                    {
                        method.type = tmp[1].Trim();
                    }
                    method.parameters = getParameter(stringValue);
                }

                if (stringValue.StartsWith("-") == true)
                {
                    method.accessModifier = modifierPrivate;
                    method.name = tmp[0].Trim('-');
                    if (stringValue.Contains(':') == true)
                    {
                        method.type = tmp[1].Trim();
                    }
                    method.parameters = getParameter(stringValue);
                }

                if (stringValue.StartsWith("#") == true)
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

                    if (!stringValue.Contains(':'))
                    {
                        method.type = "Void";
                    }
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
            int sum = lastIndex - firstIndex;
            if (lastIndex - firstIndex > 3)
            {
                var sections = value.Substring(firstIndex, sum);
                var section = sections.Split(':');
                UML_Parameter parameter = new UML_Parameter()
                {
                    parameterName = section[0].Trim('('),
                    parameterType = section[1].Trim(')') 
                };
                listParamters.Add(parameter);
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
