using System;
using viviLib.Configuration;
using viviLib.ExceptionHandling;
using viviLib.Logging;
using viviLib.TimeControl;

namespace viviLib.ScheduledTask
{
    /// <summary>
    /// ScheduledTaskLog 的摘要说明。
    /// </summary>
    public sealed class ScheduledTaskLog
    {
        static log4net.ILog log = log4net.LogManager.GetLogger("ScheduledTaskLogger");

        private ScheduledTaskLog()
        {
        }

        /// <summary>
        /// 写执行开始日志。
        /// </summary>
        /// <param name="type">类型。</param>
        /// <param name="startTime">开始时间。</param>
        /// <param name="endTime">结束时间。</param>
        public static void WriteExecuteLog(Type type, DateTime startTime, DateTime endTime)
        {
            if (LogSetting.ScheduledTaskLogEnabled)
            {
                try
                {
                    string str = string.Format("{0},{1:yyyy-MM-dd HH:mm:ss.fff},{2:yyyy-MM-dd HH:mm:ss.fff}", type.FullName, startTime, endTime);
                    //LogHelper.Write(LogSetting.ScheduleTaskExecuteLogFilePath(DateTime.Today), str, false);

                    log.Info(str);
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                }
            }
        }

        /// <summary>
        /// 写执行日志文件。
        /// </summary>
        /// <param name="config">配置信息。</param>
        public static void WriteLog(ScheduledTaskConfiguration config)
        {
            if ((config != null) && LogSetting.ScheduledTaskLogEnabled)
            {
                try
                {
                    string str = string.Format("Task\t\t\t\t= {0}\r\nTime              = {1}", config.ScheduledTaskType, FormatConvertor.DateTimeToTimeString(DateTime.Now, true));
                   // LogHelper.Write(LogSetting.ScheduleTaskLogFilePath(DateTime.Today, config), str);

                    log.Info(str);
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                }
            }
        }
    }
}

