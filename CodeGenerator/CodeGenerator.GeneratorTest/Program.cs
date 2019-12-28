using System.Collections.Generic;
using gen = CodeGenerator.Generator;
using dm = CodeGenerator.Datamodel;


/* ToDos:
 * Empty collection instead of null in CodeGenerator.Datamodel.Datamodel
 * Class files have no: public / private
 * Indentation missing
 * Implemented interfaces, inherited classes syntax
 * 
 * Interfaces test
 * 
 */

namespace CodeGenerator.GeneratorTest
{
    class Program
    {
        static void Main(string[] args)
        {

            classTest();

        }


        ////////////////// CLASS TEST
        public static void classTest()
        {

            // 4 attributes
            dm.UML_Attribute att1 = new dm.UML_Attribute("public", "int", "testNumber");
            dm.UML_Attribute att2 = new dm.UML_Attribute("private", "double", "testDouble");
            dm.UML_Attribute att3 = new dm.UML_Attribute("private", "string", "rasr33");
            dm.UML_Attribute att4 = new dm.UML_Attribute("public", "char", "rjojo32");

            // 4 methods
            dm.UML_Parameter param1 = new dm.UML_Parameter("int", "testInt");
            dm.UML_Parameter param2 = new dm.UML_Parameter("double", "testDouble");
            dm.UML_Parameter param3 = new dm.UML_Parameter("string", "testString");
            dm.UML_Parameter param4 = new dm.UML_Parameter("char", "testChar");

            dm.UML_Method method1 = new dm.UML_Method("public", "void", "testMethod1", new List<dm.UML_Parameter>() { param1 });
            dm.UML_Method method2 = new dm.UML_Method("public", "void", "testMethod2", new List<dm.UML_Parameter>() { param1, param2 });
            dm.UML_Method method3 = new dm.UML_Method("public", "void", "testMethod3", new List<dm.UML_Parameter>() { param1, param2, param3, param4 });

            // 3 classes
            dm.UML_Class class1 = new dm.UML_Class("public", "class1", new List<dm.UML_Attribute>() { att1 }, new List<dm.UML_Method>() { method1 }, new List<dm.UML_Class>() { }, new List<dm.UML_Interface>());
            dm.UML_Class class2 = new dm.UML_Class("private", "class2", new List<dm.UML_Attribute>() { att1, att2, att3, att4 }, new List<dm.UML_Method>() { method1, method2, method3 }, new List<dm.UML_Class>() { }, new List<dm.UML_Interface>());
            dm.UML_Class class3 = new dm.UML_Class("public", "class3", new List<dm.UML_Attribute>() { att1, att2, att3, att4 }, new List<dm.UML_Method>() { method1, method2, method3 }, new List<dm.UML_Class>() { class1, class2 }, new List<dm.UML_Interface>());


            // Bring it all together
            dm.Datamodel dml_classes = new dm.Datamodel();
            dml_classes.umlClasses = new List<dm.UML_Class>() { class1, class2, class3 };

            // Create generator object
            gen.Generator gen = new gen.Generator();
            gen.generateCode("C:\\Users\\Yannik\\Desktop\\testFolder", dml_classes);

        }



        ////////////////// INTERFACE TEST
        public void interfaceTest()
        {

            Datamodel.Datamodel dml_interfaces = new Datamodel.Datamodel(); // 2 interfaces

        }
    }
}
