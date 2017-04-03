using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBAccess;
using viviLib.Data;

namespace viviapi.DAL.Order.Bank
{
    /// <summary>
    /// </summary>
    public class BankNotify
    {
        internal const string SQL_TABLE = "v_orderbanknotify";
        internal const string FIELDS = @"[id]
      ,[orderid]
      ,[status]
      ,[message]
      ,[httpStatusCode]
      ,[StatusDescription]
      ,[againNotifyUrl]
      ,[notifycount]
      ,[notifystat]
      ,[notifycontext]
      ,[addtime]
      ,[notifytime]
      ,[version]
      ,[userorder]
      ,[userid],[agentid]";


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public byte Exists(string orderid)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@orderid", SqlDbType.VarChar,30),
                    new SqlParameter("@addtime", SqlDbType.DateTime,8)	};
            parameters[0].Value = orderid;
            parameters[1].Value = DateTime.Now;

            return
                Convert.ToByte(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_orderbanknotify_exists",
                    parameters));
        }

        /// <summary>
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Insert(Model.Order.Bank.BankNotify model)
        {
            int rowsAffected = 0;

            SqlParameter[] parameters =
            {
                new SqlParameter("@orderid", SqlDbType.VarChar, 30),
                new SqlParameter("@status", SqlDbType.VarChar, 200),
                new SqlParameter("@message", SqlDbType.VarChar, 200),
                new SqlParameter("@httpStatusCode", SqlDbType.VarChar, 200),
                new SqlParameter("@StatusDescription", SqlDbType.VarChar, 2000),
                new SqlParameter("@againNotifyUrl", SqlDbType.VarChar, 2000),
                new SqlParameter("@notifycount", SqlDbType.Int, 4),
                new SqlParameter("@notifystat", SqlDbType.TinyInt, 1),
                new SqlParameter("@notifycontext", SqlDbType.VarChar, 200),
                new SqlParameter("@addtime", SqlDbType.DateTime),
                new SqlParameter("@notifytime", SqlDbType.DateTime)
            };
            parameters[0].Value = model.orderid;
            parameters[1].Value = model.status;
            parameters[2].Value = model.message;
            parameters[3].Value = model.httpStatusCode;
            parameters[4].Value = model.StatusDescription;
            parameters[5].Value = model.againNotifyUrl;
            parameters[6].Value = model.notifycount;
            parameters[7].Value = model.notifystat;
            parameters[8].Value = model.notifycontext;
            parameters[9].Value = model.addtime;
            parameters[10].Value = model.notifytime;

            rowsAffected = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_orderbank_notify", parameters);
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        public void UpdateInQueueStatus(string orderid)
        {
            string sqlText = "update orderbanknotify set inQueue=1 where orderid=@orderid ";
            SqlParameter[] parameters = {
					new SqlParameter("@orderid", SqlDbType.VarChar,30)	};
            parameters[0].Value = orderid;

            DataBase.ExecuteScalar(CommandType.Text, sqlText,
                parameters);
        }

        public DataTable GetNotifyFailList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 500 a.orderid,againNotifyUrl,notifycount,b.[version] ");
            strSql.AppendFormat(" FROM orderbanknotify  a with(nolock) left join orderbank b with(nolock) on a.orderid=b.orderid where notifystat=4 and notifycount<10 and inQueue=0 order by notifycount asc");
           
            return DbHelperSQL.Query(strSql.ToString()).Tables[0];
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" id,orderid,status,message,httpStatusCode,StatusDescription,againNotifyUrl,notifycount,notifystat,notifycontext,addtime,notifytime ");
            strSql.Append(" FROM orderbanknotify ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        #region 查询有关

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
            string tables = SQL_TABLE;
            string key = "[id]";
            if (string.IsNullOrEmpty(orderby))
            {
                orderby = "id desc";
            }

            var paramList = new List<SqlParameter>();
            string searchWhere = BuilderWhere(searchParams, paramList);

            string sql = SqlHelper.GetCountSQL(tables, searchWhere, string.Empty) + "\r\n" +
                         SqlHelper.GetPageSelectSQL(FIELDS, tables, searchWhere, orderby, key, pageSize, page, false);

           //s viviLib.Logging.LogHelper.Write(sql);

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
                foreach (SearchParam iparam in param)
                {
                    SqlParameter parameter;

                    switch (iparam.ParamKey.Trim().ToLower())
                    {
                        case "userid":
                            builder.Append(" AND [userid] = @userid");
                            parameter = new SqlParameter("@userid", SqlDbType.Int);
                            parameter.Value = (int) iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "agentid":
                            builder.Append(" AND [agentid] = @agentid");
                            parameter = new SqlParameter("@agentid", SqlDbType.Int);
                            parameter.Value = (int) iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "userorder":
                            builder.Append(" AND [userorder] like @userorder");
                            parameter = new SqlParameter("@userorder", SqlDbType.VarChar, 30);
                            parameter.Value = "%" + SqlHelper.CleanString((string) iparam.ParamValue, 30) + "%";
                            paramList.Add(parameter);
                            break;
                        case "orderid":
                            builder.Append(" AND [orderid] like @orderid");
                            parameter = new SqlParameter("@orderid", SqlDbType.VarChar, 30);
                            parameter.Value = "%" + SqlHelper.CleanString((string) iparam.ParamValue, 30) + "%";
                            paramList.Add(parameter);
                            break;
                        case "notifystat":
                            builder.Append(" AND [notifystat] = @notifystat");
                            parameter = new SqlParameter("@notifystat", SqlDbType.TinyInt);
                            parameter.Value = (int) iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "stime":
                            builder.Append(" AND [notifytime] >= @stime");
                            parameter = new SqlParameter("@stime", SqlDbType.DateTime);
                            parameter.Value = iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "etime":
                            builder.Append(" AND [notifytime] <= @etime");
                            parameter = new SqlParameter("@etime", SqlDbType.DateTime);
                            parameter.Value = iparam.ParamValue;
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