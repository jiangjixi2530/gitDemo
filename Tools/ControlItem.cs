using System;
using System.Windows.Forms;

namespace Tools
{
    public partial class ControlItem : UserControl
    {
        public string goodsName;
        public ControlItem(string name)
        {
            InitializeComponent();
            goodsName = name;
        }

        private void ControlItem_Load(object sender, EventArgs e)
        {
            label1.Text = goodsName;
        }
    }
}
