using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonInterfaces;
using CodeGenerator.Reader;
using CodeGenerator.Datamodel;
using CodeGenerator.Generator;

namespace CodeGenerator.Controller
{
    public class Controller : IController
    {
        /// <summary>
        /// Interface-Methode StartProcess(). Wenn Berechtigung auf Graphml-Datei erlaubt ist, erstellt Sie Reader
        /// und Generator, ruft deren Main-Methoden auf und gibt Pfade und Datamodell weiter.
        /// </summary>
        /// <param name="filePath_Model">Graphml-Dateipfad als string vom GUI</param>
        /// <param name="filePath_Output">Ausgabepfad als string vom GUI</param>
        /// <returns>true, wenn Berechtigung erlaubt ist</returns>
        public bool StartProcess(string filePath_Model, string filePath_Output)
        {
            if (checkPermission(filePath_Model))
            {
                Reader.Reader reader = new Reader.Reader();
                Datamodel.Datamodel datamodel = reader.ReadGraphml(filePath_Model);
                Generator.Generator generator = new Generator.Generator(filePath_Output, datamodel);
                generator.generateCode(filePath_Output, datamodel);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool checkPermission(string filePath)
        {
            return true;
        }
    }
}