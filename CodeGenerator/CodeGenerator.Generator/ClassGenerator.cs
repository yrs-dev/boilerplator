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
        public ClassGenerator(StreamWriter outputFile, UML_BaseExtension classOrInterface) : base(outputFile, classOrInterface)
        {

        }

        public override StringBuilder writeBeginning_FirstLine(UML_BaseExtension classOrInterface)
        {
            // Write beginning
            StringBuilder sb = new StringBuilder($"{classOrInterface.accessModifier} class {classOrInterface.name}");

            // Append parents, interfaces
            writeBeginning_appendElements(sb, (UML_Class)classOrInterface);

            // Return StringBuilder
            return sb;

        }
        
        /// <summary> Writes elements belonging to the class declaration that may or may not be neccessary (e.g. parent classes or interfaces). </summary>
        /// <param name="sb">Currently used StringBuilder.</param>
        private void writeBeginning_appendElements(StringBuilder sb, UML_Class umlClass)
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
                sb.Length -= 2;
            }
        }

        public override string writeAttribute(UML_Attribute umlAttribute)
        {
            string attributeString = $"{umlAttribute.accessModifier} {umlAttribute.type} {umlAttribute.name};";
            return attributeString;
        }

        public override void writeMethod_Body(StringBuilder sb)
        {
            sb.Append($"\n{structureTab}{{");
            sb.Append($"\n{structureTab}{structureTab}throw new System.NotImplementedException();");
            sb.Append($"\n{structureTab}}}\n");
        }
    }
}
