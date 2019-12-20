using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Datamodel
{
    public class UML_BaseExtension : UML_Base
    {

        // List to store methods specified in the class diagram belonging to one class
        public List<UML_Method> umlMethods { get; set; }

        // List to store attributes specified in the class diagram belonging to one class
        public List<UML_Attribute> umlAttributes { get; set; }

    }
}
