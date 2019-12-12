using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Datamodel
{
    public class Datamodel
    {
        // List to store all classes found in the diagram
        public List<UML_Class> umlClasses { get; set; }

        // List to store all interfaces found in the diagram
        public List<UML_Interface> umlInterfaces { get; set; }

    }
}
