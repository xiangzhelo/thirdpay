using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using viviLib;
using DBAccess;
using viviapi.Model;
using viviLib.ExceptionHandling;
using viviLib.Data;

namespace viviapi.BLL
{
    /// <summary>
    /// 手机验证码操作类
    /// 2012-02-15
    /// </summary>
    public class PhoneValidFactory
    {
        internal const string SQL_TABLE = "phoneValid";
        internal const string SQL_TABLE_FIELD = "[ID],[phone],[count],[isValid],[enable]";

        #region Add
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(PhoneValidLog model)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@phone", SqlDbType.NVarChar,20),
					new SqlParameter("@sendTime", SqlDbType.DateTime),					
					new SqlParameter("@clientIP", SqlDbType.NVarChar,50),
					new SqlParameter("@code", SqlDbType.NVarChar,50),
                    new SqlParameter("@enable", SqlDbType.Bit)};
                parameters[0].Direction = ParameterDirection.Output;

                parameters[1].Value = model.phone;
                parameters[2].Value = model.sendTime;
                parameters[3].Value = model.clientIP;
                parameters[4].Value = model.code;
                parameters[5].Value = model.Enable;

                DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_phoneValid_Add", parameters);
                if (parameters[0].Value != DBNull.Value)
                    return (int)parameters[0].Value;
                return 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }
        #endregion

        #region Setting
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool PhoneSetting(int id,bool state)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4),
                    new SqlParameter("@enable", SqlDbType.Bit)};

                parameters[0].Value = id;
                parameters[1].Value = state;

                return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_phoneValid_setting", parameters) > 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }
        #endregion

        #region SendCount
        /// <summary>
        /// 获取手机已发信息总条数
        /// </summary>
        public static int SendCount(string phone)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select [count] from phoneValid");
                strSql.Append(" where phone=@phone ");
                SqlParameter[] parameters = {
					new SqlParameter("@phone", SqlDbType.NVarChar,50)};
                parameters[0].Value = phone;

                object obj = DataBase.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters);
                if (obj == null || obj == DBNull.Value)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(obj);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        /// <summary>
        /// 是否受限制
        /// true  表示受限制
        /// false 代表不受限制
        /// </summary>
        public static bool isLimited(string phone)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" select [enable] from phoneValid");
                strSql.Append(" where phone=@phone ");
                SqlParameter[] parameters = {
					new SqlParameter("@phone", SqlDbType.NVarChar,50)};
                parameters[0].Value = phone;

                object obj = DataBase.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters);
                if (obj == null || obj == DBNull.Value)
                {
                    return false;
                }
                else
                {
                    return Convert.ToBoolean(obj);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }
        #endregion       

        #region
        /// <summary>
        /// 根据搜索条件返回指定分页的商户信息。
        /// </summary>
        /// <param name="searchParams">搜索条件数组。</param>
        /// <param name="pageSize">分页大小。</param>
        /// <param name="page">页码。</param>
        /// <param name="orderby">排序方式。</param>
        /// <returns>分页数据。</returns>
        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet ds = new DataSet();
            try
            {
                string tables = SQL_TABLE;
                string key = "[id]";
                string fields = SQL_TABLE_FIELD;

                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "id desc";
                }

                List<SqlParameter> paramList = new List<SqlParameter>();
                string where = BuilderWhere(searchParams, paramList);

                string sql = SqlHelper.GetCountSQL(tables, where, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL(fields, tables, where, orderby, key, pageSize, page, false)
                    + "\r\n" + "select * from dbo.phoneValidLog where " + where;                

                ds = DataBase.ExecuteDataset(CommandType.Text, sql, paramList.ToArray());               
                return ds;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return ds;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        private static string BuilderWhere(List<SearchParam> param, List<SqlParameter> paramList)
        {
            StringBuilder builder = new StringBuilder(" 1 = 1");

            if ((param != null) && (param.Count > 0))
            {
                for (int i = 0; i < param.Count; i++)
                {
                    SqlParameter parameter;
                    SearchParam iparam = param[i];
                    switch (iparam.ParamKey.Trim().ToLower())
                    {
                        case "mobile":
                            builder.Append(" AND [phone] like @phone");
                            parameter = new SqlParameter("@phone", SqlDbType.VarChar,40);
                            parameter.Value = "%" + iparam.ParamValue + "%";
                            paramList.Add(parameter);
                            break;                       
                    }
                }
            }
            return builder.ToString();
        }
        #endregion
    }
}
