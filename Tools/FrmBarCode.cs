using System;
using System.Windows.Forms;

namespace Tools
{
    public partial class FrmBarCode : Form
    {
        private Tool.CBarCode _barCode;
        public FrmBarCode()
        {
            InitializeComponent();
        }

        private void chkAutoPromotion_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoPromotion.Checked)
            {
                _barCode.ScanCommodityCodeEvent += BarCode_ScanCommodityCodeEvent;
            }
            else
            {
                _barCode.ScanCommodityCodeEvent -= BarCode_ScanCommodityCodeEvent;
            }
        }

        private void BarCode_ScanCommodityCodeEvent(object sender, Tool.BarCodesEvent e)
        {
            string EventMsg = "触发自动优惠：" + e.BarCode;
            BeginInvoke((EventHandler)delegate
            {
                richTextBox1.AppendText(EventMsg);
            });

        }

        private void chkSimplePay_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSimplePay.Checked)
            {
                _barCode.SomeEvent += BarCode_SomeEvent;
            }
            else
            {
                _barCode.SomeEvent -= BarCode_SomeEvent;
            }
        }

        private void BarCode_SomeEvent(object sender, Tool.BarCodesEvent e)
        {
            string EventMsg = "触发精简模式：" + e.BarCode;
            BeginInvoke((EventHandler)delegate
           {
               richTextBox1.AppendText(EventMsg);
           });
        }

        private void FrmBarCode_Load(object sender, EventArgs e)
        {
            _barCode = new Tool.CBarCode();
            _barCode.Start();
        }
    }
}
