using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.IO;
using System.Xml;
using CodeGenerator.Datamodel;
using CodeGenerator.Reader;

namespace CodeGenerator.ReaderUnitTest
{
    
    public class ReaderTest
    {
        [Fact]
        public void checkInterface()
        {
            // Arrange
            string filepath = Environment.CurrentDirectory + "/interfacediagram.graphml";
            UML_Interface expectedInterface = new UML_Interface("<<interface>>\nEmployee");

            // Act
            XmlReader reader = new XmlTextReader(filepath);
            Reader.Reader instanceForDatamodel = new Reader.Reader();

            UML_Base baseModel = instanceForDatamodel.AnalyzeNodeLabel<UML_Base>(reader);

            // Assert
            Assert.Equal(baseModel, expectedInterface);

        }

        [Fact]
        public void checkClass()
        {
            // Arrange
            string filepath = Environment.CurrentDirectory + "/classdiagram.graphml";
            UML_Class expectedClass = new UML_Class("Employee");

            // Act
            XmlReader reader = new XmlTextReader(filepath);
            Reader.Reader instanceForDatamodel = new Reader.Reader();

            UML_Base baseModel = instanceForDatamodel.AnalyzeNodeLabel<UML_Base>(reader);

            // Assert
            Assert.Equal(baseModel, expectedClass);
        }

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
            Reader.Reader instanceForClass = new CodeGenerator.Reader.Reader();
            UML_Class ClassActual = instanceForClass.AnalyzeNodeLabel<UML_Class>(reader);

            // Assert
            Assert.Equal(ClassExpected ,ClassActual);
        }

        [Fact]
        public void CanGetValueOfAnalyzeAttributeLabel()
        {
            // Arrange
            string filepath = Environment.CurrentDirectory + "/classdiagram.graphml";

            List<UML_Attribute> expectedAttributes = new List<UML_Attribute>();
            UML_Attribute attribute1 = new UML_Attribute() {
                accessModifier = "public",
                name = "name",
                type = "string"
            };
            UML_Attribute attribute2 = new UML_Attribute()
            {
                accessModifier = "public",
                name = "age",
                type = "int"
            };
            expectedAttributes.Add(attribute1);
            expectedAttributes.Add(attribute2);

            List<UML_Attribute> classAttributes = new List<UML_Attribute>();

            // Act
            XmlReader reader = new XmlTextReader(filepath);
            Reader.Reader instanceForAttributes = new Reader.Reader();
            classAttributes = instanceForAttributes.AnalyzeAttributeLabel(reader);

            // Assert
            Assert.Equal(expectedAttributes, classAttributes);
        }

        [Fact]
        public void CanGetValueOfAnalyzeMethodLabel()
        {
            // Arrange
            string filepath = Environment.CurrentDirectory + "/classdiagram.graphml";

            List<UML_Method> expectedMethods = new List<UML_Method>();
            UML_Method method1 = new UML_Method()
            {
                name = "getName",
                type = "String",
                parameters = new List<UML_Parameter>() { new UML_Parameter() { parameterName = "value", parameterType = "string" } }
            };
            UML_Method method2 = new UML_Method()
            {

                name = "getTitle",
                type = "String"
            };
            UML_Method method3 = new UML_Method()
            {

                name = "getStaffNo",
                type = "Number"
            };
            UML_Method method4 = new UML_Method()
            {

                name = "getRoom",
                type = "String"
            };
            UML_Method method5 = new UML_Method()
            {

                name = "getPhone"
            };

            expectedMethods.Add(method1);
            expectedMethods.Add(method2);
            expectedMethods.Add(method3);
            expectedMethods.Add(method4);
            expectedMethods.Add(method5);

            List<UML_Method> classMethods = new List<UML_Method>();

            // Act
            XmlReader reader = new XmlTextReader(filepath);
            Reader.Reader instanceForMethods = new Reader.Reader();
            classMethods = instanceForMethods.AnalyzeMethodLabel(reader);

            // Assert
            Assert.Equal(expectedMethods, classMethods);
        }
    }
}
