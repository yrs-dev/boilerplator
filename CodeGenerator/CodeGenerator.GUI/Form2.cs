using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeGenerator.GUI
{
    public partial class Form2 : Form
    {
        public Form2(Exception ex)
        {
            InitializeComponent();

            // Nachdem das Form erstellt wird, werden die Labels mit Name und Message
            // der im Konstruktor angegebenen Exceptionklasse überschrieben.
            ErrorDescribtionLabel.Text = ex.Message;
            ErrorNameLabel.Text = ex.ToString();
        }

        /// <summary>
        /// Wenn Der Ok-Button geklickt wird, wird das Form geschlossen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkErrorButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
