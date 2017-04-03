using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBAccess;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.DAL.Finance
{
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
        /// </summary>
        public decimal GetUserIncome(int userId, DateTime sdate, DateTime edate)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@userid", SqlDbType.Int),
                new SqlParameter("@btime", SqlDbType.VarChar, 10),
                new SqlParameter("@etime", SqlDbType.VarChar, 10)
            };

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

        /// <summary>
        /// </summary>
        /// <param name="classid">1网银收入 2点卡收入 3其它收入</param>
        /// <param name="userId"></param>
        /// <param name="sdate"></param>
        /// <param name="edate"></param>
        /// <returns></returns>
        public decimal GetUserIncome(int classid, int userId, DateTime sdate, DateTime edate)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@userid", SqlDbType.Int),
                new SqlParameter("@classid", SqlDbType.TinyInt),
                new SqlParameter("@btime", SqlDbType.DateTime, 8),
                new SqlParameter("@etime", SqlDbType.DateTime, 8)
            };

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


        /// <summary>
        /// </summary>
        public decimal GetUserIncome2(int userId, DateTime sdate, DateTime edate)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@userid", SqlDbType.Int),
                new SqlParameter("@btime", SqlDbType.DateTime, 8),
                new SqlParameter("@etime", SqlDbType.DateTime, 8)
            };

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

        /// <summary>
        ///     总交易金额
        /// </summary>
        public decimal GetUserOrderAmt(int userId, DateTime sdate, DateTime edate)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@userid", SqlDbType.Int),
                new SqlParameter("@btime", SqlDbType.VarChar, 10),
                new SqlParameter("@etime", SqlDbType.VarChar, 10)
            };

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

        /// <summary>
        /// 根据搜索条件返回指定分页的商户信息。
        /// </summary>
        /// <param name="searchParams">搜索条件数组。</param>
        /// <param name="pageSize">分页大小。</param>
        /// <param name="page">页码。</param>
        /// <param name="orderby">排序方式。</param>
        /// <returns>分页数据。</returns>
        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            var ds = new DataSet();
            try
            {
                string tables = SQL_TABLE;
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "userid asc,id desc";
                }

                var paramList = new List<SqlParameter>();
                string where = BuilderWhere(searchParams, paramList);

                string sql = SqlHelper.GetCountSQL(tables, where, string.Empty) + "\r\n" +
                             SqlHelper.GetPageSelectSQL(SQL_TABLE_FIELDS, tables, where, orderby, key, pageSize, page,
                                 false)
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
        /// </summary>
        /// <param name="param"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        private static string BuilderWhere(List<SearchParam> param, List<SqlParameter> paramList)
        {
            var builder = new StringBuilder(" 1 = 1");

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
                            parameter.Value = (int) iparam.ParamValue;
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
                            parameter.Value = (int) iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "supplier":
                            builder.Append(
                                " AND exists(select 0 from ordercard with(nolock) where v_trade.billNo = ordercard.orderid and ordercard.supplierID = @supplier)");
                            parameter = new SqlParameter("@supplier", SqlDbType.Int);
                            parameter.Value = (int) iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                    }
                }
            }
            return builder.ToString();
        }

        /// <summary>
        ///     取用户扣压金额
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataTable GetUserDetentionAmt(int userId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@userid", SqlDbType.Int),
                new SqlParameter("@nowTime", SqlDbType.DateTime)
            };

            parameters[0].Value = userId;
            parameters[1].Value = DateTime.Today;

            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_trade_getuserdetentionAmt",
                parameters);

            return ds.Tables[0];
        }

        #region GetNdaysIncome

        /// <summary>
        /// </summary>
        /// <param name="classid">1 网银 2点卡 3其它</param>
        /// <param name="userid"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public decimal GetNdaysIncome(int classid, int userid, int days)
        {
            DateTime sdt = DateTime.Today.AddDays(0 - days + 1);
            DateTime edt = DateTime.Today.AddDays(1);

            return GetUserIncome(classid, userid, sdt, edt);
        }

        #endregion
    }
}