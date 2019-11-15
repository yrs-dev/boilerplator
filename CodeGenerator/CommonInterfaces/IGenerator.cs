using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeGenerator.Reader;

namespace CommonInterfaces
{
    interface IGenerator
    {
        bool generateCode(string filePath, Datamodel dml);

    }
}
