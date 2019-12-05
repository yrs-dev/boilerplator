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
        public static string getValue(XmlReader reader)
        {
            string text = "";
            while (reader.Read())
            {
                if (reader.HasValue)
                {
                    text += reader.Value;
                }
            }
            return text;
        }


        public static UML_Attribute getAttributes(XmlReader reader)
        {
            UML_Attribute attribute = new UML_Attribute();

            while (reader.Read())
            {
                if (reader.HasValue && reader.Name == "y:AttributeLabel")
                {
                    string readerValue = reader.HasValue.ToString();
                    char[] arr = new char[readerValue.Length];

                    using (StringReader sr = new StringReader(readerValue))
                    {
                        sr.Read(arr, 0, arr.Length);
                        string attributeName = sr.Read(arr, 1, arr.Length).ToString();

                        if (arr[0].ToString() == "+")
                        {
                            attribute.name = attributeName;
                            attribute.accessModifier = '+';
                            attribute.type = "Attribute";
                        }
                    }
                }
            }
            return attribute;
        }


         public CodeGenerator.Datamodel.Datamodel getDatamodel(string filePath)
         {
             throw new NotImplementedException();
         }


        }
    }

}
