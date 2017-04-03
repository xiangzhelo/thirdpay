using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DBAccess
{
    /// <summary>
    /// 
    /// </summary>
    public class Comm
    {
        static string str = @"0123456789abcdefghigklmnopqrstuvwxyzABCDEFGHIGKLMNOPQRSTUVWXYZ";

        public static string RandomNum(int n) //
        {
            string strchar = "0,1,2,3,4,5,6,7,8,9";
            string[] VcArray = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string VNum = "";//由于字符串很短，就不用StringBuilder了
            int temp = -1; //记录上次随机数值，尽量避免产生几个一样的随
            //机数
            //采用一个简单的算法以保证生成随机数的不同
            Random rand = new Random();
            for (int i = 1; i < n + 1; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * unchecked((int)
                    DateTime.Now.Ticks));
                }
                //int t = rand.Next(35) ;
                int t = rand.Next(10);
                if (temp != -1 && temp == t)
                {
                    return RandomNum(n);
                }
                temp = t;
                VNum += VcArray[t];
            }
            return VNum;//返回生成的随机数
        }

        public static string GetRandomChar(Random rnd)
        {
            // 0-9  

            // A-Z  ASCII值  65-90  

            // a-z  ASCII值  97-122  

            int i = rnd.Next(0, 123);

            if (i < 10)
            {

                // 返回数字  

                return i.ToString();

            }



            char c = (char)i;



            // 返回小写字母加数字  

            // return char.IsLower(c) ? c.ToString() : GetChar(rnd);  



            // 返回大写字母加数字  

            // return char.IsUpper(c) ? c.ToString() : GetChar(rnd);  



            // 返回大小写字母加数字  

            return char.IsLower(c) ? c.ToString() : GetRandomChar(rnd);

        }


        public static int Delete(string tableName, string condition, params SqlParameter[] sqlParams)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentNullException("tableName");
            }
            if (string.IsNullOrEmpty(condition))
            {
                condition = string.Empty;
            }
            condition = condition.Trim();
            if (!string.IsNullOrEmpty(condition) && !condition.StartsWith("WHERE", true, null))
            {
                condition = "WHERE " + condition;
            }
            string commandText = "DELETE FROM [" + tableName + "] " + condition;
            return DataBase.ExecuteNonQuery(CommandType.Text, commandText, sqlParams);
        }

        public static List<T> ExecuteDataset<T>(string cmdText, params SqlParameter[] sqlParams)
        {
            if (string.IsNullOrEmpty(cmdText))
            {
                throw new ArgumentNullException("cmdText");
            }
            using (DataSet set = DataBase.ExecuteDataset(CommandType.Text, cmdText, sqlParams))
            {
                if ((set.Tables.Count < 1) || (set.Tables[0].Rows.Count < 1))
                {
                    return new List<T>(0);
                }
                return DataBinding.LoadForList<T>(set.Tables[0].Rows);
            }
        }

        public static int ExtcuteCommand(string cmdText, params SqlParameter[] sqlParams)
        {
            return DataBase.ExecuteNonQuery(CommandType.Text, cmdText, sqlParams);
        }

        public static int GetRecordCount(string tableName, string condition, params SqlParameter[] sqlParams)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentNullException("tableName");
            }
            condition = condition.Trim();
            if (!string.IsNullOrEmpty(condition) && !condition.StartsWith("WHERE", true, null))
            {
                condition = "WHERE " + condition;
            }
            string commandText = "SELECT COUNT(*) FROM [" + tableName + "] " + condition;
            return (int)DataBase.ExecuteScalar(CommandType.Text, commandText, sqlParams);
        }

        public static void Insert(string cmdText, object dataObj)
        {
            DataBase.ExecuteNonQuery(CommandType.Text, cmdText, DataBinding.BuildParameter(cmdText, dataObj));
        }

        public static List<T> Select<T>(string tableName, string condition, params SqlParameter[] sqlParams)
        {
            return Select<T>(tableName, "*", condition, sqlParams);
        }

        public static List<T> Select<T>(string tableName, string[] fieldsArray, string condition, params SqlParameter[] sqlParams)
        {
            string fields = string.Empty;
            if ((fieldsArray == null) || (fieldsArray.Length == 0))
            {
                fields = "*";
            }
            else
            {
                for (int i = 0; i < (fieldsArray.Length - 1); i++)
                {
                    fields = fields + fieldsArray[i] + ", ";
                }
                fields = fields + fieldsArray[fieldsArray.Length - 1];
            }
            return Select<T>(tableName, fields, -1, condition, sqlParams);
        }

        public static List<T> Select<T>(string tableName, string fields, string condition, params SqlParameter[] sqlParams)
        {
            return Select<T>(tableName, fields, -1, condition, sqlParams);
        }

        public static List<T> Select<T>(string tableName, string fields, int topCount, string condition, params SqlParameter[] sqlParams)
        {
            return Select<T>(tableName, fields, topCount, condition, string.Empty, true, sqlParams);
        }

        public static List<T> Select<T>(string tableName, string fields, string condition, string fldName, int pageIndex, int pageSize)
        {
            return Select<T>(tableName, fields, condition, fldName, pageIndex, pageSize);
        }

        public static List<T> Select<T>(string tableName, string fields, int topCount, string condition, string orderField, bool asc, params SqlParameter[] sqlParams)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentNullException("tableName");
            }
            if (condition == null)
            {
                condition = string.Empty;
            }
            condition = condition.Trim();
            if (!string.IsNullOrEmpty(condition) && !condition.StartsWith("WHERE", true, null))
            {
                condition = "WHERE " + condition;
            }
            if (string.IsNullOrEmpty(fields))
            {
                fields = "*";
            }
            string commandText = "SELECT";
            if (topCount > 0)
            {
                commandText = commandText + " TOP " + topCount.ToString();
            }
            string str2 = commandText;
            commandText = str2 + " " + fields + " FROM [" + tableName + "] " + condition;
            orderField = orderField.Trim();
            if (!string.IsNullOrEmpty(orderField))
            {
                orderField = " ORDER BY [" + orderField + "] " + (asc ? "ASC" : "DESC");
                commandText = commandText + orderField;
            }
            using (DataSet set = DataBase.ExecuteDataset(CommandType.Text, commandText, sqlParams))
            {
                if ((set.Tables.Count < 1) || (set.Tables[0].Rows.Count < 1))
                {
                    return new List<T>(0);
                }
                return DataBinding.LoadForList<T>(set.Tables[0].Rows);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tableName"></param>
        /// <param name="fields"></param>
        /// <param name="condition"></param>
        /// <param name="fldName"></param>
        /// <param name="asc"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static List<T> Select<T>(string tableName, string fields, string condition, string fldName, int asc, int pageIndex, int pageSize)
        {
            List<SqlParameter> list = new List<SqlParameter>(7);
            list.Add(new SqlParameter("@tblName", tableName));
            list.Add(new SqlParameter("@fldName", fldName));
            list.Add(new SqlParameter("@PageSize", pageSize));
            list.Add(new SqlParameter("@PageIndex", pageIndex));
            list.Add(new SqlParameter("@IsCount", SqlDbType.BigInt));
            list.Add(new SqlParameter("@OrderType", asc));
            list.Add(new SqlParameter("@strWhere", condition));
            using (DataSet set = DataBase.ExecuteDataset(CommandType.StoredProcedure, "GetRecordFromPage", list.ToArray()))
            {
                if ((set.Tables.Count < 1) || (set.Tables[0].Rows.Count < 1))
                {
                    return new List<T>(0);
                }
                return DataBinding.LoadForList<T>(set.Tables[0].Rows);
            }
        }

        public static List<T> Select<T>(string tbname, string FieldKey, int PageCurrent, int PageSize, string FieldShow, string FieldOrder, string Where, int pageSize)
        {
            List<SqlParameter> list = new List<SqlParameter>(8);
            list.Add(new SqlParameter("@tbname", tbname));
            list.Add(new SqlParameter("@FieldKey", FieldKey));
            list.Add(new SqlParameter("@PageCurrent", PageCurrent));
            list.Add(new SqlParameter("@PageSize", PageSize));
            list.Add(new SqlParameter("@FieldShow", FieldShow));
            list.Add(new SqlParameter("@FieldOrder", FieldOrder));
            list.Add(new SqlParameter("@Where", Where));
            list.Add(new SqlParameter("@PageCount", 10));
            using (DataSet set = DataBase.ExecuteDataset(CommandType.StoredProcedure, "sp_pageView", list.ToArray()))
            {
                if ((set.Tables.Count < 1) || (set.Tables[0].Rows.Count < 1))
                {
                    return new List<T>(0);
                }
                return DataBinding.LoadForList<T>(set.Tables[0].Rows);
            }
        }

        public static List<T> SelectObjectList<T>(string commandText, params SqlParameter[] sqlParams)
        {
            using (DataSet set = DataBase.ExecuteDataset(CommandType.Text, commandText, sqlParams))
            {
                if ((set.Tables.Count < 1) || (set.Tables[0].Rows.Count < 1))
                {
                    return new List<T>(0);
                }
                return DataBinding.LoadForObjectList<T>(set.Tables[0].Rows);
            }
        }

        public static T SelectOne<T>(string tableName, string condition, params SqlParameter[] sqlParams)
        {
            return SelectOne<T>(tableName, "*", condition, sqlParams);
        }

        public static T SelectOne<T>(string tableName, string fields, string condition, params SqlParameter[] sqlParams)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentNullException("tableName");
            }
            condition = condition.Trim();
            if (!string.IsNullOrEmpty(condition) && !condition.StartsWith("WHERE", true, null))
            {
                condition = "WHERE " + condition;
            }
            fields = fields.Trim();
            if (string.IsNullOrEmpty(fields))
            {
                fields = "*";
            }
            string commandText = "SELECT " + fields + " FROM [" + tableName + "] " + condition;
            using (DataSet set = DataBase.ExecuteDataset(CommandType.Text, commandText, sqlParams))
            {
                if ((set.Tables.Count < 1) || (set.Tables[0].Rows.Count < 1))
                {
                    return default(T);
                }
                return DataBinding.LoadFromDataRow<T>(set.Tables[0].Rows[0]);
            }
        }

        public static T SelectOne<T>(string tableName, string[] fieldsArray, string condition, params SqlParameter[] sqlParams)
        {
            string fields = string.Empty;
            if ((fieldsArray == null) || (fieldsArray.Length == 0))
            {
                fields = "*";
            }
            else
            {
                for (int i = 0; i < (fieldsArray.Length - 1); i++)
                {
                    fields = fields + fieldsArray[i] + ", ";
                }
                fields = fields + fieldsArray[fieldsArray.Length - 1];
            }
            return SelectOne<T>(tableName, fields, condition, sqlParams);
        }

        public static int Update(string cmdText, object dataObj)
        {
            return DataBase.ExecuteNonQuery(CommandType.Text, cmdText, DataBinding.BuildParameter(cmdText, dataObj));
        }
    }
}

