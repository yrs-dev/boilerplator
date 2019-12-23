using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.IO;
using System.Xml;
using CodeGenerator.Datamodel;

namespace CodeGenerator.ReaderUnitTest
{
    
    public class ReaderTest
    {
        [Fact]
        public void CanGetValueofAnalyzeNodeLabel()
        {
            // Arange
            string className = "";
            string filepath = Environment.CurrentDirectory + "/classdiagram.graphml";

            UML_Class ClassExpected = new UML_Class(className);
            ClassExpected.name = "Employee";


            // Act
            XmlReader reader = new XmlTextReader(filepath);
            UML_Class ClassActual = new UML_Class(className);
            ClassActual = CodeGenerator.Reader.Reader.AnalyzeNodeLabel(reader);

            // Assert
            Assert.Equal(ClassExpected.name ,ClassActual.name);
        }

        [Fact]
        public void CanGetValueOfAnalyzeAttributeLabel()
        {
            // Arrange
            string filepath = Environment.CurrentDirectory + "/classdiagram.graphml";

            List<UML_Attribute> expectedAttributes = new List<UML_Attribute>();
            UML_Attribute attribute1 = new UML_Attribute() {
                accessModifier = '+',
                name = "name",
                type = "string"
            };
            UML_Attribute attribute2 = new UML_Attribute()
            {
                accessModifier = '+',
                name = "age",
                type = "int"
            };
            expectedAttributes.Add(attribute1);
            expectedAttributes.Add(attribute2);

            List<UML_Attribute> classAttributes = new List<UML_Attribute>();

            // Act
            XmlReader reader = new XmlTextReader(filepath);
            classAttributes = CodeGenerator.Reader.Reader.AnalyzeAttributeLabel(reader);

            // Assert
            Assert.Equal(expectedAttributes, classAttributes);
        }
    }
}
