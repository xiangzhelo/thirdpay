using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBAccess;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.DAL.Order
{
    public class OrderIncome
    {
        internal const string SQL_TABLE = "v_usersOrderIncome";
        internal const string FIELDS = @"[id]
      ,[mydate]
      ,[typeId]
      ,[modetypename]
      ,[faceValue]
      ,[payrate]
      ,[s_num]
      ,[userId]
      ,[Username]
      ,[full_name]
      ,[sumpay]";

        /// <summary>
        ///     根据搜索条件返回指定分页的商户信息。
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
                    orderby = "id desc";
                }

                var paramList = new List<SqlParameter>();
                string searchWhere = BuilderWhere(searchParams, paramList);

                string sql = SqlHelper.GetCountSQL(tables, searchWhere, string.Empty) + "\r\n" +
                             SqlHelper.GetPageSelectSQL(FIELDS, tables, searchWhere, orderby, key, pageSize, page, false)
                             + "\r\n" + "select sum(sumpay) sumpay,sum(s_num) s_num from " + SQL_TABLE + " where " +
                             searchWhere;


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
        private string BuilderWhere(List<SearchParam> param, List<SqlParameter> paramList)
        {
            var builder = new StringBuilder(" 1 = 1");

            if ((param != null) && (param.Count > 0))
            {
                for (int i = 0; i < param.Count; i++)
                {
                    SqlParameter parameter;
                    SearchParam iparam = param[i];
                    if (iparam.CmpOperator == "=")
                    {
                        switch (iparam.ParamKey.Trim().ToLower())
                        {
                            case "userid":
                                builder.Append(" AND [userid] = @userid");
                                parameter = new SqlParameter("@userid", SqlDbType.Int);
                                parameter.Value = (int) iparam.ParamValue;
                                paramList.Add(parameter);
                                break;
                            case "stime":
                                builder.Append(" AND [mydate] >= @beginmydate");
                                parameter = new SqlParameter("@beginmydate", SqlDbType.VarChar, 10);
                                parameter.Value = iparam.ParamValue;
                                paramList.Add(parameter);
                                break;
                            case "etime":
                                builder.Append(" AND [mydate] <= @endmydate");
                                parameter = new SqlParameter("@endmydate", SqlDbType.VarChar, 10);
                                parameter.Value = iparam.ParamValue;
                                paramList.Add(parameter);
                                break;
                            case "fvaluefrom":
                                builder.Append(" AND [faceValue] >= @fvaluefrom");
                                parameter = new SqlParameter("@fvaluefrom", SqlDbType.Decimal, 9);
                                parameter.Value = iparam.ParamValue;
                                paramList.Add(parameter);
                                break;
                            case "fvalueto":
                                builder.Append(" AND [faceValue] <= @fvalueto");
                                parameter = new SqlParameter("@fvalueto", SqlDbType.Decimal, 9);
                                parameter.Value = iparam.ParamValue;
                                paramList.Add(parameter);
                                break;
                            case "typeid":
                                builder.Append(" AND [typeId] = @typeId");
                                parameter = new SqlParameter("@typeId", SqlDbType.Int);
                                parameter.Value = (int) iparam.ParamValue;
                                paramList.Add(parameter);
                                break;
                        }
                    }
                }
            }
            return builder.ToString();
        }


        /// <summary>
        ///     用户收益统计proc_sysOrderIncome_stat
        /// </summary>
        public DataSet IncomeStat(int userid, string sdate, string edate)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters =
            {
                new SqlParameter("@userId", SqlDbType.Int, 4),
                new SqlParameter("@sdate", SqlDbType.VarChar, 10),
                new SqlParameter("@edate", SqlDbType.VarChar, 10)
            };
            parameters[0].Value = userid;
            parameters[1].Value = sdate;
            parameters[2].Value = edate;

            return DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_usersOrderIncome_stat", parameters);

        }
        /// <summary>
        ///     用户收益统计
        /// </summary>
        public DataSet IncomeStat( string sdate, string edate)
        {
            
            SqlParameter[] parameters =
            {
                new SqlParameter("@sdate", SqlDbType.VarChar, 10),
                new SqlParameter("@edate", SqlDbType.VarChar, 10)
            };
            parameters[0].Value = sdate;
            parameters[1].Value = edate;

            return DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_sysOrderIncome_stat", parameters);

        }

        /// <summary>
        ///     更新一条数据
        /// </summary>
        public DataSet IncomeStatGroupByPayType(int userid, string sdate, string edate)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters =
            {
                new SqlParameter("@userId", SqlDbType.Int, 4),
                new SqlParameter("@sdate", SqlDbType.VarChar, 10),
                new SqlParameter("@edate", SqlDbType.VarChar, 10)
            };
            parameters[0].Value = userid;
            parameters[1].Value = sdate;
            parameters[2].Value = edate;

            return DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_usersOrderIncome_stat_paytype", parameters);

        }
    }
}