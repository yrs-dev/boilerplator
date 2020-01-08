using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonInterfaces;
using Exceptions;
using CodeGenerator.Controller;

namespace CodeGenerator.GUI
{
    public partial class Form1 : Form, CommonInterfaces.IController
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Wenn der SelectFileButton geklickt wird, wird die openFileDialog-Komponente aufgerufen, 
        /// um die Modellierdatei auszuwählen. Wenn ein Dateipfad ausgewählt wurde, wird 
        /// dieser in das Path_Model-Label geschrieben.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectFileButton_Click(object sender, EventArgs e)
        {
            if(openFileDialogFile.ShowDialog()==DialogResult.OK)
            {
                Path_Model.Text = openFileDialogFile.FileName;
            }
        }

        /// <summary>
        /// Wenn der SelectOutputButton geklickt wird, wird die folderBrowserDialog-Komponente 
        /// aufgerufen, um den Ausgabeort auszuwählen. Wenn ein Ausgabeort ausgewählt wurde, wird 
        /// dieser in das Path_Output-Label geschrieben.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectOutputButton_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialogOutput.ShowDialog() == DialogResult.OK)
            {
                Path_Output.Text = folderBrowserDialogOutput.SelectedPath;
            }
        }

        /// <summary>
        /// Wenn der GenerateButton geklickt wird, werden die beiden ausgewählten Pfade in
        /// string Variablen gespeichert, geprüft ob diese ausgewählt wurden und StartProcess() aufgerufen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenerateButton_Click(object sender, EventArgs e)
        {
            string filePath_Model = Path_Model.Text;
            string filePath_Output = Path_Output.Text;

            // Wenn  der Text auf den filePath-Labels sich nicht geändert hat, 
            // wird Form2 mit der FileNotChoosenException aufgerufen.
            if (filePath_Model == "Keine Datei ausgewählt."
                || filePath_Output == "Keinen Ausgabeort ausgewählt.")
            {
                CreateNewErrorForm(new FileIsNotChoosenException());
            }
            else
            {
                StartProcess(filePath_Model, filePath_Output);
            }
                
        }

        /// <summary>
        /// Interface Methode von IController. Startet den Prozess, 
        /// indem sie die StartProcess()-Methode vom Controller aufruft.
        /// </summary>
        /// <param name="filePath_Model">Dateipfad im Typ string</param>
        /// <param name="filePath_Output">Ausgabepfad im Typ string</param>
        /// <returns> Rückgabe von controller.Startprocess() </returns>
        public bool StartProcess(string filePath_Model, string filePath_Output)
        {
            try
            {
                Controller.Controller controller = new Controller.Controller();
                controller.StartProcess(filePath_Model, filePath_Output);
            }
            catch(Exceptions.DatamodelMissingContentException)
            {
                CreateNewErrorForm(new DatamodelMissingContentException("Text"));
            }
            catch(Exceptions.DatamodelMissingInformationException)
            {
                CreateNewErrorForm(new DatamodelMissingInformationException("Text"));
            }

            return true;
        }

        /// <summary>
        /// Erstellt und Öffnet Neues Error-Fenster, um den Nutzer einen Fehler anzuzeigen.
        /// </summary>
        /// <param name="ex">Die Exception die Ausgelöst wurde, um dazugehörigen Namen und Message anzuzeigen</param>
        public void CreateNewErrorForm(Exception ex)
        {
            new Form2(ex).ShowDialog();
            this.Show();
        }
    }
}
