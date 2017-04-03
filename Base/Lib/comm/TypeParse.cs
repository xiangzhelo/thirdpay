using System;
using System.Text.RegularExpressions;

namespace viviLib.Utils
{
    public class TypeParse
    {
        public static bool IsDouble(object Expression)
        {
            return ((Expression != null) && Regex.IsMatch(Expression.ToString(), @"^([0-9])[0-9]*(\.\w*)?$"));
        }

        public static bool IsNumeric(object Expression)
        {
            if (Expression != null)
            {
                string input = Expression.ToString();
                if ((((input.Length > 0) && (input.Length <= 11)) && Regex.IsMatch(input, "^[-]?[0-9]*[.]?[0-9]*$")) && (((input.Length < 10) || ((input.Length == 10) && (input[0] == '1'))) || (((input.Length == 11) && (input[0] == '-')) && (input[1] == '1'))))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsNumericArray(string[] strNumber)
        {
            if (strNumber == null)
            {
                return false;
            }
            if (strNumber.Length < 1)
            {
                return false;
            }
            foreach (string str in strNumber)
            {
                if (!IsNumeric(str))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool StrToBool(object Expression, bool defValue)
        {
            if (Expression != null)
            {
                if (string.Compare(Expression.ToString(), "true", true) == 0)
                {
                    return true;
                }
                if (string.Compare(Expression.ToString(), "false", true) == 0)
                {
                    return false;
                }
            }
            return defValue;
        }

        public static float StrToFloat(object strValue, float defValue)
        {
            if ((strValue == null) || (strValue.ToString().Length > 10))
            {
                return defValue;
            }
            float num = defValue;
            if ((strValue != null) && Regex.IsMatch(strValue.ToString(), @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
            {
                num = Convert.ToSingle(strValue);
            }
            return num;
        }

        public static int StrToInt(object Expression, int defValue)
        {
            if (Expression != null)
            {
                string input = Expression.ToString();
                if ((((input.Length > 0) && (input.Length <= 11)) && Regex.IsMatch(input, "^[-]?[0-9]*$")) && (((input.Length < 10) || ((input.Length == 10) && (input[0] == '1'))) || (((input.Length == 11) && (input[0] == '-')) && (input[1] == '1'))))
                {
                    return Convert.ToInt32(input);
                }
            }
            return defValue;
        }

        /// <summary>
        /// 字符串转成整型数组
        /// </summary>
        /// <param name="idList">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int[] StringToIntArray(string idList, int defValue)
        {
            if (string.IsNullOrEmpty(idList))
                return null;
            string[] strArr = comm.Utils.SplitString(idList, ",");
            int[] intArr = new int[strArr.Length];
            for (int i = 0; i < strArr.Length; i++)
                intArr[i] = StrToInt(strArr[i], defValue);

            return intArr;
        }

        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int ObjectToInt(object expression)
        {
            return ObjectToInt(expression, 0);
        }

        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int ObjectToInt(object expression, int defValue)
        {
            if (expression != null)
                return StrToInt(expression.ToString(), defValue);

            return defValue;
        }
    }
}

