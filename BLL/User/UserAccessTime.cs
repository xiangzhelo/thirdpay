using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using viviapi.Model.User;
using DBAccess;
using viviLib.ExceptionHandling;
using viviapi.Model;
using viviLib.Data;

namespace viviapi.BLL.User
{
    /// <summary>
    /// 
    /// </summary>
    public class UserAccessTime
    {
        internal const string SQL_TABLE = "V_userhost";
        internal const string SQL_FIELDS = @"[id],[userid],[siteip],[sitetype],[hostName],[hostUrl],[status],[desc],[username]";

        #region Add
        /// <summary>
        ///  增加一条数据
        /// </summary>
        public static bool Add(UserAccessTimeInfo model)
        {
            try
            {
                int rowsAffected;

                SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@lastAccesstime", SqlDbType.DateTime)};
                parameters[0].Value = model.userid;
                parameters[1].Value = model.lastAccesstime;

                rowsAffected = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_usertime_add", parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }
        #endregion

        #region Delete
        /// <summary>
        ///  增加一条数据
        /// </summary>
        public bool Delete(int id)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
                parameters[0].Value = id;

                bool success = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_usertime_Delete", parameters) > 0;               
                return success;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }
        #endregion

        #region GetModel

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static UserAccessTimeInfo GetModel(int userId)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@userId", SqlDbType.Int,4)};
                parameters[0].Value = userId;

                DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_usertime_GetModel", parameters);
                return GetModelFromDs(ds);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return new UserAccessTimeInfo();
            }
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static UserAccessTimeInfo GetModelFromDs(DataSet ds)
        {
            UserAccessTimeInfo model = new UserAccessTimeInfo();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["userid"].ToString() != "")
                {
                    model.userid = int.Parse(ds.Tables[0].Rows[0]["userid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["lastAccesstime"].ToString() != "")
                {
                    model.lastAccesstime = DateTime.Parse(ds.Tables[0].Rows[0]["lastAccesstime"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region GetList
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(int userId)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@userId", SqlDbType.Int) };
                parameters[0].Value = userId;
                return DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_usertime_GetList", parameters).Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
        #endregion

        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet ds = new DataSet();
            try
            {
                string tables = SQL_TABLE;
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "userid desc,id desc";
                }

                List<SqlParameter> paramList = new List<SqlParameter>();
                string userSearchWhere = BuilderWhere(searchParams, paramList);

                string sql = SqlHelper.GetCountSQL(tables, userSearchWhere, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL(SQL_FIELDS, tables, userSearchWhere, orderby, key, pageSize, page, false);

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
                            builder.Append(" AND [userName] like @UserName");
                            parameter = new SqlParameter("@UserName", SqlDbType.VarChar, 20);
                            parameter.Value = "%" + SqlHelper.CleanString((string)iparam.ParamValue, 20) + "%";
                            paramList.Add(parameter);
                            break;
                        case "status":
                            builder.Append(" AND [status] = @status");
                            parameter = new SqlParameter("@status", SqlDbType.TinyInt);
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
