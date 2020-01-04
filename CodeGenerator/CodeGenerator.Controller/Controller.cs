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
        public bool IController.StartProcess(string filePath_Model, string filePath_Output)
        {
            
        }

        public bool checkPermission(string filePath){}

        public bool exchangeData(){}
    }
}