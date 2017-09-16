
using System.Diagnostics;

namespace Tools.BoxLog
{
    public class BoxWriteLog
    {
        private readonly string SoftLog = "";

        public void write()
        {
            Trace.TraceError("这是一个Error级别的日志");
            Trace.TraceWarning("这是一个Warning级别的日志");
            Trace.TraceInformation("这是一个Info级别的日志");
            Trace.WriteLine("这是一个普通日志");
            Trace.Flush();//立即输出
        }
    }
}
