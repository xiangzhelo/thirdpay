using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace viviLib.Text
{
    public class Validate
    {
        public static string ChkSQL(string str)
        {
            if (str == null)
            {
                return "";
            }
            str = str.Replace("'", "''");
            return str;
        }

        public static string[] GetDifDateAndTime(object todate, object fodate)
        {
            string[] strArray = new string[2];
            TimeSpan span = (TimeSpan)(DateTime.Parse(todate.ToString()) - DateTime.Parse(fodate.ToString()));
            double num = span.TotalSeconds / 86400.0;
            string str = num.ToString();
            int length = num.ToString().Length;
            int startIndex = num.ToString().LastIndexOf(".");
            int num4 = (int)Math.Round(num, 10);
            int num5 = (int)(double.Parse("0" + num.ToString().Substring(startIndex, length - startIndex)) * 24.0);
            strArray[0] = num4.ToString();
            strArray[1] = num5.ToString();
            return strArray;
        }

        public static string GetDifDateAndTime(object todate, object fodate, string v1, string v2, string v3, string v4, string v5, string v6)
        {
            TimeSpan span = (TimeSpan)(DateTime.Parse(todate.ToString()) - DateTime.Parse(fodate.ToString()));
            int num = ((int)span.TotalDays) / 0x16d;
            int num2 = (int)(((span.TotalDays / 365.0) - ((int)(span.TotalDays / 365.0))) * 12.0);
            int num3 = (span.Days - (num * 0x16d)) - (num2 * 30);
            int hours = span.Hours;
            int minutes = span.Minutes;
            string str = "";
            if (0 != num)
            {
                str = str + num.ToString() + v1;
            }
            if (0 != num2)
            {
                str = str + num2.ToString() + v2;
            }
            if (0 != num3)
            {
                str = str + num3.ToString() + v3;
            }
            if (0 != hours)
            {
                str = str + hours.ToString() + v4;
            }
            if (0 != minutes)
            {
                str = str + minutes.ToString() + v5;
            }
            if ((((num == 0) && (num2 == 0)) && ((num3 == 0) && (hours == 0))) && (0 == minutes))
            {
                return v6;
            }
            return str;
        }

        public static string[] GetPercence(int a, int b)
        {
            while (true)
            {
                if (((a % 2) == 0) && (0 == (b % 2)))
                {
                    a /= 2;
                    b /= 2;
                }
                else if (((a % 3) == 0) && (0 == (b % 3)))
                {
                    a /= 3;
                    b /= 3;
                }
                else if (((a % 5) == 0) && (0 == (b % 5)))
                {
                    a /= 5;
                    b /= 5;
                }
                else if (((a % 7) == 0) && (0 == (b % 7)))
                {
                    a /= 7;
                    b /= 7;
                }
                else
                {
                    return new string[] { a.ToString(), b.ToString() };
                }
            }
        }

        public static bool isChinese(string s)
        {
            string pattern = @"^[\u4e00-\u9fa5]{2,}$";
            return Regex.IsMatch(s, pattern);
        }

        //public static bool IsEmail(string s)
        //{
        //    string pattern = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
        //    return Regex.IsMatch(s, pattern);
        //}
        public static bool IsEmail(string _value)
        {
            Regex regex = new Regex(@"^\w+([-+.]\w+)*@(\w+([-.]\w+)*\.)+([a-zA-Z]+)+$", RegexOptions.IgnoreCase);
            return regex.Match(_value).Success;
        }


        public static bool IsInt(string _value)
        {
            Regex regex = new Regex(@"^(-){0,1}\d+$");
            if (regex.Match(_value).Success)
            {
                if ((long.Parse(_value) > 0x7fffffffL) || (long.Parse(_value) < -2147483648L))
                {
                    return false;
                }
                return true;
            }
            return false;
        }
        //public static bool IsInt(string str)
        //{
        //    return Regex.IsMatch(str, "^[0-9]*$");
        //}

        public static bool IsIp(string s)
        {
            string pattern = @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$";
            return Regex.IsMatch(s, pattern);
        }

        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        public static bool IsIPSect(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){2}((2[0-4]\d|25[0-5]|[01]?\d\d?|\*)\.)(2[0-4]\d|25[0-5]|[01]?\d\d?|\*)$");
        }

        //public static bool IsNumeric(string s)
        //{
        //    string pattern = @"^\-?[0-9]+$";
        //    return Regex.IsMatch(s, pattern);
        //}

        public static bool IsPhysicalPath(string s)
        {
            string pattern = @"^\s*[a-zA-Z]:.*$";
            return Regex.IsMatch(s, pattern);
        }

        public static bool IsRelativePath(string s)
        {
            if ((s == null) || (s == string.Empty))
            {
                return false;
            }
            if (s.StartsWith("/") || s.StartsWith("?"))
            {
                return false;
            }
            if (Regex.IsMatch(s, @"^\s*[a-zA-Z]{1,10}:.*$"))
            {
                return false;
            }
            return true;
        }

        public static bool IsSafety(string s)
        {
            string input = Regex.Replace(s.Replace("%20", " "), @"\s", " ");
            string pattern = "select |insert |delete from |count\\(|drop table|update |truncate |asc\\(|mid\\(|char\\(|xp_cmdshell|exec master|net localgroup administrators|:|net user|\"|\\'| or ";
            return !Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }

        public static bool IsUnicode(string s)
        {
            string pattern = @"^[\u4E00-\u9FA5\uE815-\uFA29]+$";
            return Regex.IsMatch(s, pattern);
        }

        public static bool IsUnsFlaot(string s)
        {
            string pattern = "^[0-9]+.?[0-9]+$";
            return Regex.IsMatch(s, pattern);
        }

        public static bool IsUnsNumeric(string s)
        {
            string pattern = "^[0-9]+$";
            return Regex.IsMatch(s, pattern);
        }

        /// <summary>
        /// (h|H)(t|T)(t|T)(p|P)|(f|F)(t|T)(p|P)|(f|F)(i|I)(l|L)(e|E)|(t|T)(e|E)(l|L)(n|N)(e|E)(t|T)|(g|G)(o|O)(p|P)(h|H)(e|E)(r|R)|(h|H)(t|T)(t|T)(p|P)(s|S)|(m|M)(a|A)(i|I)(l|L)(t|T)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsUrl(string source)
        {
            return Regex.IsMatch(source, @"^((h|H)(t|T)(t|T)(p|P)|(f|F)(t|T)(p|P)|(f|F)(i|I)(l|L)(e|E)|(t|T)(e|E)(l|L)(n|N)(e|E)(t|T)|(g|G)(o|O)(p|P)(h|H)(e|E)(r|R)|(h|H)(t|T)(t|T)(p|P)(s|S)|(m|M)(a|A)(i|I)(l|L)(t|T)(o|O)|(n|N)(e|E)(w|W)(s|S)|(w|W)(a|A)(i|I)(s|S))://([\w-]+(\.)?)+[\w-]+(:\d+)?(/[\w- ./?%&=]*)?$", RegexOptions.IgnoreCase);

        }      

        public static bool IsValidDoEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail, @"^@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        public static bool IsValidEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        public static bool IsValiduserName(string strUsername)
        {
            return Regex.IsMatch(strUsername, "^[a-zA-Z]{1}([a-zA-Z0-9]|[._]){5,19}$");
        }

        //public static string MD5(string str)
        //{
        //    byte[] bytes = Encoding.Default.GetBytes(str);
        //    bytes = new MD5CryptoServiceProvider().ComputeHash(bytes);
        //    string str2 = "";
        //    for (int i = 0; i < bytes.Length; i++)
        //    {
        //        str2 = str2 + bytes[i].ToString("x").PadLeft(2, '0');
        //    }
        //    return str2;
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_value"></param>
        /// <returns></returns>
        public static bool IsIDCard(string _value)
        {
            Regex regex;
            string[] strArray;
            DateTime time;
            if ((_value.Length != 15) && (_value.Length != 0x12))
            {
                return false;
            }
            if (_value.Length == 15)
            {
                regex = new Regex(@"^(\d{6})(\d{2})(\d{2})(\d{2})(\d{3})$");
                if (!regex.Match(_value).Success)
                {
                    return false;
                }
                strArray = regex.Split(_value);
                try
                {
                    time = new DateTime(int.Parse("19" + strArray[2]), int.Parse(strArray[3]), int.Parse(strArray[4]));
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            regex = new Regex(@"^(\d{6})(\d{4})(\d{2})(\d{2})(\d{3})([0-9Xx])$");
            if (!regex.Match(_value).Success)
            {
                return false;
            }
            strArray = regex.Split(_value);
            try
            {
                time = new DateTime(int.Parse(strArray[2]), int.Parse(strArray[3]), int.Parse(strArray[4]));
                return true;
            }
            catch
            {
                return false;
            }
        }

        

        public static bool IsLengthStr(string _value, int _begin, int _end)
        {
            int length = _value.Length;
            if ((length < _begin) && (length > _end))
            {
                return false;
            }
            return true;
        }

        public static bool IsLetterOrNumber(string _value)
        {
            return QuickValidate("^[a-zA-Z0-9_]*$", _value);
        }

        /// <summary>
        /// 是否为手机号码
        /// </summary>
        /// <param name="_value"></param>
        /// <returns></returns>
        public static bool IsMobileNum(string _value)
        {
            Regex regex = new Regex(@"^1\d{10}$", RegexOptions.IgnoreCase);
            return regex.Match(_value).Success;
        }

        /// <summary>
        /// 二位小数
        /// </summary>
        /// <param name="_value"></param>
        /// <returns></returns>
        public static bool IsNumber(string _value)
        {
            return QuickValidate("^(0|([1-9]+[0-9]*))(.[0-9][0-9])?$", _value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_value"></param>
        /// <returns></returns>
        public static bool IsNumeric(string _value)
        {
            return QuickValidate("^[1-9]*[0-9]*$", _value);
        }

        public static bool IsPhoneNum(string _value)
        {
            Regex regex = new Regex(@"^(86)?(-)?(0\d{2,3})?(-)?(\d{7,8})(-)?(\d{3,5})?$", RegexOptions.IgnoreCase);
            return regex.Match(_value).Success;
        }

        public static bool IsStringDate(string _value)
        {
            try
            {
                DateTime time = DateTime.Parse(_value);
            }
            catch (FormatException exception)
            {
                Console.WriteLine(exception.Message);
                return false;
            }
            return true;
        }

        //public static bool IsUrl(string _value)
        //{
        //    Regex regex = new Regex(@"(http://)?([\w-]+\.)*[\w-]+(/[\w- ./?%&=]*)?", RegexOptions.IgnoreCase);
        //    return regex.Match(_value).Success;
        //}

        public static bool IsWord(string _value)
        {
            return Regex.IsMatch(_value, "[A-Za-z]");
        }

        public static bool IsWordAndNum(string _value)
        {
            Regex regex = new Regex("[a-zA-Z0-9]?");
            return regex.Match(_value).Success;
        }

        public static bool QuickValidate(string _express, string _value)
        {
            Regex regex = new Regex(_express);
            if (_value.Length == 0)
            {
                return false;
            }
            return regex.IsMatch(_value);
        }

        public static DateTime StrToDate(string _value, DateTime _defaultValue)
        {
            if (IsStringDate(_value))
            {
                return Convert.ToDateTime(_value);
            }
            return _defaultValue;
        }

        public static int StrToInt(string _value, int _defaultValue)
        {
            if (IsNumber(_value))
            {
                return int.Parse(_value);
            }
            return _defaultValue;
        }
    }
}

