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


         public CodeGenerator.Datamodel.Datamodel getDatamodel(string filePath)
         {
             throw new NotImplementedException();
         }


        }
    }

}
