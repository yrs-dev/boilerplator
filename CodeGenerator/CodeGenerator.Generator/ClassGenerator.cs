using System.Collections.Generic;
using System.Text;
using System.IO;
using CodeGenerator.Datamodel;

namespace CodeGenerator.Generator
{
    /// <summary> Class responsible for generating UML classes. </summary> 
    public class ClassGenerator : BaseGenerator
    {

        // Constructor
        public ClassGenerator(StreamWriter outputFile, UML_BaseExtension umlClass) : base(outputFile, umlClass)
        {

        }


        /// <summary>
        /// Writes introductory line to the class file.
        /// </summary>
        /// <param name="umlClass"> Object containing information about the class. </param>
        /// <returns></returns>
        public override StringBuilder writeBeginning_Specify(UML_BaseExtension umlClass)
        {
            // Write beginning
            StringBuilder sb = umlClass.extraKeyword.Equals("") ? new StringBuilder($"{umlClass.accessModifier} class {umlClass.name}") : new StringBuilder($"{umlClass.accessModifier} {umlClass.extraKeyword} class {umlClass.name}");

            // Append parents, interfaces
            writeBeginning_appendElements(sb, (UML_Class)umlClass);

            // Return StringBuilder
            return sb;

        }
        
        /// <summary> 
        /// Writes elements belonging to the class declaration that may or may not be neccessary (e.g. parent classes or interfaces). 
        /// </summary>
        /// <param name="sb"> Currently used StringBuilder. </param>
        private void writeBeginning_appendElements(StringBuilder sb, UML_Class umlClass)
        {

            // Determine presence of parent / implemented interfaces
            bool parentPresent = umlClass.parent != null;
            bool implementedInterfacesPresent = umlClass.implementedInterfaces.Count > 0;
            bool aditionalElementsPresent = parentPresent || implementedInterfacesPresent;

            // Append colon
            if (aditionalElementsPresent)
            {
                sb.Append(" : ");
            }

            // Append parents
            if (parentPresent)
            {
                sb.Append(umlClass.parent.name);
                if (implementedInterfacesPresent) sb.Append(", ");
            }

            // Append interfaces
            if (implementedInterfacesPresent)
            {
                foreach (UML_Interface implementedInterface in umlClass.implementedInterfaces)
                {
                    sb.Append($"{implementedInterface.name}, ");
                }

                // Delete last comma
                sb.Length -= 2;
            }
        }

        /// <summary>
        /// Fills in the attribute definition.
        /// </summary>
        /// <param name="umlAttribute"> Object containing information about the attribute. </param>
        /// <returns> Returns the string representing the UML_Attribute object. </returns>
        public override string writeAttribute_Specify(UML_Attribute umlAttribute)
        {

            // Overall attribute structure
            StringBuilder sb = umlAttribute.extraKeyword.Equals("") ? new StringBuilder($"{umlAttribute.accessModifier} {umlAttribute.type} {umlAttribute.name}") : new StringBuilder($"{umlAttribute.accessModifier} {umlAttribute.extraKeyword} {umlAttribute.type} {umlAttribute.name}");

            // Append respective information
            if (umlAttribute.autoGetterSetterSpecified)
            {
                sb.Append(" { get; set; }");
            }
            else
            {
                sb.Append(";");
            }
            
            // Return built string
            return sb.ToString();
        }
        
        /// <summary>
        /// Fills in method body.
        /// </summary>
        /// <param name="sb"> Current StringBuilder being used. </param>
        public override void writeMethod_Specify(StringBuilder sb)
        {
            sb.Append($"\n{structureTab}{{");
            sb.Append($"\n{structureTab}{structureTab}throw new System.NotImplementedException();");
            sb.Append($"\n{structureTab}}}\n");
        }
    }
}
