using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Datamodel
{
    public class UML_Attribute : UML_Base
    {

        // "void", "int", etc.
        public string type { get; set; }

        public UML_Attribute()
        {

        }

        public UML_Attribute(object attribute)
        {
            string attributeName = attribute.ToString();
            this.name = attributeName;
        }

    }
}
