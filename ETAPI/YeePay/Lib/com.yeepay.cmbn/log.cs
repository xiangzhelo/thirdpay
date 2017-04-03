using System;
using System.IO;

namespace viviapi.ETAPI.YeePay.Lib.com.yeepay.cmbn
{
    public class log
    {
        public static string logdir = "c:/";

        public static void logstr(string logFileName, string str)
        {
            try
            {
                StreamWriter writer = new StreamWriter(logdir + logFileName, true);
                writer.BaseStream.Seek(0L, SeekOrigin.End);
                writer.WriteLine("[" + DateTime.Now.ToString() + "]" + str);
                writer.Flush();
                writer.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}

