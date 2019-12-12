using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Datamodel
{
    public class UML_Class : UML_Interface
    {

        // Relations
        public List<UML_Class> parents { get; set; }
        public List<UML_Interface> implementedInterfaces { get; set; }


        // Constructor
        public UML_Class(string classname) : base(classname) { 
            
            // Assign name
            this.name = classname;

            // Status print
            Console.WriteLine("Class object created for: " + classname);

        }

    }
}
