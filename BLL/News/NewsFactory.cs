using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBAccess;
using viviapi.Model;
using viviapi.Model.News;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.News
{
    /// <summary>
    /// 
    /// </summary>
    public class NewsFactory
    {
        public static string NEWS_CACHE_KEY = Sys.Constant.CacheMark + "NEWS";
        //internal const string NEWS_CACHE_KEY = "{{7D0E57D0-EA0A-4279-9549-1E97CB290760}}_{0}";
        //internal const string NEWS_CACHE_KEY2 = "{{87ED9CA7-25A5-449e-959F-B81828C59A56}}_{0}";
        //internal const string NEWS_CACHE_KEY3 = "NEWS3_{05C479AC-327B-44e4-B63C-8270A03AF554}";
        internal const string SQL_TABLE = "news";
        internal const string FIELD_NEWS = "[newsid],[newstype],[newstitle],[addTime],[newscontent],[IsRed],[IsTop],[IsPop],[Isbold],[Color],[release]";
        internal const string FIELD_NEWS1 = "[newsid],[newstype],[newstitle],[release]";

        #region Add
        /// <summary>
        ///  增加一条数据
        /// </summary>
        public static int Add(NewsInfo model)
        {
            try
            {
                int rowsAffected;
                SqlParameter[] parameters = {
					new SqlParameter("@newsid", SqlDbType.Int,4),
					new SqlParameter("@newstype", SqlDbType.TinyInt,1),
					new SqlParameter("@newstitle", SqlDbType.NVarChar,50),
					new SqlParameter("@addTime", SqlDbType.DateTime),
					new SqlParameter("@newscontent", SqlDbType.Text),
					new SqlParameter("@IsRed", SqlDbType.TinyInt,1),
					new SqlParameter("@IsTop", SqlDbType.TinyInt,1),
					new SqlParameter("@IsPop", SqlDbType.TinyInt,1),
					new SqlParameter("@Isbold", SqlDbType.TinyInt,1),
					new SqlParameter("@Color", SqlDbType.VarChar,20),
                    new SqlParameter("@release", SqlDbType.Bit)};
                parameters[0].Direction = ParameterDirection.Output;
                parameters[1].Value = (int)model.newstype;
                parameters[2].Value = model.newstitle;
                parameters[3].Value = model.addTime;
                parameters[4].Value = model.newscontent;
                parameters[5].Value = model.IsRed;
                parameters[6].Value = model.IsTop;
                parameters[7].Value = model.IsPop;
                parameters[8].Value = model.Isbold;
                parameters[9].Value = model.Color;
                parameters[10].Value = model.release;

                rowsAffected = DataBase.ExecuteNonQuery( CommandType.StoredProcedure,"proc_news_add", parameters);
                int id = (int)parameters[0].Value;
                if (id > 0)
                {
                    ClearCache();
                }
                return id;
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
        public static bool Update(NewsInfo model)
        {
            try
            {
                int rowsAffected;
                SqlParameter[] parameters = {
					new SqlParameter("@newsid", SqlDbType.Int,4),
					new SqlParameter("@newstype", SqlDbType.TinyInt,1),
					new SqlParameter("@newstitle", SqlDbType.NVarChar,50),
					new SqlParameter("@addTime", SqlDbType.DateTime),
					new SqlParameter("@newscontent", SqlDbType.Text),
					new SqlParameter("@IsRed", SqlDbType.TinyInt,1),
					new SqlParameter("@IsTop", SqlDbType.TinyInt,1),
					new SqlParameter("@IsPop", SqlDbType.TinyInt,1),
					new SqlParameter("@Isbold", SqlDbType.TinyInt,1),
					new SqlParameter("@Color", SqlDbType.VarChar,20),
                    new SqlParameter("@release", SqlDbType.Bit)};
                parameters[0].Value = model.newsid;
                parameters[1].Value = (int)model.newstype;
                parameters[2].Value = model.newstitle;
                parameters[3].Value = model.addTime;
                parameters[4].Value = model.newscontent;
                parameters[5].Value = model.IsRed;
                parameters[6].Value = model.IsTop;
                parameters[7].Value = model.IsPop;
                parameters[8].Value = model.Isbold;
                parameters[9].Value = model.Color;
                parameters[10].Value = model.release;

                rowsAffected = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_news_Update", parameters);

                if (rowsAffected > 0)
                {
                    ClearCache();
                    return true;
                }
                return false;
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
        public static bool Delete(int newsid)
        {
            try
            {
                int rowsAffected = 0;
                SqlParameter[] parameters = {
					new SqlParameter("@newsid", SqlDbType.Int,4)
};
                parameters[0].Value = newsid;

                rowsAffected = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_news_del", parameters);
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
        /// 
        /// </summary>
        /// <param name="newsid"></param>
        /// <returns></returns>
        public static NewsInfo GetModel(int newsid)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@newsid", SqlDbType.Int,4)
};
                parameters[0].Value = newsid;

                NewsInfo model = new NewsInfo();
                DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_news_GetModel", parameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return GetModelFromDR(ds.Tables[0].Rows[0]);
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

        public static NewsInfo GetModelFromDR(DataRow DR)
        {
            if (DR == null)
                return null;

            NewsInfo model = new NewsInfo();

            if (DR["newsid"].ToString() != "")
            {
                model.newsid = int.Parse(DR["newsid"].ToString());
            }
            if (DR["newstype"].ToString() != "")
            {
                model.newstype = (NewsType)int.Parse(DR["newstype"].ToString());
            }
            model.newstitle = DR["newstitle"].ToString();
            if (DR["addTime"].ToString() != "")
            {
                model.addTime = DateTime.Parse(DR["addTime"].ToString());
            }
            model.newscontent = DR["newscontent"].ToString();
            if (DR["IsRed"].ToString() != "")
            {
                model.IsRed = int.Parse(DR["IsRed"].ToString());
            }
            if (DR["IsTop"].ToString() != "")
            {
                model.IsTop = int.Parse(DR["IsTop"].ToString());
            }
            if (DR["IsPop"].ToString() != "")
            {
                model.IsPop = int.Parse(DR["IsPop"].ToString());
            }
            if (DR["Isbold"].ToString() != "")
            {
                model.Isbold = int.Parse(DR["Isbold"].ToString());
            }
            if (DR["release"].ToString() != "")
            {
                model.release = bool.Parse(DR["release"].ToString());
            }
            model.Color = DR["Color"].ToString();
            return model;
        }
        #endregion

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [newsid],[newstype],[newstitle],[addTime],[newscontent],[IsRed],[IsTop],[IsPop],[Isbold],[Color],[release]");
            strSql.Append(" FROM news ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DataBase.ExecuteDataset(CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="startIndex"></param>
        /// <param name="maxIndex"></param>
        /// <returns></returns>
        public static DataSet GetList(int newstype, int startIndex, int maxIndex)
        {
            string sql = string.Format(@"with t as(
select row_number() over(order by newsid Desc) as rowNum,*
from news where newstype={0})
select * from t where rowNum between {1} and {2}", newstype, startIndex, maxIndex);

            return DataBase.ExecuteDataset(CommandType.Text, sql);

        }
        public static DataTable GetReleaseNews()
        {
            string cacheKey = NEWS_CACHE_KEY;
            DataTable data = (DataTable)viviapi.Cache.WebCache.GetCacheService().RetrieveObject(cacheKey);
            if (data == null)
            {                
                IDictionary<string, object> sqldepparms = new Dictionary<string, object>();
                sqldepparms.Add("@release", 1);
                SqlDependency sqlDep = DataBase.AddSqlDependency(cacheKey, SQL_TABLE, FIELD_NEWS, "[release]=@release", sqldepparms);    
           
                data = GetList("[release]=1 order by isTop desc,addtime desc").Tables[0];
                viviapi.Cache.WebCache.GetCacheService().AddObject(cacheKey, data);
            }
            return data;
        }

        #region GetCacheList
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newstype"></param>
        /// <param name="startIndex"></param>
        /// <param name="maxIndex"></param>
        /// <returns></returns>
        public static List<NewsInfo> GetCacheList(int newstype, int startIndex, int maxIndex)
        {
            if (maxIndex < 0 || maxIndex < startIndex)
                return null;

            DataTable data = GetReleaseNews();            
            if (data != null)
            {
                List<NewsInfo> list = new List<NewsInfo>();

                int count = 0;
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    if (int.Parse(data.Rows[i]["newstype"].ToString()) == newstype)
                    {
                        count++;

                        if (count >= startIndex && count <= maxIndex)
                        {
                            list.Add(GetModelFromDR(data.Rows[i]));
                        }
                        if (count > maxIndex)
                        {
                            break;
                        }
                    }
                }
                return list;
            }
            return null;
        }
        #endregion

        #region GetCacheModel
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newsid"></param>
        /// <returns></returns>
        public static NewsInfo GetCacheModel(int newsid)
        {
            DataTable data = GetReleaseNews();
            if (data == null)
                return null;
            DataRow[] result = data.Select(" newsid="+newsid.ToString());
            if (result == null || result.Length <= 0)
            {
                return null;
            }
            return GetModelFromDR(result[0]);
        }       
        #endregion

        #region GetCacheTipsNews
        /// <summary>
        /// 取得弹出公告
        /// </summary>
        /// <param name="newstype"></param>
        /// <param name="startIndex"></param>
        /// <param name="maxIndex"></param>
        /// <returns></returns>
        public static List<int> GetCacheTipsNews()
        {
            DataTable data = GetReleaseNews();
            if (data == null)
                return null;

            List<int> list = new List<int>();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (int.Parse(data.Rows[i]["newstype"].ToString()) == 2 && int.Parse(data.Rows[i]["IsPop"].ToString()) == 1)
                {
                    list.Add(int.Parse(data.Rows[i]["newsid"].ToString()));
                }
            }
            return list;
        }
        #endregion

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
                string key = "[newsid]";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "addTime desc";
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
                        case "newstype":
                            builder.Append(" AND [newstype] = @newstype");
                            parameter = new SqlParameter("@newstype", SqlDbType.Int);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "newstitle":
                            builder.Append(" AND [newstitle] like @newstitle");
                            parameter = new SqlParameter("@newstitle", SqlDbType.VarChar, 100);
                            parameter.Value = "%" + SqlHelper.CleanString((string)iparam.ParamValue, 100) + "%";
                            paramList.Add(parameter);
                            break;
                    }
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeid"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static DataTable GetNewsList(int typeid, int pagesize, int page)
        {
            int sbet = (page * pagesize) + 1;
            int ebet = (page * pagesize) + pagesize;

            string swhere = string.Empty;
            if (typeid > 0)
            {
                swhere = "and newstype=" + typeid;
            }
            string sqlStr = string.Concat(new object[] { "SELECT * FROM \r\n\t(SELECT *,ROW_NUMBER() OVER(ORDER BY News.NewsId DESC) AS RW  FROM [News] WHERE 1=1 ", swhere, " )TG WHERE TG.RW between ", sbet, " AND ", ebet, " ORDER BY istop desc,newsid desc" });
            
            return DataBase.ExecuteDataset(CommandType.Text, sqlStr).Tables[0];
        }

        //清理缓存 
        static void ClearCache()
        {
            string cacheKey = NEWS_CACHE_KEY;
            viviapi.Cache.WebCache.GetCacheService().RemoveObject(cacheKey);

            //cacheKey = Channel.CHANEL_CACHEKEY;
            //viviapi.Cache.WebCache.GetCacheService().RemoveObject(cacheKey);
        }
    }
}

