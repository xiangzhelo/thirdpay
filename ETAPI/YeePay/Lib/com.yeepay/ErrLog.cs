using System;
using System.IO;
using System.Text;

namespace viviapi.ETAPI.YeePay.Lib.com.yeepay
{
    public abstract class ErrLog
    {
        private static string logFilePath = @"G:\易宝接口开发项目\com.yeepay\ErrLog.txt";

        protected ErrLog()
        {
        }

        public static void Write(string strLog)
        {
            if (File.Exists(logFilePath))
            {
                try
                {
                    StreamWriter writer = new StreamWriter(logFilePath, true, Encoding.GetEncoding("gb2312"));
                    writer.WriteLine(DateTime.Now.ToString());
                    writer.WriteLine(strLog);
                    writer.Flush();
                    writer.Close();
                }
                catch
                {
                }
            }
        }
    }
}

