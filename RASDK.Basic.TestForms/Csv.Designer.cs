
namespace RASDK.Basic.TestForms
{
    partial class Csv
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
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.buttonRead = new System.Windows.Forms.Button();
            this.labelReadedFile = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxPath
            // 
            this.textBoxPath.Location = new System.Drawing.Point(27, 25);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(500, 38);
            this.textBoxPath.TabIndex = 0;
            this.textBoxPath.Text = "C:\\target.csv";
            // 
            // buttonRead
            // 
            this.buttonRead.Location = new System.Drawing.Point(27, 86);
            this.buttonRead.Name = "buttonRead";
            this.buttonRead.Size = new System.Drawing.Size(110, 53);
            this.buttonRead.TabIndex = 1;
            this.buttonRead.Text = "Read";
            this.buttonRead.UseVisualStyleBackColor = true;
            this.buttonRead.Click += new System.EventHandler(this.buttonRead_Click);
            // 
            // labelReadedFile
            // 
            this.labelReadedFile.AutoSize = true;
            this.labelReadedFile.Location = new System.Drawing.Point(27, 253);
            this.labelReadedFile.Name = "labelReadedFile";
            this.labelReadedFile.Size = new System.Drawing.Size(33, 32);
            this.labelReadedFile.TabIndex = 2;
            this.labelReadedFile.Text = "--";
            // 
            // Csv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 805);
            this.Controls.Add(this.labelReadedFile);
            this.Controls.Add(this.buttonRead);
            this.Controls.Add(this.textBoxPath);
            this.Name = "Csv";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Button buttonRead;
        private System.Windows.Forms.Label labelReadedFile;
    }
}

