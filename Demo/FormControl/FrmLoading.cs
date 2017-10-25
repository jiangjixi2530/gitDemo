using System;
using System.Drawing;
using System.Windows.Forms;

namespace Demo.FormControl
{
    public partial class FrmLoading : Form
    {
        public FrmLoading()
        {
            InitializeComponent();
        }

        private void FrmLoading_Load(object sender, EventArgs e)
        {
            this.TransparencyKey = Color.Red;
            this.Opacity = 0.6;
        }
    }
}
