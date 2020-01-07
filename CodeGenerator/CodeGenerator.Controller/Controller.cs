using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonInterfaces;
using CodeGenerator.GUI;

namespace CodeGenerator.Controller
{
    public class Controller : IController
    {
        public bool StartProcess(string filePath_Model, string filePath_Output)
        {
            return true;
            
        }

        public bool checkPermission(string filePath){ return false; }

        public bool exchangeData(){ return true; }
    }
}