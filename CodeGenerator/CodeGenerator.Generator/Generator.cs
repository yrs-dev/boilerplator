using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CommonInterfaces;
using CodeGenerator.Datamodel;

/* TODO:
 * 
 * 
 * 
 * 
 */

namespace CodeGenerator.Generator
{
    public class Generator : IGenerator
    {

        // Main method
        public bool generateCode(string filePath, Datamodel.Datamodel dml)
        {

            // Iterate over classes
            foreach (UML_Class umlClass in dml.umlClasses)
            {
                // Create new file
                using (StreamWriter classFile = new StreamWriter(filePath + umlClass.name + ".cs"))
                {

                    // Write class
                    writeClass(classFile, umlClass);

                    // Write attributes
                    writeAttribute(classFile, umlClass.umlAttributes);

                    // Write methods
                    writeMethod(classFile, umlClass.umlMethods);

                    // Ending line
                    classFile.WriteLine("}");

                }
            }
            // Iterate over interfaces
            foreach (UML_Interface umlInterface in dml.umlInterfaces)
            {
                // Create new file
                using (StreamWriter interfaceFile = new StreamWriter(filePath + umlInterface.name + ".cs"))
                {

                    // Write class
                    writeClass(classFile, umlClass);

                    // Write attributes
                    writeAttribute(classFile, umlClass.umlAttributes);

                    // Write methods
                    writeMethod(classFile, umlClass.umlMethods);

                    // Ending line
                    classFile.WriteLine("}");

                }
            }

             return true;
        }



        /// <summary>
        /// Writes the introductory class line with the name matching what is found in the UML_Class object.
        /// </summary>
        /// <param name="classFile">File to be written into.</param>
        /// <param name="umlClass">UML_Class object containing information about to be written class.</param>
        /// <returns>Nothing.</returns>
        void writeClass(StreamWriter classFile, UML_Class umlClass)
        {
            // Write beginning
            StringBuilder sb = new StringBuilder($"class {umlClass.name}");

            // Append parents, interfaces
            writeClass_appendElements(classFile, umlClass, sb);

            // Last line
            sb.Append(")\n{");

            // Write built string
            classFile.WriteLine(sb.ToString());
        }



        /// <summary>
        /// Writes elements belonging to the class declaration that may or may not be neccessary (e.g. parent classes or interfaces)
        /// </summary>
        /// <param name="classFile">File to be written into.</param>
        /// <param name="umlClass">UML_Class object containing information about to be written class.</param>
        /// <param name="sb">Currently used StringBuilder.</param>
        void writeClass_appendElements(StreamWriter classFile, UML_Class umlClass, StringBuilder sb)
        {

            // Append colon
            bool aditionalElementsPresent = umlClass.parents.Count > 0 || umlClass.implementedInterfaces.Count > 0;
            if (aditionalElementsPresent)
            {
                sb.Append(" :");
            }

            // Append parents
            if (umlClass.parents.Count > 0)
            {
                foreach (UML_Class parentClass in umlClass.parents)
                {
                    sb.Append($"{parentClass.name}, ");
                }
            }

            // Append interfaces
            if (umlClass.implementedInterfaces.Count > 0)
            {
                foreach (UML_Interface implementedInterface in umlClass.implementedInterfaces)
                {
                    sb.Append($"{implementedInterface.name}, ");
                }
            }

            // Delete last comma
            if (aditionalElementsPresent)
            {
                sb.Length = sb.Length - 2;
            }
        }


        /// <summary>
        /// Writes attributes into a specified file with the name and type matching what is found in the passed list of attributes that belong to the current class.
        /// </summary>
        /// <param name="classFile">File to be written into.</param>
        /// <param name="umlAttributes">List of all attributes found in the Class Diagram.</param>
        /// <returns>Nothing.</returns>
        void writeAttribute(StreamWriter classFile, List<UML_Attribute> umlAttributes)
        {

            // Iterate over attribute list
            foreach(UML_Attribute umlAttribute in umlAttributes)
            {
                // Write attribute to file
                string attributeString = $"{umlAttribute.accessModifier} {umlAttribute.type} {umlAttribute.name};";
                classFile.WriteLine(attributeString);
            }
            
        }


        /// <summary>
        /// Writes empty functions into a specified file with the name and parameters matching what is found in the passed list of methods that belong to the current class.
        /// </summary>
        /// <param name="classFile">File to be written into.</param>
        /// <param name="umlMethods">List of all methods found in the Class Diagram.</param>
        /// <returns>Nothing.</returns>
        void writeMethod(StreamWriter classFile, List<UML_Method> umlMethods)
        {

            // Iterate over method list
            foreach (UML_Method umlMethod in umlMethods)
            {
                StringBuilder sb = new StringBuilder();

                // First line
                sb.Append($"{umlMethod.accessModifier} {umlMethod.type} {umlMethod.name} (");
                foreach (UML_Parameter umlParameter in umlMethod.parameters)
                {
                    sb.Append($"{umlParameter.parameterName} {umlParameter.parameterType}, ");
                }

                sb.Length = sb.Length - 2; // Delete trailing ,
                sb.Append(")");

                // Write body
                sb.Append("\n{\n\tthrow new System.NotImplementedException();\n}");

                // Write built string
                classFile.WriteLine(sb.ToString());

            }
        }

        

        void testMethod(Type UML_Class)
        {
            
        }




    }
}
