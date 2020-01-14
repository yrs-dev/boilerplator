using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Datamodel
{
    public class UML_Class : UML_BaseExtension
    {

        // Relations
        public UML_Class parent { get; set; }
        public List<UML_Interface> implementedInterfaces { get; set; }


        // Constructors
        public UML_Class()
        {
            // Empty collections
            this.parent = new UML_Class();
            this.implementedInterfaces = new List<UML_Interface>();
        }

        public UML_Class(string name, string id)
        {
            this.name = name;
            this.id = id;

            // Empty collections
            this.parent = new UML_Class();
            this.implementedInterfaces = new List<UML_Interface>();

        }

        public UML_Class(string accessModifier, string className, List<UML_Attribute> umlAttributes, List<UML_Method> umlMethods, UML_Class parent, List<UML_Interface> implementedInterfaces)
        {
            this.accessModifier = accessModifier;
            this.name = className;
            this.umlAttributes = umlAttributes;
            this.umlMethods = umlMethods;
            this.parent = parent;
            this.implementedInterfaces = implementedInterfaces;
        }

        public override bool Equals(object obj)
        {
            return this.name == ((UML_Class)obj).name &&  
                id.SequenceEqual(((UML_Class)obj).id);
        }
    }
}
