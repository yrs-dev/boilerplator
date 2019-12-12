using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CommonInterfaces;
using CodeGenerator.Datamodel;

/* TODO:
 * uml class: parents and interfaces proper syntax, string instead of object?
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
                    foreach(UML_Attribute umlAttribute in umlClass.umlAttributes)
                    {
                        writeAttribute(classFile, umlAttribute);
                    }

                    // Write methods
                    foreach (UML_Method umlMethod in umlClass.umlMethods)
                    {
                        writeMethod(classFile, umlMethod);
                    }

                    // Ending line
                    classFile.WriteLine("}");

                }

            }
            return true;
        }



        // Helper methods
        void writeClass(StreamWriter classFile, UML_Class umlClass)
        {
            // Write beginning
            StringBuilder sb = new StringBuilder($"class {umlClass.name}");

            // Maybe append parents
            if(umlClass.parents.Count > 0 || umlClass.implementedInterfaces.Count > 0)
            {
                sb.Append(" :");
            }

            // Maybe append parents
            if (umlClass.parents.Count > 0)
            {
                string prefix = "";
                foreach (UML_Class parentClass in umlClass.parents)
                {
                    sb.Append($"{prefix} {parentClass.name}");
                    prefix = ",";
                }
            }

            // Maybe append interfaces
            if (umlClass.implementedInterfaces.Count > 0)
            {
                string prefix = "";
                foreach (UML_Interface implementedInterface in umlClass.implementedInterfaces)
                {
                    sb.Append($"{prefix} {implementedInterface.name}");
                    prefix = ",";
                }
            }

            // Last line
            sb.Append(")\n{");

            // Write built string
            classFile.WriteLine(sb.ToString());
        }



        void writeAttribute(StreamWriter classFile, UML_Attribute umlAttribute)
        {
            string attributeString = $"{umlAttribute.accessModifier} {umlAttribute.type} {umlAttribute.name};";
            classFile.WriteLine(attributeString);
        }



        void writeMethod(StreamWriter classFile, UML_Method umlMethod)
        {
            StringBuilder sb = new StringBuilder();

            // First line
            sb.Append($"{umlMethod.accessModifier} {umlMethod.type} {umlMethod.name} (");
            foreach(UML_Parameter umlParameter in umlMethod.parameters)
            {
                sb.Append($"{umlParameter.parameterName} {umlParameter.parameterType}, ");
            }

            sb.Length = sb.Length -2; // Delete trailing ,
            sb.Append(")");

            // Write body
            sb.Append("\n{\n\tthrow new System.NotImplementedException();\n}");

            // Write built string
            classFile.WriteLine(sb.ToString());

        }

        

        void testMethod(Type UML_Class)
        {
            
        }




    }
}
