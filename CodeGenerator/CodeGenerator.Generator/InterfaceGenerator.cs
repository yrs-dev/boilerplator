using System.Collections.Generic;
using System.Text;
using System.IO;
using CodeGenerator.Datamodel;

namespace CodeGenerator.Generator
{
    public class InterfaceGenerator
    {

        /// <summary> Variable to store the currently used file. </summary> 
        private StreamWriter interfaceFile;

        /// <summary> Variable which will contain the needed information for the class generator logic. </summary> 
        private UML_Interface umlInterface;


        /// <summary> Constructor for the class. Takes the current file and the data object as input. </summary> 
        public InterfaceGenerator(StreamWriter interfaceFile, UML_Interface umlInterface)
        {
            this.interfaceFile = interfaceFile;
            this.umlInterface = umlInterface;
        }



        /// <summary> Top-method orchestrating the generator steps. </summary>
        public void generateInterface()
        {
            // Write interface
            writeInterface();

            // Write attributes
            writeAttributes();

            // Write methods
            writeMethods();

            // Ending line
            interfaceFile.WriteLine("}");
        }


        /// <summary> Writes the introductory interface line with the name matching what is found in the UML_Interface object. </summary>
        void writeInterface()
        {

            // Write first line
            string interfaceString = $"interface {umlInterface.name}\n{{";
            interfaceFile.WriteLine(interfaceString);

        }


        /// <summary> Writes attributes into a specified file with the name and type matching what is found in the passed list of attributes that belong to the current interface. </summary>
        void writeAttributes()
        {

            // Extract attributes
            List<UML_Attribute> umlAttributes = umlInterface.umlAttributes;

            // Iterate over attribute list
            foreach (UML_Attribute umlAttribute in umlAttributes)
            {
                // Write attribute to file
                string attributeString = $"{umlAttribute.type} {umlAttribute.name} {{get; set;}}";
                interfaceFile.WriteLine(attributeString);
            }

        }


        /// <summary> Writes functions into a specified file with the name and parameters matching what is found in the passed list of methods that belong to the current interface. </summary>
        void writeMethods()
        {

            // Extract methods
            List<UML_Method> umlMethods = umlInterface.umlMethods;

            // Iterate over method list
            foreach (UML_Method umlMethod in umlMethods)
            {
                StringBuilder sb = new StringBuilder();

                // First line
                sb.Append($"{umlMethod.type} {umlMethod.name}(");
                foreach (UML_Parameter umlParameter in umlMethod.parameters)
                {
                    sb.Append($"{umlParameter.parameterName} {umlParameter.parameterType}, ");
                }

                // Delete trailing ,
                if (umlMethod.parameters.Count < 0) sb.Length -= 2;

                
                sb.Append(")");

                // Write built string
                interfaceFile.WriteLine(sb.ToString());

            }
        }

    }
}
