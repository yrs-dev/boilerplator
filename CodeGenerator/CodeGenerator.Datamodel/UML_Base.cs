using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Datamodel
{
    // Base class for UML_Class, UML_Attribute and UML_Method with variables all are in need of
    public class UML_Base
    {

        // "public", "private", etc
        public string accessModifier { get; set; }

        // "variable name" or "method name" or "class name"
        public string name { get; set; }

        // .graphml class node notation --> important for Inheritance
        public string id { get; set; }


        /*
        // Constructor
        public UML_Base(char accessModifier, string name)
        {
            this.accessModifier = accessModifier;
            this.name = name;
        }
        */

        public override bool Equals(object obj)
        {
            //return obj.GetType() == typeof(UML_Class) || 
            //obj.GetType() == typeof(UML_Interface);
            return
                this.id == ((UML_Base)obj).id && this.name == ((UML_Base)obj).name;
        }

    }
}
