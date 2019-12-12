using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Datamodel
{
    public class UML_Method : UML_Attribute
    {
        // List to store method parameters, may be empty
        public List<string> parameter { get; set; }

        // List to store method parameter types, may be empty
        public List<string> parameterType { get; set; }

    }
}
