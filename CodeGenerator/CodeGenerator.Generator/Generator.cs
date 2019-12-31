using System;
using System.IO;
using CommonInterfaces;
using CodeGenerator.Datamodel;
using dm = CodeGenerator.Datamodel;

/* TODO: 
 * bool error return
 * base class for class-, interfacegenerator
 * test run
 */


namespace CodeGenerator.Generator
{
    public class Generator : IGenerator
    {

        /*
        /// <summary> Created files go here. </summary>
        public string filePath { get; set; }

        /// <summary> Datamodel object containing information about everything found in the class diagram.</summary>
        public dm.Datamodel dml { get; set; }


        // Constructor
        public Generator(string filePath, dm.Datamodel dml)
        {
            this.filePath = filePath;
            this.dml = dml;
        }
        */

        /// <summary>
        /// Writes ".cs" files to the specified file path according to the passed Datamodel.
        /// </summary>
        /// <param name="filePath">Created files go here. </param>
        /// <param name="dml">Datamodel object containing information about everything found in the class diagram. </param>
        /// <returns>Nothing.</returns>
        public bool generateCode(string filePath, dm.Datamodel dml)
        {

            // Check datamodel
            if (!isDataModelComplete())
            {
                return false;
            }

            // Iterate over classes
            foreach (UML_Class umlClass in dml.umlClasses)
            {
                // Create new file
                using (StreamWriter classFile = new StreamWriter(filePath + "\\" + umlClass.name + ".cs"))
                {

                    // Create ClassGenerator object
                    ClassGenerator cGen = new ClassGenerator(classFile, umlClass);

                    // Generate class
                    cGen.generateClass();

                }
            }

            // Iterate over interfaces
            foreach (UML_Interface umlInterface in dml.umlInterfaces)
            {
                // Create new file
                using (StreamWriter interfaceFile = new StreamWriter(filePath + "\\" + umlInterface.name + ".cs"))
                {

                    // Create InterfaceGenerator object
                    InterfaceGenerator iGen = new InterfaceGenerator(interfaceFile, umlInterface);

                    // Generate class
                    iGen.generateInterface();

                }
            }

            return true;
        }

        private bool isDataModelComplete()
        {


            return false;
        }

    }
}
