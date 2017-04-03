using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
namespace DBAccess
{
    public class SqlHelper
    {
        /// <summary>
        /// 替换字符串中的特殊字符
        /// </summary>
        /// <param name="inputString">输入的字符串</param>
        /// <param name="length">最大长度</param>
        /// <param name="cleanSpecial">清除特殊字符后的字符串</param>
        /// <returns>清除特殊字符后的字符串</returns>
        public static string CleanString(string inputString, int length, bool cleanSpecial)
        {
            string str;
            if ((length > 0) && (length < HttpUtility.HtmlEncode(inputString).Length))
            {
                str = HttpUtility.HtmlEncode(inputString).Substring(0, length);
            }
            else
            {
                str = HttpUtility.HtmlEncode(inputString);
            }
            if (cleanSpecial)
            {
                str = str.Replace("'", "''");
            }
            return str;
        }

        public static string CleanString(string inputString, int length)
        {
            return CleanString(inputString, length, false);
        }


        /// <summary>
        /// 取得查询行数的SQL语名
        /// </summary>
        /// <param name="tables">查询的表</param>
        /// <param name="wheres">查询的条件</param>
        /// <param name="distinctField">唯一统计字段</param>
        /// <returns>查询行数的SQL语名</returns>
        public static string GetCountSQL(string tables, string wheres, string distinctField)
        {
            return string.Format("\r\nSELECT \r\n\tCOUNT({0}) \r\nFROM \r\n\t{1} with(nolock)\r\nWHERE\r\n\t{2}", (distinctField.Length > 0) ? ("DISTINCT " + distinctField) : "0", tables, wheres);
        }

        /// <summary>
        /// 根据输入信息合成分页查询SQL语句。
        /// </summary>
        /// <param name="columns">查询的字段。</param>
        /// <param name="tables">查询的表。</param>
        /// <param name="wheres">查询的条件。</param>
        /// <param name="orders">查询的排序。</param>
        /// <param name="key">主键。</param>
        /// <param name="pageSize">页大小。</param>
        /// <param name="pageNum">页码。</param>
        /// <param name="isDistinct">是否只取不相同的。</param>
        /// <returns>分页查询SQL语句。</returns>
        public static string GetPageSelectSQL(string columns, string tables, string wheres, string orders, string key, int pageSize, int pageNum, bool isDistinct)
        {
            int count = pageSize;
            int startIndex = (pageNum - 1) * pageSize;
            if (startIndex < 0)
            {
                startIndex = 0;
            }
            return GetSelectSQL(columns, tables, wheres, orders, key, startIndex, count, isDistinct);
        }

        /// <summary>
        /// 根据输入信息合成查询SQL语句。
        /// </summary>
        /// <param name="columns">查询的字段。</param>
        /// <param name="tables">查询的表。</param>
        /// <param name="wheres">查询的条件。</param>
        /// <param name="orders">查询的排序。</param>
        /// <param name="key">主键。</param>
        /// <param name="isDistinct">是否只取不相同的。</param>
        /// <returns>分页查询SQL语句。</returns>
        public static string GetSelectSQL(string columns, string tables, string wheres, string orders, string key, bool isDistinct)
        {
            return GetSelectSQL(columns, tables, wheres, orders, key, 0, 0, isDistinct);
        }

        /// <summary>
        /// 根据输入信息合成查询SQL语句。
        /// </summary>
        /// <param name="columns">查询的字段。</param>
        /// <param name="tables">查询的表。</param>
        /// <param name="wheres">查询的条件。</param>
        /// <param name="orders">查询的排序。</param>
        /// <param name="key">主键。</param>
        /// <param name="startIndex">开始偏移量。</param>
        /// <param name="isDistinct">是否只取不相同的。</param>
        /// <returns>分页查询SQL语句。</returns>
        public static string GetSelectSQL(string columns, string tables, string wheres, string orders, string key, int startIndex, bool isDistinct)
        {
            return GetSelectSQL(columns, tables, wheres, orders, key, startIndex, 0, isDistinct);
        }

        /// <summary>
        /// 根据输入信息合成查询SQL语句。
        /// </summary>
        /// <param name="columns">查询的字段。</param>
        /// <param name="tables">查询的表。</param>
        /// <param name="wheres">查询的条件。</param>
        /// <param name="orders">查询的排序。</param>
        /// <param name="key">主键。</param>
        /// <param name="startIndex">开始偏移量。</param>
        /// <param name="count">返回数据数量。</param>
        /// <param name="isDistinct">是否只取不相同的。</param>
        /// <returns>分页查询SQL语句。</returns>
        public static string GetSelectSQL(string columns, string tables, string wheres, string orders, string key, int startIndex, int count, bool isDistinct)
        {
            if (startIndex > 1)
            {
                return string.Format("\r\nSELECT {5} {7:d} \r\n\t{0}\r\nFROM \r\n\t{1} with(nolock) \r\nWHERE \r\n\t{2} \r\nAND \r\n\t{4} NOT IN (\r\n\t\tSELECT {5} TOP {6:d}\r\n\t\t\t{4}\r\n\t\tFROM\r\n\t\t\t{1} with(nolock)\r\n\t\tWHERE \r\n\t\t\t{2} \r\n\t\tORDER BY {3}\r\n\t)\r\nORDER BY {3}"
                    , new object[] { columns, tables, wheres, orders, key, isDistinct ? "DISTINCT" : string.Empty, startIndex, (count > 0) ? string.Format("TOP {0:d}", count) : string.Empty });
            }
            return string.Format("\r\nSELECT {4} {5}\r\n\t{0} \r\nFROM \r\n\t{1} with(nolock) \r\nWHERE \r\n\t{2} \r\nORDER BY {3}"
                , new object[] { columns, tables, wheres, orders, isDistinct ? "DISTINCT" : "", (count > 0) ? string.Format("TOP {0:d}", count) : string.Empty });
        }
    }
}
