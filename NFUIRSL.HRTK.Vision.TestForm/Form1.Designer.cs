
namespace NFUIRSL.HRTK.Vision.TestForm
{
    partial class VisionTestForm
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
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonChooseCamera = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonCapModeSnapshot = new System.Windows.Forms.Button();
            this.radioButtonCapModeStop = new System.Windows.Forms.RadioButton();
            this.radioButtonCapModeFreeRun = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDisplay)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxDisplay
            // 
            this.pictureBoxDisplay.Location = new System.Drawing.Point(28, 12);
            this.pictureBoxDisplay.Name = "pictureBoxDisplay";
            this.pictureBoxDisplay.Size = new System.Drawing.Size(607, 305);
            this.pictureBoxDisplay.TabIndex = 0;
            this.pictureBoxDisplay.TabStop = false;
            // 
            // buttonOpenFreeRun
            // 
            this.buttonOpenFreeRun.Location = new System.Drawing.Point(28, 359);
            this.buttonOpenFreeRun.Name = "buttonOpenFreeRun";
            this.buttonOpenFreeRun.Size = new System.Drawing.Size(88, 30);
            this.buttonOpenFreeRun.TabIndex = 1;
            this.buttonOpenFreeRun.Text = "Open";
            this.buttonOpenFreeRun.UseVisualStyleBackColor = true;
            this.buttonOpenFreeRun.Click += new System.EventHandler(this.buttonOpenFreeRun_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(28, 395);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(88, 30);
            this.buttonExit.TabIndex = 1;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonChooseCamera
            // 
            this.buttonChooseCamera.Location = new System.Drawing.Point(28, 323);
            this.buttonChooseCamera.Name = "buttonChooseCamera";
            this.buttonChooseCamera.Size = new System.Drawing.Size(88, 30);
            this.buttonChooseCamera.TabIndex = 1;
            this.buttonChooseCamera.Text = "Choose";
            this.buttonChooseCamera.UseVisualStyleBackColor = true;
            this.buttonChooseCamera.Click += new System.EventHandler(this.buttonChooseCamera_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonCapModeSnapshot);
            this.groupBox1.Controls.Add(this.radioButtonCapModeStop);
            this.groupBox1.Controls.Add(this.radioButtonCapModeFreeRun);
            this.groupBox1.Location = new System.Drawing.Point(122, 323);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(135, 133);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cap Mode";
            // 
            // buttonCapModeSnapshot
            // 
            this.buttonCapModeSnapshot.Location = new System.Drawing.Point(26, 88);
            this.buttonCapModeSnapshot.Name = "buttonCapModeSnapshot";
            this.buttonCapModeSnapshot.Size = new System.Drawing.Size(86, 35);
            this.buttonCapModeSnapshot.TabIndex = 1;
            this.buttonCapModeSnapshot.Text = "Snapshot";
            this.buttonCapModeSnapshot.UseVisualStyleBackColor = true;
            this.buttonCapModeSnapshot.Click += new System.EventHandler(this.buttonCapModeSnapshot_Click);
            // 
            // radioButtonCapModeStop
            // 
            this.radioButtonCapModeStop.AutoSize = true;
            this.radioButtonCapModeStop.Location = new System.Drawing.Point(6, 49);
            this.radioButtonCapModeStop.Name = "radioButtonCapModeStop";
            this.radioButtonCapModeStop.Size = new System.Drawing.Size(54, 19);
            this.radioButtonCapModeStop.TabIndex = 0;
            this.radioButtonCapModeStop.Text = "Stop";
            this.radioButtonCapModeStop.UseVisualStyleBackColor = true;
            // 
            // radioButtonCapModeFreeRun
            // 
            this.radioButtonCapModeFreeRun.AutoSize = true;
            this.radioButtonCapModeFreeRun.Checked = true;
            this.radioButtonCapModeFreeRun.Location = new System.Drawing.Point(6, 24);
            this.radioButtonCapModeFreeRun.Name = "radioButtonCapModeFreeRun";
            this.radioButtonCapModeFreeRun.Size = new System.Drawing.Size(80, 19);
            this.radioButtonCapModeFreeRun.TabIndex = 0;
            this.radioButtonCapModeFreeRun.TabStop = true;
            this.radioButtonCapModeFreeRun.Text = "Free Run";
            this.radioButtonCapModeFreeRun.UseVisualStyleBackColor = true;
            this.radioButtonCapModeFreeRun.CheckedChanged += new System.EventHandler(this.radioButtonCapModeFreeRun_CheckedChanged);
            // 
            // VisionTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 622);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonChooseCamera);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonOpenFreeRun);
            this.Controls.Add(this.pictureBoxDisplay);
            this.Name = "VisionTestForm";
            this.Text = "Vision Test";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDisplay)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxDisplay;
        private System.Windows.Forms.Button buttonOpenFreeRun;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonChooseCamera;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonCapModeStop;
        private System.Windows.Forms.RadioButton radioButtonCapModeFreeRun;
        private System.Windows.Forms.Button buttonCapModeSnapshot;
    }
}

