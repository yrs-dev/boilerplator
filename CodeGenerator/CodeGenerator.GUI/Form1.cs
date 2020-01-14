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
                PathModelLabel.Text = openFileDialogFile.FileName;
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
                PathOutputLabel.Text = folderBrowserDialogOutput.SelectedPath;
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
            string filePath_Model = PathModelLabel.Text;
            string filePath_Output = PathOutputLabel.Text;
            string noModel = "Keine Datei ausgewählt!";
            string noOutput = "Keinen Ausgabeort ausgewählt!";

            // Wenn Datei nicht ausgewählt und Ausgabeort ausgewählt wurde, wird FilePictureBox rot und
            // OutputPictureBox default. Bei FilePictureBox wird ErrorProvider ausgelöst und 
            // bei OutputPictureBox null gesetzt.
            if (filePath_Model == noModel && filePath_Output != noOutput)
            {
                errorProvider1.SetError(FilePictureBox,"Bitte wählen Sie eine \".graphml\"- Datei!");
                errorProvider1.SetError(OutputPictureBox, null);
                FilePictureBox.BackColor = Color.Red;
                OutputPictureBox.BackColor = DefaultBackColor;
            }

            // Wenn Datei ausgewählt und Ausgabeort nicht ausgewählt wurde, wird FilePictureBox default und
            // OutputPictureBox rot. Bei OutputPictureBox wird ErrorProvider ausgelöst
            // und bei FilePictureBox null gesetzt.
            else if (filePath_Output == noOutput && filePath_Model != noModel)
            {
                errorProvider1.SetError(OutputPictureBox, "Bitte wählen Sie einen Ausgabeort!");
                errorProvider1.SetError(FilePictureBox, null);
                OutputPictureBox.BackColor = Color.Red;
                FilePictureBox.BackColor = DefaultBackColor;
            }

            // Wenn beide nicht ausgewählt wurden, werden bei beiden PictureBoxes rot und ErrorProvider ausgelöst.
            else if (filePath_Output == noOutput && filePath_Model == noModel)
            {
                errorProvider1.SetError(FilePictureBox, "Bitte wählen Sie eine \".graphml\"- Datei!");
                errorProvider1.SetError(OutputPictureBox, "Bitte wählen Sie einen Ausgabeort!");
                OutputPictureBox.BackColor = Color.Red;
                FilePictureBox.BackColor = Color.Red;
            }

            // Letzte Möglichkeit: Beide ausgewählt. Beide werden default, ErrorProvider werden null gesetzt
            // und CreateController wird ausgeführt.
            else
            {
                errorProvider1.SetError(FilePictureBox, null);
                errorProvider1.SetError(OutputPictureBox, null);
                FilePictureBox.BackColor = DefaultBackColor;
                OutputPictureBox.BackColor = DefaultBackColor;
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
            {
                new Form2(ex).ShowDialog();
                this.Show();
            }
        }
    }
}
