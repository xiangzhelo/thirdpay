using System;
using viviLib.ExceptionHandling;

namespace viviLib.TimeControl
{

    /// <summary>
    /// FormatConvertor 的摘要说明。
    /// </summary>
    public sealed class FormatConvertor
    {
        /// <summary>
        /// 精确到天的日期时间的格式化字符串。
        /// </summary>
        public static readonly string DATE_FORMAT = "yyyy-MM-dd";
        /// <summary>
        /// 完整日期时间的格式化字符串。
        /// </summary>
        public static readonly string DATETIME_FORMAT = "yyyy-MM-dd HH:mm:ss";
        /// <summary>
        /// 精确到分钟的日期时间的格式化字符串。
        /// </summary>
        public static readonly string DATETIME_FORMAT_WITHOUT_SECOND = "yyyy-MM-dd HH:mm";
        /// <summary>
        /// 返回SQL Server中DateTime类型使用的最小值。
        /// </summary>
        public static readonly DateTime SqlDateTimeMinValue = new DateTime(0x76c, 1, 1, 0, 0, 0, 0);
        /// <summary>
        /// 小时：分钟的时间格式。
        /// </summary>
        public static readonly string TIME_HOUR_MINUTE_FORMAT = "hh:mm";
        /// <summary>
        /// 精确到月的日期时间的格式化字符串。
        /// </summary>
        public static readonly string YEARMONTH_FORMAT = "yyyy-MM";

        /// <summary>
        /// 构造函数。
        /// </summary>
        private FormatConvertor()
        {
        }

        /// <summary>
        /// 把DateTime类型转换成日期字符串
        /// </summary>
        /// <param name="d">DateTime值</param>
        /// <returns>字符串</returns>
        public static string DateTimeToDateString(DateTime d)
        {
            return DateTimeToDateString(d, true);
        }

        /// <summary>
        /// 把DateTime类型转换成日期字符串
        /// </summary>
        /// <param name="d">DateTime值</param>
        /// <param name="viewDay">显示日期</param>
        /// <returns>字符串</returns>
        public static string DateTimeToDateString(DateTime d, bool viewDay)
        {
            if (d == DateTime.MinValue)
            {
                return string.Empty;
            }
            if (viewDay)
            {
                return string.Format("{0:" + DATE_FORMAT + "}", d);
            }
            return string.Format("{0:" + YEARMONTH_FORMAT + "}", d);
        }

        /// <summary>
        /// 把DateTime类型转换成包括时间的s字符串。
        /// </summary>
        /// <param name="d">DateTime值。</param>
        /// <returns>字符串</returns>
        public static string DateTimeToTimeString(DateTime d)
        {
            return DateTimeToTimeString(d, false);
        }

        /// <summary>
        /// 把DateTime类型转换成包括时间的s字符串。
        /// </summary>
        /// <param name="d">DateTime值。</param>
        /// <param name="viewSecond">是否显示到秒。</param>
        /// <returns>字符串</returns>
        public static string DateTimeToTimeString(DateTime d, bool viewSecond)
        {
            if (d == DateTime.MinValue)
            {
                return string.Empty;
            }
            if (!viewSecond)
            {
                return string.Format("{0:" + DATETIME_FORMAT_WITHOUT_SECOND + "}", d);
            }
            return string.Format("{0:" + DATETIME_FORMAT + "}", d);
        }

        /// <summary>
        /// 取得格式化好的时间字符串，格式为HH:mm:ss。
        /// </summary>
        /// <param name="s">时间字符串</param>
        /// <returns>格式化好的时间字符串</returns>
        public static string GetFormatedTime(string s)
        {
            if ((s == null) || (s.Length == 0))
            {
                return "00:00:00";
            }
            string[] strArray = s.Trim().Split(new char[] { ':' });
            try
            {
                switch (strArray.Length)
                {
                    case 1:
                        return string.Format("{0:00}:00:00", Convert.ToInt32(strArray[0], 10));

                    case 2:
                        return string.Format("{0:00}:{1:00}:00", Convert.ToInt32(strArray[0], 10), Convert.ToInt32(strArray[1], 10));

                    case 3:
                        return string.Format("{0:00}:{1:00}:{2:00}", Convert.ToInt32(strArray[0], 10), Convert.ToInt32(strArray[1], 10), Convert.ToInt32(strArray[2], 10));
                }
                return "00:00:00";
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return "00:00:00";
            }
        }

        /// <summary>
        /// 把字符串转换成DateTime类型
        /// </summary>
        /// <remarks>针对yyyy-MM-dd HH:mm:ss型式的值</remarks>
        /// <param name="s">日期时间字符串</param>
        /// <returns>DateTime值</returns>
        public static DateTime StringToDateTime(string s)
        {
            if ((s == null) || (s.Length == 0))
            {
                return DateTime.MinValue;
            }
            string[] strArray = s.Trim().Split(new char[] { '-', ' ', ':' });
            try
            {
                switch (strArray.Length)
                {
                    case 2:
                        return new DateTime(Convert.ToInt32(strArray[0], 10), Convert.ToInt32(strArray[1], 10), 1);

                    case 3:
                        return new DateTime(Convert.ToInt32(strArray[0], 10), Convert.ToInt32(strArray[1], 10), Convert.ToInt32(strArray[2], 10));

                    case 4:
                    case 5:
                    case 6:
                        return new DateTime(Convert.ToInt32(strArray[0], 10), Convert.ToInt32(strArray[1], 10), Convert.ToInt32(strArray[2], 10), Convert.ToInt32(strArray[3], 10), (strArray.Length > 4) ? Convert.ToInt32(strArray[4], 10) : 0, (strArray.Length > 5) ? Convert.ToInt32(strArray[5], 10) : 0);
                }
                return DateTime.MinValue;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// 由表述字符串返回时间隔隔值。
        /// </summary>
        /// <param name="s">表述字符串。</param>
        /// <returns>时间间隔。</returns>
        public static TimeSpan StringToTimeSpan(string s)
        {
            if ((s == null) || (s.Length == 0))
            {
                return new TimeSpan(0L);
            }
            try
            {
                return TimeSpan.Parse(s);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return new TimeSpan(0L);
            }
        }

        /// <summary>
        /// 从时间间隔返回相应的表述字符串。
        /// </summary>
        /// <param name="timeSpan">时间间隔。</param>
        /// <returns>表述字符串。</returns>
        public static string TimsSpanToString(TimeSpan timeSpan)
        {
            return timeSpan.ToString();
        }
    }
}

