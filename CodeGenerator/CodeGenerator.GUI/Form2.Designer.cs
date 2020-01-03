namespace CodeGenerator.GUI
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.OkErrorButton = new System.Windows.Forms.Button();
            this.ErrorNameLabel = new System.Windows.Forms.Label();
            this.ErrorDescribtionLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // OkErrorButton
            // 
            this.OkErrorButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OkErrorButton.Location = new System.Drawing.Point(150, 180);
            this.OkErrorButton.Name = "OkErrorButton";
            this.OkErrorButton.Size = new System.Drawing.Size(108, 34);
            this.OkErrorButton.TabIndex = 1;
            this.OkErrorButton.Text = "OK";
            this.OkErrorButton.UseVisualStyleBackColor = true;
            this.OkErrorButton.Click += new System.EventHandler(this.OkErrorButton_Click);
            // 
            // ErrorNameLabel
            // 
            this.ErrorNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ErrorNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorNameLabel.Location = new System.Drawing.Point(3, 0);
            this.ErrorNameLabel.Name = "ErrorNameLabel";
            this.ErrorNameLabel.Size = new System.Drawing.Size(296, 32);
            this.ErrorNameLabel.TabIndex = 0;
            this.ErrorNameLabel.Text = "Name des Fehlers";
            this.ErrorNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ErrorDescribtionLabel
            // 
            this.ErrorDescribtionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ErrorDescribtionLabel.Location = new System.Drawing.Point(3, 32);
            this.ErrorDescribtionLabel.Name = "ErrorDescribtionLabel";
            this.ErrorDescribtionLabel.Size = new System.Drawing.Size(296, 130);
            this.ErrorDescribtionLabel.TabIndex = 2;
            this.ErrorDescribtionLabel.Text = "Beschreibung des Fehlers.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CodeGenerator.GUI.Properties.Resources.error_image_icon;
            this.pictureBox1.Location = new System.Drawing.Point(12, 52);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(111, 109);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.ErrorDescribtionLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ErrorNameLabel, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(129, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(302, 162);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(443, 250);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.OkErrorButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(461, 297);
            this.MinimumSize = new System.Drawing.Size(461, 297);
            this.Name = "Form2";
            this.ShowIcon = false;
            this.Text = "ERROR";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button OkErrorButton;
        private System.Windows.Forms.Label ErrorNameLabel;
        private System.Windows.Forms.Label ErrorDescribtionLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}