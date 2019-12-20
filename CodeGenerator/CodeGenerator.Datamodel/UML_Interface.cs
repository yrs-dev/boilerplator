using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Datamodel
{
    public class UML_Interface : UML_BaseExtension
    {

        // Constructor
        public UML_Interface(string interfacename)
        {

            // Assign name
            this.name = interfacename;

            // Status print
            Console.WriteLine("Interface object created for: " + interfacename);

        }
    }
}
