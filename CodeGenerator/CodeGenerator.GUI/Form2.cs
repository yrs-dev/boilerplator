using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeGen
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            string ErrorName = "ArgumentNullException";
            string ErrorDescribtion = "Du hast nicht beide Pfade ausgewählt!";
            string ErrorRestification = "Bitte wähle eine .graphml-Datei und einen Ausgabeort.";
            ErrorNameLabel.Text = ErrorName;
            ErrorDescribtionLabel.Text = ErrorDescribtion;
            ErrorRestificationLabel.Text = ErrorRestification;
        }

        private void OkErrorButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
