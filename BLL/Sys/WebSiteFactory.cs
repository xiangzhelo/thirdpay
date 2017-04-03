using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using viviapi.Model;
using viviLib;
using DBAccess;
///
namespace viviapi.BLL
{
   
    public class WebSiteFactory
    {
        public static bool AddSite(WebSiteInfo _websiteinfo)
        {
            SqlParameter[] prams = new SqlParameter[] { DataBase.MakeInParam("@SiteName", SqlDbType.VarChar, 100, _websiteinfo.SiteName), DataBase.MakeInParam("@Domain", SqlDbType.VarChar, 100, _websiteinfo.Domain), DataBase.MakeInParam("@Description", SqlDbType.VarChar, 500, _websiteinfo.Description), DataBase.MakeInParam("@SiteType", SqlDbType.Int, 4, _websiteinfo.SiteType), DataBase.MakeInParam("@AddTime", SqlDbType.DateTime, 8, _websiteinfo.AddTime), DataBase.MakeInParam("@Status", SqlDbType.Int, 4, _websiteinfo.Status), DataBase.MakeInParam("@Uid", SqlDbType.Int, 4, _websiteinfo.Uid) };
            return (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "WebSite_Add", prams) > 0);
        }

        public static List<WebSiteInfo> GetListArray(int uid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,SiteName,Domain,Description,SiteType,AddTime,Status,Uid ");
            strSql.Append(" FROM WebSite WHERE Uid=" + uid);
            List<WebSiteInfo> list = new List<WebSiteInfo>();
            using (SqlDataReader dataReader = DataBase.ExecuteReader(CommandType.Text, strSql.ToString()))
            {
                while (dataReader.Read())
                {
                    list.Add(ReaderBind(dataReader));
                }
            }
            return list;
        }

        //public static List<WebSiteInfo> GetListArray(int uid, string strWhere)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select ID,SiteName,Domain,Description,SiteType,AddTime,Status,Uid ");
        //    strSql.Append(" FROM WebSite WHERE Uid=" + uid);
        //    if (strWhere.Trim() != "")
        //    {
        //        strSql.Append(" " + strWhere);
        //    }
        //    List<WebSiteInfo> list = new List<WebSiteInfo>();
        //    using (SqlDataReader dataReader = Database.ExecuteReader(CommandType.Text, strSql.ToString()))
        //    {
        //        while (dataReader.Read())
        //        {
        //            list.Add(ReaderBind(dataReader));
        //        }
        //    }
        //    return list;
        //}

        public static WebSiteInfo GetModel(int id)
        {
            SqlParameter[] prams = new SqlParameter[] { DataBase.MakeInParam("@ID", SqlDbType.Int, 4, id) };
            WebSiteInfo model = null;
            using (SqlDataReader dataReader = DataBase.ExecuteReader(CommandType.StoredProcedure, "UP_WebSite_GetModel", prams))
            {
                if (dataReader.Read())
                {
                    model = ReaderBind(dataReader);
                }
            }
            return model;
        }

        public static DataTable GetMySiteList(int uid)
        {
            string sqlstr = "SELECT * FROM [WebSite] WHERE [Uid]=" + uid;
            return DataBase.ExecuteDataset(CommandType.Text, sqlstr).Tables[0];
        }

        //public static int GetWebSiteIdByDomain(string domain)
        //{
        //    int _id = 0;
        //    string slqstr = string.Concat(new object[] { "SELECT [ID] FROM [WebSite] WHERE Status=", 1, " AND [Domain]='", domain, "'" });
        //    SqlDataReader dr = Database.ExecuteReader(CommandType.Text, slqstr);
        //    if (dr.Read())
        //    {
        //        _id = (int) dr["ID"];
        //    }
        //    dr.Close();
        //    dr.Dispose();
        //    return _id;
        //}

        public static WebSiteInfo GetWebSiteInfoByDomain(string domain)
        {
            WebSiteInfo _websiteinfo = new WebSiteInfo();
            if (HttpContext.Current.Cache[domain] != null)
            {
                return (WebSiteInfo) HttpContext.Current.Cache.Get(domain);
            }
            string slqstr = string.Concat(new object[] { "SELECT * FROM [WebSite] WHERE Status=", 1, " AND [Domain]='", domain, "'" });
            SqlDataReader dr = DataBase.ExecuteReader(CommandType.Text, slqstr);
            if (dr.Read())
            {
                _websiteinfo.ID = (int) dr["ID"];
                _websiteinfo.SiteName = dr["SiteName"].ToString();
                _websiteinfo.Domain = dr["Domain"].ToString();
                _websiteinfo.Description = dr["Description"].ToString();
                _websiteinfo.SiteType = (int) dr["SiteType"];
                _websiteinfo.AddTime = DateTime.Parse(dr["AddTime"].ToString());
                _websiteinfo.Status = (int) dr["Status"];
                _websiteinfo.Uid = (int) dr["Uid"];
            }
            dr.Close();
            dr.Dispose();
            HttpContext.Current.Cache.Insert(domain, _websiteinfo);
            return _websiteinfo;
        }

        public static WebSiteInfo GetWebSiteInfoById(int id)
        {
            WebSiteInfo _websiteinfo = new WebSiteInfo();
            if (HttpContext.Current.Cache["websiteinfo" + id.ToString()] != null)
            {
                return (WebSiteInfo) HttpContext.Current.Cache.Get("websiteinfo" + id.ToString());
            }
            string slqstr = "SELECT * FROM [WebSite] WHERE [ID]=" + id;
            SqlDataReader dr = DataBase.ExecuteReader(CommandType.Text, slqstr);
            if (dr.Read())
            {
                _websiteinfo.ID = (int) dr["ID"];
                _websiteinfo.SiteName = dr["SiteName"].ToString();
                _websiteinfo.Domain = dr["Domain"].ToString();
                _websiteinfo.Description = dr["Description"].ToString();
                _websiteinfo.SiteType = (int) dr["SiteType"];
                _websiteinfo.AddTime = DateTime.Parse(dr["AddTime"].ToString());
                _websiteinfo.Status = (int) dr["Status"];
                _websiteinfo.Uid = (int) dr["Uid"];
            }
            dr.Close();
            HttpContext.Current.Cache.Insert("websiteinfo" + id.ToString(), _websiteinfo);
            return _websiteinfo;
        }

        public static WebSiteInfo ReaderBind(SqlDataReader dataReader)
        {
            WebSiteInfo model = new WebSiteInfo();
            object ojb = dataReader["ID"];
            if ((ojb != null) && (ojb != DBNull.Value))
            {
                model.ID = (int) ojb;
            }
            model.SiteName = dataReader["SiteName"].ToString();
            model.Domain = dataReader["Domain"].ToString();
            model.Description = dataReader["Description"].ToString();
            ojb = dataReader["SiteType"];
            if ((ojb != null) && (ojb != DBNull.Value))
            {
                model.SiteType = (int) ojb;
            }
            ojb = dataReader["AddTime"];
            if ((ojb != null) && (ojb != DBNull.Value))
            {
                model.AddTime = (DateTime) ojb;
            }
            ojb = dataReader["Status"];
            if ((ojb != null) && (ojb != DBNull.Value))
            {
                model.Status = (int) ojb;
            }
            ojb = dataReader["Uid"];
            if ((ojb != null) && (ojb != DBNull.Value))
            {
                model.Uid = (int) ojb;
            }
            return model;
        }

        public static bool UpdateWebSite(WebSiteInfo _websiteinfo)
        {
            HttpContext.Current.Cache.Remove("websiteinfo" + _websiteinfo.ID.ToString());
            HttpContext.Current.Cache.Remove(_websiteinfo.Domain);
            SqlParameter[] prams = new SqlParameter[] { DataBase.MakeInParam("@ID", SqlDbType.Int, 4, _websiteinfo.ID), DataBase.MakeInParam("@SiteName", SqlDbType.VarChar, 100, _websiteinfo.SiteName), DataBase.MakeInParam("@Domain", SqlDbType.VarChar, 100, _websiteinfo.Domain), DataBase.MakeInParam("@Description", SqlDbType.VarChar, 500, _websiteinfo.Description), DataBase.MakeInParam("@SiteType", SqlDbType.Int, 4, _websiteinfo.SiteType), DataBase.MakeInParam("@AddTime", SqlDbType.DateTime, 8, _websiteinfo.AddTime), DataBase.MakeInParam("@Status", SqlDbType.Int, 4, _websiteinfo.Status), DataBase.MakeInParam("@Uid", SqlDbType.Int, 4, _websiteinfo.Uid) };
            return (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "WebSite_Update", prams) > 0);
        }
    }
}

