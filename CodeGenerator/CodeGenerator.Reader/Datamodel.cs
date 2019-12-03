using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Reader
{
    public class Datamodel
    {
        public string ClassName { get; set; }
        public string AttributeName { get; set; }
        public string MethodName { get; set; }
        public List<string> Attributes { get; set; }
        public List<string> Methods { get; set; }

        public Datamodel(string className, string attributeName, string methodName)
        {
            
        }

    }
}