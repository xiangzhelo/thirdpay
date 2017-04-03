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
    public class UserHost
    {
        internal const string SQL_TABLE = "V_userhost";
        internal const string SQL_FIELDS = @"[id],[userid],[siteip],[sitetype],[hostName],[hostUrl],[status],[desc],[username]";

        public const string CACHE_KEY = "USERHOST_{0}";

        #region Exists
        /// <summary>
        ///  
        /// </summary>
        public bool Exists(int userid)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@userId", SqlDbType.Int,4)};
                parameters[0].Value = userid;

                return Convert.ToInt32(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_userhost_Exists", parameters)) == 1;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }
        #endregion

        #region Exists
        /// <summary>
        ///  
        /// </summary>
        public bool Exists(int userid,string host)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@userId", SqlDbType.Int,4),
                    new SqlParameter("@hostName", SqlDbType.VarChar,200)//hostUrl                                            
                                            };
                parameters[0].Value = userid;
                parameters[1].Value = host;

                return Convert.ToInt32(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_userhost_Exists2", parameters)) == 1;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }
        #endregion

        #region Add
        /// <summary>
        ///  增加一条数据
        /// </summary>
        public int Add(UserHostInfo model)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@siteip", SqlDbType.VarChar,50),
					new SqlParameter("@sitetype", SqlDbType.TinyInt,1),
					new SqlParameter("@hostName", SqlDbType.VarChar,200),
					new SqlParameter("@hostUrl", SqlDbType.VarChar,255),
					new SqlParameter("@desc", SqlDbType.VarChar,255),
                    new SqlParameter("@status", SqlDbType.TinyInt)};
                parameters[0].Direction = ParameterDirection.Output;
                parameters[1].Value = model.userid;
                parameters[2].Value = model.siteip;
                parameters[3].Value = model.sitetype;
                parameters[4].Value = model.hostName;
                parameters[5].Value = model.hostUrl;
                parameters[6].Value = model.desc;
                parameters[7].Value = 1;

                DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_userhost_add", parameters);
                return (int)parameters[0].Value;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }
        #endregion

        #region Update
        /// <summary>
        ///  增加一条数据
        /// </summary>
        public bool Update(UserHostInfo model)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@siteip", SqlDbType.VarChar,50),
					new SqlParameter("@sitetype", SqlDbType.TinyInt,1),
					new SqlParameter("@hostName", SqlDbType.VarChar,200),
					new SqlParameter("@hostUrl", SqlDbType.VarChar,255),
					new SqlParameter("@desc", SqlDbType.VarChar,255),
                    new SqlParameter("@status", SqlDbType.TinyInt)};
                parameters[0].Value = model.id;
                parameters[1].Value = model.userid;
                parameters[2].Value = model.siteip;
                parameters[3].Value = model.sitetype;
                parameters[4].Value = model.hostName;
                parameters[5].Value = model.hostUrl;
                parameters[6].Value = model.desc;
                parameters[7].Value = (int)model.status;

                bool success = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_userhost_update", parameters) > 0;
                if (success)
                {
                    ClearCache(model.id);
                }
                return success;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }
        #endregion

        #region ChangeStatus
        /// <summary>
        ///  
        /// </summary>
        public bool ChangeStatus(int id, int status)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
                    new SqlParameter("@status", SqlDbType.TinyInt)};
                parameters[0].Value = id;
                parameters[1].Value = status;

                bool success = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_userhost_ChangeStatus", parameters) > 0;
                if (success)
                {
                    ClearCache(id);
                }
                return success;
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

                bool success = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_userhost_Delete", parameters) > 0;
                if (success)
                {
                    ClearCache(id);
                }
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
        public UserHostInfo GetModel(int id)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
                parameters[0].Value = id;

                DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_userhost_GetModel", parameters);
                return GetModelFromDs(ds);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public UserHostInfo GetCacheModel(int id)
        {
            UserHostInfo model = new UserHostInfo();

            string cacheKey = string.Format(CACHE_KEY, id);
            model = (UserHostInfo)viviapi.Cache.WebCache.GetCacheService().RetrieveObject(cacheKey);

            if (model == null)
            {
                IDictionary<string, object> sqldepparms = new Dictionary<string, object>();
                sqldepparms.Add("id", id);
                SqlDependency sqlDep = DataBase.AddSqlDependency(cacheKey, SQL_TABLE, SQL_FIELDS, "[id]=@id", sqldepparms);

                model = GetModel(id);
                viviapi.Cache.WebCache.GetCacheService().AddObject(cacheKey, model);
            }

            return model;
        }
      

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static UserHostInfo GetModelFromDs(DataSet ds)
        {
            UserHostInfo model = new UserHostInfo();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["userid"].ToString() != "")
                {
                    model.userid = int.Parse(ds.Tables[0].Rows[0]["userid"].ToString());
                }
                model.siteip = ds.Tables[0].Rows[0]["siteip"].ToString();
                if (ds.Tables[0].Rows[0]["sitetype"].ToString() != "")
                {
                    model.sitetype = int.Parse(ds.Tables[0].Rows[0]["sitetype"].ToString());
                }
                model.hostName = ds.Tables[0].Rows[0]["hostName"].ToString();
                model.hostUrl = ds.Tables[0].Rows[0]["hostUrl"].ToString();
                model.desc = ds.Tables[0].Rows[0]["desc"].ToString();
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.status = (UserHostStatus)int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                }
                else
                {
                    model.status = UserHostStatus.未知;
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
        public  DataTable GetList(int userId)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@userId", SqlDbType.Int) };
                parameters[0].Value = userId;
                return DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_userhost_GetList", parameters).Tables[0];
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

        internal void ClearCache(int id)
        {
            string cahcekey = string.Format(CACHE_KEY, id);
            viviapi.Cache.WebCache.GetCacheService().RemoveObject(cahcekey);
        }
    }
}