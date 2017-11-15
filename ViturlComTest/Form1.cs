using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;

namespace ViturlComTest
{
    public partial class Form1 : Form
    {
        private SerialPort sendPort;
        private SerialPort receivePort;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbSendCom.DataSource = SerialPort.GetPortNames();
            cmbReceiveCom.DataSource = SerialPort.GetPortNames();
            cmbReceiveCom.Text = string.Empty;
            cmbSendCom.Text = string.Empty;
        }

        private void cmbSendCom_TextChanged(object sender, EventArgs e)
        {
            labSendComStatus.Text = string.IsNullOrEmpty(cmbSendCom.Text.Trim()) ? "串口未选择" : "串口未打开";
            labSendComStatus.ForeColor = string.IsNullOrEmpty(cmbSendCom.Text.Trim()) ? Color.Black : Color.Red;
            btnSendComControl.Enabled = !string.IsNullOrEmpty(cmbSendCom.Text.Trim());
            btnSendComControl.Tag = false;
            try
            {
                sendPort = new SerialPort(cmbSendCom.Text.Trim());
            }
            catch
            {

            }
        }

        private void btnSendComControl_Click(object sender, EventArgs e)
        {
            bool comIsOpen = (bool)btnSendComControl.Tag;
            if (comIsOpen)
            {
                try
                {
                    sendPort.Close();
                    labSendComStatus.Text = "串口" + cmbSendCom.Text.Trim() + "未打开";
                    labSendComStatus.ForeColor = Color.Red;
                    cmbSendCom.Enabled = true;
                    btnSendComControl.Text = "打开串口";
                    btnSendComControl.Tag = false;
                }
                catch (Exception ex)
                {
                    labSendComStatus.Text = "串口" + cmbSendCom.Text.Trim() + "关闭失败，" + ex.Message;
                    labSendComStatus.ForeColor = Color.Red;
                }
            }
            else
            {

                try
                {
                    sendPort.Open();
                    labSendComStatus.Text = "串口" + cmbSendCom.Text.Trim() + "已打开";
                    labSendComStatus.ForeColor = Color.ForestGreen;
                    cmbSendCom.Enabled = false;
                    btnSendComControl.Text = "关闭串口";
                    btnSendComControl.Tag = true;
                }
                catch (Exception ex)
                {
                    labSendComStatus.Text = "串口" + cmbSendCom.Text.Trim() + "打开失败，" + ex.Message;
                    labSendComStatus.ForeColor = Color.Red;
                }
            }
        }

        private CLedControl ledControl;
        private void btnSendData_Click(object sender, EventArgs e)
        {
            try
            {
                if (sendPort != null && sendPort.IsOpen && !string.IsNullOrEmpty(txtSendData.Text))
                {
                    if (chkLed.Checked)
                    {
                        sendPort.Close();
                        ledControl = new CLedControl(cmbSendCom.Text.Trim());
                        ledControl.DisplayData(txtSendData.Text);
                        ledControl.CloseSerial();
                        txtSendLog.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":数据 " + txtSendData.Text +
                                              " 采用客显格式 发送成功！\n");
                        sendPort.Open();
                    }
                    else
                    {
                        sendPort.WriteLine(txtSendData.Text);
                        txtSendLog.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":数据 " + txtSendData.Text +
                                              " 发送成功！\n");
                    }
                }
            }
            catch (Exception ex)
            {
                txtSendLog.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":异常" + ex.Message + "\n");
            }
            txtSendLog.ScrollToCaret();
        }

        private void btnReceiveComControl_Click(object sender, EventArgs e)
        {
            bool comIsOpen = (bool)btnReceiveComControl.Tag;
            if (comIsOpen)
            {
                try
                {
                    receivePort.Close();
                    receivePort.DataReceived -= ReceivePort_DataReceived;
                    labReceiveComStatus.Text = "串口" + cmbReceiveCom.Text.Trim() + "未打开";
                    labReceiveComStatus.ForeColor = Color.Red;
                    cmbReceiveCom.Enabled = true;
                    btnReceiveComControl.Text = "打开串口";
                    btnReceiveComControl.Tag = false;
                }
                catch (Exception ex)
                {
                    labReceiveComStatus.Text = "串口" + cmbReceiveCom.Text.Trim() + "关闭失败，" + ex.Message;
                    labReceiveComStatus.ForeColor = Color.Red;
                }
            }
            else
            {

                try
                {
                    receivePort.Open();
                    receivePort.DataReceived += ReceivePort_DataReceived;
                    labReceiveComStatus.Text = "串口" + cmbReceiveCom.Text.Trim() + "已打开";
                    labReceiveComStatus.ForeColor = Color.ForestGreen;
                    cmbReceiveCom.Enabled = false;
                    btnReceiveComControl.Text = "关闭串口";
                    btnReceiveComControl.Tag = true;
                }
                catch (Exception ex)
                {
                    labReceiveComStatus.Text = "串口" + cmbReceiveCom.Text.Trim() + "打开失败，" + ex.Message;
                    labReceiveComStatus.ForeColor = Color.Red;
                }
            }
        }

        private void cmbReceiveCom_TextChanged(object sender, EventArgs e)
        {
            labReceiveComStatus.Text = string.IsNullOrEmpty(cmbReceiveCom.Text.Trim()) ? "串口未选择" : "串口未打开";
            labReceiveComStatus.ForeColor = string.IsNullOrEmpty(cmbReceiveCom.Text.Trim()) ? Color.Black : Color.Red;
            btnReceiveComControl.Enabled = !string.IsNullOrEmpty(cmbReceiveCom.Text.Trim());
            btnReceiveComControl.Tag = false;
            try
            {
                receivePort = new SerialPort(cmbReceiveCom.Text.Trim());
            }
            catch
            {

            }
        }

        private void ReceivePort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string readString;
            try
            {
                byte[] buffer = new byte[receivePort.BytesToRead];
                receivePort.Read(buffer, 0, buffer.Length);
                readString = System.Text.Encoding.ASCII.GetString(buffer).Trim();
                //readString = receivePort.ReadExisting();
                Invoke((EventHandler)delegate
               {
                   txtReceiveLog.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff") + ":收到数据 " + readString + "\n");
               });
                if (chkLedReceive.Checked)
                {
                    readString = readString.Replace(((char)27).ToString(), "");
                    readString = readString.Replace(((char)81).ToString(), "");
                    readString = readString.Replace(((char)65).ToString(), "");
                    readString = readString.Replace(((char)13).ToString(), "");
                    readString = readString.Replace("s2", ""); //readString.Substring(2, readString.Length - 2);
                    Invoke((EventHandler)delegate
                   {
                       txtReceiveLog.AppendText("转成金额为： " + readString + "\n");
                   });
                }
            }
            catch (Exception ex)
            {
                Invoke((EventHandler)delegate
                {
                    txtReceiveLog.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":出现异常 " + ex.Message + "\n");
                });
            }
            txtReceiveLog.ScrollToCaret();
        }
    }
}
