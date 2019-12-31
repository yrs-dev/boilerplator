using System.Collections.Generic;
using System.Text;
using System.IO;
using CodeGenerator.Datamodel;

namespace CodeGenerator.Generator
{
    /// <summary> Class responsible for generating UML classes. </summary> 
    public class ClassGenerator
    {
        /// <summary> Variable to store the currently used file. </summary> 
        private StreamWriter classFile;

        /// <summary> Variable which will contain the needed information for the class generator logic. </summary> 
        private UML_Class umlClass;

        /// <summary> Tab needed for indentation purposes.</summary>
        string structureTab = "\t";


        /// <summary> Constructor for the class. Takes the current file and the data object as input. </summary> 
        public ClassGenerator(StreamWriter classFile, UML_Class umlClass)
        {
            this.classFile = classFile;
            this.umlClass = umlClass;
        }



        /// <summary> Top-method orchestrating the generator steps. </summary>
        public void generateClass()
        {
            // Write class
            writeClass();

            // Write attributes
            writeAttributes();

            // Write methods
            writeMethods();

            // Ending line
            classFile.WriteLine("}");
        }


        /// <summary> Writes the introductory class line with the name matching what is found in the UML_Class object. </summary>
        void writeClass()
        {
            // Write beginning
            StringBuilder sb = new StringBuilder($"{umlClass.accessModifier} class {umlClass.name}");

            // Append parents, interfaces
            writeClass_appendElements(sb);

            // Last line
            sb.Append("\n{\n");

            // Write built string
            classFile.WriteLine(sb.ToString());
        }



        /// <summary> Writes elements belonging to the class declaration that may or may not be neccessary (e.g. parent classes or interfaces). </summary>
        /// <param name="sb">Currently used StringBuilder.</param>
        void writeClass_appendElements(StringBuilder sb)
        {

            // Append colon
            bool aditionalElementsPresent = umlClass.parents.Count > 0 || umlClass.implementedInterfaces.Count > 0;
            if (aditionalElementsPresent)
            {
                sb.Append(" : ");
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


        /// <summary> Writes attributes into a specified file with the name and type matching what is found in the passed list of attributes that belong to the current class. </summary>
        void writeAttributes()
        {

            // Comment in file
            classFile.WriteLine($"{structureTab}// Attributes");

            // Extract attributes
            List<UML_Attribute> umlAttributes = umlClass.umlAttributes;

            // Iterate over attribute list
            foreach (UML_Attribute umlAttribute in umlAttributes)
            {
                // Write attribute to file
                string attributeString = $"{umlAttribute.accessModifier} {umlAttribute.type} {umlAttribute.name};";
                
                classFile.WriteLine(structureTab + attributeString);
            }

            // Trailing line
            classFile.WriteLine("");

        }


        /// <summary> Writes empty functions into a specified file with the name and parameters matching what is found in the passed list of methods that belong to the current class. </summary>
        void writeMethods()
        {
            // Comment in file
            classFile.WriteLine($"{structureTab}// Methods");

            // Extract methods
            List<UML_Method> umlMethods = umlClass.umlMethods;

            // Iterate over method list
            foreach (UML_Method umlMethod in umlMethods)
            {
                StringBuilder sb = new StringBuilder();

                // First line
                sb.Append($"{structureTab}{umlMethod.accessModifier} {umlMethod.type} {umlMethod.name}(");
                foreach (UML_Parameter umlParameter in umlMethod.parameters)
                {
                    sb.Append($"{umlParameter.parameterName} {umlParameter.parameterType}, ");
                }

                // Delete trailing ,
                sb.Length -= 2;

                // Close parantheses
                sb.Append(")");

                // Write body
                sb.Append($"\n{structureTab}{{");
                sb.Append($"\n{structureTab}{structureTab}throw new System.NotImplementedException();");
                sb.Append($"\n{structureTab}}}\n");

                // Write built string
                classFile.WriteLine(sb.ToString());

            }
        }
    }
}
