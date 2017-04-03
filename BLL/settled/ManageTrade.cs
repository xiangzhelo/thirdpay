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
    public class ManageTrade
    {
        internal const string SQL_TABLE = "V_ManageTrade";
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
      ,[relname]";


        /// <summary>
        /// 
        /// </summary>
        public static decimal GetManageIncome(int ManageId, DateTime sdate, DateTime edate)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@ManageId", SqlDbType.Int),
					new SqlParameter("@btime", SqlDbType.DateTime,8),
					new SqlParameter("@etime", SqlDbType.DateTime,8)};

                parameters[0].Value = ManageId;
                parameters[1].Value = sdate;
                parameters[2].Value = edate;

                object result = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_trade_getManageIncome", parameters);
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
        ///  增加一条数据
        /// </summary>
        public static int Add(int manageId, int _type, int _billType, string billNo, DateTime tradeTime, decimal Amt, string Remark)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@manageid", SqlDbType.Int,4),
					new SqlParameter("@type", SqlDbType.TinyInt,1),
					new SqlParameter("@billType", SqlDbType.TinyInt,1),
					new SqlParameter("@billNo", SqlDbType.NVarChar,50),
					new SqlParameter("@tradeTime", SqlDbType.DateTime),
					new SqlParameter("@Amt", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.VarChar,100)
                                            };
                parameters[0].Direction = ParameterDirection.Output;
                parameters[1].Value = manageId;
                parameters[2].Value = _type;
                parameters[3].Value = _billType;
                parameters[4].Value = billNo;
                parameters[5].Value = tradeTime;
                parameters[6].Value = Amt;
                parameters[7].Value = Remark;

                DataBase.ExecuteNonQuery( CommandType.StoredProcedure,"proc_managetrade_add", parameters);
                return (int)parameters[0].Value;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static decimal GetSettledAmt(int ManageId, DateTime sdate, DateTime edate)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@ManageId", SqlDbType.Int),
					new SqlParameter("@btime", SqlDbType.DateTime,8),
					new SqlParameter("@etime", SqlDbType.DateTime,8)};

                parameters[0].Value = ManageId;
                parameters[1].Value = sdate;
                parameters[2].Value = edate;

                object result = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_Managetrade_get", parameters);
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
                string userSearchWhere = BuilderWhere(searchParams, paramList);

                string sql = SqlHelper.GetCountSQL(tables, userSearchWhere, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL(SQL_TABLE_FIELDS, tables, userSearchWhere, orderby, key, pageSize, page, false);
                // PageData data = new PageData();

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
                    }
                }
            }
            return builder.ToString();
        }


    }
}
