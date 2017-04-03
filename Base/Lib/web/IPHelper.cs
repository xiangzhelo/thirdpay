using System;
using System.Globalization;
using System.IO;
using System.Text;
using viviLib.Configuration;
using viviLib.ExceptionHandling;

namespace viviLib.Web
{
    /// <summary>
    /// IPHelper 的摘要说明。
    /// </summary>
    public sealed class IPHelper
    {
        internal static readonly string PATH_BASE = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Configurations\IP\");

        /// <summary>
        /// 构造函数。
        /// </summary>
        private IPHelper()
        {
        }

        /// <summary>
        /// 格式化IP地址。
        /// </summary>
        /// <param name="ip">IP地址。</param>
        /// <returns>格式化好的IP地址。</returns>
        public static string FormatIP(string ip)
        {
            string[] strArray = ip.Trim().Split(new char[] { '.' });
            for (int i = 0; i < strArray.Length; i++)
            {
                strArray[i] = (strArray[i].Length >= 3) ? strArray[i] : (new string('0', 3 - strArray[i].Length) + strArray[i]);
            }
            return string.Join(".", strArray);
        }

        /// <summary>
        /// 根据IP返回地区名。
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string GetArea(string ip)
        {
            return GetArea(PATH_BASE, ip);
        }

        /// <summary>
        /// 根据IP返回地区名。
        /// </summary>
        /// <param name="pathBase">目录名。</param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string GetArea(string pathBase, string ip)
        {
            return GetArea(pathBase, ip, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// 根据IP返回地区名。
        /// </summary>
        /// <param name="pathBase">目录名。</param>
        /// <param name="ip"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string GetArea(string pathBase, string ip, CultureInfo culture)
        {
            try
            {
                int range = Convert.ToInt32(ip.Substring(0, ip.IndexOf(".")), 10);
                string path = GetPath(pathBase, range, culture);
                if (!System.IO.File.Exists(path))
                {
                    path = GetPath(pathBase, range, ConfigHelper.DefaultCulture);
                }
                ip = FormatIP(ip);
                string str2 = string.Empty;
                if (System.IO.File.Exists(path))
                {
                    using (StreamReader reader = new StreamReader(path, Encoding.UTF8, true))
                    {
                        for (string str3 = reader.ReadLine(); str3 != null; str3 = reader.ReadLine())
                        {
                            string[] strArray = str3.Split(new char[] { '-' });
                            if (((strArray.Length > 2) && (string.Compare(ip, strArray[0]) >= 0)) && (string.Compare(ip, strArray[1]) <= 0))
                            {
                                str2 = strArray[2];
                                break;
                            }
                        }
                        reader.Close();
                    }
                    return str2;
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
            return string.Empty;
        }

        /// <summary>
        /// 根据IP值返回IP。
        /// </summary>
        /// <param name="ipValue">IP值。</param>
        /// <returns>IP字符串。</returns>
        public static string GetIP(long ipValue)
        {
            string[] strArray = new string[] { string.Format("{0:000}", Convert.ToInt64(Math.Floor((double) (((double) ipValue) / Math.Pow(256.0, 3.0))))), string.Format("{0:000}", Convert.ToInt64(Math.Floor((double) (((double) ipValue) / Math.Pow(256.0, 2.0)))) % 0x100L), string.Format("{0:000}", Convert.ToInt64(Math.Floor((double) (ipValue / 0x100L))) % 0x100L), string.Format("{0:000}", ipValue % 0x100L) };
            return string.Join(".", strArray);
        }

        /// <summary>
        /// 返回文件名。
        /// </summary>
        /// <param name="range">IP段。</param>
        /// <returns></returns>
        public static string GetPath(int range)
        {
            return GetPath(PATH_BASE, range);
        }

        /// <summary>
        /// 返回文件名。
        /// </summary>
        /// <param name="pathBase">目录名。</param>
        /// <param name="range">IP段。</param>
        /// <returns></returns>
        public static string GetPath(string pathBase, int range)
        {
            return GetPath(pathBase, range, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// 返回文件名。
        /// </summary>
        /// <param name="pathBase">目录名。</param>
        /// <param name="range">IP段。</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string GetPath(string pathBase, int range, CultureInfo culture)
        {
            if ((pathBase != null) && (pathBase.Trim().Length != 0))
            {
                return System.IO.Path.Combine(System.IO.Path.Combine(pathBase, culture.Name), string.Format("{0:000}.000.000.000-{0:000}.255.255.255.txt", range));
            }
            return System.IO.Path.Combine(System.IO.Path.Combine(PATH_BASE, culture.Name), string.Format("{0:000}.000.000.000-{0:000}.255.255.255.txt", range));
        }

        /// <summary>
        /// 根据IP返回值。
        /// </summary>
        /// <param name="ip">IP。</param>
        /// <returns>IP的数字值。</returns>
        public static long GetValue(string ip)
        {
            if ((ip != null) && (ip.Length != 0))
            {
                try
                {
                    long num = 0L;
                    string[] strArray = ip.Trim().Split(new char[] { '.' });
                    for (int i = 0; i < 4; i++)
                    {
                        num += Convert.ToInt32(strArray[i], 10) * Convert.ToInt64(Math.Pow(256.0, (double) (3 - i)));
                    }
                    return num;
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                }
            }
            return 0L;
        }
    }
}

