using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.ControllerTest
{
    class Program
    {
        static void Main(string[] args)
        {

            StartProcess(Environment.CurrentDirectory + "/filepath.graphml", Environment.CurrentDirectory);

        }

        private static bool StartProcess(string filePath_Model, string filePath_Output)
        {Reader.Reader reader = new Reader.Reader(filePath_Model);
            Datamodel.Datamodel datamodel = reader.getDatamodel();
            Generator.Generator generator = new Generator.Generator(filePath_Output, datamodel);
            return generator.generateCode();
        }   
    }
}
