using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Tools
{
    public partial class FrmPage : Form
    {
        private List<ControlItem> list;
        public FrmPage()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            testPanel.NextPage();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            testPanel.PreviousPage();
        }

        private void testPanel_DataSourceChanged(object sender, EventArgs e)
        {
            label1.Text = testPanel.CurrentPage.ToString();
            label2.Text = testPanel.TotalPage.ToString();
        }

        private void testPanel_PageTurned(object sender, EventArgs e)
        {
            label1.Text = testPanel.CurrentPage.ToString();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            list = new List<ControlItem>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int count = 0;
            try
            {
                count = int.Parse(textBox1.Text.Trim());
            }
            catch
            {
            }
            list = new List<ControlItem>();
            for (var i = 1; i <= count; i++)
            {
                ControlItem item = new ControlItem(i.ToString());
                item.Click += item_Click; ;
                list.Add(item);
            }
            testPanel.DataSource = list.ConvertAll<Control>(input => input as Control);
        }

        void item_Click(object sender, EventArgs e)
        {
            ControlItem item = sender as ControlItem;
            MessageBox.Show(item.goodsName);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(textBox2.Text.Trim()))
            //{
            //   testPanel.Search<ControlItem>(item=>item.goodsName.Contains(textBox2.Text.Trim()));
            //}
        }
    }
}
