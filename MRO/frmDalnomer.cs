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
    public partial class frmDalnomer : Form
    {
        public frmMain _frmMain;
        public frmDalnomer()
        {
            InitializeComponent();
        }

        private void frmDalnomer_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            _frmMain._frmDalnomer.Hide();
        }

        private void trackA5_21_Scroll(object sender, EventArgs e)
        {
            numA5_21.Value = trackA5_21.Value;
        }

        private void numA5_21_ValueChanged(object sender, EventArgs e)
        {
            trackA5_21.Value = (int)numA5_21.Value;
        }

        private void frmDalnomer_Load(object sender, EventArgs e)
        {
            btnA5_21.Click += _frmMain.btnA5_21_Click;
            btnA5_6.Click += _frmMain.btnA5_6_Click;
            btnA5_13.Click += _frmMain.btnA5_13_Click;
            btnA5_27.Click += _frmMain.btnA5_27_Click;
            btnA5_28.Click += _frmMain.btnA5_28_Click;
            btnA5_40.Click += _frmMain.btnA5_40_Click;
        }

        private void frmDalnomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
