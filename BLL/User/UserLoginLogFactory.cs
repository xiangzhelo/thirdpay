using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBAccess;
using viviapi.Model;
using viviapi.Model.User;
using viviLib.ExceptionHandling;
using viviLib.Data;
namespace viviapi.BLL.User
{
    public class UserLoginLogFactory
    {
        internal const string SQL_TABLE = "V_usersLoginLog";
        internal const string FIELD_NEWS = @"[id]
      ,[type]
      ,[userID]
      ,[lastIP]
      ,[address]
      ,[remark]
      ,[lastTime]
      ,[sessionId]
      ,[userName],[payeeName]";

        #region Add
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logEntity"></param>
        /// <returns></returns>
        public static int Add(UserLoginLog logEntity)
        {
            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("insert into usersLoginLog(");
                strSQL.Append("type,userID,lastIP,address,remark,lastTime)");
                strSQL.Append(" values (");
                strSQL.Append("@type,@userID,@lastIP,@address,@remark,@lastTime)");
                SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@type", logEntity.type)
                , new SqlParameter("@userID", logEntity.userID)
                , new SqlParameter("@lastIP", logEntity.lastIP)
                , new SqlParameter("@address", logEntity.address)
                , new SqlParameter("@remark", logEntity.remark)
                , new SqlParameter("@lastTime", logEntity.lastTime) };

                return DataBase.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), parameters);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }
        #endregion

        /// <summary>
        /// É¾³ýµÇÂ¼ÈÕÖ¾
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool Del(int id)
        {
            try
            {
                int rowsAffected = 0;
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
                parameters[0].Value = id;

                rowsAffected = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_usersLoginLog_del", parameters);
                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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
        /// <param name="searchParams"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet ds = new DataSet();
            try
            {
                string tables = SQL_TABLE;
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "lastTime desc";
                }

                List<SqlParameter> paramList = new List<SqlParameter>();
                string where = BuilderWhere(searchParams, paramList);

                string sql = SqlHelper.GetCountSQL(tables, where, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL(FIELD_NEWS, tables, where, orderby, key, pageSize, page, false);

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
                        case "username":
                            builder.Append(" AND [userName] like @userName");
                            parameter = new SqlParameter("@userName", SqlDbType.VarChar, 20);
                            parameter.Value = "%" + SqlHelper.CleanString((string)iparam.ParamValue, 100) + "%";
                            paramList.Add(parameter);
                            break;
                        case "starttime":
                            builder.Append(" AND [lastTime] > @starttime");
                            parameter = new SqlParameter("@starttime", SqlDbType.DateTime);
                            parameter.Value = Convert.ToDateTime(iparam.ParamValue);
                            paramList.Add(parameter);
                            break;
                        case "endtime":
                            builder.Append(" AND [lastTime] < @endtime");
                            parameter = new SqlParameter("@endtime", SqlDbType.DateTime);
                            parameter.Value = Convert.ToDateTime(iparam.ParamValue);
                            paramList.Add(parameter);
                            break;
                    }
                }
            }
            return builder.ToString();
        }
    }
}

