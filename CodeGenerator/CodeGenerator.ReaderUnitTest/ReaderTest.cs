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
            UML_Attribute attribute = new UML_Attribute() {
                accessModifier = '+',
                name = "name ",
                type = " string"
            };
            expectedAttributes.Add(attribute);

            List<UML_Attribute> classAttributes = new List<UML_Attribute>();

            // Act
            XmlReader reader = new XmlTextReader(filepath);
            classAttributes = CodeGenerator.Reader.Reader.AnalyzeAttributeLabel(reader);

            // Assert
            //Assert.Equal(expectedAttributes, classAttributes);
            Assert.Equal(expectedAttributes.ElementAt(0), classAttributes.ElementAt(0));
            Assert.Equal(expectedAttributes.ElementAt(1), classAttributes.ElementAt(1));
            Assert.Equal(expectedAttributes.ElementAt(2), classAttributes.ElementAt(2));
        }
    }
}
