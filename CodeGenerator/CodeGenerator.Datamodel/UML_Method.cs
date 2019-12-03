using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Datamodel
{
    // public void testCase(int param1, string param2);
    public class UML_Method : UML_Base
    {

        public List<string> parameter { get; set; }

        public List<string> parameterType { get; set; }

    }
}
