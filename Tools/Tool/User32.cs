using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Tools.Tool
{
    #region User32
    [SuppressUnmanagedCodeSecurity]
    internal static class User32
    {
        /// <summary>
        /// 无效的文件句柄
        /// </summary>
        public static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        #region InstallHook
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int CallNextHookEx(int idHook, int nCode, Int32 wParam, IntPtr lParam);

        [DllImport("user32", EntryPoint = "GetKeyNameText", SetLastError = true)]
        public static extern int GetKeyNameText(int lParam, StringBuilder lpBuffer, int nSize);

        [DllImport("user32", EntryPoint = "GetKeyboardState", SetLastError = true)]
        public static extern int GetKeyboardState(byte[] pbKeyState);

        [DllImport("user32", EntryPoint = "ToAscii", SetLastError = true)]
        public static extern bool ToAscii(int VirtualKey, int ScanCode, byte[] lpKeyState, ref uint lpChar, int uFlags);

        public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);

        #endregion

        [DllImport("user32.dll", EntryPoint = "ShowCursor", CharSet = CharSet.Auto)]
        public static extern int ShowCursor(bool bShow);

        #region RegisterHotKey
        [DllImport("user32.dll", EntryPoint = "RegisterHotKey")]
        public static extern int RegisterHotKey(IntPtr hwnd, int id, int fsModifiers, System.Windows.Forms.Keys vk);
        [DllImport("user32.dll", EntryPoint = "UnregisterHotKey")]
        public static extern int UnregisterHotKey(IntPtr hwnd, int id);
        #endregion

        [DllImport("user32.dll", EntryPoint = "keybd_event", SetLastError = true)]
        public static extern int keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern void SwitchToThisWindow(IntPtr hWnd, Boolean fAltTab);

        [DllImport("user32.dll")]
        public static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);//设置此窗体句柄的窗体为活动窗体
        [DllImport("user32.dll", EntryPoint = "GetWindowDC")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(
             IntPtr hWnd,   // handle to destination window 
             int Msg,    // message 
             int wParam, // first message parameter 
             int lParam // second message parameter 
         );
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
    }
    #endregion
}
