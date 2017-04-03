namespace viviLib.Utils
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;

    public class StringText
    {
        /// <summary>
        /// 生成特定的字符串        
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string BuilderCode(int n) 
        {
            string[] VcArray = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f", "g", "h","i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            StringBuilder code = new StringBuilder();
            int temp = -1; 
            
            Random rand = new Random();
            for (int i = 1; i < n + 1; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * unchecked((int)DateTime.Now.Ticks));
                }                
                int t = rand.Next(57);
                if (temp != -1 && temp == t)
                {
                    return BuilderCode(n);
                }
                temp = t;
                code.Append(VcArray[t]);
            }
            return code.ToString();
        }

        public static string GetPageName()
        {
            string[] strArray = HttpContext.Current.Request.Url.AbsolutePath.Split(new char[] { '/' });
            return strArray[strArray.Length - 1].ToLower();
        }

        public static string GetQueryString(string strName)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null)
            {
                return "";
            }
            return HttpContext.Current.Request.QueryString[strName];
        }

        public static int GetStringLength(string str)
        {
            return Encoding.Default.GetBytes(str).Length;
        }

        public static string getWeekDay(int y, int m, int d)
        {
            DateTime now = DateTime.Now;
            return (now.Year.ToString() + "年" + now.Month.ToString() + "月" + now.Day.ToString() + "日");
        }

        public static bool IsUnicode(string s)
        {
            string pattern = @"^[\u4E00-\u9FA5\uE815-\uFA29]+$";
            return Regex.IsMatch(s, pattern);
        }

        public static string Left(string str, int need, bool encode)
        {
            char ch;
            if ((str == null) || (str == string.Empty))
            {
                return string.Empty;
            }
            int length = str.Length;
            if (length < (need / 2))
            {
                return (encode ? TextEncode(str) : str);
            }
            int num4 = 0;
            int num2 = 0;
            while (num2 < length)
            {
                ch = str[num2];
                num4 += IsUnicode(ch.ToString()) ? 2 : 1;
                if (num4 >= need)
                {
                    break;
                }
                num2++;
            }
            string str2 = str.Substring(0, num2);
            if (length > num2)
            {
                int num3 = 0;
                while (num3 < 5)
                {
                    if (((num2 - num3) >= str.Length) || ((num2 - num3) < 0))
                    {
                        num3--;
                        break;
                    }
                    ch = str[num2 - num3];
                    num4 -= IsUnicode(ch.ToString()) ? 2 : 1;
                    if (num4 <= need)
                    {
                        break;
                    }
                    num3++;
                }
                str2 = str.Substring(0, num2 - num3) + "...";
            }
            return (encode ? TextEncode(str2) : str2);
        }

        public static string ShitEncode(string str)
        {
            string input = "";
            if ((input == null) || (input == string.Empty))
            {
                input = "妈的|你妈|他妈|妈b|妈比|我日|我操|法轮|fuck|shit";
            }
            else
            {
                input = Regex.Replace(Regex.Replace(input, @"\|{2,}", "|"), @"(^\|)|(\|$)", "");
            }
            return Regex.Replace(str, input, "**", RegexOptions.IgnoreCase);
        }

        public static string TextEncode(string str)
        {
            StringBuilder builder = new StringBuilder(str);
            builder.Replace("&", "&amp;");
            builder.Replace("<", "&lt;");
            builder.Replace(">", "&gt;");
            builder.Replace("\"", "&quot;");
            builder.Replace("'", "&#39;");
            return ShitEncode(builder.ToString());
        }
    }
}

