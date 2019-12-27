using System;
using System.IO;
using CommonInterfaces;
using CodeGenerator.Datamodel;




namespace CodeGenerator.Generator
{
    public class Generator : IGenerator
    {

        /// <summary>
        /// Writes .cs files according to the passed Datamodel to the specified file path.
        /// </summary>
        /// <param name="filePath">Created files go here.</param>
        /// <param name="dml">Datamodel object containing information about everything found in the class diagram.</param>
        /// <returns>Nothing.</returns>
        public bool generateCode(string filePath, Datamodel.Datamodel dml)
        {

            // Iterate over classes
            foreach (UML_Class umlClass in dml.umlClasses)
            {
                // Create new file
                using (StreamWriter classFile = new StreamWriter(filePath + umlClass.name + ".cs"))
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
                using (StreamWriter interfaceFile = new StreamWriter(filePath + umlInterface.name + ".cs"))
                {

                    // Create InterfaceGenerator object
                    InterfaceGenerator iGen = new InterfaceGenerator(interfaceFile, umlInterface);

                    // Generate class
                    iGen.generateInterface();

                }
            }

             return true;
        }

    }
}
