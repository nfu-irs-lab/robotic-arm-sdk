
namespace UI
{
    partial class BasicArmController
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxArmNowPositionXJ1 = new System.Windows.Forms.TextBox();
            this.textBoxArmNowPositionYJ2 = new System.Windows.Forms.TextBox();
            this.textBoxArmNowPositionZJ3 = new System.Windows.Forms.TextBox();
            this.textBoxArmNowPositionAJ4 = new System.Windows.Forms.TextBox();
            this.textBoxArmNowPositionBJ5 = new System.Windows.Forms.TextBox();
            this.textBoxArmNowPositionCJ6 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDownArmTargetPositionXJ1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownArmTargetPositionYJ2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownArmTargetPositionZJ3 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownArmTargetPositionAJ4 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownArmTargetPositionBJ5 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownArmTargetPositionZJ6 = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonArmCopyPositionFromNowToTarget = new System.Windows.Forms.Button();
            this.buttonArmUpdateNowPosition = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButtonPositionTypeRelative = new System.Windows.Forms.RadioButton();
            this.radioButtonPositionTypeAbsolute = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radioButtonCoordinateTypeJoint = new System.Windows.Forms.RadioButton();
            this.radioButtonCoordinateTypeDescartes = new System.Windows.Forms.RadioButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.radioButtonMotionTypeLinear = new System.Windows.Forms.RadioButton();
            this.radioButtonMotionTypePointToPoint = new System.Windows.Forms.RadioButton();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonArmHoming = new System.Windows.Forms.Button();
            this.checkBoxArmSlowlyHoming = new System.Windows.Forms.CheckBox();
            this.buttonArmMotionStart = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.numericUpDownArmSpeed = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownArmAcceleration = new System.Windows.Forms.NumericUpDown();
            this.buttonSetSpeedAndAcceleration = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArmTargetPositionXJ1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArmTargetPositionYJ2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArmTargetPositionZJ3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArmTargetPositionAJ4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArmTargetPositionBJ5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArmTargetPositionZJ6)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArmSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArmAcceleration)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1936, 625);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "位置";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 7;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.Controls.Add(this.textBoxArmNowPositionXJ1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBoxArmNowPositionYJ2, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBoxArmNowPositionZJ3, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBoxArmNowPositionAJ4, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBoxArmNowPositionBJ5, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBoxArmNowPositionCJ6, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label2, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.label4, 4, 1);
            this.tableLayoutPanel2.Controls.Add(this.label5, 5, 1);
            this.tableLayoutPanel2.Controls.Add(this.label6, 6, 1);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.numericUpDownArmTargetPositionXJ1, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.numericUpDownArmTargetPositionYJ2, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.numericUpDownArmTargetPositionZJ3, 3, 2);
            this.tableLayoutPanel2.Controls.Add(this.numericUpDownArmTargetPositionAJ4, 4, 2);
            this.tableLayoutPanel2.Controls.Add(this.numericUpDownArmTargetPositionBJ5, 5, 2);
            this.tableLayoutPanel2.Controls.Add(this.numericUpDownArmTargetPositionZJ6, 6, 2);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 27);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.5F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1930, 595);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // textBoxArmNowPositionXJ1
            // 
            this.textBoxArmNowPositionXJ1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxArmNowPositionXJ1.Location = new System.Drawing.Point(196, 96);
            this.textBoxArmNowPositionXJ1.Name = "textBoxArmNowPositionXJ1";
            this.textBoxArmNowPositionXJ1.ReadOnly = true;
            this.textBoxArmNowPositionXJ1.Size = new System.Drawing.Size(283, 31);
            this.textBoxArmNowPositionXJ1.TabIndex = 0;
            this.textBoxArmNowPositionXJ1.Text = "--";
            // 
            // textBoxArmNowPositionYJ2
            // 
            this.textBoxArmNowPositionYJ2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxArmNowPositionYJ2.Location = new System.Drawing.Point(485, 96);
            this.textBoxArmNowPositionYJ2.Name = "textBoxArmNowPositionYJ2";
            this.textBoxArmNowPositionYJ2.ReadOnly = true;
            this.textBoxArmNowPositionYJ2.Size = new System.Drawing.Size(283, 31);
            this.textBoxArmNowPositionYJ2.TabIndex = 1;
            this.textBoxArmNowPositionYJ2.Text = "--";
            // 
            // textBoxArmNowPositionZJ3
            // 
            this.textBoxArmNowPositionZJ3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxArmNowPositionZJ3.Location = new System.Drawing.Point(774, 96);
            this.textBoxArmNowPositionZJ3.Name = "textBoxArmNowPositionZJ3";
            this.textBoxArmNowPositionZJ3.ReadOnly = true;
            this.textBoxArmNowPositionZJ3.Size = new System.Drawing.Size(283, 31);
            this.textBoxArmNowPositionZJ3.TabIndex = 1;
            this.textBoxArmNowPositionZJ3.Text = "--";
            // 
            // textBoxArmNowPositionAJ4
            // 
            this.textBoxArmNowPositionAJ4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxArmNowPositionAJ4.Location = new System.Drawing.Point(1063, 96);
            this.textBoxArmNowPositionAJ4.Name = "textBoxArmNowPositionAJ4";
            this.textBoxArmNowPositionAJ4.ReadOnly = true;
            this.textBoxArmNowPositionAJ4.Size = new System.Drawing.Size(283, 31);
            this.textBoxArmNowPositionAJ4.TabIndex = 1;
            this.textBoxArmNowPositionAJ4.Text = "--";
            // 
            // textBoxArmNowPositionBJ5
            // 
            this.textBoxArmNowPositionBJ5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxArmNowPositionBJ5.Location = new System.Drawing.Point(1352, 96);
            this.textBoxArmNowPositionBJ5.Name = "textBoxArmNowPositionBJ5";
            this.textBoxArmNowPositionBJ5.ReadOnly = true;
            this.textBoxArmNowPositionBJ5.Size = new System.Drawing.Size(283, 31);
            this.textBoxArmNowPositionBJ5.TabIndex = 1;
            this.textBoxArmNowPositionBJ5.Text = "--";
            // 
            // textBoxArmNowPositionCJ6
            // 
            this.textBoxArmNowPositionCJ6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxArmNowPositionCJ6.Location = new System.Drawing.Point(1641, 96);
            this.textBoxArmNowPositionCJ6.Name = "textBoxArmNowPositionCJ6";
            this.textBoxArmNowPositionCJ6.ReadOnly = true;
            this.textBoxArmNowPositionCJ6.Size = new System.Drawing.Size(286, 31);
            this.textBoxArmNowPositionCJ6.TabIndex = 1;
            this.textBoxArmNowPositionCJ6.Text = "--";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(196, 284);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(283, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "X/J1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(485, 284);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(283, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Y/J2";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(774, 284);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(283, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Z/J3";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1063, 284);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(283, 25);
            this.label4.TabIndex = 2;
            this.label4.Text = "A/J4";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1352, 284);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(283, 25);
            this.label5.TabIndex = 2;
            this.label5.Text = "B/J5";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1641, 284);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(286, 25);
            this.label6.TabIndex = 2;
            this.label6.Text = "C/J6";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 99);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(187, 25);
            this.label7.TabIndex = 3;
            this.label7.Text = "目前：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 470);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(187, 25);
            this.label8.TabIndex = 3;
            this.label8.Text = "目標：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownArmTargetPositionXJ1
            // 
            this.numericUpDownArmTargetPositionXJ1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownArmTargetPositionXJ1.DecimalPlaces = 3;
            this.numericUpDownArmTargetPositionXJ1.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownArmTargetPositionXJ1.Location = new System.Drawing.Point(196, 467);
            this.numericUpDownArmTargetPositionXJ1.Maximum = new decimal(new int[] {
            800,
            0,
            0,
            0});
            this.numericUpDownArmTargetPositionXJ1.Minimum = new decimal(new int[] {
            800,
            0,
            0,
            -2147483648});
            this.numericUpDownArmTargetPositionXJ1.Name = "numericUpDownArmTargetPositionXJ1";
            this.numericUpDownArmTargetPositionXJ1.Size = new System.Drawing.Size(283, 31);
            this.numericUpDownArmTargetPositionXJ1.TabIndex = 5;
            // 
            // numericUpDownArmTargetPositionYJ2
            // 
            this.numericUpDownArmTargetPositionYJ2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownArmTargetPositionYJ2.DecimalPlaces = 3;
            this.numericUpDownArmTargetPositionYJ2.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownArmTargetPositionYJ2.Location = new System.Drawing.Point(485, 467);
            this.numericUpDownArmTargetPositionYJ2.Maximum = new decimal(new int[] {
            800,
            0,
            0,
            0});
            this.numericUpDownArmTargetPositionYJ2.Minimum = new decimal(new int[] {
            800,
            0,
            0,
            -2147483648});
            this.numericUpDownArmTargetPositionYJ2.Name = "numericUpDownArmTargetPositionYJ2";
            this.numericUpDownArmTargetPositionYJ2.Size = new System.Drawing.Size(283, 31);
            this.numericUpDownArmTargetPositionYJ2.TabIndex = 5;
            // 
            // numericUpDownArmTargetPositionZJ3
            // 
            this.numericUpDownArmTargetPositionZJ3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownArmTargetPositionZJ3.DecimalPlaces = 3;
            this.numericUpDownArmTargetPositionZJ3.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownArmTargetPositionZJ3.Location = new System.Drawing.Point(774, 467);
            this.numericUpDownArmTargetPositionZJ3.Maximum = new decimal(new int[] {
            800,
            0,
            0,
            0});
            this.numericUpDownArmTargetPositionZJ3.Minimum = new decimal(new int[] {
            800,
            0,
            0,
            -2147483648});
            this.numericUpDownArmTargetPositionZJ3.Name = "numericUpDownArmTargetPositionZJ3";
            this.numericUpDownArmTargetPositionZJ3.Size = new System.Drawing.Size(283, 31);
            this.numericUpDownArmTargetPositionZJ3.TabIndex = 5;
            // 
            // numericUpDownArmTargetPositionAJ4
            // 
            this.numericUpDownArmTargetPositionAJ4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownArmTargetPositionAJ4.DecimalPlaces = 3;
            this.numericUpDownArmTargetPositionAJ4.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownArmTargetPositionAJ4.Location = new System.Drawing.Point(1063, 467);
            this.numericUpDownArmTargetPositionAJ4.Maximum = new decimal(new int[] {
            800,
            0,
            0,
            0});
            this.numericUpDownArmTargetPositionAJ4.Minimum = new decimal(new int[] {
            800,
            0,
            0,
            -2147483648});
            this.numericUpDownArmTargetPositionAJ4.Name = "numericUpDownArmTargetPositionAJ4";
            this.numericUpDownArmTargetPositionAJ4.Size = new System.Drawing.Size(283, 31);
            this.numericUpDownArmTargetPositionAJ4.TabIndex = 5;
            // 
            // numericUpDownArmTargetPositionBJ5
            // 
            this.numericUpDownArmTargetPositionBJ5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownArmTargetPositionBJ5.DecimalPlaces = 3;
            this.numericUpDownArmTargetPositionBJ5.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownArmTargetPositionBJ5.Location = new System.Drawing.Point(1352, 467);
            this.numericUpDownArmTargetPositionBJ5.Maximum = new decimal(new int[] {
            800,
            0,
            0,
            0});
            this.numericUpDownArmTargetPositionBJ5.Minimum = new decimal(new int[] {
            800,
            0,
            0,
            -2147483648});
            this.numericUpDownArmTargetPositionBJ5.Name = "numericUpDownArmTargetPositionBJ5";
            this.numericUpDownArmTargetPositionBJ5.Size = new System.Drawing.Size(283, 31);
            this.numericUpDownArmTargetPositionBJ5.TabIndex = 5;
            // 
            // numericUpDownArmTargetPositionZJ6
            // 
            this.numericUpDownArmTargetPositionZJ6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownArmTargetPositionZJ6.DecimalPlaces = 3;
            this.numericUpDownArmTargetPositionZJ6.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownArmTargetPositionZJ6.Location = new System.Drawing.Point(1641, 467);
            this.numericUpDownArmTargetPositionZJ6.Maximum = new decimal(new int[] {
            800,
            0,
            0,
            0});
            this.numericUpDownArmTargetPositionZJ6.Minimum = new decimal(new int[] {
            800,
            0,
            0,
            -2147483648});
            this.numericUpDownArmTargetPositionZJ6.Name = "numericUpDownArmTargetPositionZJ6";
            this.numericUpDownArmTargetPositionZJ6.Size = new System.Drawing.Size(286, 31);
            this.numericUpDownArmTargetPositionZJ6.TabIndex = 5;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.buttonArmCopyPositionFromNowToTarget, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.buttonArmUpdateNowPosition, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 226);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(187, 142);
            this.tableLayoutPanel3.TabIndex = 6;
            // 
            // buttonArmCopyPositionFromNowToTarget
            // 
            this.buttonArmCopyPositionFromNowToTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonArmCopyPositionFromNowToTarget.Location = new System.Drawing.Point(3, 74);
            this.buttonArmCopyPositionFromNowToTarget.Name = "buttonArmCopyPositionFromNowToTarget";
            this.buttonArmCopyPositionFromNowToTarget.Size = new System.Drawing.Size(181, 65);
            this.buttonArmCopyPositionFromNowToTarget.TabIndex = 4;
            this.buttonArmCopyPositionFromNowToTarget.Text = "複製";
            this.buttonArmCopyPositionFromNowToTarget.UseVisualStyleBackColor = true;
            this.buttonArmCopyPositionFromNowToTarget.Click += new System.EventHandler(this.buttonArmCopyPositionFromNowToTarget_Click);
            // 
            // buttonArmUpdateNowPosition
            // 
            this.buttonArmUpdateNowPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonArmUpdateNowPosition.Location = new System.Drawing.Point(3, 3);
            this.buttonArmUpdateNowPosition.Name = "buttonArmUpdateNowPosition";
            this.buttonArmUpdateNowPosition.Size = new System.Drawing.Size(181, 65);
            this.buttonArmUpdateNowPosition.TabIndex = 5;
            this.buttonArmUpdateNowPosition.Text = "更新";
            this.buttonArmUpdateNowPosition.UseVisualStyleBackColor = true;
            this.buttonArmUpdateNowPosition.Click += new System.EventHandler(this.buttonArmUpdateNowPosition_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel4);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 634);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1936, 414);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "動作";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 5;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.Controls.Add(this.groupBox4, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.groupBox5, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.groupBox6, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.groupBox7, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.buttonArmMotionStart, 3, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 27);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1930, 384);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioButtonPositionTypeRelative);
            this.groupBox4.Controls.Add(this.radioButtonPositionTypeAbsolute);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(380, 378);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "位置類型";
            // 
            // radioButtonPositionTypeRelative
            // 
            this.radioButtonPositionTypeRelative.AutoSize = true;
            this.radioButtonPositionTypeRelative.Location = new System.Drawing.Point(16, 82);
            this.radioButtonPositionTypeRelative.Name = "radioButtonPositionTypeRelative";
            this.radioButtonPositionTypeRelative.Size = new System.Drawing.Size(127, 29);
            this.radioButtonPositionTypeRelative.TabIndex = 1;
            this.radioButtonPositionTypeRelative.Text = "相對位置";
            this.radioButtonPositionTypeRelative.UseVisualStyleBackColor = true;
            // 
            // radioButtonPositionTypeAbsolute
            // 
            this.radioButtonPositionTypeAbsolute.AutoSize = true;
            this.radioButtonPositionTypeAbsolute.Checked = true;
            this.radioButtonPositionTypeAbsolute.Location = new System.Drawing.Point(16, 47);
            this.radioButtonPositionTypeAbsolute.Name = "radioButtonPositionTypeAbsolute";
            this.radioButtonPositionTypeAbsolute.Size = new System.Drawing.Size(122, 29);
            this.radioButtonPositionTypeAbsolute.TabIndex = 0;
            this.radioButtonPositionTypeAbsolute.TabStop = true;
            this.radioButtonPositionTypeAbsolute.Text = "絕對位置";
            this.radioButtonPositionTypeAbsolute.UseVisualStyleBackColor = true;
            this.radioButtonPositionTypeAbsolute.CheckedChanged += new System.EventHandler(this.radioButtonPositionTypeAbsolute_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.radioButtonCoordinateTypeJoint);
            this.groupBox5.Controls.Add(this.radioButtonCoordinateTypeDescartes);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(389, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(380, 378);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "座標類型";
            // 
            // radioButtonCoordinateTypeJoint
            // 
            this.radioButtonCoordinateTypeJoint.AutoSize = true;
            this.radioButtonCoordinateTypeJoint.Location = new System.Drawing.Point(7, 83);
            this.radioButtonCoordinateTypeJoint.Name = "radioButtonCoordinateTypeJoint";
            this.radioButtonCoordinateTypeJoint.Size = new System.Drawing.Size(127, 29);
            this.radioButtonCoordinateTypeJoint.TabIndex = 1;
            this.radioButtonCoordinateTypeJoint.Text = "關節座標";
            this.radioButtonCoordinateTypeJoint.UseVisualStyleBackColor = true;
            // 
            // radioButtonCoordinateTypeDescartes
            // 
            this.radioButtonCoordinateTypeDescartes.AutoSize = true;
            this.radioButtonCoordinateTypeDescartes.Checked = true;
            this.radioButtonCoordinateTypeDescartes.Location = new System.Drawing.Point(7, 47);
            this.radioButtonCoordinateTypeDescartes.Name = "radioButtonCoordinateTypeDescartes";
            this.radioButtonCoordinateTypeDescartes.Size = new System.Drawing.Size(148, 29);
            this.radioButtonCoordinateTypeDescartes.TabIndex = 0;
            this.radioButtonCoordinateTypeDescartes.TabStop = true;
            this.radioButtonCoordinateTypeDescartes.Text = "笛卡爾座標";
            this.radioButtonCoordinateTypeDescartes.UseVisualStyleBackColor = true;
            this.radioButtonCoordinateTypeDescartes.CheckedChanged += new System.EventHandler(this.radioButtonCoordinateTypeDescartes_CheckedChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.radioButtonMotionTypeLinear);
            this.groupBox6.Controls.Add(this.radioButtonMotionTypePointToPoint);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(775, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(380, 378);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "運動類型";
            // 
            // radioButtonMotionTypeLinear
            // 
            this.radioButtonMotionTypeLinear.AutoSize = true;
            this.radioButtonMotionTypeLinear.Location = new System.Drawing.Point(7, 83);
            this.radioButtonMotionTypeLinear.Name = "radioButtonMotionTypeLinear";
            this.radioButtonMotionTypeLinear.Size = new System.Drawing.Size(127, 29);
            this.radioButtonMotionTypeLinear.TabIndex = 1;
            this.radioButtonMotionTypeLinear.Text = "線性運動";
            this.radioButtonMotionTypeLinear.UseVisualStyleBackColor = true;
            // 
            // radioButtonMotionTypePointToPoint
            // 
            this.radioButtonMotionTypePointToPoint.AutoSize = true;
            this.radioButtonMotionTypePointToPoint.Checked = true;
            this.radioButtonMotionTypePointToPoint.Location = new System.Drawing.Point(7, 46);
            this.radioButtonMotionTypePointToPoint.Name = "radioButtonMotionTypePointToPoint";
            this.radioButtonMotionTypePointToPoint.Size = new System.Drawing.Size(148, 29);
            this.radioButtonMotionTypePointToPoint.TabIndex = 0;
            this.radioButtonMotionTypePointToPoint.TabStop = true;
            this.radioButtonMotionTypePointToPoint.Text = "點到點運動";
            this.radioButtonMotionTypePointToPoint.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.tableLayoutPanel6);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.Location = new System.Drawing.Point(1547, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(380, 378);
            this.groupBox7.TabIndex = 3;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "原點";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Controls.Add(this.buttonArmHoming, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.checkBoxArmSlowlyHoming, 0, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 27);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(374, 348);
            this.tableLayoutPanel6.TabIndex = 2;
            // 
            // buttonArmHoming
            // 
            this.buttonArmHoming.Location = new System.Drawing.Point(3, 3);
            this.buttonArmHoming.Name = "buttonArmHoming";
            this.buttonArmHoming.Size = new System.Drawing.Size(167, 69);
            this.buttonArmHoming.TabIndex = 1;
            this.buttonArmHoming.Text = "回到原點";
            this.buttonArmHoming.UseVisualStyleBackColor = true;
            this.buttonArmHoming.Click += new System.EventHandler(this.buttonArmHoming_Click);
            // 
            // checkBoxArmSlowlyHoming
            // 
            this.checkBoxArmSlowlyHoming.AutoSize = true;
            this.checkBoxArmSlowlyHoming.Checked = true;
            this.checkBoxArmSlowlyHoming.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxArmSlowlyHoming.Location = new System.Drawing.Point(3, 177);
            this.checkBoxArmSlowlyHoming.Name = "checkBoxArmSlowlyHoming";
            this.checkBoxArmSlowlyHoming.Size = new System.Drawing.Size(86, 29);
            this.checkBoxArmSlowlyHoming.TabIndex = 0;
            this.checkBoxArmSlowlyHoming.Text = "慢速";
            this.checkBoxArmSlowlyHoming.UseVisualStyleBackColor = true;
            // 
            // buttonArmMotionStart
            // 
            this.buttonArmMotionStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonArmMotionStart.Location = new System.Drawing.Point(1161, 148);
            this.buttonArmMotionStart.Name = "buttonArmMotionStart";
            this.buttonArmMotionStart.Size = new System.Drawing.Size(380, 88);
            this.buttonArmMotionStart.TabIndex = 4;
            this.buttonArmMotionStart.Text = "進行動作";
            this.buttonArmMotionStart.UseVisualStyleBackColor = true;
            this.buttonArmMotionStart.Click += new System.EventHandler(this.buttonArmMotionStart_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel5);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 1054);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1936, 346);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "速度與加速度";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 6;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.Controls.Add(this.label9, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.label10, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.numericUpDownArmSpeed, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.numericUpDownArmAcceleration, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.buttonSetSpeedAndAcceleration, 5, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 27);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 316F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1930, 316);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 145);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(374, 25);
            this.label9.TabIndex = 3;
            this.label9.Text = "速度：";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(763, 145);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(374, 25);
            this.label10.TabIndex = 3;
            this.label10.Text = "加速度：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownArmSpeed
            // 
            this.numericUpDownArmSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownArmSpeed.Location = new System.Drawing.Point(383, 142);
            this.numericUpDownArmSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownArmSpeed.Name = "numericUpDownArmSpeed";
            this.numericUpDownArmSpeed.Size = new System.Drawing.Size(374, 31);
            this.numericUpDownArmSpeed.TabIndex = 4;
            this.numericUpDownArmSpeed.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numericUpDownArmAcceleration
            // 
            this.numericUpDownArmAcceleration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownArmAcceleration.Location = new System.Drawing.Point(1143, 142);
            this.numericUpDownArmAcceleration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownArmAcceleration.Name = "numericUpDownArmAcceleration";
            this.numericUpDownArmAcceleration.Size = new System.Drawing.Size(374, 31);
            this.numericUpDownArmAcceleration.TabIndex = 5;
            this.numericUpDownArmAcceleration.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // buttonSetSpeedAndAcceleration
            // 
            this.buttonSetSpeedAndAcceleration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetSpeedAndAcceleration.Location = new System.Drawing.Point(1553, 117);
            this.buttonSetSpeedAndAcceleration.Name = "buttonSetSpeedAndAcceleration";
            this.buttonSetSpeedAndAcceleration.Size = new System.Drawing.Size(374, 82);
            this.buttonSetSpeedAndAcceleration.TabIndex = 6;
            this.buttonSetSpeedAndAcceleration.Text = "設定";
            this.buttonSetSpeedAndAcceleration.UseVisualStyleBackColor = true;
            this.buttonSetSpeedAndAcceleration.Click += new System.EventHandler(this.buttonSetSpeedAndAcceleration_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1942, 1403);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // BasicArmController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "BasicArmController";
            this.Size = new System.Drawing.Size(1942, 1403);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArmTargetPositionXJ1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArmTargetPositionYJ2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArmTargetPositionZJ3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArmTargetPositionAJ4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArmTargetPositionBJ5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArmTargetPositionZJ6)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArmSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArmAcceleration)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox textBoxArmNowPositionXJ1;
        private System.Windows.Forms.TextBox textBoxArmNowPositionYJ2;
        private System.Windows.Forms.TextBox textBoxArmNowPositionZJ3;
        private System.Windows.Forms.TextBox textBoxArmNowPositionAJ4;
        private System.Windows.Forms.TextBox textBoxArmNowPositionBJ5;
        private System.Windows.Forms.TextBox textBoxArmNowPositionCJ6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonArmCopyPositionFromNowToTarget;
        private System.Windows.Forms.NumericUpDown numericUpDownArmTargetPositionXJ1;
        private System.Windows.Forms.NumericUpDown numericUpDownArmTargetPositionYJ2;
        private System.Windows.Forms.NumericUpDown numericUpDownArmTargetPositionZJ3;
        private System.Windows.Forms.NumericUpDown numericUpDownArmTargetPositionAJ4;
        private System.Windows.Forms.NumericUpDown numericUpDownArmTargetPositionBJ5;
        private System.Windows.Forms.NumericUpDown numericUpDownArmTargetPositionZJ6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button buttonArmUpdateNowPosition;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.RadioButton radioButtonPositionTypeRelative;
        private System.Windows.Forms.RadioButton radioButtonPositionTypeAbsolute;
        private System.Windows.Forms.RadioButton radioButtonCoordinateTypeJoint;
        private System.Windows.Forms.RadioButton radioButtonCoordinateTypeDescartes;
        private System.Windows.Forms.RadioButton radioButtonMotionTypePointToPoint;
        private System.Windows.Forms.RadioButton radioButtonMotionTypeLinear;
        private System.Windows.Forms.Button buttonArmMotionStart;
        private System.Windows.Forms.Button buttonArmHoming;
        private System.Windows.Forms.CheckBox checkBoxArmSlowlyHoming;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numericUpDownArmSpeed;
        private System.Windows.Forms.NumericUpDown numericUpDownArmAcceleration;
        private System.Windows.Forms.Button buttonSetSpeedAndAcceleration;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
    }
}
