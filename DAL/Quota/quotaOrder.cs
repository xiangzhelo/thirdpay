using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using viviLib.Data;

namespace viviapi.DAL.Quota
{
    public class quotaOrder
    {
        internal const string SqlTable = "quotaOrder";
        internal const string key = "[id]";
        internal const string FIELDS = @"[id]
      ,[userid]
      ,[orderid]
      ,[quotaValue]
      ,[charge]
      ,[payrate]
      ,[addtime]
      ,[quota_type]
      ,[clientip]
      ,[remark]
      ,[status]
      ,[updatetime]
      ,[year]
      ,[month]";
        public int placeorder(Model.Quota.quotaOrder model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4),
                    new SqlParameter("@userid", SqlDbType.Int,4),
                    new SqlParameter("@quota_type", SqlDbType.Int,4),
                    new SqlParameter("@quotaValue", SqlDbType.Decimal,9),
                    new SqlParameter("@orderid", SqlDbType.VarChar,30),
                    new SqlParameter("@addtime", SqlDbType.DateTime),
                    new SqlParameter("@updatetime", SqlDbType.DateTime),
                    new SqlParameter("@year", SqlDbType.Int,4),
                    new SqlParameter("@month", SqlDbType.Int,4),
                    new SqlParameter("@clientip", SqlDbType.VarChar,20)
            };
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.userid;
            parameters[2].Value = model.quota_type;
            parameters[3].Value = model.quotaValue;
            parameters[4].Value = model.orderid;
            parameters[5].Value = model.addtime;
            parameters[6].Value = model.updatetime;
            parameters[7].Value = model.year;
            parameters[8].Value = model.month;
            parameters[9].Value = model.clientip;
            DbHelperSQL.RunProcedure("proc_quotaOrder_add", parameters, out rowsAffected);
            return (int) parameters[0].Value;
        }
        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby) {
            string tables = SqlTable;
            var paramList = new List<SqlParameter>();
            string searchWhere = BuilderWhere(searchParams, paramList);
            string sql = SqlHelper.GetSelectSQL(FIELDS, tables, searchWhere, orderby, key,false) + "\r\n" +
                         SqlHelper.GetPageSelectSQL(FIELDS, tables, searchWhere, orderby, key, pageSize, page, false);
            DataSet ds = DataBase.ExecuteDataset(CommandType.Text, sql, paramList.ToArray());
            return ds;
        }
        public DataSet getOrder(List<SearchParam> searchParams) {
            string tables = SqlTable;
            var paramList = new List<SqlParameter>();
            string searchWhere = BuilderWhere(searchParams, paramList);
            string sql = SqlHelper.GetSelectSQL(FIELDS, tables, searchWhere, "id desc", key, false);
            DataSet ds = DataBase.ExecuteDataset(CommandType.Text, sql, paramList.ToArray());
            return ds;
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
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "orderid": 
                            builder.Append(" AND [orderid] = @orderid");
                            parameter = new SqlParameter("@orderid", SqlDbType.VarChar,30);
                            parameter.Value = (string)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "quota_type":
                            builder.Append(" AND [quota_type] = @quota_type");
                            parameter = new SqlParameter("@quota_type", SqlDbType.Int);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "year":
                            builder.Append(" AND [year] = @year");
                            parameter = new SqlParameter("@year", SqlDbType.Int);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "month":
                            builder.Append(" AND [month] = @month");
                            parameter = new SqlParameter("@month", SqlDbType.Int);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "stime":
                            builder.Append(" AND [addtime] >= @stime");
                            parameter = new SqlParameter("@stime", SqlDbType.DateTime);
                            parameter.Value = iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "etime":
                            builder.Append(" AND [addtime] <= @etime");
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
