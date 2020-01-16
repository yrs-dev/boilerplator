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
        // Test Komponente basierend auf die Struktur des alten Readers

        //[Fact]
        //public void checkInterface()
        //{
        //    // Arrange
        //    string filepath = Environment.CurrentDirectory + "/interfacediagram.graphml";
        //    string id = "n0";
        //    UML_Interface expectedInterface = new UML_Interface("<<interface>>\r\n\t\t  Employee",id);

        //    // Act
        //    XmlReader reader = new XmlTextReader(filepath);
        //    Reader.Reader instanceForDatamodel = new Reader.Reader(filepath);

        //    UML_Base baseModel = instanceForDatamodel.AnalyzeNodeLabel<UML_Base>("n0","name");

        //    // Assert
        //    Assert.Equal(baseModel, expectedInterface);

        //}

        //[Fact]
        //public void checkClass()
        //{
        //    // Arrange
        //    string filepath = Environment.CurrentDirectory + "/classdiagram.graphml";
        //    UML_Class expectedClass = new UML_Class("Employee", "n0");

        //    // Act
        //    XmlReader reader = new XmlTextReader(filepath);
        //    Reader.Reader instanceForDatamodel = new Reader.Reader(filepath);

        //    UML_Base baseModel = instanceForDatamodel.AnalyzeNodeLabel<UML_Base>("n0","name");

        //    // Assert
        //    Assert.Equal(baseModel, expectedClass);
        //}

        //[Fact]
        //public void CanGetValueOfAnalyzeNode()
        //{
        //    // Arrange
        //    string filepath = Environment.CurrentDirectory + "/classdiagram.graphml";
        //    List<UML_Base> expectedClassList = new List<UML_Base>(); 
        //    UML_Class expectedClass = new UML_Class("Employee", "n0");
        //    expectedClassList.Add(expectedClass);


        //    // Act
        //    XmlReader reader = new XmlTextReader(filepath);
        //    Reader.Reader instanceForDatamodel = new Reader.Reader(filepath);
        //    List<UML_Base> baseModelList = instanceForDatamodel.AnalyzeNode();


        //    // Assert
        //    Assert.Equal<UML_Base>(expectedClassList, baseModelList);
        //}

        //[Fact]
        //public void CanGetValueOfgetAttributeNode()
        //{
        //    // Arrange
        //    string filepath = Environment.CurrentDirectory + "/classdiagram.graphml";
        //    List<string> expectedId = new List<string>();
        //    string id = "n0";
        //    expectedId.Add(id);

        //    // Act
        //    Reader.Reader instanceOfReader = new Reader.Reader(filepath);
        //    List<string> actualId = instanceOfReader.getNodeAttributeValue(filepath);

        //    // Assert
        //    Assert.Equal(expectedId, actualId);
        //}

        //[Fact]
        //public void CanGetMultipleValueOfAnalyzeNode()
        //{
        //    // Arrange
        //    string filepath = Environment.CurrentDirectory + "/classdiagram.graphml";
        //    List<UML_Base> classes = new List<UML_Base>();
        //    UML_Class expectedClass = new UML_Class("Employee", "n0");
        //    UML_Class expectedClass2 = new UML_Class("User", "n1");
        //    classes.Add(expectedClass);
        //    classes.Add(expectedClass2);

        //    // Act
        //    XmlReader reader = XmlReader.Create(filepath);
        //    Reader.Reader instanceForDatamodel = new Reader.Reader(filepath);
        //    List<UML_Base> listOfClasses = instanceForDatamodel.AnalyzeNode();

        //    // Assert
        //    Assert.Equal(classes, listOfClasses);
        //}

        //[Fact]
        //public void CanGetValueofAnalyzeNodeLabel()
        //{
        //    // Arange
        //    string className = "";
        //    string id = "n0";
        //    string filepath = Environment.CurrentDirectory + "/classdiagram.graphml";

        //    UML_Class ClassExpected = new UML_Class(className, id);
        //    ClassExpected.name = "Employee";

        //    // Act
        //    XmlReader reader = XmlReader.Create(filepath);
        //    Reader.Reader instanceForClass = new CodeGenerator.Reader.Reader(filepath);
        //    UML_Class ClassActual = instanceForClass.AnalyzeNodeLabel<UML_Class>("n0","name");

        //    // Assert
        //    Assert.Equal(ClassExpected ,ClassActual);
        //}

        //[Fact]
        //public void CanGetValueOfAnalyzeAttributeLabel()
        //{
        //    // Arrange
        //    string filepath = Environment.CurrentDirectory + "/classdiagram.graphml";

        //    List<UML_Attribute> expectedAttributes = new List<UML_Attribute>();
        //    UML_Attribute attribute1 = new UML_Attribute() {
        //        accessModifier = "public",
        //        name = "name",
        //        type = "string"
        //    };
        //    UML_Attribute attribute2 = new UML_Attribute()
        //    {
        //        accessModifier = "public",
        //        name = "age",
        //        type = "int"
        //    };
        //    expectedAttributes.Add(attribute1);
        //    expectedAttributes.Add(attribute2);

        //    List<UML_Attribute> classAttributes = new List<UML_Attribute>();

        //    // Act
        //    XmlReader reader = new XmlTextReader(filepath);
        //    Reader.Reader instanceForAttributes = new Reader.Reader(filepath);
        //    classAttributes = instanceForAttributes.AnalyzeAttributeLabel(reader);

        //    // Assert
        //    Assert.Equal(expectedAttributes, classAttributes);
        //}

        //[Fact]
        //public void CanGetValueOfAnalyzeMethodLabel()
        //{
        //    // Arrange
        //    string filepath = Environment.CurrentDirectory + "/classdiagram.graphml";

        //    List<UML_Method> expectedMethods = new List<UML_Method>();
        //    UML_Method method1 = new UML_Method()
        //    {
        //        name = "getName",
        //        type = "String",
        //        parameters = new List<UML_Parameter>() { new UML_Parameter() { parameterName = "value", parameterType = "string" } }
        //    };
        //    UML_Method method2 = new UML_Method()
        //    {
        //        name = "getTitle",
        //        type = "String",
        //        parameters = null
        //    };
        //    UML_Method method3 = new UML_Method()
        //    {
        //        name = "getStaffNo",
        //        type = "Number",
        //        parameters = null
        //    };
        //    UML_Method method4 = new UML_Method()
        //    {
        //        name = "getRoom",
        //        type = "String",
        //        parameters = null
        //    };
        //    UML_Method method5 = new UML_Method()
        //    {
        //        name = "getPhone",
        //        parameters = null
        //    };

        //    expectedMethods.Add(method1);
        //    expectedMethods.Add(method2);
        //    expectedMethods.Add(method3);
        //    expectedMethods.Add(method4);
        //    expectedMethods.Add(method5);

        //    List<UML_Method> classMethods = new List<UML_Method>();

        //    // Act
        //    XmlReader reader = new XmlTextReader(filepath);
        //    Reader.Reader instanceForMethods = new Reader.Reader(filepath);
        //    classMethods = instanceForMethods.AnalyzeMethodLabel(reader);

        //    // Assert
        //    Assert.Equal(expectedMethods, classMethods);
        //}
    }
}
