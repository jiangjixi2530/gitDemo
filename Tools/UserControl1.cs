using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Tools
{
    public partial class UserControl1 : UserControl
    {
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out Point p);
        public UserControl1()
        {
            InitializeComponent();
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {

        }
    }
}
