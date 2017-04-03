using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using DBAccess;
using viviLib;
using viviLib.ExceptionHandling;
using viviapi.Model;


namespace viviapi.BLL
{
    /// <summary>
    /// 网站配置项
    /// 1.GetWebInfoByDomain 引用缓存
    /// </summary>
    public class WebInfoFactory
    {
        public static string WEBINFO_DOMAIN_CACHEKEY = Sys.Constant.CacheMark + "WEBINFOCONFIG_{0}";
        internal static string SQL_TABLE = "webinfo";
        internal static string SQL_TABLE_FIELD = "[id],[templateID],[name],[domain],[jsqq],[kfqq],[phone],[footer],[code],[logopath],[payurl]";

        #region Add
        /// <summary>
        /// 添加新对象
        /// </summary>
        /// <param name="model"></param>
        public static void Add(WebInfo model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into webinfo(");
                strSql.Append("[id],[templateID],[name],[domain],[jsqq],[kfqq],[phone],[footer],[code],[logopath],[payurl])");
                strSql.Append(" values (");
                strSql.Append("@id,@templateID,@name,@domain,@jsqq,@kfqq,@phone,@footer,@code,@logopath,@payurl)");

                SqlParameter[] parameters = new SqlParameter[] { 
                    new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@templateID", SqlDbType.VarChar,50),
					new SqlParameter("@name", SqlDbType.VarChar,50),
					new SqlParameter("@domain", SqlDbType.VarChar,50),
					new SqlParameter("@jsqq", SqlDbType.VarChar,50),
					new SqlParameter("@kfqq", SqlDbType.VarChar,50),
					new SqlParameter("@phone", SqlDbType.VarChar,50),
					new SqlParameter("@footer", SqlDbType.VarChar,50),
					new SqlParameter("@code", SqlDbType.VarChar,50),
					new SqlParameter("@logopath", SqlDbType.VarChar,50),
					new SqlParameter("@payurl", SqlDbType.VarChar,50)};
                parameters[0].Value = model.ID;
                parameters[1].Value = model.TemplateId;
                parameters[2].Value = model.Name;
                parameters[3].Value = model.Domain;
                parameters[4].Value = model.Jsqq;
                parameters[5].Value = model.Kfqq;
                parameters[6].Value = model.Phone;
                parameters[7].Value = model.Footer;
                parameters[8].Value = model.Code;
                parameters[9].Value = model.LogoPath;
                parameters[9].Value = model.PayUrl;

                DataBase.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }
        #endregion

        #region Update
        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="_webinfo"></param>
        /// <returns></returns>
        public static bool Update(WebInfo _webinfo)
        {
            try
            {
                SqlParameter[] prams = new SqlParameter[] { 
                  DataBase.MakeInParam("@id", SqlDbType.Int, 4, _webinfo.ID)
                , DataBase.MakeInParam("@templateID", SqlDbType.VarChar, 20, _webinfo.TemplateId)
                , DataBase.MakeInParam("@name", SqlDbType.VarChar, 100, _webinfo.Name)
                , DataBase.MakeInParam("@domain", SqlDbType.VarChar, 500, _webinfo.Domain)
                , DataBase.MakeInParam("@jsqq", SqlDbType.VarChar, 300, _webinfo.Jsqq)
                , DataBase.MakeInParam("@kfqq", SqlDbType.VarChar, 300, _webinfo.Kfqq)
                , DataBase.MakeInParam("@phone", SqlDbType.VarChar, 50, _webinfo.Phone)
                , DataBase.MakeInParam("@footer", SqlDbType.VarChar, 500, _webinfo.Footer)
                , DataBase.MakeInParam("@code", SqlDbType.VarChar, 500, _webinfo.Code)
                , DataBase.MakeInParam("@logopath", SqlDbType.VarChar, 500, _webinfo.LogoPath)
                , DataBase.MakeInParam("@payurl", SqlDbType.VarChar, 500, _webinfo.PayUrl)           

                , DataBase.MakeInParam("@apibankname", SqlDbType.VarChar, 100, _webinfo.apibankname) 
                , DataBase.MakeInParam("@apibankversion", SqlDbType.VarChar, 20, _webinfo.apibankversion) 
                , DataBase.MakeInParam("@apicardname", SqlDbType.VarChar, 100, _webinfo.apicardname) 
                , DataBase.MakeInParam("@apicardversion", SqlDbType.VarChar, 20, _webinfo.apicardversion) 
            };

                bool succ = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_webinfo_Update", prams) > 0;
                if (succ == true)
                    ClearCache(HttpContext.Current.Request.Url.Host);
                return succ;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }
        #endregion

        #region GetModel
        #region GetWebInfoById
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static WebInfo GetWebInfoById(int id)
        {
            try
            {
                WebInfo _webinfo = new WebInfo();

                string sqlstr = @"SELECT [id]
      ,[templateID]
      ,[name]
      ,[domain]
      ,[jsqq]
      ,[kfqq]
      ,[phone]
      ,[footer]
      ,[code]
      ,[logopath]
      ,[payurl]
      ,[apibankname]
      ,[apibankversion]
      ,[apicardname]
      ,[apicardversion]
  FROM [webinfo] WHERE [id]=@id";

                SqlParameter[] prams = new SqlParameter[] { DataBase.MakeInParam("@id", SqlDbType.Int, 4, id) };

                SqlDataReader dr = DataBase.ExecuteReader(CommandType.Text, sqlstr, prams);
                _webinfo = GetObjectFromDR(dr);
                return _webinfo;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
        #endregion

        #region GetWebInfoByDomain
        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static WebInfo GetCacheWebInfoByDomain(string domain)
        {
            string cacheKey = string.Format(WEBINFO_DOMAIN_CACHEKEY, domain);

            WebInfo _webinfo = new WebInfo();
            _webinfo = (WebInfo)viviapi.Cache.WebCache.GetCacheService().RetrieveObject(cacheKey);

            if (_webinfo == null)
            {
                IDictionary<string, object> sqldepparms = new Dictionary<string, object>();
                sqldepparms.Add("domain", domain);
                SqlDependency sqlDep = DataBase.AddSqlDependency(cacheKey, SQL_TABLE, SQL_TABLE_FIELD, "[domain]=@domain", sqldepparms);

                _webinfo = GetWebInfoByDomain(domain);
                if (_webinfo == null)
                    return null;
                viviapi.Cache.WebCache.GetCacheService().AddObject(cacheKey, _webinfo);
            }
            return _webinfo;
        }
        /// <summary>
        /// 根据域名取得网站的配置值
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static WebInfo GetWebInfoByDomain(string domain)
        {
            try
            {
                string sqlstr = @"DECLARE @TempID int
SELECT @TempID = [id] FROM [webinfo] WHERE [domain]=@domain
IF(@TempID IS NULL)
SELECT @TempID = 1

SELECT [id]
      ,[templateID]
      ,[name]
      ,[domain]
      ,[jsqq]
      ,[kfqq]
      ,[phone]
      ,[footer]
      ,[code]
      ,[logopath]
      ,[payurl] ,[apibankname]
      ,[apibankversion]
      ,[apicardname]
      ,[apicardversion]
FROM [dbo].[webinfo] WHERE [id] = @TempID ";

                SqlParameter[] prams = new SqlParameter[] { DataBase.MakeInParam("@domain", SqlDbType.VarChar, 50, domain) };

                SqlDataReader dr = DataBase.ExecuteReader(CommandType.Text, sqlstr, prams);
                WebInfo _webinfo = GetObjectFromDR(dr);


                return _webinfo;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
        #endregion

        #region GetObjectFromDR
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlDR"></param>
        /// <returns></returns>
        private static WebInfo GetObjectFromDR(SqlDataReader dr)
        {
            if (dr == null)
                return null;

            WebInfo _webinfo = new WebInfo(); 
            if (dr.Read())
            {
                _webinfo.ID = (int)dr["id"];
                _webinfo.TemplateId = dr["templateID"].ToString();
                _webinfo.Name = dr["name"].ToString();
                _webinfo.Domain = dr["domain"].ToString();
                _webinfo.Jsqq = dr["jsqq"].ToString();
                _webinfo.Kfqq = dr["kfqq"].ToString();
                _webinfo.Phone = dr["phone"].ToString();
                _webinfo.Footer = dr["footer"].ToString();
                _webinfo.Code = dr["code"].ToString();
                _webinfo.LogoPath = dr["logopath"].ToString();
                _webinfo.PayUrl = dr["payurl"].ToString();

                _webinfo.apibankname = dr["apibankname"].ToString();
                _webinfo.apibankversion = dr["apibankversion"].ToString();
                _webinfo.apicardname = dr["apicardname"].ToString();
                _webinfo.apicardversion = dr["apicardversion"].ToString();
            }
            dr.Close();

            return _webinfo;
        }
        #endregion

        public static String GetAgent_Payrate_Setconfig()
        {
            string sqlText = "select top 1 isnull(agentpayratesetconfig,'') from webinfo where id = 1";

           object config = DataBase.ExecuteScalar(CommandType.Text, sqlText);

           return Convert.ToString(config);

        }
        public static bool SetAgent_Payrate_Setconfig(string config)
        {
            string sqlText = "update webinfo set agentpayratesetconfig=@agentpayratesetconfig where id = 1";
            SqlParameter[] prams = new SqlParameter[] { DataBase.MakeInParam("@agentpayratesetconfig", SqlDbType.VarChar, 4000, config) };
            return DataBase.ExecuteNonQuery(CommandType.Text, sqlText, prams) > 0;

        }
        #endregion

        #region GetList
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataTable GetList(string where)
        {
            try
            {
                string sql = @"SELECT [id]
      ,[templateID]
      ,[name]
      ,[domain]
      ,[jsqq]
      ,[kfqq]
      ,[phone]
      ,[footer]
      ,[code]
      ,[logopath]
      ,[payurl] ,[apibankname]
      ,[apibankversion]
      ,[apicardname]
      ,[apicardversion]
  FROM [webinfo] ";

                return DataBase.ExecuteDataset(CommandType.Text, sql).Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
        #endregion

        static void ClearCache(string domain)
        {
            string cacheKey = string.Format(WEBINFO_DOMAIN_CACHEKEY, domain);
            viviapi.Cache.WebCache.GetCacheService().RemoveObject(cacheKey);
        }

        /// <summary>
        /// 当前网站的配置信息
        /// </summary>
        public static WebInfo CurrentWebInfo
        {
            get
            {
                return GetCacheWebInfoByDomain(HttpContext.Current.Request.Url.Host);
            }
        }
    }
}

