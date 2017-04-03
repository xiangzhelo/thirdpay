using System;
using System.IO;
using System.Text;

namespace viviLib.Logging
{
    /// <summary>
    /// 写日志文件操作类。
    /// </summary>
    public sealed class LogHelper
    {
        static log4net.ILog log = log4net.LogManager.GetLogger("SystemLogger");

        /// <summary>
        /// 
        /// </summary>
        private LogHelper()
        {
        }
        public static string GetTenPayLogPath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"LogFiles\payLog\tenpay.log");
        }
        /// <summary>
        /// 写到默认日志文件中。
        /// </summary>
        /// <param name="str"></param>
        public static void Write(string str)
        {
            Write(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"LogFiles\log.log"), str);
        }

        /// <summary>
        /// 写入信息。
        /// </summary>
        /// <param name="path">日志文件路径。</param>
        /// <param name="str">内容。</param>
        /// <returns></returns>
        public static void Write(string path, string str)
        {
            Write(path, str, true);
        }
        public static object obj = new object();
        /// <summary>
        /// 写入信息。
        /// </summary>
        /// <param name="path">日志文件路径。</param>
        /// <param name="str">内容。</param>
        /// <param name="withSeparator">是否带分隔信息。</param>
        /// <returns></returns>
        public static void Write(string path, string str, bool withSeparator)
        {
            //if (!Directory.Exists(Path.GetDirectoryName(path)))
            //{
            //    Directory.CreateDirectory(Path.GetDirectoryName(path));
            //}
            //lock (obj)
            //{
            //    using (StreamWriter writer = new StreamWriter(path, true, Encoding.UTF8))
            //    {
            //        if (withSeparator)
            //        {
            //            writer.WriteLine();
            //            writer.WriteLine(new string('-', 50));
            //        }
            //        writer.WriteLine(str);
            //        writer.Close();
            //    }
            //}


            Debug(str);

        }

        

        public static void Error(string context)
        {
            log.Fatal(context);
        }

        public static void Fatal(string context)
        {
            log.Fatal(context);
        }

        public static void Info(string context)
        {
            log.Info(context);
        }
        public static void Info(string logger, string str)
        {
            log4net.ILog log2 = log4net.LogManager.GetLogger(logger);
            log2.Info(str);
        }
        public static void Debug(string context)
        {
            log.Debug(context);
        }

        public static void Warn(string context)
        {
            log.Warn(context);
        }


    }
}

