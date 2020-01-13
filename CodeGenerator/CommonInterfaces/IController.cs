using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonInterfaces
{
    public interface IController
    {
        Exception StartProcess(string filePath_Model, string filePath_Output);
    }
}