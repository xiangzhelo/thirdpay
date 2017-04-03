using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBAccess;
using viviapi.Model.Sys;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.DAL.Sys
{
    /// <summary>
    /// 加款
    /// </summary>
    public sealed class Debuglog
    {
        internal const string SqlTable = "v_debuginfo";
        internal const string SqlTableFIELDS = @"id,bugtype,userid,userName,url,errorcode,errorinfo,detail,addtime,userorder";

        #region Delete
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            var strSql = new StringBuilder();
            strSql.Append("delete from debuginfo ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region DeleteList
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            var strSql = new StringBuilder();
            strSql.Append("delete from debuginfo ");
            strSql.Append(" where id in (" + idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Insert
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Insert(viviapi.Model.Sys.debuginfo model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@bugtype", SqlDbType.TinyInt,1),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@url", SqlDbType.VarChar,2000),
					new SqlParameter("@errorcode", SqlDbType.VarChar,50),
					new SqlParameter("@errorinfo", SqlDbType.VarChar,200),
					new SqlParameter("@detail", SqlDbType.VarChar,500),
					new SqlParameter("@addtime", SqlDbType.DateTime),
                    new SqlParameter("@userorder",SqlDbType.VarChar,30)};
            parameters[0].Value = model.id;
            parameters[1].Value = (int)model.bugtype;
            parameters[2].Value = model.userid;
            parameters[3].Value = model.url;
            parameters[4].Value = model.errorcode;
            parameters[5].Value = model.errorinfo;
            parameters[6].Value = model.detail;
            parameters[7].Value = model.addtime;
            parameters[8].Value = model.userorder;

            object result = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_debuginfo_Insert", parameters);
            if (result != null)
            {
                return Convert.ToInt32(result);
            }
            return 0;
        }
        #endregion

        #region GetModel
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public debuginfo GetModel(int id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
};
            parameters[0].Value = id;

            debuginfo model = new debuginfo();
            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_debuginfo_GetModel", parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["bugtype"].ToString() != "")
                {
                    model.bugtype = (debugtypeenum)int.Parse(ds.Tables[0].Rows[0]["bugtype"].ToString());
                }
                if (ds.Tables[0].Rows[0]["userid"].ToString() != "")
                {
                    model.userid = int.Parse(ds.Tables[0].Rows[0]["userid"].ToString());
                }
                model.url = ds.Tables[0].Rows[0]["url"].ToString();
                model.errorcode = ds.Tables[0].Rows[0]["errorcode"].ToString();
                model.errorinfo = ds.Tables[0].Rows[0]["errorinfo"].ToString();
                model.detail = ds.Tables[0].Rows[0]["detail"].ToString();
                model.userorder = ds.Tables[0].Rows[0]["userorder"].ToString();
                if (ds.Tables[0].Rows[0]["addtime"].ToString() != "")
                {
                    model.addtime = DateTime.Parse(ds.Tables[0].Rows[0]["addtime"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region PageSearch
        /// <summary>
        /// 根据搜索条件返回指定分页的商户信息。
        /// </summary>
        /// <param name="searchParams">搜索条件数组。</param>
        /// <param name="pageSize">分页大小。</param>
        /// <param name="page">页码。</param>
        /// <param name="orderby">排序方式。</param>
        /// <returns>分页数据。</returns>
        public  DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet ds = new DataSet();
            try
            {
                string tables = SqlTable;
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "id desc";
                }

                List<SqlParameter> paramList = new List<SqlParameter>();
                string userSearchWhere = BuilderWhere(searchParams, paramList);

                string sql = SqlHelper.GetCountSQL(tables, userSearchWhere, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL(SqlTableFIELDS, tables, userSearchWhere, orderby, key, pageSize, page, false);
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
                        case "userorder":
                            builder.Append(" AND [userorder] like @userorder");
                            parameter = new SqlParameter("@userorder", SqlDbType.VarChar, 30);
                            parameter.Value = "%" + iparam.ParamValue.ToString() + "%";
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
        #endregion
    }
}
