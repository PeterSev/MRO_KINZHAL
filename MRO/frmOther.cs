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
    public partial class frmOther : Form
    {
        public frmMain _frmMain;
        public frmOther()
        {
            InitializeComponent();
        }

        private void frmOther_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            _frmMain._frmOther.Hide();
        }

        private void frmOther_Load(object sender, EventArgs e)
        {
            chkA5_7_IndNizhPol.CheckedChanged += _frmMain.chkA5_7_CheckedChanged;
            chkA5_7_IndPricel.CheckedChanged += _frmMain.chkA5_7_CheckedChanged;
            chkA5_7_VsplyvInd.CheckedChanged += _frmMain.chkA5_7_CheckedChanged;
            chkA5_7_IndKrugDiag.CheckedChanged += _frmMain.chkA5_7_CheckedChanged;
            chkA5_7_ShowMenu.CheckedChanged += _frmMain.chkA5_7_CheckedChanged;
            chkA5_7_CvetPolya.CheckedChanged += _frmMain.chkA5_7_CheckedChanged;
            chkA5_7_IndUglVizir.CheckedChanged += _frmMain.chkA5_7_CheckedChanged;

            btnA5_25.Click += _frmMain.btnA5_25_Click;
            btnA5_38.Click += _frmMain.btnA5_38_Click;
            btnA5_39.Click += _frmMain.btnA5_39_Click;

            btnA5_34.Click+=_frmMain.btnA5_34_Click;
        }

        private void trackA5_34_Hor_Scroll(object sender, EventArgs e)
        {
            numA5_34_Hor.Value = trackA5_34_Hor.Value;
        }

        private void numA5_34_Hor_ValueChanged(object sender, EventArgs e)
        {
            trackA5_34_Hor.Value = (int)numA5_34_Hor.Value;
        }

        private void numA5_34_Vert_ValueChanged(object sender, EventArgs e)
        {
            trackA5_34_Vert.Value = (int)numA5_34_Vert.Value;
        }

        private void trackA5_34_Vert_Scroll(object sender, EventArgs e)
        {
            numA5_34_Vert.Value = trackA5_34_Vert.Value;
        }

        private void frmOther_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
