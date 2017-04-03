using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

using viviLib.ExceptionHandling;
using viviapi.Model;
using viviapi.Model.User;
using viviLib;
using DBAccess;

namespace viviapi.BLL.User
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Question
    {
        public const string CACHE_KEY = "Question";

        internal const string SQL_TABLE = "question";
        internal const string SQL_FIELDS = @"[id]
      ,[question]
      ,[release]
      ,[sort]";

        #region Add
        /// <summary>
        ///  增加一条数据
        /// </summary>
        public int Add(QuestionInfo model)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@question", SqlDbType.NVarChar,150),
					new SqlParameter("@release", SqlDbType.Bit,1),
					new SqlParameter("@sort", SqlDbType.Int,4)};
                parameters[0].Direction = ParameterDirection.Output;
                parameters[1].Value = model.question;
                parameters[2].Value = model.release;
                parameters[3].Value = model.sort;

                DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_question_ADD", parameters);
                ClearCache();
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
        ///  更新一条数据
        /// </summary>
        public bool Update(QuestionInfo model)
        {
            try
            {
                int rowsAffected = 0;
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@question", SqlDbType.NVarChar,150),
					new SqlParameter("@release", SqlDbType.Bit,1),
					new SqlParameter("@sort", SqlDbType.Int,4)};
                parameters[0].Value = model.id;
                parameters[1].Value = model.question;
                parameters[2].Value = model.release;
                parameters[3].Value = model.sort;

                rowsAffected = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_question_Update", parameters);
                if (rowsAffected > 0)
                {
                    ClearCache();
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
        #endregion

        #region Delete
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            try
            {
                int rowsAffected = 0;
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
};
                parameters[0].Value = id;

                rowsAffected = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_question_Delete", parameters);                
                if (rowsAffected > 0)
                {
                    ClearCache();
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
        #endregion

        #region GetModel
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public QuestionInfo GetModel(int id)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
};
                parameters[0].Value = id;

                QuestionInfo model = new QuestionInfo();
                DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_question_GetModel", parameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                    {
                        model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                    }
                    model.question = ds.Tables[0].Rows[0]["question"].ToString();
                    if (ds.Tables[0].Rows[0]["release"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[0]["release"].ToString() == "1") || (ds.Tables[0].Rows[0]["release"].ToString().ToLower() == "true"))
                        {
                            model.release = true;
                        }
                        else
                        {
                            model.release = false;
                        }
                    }
                    if (ds.Tables[0].Rows[0]["sort"].ToString() != "")
                    {
                        model.sort = int.Parse(ds.Tables[0].Rows[0]["sort"].ToString());
                    }
                    return model;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
        #endregion

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,question,release,sort ");
            strSql.Append(" FROM question ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DataBase.ExecuteDataset(CommandType.Text,strSql.ToString());
        }

        #region CacheData
        #region CacheData
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetCacheList()
        {
            try
            {
                string cacheKey = CACHE_KEY;

                DataSet ds = new DataSet();

                ds = (DataSet)viviapi.Cache.WebCache.GetCacheService().RetrieveObject(cacheKey);

                if (ds == null)
                {
                    SqlDependency sqlDep = DataBase.AddSqlDependency(cacheKey, SQL_TABLE, SQL_FIELDS, "", null);
                    ds = GetList("release=1");
                    viviapi.Cache.WebCache.GetCacheService().AddObject(cacheKey, ds);
                }

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
        #endregion
        #endregion

        internal static void ClearCache()
        {
            string cahcekey = CACHE_KEY;
            viviapi.Cache.WebCache.GetCacheService().RemoveObject(cahcekey);
        }
    }
}
