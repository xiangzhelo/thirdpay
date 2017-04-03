using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Web;
using viviLib.ExceptionHandling;
using viviapi.Model;
using viviapi.Model.User;
using viviLib;
using DBAccess;
using viviLib.Data;

namespace viviapi.BLL.Settled
{
    /// <summary>
    /// 
    /// </summary>
    public class Trade
    {
        internal const string SQL_TABLE = "V_Trade";
        //money,pay,nopay,Integral
        internal const string SQL_TABLE_FIELDS = @"[id]
      ,[userid]
      ,[type]
      ,[billType]
      ,[billNo]
      ,[tradeTime]
      ,[Amt]
      ,[Balance]
      ,[Remark]
      ,[username]";


        /// <summary>
        /// 
        /// </summary>
        public static decimal GetUserIncome(int userId,DateTime sdate,DateTime edate)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.Int),
					new SqlParameter("@btime", SqlDbType.VarChar,10),
					new SqlParameter("@etime", SqlDbType.VarChar,10)};

                parameters[0].Value = userId;
                parameters[1].Value = sdate.ToString("yyyy-MM-dd");
                parameters[2].Value = edate.ToString("yyyy-MM-dd");

                object result = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_trade_getuserIncome", parameters);
                if (result != DBNull.Value)
                {
                    return Convert.ToDecimal(result);
                }
                return 0M;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0M;
            }
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="classid">1网银收入 2点卡收入 3其它收入</param>
        /// <param name="userId"></param>
        /// <param name="sdate"></param>
        /// <param name="edate"></param>
        /// <returns></returns>
        public static decimal GetUserIncome(int classid,int userId, DateTime sdate, DateTime edate)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.Int),
                    new SqlParameter("@classid", SqlDbType.TinyInt),
					new SqlParameter("@btime", SqlDbType.DateTime,8),
					new SqlParameter("@etime", SqlDbType.DateTime,8)};

                parameters[0].Value = userId;
                parameters[1].Value = classid;
                parameters[2].Value = sdate;
                parameters[3].Value = edate;

                object result = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_trade_getuserIncomex", parameters);
                if (result != DBNull.Value)
                {
                    return Convert.ToDecimal(result);
                }
                return 0M;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0M;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public static decimal GetUserIncome2(int userId, DateTime sdate, DateTime edate)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.Int),
					new SqlParameter("@btime", SqlDbType.DateTime,8),
					new SqlParameter("@etime", SqlDbType.DateTime,8)};

                parameters[0].Value = userId;
                parameters[1].Value = sdate;
                parameters[2].Value = edate;

                object result = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_trade_getuserIncome2", parameters);
                if (result != DBNull.Value)
                {
                    return Convert.ToDecimal(result);
                }
                return 0M;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0M;
            }
        }

        /// <summary>
        /// 总交易金额
        /// </summary>
        public static decimal GetUserOrderAmt(int userId, DateTime sdate, DateTime edate)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.Int),
					new SqlParameter("@btime", SqlDbType.VarChar,10),
					new SqlParameter("@etime", SqlDbType.VarChar,10)};

                parameters[0].Value = userId;
                parameters[1].Value = sdate.ToString("yyyy-MM-dd");
                parameters[2].Value = edate.ToString("yyyy-MM-dd");

                object result = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_order_getuserOrdAmt", parameters);
                if (result != DBNull.Value)
                {
                    return Convert.ToDecimal(result);
                }
                return 0M;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0M;
            }

        }

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
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "userid asc,id desc";
                }

                List<SqlParameter> paramList = new List<SqlParameter>();
                string where = BuilderWhere(searchParams, paramList);

                string sql = SqlHelper.GetCountSQL(tables, where, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL(SQL_TABLE_FIELDS, tables, where, orderby, key, pageSize, page, false)
                + "\r\n" + @"select 
 sum(case when [type] = 1 then Amt else 0 end) income
,sum(case when [billType] = 2 then Amt else 0 end) agentincome
,sum(case when [type] = 0 then 0-Amt else 0 end) expenditure from v_trade where " + where;                 
                

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
                        case "userid":
                            builder.Append(" AND [userid] = @userid");
                            parameter = new SqlParameter("@userid", SqlDbType.Int);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "stime":
                            builder.Append(" AND [tradeTime] >= @stime");
                            parameter = new SqlParameter("@stime", SqlDbType.DateTime);
                            parameter.Value = iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "etime":
                            builder.Append(" AND [tradeTime] <= @etime");
                            parameter = new SqlParameter("@etime", SqlDbType.DateTime);
                            parameter.Value = iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "billtype":
                            builder.Append(" AND [billtype] = @billtype");
                            parameter = new SqlParameter("@billtype", SqlDbType.Int);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "supplier":
                            builder.Append(" AND exists(select 0 from ordercard with(nolock) where v_trade.billNo = ordercard.orderid and ordercard.supplierID = @supplier)");
                            parameter = new SqlParameter("@supplier", SqlDbType.Int);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                    }
                }
            }
            return builder.ToString();
        }

        #region GetNdaysIncome
        /// <summary>
        /// 
        /// </summary>
        /// <param name="classid">1 网银 2点卡 3其它</param>
        /// <param name="userid"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public static decimal GetNdaysIncome(int classid, int userid, int days)
        {
            try
            {
                DateTime sdt = DateTime.Today.AddDays(0 - days + 1);
                DateTime edt = DateTime.Today.AddDays(1);

                return GetUserIncome(classid,userid, sdt, edt);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0M;
            }
        }
        #endregion
    }
}
