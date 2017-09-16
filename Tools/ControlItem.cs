using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
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
            this.label1.Text = goodsName;
        }
    }
}
