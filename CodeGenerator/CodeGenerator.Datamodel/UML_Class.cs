using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Datamodel
{
    public class UML_Class : UML_Base
    {

        // Acknowledge as interface -> all specifier will be understood as public by controller
        bool isInterface = false;

        // List to store methods specified in the class diagram belonging to one class
        public List<UML_Method> ListUmlMethods { get; set; }

        // List to store attributes specified in the class diagram belonging to one class
        public List<UML_Attribute> ListUmlAttributes { get; set; }




        // Constructor
        public UML_Class(string classname) { 
            
            // Assign name
            this.name = classname;

            // Status print
            Console.WriteLine("Class object created for: " + classname);

        }

    }
}
