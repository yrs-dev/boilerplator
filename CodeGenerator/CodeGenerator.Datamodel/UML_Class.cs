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
            // Empty collection
            this.implementedInterfaces = new List<UML_Interface>();
        }

        public UML_Class(string name, string id)
        {
            this.name = name;
            this.id = id;

            // Empty collection
            this.implementedInterfaces = new List<UML_Interface>();

        }

        public UML_Class(string accessModifier, string name, string id, List<UML_Attribute> umlAttributes, List<UML_Method> umlMethods, List<UML_Interface> implementedInterfaces, string extraKeyword = "", UML_Class parent = null) : base(accessModifier, name, id, umlMethods, umlAttributes, extraKeyword)
        {
            this.parent = parent;

            // Empty collection
            this.implementedInterfaces = implementedInterfaces ?? new List<UML_Interface>();
            
        }

        public override bool Equals(object obj)
        {
            return this.name == ((UML_Class)obj).name &&  
                id.SequenceEqual(((UML_Class)obj).id);
        }
    }
}
