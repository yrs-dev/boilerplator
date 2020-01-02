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
        /// string Variablen gespeichert und mit der Methode startProcess() aufgerufen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenerateButton_Click(object sender, EventArgs e)
        {
            string filePath_Model = Path_Model.Text;
            string filePate_Output = Path_Output.Text;

            // Wenn StartProcess false liefert, wird Form2 mit der FileNotFoundException geöffnet
            if (!StartProcess(filePath_Model, filePate_Output))
            {
                new Form2(new ArgumentNullException()).ShowDialog();
                this.Show();
            }
                
        }

        /// <summary>
        /// Interface Methode von IController. Startet den Prozess, 
        /// indem sie dem Controller ein true oder false zurückgibt.
        /// </summary>
        /// <param name="filePath_Model">Dateipfad im Typ string</param>
        /// <param name="filePath_Output">Ausgabepfad im Typ string</param>
        /// <returns>true, wenn beides ausgewählt wurde. false, wenn nicht.</returns>
        public bool StartProcess(string filePath_Model, string filePath_Output)
        {
            return filePath_Model != "Keine Datei ausgewählt." 
                && filePath_Output != "Keinen Ausgabeort ausgewählt.";
        }
    }
}
