using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBAccess;
using viviapi.Model;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Communication
{
    /// <summary>
    /// 
    /// </summary>
    public class feedback
    {
        internal const string SQL_TABLE = "V_feedback";
        internal const string SQL_FIELDS = @"[id]
      ,[userid]
      ,[typeid]
      ,[title]
      ,[cont]
      ,[status]
      ,[addtime]
      ,[reply]
      ,[replyer]
      ,[replytime]
      ,[userName]
      ,[replyname]
      ,[relname],clientip";

        public static string CACHE_KEY = Sys.Constant.CacheMark + "USERHOST_{0}";

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

                return Convert.ToInt32(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_feedback_Exists", parameters)) == 1;
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
        public int Add(feedbackInfo model)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@typeid", SqlDbType.TinyInt,1),
					new SqlParameter("@title", SqlDbType.NVarChar,50),
					new SqlParameter("@cont", SqlDbType.NVarChar,200),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@addtime", SqlDbType.DateTime),
					new SqlParameter("@reply", SqlDbType.NVarChar,50),
					new SqlParameter("@replyer", SqlDbType.Int,4),
					new SqlParameter("@replytime", SqlDbType.DateTime),
                                            new SqlParameter("@clientip", SqlDbType.VarChar,20)};
                parameters[0].Direction = ParameterDirection.Output;
                parameters[1].Value = model.userid;
                parameters[2].Value = model.typeid;
                parameters[3].Value = model.title;
                parameters[4].Value = model.cont;
                parameters[5].Value = model.status;
                parameters[6].Value = model.addtime;
                parameters[7].Value = model.reply;
                parameters[8].Value = model.replyer;
                parameters[9].Value = model.replytime;
                parameters[10].Value = model.clientip;


                DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_feedback_add", parameters);
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
        public bool Update(feedbackInfo model)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@typeid", SqlDbType.TinyInt,1),
					new SqlParameter("@title", SqlDbType.NVarChar,50),
					new SqlParameter("@cont", SqlDbType.NVarChar,200),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@addtime", SqlDbType.DateTime),
					new SqlParameter("@reply", SqlDbType.NVarChar,50),
					new SqlParameter("@replyer", SqlDbType.Int,4),
					new SqlParameter("@replytime", SqlDbType.DateTime)};
                parameters[0].Value = model.id;
                parameters[1].Value = model.userid;
                parameters[2].Value = (int)model.typeid;
                parameters[3].Value = model.title;
                parameters[4].Value = model.cont;
                parameters[5].Value = (int)model.status;
                parameters[6].Value = model.addtime;
                parameters[7].Value = model.reply;
                parameters[8].Value = model.replyer;
                parameters[9].Value = model.replytime;

                bool success = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_feedback_update", parameters) > 0;
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

                bool success = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_feedback_ChangeStatus", parameters) > 0;
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

                bool success = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_feedback_Delete", parameters) > 0;
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
        public feedbackInfo GetModel(int id)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
                parameters[0].Value = id;

                DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_feedback_GetModel", parameters);
                return GetModelFromDs(ds);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public feedbackInfo GetModel(int id,int userid)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
                    new SqlParameter("@userid", SqlDbType.Int,4)
                                            };
                parameters[0].Value = id;
                parameters[1].Value = userid;

                DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_feedback_GetModelByuser", parameters);
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
        public feedbackInfo GetCacheModel(int id)
        {
            feedbackInfo model = new feedbackInfo();

            string cacheKey = string.Format(CACHE_KEY, id);
            model = (feedbackInfo)viviapi.Cache.WebCache.GetCacheService().RetrieveObject(cacheKey);

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
        public static feedbackInfo GetModelFromDs(DataSet ds)
        {
            feedbackInfo model = new feedbackInfo();

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
                if (ds.Tables[0].Rows[0]["typeid"].ToString() != "")
                {
                    model.typeid = (feedbacktype)int.Parse(ds.Tables[0].Rows[0]["typeid"].ToString());
                }
                model.title = ds.Tables[0].Rows[0]["title"].ToString();
                model.cont = ds.Tables[0].Rows[0]["cont"].ToString();
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.status = (feedbackstatus) int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["addtime"].ToString() != "")
                {
                    model.addtime = DateTime.Parse(ds.Tables[0].Rows[0]["addtime"].ToString());
                }
                model.reply = ds.Tables[0].Rows[0]["reply"].ToString();
                if (ds.Tables[0].Rows[0]["replyer"].ToString() != "")
                {
                    model.replyer = int.Parse(ds.Tables[0].Rows[0]["replyer"].ToString());
                }
                if (ds.Tables[0].Rows[0]["replytime"].ToString() != "")
                {
                    model.replytime = DateTime.Parse(ds.Tables[0].Rows[0]["replytime"].ToString());
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
                return DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_feedback_GetList", parameters).Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
        #endregion

        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet ds = new DataSet();
            try
            {
                string tables = SQL_TABLE;
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "id desc,userid desc";
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
        private  string BuilderWhere(List<SearchParam> param, List<SqlParameter> paramList)
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