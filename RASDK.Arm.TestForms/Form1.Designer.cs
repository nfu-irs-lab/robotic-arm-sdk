
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
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.comboBoxArmType = new System.Windows.Forms.ComboBox();
            this.buttonCheckConnect = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownJogXY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownJogZ)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(8, 38);
            this.buttonConnect.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(105, 34);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Location = new System.Drawing.Point(8, 75);
            this.buttonDisconnect.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(105, 34);
            this.buttonDisconnect.TabIndex = 0;
            this.buttonDisconnect.Text = "Disconnect";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // textBoxIp
            // 
            this.textBoxIp.Location = new System.Drawing.Point(8, 11);
            this.textBoxIp.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.textBoxIp.Name = "textBoxIp";
            this.textBoxIp.Size = new System.Drawing.Size(135, 25);
            this.textBoxIp.TabIndex = 1;
            this.textBoxIp.Text = "192.168.0.1";
            // 
            // buttonHoming
            // 
            this.buttonHoming.Location = new System.Drawing.Point(8, 151);
            this.buttonHoming.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.buttonHoming.Name = "buttonHoming";
            this.buttonHoming.Size = new System.Drawing.Size(105, 34);
            this.buttonHoming.TabIndex = 0;
            this.buttonHoming.Text = "Homing";
            this.buttonHoming.UseVisualStyleBackColor = true;
            this.buttonHoming.Click += new System.EventHandler(this.buttonHoming_Click);
            // 
            // buttonMove1
            // 
            this.buttonMove1.Location = new System.Drawing.Point(8, 189);
            this.buttonMove1.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.buttonMove1.Name = "buttonMove1";
            this.buttonMove1.Size = new System.Drawing.Size(105, 34);
            this.buttonMove1.TabIndex = 0;
            this.buttonMove1.Text = "Unblock";
            this.buttonMove1.UseVisualStyleBackColor = true;
            this.buttonMove1.Click += new System.EventHandler(this.buttonMove1_Click);
            // 
            // buttonMove2
            // 
            this.buttonMove2.Location = new System.Drawing.Point(8, 225);
            this.buttonMove2.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.buttonMove2.Name = "buttonMove2";
            this.buttonMove2.Size = new System.Drawing.Size(105, 34);
            this.buttonMove2.TabIndex = 0;
            this.buttonMove2.Text = "Blocked";
            this.buttonMove2.UseVisualStyleBackColor = true;
            this.buttonMove2.Click += new System.EventHandler(this.buttonMove2_Click);
            // 
            // buttonJogXP
            // 
            this.buttonJogXP.Location = new System.Drawing.Point(301, 92);
            this.buttonJogXP.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.buttonJogXP.Name = "buttonJogXP";
            this.buttonJogXP.Size = new System.Drawing.Size(33, 30);
            this.buttonJogXP.TabIndex = 2;
            this.buttonJogXP.Text = "X+";
            this.buttonJogXP.UseVisualStyleBackColor = true;
            this.buttonJogXP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonJogXP_MouseDown);
            this.buttonJogXP.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonJogXP_MouseUp);
            // 
            // buttonJogXM
            // 
            this.buttonJogXM.Location = new System.Drawing.Point(165, 92);
            this.buttonJogXM.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.buttonJogXM.Name = "buttonJogXM";
            this.buttonJogXM.Size = new System.Drawing.Size(33, 30);
            this.buttonJogXM.TabIndex = 2;
            this.buttonJogXM.Text = "X-";
            this.buttonJogXM.UseVisualStyleBackColor = true;
            this.buttonJogXM.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonJogXM_MouseDown);
            this.buttonJogXM.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonJogXM_MouseUp);
            // 
            // buttonJogYP
            // 
            this.buttonJogYP.Location = new System.Drawing.Point(235, 54);
            this.buttonJogYP.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.buttonJogYP.Name = "buttonJogYP";
            this.buttonJogYP.Size = new System.Drawing.Size(33, 30);
            this.buttonJogYP.TabIndex = 2;
            this.buttonJogYP.Text = "Y+";
            this.buttonJogYP.UseVisualStyleBackColor = true;
            this.buttonJogYP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonJogYP_MouseDown);
            this.buttonJogYP.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonJogYP_MouseUp);
            // 
            // buttonJogZM
            // 
            this.buttonJogZM.Location = new System.Drawing.Point(372, 124);
            this.buttonJogZM.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.buttonJogZM.Name = "buttonJogZM";
            this.buttonJogZM.Size = new System.Drawing.Size(33, 30);
            this.buttonJogZM.TabIndex = 2;
            this.buttonJogZM.Text = "Z-";
            this.buttonJogZM.UseVisualStyleBackColor = true;
            this.buttonJogZM.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonJogZM_MouseDown);
            this.buttonJogZM.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonJogZM_MouseUp);
            // 
            // buttonJogYM
            // 
            this.buttonJogYM.Location = new System.Drawing.Point(235, 132);
            this.buttonJogYM.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.buttonJogYM.Name = "buttonJogYM";
            this.buttonJogYM.Size = new System.Drawing.Size(33, 30);
            this.buttonJogYM.TabIndex = 2;
            this.buttonJogYM.Text = "Y-";
            this.buttonJogYM.UseVisualStyleBackColor = true;
            this.buttonJogYM.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonJogYM_MouseDown);
            this.buttonJogYM.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonJogYM_MouseUp);
            // 
            // buttonJogZP
            // 
            this.buttonJogZP.Location = new System.Drawing.Point(372, 59);
            this.buttonJogZP.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.buttonJogZP.Name = "buttonJogZP";
            this.buttonJogZP.Size = new System.Drawing.Size(33, 30);
            this.buttonJogZP.TabIndex = 2;
            this.buttonJogZP.Text = "Z+";
            this.buttonJogZP.UseVisualStyleBackColor = true;
            this.buttonJogZP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonJogZP_MouseDown);
            this.buttonJogZP.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonJogZP_MouseUp);
            // 
            // numericUpDownJogXY
            // 
            this.numericUpDownJogXY.DecimalPlaces = 3;
            this.numericUpDownJogXY.Location = new System.Drawing.Point(211, 99);
            this.numericUpDownJogXY.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.numericUpDownJogXY.Name = "numericUpDownJogXY";
            this.numericUpDownJogXY.Size = new System.Drawing.Size(80, 25);
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
            this.numericUpDownJogZ.Location = new System.Drawing.Point(349, 99);
            this.numericUpDownJogZ.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.numericUpDownJogZ.Name = "numericUpDownJogZ";
            this.numericUpDownJogZ.Size = new System.Drawing.Size(80, 25);
            this.numericUpDownJogZ.TabIndex = 3;
            this.numericUpDownJogZ.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(152, 11);
            this.textBoxPort.Margin = new System.Windows.Forms.Padding(1);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(52, 25);
            this.textBoxPort.TabIndex = 4;
            this.textBoxPort.Text = "5890";
            // 
            // comboBoxArmType
            // 
            this.comboBoxArmType.FormattingEnabled = true;
            this.comboBoxArmType.Items.AddRange(new object[] {
            "HIWIN",
            "TM Robot",
            "CoppeliaSim"});
            this.comboBoxArmType.Location = new System.Drawing.Point(221, 11);
            this.comboBoxArmType.Margin = new System.Windows.Forms.Padding(1);
            this.comboBoxArmType.Name = "comboBoxArmType";
            this.comboBoxArmType.Size = new System.Drawing.Size(95, 23);
            this.comboBoxArmType.TabIndex = 5;
            // 
            // buttonCheckConnect
            // 
            this.buttonCheckConnect.Location = new System.Drawing.Point(8, 113);
            this.buttonCheckConnect.Name = "buttonCheckConnect";
            this.buttonCheckConnect.Size = new System.Drawing.Size(105, 34);
            this.buttonCheckConnect.TabIndex = 6;
            this.buttonCheckConnect.Text = "Check";
            this.buttonCheckConnect.UseVisualStyleBackColor = true;
            this.buttonCheckConnect.Click += new System.EventHandler(this.buttonCheckConnect_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 283);
            this.Controls.Add(this.buttonCheckConnect);
            this.Controls.Add(this.comboBoxArmType);
            this.Controls.Add(this.textBoxPort);
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
            this.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
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
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.ComboBox comboBoxArmType;
        private System.Windows.Forms.Button buttonCheckConnect;
    }
}

