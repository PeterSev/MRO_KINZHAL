using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MRO
{
    public partial class frmBU : Form
    {
        public frmMain _frmMain;

        public frmBU()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            
        }

        private void frmBU_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            _frmMain._frmBU.Hide();
        }

        private void frmBU_Load(object sender, EventArgs e)
        {
            btnA8_1.Click += _frmMain.btnA8_1_Click;
            btnA8_3.Click+=_frmMain.btnA8_3_Click;
            numA8_3_Hor.ValueChanged += numA8_3_Hor_ValueChanged;
            numA8_3_Vert.ValueChanged += numA8_3_Vert_ValueChanged;
            trackA8_3_Hor.Scroll += trackA8_3_Hor_Scroll;
            trackA8_3_Vert.Scroll+=trackA8_3_Vert_Scroll;
        }

        private void trackA8_3_Hor_Scroll(object sender, EventArgs e)
        {
            numA8_3_Hor.Value = trackA8_3_Hor.Value;
        }

        private void numA8_3_Hor_ValueChanged(object sender, EventArgs e)
        {
            trackA8_3_Hor.Value = (int)numA8_3_Hor.Value;
        }

        private void trackA8_3_Vert_Scroll(object sender, EventArgs e)
        {
            numA8_3_Vert.Value = trackA8_3_Vert.Value;
        }

        private void numA8_3_Vert_ValueChanged(object sender, EventArgs e)
        {
            trackA8_3_Vert.Value = (int)numA8_3_Vert.Value;
        }

        private void frmBU_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
