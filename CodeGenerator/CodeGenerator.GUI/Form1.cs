using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Exceptions;
using CodeGenerator.Controller;

namespace CodeGenerator.GUI
{
    public partial class Form1 : Form
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
        /// string Variablen gespeichert, geprüft ob diese ausgewählt wurden und CreateController() aufgerufen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenerateButton_Click(object sender, EventArgs e)
        {
            string filePath_Model = Path_Model.Text;
            string filePath_Output = Path_Output.Text;
            string noModel = "Keine Datei ausgewählt!";
            string noOutput = "Keinen Ausgabeort ausgewählt!";
            
            // Wenn Datei nicht ausgewählt und Ausgabeort ausgewählt wurde, wird Dateilabel rot und Ausgabeortlabel schwarz.
            if (filePath_Model == noModel && filePath_Output != noOutput)
            {
                Path_Model.ForeColor = Color.Red;
                Path_Output.ForeColor = DefaultForeColor;
            }

            // Wenn Datei ausgewählt und Ausgabeort nicht ausgewählt wurde, wird Dateilabel schwarz und Ausgabeortlabel rot.
            else if (filePath_Output == noOutput && filePath_Model != noModel)
            {
                Path_Output.ForeColor = Color.Red;
                Path_Model.ForeColor = DefaultForeColor;
            }

            // Wenn beide nicht ausgewählt wurden, werden beide Labels rot.
            else if (filePath_Output == noOutput && filePath_Model == noModel)
            {
                Path_Model.ForeColor = Color.Red;
                Path_Output.ForeColor = Color.Red;
            }

            // Letzte Möglichkeit: Beide ausgewählt. Beide werden schwarz und CreateController wird ausgeführt
            else
            {
                Path_Model.ForeColor = DefaultForeColor;
                Path_Output.ForeColor = DefaultForeColor;
                CreateController(filePath_Model, filePath_Output);
            }
                
        }

        /// <summary>
        /// Erstellt neues Controller-Objekt und ruft dessen StartProcess-Methode auf.
        /// Wenn die Methode eine Exception zurückgibt, wird CreateNewErrorForm() aufgerufen.
        /// </summary>
        /// <param name="filePath_Model">Dateipfad im Typ string</param>
        /// <param name="filePath_Output">Ausgabepfad im Typ string</param>
        public void CreateController(string filePath_Model, string filePath_Output)
        {
            Controller.Controller controller = new Controller.Controller();
            Exception ex = controller.StartProcess(filePath_Model, filePath_Output);
            if (ex != null)
                CreateNewErrorForm(ex);
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
