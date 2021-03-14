
namespace NFUIRSL.HRTK.Vision.TestForm
{
    partial class Form1
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
            this.pictureBoxDisplay = new System.Windows.Forms.PictureBox();
            this.buttonOpenFreeRun = new System.Windows.Forms.Button();
            this.buttonStopFreeRun = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonChooseCamera = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxDisplay
            // 
            this.pictureBoxDisplay.Location = new System.Drawing.Point(53, 23);
            this.pictureBoxDisplay.Name = "pictureBoxDisplay";
            this.pictureBoxDisplay.Size = new System.Drawing.Size(607, 305);
            this.pictureBoxDisplay.TabIndex = 0;
            this.pictureBoxDisplay.TabStop = false;
            // 
            // buttonOpenFreeRun
            // 
            this.buttonOpenFreeRun.Location = new System.Drawing.Point(103, 359);
            this.buttonOpenFreeRun.Name = "buttonOpenFreeRun";
            this.buttonOpenFreeRun.Size = new System.Drawing.Size(88, 30);
            this.buttonOpenFreeRun.TabIndex = 1;
            this.buttonOpenFreeRun.Text = "Open";
            this.buttonOpenFreeRun.UseVisualStyleBackColor = true;
            // 
            // buttonStopFreeRun
            // 
            this.buttonStopFreeRun.Location = new System.Drawing.Point(242, 359);
            this.buttonStopFreeRun.Name = "buttonStopFreeRun";
            this.buttonStopFreeRun.Size = new System.Drawing.Size(88, 30);
            this.buttonStopFreeRun.TabIndex = 1;
            this.buttonStopFreeRun.Text = "Stop";
            this.buttonStopFreeRun.UseVisualStyleBackColor = true;
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(359, 359);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(88, 30);
            this.buttonExit.TabIndex = 1;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            // 
            // buttonChooseCamera
            // 
            this.buttonChooseCamera.Location = new System.Drawing.Point(488, 359);
            this.buttonChooseCamera.Name = "buttonChooseCamera";
            this.buttonChooseCamera.Size = new System.Drawing.Size(88, 30);
            this.buttonChooseCamera.TabIndex = 1;
            this.buttonChooseCamera.Text = "Choose";
            this.buttonChooseCamera.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonChooseCamera);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonStopFreeRun);
            this.Controls.Add(this.buttonOpenFreeRun);
            this.Controls.Add(this.pictureBoxDisplay);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDisplay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxDisplay;
        private System.Windows.Forms.Button buttonOpenFreeRun;
        private System.Windows.Forms.Button buttonStopFreeRun;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonChooseCamera;
    }
}

