using System;
using viviLib.TimeControl;
using viviLib.ScheduledTask;

namespace viviLib.Configuration
{
   

    /// <summary>
    /// LogSettings 的摘要说明。
    /// </summary>
    public sealed class LogSetting
    {
        internal static readonly string _group = "logSettings";

        private LogSetting()
        {
        }

        /// <summary>
        /// 异常日志文件路径。
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ExceptionLogFilePath(DateTime date)
        {
            return (AppDomain.CurrentDomain.BaseDirectory + "LogFiles/Exceptions/" + string.Format("{0:yyyy-MM-dd}", date) + ".log");
        }

        

        /// <summary>
        /// 是否打开异常日志记录功能。
        /// </summary>
        public static bool ExceptionLogEnabled
        {
            get
            {
                //return true;
                string config = ConfigHelper.GetConfig(SettingGroup, "ExceptionLogEnabled");
                return ((config != null) && (string.Compare(config, bool.TrueString, true) == 0));
            }
        }

        /// <summary>
        /// 是否打开日志记录功能。
        /// </summary>
        public static bool ScheduledTaskLogEnabled
        {
            get
            {
                //return true;
                string config = ConfigHelper.GetConfig(SettingGroup, "ScheduledTaskLogEnabled");
                return ((config != null) && (string.Compare(config, bool.TrueString, true) == 0));
            }
        }

        /// <summary>
        /// 设置组。
        /// </summary>
        public static string SettingGroup
        {
            get
            {
                return _group;
            }
        }

        /// <summary>
        /// 是否打开日志记录功能。
        /// </summary>
        public static bool SMSLogEnabled
        {
            get
            {
                return true;
                //string config = ConfigHelper.GetConfig(SettingGroup, "SMSLogEnabled");
                //return ((config != null) && (string.Compare(config, bool.TrueString, true) == 0));
            }
        }

        /// <summary>
        /// 计划任务执行日志路径。
        /// </summary>
        /// <param name="date">日期。</param>
        /// <returns>完整路径。</returns>
        public static string ScheduleTaskExecuteLogFilePath(DateTime date)
        {
            return (AppDomain.CurrentDomain.BaseDirectory + "LogFiles/ScheduleTask/" + FormatConvertor.DateTimeToDateString(date, true) + "_execute.log");
        }


        /// <summary>
        /// 计划任务执行日志路径。
        /// </summary>
        /// <param name="date">日期。</param>
        /// <param name="config">配置值。</param>
        /// <returns>完整路径。</returns>
        public static string ScheduleTaskLogFilePath(DateTime date, ScheduledTaskConfiguration config)
        {
            return (AppDomain.CurrentDomain.BaseDirectory + "LogFiles/ScheduleTask/" + FormatConvertor.DateTimeToDateString(date, true) + ".log");
        }

    }
}

