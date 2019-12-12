using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Datamodel
{
    public class UML_Interface : UML_Base
    {

        // List to store methods specified in the class diagram belonging to one class
        public List<UML_Method> umlMethods { get; set; }

        // List to store attributes specified in the class diagram belonging to one class
        public List<UML_Attribute> umlAttributes { get; set; }


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
