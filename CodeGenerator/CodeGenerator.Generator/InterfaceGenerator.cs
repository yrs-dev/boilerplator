using System.Collections.Generic;
using System.Text;
using System.IO;
using CodeGenerator.Datamodel;

namespace CodeGenerator.Generator
{
    public class InterfaceGenerator : BaseGenerator
    {

        // Constructor
        public InterfaceGenerator(StreamWriter outputFile, UML_BaseExtension classOrInterface) : base(outputFile, classOrInterface)
        {

        }

        // Inherited methods
        public override StringBuilder writeBeginning_FirstLine(UML_BaseExtension classOrInterface)
        {
            // Write beginning
            StringBuilder sb = new StringBuilder($"interface {classOrInterface.name}");

            // Return StringBuilder
            return sb;
        }

        public override string writeAttribute(UML_Attribute umlAttribute)
        {
            string attributeString = $"{umlAttribute.type} {umlAttribute.name};";
            return attributeString;
        }

        public override void writeMethod_Body(StringBuilder sb)
        {

        }

    }
}
