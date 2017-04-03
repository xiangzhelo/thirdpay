using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace viviLib.Text
{
    /// <summary>
    /// 
    /// </summary>
    public class Strings
    {
        public static string ReplaceStringSeparator(string s)
        {
            return s.Replace(@"\", @"\\").Replace("'", @"\'").Replace("\"", "\\\"").Replace("\n", @"\n").Replace("\r", @"\r");
        }

        /// <summary>
        /// 合并字符
        /// </summary>
        /// <param name="source">要合并的源字符串</param>
        /// <param name="target">要被合并到的目的字符串</param>
        /// <param name="mergechar">合并符</param>
        /// <returns>合并到的目的字符串</returns>
        public static string MergeString(string source, string target)
        {
            return MergeString(source, target, ",");
        }

        /// <summary>
        /// 合并字符
        /// </summary>
        /// <param name="source">要合并的源字符串</param>
        /// <param name="target">要被合并到的目的字符串</param>
        /// <param name="mergechar">合并符</param>
        /// <returns>并到字符串</returns>
        public static string MergeString(string source, string target, string mergechar)
        {
            if (string.IsNullOrEmpty(target))
                target = source;
            else
                target += mergechar + source;

            return target;
        }

        public static String ReplaceString(string source, int start, int len, string repchar)
        {
            try
            {
                string target = string.Empty;
                if (string.IsNullOrEmpty(source))
                    return target;

                if (source.Length < start + len)
                    return target;

                System.Text.StringBuilder repStr = new StringBuilder();

                for (int i = 0; i < len; i++)
                {
                    repStr.Append(repchar);
                }
                target = source.Replace(source.Substring(start, len), repStr.ToString());
                return target;
            }
            catch
            {
                return source;
            }
        }

        public static String ReplaceString(string source, int lev, string repchar)
        {
            try
            {
                string target = string.Empty;
                if (string.IsNullOrEmpty(source))
                    return target;

                if (source.Length < lev)
                    return target;

                int len = source.Length - lev;
                System.Text.StringBuilder repStr = new StringBuilder();

                for (int i = 0; i < len; i++)
                {
                    repStr.Append(repchar);
                }
                target = source.Replace(source.Substring(0, len), repStr.ToString());
                return target;
            }
            catch
            {
                return source;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static String Mark(string num)
        {
            string result = "";
            if (num.Length > 4)
            {
                int l = (num.Length / 3);
                for (int i = 0; i < num.Length - l - l; i++)
                {
                    result = result + "*";
                }
                result = num.Substring(0, l) + result + num.Substring(num.Length - l, l);
                return result;
            }
            else if (num.Length > 1)
            {
                for (int i = 0; i < num.Length - 1; i++)
                {
                    result = result + "*";
                }

                return num.Substring(0, 1) + result;
            }
            else
            {
                return num;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static String Mark(string num, char split)
        {
            string[] arr = num.Split(split);
            if (arr.Length >= 2)
            {
                return Mark(arr[0]) + split.ToString() + arr[1];
            }
            return num;
        }



        public static String ReplaceString(string source, char split, int lev, string repchar)
        {
            try
            {
                string target = string.Empty;
                if (string.IsNullOrEmpty(source))
                    return target;

                string[] temp = source.Split(split);
                if (temp.Length == 1)
                    return target;

                string tempStr = temp[0];
                if (tempStr.Length < lev)
                    return target;

                int len = tempStr.Length - lev;
                System.Text.StringBuilder repStr = new StringBuilder();

                for (int i = 0; i < len; i++)
                {
                    repStr.Append(repchar);
                }
                target = source.Replace(source.Substring(lev - 1, len - 1), repStr.ToString());
                return target;
            }
            catch
            {
                return source;
            }
        }


        /// <summary>
        /// 金额 转化成中文大字显示
        /// </summary>
        /// <param name="lowerMoney"></param>
        /// <returns></returns>
        public static string MoneyToChinese(string lowerMoney)
        {
            string functionReturnValue = null;
            bool isNegative = false; // 是否是负数
            if (lowerMoney.Trim().Substring(0, 1) == "-")
            {
                // 是负数则先转为正数
                lowerMoney = lowerMoney.Trim().Remove(0, 1);
                isNegative = true;
            }
            string strLower = null;
            string strUpart = null;
            string strUpper = null;
            int iTemp = 0;

            // 保留两位小数 123.489→123.49　　123.4→123.4
            lowerMoney = Math.Round(double.Parse(lowerMoney), 2).ToString(CultureInfo.InvariantCulture);
            if (lowerMoney.IndexOf(".", System.StringComparison.Ordinal) > 0)
            {
                if (lowerMoney.IndexOf(".", System.StringComparison.Ordinal) == lowerMoney.Length - 2)
                {
                    lowerMoney = lowerMoney + "0";
                }
            }
            else
            {
                lowerMoney = lowerMoney + ".00";
            }
            strLower = lowerMoney;
            iTemp = 1;
            strUpper = "";
            while (iTemp <= strLower.Length)
            {
                switch (strLower.Substring(strLower.Length - iTemp, 1))
                {
                    case ".":
                        strUpart = "圆";
                        break;
                    case "0":
                        strUpart = "零";
                        break;
                    case "1":
                        strUpart = "壹";
                        break;
                    case "2":
                        strUpart = "贰";
                        break;
                    case "3":
                        strUpart = "叁";
                        break;
                    case "4":
                        strUpart = "肆";
                        break;
                    case "5":
                        strUpart = "伍";
                        break;
                    case "6":
                        strUpart = "陆";
                        break;
                    case "7":
                        strUpart = "柒";
                        break;
                    case "8":
                        strUpart = "捌";
                        break;
                    case "9":
                        strUpart = "玖";
                        break;
                }
                switch (iTemp)
                {
                    case 1:
                        strUpart = strUpart + "分";
                        break;
                    case 2:
                        strUpart = strUpart + "角";
                        break;
                    case 3:
                        strUpart = strUpart + "";
                        break;
                    case 4:
                        strUpart = strUpart + "";
                        break;
                    case 5:
                        strUpart = strUpart + "拾";
                        break;
                    case 6:
                        strUpart = strUpart + "佰";
                        break;
                    case 7:
                        strUpart = strUpart + "仟";
                        break;
                    case 8:
                        strUpart = strUpart + "万";
                        break;
                    case 9:
                        strUpart = strUpart + "拾";
                        break;
                    case 10:
                        strUpart = strUpart + "佰";
                        break;
                    case 11:
                        strUpart = strUpart + "仟";
                        break;
                    case 12:
                        strUpart = strUpart + "亿";
                        break;
                    case 13:
                        strUpart = strUpart + "拾";
                        break;
                    case 14:
                        strUpart = strUpart + "佰";
                        break;
                    case 15:
                        strUpart = strUpart + "仟";
                        break;
                    case 16:
                        strUpart = strUpart + "万";
                        break;
                    default:
                        strUpart = strUpart + "";
                        break;
                }
                strUpper = strUpart + strUpper;
                iTemp = iTemp + 1;
            }
            strUpper = strUpper.Replace("零拾", "零");
            strUpper = strUpper.Replace("零佰", "零");
            strUpper = strUpper.Replace("零仟", "零");
            strUpper = strUpper.Replace("零零零", "零");
            strUpper = strUpper.Replace("零零", "零");
            strUpper = strUpper.Replace("零角零分", "整");
            strUpper = strUpper.Replace("零分", "整");
            strUpper = strUpper.Replace("零角", "零");
            strUpper = strUpper.Replace("零亿零万零圆", "亿圆");
            strUpper = strUpper.Replace("亿零万零圆", "亿圆");
            strUpper = strUpper.Replace("零亿零万", "亿");
            strUpper = strUpper.Replace("零万零圆", "万圆");
            strUpper = strUpper.Replace("零亿", "亿");
            strUpper = strUpper.Replace("零万", "万");
            strUpper = strUpper.Replace("零圆", "圆");
            strUpper = strUpper.Replace("零零", "零");
            // 对壹圆以下的金额的处理
            if (strUpper.Substring(0, 1) == "圆")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "零")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "角")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "分")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "整")
            {
                strUpper = "零圆整";
            }
            functionReturnValue = strUpper;
            if (isNegative == true)
            {
                return "负" + functionReturnValue;
            }
            else
            {
                return functionReturnValue;
            }
        }

        /// <summary>
        /// 生成随机字符串 
        /// </summary>
        /// <param name="length">目标字符串的长度</param>
        /// <param name="useNum">是否包含数字，true=包含</param>
        /// <param name="useLow">是否包含小写字母，true=包含</param>
        /// <param name="useUpp">是否包含大写字母，true=包含</param>
        /// <param name="useSpe">是否包含特殊字符，true=包含</param>
        /// <param name="custom">要包含的自定义字符，直接输入要包含的字符列表</param>
        /// <returns></returns>
        public static string GetRnd(int length, bool useNum, bool useLow, bool useUpp, bool useSpe, string custom)
        {
            var b = new byte[4];
            new RNGCryptoServiceProvider().GetBytes(b);
            var r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = custom;

            if (useNum == true) { str += "0123456789"; }
            if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
            if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            if (useSpe == true) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }

            for (int i = 0; i < length; i++) { s += str.Substring(r.Next(0, str.Length - 1), 1); }

            return s;
        }
    }
}
