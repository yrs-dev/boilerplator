using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeGenerator.Datamodel;

namespace CodeGenerator.Reader
{
    public class BaseReader
    {
        public string filepath { get; set; }

        public BaseReader(string path)
        {
            this.filepath = path;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methods"></param>
        /// <returns></returns>
        public List<UML_Method> getMethod(string methods)
        {
            // Possible accessmodifier
            string modifierPublic = "public";
            string modifierPrivate = "private";
            string modifierProtected = "protected";

            // Storing each method into this list
            List<UML_Method> listMethods = new List<UML_Method>();

            // Splitting the string input at whitespaces
            List<string> readerValueList = System.Text.RegularExpressions.Regex.Split(methods, "\\n").ToList<string>();

            // Looping through all splitted string-elements
            foreach (string stringValue in readerValueList)
            {
                if (checkListValue(stringValue))
                {
                    UML_Method method = new UML_Method();

                    // Separating name and type
                    var tmp = stringValue.Split('(');
                    string staticKey = "static";

                    // Check for valid data
                    if (stringValue.StartsWith("+") == true)
                    {
                        method.accessModifier = modifierPublic;
                        method.name = tmp[0].Trim('+','*');
                        if (stringValue.Contains(':') == true)
                        {
                            method.type = tmp[1].Trim();
                        }
                        if (checkStatic(stringValue))
                        {
                            method.extraKeyword = staticKey;
                        }
                        method.parameters = getParameter(stringValue);
                    }

                    if (stringValue.StartsWith("-") == true)
                    {
                        method.accessModifier = modifierPrivate;
                        method.name = tmp[0].Trim('-', '*');
                        if (stringValue.Contains(':') == true)
                        {
                            method.type = tmp[1].Trim();
                        }
                        if (checkStatic(stringValue))
                        {
                            method.extraKeyword = staticKey;
                        }
                        method.parameters = getParameter(stringValue);
                    }

                    if (stringValue.StartsWith("#") == true)
                    {
                        method.accessModifier = modifierProtected;
                        method.name = tmp[0].Trim('#', '*');
                        if (stringValue.Contains(':') == true)
                        {
                            method.type = tmp[1].Trim();
                        }
                        if (checkStatic(stringValue))
                        {
                            method.extraKeyword = staticKey;
                        }
                        method.type = tmp[1].Trim();
                        method.parameters = getParameter(stringValue);
                    }

                    if (stringValue.StartsWith("+") == false && stringValue.StartsWith("-") == false && stringValue.StartsWith("#") == false && stringValue.Length > 1)
                    {
                        method.accessModifier = null;
                        method.name = tmp[0].Trim('(', '*');

                        if (!stringValue.Contains(':'))
                        {
                            method.type = "Void";
                        }
                        if (stringValue.Contains(':') == true)
                        {
                            var tmp2 = stringValue.Split(')');
                            method.type = tmp2[1].Trim(':');
                        }
                        if (checkStatic(stringValue))
                        {
                            method.extraKeyword = staticKey;
                        }
                        method.parameters = getParameter(stringValue);
                    }
                    listMethods.Add(method);
                }
            }

            return listMethods;
        }

        /// <summary>
        ///  Method for handling data about each existing Parameter of a Method and storing these as a datamodel valid form into a List of objects
        /// </summary>
        /// <param name="value"> Value containing parsed data from MethodLabel </param>
        /// <returns></returns>
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

        // Null Input Check
        bool checkListValue(string value)
        {
            if (value != null && value.Length > 1)
            {
                return true;
            }

            //if (value == null)
            //{
            //  Exception
            //}
            return false;
        }


        /// <summary>
        /// Method for handling the data about each existing attribute and storing these as a datamodel valid form into a List of objects
        /// </summary>
        /// <param name="attr"> Value containing parsed data of AttributeLabel </param>
        /// <returns></returns>
        public List<UML_Attribute> getAttribute(string attr)
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
                if (checkListValue(stringValue)) 
                {
                    UML_Attribute attribute = new UML_Attribute();

                    // Separating name and type
                    var kvp = stringValue.Split(':');

                    string staticKey = "static";

                    // Cutting of whitespaces
                    string current = kvp[1].Trim();

                    // Checking for valid data
                    // Checking current accessmodifier
                    if (stringValue.StartsWith("+") == true && kvp[1] != null)
                    {
                        attribute.accessModifier = modifierPublic;
                        attribute.name = kvp[0].Trim('+', ' ', '*');
                        attribute.type = current;
                        if (checkStatic(stringValue))
                        {
                            attribute.extraKeyword = staticKey;
                        }
                        attribute.autoGetterSetterSpecified = checkGetterSetter(stringValue);
                    }
                    if (stringValue.StartsWith("-") == true && kvp[1] != null)
                    {
                        attribute.accessModifier = modifierPrivate;
                        attribute.name = kvp[0].Trim('-', ' ', '*');
                        attribute.type = current;
                        if (checkStatic(stringValue))
                        {
                            attribute.extraKeyword = staticKey;
                        }
                        attribute.autoGetterSetterSpecified = checkGetterSetter(stringValue);
                    }
                    if (stringValue.StartsWith("#") == true && kvp[1] != null)
                    {
                        attribute.accessModifier = modifierProtected;
                        attribute.name = kvp[0].Trim('#', ' ', '*');
                        attribute.type = current;
                        if (checkStatic(stringValue))
                        {
                            attribute.extraKeyword = staticKey;
                        }
                        attribute.autoGetterSetterSpecified = checkGetterSetter(stringValue);
                    }
                    if (stringValue.StartsWith("+") == false && stringValue.StartsWith("-") == false && stringValue.StartsWith("#") == false && kvp[1] != null)
                    {
                        attribute.accessModifier = null;
                        attribute.name = kvp[0].Trim(' ', '*');
                        attribute.type = current;
                        if (checkStatic(stringValue))
                        {
                            attribute.extraKeyword = staticKey;
                        }
                        attribute.autoGetterSetterSpecified = checkGetterSetter(stringValue);
                    }

                    // List of all existing attributes as objects
                    listAttributes.Add(attribute);
                }
            }

            return listAttributes;
        }

        bool checkGetterSetter(string objectValue)
        {
            if (!objectValue.Contains("readonly"))
            {
                return true;
            }
            return false;
        }

        bool checkStatic(string nameValue)
        {
            if (nameValue.StartsWith("*") && nameValue.EndsWith("*"))
            {
                return true;
            }
            return false;
        }
    }
}
