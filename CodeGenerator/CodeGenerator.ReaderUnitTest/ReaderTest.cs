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
    }
}
