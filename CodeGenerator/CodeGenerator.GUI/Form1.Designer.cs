namespace CodeGenerator.GUI
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.SelectFileButton = new System.Windows.Forms.Button();
            this.PathModelLabel = new System.Windows.Forms.Label();
            this.SelectOutputButton = new System.Windows.Forms.Button();
            this.PathOutputLabel = new System.Windows.Forms.Label();
            this.GenerateButton = new System.Windows.Forms.Button();
            this.openFileDialogFile = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialogOutput = new System.Windows.Forms.FolderBrowserDialog();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.FilePictureBox = new System.Windows.Forms.PictureBox();
            this.OutputPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FilePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OutputPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // SelectFileButton
            // 
            this.SelectFileButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.SelectFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectFileButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SelectFileButton.Location = new System.Drawing.Point(12, 48);
            this.SelectFileButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SelectFileButton.MaximumSize = new System.Drawing.Size(213, 31);
            this.SelectFileButton.MinimumSize = new System.Drawing.Size(213, 31);
            this.SelectFileButton.Name = "SelectFileButton";
            this.SelectFileButton.Size = new System.Drawing.Size(213, 31);
            this.SelectFileButton.TabIndex = 0;
            this.SelectFileButton.Text = "Datei auswählen...";
            this.SelectFileButton.UseVisualStyleBackColor = false;
            this.SelectFileButton.Click += new System.EventHandler(this.SelectFileButton_Click);
            // 
            // PathModelLabel
            // 
            this.PathModelLabel.BackColor = System.Drawing.SystemColors.MenuBar;
            this.PathModelLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.PathModelLabel.Location = new System.Drawing.Point(233, 54);
            this.PathModelLabel.Name = "PathModelLabel";
            this.PathModelLabel.Size = new System.Drawing.Size(300, 20);
            this.PathModelLabel.TabIndex = 1;
            this.PathModelLabel.Text = "Keine Datei ausgewählt!";
            // 
            // SelectOutputButton
            // 
            this.SelectOutputButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.SelectOutputButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectOutputButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SelectOutputButton.Location = new System.Drawing.Point(12, 88);
            this.SelectOutputButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SelectOutputButton.MaximumSize = new System.Drawing.Size(213, 31);
            this.SelectOutputButton.MinimumSize = new System.Drawing.Size(213, 31);
            this.SelectOutputButton.Name = "SelectOutputButton";
            this.SelectOutputButton.Size = new System.Drawing.Size(213, 31);
            this.SelectOutputButton.TabIndex = 2;
            this.SelectOutputButton.Text = "Ausgabeort auswählen...";
            this.SelectOutputButton.UseVisualStyleBackColor = false;
            this.SelectOutputButton.Click += new System.EventHandler(this.SelectOutputButton_Click);
            // 
            // PathOutputLabel
            // 
            this.PathOutputLabel.BackColor = System.Drawing.SystemColors.MenuBar;
            this.PathOutputLabel.Location = new System.Drawing.Point(233, 96);
            this.PathOutputLabel.Name = "PathOutputLabel";
            this.PathOutputLabel.Size = new System.Drawing.Size(300, 20);
            this.PathOutputLabel.TabIndex = 3;
            this.PathOutputLabel.Text = "Keinen Ausgabeort ausgewählt!";
            // 
            // GenerateButton
            // 
            this.GenerateButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.GenerateButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.GenerateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GenerateButton.Location = new System.Drawing.Point(200, 150);
            this.GenerateButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GenerateButton.Name = "GenerateButton";
            this.GenerateButton.Size = new System.Drawing.Size(149, 50);
            this.GenerateButton.TabIndex = 4;
            this.GenerateButton.Text = "Generieren";
            this.GenerateButton.UseVisualStyleBackColor = false;
            this.GenerateButton.Click += new System.EventHandler(this.GenerateButton_Click);
            // 
            // openFileDialogFile
            // 
            this.openFileDialogFile.Filter = "GRAPHML FILES (*.graphml)|*.graphml";
            this.openFileDialogFile.Title = "Select a \".graphml\" file";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FilePictureBox
            // 
            this.FilePictureBox.BackColor = System.Drawing.SystemColors.Control;
            this.FilePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FilePictureBox.Location = new System.Drawing.Point(231, 52);
            this.FilePictureBox.Name = "FilePictureBox";
            this.FilePictureBox.Size = new System.Drawing.Size(305, 24);
            this.FilePictureBox.TabIndex = 5;
            this.FilePictureBox.TabStop = false;
            // 
            // OutputPictureBox
            // 
            this.OutputPictureBox.BackColor = System.Drawing.SystemColors.Control;
            this.OutputPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OutputPictureBox.Location = new System.Drawing.Point(231, 93);
            this.OutputPictureBox.Name = "OutputPictureBox";
            this.OutputPictureBox.Size = new System.Drawing.Size(305, 24);
            this.OutputPictureBox.TabIndex = 6;
            this.OutputPictureBox.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(576, 434);
            this.Controls.Add(this.GenerateButton);
            this.Controls.Add(this.PathOutputLabel);
            this.Controls.Add(this.SelectOutputButton);
            this.Controls.Add(this.PathModelLabel);
            this.Controls.Add(this.SelectFileButton);
            this.Controls.Add(this.FilePictureBox);
            this.Controls.Add(this.OutputPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximumSize = new System.Drawing.Size(594, 481);
            this.MinimumSize = new System.Drawing.Size(594, 481);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "CodeGenerator";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FilePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OutputPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SelectFileButton;
        private System.Windows.Forms.Label PathModelLabel;
        private System.Windows.Forms.Button SelectOutputButton;
        private System.Windows.Forms.Label PathOutputLabel;
        private System.Windows.Forms.Button GenerateButton;
        private System.Windows.Forms.OpenFileDialog openFileDialogFile;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogOutput;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.PictureBox FilePictureBox;
        private System.Windows.Forms.PictureBox OutputPictureBox;
    }
}

