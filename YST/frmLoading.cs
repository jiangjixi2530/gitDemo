using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace YST
{
    public partial class frmLoading : Form
    {
        string msg = "正在导入，请稍后";
        int i = 0;
        public frmLoading()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            i++;
            if (i == 1)
                this.labMsg.Text = msg + ".";
            if (i == 2)
                this.labMsg.Text = msg + "..";
            if (i == 3)
            {
                this.labMsg.Text = msg + "...";
                i = 0;
            }
        }

        private void frmLoading_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.timer1.Enabled = false;
        }
    }
}
