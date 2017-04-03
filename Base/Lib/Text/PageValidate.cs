using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace viviLib.Text
{
    public class PageValidate
    {
        private static Regex RegCHZN = new Regex("[一-龥]");
        private static Regex RegDecimal = new Regex("^[0-9]+[.]?[0-9]+$");
        private static Regex RegDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$");
        //\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*
        private static Regex RegEmail = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        private static Regex RegNumber = new Regex("^[0-9]+$");
        private static Regex RegNumberSign = new Regex("^[+-]?[0-9]+$");
        private static Regex RegPhone = new Regex("^[0-9]+[-]?[0-9]+[-]?[0-9]$");
        private static Regex RegMoblie = new Regex(@"(86)*0*1\d{10}");
        //private static Regex RegUrl = new Regex("^((h|H)(t|T)(t|T)(p|P)|(f|F)(t|T)(p|P)|(f|F)(i|I)(l|L)(e|E)|(t|T)(e|E)(l|L)(n|N)(e|E)(t|T)|(g|G)(o|O)(p|P)(h|H)(e|E)(r|R)|(h|H)(t|T)(t|T)(p|P)(s|S)|(m|M)(a|A)(i|I)(l|L)(t|T)(o|O)|(n|N)(e|E)(w|W)(s|S)|(w|W)(a|A)(i|I)(s|S))://([\\w-]+(\\.)?)+[\\w-]+(:\\d+)?(/[\\w- ./?%&=]*)?$");
        //
        private static Regex RegUrl = new Regex(@"^(http|https|ftp)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&amp;%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{2}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&amp;%\$#\=~_\-]+))*$");

        public static bool CheckIDCard(string Id)
        {
            if (Id.Length == 0x12)
            {
                return CheckIDCard18(Id);
            }
            return ((Id.Length == 15) && CheckIDCard15(Id));
        }

        private static bool CheckIDCard15(string Id)
        {
            long result = 0;
            if (!long.TryParse(Id, out result) || (result < Math.Pow(10.0, 14.0)))
            {
                return false;
            }
            string str = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (str.IndexOf(Id.Remove(2)) == -1)
            {
                return false;
            }
            string s = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (!DateTime.TryParse(s, out time))
            {
                return false;
            }
            return true;
        }

        private static bool CheckIDCard18(string Id)
        {
            long result = 0;
            if ((!long.TryParse(Id.Remove(0x11), out result) || (result < Math.Pow(10.0, 16.0))) || !long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out result))
            {
                return false;
            }
            string str = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (str.IndexOf(Id.Remove(2)) == -1)
            {
                return false;
            }
            string s = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (!DateTime.TryParse(s, out time))
            {
                return false;
            }
            string[] strArray = "1,0,x,9,8,7,6,5,4,3,2".Split(new char[] { ',' });
            string[] strArray2 = "7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2".Split(new char[] { ',' });
            char[] chArray = Id.Remove(0x11).ToCharArray();
            int a = 0;
            for (int i = 0; i < 0x11; i++)
            {
                a += int.Parse(strArray2[i]) * int.Parse(chArray[i].ToString());
            }
            int num4 = -1;
            Math.DivRem(a, 11, out num4);
            if (strArray[num4] != Id.Substring(0x11, 1).ToLower())
            {
                return false;
            }
            return true;
        }

        public static string Decode(string str)
        {
            str = str.Replace("<br>", "\n");
            str = str.Replace("&gt;", ">");
            str = str.Replace("&lt;", "<");
            str = str.Replace("&nbsp;", " ");
            str = str.Replace("&quot;", "\"");
            return str;
        }

        public static string Encode(string str)
        {
            str = str.Replace("&", "&amp;");
            str = str.Replace("'", "''");
            str = str.Replace("\"", "&quot;");
            str = str.Replace(" ", "&nbsp;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            str = str.Replace("\n", "<br>");
            return str;
        }

        //public static string FetchInputDigit(HttpRequest req, string inputKey, int maxLen)
        //{
        //    string sqlInput = string.Empty;
        //    if ((inputKey != null) && (inputKey != string.Empty))
        //    {
        //        sqlInput = req.QueryString[inputKey];
        //        if (sqlInput == null)
        //        {
        //            sqlInput = req.Form[inputKey];
        //        }
        //        if (sqlInput != null)
        //        {
        //            sqlInput = SqlText(sqlInput, maxLen);
        //            if (!IsNumber(sqlInput))
        //            {
        //                sqlInput = string.Empty;
        //            }
        //        }
        //    }
        //    if (sqlInput == null)
        //    {
        //        sqlInput = string.Empty;
        //    }
        //    return sqlInput;
        //}

        //public static string HtmlEncode(string inputData)
        //{
        //    return HttpUtility.HtmlEncode(inputData);
        //}

        public static string InputText(string inputString, int maxLength)
        {
            StringBuilder builder = new StringBuilder();
            if ((inputString != null) && (inputString != string.Empty))
            {
                inputString = inputString.Trim();
                if (inputString.Length > maxLength)
                {
                    inputString = inputString.Substring(0, maxLength);
                }
                for (int i = 0; i < inputString.Length; i++)
                {
                    switch (inputString[i])
                    {
                        case '<':
                            {
                                builder.Append("&lt;");
                                continue;
                            }
                        case '>':
                            {
                                builder.Append("&gt;");
                                continue;
                            }
                        case '"':
                            {
                                builder.Append("&quot;");
                                continue;
                            }
                    }
                    builder.Append(inputString[i]);
                }
                builder.Replace("'", " ");
            }
            return builder.ToString();
        }

        public static bool isContainSameChar(string strInput)
        {
            string charInput = string.Empty;
            if (!string.IsNullOrEmpty(strInput))
            {
                charInput = strInput.Substring(0, 1);
            }
            return isContainSameChar(strInput, charInput, strInput.Length);
        }

        public static bool isContainSameChar(string strInput, string charInput, int lenInput)
        {
            if (string.IsNullOrEmpty(charInput))
            {
                return false;
            }
            Regex regex = new Regex(string.Format("^([{0}])+$", charInput));
            return regex.Match(strInput).Success;
        }

        public static bool isContainSpecChar(string strInput)
        {
            string[] strArray = new string[] { "123456", "654321" };
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strInput == strArray[i])
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsDateTime(string str)
        {
            try
            {
                if (!string.IsNullOrEmpty(str))
                {
                    DateTime.Parse(str);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsDecimal(string inputData)
        {
            return RegDecimal.Match(inputData).Success;
        }

        public static bool IsDecimalSign(string inputData)
        {
            return RegDecimalSign.Match(inputData).Success;
        }

        public static bool IsEmail(string inputData)
        {
            return RegEmail.Match(inputData).Success;
        }

        public static bool IsHasCHZN(string inputData)
        {
            return RegCHZN.Match(inputData).Success;
        }

        public static bool IsNumber(string inputData)
        {
            return RegNumber.Match(inputData).Success;
        }

        public static bool IsNumberSign(string inputData)
        {
            return RegNumberSign.Match(inputData).Success;
        }

        public static bool IsPhone(string inputData)
        {
            return RegPhone.Match(inputData).Success;
        }

        public static bool IsMobile(string inputData)
        {
            return RegMoblie.Match(inputData).Success;
        }

        public static bool IsUrl(string inputData)
        {
            return RegUrl.Match(inputData).Success;
        }

        //public static void SetLabel(Label lbl, object inputObj)
        //{
        //    SetLabel(lbl, inputObj.ToString());
        //}

        //public static void SetLabel(Label lbl, string txtInput)
        //{
        //    lbl.Text = HtmlEncode(txtInput);
        //}

        public static string SqlText(string sqlInput, int maxLength)
        {
            if (!string.IsNullOrEmpty(sqlInput))
            {
                sqlInput = sqlInput.Trim();
                if (sqlInput.Length > maxLength)
                {
                    sqlInput = sqlInput.Substring(0, maxLength);
                }
            }
            return sqlInput;
        }

        public static string SqlTextClear(string sqlText)
        {
            if (sqlText == null)
            {
                return null;
            }
            if (sqlText == "")
            {
                return "";
            }
            sqlText = sqlText.Replace(",", "");
            sqlText = sqlText.Replace("<", "");
            sqlText = sqlText.Replace(">", "");
            sqlText = sqlText.Replace("--", "");
            sqlText = sqlText.Replace("'", "");
            sqlText = sqlText.Replace("\"", "");
            sqlText = sqlText.Replace("=", "");
            sqlText = sqlText.Replace("%", "");
            sqlText = sqlText.Replace(" ", "");
            return sqlText;
        }
    }

}

