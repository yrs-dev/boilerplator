using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Datamodel
{
    public class UML_Interface : UML_BaseExtension
    {

        // Constructors
        public UML_Interface(string interfacename)
        {

            // Assign name
            this.name = interfacename;

            // Status print
            Console.WriteLine("Interface object created for: " + interfacename);

        }

        public UML_Interface(string interfacename, string id)
        {
            this.name = interfacename;
            this.id = id;
        }

        public UML_Interface(string accessModifier, string className, List<UML_Attribute> umlAttributes, List<UML_Method> umlMethods)
        {
            this.accessModifier = accessModifier;
            this.name = className;
            this.umlAttributes = umlAttributes;
            this.umlMethods = umlMethods;
        }
    }
}
