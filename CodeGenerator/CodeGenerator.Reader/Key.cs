using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeGenerator.Datamodel;

namespace CodeGenerator.Reader
{
    public class Key 
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<UML_Base> model { get; set; }
        public string source { get; set; }
        public string target { get; set; }
    }
}
