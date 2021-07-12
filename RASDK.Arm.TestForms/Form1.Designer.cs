
namespace RASDK.Arm.TestForms
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
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.textBoxIp = new System.Windows.Forms.TextBox();
            this.buttonHoming = new System.Windows.Forms.Button();
            this.buttonMove1 = new System.Windows.Forms.Button();
            this.buttonMove2 = new System.Windows.Forms.Button();
            this.buttonJogXP = new System.Windows.Forms.Button();
            this.buttonJogXM = new System.Windows.Forms.Button();
            this.buttonJogYP = new System.Windows.Forms.Button();
            this.buttonJogZM = new System.Windows.Forms.Button();
            this.buttonJogYM = new System.Windows.Forms.Button();
            this.buttonJogZP = new System.Windows.Forms.Button();
            this.numericUpDownJogXY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownJogZ = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownJogXY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownJogZ)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(12, 63);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(158, 56);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Location = new System.Drawing.Point(12, 125);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(158, 56);
            this.buttonDisconnect.TabIndex = 0;
            this.buttonDisconnect.Text = "Disconnect";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // textBoxIp
            // 
            this.textBoxIp.Location = new System.Drawing.Point(12, 19);
            this.textBoxIp.Name = "textBoxIp";
            this.textBoxIp.Size = new System.Drawing.Size(200, 31);
            this.textBoxIp.TabIndex = 1;
            this.textBoxIp.Text = "192.168.100.111";
            // 
            // buttonHoming
            // 
            this.buttonHoming.Location = new System.Drawing.Point(12, 187);
            this.buttonHoming.Name = "buttonHoming";
            this.buttonHoming.Size = new System.Drawing.Size(158, 56);
            this.buttonHoming.TabIndex = 0;
            this.buttonHoming.Text = "Homing";
            this.buttonHoming.UseVisualStyleBackColor = true;
            this.buttonHoming.Click += new System.EventHandler(this.buttonHoming_Click);
            // 
            // buttonMove1
            // 
            this.buttonMove1.Location = new System.Drawing.Point(12, 249);
            this.buttonMove1.Name = "buttonMove1";
            this.buttonMove1.Size = new System.Drawing.Size(158, 56);
            this.buttonMove1.TabIndex = 0;
            this.buttonMove1.Text = "Move1";
            this.buttonMove1.UseVisualStyleBackColor = true;
            this.buttonMove1.Click += new System.EventHandler(this.buttonMove1_Click);
            // 
            // buttonMove2
            // 
            this.buttonMove2.Location = new System.Drawing.Point(12, 311);
            this.buttonMove2.Name = "buttonMove2";
            this.buttonMove2.Size = new System.Drawing.Size(158, 56);
            this.buttonMove2.TabIndex = 0;
            this.buttonMove2.Text = "Move2";
            this.buttonMove2.UseVisualStyleBackColor = true;
            this.buttonMove2.Click += new System.EventHandler(this.buttonMove2_Click);
            // 
            // buttonJogXP
            // 
            this.buttonJogXP.Location = new System.Drawing.Point(451, 154);
            this.buttonJogXP.Name = "buttonJogXP";
            this.buttonJogXP.Size = new System.Drawing.Size(50, 50);
            this.buttonJogXP.TabIndex = 2;
            this.buttonJogXP.Text = "X+";
            this.buttonJogXP.UseVisualStyleBackColor = true;
            this.buttonJogXP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonJogXP_MouseDown);
            this.buttonJogXP.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonJogXP_MouseUp);
            // 
            // buttonJogXM
            // 
            this.buttonJogXM.Location = new System.Drawing.Point(249, 154);
            this.buttonJogXM.Name = "buttonJogXM";
            this.buttonJogXM.Size = new System.Drawing.Size(50, 50);
            this.buttonJogXM.TabIndex = 2;
            this.buttonJogXM.Text = "X-";
            this.buttonJogXM.UseVisualStyleBackColor = true;
            this.buttonJogXM.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonJogXM_MouseDown);
            this.buttonJogXM.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonJogXM_MouseUp);
            // 
            // buttonJogYP
            // 
            this.buttonJogYP.Location = new System.Drawing.Point(351, 89);
            this.buttonJogYP.Name = "buttonJogYP";
            this.buttonJogYP.Size = new System.Drawing.Size(50, 50);
            this.buttonJogYP.TabIndex = 2;
            this.buttonJogYP.Text = "Y+";
            this.buttonJogYP.UseVisualStyleBackColor = true;
            this.buttonJogYP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonJogYP_MouseDown);
            this.buttonJogYP.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonJogYP_MouseUp);
            // 
            // buttonJogZM
            // 
            this.buttonJogZM.Location = new System.Drawing.Point(558, 207);
            this.buttonJogZM.Name = "buttonJogZM";
            this.buttonJogZM.Size = new System.Drawing.Size(50, 50);
            this.buttonJogZM.TabIndex = 2;
            this.buttonJogZM.Text = "Z-";
            this.buttonJogZM.UseVisualStyleBackColor = true;
            this.buttonJogZM.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonJogZM_MouseDown);
            this.buttonJogZM.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonJogZM_MouseUp);
            // 
            // buttonJogYM
            // 
            this.buttonJogYM.Location = new System.Drawing.Point(351, 221);
            this.buttonJogYM.Name = "buttonJogYM";
            this.buttonJogYM.Size = new System.Drawing.Size(50, 50);
            this.buttonJogYM.TabIndex = 2;
            this.buttonJogYM.Text = "Y-";
            this.buttonJogYM.UseVisualStyleBackColor = true;
            this.buttonJogYM.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonJogYM_MouseDown);
            this.buttonJogYM.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonJogYM_MouseUp);
            // 
            // buttonJogZP
            // 
            this.buttonJogZP.Location = new System.Drawing.Point(558, 97);
            this.buttonJogZP.Name = "buttonJogZP";
            this.buttonJogZP.Size = new System.Drawing.Size(50, 50);
            this.buttonJogZP.TabIndex = 2;
            this.buttonJogZP.Text = "Z+";
            this.buttonJogZP.UseVisualStyleBackColor = true;
            this.buttonJogZP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonJogZP_MouseDown);
            this.buttonJogZP.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonJogZP_MouseUp);
            // 
            // numericUpDownJogXY
            // 
            this.numericUpDownJogXY.DecimalPlaces = 3;
            this.numericUpDownJogXY.Location = new System.Drawing.Point(316, 165);
            this.numericUpDownJogXY.Name = "numericUpDownJogXY";
            this.numericUpDownJogXY.Size = new System.Drawing.Size(120, 31);
            this.numericUpDownJogXY.TabIndex = 3;
            this.numericUpDownJogXY.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numericUpDownJogZ
            // 
            this.numericUpDownJogZ.DecimalPlaces = 3;
            this.numericUpDownJogZ.Location = new System.Drawing.Point(523, 164);
            this.numericUpDownJogZ.Name = "numericUpDownJogZ";
            this.numericUpDownJogZ.Size = new System.Drawing.Size(120, 31);
            this.numericUpDownJogZ.TabIndex = 3;
            this.numericUpDownJogZ.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.numericUpDownJogZ);
            this.Controls.Add(this.numericUpDownJogXY);
            this.Controls.Add(this.buttonJogYM);
            this.Controls.Add(this.buttonJogZP);
            this.Controls.Add(this.buttonJogZM);
            this.Controls.Add(this.buttonJogYP);
            this.Controls.Add(this.buttonJogXM);
            this.Controls.Add(this.buttonJogXP);
            this.Controls.Add(this.textBoxIp);
            this.Controls.Add(this.buttonMove2);
            this.Controls.Add(this.buttonMove1);
            this.Controls.Add(this.buttonHoming);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.buttonConnect);
            this.Name = "Form1";
            this.Text = "RASDK.Arm Test";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownJogXY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownJogZ)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.TextBox textBoxIp;
        private System.Windows.Forms.Button buttonHoming;
        private System.Windows.Forms.Button buttonMove1;
        private System.Windows.Forms.Button buttonMove2;
        private System.Windows.Forms.Button buttonJogXP;
        private System.Windows.Forms.Button buttonJogXM;
        private System.Windows.Forms.Button buttonJogYP;
        private System.Windows.Forms.Button buttonJogZM;
        private System.Windows.Forms.Button buttonJogYM;
        private System.Windows.Forms.Button buttonJogZP;
        private System.Windows.Forms.NumericUpDown numericUpDownJogXY;
        private System.Windows.Forms.NumericUpDown numericUpDownJogZ;
    }
}

