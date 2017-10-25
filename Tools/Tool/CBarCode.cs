using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Tools.Tool
{
    public class BarCodesEvent : EventArgs
    {
        public int VirtKey;      //虚拟码  
        public int ScanCode;     //扫描码  
        public string KeyName;   //键名  
        public uint AscII;       //AscII  
        public char Chr;         //字符  

        public string BarCode;   //条码信息  
        public bool IsValid;     //条码是否有效  
        public DateTime Time;    //扫描时间  
    };

    public class CBarCode
    {
        private const int WH_KEYBOARD_LL = 13;

        private IntPtr _hookWindowPtr = IntPtr.Zero;
        private static int _hKeyboardHook = 0;
        private User32.HookProc _KeyBoardHookProc;

        private string strBarCode = "";
        BarCodesEvent barCodeEvent = new BarCodesEvent();

        public delegate void SomeHandler(object sender, BarCodesEvent e);
        public event SomeHandler SomeEvent;

        public event SomeHandler ScanCommodityCodeEvent;
        private List<uint> UKeyList = new List<uint>();

        private bool intercept = true;
        /// <summary>
        /// 是否监听扫描枪
        /// </summary>
        private bool isListen = true;
        /// <summary> 
        /// 是否监听扫描枪
        /// </summary>
        public bool IsListen
        {
            get { return isListen; }
            set { isListen = value; }
        }
        /// <summary>
        /// 是否以2开始的字符串（主要判断
        /// </summary>
        private bool isTwoStart;
        /// <summary>
        /// 统计阻塞信号
        /// </summary>
        protected ManualResetEvent sendTwoEvent = new ManualResetEvent(false);
        //已被发送
        bool isSended = false;
        [StructLayout(LayoutKind.Sequential)]
        private struct KeytEventMsg
        {
            public int message;
            public int paramL;
            public int paramH;
            public int Time;
            public int hwnd;
        };

        private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            if (!IsListen)
                return User32.CallNextHookEx(_hKeyboardHook, nCode, wParam, lParam);
            if (nCode == 0)
            {
                KeytEventMsg msg = (KeytEventMsg)Marshal.PtrToStructure(lParam, typeof(KeytEventMsg));

                if (wParam == 0x100)   //WM_KEYDOWN = 0x100  
                {
                    barCodeEvent.VirtKey = msg.message & 0xff;  //虚拟码  
                    barCodeEvent.ScanCode = msg.paramL & 0xff;  //扫描码  

                    StringBuilder strKeyName = new StringBuilder(255);
                    if (User32.GetKeyNameText(barCodeEvent.ScanCode * 65536, strKeyName, 255) > 0)
                    {
                        barCodeEvent.KeyName = strKeyName.ToString().Trim(new char[] { ' ', '\0' });
                    }
                    else
                    {
                        barCodeEvent.KeyName = "";
                    }

                    byte[] kbArray = new byte[256];
                    uint uKey = 0;
                    User32.GetKeyboardState(kbArray);
                    if (User32.ToAscii(barCodeEvent.VirtKey, barCodeEvent.ScanCode, kbArray, ref uKey, 0))
                    {
                        barCodeEvent.AscII = uKey;
                        barCodeEvent.Chr = Convert.ToChar(uKey);
                    }
                    if (DateTime.Now.Subtract(barCodeEvent.Time).TotalMilliseconds > 50 || intercept == false)
                    {
                        strBarCode = barCodeEvent.Chr.ToString();
                        barCodeEvent.Time = DateTime.Now;
                        barCodeEvent.IsValid = false;
                        //首个输入为2，特殊处理
                        if (barCodeEvent.Chr.ToString() == "2" && intercept == true)
                        {
                            isTwoStart = true;
                            //打开锁，等待输出
                            sendTwoEvent.Set();
                            return 1;
                        }
                        else
                        {
                            isTwoStart = false;
                            return User32.CallNextHookEx(_hKeyboardHook, nCode, wParam, lParam);
                        }
                    }
                    else 
                    {
                        //50毫秒内有键值输入，停止输出
                        sendTwoEvent.Reset();
                        if ((msg.message & 0xff) == 13) //回车  
                        {
                            barCodeEvent.BarCode = strBarCode;
                            barCodeEvent.IsValid = true;
                        }
                        strBarCode += barCodeEvent.Chr.ToString();
                        UKeyList.Add(uKey);
                        barCodeEvent.Time = DateTime.Now;
                        //定义了事件并且条码为有效条码时才触发事件
                        if (SomeEvent != null && barCodeEvent.IsValid)
                        {
                            //AnalyseBarCode();
                            Thread thread1 = new Thread(AnalyseBarCode);
                            thread1.IsBackground = true;
                            thread1.Start();
                        } //触发事件 
                        if (ScanCommodityCodeEvent != null && barCodeEvent.IsValid)
                        {
                            Thread thread2 = new Thread(AnalyseBarCodeAll);
                            thread2.IsBackground = true;
                            thread2.Start();
                        }
                        barCodeEvent.IsValid = false;
                        if (isTwoStart)
                            return 1;
                        else
                        {
                            return User32.CallNextHookEx(_hKeyboardHook, nCode, wParam, lParam);
                        }
                    }
                }
                else
                {
                    return User32.CallNextHookEx(_hKeyboardHook, nCode, wParam, lParam);
                }
            }
            else
            {
                return User32.CallNextHookEx(_hKeyboardHook, nCode, wParam, lParam);
            }
        }
        private string barCode = "";
        private void AnalyseBarCode()
        {
            barCode = barCodeEvent.BarCode;
            if (judeBarCode())
            {
                isSended = true;
                sendTwoEvent.Set();
                SomeEvent(this, barCodeEvent);
            }
            else
            {
                sendBarCode(UKeyList);
            }
            UKeyList = new List<uint>();
            strBarCode = "";
            barCodeEvent.BarCode = "";
        }
        /// <summary>
        /// 首字符为2的等待处理线程
        /// </summary>
        private void ListenTwoThread()
        {
            //循环等待是否有2首次输入
            while (sendTwoEvent.WaitOne())
            {
                Thread.Sleep(50);
                //等待输出
                sendTwoEvent.WaitOne();
                ///为了兼容其他2开头的条码（发送前判断是否被 sendBarCode处理过）
                if (!isSended)
                {
                    intercept = false;
                    try
                    {
                        char[] chs = strBarCode.ToCharArray();
                        if (chs.Length == 1)
                        {
                            User32.keybd_event(Convert.ToByte(chs[0]), 0, 0, 0);
                            User32.keybd_event(Convert.ToByte(chs[0]), 0, 2, 0);
                            strBarCode = "";
                            barCodeEvent.BarCode = "";
                            UKeyList = new List<uint>();
                        }
                    }
                    catch { }
                    intercept = true;
                }
                isSended = false;
                //重新等待
                sendTwoEvent.Reset();
            }
        }
        private void AnalyseBarCodeAll()
        {
            ScanCommodityCodeEvent(this, barCodeEvent);
            if (SomeEvent == null)
            {
                barCode = barCodeEvent.BarCode;
                //sendBarCode(UKeyList);
                isSended = true;
                UKeyList = new List<uint>();
                strBarCode = "";
                barCodeEvent.BarCode = "";
            }
        }

        //检测条码是否为支付宝二维码
        private bool judeBarCode()
        {
            string bCode = barCodeEvent.BarCode.Trim();
            if (bCode.Length == 18 && bCode.Substring(0, 2) == "28")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        //发送消息
        private void sendBarCode(List<uint> e)
        {
            List<uint> ukeyList = new List<uint>();
            ukeyList = e;
            //Stop();
            intercept = false;

            #region 原方式
            //for (int i = 1; i < barCode.Length; i++)
            //{
            //    barCode = barCode.ToUpper();
            //    Char c = barCode.ToCharArray(i, 1)[0];
            //    Byte b = Convert.ToByte(c);
            //    //if (Char.IsLetter(c))
            //    // {
            //    //     b = Convert.ToByte(c);
            //    // }

            //    User32.keybd_event(b, 0, 0, 0);
            //    Thread.Sleep(1);
            //}
            #endregion
            #region 新方式
            barCode = barCode.ToUpper();
            char[] chs = barCode.ToCharArray();
            if (chs.Length > 0)
            {
                if (chs[0].ToString() == "2")
                {
                    User32.keybd_event(Convert.ToByte(chs[0]), 0, 0, 0);
                    Thread.Sleep(12);
                    //已经输出首字符2
                    isSended = true;
                    //等待线程继续（不再输出）
                    sendTwoEvent.Set();
                }
                else
                {
                    //首字符不是2，等待线程无须输出
                    isSended = true;
                }
                for (int i = 1; i < chs.Length; i++)
                {
                    byte b = Convert.ToByte(chs[i]);
                    User32.keybd_event(b, 0, 0, 0);
                    Thread.Sleep(12);
                }
                byte hc = Convert.ToByte(13);
                User32.keybd_event(hc, 0, 0, 0);
            }
            #endregion
            intercept = true;
            //Start();
        }
        public void uninstallSomeEvent()
        {
            SomeEvent = null;
        }

        // 安装钩子   
        public bool Start()
        {
            if (_hKeyboardHook == 0)
            {
                _KeyBoardHookProc = new User32.HookProc(KeyboardHookProc);
                _hookWindowPtr = Kernel32.GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName);

                _hKeyboardHook = User32.SetWindowsHookEx(WH_KEYBOARD_LL, _KeyBoardHookProc, _hookWindowPtr, 0);

                //如果设置钩子失败. 
                if (_hKeyboardHook == 0) UninstallHook();
                //2特殊处理
                Thread thread = new Thread(ListenTwoThread);
                thread.IsBackground = true;
                thread.Start();
            }
            intercept = true;
            return (_hKeyboardHook != 0);
        }


        private void UninstallHook()
        {
            if (_hKeyboardHook != 0)
            {
                bool ret = User32.UnhookWindowsHookEx(_hKeyboardHook);
                if (ret) _hKeyboardHook = 0;
            }
        }

        // 卸载钩子   
        public void Stop()
        {
            intercept = false;
            UninstallHook();
        }
    }
}
