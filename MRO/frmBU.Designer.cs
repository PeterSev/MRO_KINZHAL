namespace MRO
{
    partial class frmBU
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
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.chkAckBU = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.numA8_3_Vert = new System.Windows.Forms.NumericUpDown();
            this.trackA8_3_Vert = new System.Windows.Forms.TrackBar();
            this.label18 = new System.Windows.Forms.Label();
            this.numA8_3_Hor = new System.Windows.Forms.NumericUpDown();
            this.trackA8_3_Hor = new System.Windows.Forms.TrackBar();
            this.btnA8_3 = new System.Windows.Forms.Button();
            this.btnA8_1 = new System.Windows.Forms.Button();
            this.groupBox13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numA8_3_Vert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackA8_3_Vert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numA8_3_Hor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackA8_3_Hor)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.chkAckBU);
            this.groupBox13.Controls.Add(this.label17);
            this.groupBox13.Controls.Add(this.numA8_3_Vert);
            this.groupBox13.Controls.Add(this.trackA8_3_Vert);
            this.groupBox13.Controls.Add(this.label18);
            this.groupBox13.Controls.Add(this.numA8_3_Hor);
            this.groupBox13.Controls.Add(this.trackA8_3_Hor);
            this.groupBox13.Controls.Add(this.btnA8_3);
            this.groupBox13.Controls.Add(this.btnA8_1);
            this.groupBox13.Location = new System.Drawing.Point(12, 12);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(576, 130);
            this.groupBox13.TabIndex = 3;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Обмен БУ с абонентом ПКП-БУ";
            // 
            // chkAckBU
            // 
            this.chkAckBU.AutoSize = true;
            this.chkAckBU.Checked = true;
            this.chkAckBU.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAckBU.Location = new System.Drawing.Point(15, 51);
            this.chkAckBU.Name = "chkAckBU";
            this.chkAckBU.Size = new System.Drawing.Size(63, 17);
            this.chkAckBU.TabIndex = 21;
            this.chkAckBU.Text = "Отклик";
            this.chkAckBU.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(159, 80);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(64, 13);
            this.label17.TabIndex = 20;
            this.label17.Text = "Вертикаль:";
            // 
            // numA8_3_Vert
            // 
            this.numA8_3_Vert.DecimalPlaces = 2;
            this.numA8_3_Vert.Increment = new decimal(new int[] {
            9,
            0,
            0,
            131072});
            this.numA8_3_Vert.Location = new System.Drawing.Point(465, 99);
            this.numA8_3_Vert.Maximum = new decimal(new int[] {
            1250,
            0,
            0,
            0});
            this.numA8_3_Vert.Minimum = new decimal(new int[] {
            250,
            0,
            0,
            -2147483648});
            this.numA8_3_Vert.Name = "numA8_3_Vert";
            this.numA8_3_Vert.Size = new System.Drawing.Size(85, 20);
            this.numA8_3_Vert.TabIndex = 19;
            this.numA8_3_Vert.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // trackA8_3_Vert
            // 
            this.trackA8_3_Vert.AutoSize = false;
            this.trackA8_3_Vert.Location = new System.Drawing.Point(251, 74);
            this.trackA8_3_Vert.Maximum = 1250;
            this.trackA8_3_Vert.Minimum = -250;
            this.trackA8_3_Vert.Name = "trackA8_3_Vert";
            this.trackA8_3_Vert.Size = new System.Drawing.Size(299, 24);
            this.trackA8_3_Vert.TabIndex = 18;
            this.trackA8_3_Vert.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(159, 24);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(57, 13);
            this.label18.TabIndex = 17;
            this.label18.Text = "Горизонт:";
            // 
            // numA8_3_Hor
            // 
            this.numA8_3_Hor.DecimalPlaces = 2;
            this.numA8_3_Hor.Increment = new decimal(new int[] {
            9,
            0,
            0,
            131072});
            this.numA8_3_Hor.Location = new System.Drawing.Point(465, 48);
            this.numA8_3_Hor.Maximum = new decimal(new int[] {
            5999,
            0,
            0,
            0});
            this.numA8_3_Hor.Name = "numA8_3_Hor";
            this.numA8_3_Hor.Size = new System.Drawing.Size(85, 20);
            this.numA8_3_Hor.TabIndex = 16;
            this.numA8_3_Hor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // trackA8_3_Hor
            // 
            this.trackA8_3_Hor.AutoSize = false;
            this.trackA8_3_Hor.Location = new System.Drawing.Point(251, 18);
            this.trackA8_3_Hor.Maximum = 5999;
            this.trackA8_3_Hor.Name = "trackA8_3_Hor";
            this.trackA8_3_Hor.Size = new System.Drawing.Size(306, 24);
            this.trackA8_3_Hor.TabIndex = 15;
            this.trackA8_3_Hor.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // btnA8_3
            // 
            this.btnA8_3.Location = new System.Drawing.Point(6, 99);
            this.btnA8_3.Name = "btnA8_3";
            this.btnA8_3.Size = new System.Drawing.Size(196, 23);
            this.btnA8_3.TabIndex = 14;
            this.btnA8_3.Text = "Передача углов ЦУ ПКП-БУ";
            this.btnA8_3.UseVisualStyleBackColor = true;
            // 
            // btnA8_1
            // 
            this.btnA8_1.Location = new System.Drawing.Point(7, 19);
            this.btnA8_1.Name = "btnA8_1";
            this.btnA8_1.Size = new System.Drawing.Size(88, 23);
            this.btnA8_1.TabIndex = 3;
            this.btnA8_1.Text = "Запрос А8.1";
            this.btnA8_1.UseVisualStyleBackColor = true;
            // 
            // frmBU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 155);
            this.Controls.Add(this.groupBox13);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmBU";
            this.Text = "Блок управления";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBU_FormClosing);
            this.Load += new System.EventHandler(this.frmBU_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmBU_KeyDown);
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numA8_3_Vert)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackA8_3_Vert)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numA8_3_Hor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackA8_3_Hor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TrackBar trackA8_3_Vert;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TrackBar trackA8_3_Hor;
        private System.Windows.Forms.Button btnA8_3;
        private System.Windows.Forms.Button btnA8_1;
        public System.Windows.Forms.NumericUpDown numA8_3_Vert;
        public System.Windows.Forms.NumericUpDown numA8_3_Hor;
        public System.Windows.Forms.CheckBox chkAckBU;

    }
}