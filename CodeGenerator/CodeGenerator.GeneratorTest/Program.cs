using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dm = CodeGenerator.Datamodel;

namespace CodeGenerator.GeneratorTest
{
    class Program
    {
        static void Main(string[] args)
        {



        }


        ////////////////// CLASS TEST
        public void classTest()
        {

            // 1 datamodel
            dm.Datamodel dml_classes = new dm.Datamodel();

            // 3 classes
            dm.UML_Class class1 = new dm.UML_Class("class1");
            dm.UML_Class class2 = new dm.UML_Class("class2");
            dm.UML_Class class3 = new dm.UML_Class("class3");

            // 1, 2 and 4 methods
            dm.UML_Attribute att1 = new dm.UML_Attribute();


        }



        ////////////////// INTERFACE TEST
        public void interfaceTest()
        {

            Datamodel.Datamodel dml_interfaces = new Datamodel.Datamodel(); // 2 interfaces

        }
    }
}
