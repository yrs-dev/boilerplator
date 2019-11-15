using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonInterfaces
{
    interface IController
    {
        bool startProcess(string filePath_Model, string filePath_Output);
    }
}