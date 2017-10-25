using System;
using System.Windows.Forms;

namespace Tools
{
    public partial class FrmToolMain : Form
    {
        public FrmToolMain()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmBarCode frm=new FrmBarCode();
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmPage frm=new FrmPage();
            frm.ShowDialog();
        }

        private void Panel_LostFocus(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel1.Focus();
        }

        private void FrmToolMain_Load(object sender, EventArgs e)
        {
            panel1.LostFocus += Panel1_LostFocus;
        }

        private void Panel1_LostFocus(object sender, EventArgs e)
        {
            panel1.Visible = !panel1.Visible;
        }

        private void userControl11_Leave(object sender, EventArgs e)
        {

        }

        private void userControl11_Click(object sender, EventArgs e)
        {
            userControl11.Focus();
        }
    }
}
