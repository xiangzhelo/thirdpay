using System;
using System.Text;

namespace viviLib.Text
{
    /// <summary>
    /// Regular 的摘要说明。
    /// </summary>
    public sealed class Regular
    {
        /// <summary>
        /// 构造函数。
        /// </summary>
        private Regular()
        {
        }

        /// <summary>
        /// 根据后缀数组生成文件后缀验证正则表达式
        /// </summary>
        /// <param name="exts">文件后缀数组，不含.</param>
        /// <returns>后缀验证正则表达式</returns>
        public static string GetFileExtRegularString(string[] exts)
        {
            if (exts.Length <= 0)
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(@".*(\.(");
            for (int i = 0; i < exts.Length; i++)
            {
                if (i > 0)
                {
                    builder.Append("|");
                }
                if (exts.Length > 1)
                {
                    builder.Append("(");
                }
                string str = exts[i].Trim().ToLower();
                for (int j = 0; j < str.Length; j++)
                {
                    builder.AppendFormat("({0}|{1})", str.Substring(j, 1).ToLower(), str.Substring(j, 1).ToUpper());
                }
                if (exts.Length > 1)
                {
                    builder.Append(")");
                }
            }
            builder.Append("))$");
            return builder.ToString();
        }

        /// <summary>
        /// 正则表达式。
        /// </summary>
        /// <param name="type">验证类型</param>
        /// <returns>验证的正则表达式</returns>
        public static string GetRegularString(RegularType type)
        {
            return GetRegularString(type, 0);
        }

        /// <summary>
        /// 正则表达式。
        /// </summary>
        /// <param name="type">验证类型</param>
        /// <param name="length">长度</param>
        /// <returns>验证的正则表达式</returns>
        public static string GetRegularString(RegularType type, int length)
        {
            switch (type)
            {
                case RegularType.Word:
                    if (length > 0)
                    {
                        return (@"^[\w]{0," + string.Format("{0:d}", length) + "}$");
                    }
                    return @"^[\w]*$";

                case RegularType.Email:
                    return @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

                case RegularType.Url:
                    return @"^((h|H)(t|T)(t|T)(p|P)|(f|F)(t|T)(p|P)|(f|F)(i|I)(l|L)(e|E)|(t|T)(e|E)(l|L)(n|N)(e|E)(t|T)|(g|G)(o|O)(p|P)(h|H)(e|E)(r|R)|(h|H)(t|T)(t|T)(p|P)(s|S)|(m|M)(a|A)(i|I)(l|L)(t|T)(o|O)|(n|N)(e|E)(w|W)(s|S)|(w|W)(a|A)(i|I)(s|S))://([\w-]+(\.)?)+[\w-]+(:\d+)?(/[\w- ./?%&=]*)?$";

                case RegularType.Number:
                    return @"^-{0,1}\d{1,}\.{0,1}\d{0,}$";

                case RegularType.Int:
                    return @"^-{0,1}\d{1,}$";

                case RegularType.Date:
                    return @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$";

                case RegularType.DateTime:
                    return @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d$";

                case RegularType.Time:
                    return @"^(20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d$";

                case RegularType.ChinesePostalCode:
                    return @"\d{6}";

                case RegularType.ChineseIDCard:
                    return @"(^\d{17}[xX\d]{1}$)|(^\d{15}$)";

                case RegularType.Domain:
                    return @"^\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            }
            return string.Empty;
        }

        /// <summary>
        /// 图片判断正则表达式。
        /// </summary>
        public static string ImageRegularString
        {
            get
            {
                return GetFileExtRegularString(new string[] { "jpg", "gif", "jpeg", "bmp", "png" });
            }
        }
    }
}

