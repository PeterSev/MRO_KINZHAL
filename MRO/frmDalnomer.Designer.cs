namespace MRO
{
    partial class frmDalnomer
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
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.txtDalnostMeasured = new System.Windows.Forms.TextBox();
            this.btnA5_6 = new System.Windows.Forms.Button();
            this.btnA5_13 = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.numA5_21 = new System.Windows.Forms.NumericUpDown();
            this.trackA5_21 = new System.Windows.Forms.TrackBar();
            this.btnA5_21 = new System.Windows.Forms.Button();
            this.btnA5_27 = new System.Windows.Forms.Button();
            this.btnA5_40 = new System.Windows.Forms.Button();
            this.btnA5_28 = new System.Windows.Forms.Button();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.txtCelZaStrob = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.txtCelVStrob = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.txtStrob = new System.Windows.Forms.TextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.txtDalnost = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.groupBox19.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numA5_21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackA5_21)).BeginInit();
            this.groupBox16.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox19
            // 
            this.groupBox19.Controls.Add(this.txtDalnostMeasured);
            this.groupBox19.Controls.Add(this.btnA5_6);
            this.groupBox19.Controls.Add(this.btnA5_13);
            this.groupBox19.Controls.Add(this.groupBox7);
            this.groupBox19.Controls.Add(this.btnA5_27);
            this.groupBox19.Controls.Add(this.btnA5_40);
            this.groupBox19.Controls.Add(this.btnA5_28);
            this.groupBox19.Location = new System.Drawing.Point(12, 12);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(579, 150);
            this.groupBox19.TabIndex = 5;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "Дальномер";
            // 
            // txtDalnostMeasured
            // 
            this.txtDalnostMeasured.Location = new System.Drawing.Point(170, 115);
            this.txtDalnostMeasured.Name = "txtDalnostMeasured";
            this.txtDalnostMeasured.ReadOnly = true;
            this.txtDalnostMeasured.Size = new System.Drawing.Size(120, 20);
            this.txtDalnostMeasured.TabIndex = 28;
            this.txtDalnostMeasured.Text = "не определено";
            this.txtDalnostMeasured.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnA5_6
            // 
            this.btnA5_6.Location = new System.Drawing.Point(291, 26);
            this.btnA5_6.Name = "btnA5_6";
            this.btnA5_6.Size = new System.Drawing.Size(279, 23);
            this.btnA5_6.TabIndex = 2;
            this.btnA5_6.Text = "Выбрать дальность";
            this.btnA5_6.UseVisualStyleBackColor = true;
            // 
            // btnA5_13
            // 
            this.btnA5_13.Location = new System.Drawing.Point(291, 55);
            this.btnA5_13.Name = "btnA5_13";
            this.btnA5_13.Size = new System.Drawing.Size(279, 23);
            this.btnA5_13.TabIndex = 2;
            this.btnA5_13.Text = "Ручной ввод дальности";
            this.btnA5_13.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.numA5_21);
            this.groupBox7.Controls.Add(this.trackA5_21);
            this.groupBox7.Controls.Add(this.btnA5_21);
            this.groupBox7.Location = new System.Drawing.Point(6, 20);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(273, 86);
            this.groupBox7.TabIndex = 7;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Установить строб";
            // 
            // numA5_21
            // 
            this.numA5_21.Location = new System.Drawing.Point(208, 49);
            this.numA5_21.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numA5_21.Name = "numA5_21";
            this.numA5_21.Size = new System.Drawing.Size(54, 20);
            this.numA5_21.TabIndex = 6;
            this.numA5_21.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numA5_21.ValueChanged += new System.EventHandler(this.numA5_21_ValueChanged);
            // 
            // trackA5_21
            // 
            this.trackA5_21.AutoSize = false;
            this.trackA5_21.Location = new System.Drawing.Point(6, 19);
            this.trackA5_21.Maximum = 10000;
            this.trackA5_21.Name = "trackA5_21";
            this.trackA5_21.Size = new System.Drawing.Size(256, 24);
            this.trackA5_21.TabIndex = 5;
            this.trackA5_21.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackA5_21.Scroll += new System.EventHandler(this.trackA5_21_Scroll);
            // 
            // btnA5_21
            // 
            this.btnA5_21.Location = new System.Drawing.Point(9, 49);
            this.btnA5_21.Name = "btnA5_21";
            this.btnA5_21.Size = new System.Drawing.Size(85, 23);
            this.btnA5_21.TabIndex = 3;
            this.btnA5_21.Text = "Выполнить";
            this.btnA5_21.UseVisualStyleBackColor = true;
            // 
            // btnA5_27
            // 
            this.btnA5_27.Location = new System.Drawing.Point(6, 113);
            this.btnA5_27.Name = "btnA5_27";
            this.btnA5_27.Size = new System.Drawing.Size(158, 23);
            this.btnA5_27.TabIndex = 14;
            this.btnA5_27.Text = "Измерить дальность";
            this.btnA5_27.UseVisualStyleBackColor = true;
            // 
            // btnA5_40
            // 
            this.btnA5_40.Location = new System.Drawing.Point(422, 113);
            this.btnA5_40.Name = "btnA5_40";
            this.btnA5_40.Size = new System.Drawing.Size(151, 23);
            this.btnA5_40.TabIndex = 27;
            this.btnA5_40.Text = "Подсветка лазером";
            this.btnA5_40.UseVisualStyleBackColor = true;
            // 
            // btnA5_28
            // 
            this.btnA5_28.Location = new System.Drawing.Point(299, 113);
            this.btnA5_28.Name = "btnA5_28";
            this.btnA5_28.Size = new System.Drawing.Size(117, 23);
            this.btnA5_28.TabIndex = 13;
            this.btnA5_28.Text = "Ввод Д, У";
            this.btnA5_28.UseVisualStyleBackColor = true;
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.txtCelZaStrob);
            this.groupBox16.Controls.Add(this.label42);
            this.groupBox16.Controls.Add(this.txtCelVStrob);
            this.groupBox16.Controls.Add(this.label43);
            this.groupBox16.Controls.Add(this.txtStrob);
            this.groupBox16.Controls.Add(this.label41);
            this.groupBox16.Controls.Add(this.txtDalnost);
            this.groupBox16.Controls.Add(this.label40);
            this.groupBox16.Location = new System.Drawing.Point(12, 168);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(579, 83);
            this.groupBox16.TabIndex = 6;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "Текущая дальность и строб";
            // 
            // txtCelZaStrob
            // 
            this.txtCelZaStrob.Location = new System.Drawing.Point(447, 45);
            this.txtCelZaStrob.Name = "txtCelZaStrob";
            this.txtCelZaStrob.ReadOnly = true;
            this.txtCelZaStrob.Size = new System.Drawing.Size(120, 20);
            this.txtCelZaStrob.TabIndex = 12;
            this.txtCelZaStrob.Text = "не определено";
            this.txtCelZaStrob.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(296, 48);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(144, 13);
            this.label42.TabIndex = 10;
            this.label42.Text = "Наличие целей за стробом";
            // 
            // txtCelVStrob
            // 
            this.txtCelVStrob.Location = new System.Drawing.Point(447, 19);
            this.txtCelVStrob.Name = "txtCelVStrob";
            this.txtCelVStrob.ReadOnly = true;
            this.txtCelVStrob.Size = new System.Drawing.Size(120, 20);
            this.txtCelVStrob.TabIndex = 13;
            this.txtCelVStrob.Text = "не определено";
            this.txtCelVStrob.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(296, 22);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(130, 13);
            this.label43.TabIndex = 11;
            this.label43.Text = "Наличие целей в стробе";
            // 
            // txtStrob
            // 
            this.txtStrob.Location = new System.Drawing.Point(170, 45);
            this.txtStrob.Name = "txtStrob";
            this.txtStrob.ReadOnly = true;
            this.txtStrob.Size = new System.Drawing.Size(120, 20);
            this.txtStrob.TabIndex = 9;
            this.txtStrob.Text = "не определено";
            this.txtStrob.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(6, 51);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(37, 13);
            this.label41.TabIndex = 8;
            this.label41.Text = "Строб";
            // 
            // txtDalnost
            // 
            this.txtDalnost.Location = new System.Drawing.Point(170, 19);
            this.txtDalnost.Name = "txtDalnost";
            this.txtDalnost.ReadOnly = true;
            this.txtDalnost.Size = new System.Drawing.Size(120, 20);
            this.txtDalnost.TabIndex = 9;
            this.txtDalnost.Text = "не определено";
            this.txtDalnost.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(6, 25);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(63, 13);
            this.label40.TabIndex = 8;
            this.label40.Text = "Дальность";
            // 
            // frmDalnomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 261);
            this.Controls.Add(this.groupBox16);
            this.Controls.Add(this.groupBox19);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmDalnomer";
            this.Text = "Дальномер";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDalnomer_FormClosing);
            this.Load += new System.EventHandler(this.frmDalnomer_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmDalnomer_KeyDown);
            this.groupBox19.ResumeLayout(false);
            this.groupBox19.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numA5_21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackA5_21)).EndInit();
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox19;
        private System.Windows.Forms.Button btnA5_6;
        private System.Windows.Forms.Button btnA5_13;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnA5_21;
        private System.Windows.Forms.Button btnA5_27;
        private System.Windows.Forms.Button btnA5_40;
        private System.Windows.Forms.Button btnA5_28;
        public System.Windows.Forms.NumericUpDown numA5_21;
        private System.Windows.Forms.GroupBox groupBox16;
        public System.Windows.Forms.TextBox txtCelZaStrob;
        private System.Windows.Forms.Label label42;
        public System.Windows.Forms.TextBox txtCelVStrob;
        private System.Windows.Forms.Label label43;
        public System.Windows.Forms.TextBox txtStrob;
        private System.Windows.Forms.Label label41;
        public System.Windows.Forms.TextBox txtDalnost;
        private System.Windows.Forms.Label label40;
        public System.Windows.Forms.TrackBar trackA5_21;
        public System.Windows.Forms.TextBox txtDalnostMeasured;
    }
}