using System;
using System.IO.Ports;

namespace ViturlComTest
{
    public class CLedControl
    {
        private SerialPort serialPort;

        public CLedControl(String com, int baudRate = 2400)
        {
            this.serialPort = new SerialPort();
            this.serialPort.DataBits = 8;
            this.serialPort.StopBits = StopBits.One;

            this.serialPort.BaudRate = baudRate;
            this.serialPort.PortName = com;
        }

        public bool OpenSerial()
        {
            try
            {
                if (!this.serialPort.IsOpen)
                {
                    this.serialPort.Open();
                    serialPort.BaseStream.Flush();
                }

                return true;

            }
            catch (Exception ex)
            {

            }
            return false;

        }

        public bool SendCommand(String cmdLine)
        {
            try
            {
                this.serialPort.WriteLine(cmdLine);
                return true;

            }
            catch (Exception ex)
            {

            }
            return false;

        }

        public bool SendCommand(byte[] buffer, int offset, int count)
        {
            try
            {
                this.serialPort.Write(buffer, offset, count);
                return true;

            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public bool CloseSerial()
        {
            try
            {
                if (this.serialPort.IsOpen)
                {
                    this.serialPort.Close();
                }
                return true;
            }
            catch (Exception ex)
            {

            }
            return false;

        }

        //清屏
        private bool ClearLed()
        {
            if (!SendCommand(((char)12).ToString()))
            {
                CloseSerial();
                return false;
            }
            return true;
        }

        //功能调用(16进制格式)
        private bool SetLedModel()
        {
            byte[] byteA = { 0x1B, 0x73, 0x32 };

            if (!SendCommand(byteA, 0, 3))
            {
                CloseSerial();
                return false;
            }

            return true;
        }


        //显示金额
        private bool SendData(String data)
        {

            String cmdLine = ((char)27).ToString() + ((char)81).ToString() + ((char)65).ToString() + data + ((char)13).ToString();
            if (!SendCommand(cmdLine))
            {
                CloseSerial();
                return false;
            }

            return true;
        }

        public void DisplayData(String data)
        {
            if (OpenSerial())
            {
                this.ClearLed();//清屏

                this.SetLedModel();//设置合计显示模式

                this.SendData(data);//显示数据

            }
        }
    }
}
