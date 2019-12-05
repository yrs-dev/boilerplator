using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Datamodel
{
    public class UML_Class : UML_Base
    {

        public List<UML_Method> ListUmlMethods { get; set; }

        public List<UML_Attribute> ListUmlAttributes { get; set; }

    }
}
