using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Web;

using DBAccess;
using viviLib;
using viviLib.ExceptionHandling;
using viviapi.Model;
using viviapi.Model.Settled;
using viviLib.Data;

namespace viviapi.BLL.Settled
{
    /// <summary>
    /// 
    /// </summary>
    public class UsersAmtFreeze
    {
        internal const string SQL_TABLE = "v_usersAmtFreeze";
        //money,pay,nopay,Integral
        internal const string SQL_TABLE_FIELDS = @"[id]
      ,[userid]
      ,[freezeAmt]
      ,[addtime]
      ,[manageId]
      ,[status]
      ,[checktime]
      ,[why]
      ,[unfreezemode],username,full_name";

        /// <summary>
        /// 
        /// </summary>
        public static bool Freeze(viviapi.Model.Settled.UsersAmtFreezeInfo model)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@result", SqlDbType.Bit),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@Freeze", SqlDbType.Decimal,9),
					new SqlParameter("@addtime", SqlDbType.DateTime),
					new SqlParameter("@manageId", SqlDbType.Int,4),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
                    new SqlParameter("@why", SqlDbType.VarChar,50),
                    new SqlParameter("@unfreezemode", SqlDbType.TinyInt,1)};
                parameters[0].Direction = ParameterDirection.Output;
                parameters[1].Value = model.userid;
                parameters[2].Value = model.freezeAmt;
                parameters[3].Value = model.addtime;
                parameters[4].Value = model.manageId;
                parameters[5].Value = (int)model.status;
                parameters[6].Value = model.why;
                parameters[7].Value = (int)model.unfreezemode;

                if (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_usersAmt_Freeze", parameters) > 0)
                {
                    return (bool)parameters[0].Value;
                }
                return false;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public static bool unFreeze(int id, AmtunFreezeMode mode)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@result", SqlDbType.Bit),
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@checktime", SqlDbType.DateTime),
                    new SqlParameter("@unfreezemode", SqlDbType.TinyInt,1)};
                parameters[0].Direction = ParameterDirection.Output;
                parameters[1].Value = id;
                parameters[2].Value = DateTime.Now;
                parameters[3].Value = (int)mode;

                if (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_usersAmt_unFreeze", parameters) > 0)
                {
                    return (bool)parameters[0].Value;
                }
                return false;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
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
                    orderby = "id desc";
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


    
