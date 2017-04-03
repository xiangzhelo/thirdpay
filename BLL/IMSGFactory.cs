using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using viviLib;
using DBAccess;
using viviapi.Model;
using viviLib.ExceptionHandling;
using viviLib.Data;
namespace viviapi.BLL
{
    /// <summary>
    /// 数据访问类:Msg
    /// </summary>
    public partial class IMSGFactory
    {
        internal const string SQL_TABLE = "msg";
        //money,pay,nopay,Integral
        internal const string SQL_TABLE_FIELD = @"[ID]
      ,[msg_from]
      ,[msg_to]
      ,[msg_content]
      ,[msg_addtime]
      ,[msg_title]
      ,[isRead],[msg_fromname]";


        public IMSGFactory()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Msg");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            object result = DataBase.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters);

            if (result == null || result == DBNull.Value)
                return false;
            if (int.Parse(result.ToString()) <= 0)
                return false;
            return true;
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(IMSG model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Msg(");
            strSql.Append("msg_from,msg_to,msg_content,msg_addtime,msg_title,msg_fromname)");
            strSql.Append(" values (");
            strSql.Append("@msg_from,@msg_to,@msg_content,@msg_addtime,@msg_title,@msg_fromname)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@msg_from", SqlDbType.Int,4),
					new SqlParameter("@msg_to", SqlDbType.Int,4),
					new SqlParameter("@msg_content", SqlDbType.NVarChar,2000),
					new SqlParameter("@msg_addtime", SqlDbType.DateTime),
					new SqlParameter("@msg_title", SqlDbType.NVarChar,50),
                                        new SqlParameter("@msg_fromname", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.msg_from;
            parameters[1].Value = model.msg_to;
            parameters[2].Value = model.msg_content;
            parameters[3].Value = model.msg_addtime;
            parameters[4].Value = model.msg_title;
            parameters[5].Value = model.msg_fromname;

            object obj = DataBase.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(IMSG model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Msg set ");
            strSql.Append("msg_from=@msg_from,");
            strSql.Append("msg_to=@msg_to,");
            strSql.Append("msg_content=@msg_content,");
            strSql.Append("msg_addtime=@msg_addtime,");
            strSql.Append("msg_title=@msg_title,");
            strSql.Append("isRead=@isRead");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@msg_from", SqlDbType.Int,4),
					new SqlParameter("@msg_to", SqlDbType.Int,4),
					new SqlParameter("@msg_content", SqlDbType.NVarChar,2000),
					new SqlParameter("@msg_addtime", SqlDbType.DateTime),
					new SqlParameter("@msg_title", SqlDbType.NVarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4),
            new SqlParameter("@isRead", SqlDbType.Bit,1)};
            parameters[0].Value = model.msg_from;
            parameters[1].Value = model.msg_to;
            parameters[2].Value = model.msg_content;
            parameters[3].Value = model.msg_addtime;
            parameters[4].Value = model.msg_title;
            parameters[5].Value = model.ID;
            parameters[6].Value = model.isRead;

            int rows = DataBase.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsRead(int uid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Msg");
            strSql.Append(" where msg_to=@ID and (isRead is null or isRead = 0) ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = uid;

            object result = DataBase.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters);

            if (result == null || result == DBNull.Value)
                return false;
            if (int.Parse(result.ToString()) <= 0)
                return false;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int GetUserMsgCount(int userId)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.Int,4)
};
                parameters[0].Value = userId;

                return Convert.ToInt32(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_msg_getusercount", parameters));
            }
            catch (Exception ex)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Msg ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
};
            parameters[0].Value = ID;

            int rows = DataBase.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool DeleteList(string IDlist)
        {
            //参数检查
            string ids = string.Empty;

            foreach (string id in IDlist.Split(','))
            {
                int temp = 0;
                if (int.TryParse(id, out temp))
                {
                    ids += id + ",";
                }
            }
            if (!string.IsNullOrEmpty(ids))
            {
                ids = ids.Substring(0,ids.Length - 1);
            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Msg ");
            strSql.Append(" where ID in (" + ids + ")  ");
            int rows = DataBase.ExecuteNonQuery(CommandType.Text, strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static IMSG GetModel(int ID,int msg_to)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,msg_from,msg_to,msg_content,msg_addtime,msg_title from Msg ");
            strSql.Append(" where ID=@ID and msg_to  = @msg_to");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
                    new SqlParameter("@msg_to", SqlDbType.Int,4)
};
            parameters[0].Value = ID;
            parameters[1].Value = msg_to;

            IMSG model = new IMSG();
            DataSet ds = DataBase.ExecuteDataset(CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["msg_from"].ToString() != "")
                {
                    model.msg_from = int.Parse(ds.Tables[0].Rows[0]["msg_from"].ToString());
                }
                if (ds.Tables[0].Rows[0]["msg_to"].ToString() != "")
                {
                    model.msg_to = int.Parse(ds.Tables[0].Rows[0]["msg_to"].ToString());
                }
                model.msg_content = ds.Tables[0].Rows[0]["msg_content"].ToString();
                if (ds.Tables[0].Rows[0]["msg_addtime"].ToString() != "")
                {
                    model.msg_addtime = DateTime.Parse(ds.Tables[0].Rows[0]["msg_addtime"].ToString());
                }
                model.msg_title = ds.Tables[0].Rows[0]["msg_title"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static IMSG GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,msg_from,msg_to,msg_content,msg_addtime,msg_title from Msg ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
};
            parameters[0].Value = ID;

            IMSG model = new IMSG();
            DataSet ds = DataBase.ExecuteDataset(CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["msg_from"].ToString() != "")
                {
                    model.msg_from = int.Parse(ds.Tables[0].Rows[0]["msg_from"].ToString());
                }
                if (ds.Tables[0].Rows[0]["msg_to"].ToString() != "")
                {
                    model.msg_to = int.Parse(ds.Tables[0].Rows[0]["msg_to"].ToString());
                }
                model.msg_content = ds.Tables[0].Rows[0]["msg_content"].ToString();
                if (ds.Tables[0].Rows[0]["msg_addtime"].ToString() != "")
                {
                    model.msg_addtime = DateTime.Parse(ds.Tables[0].Rows[0]["msg_addtime"].ToString());
                }
                model.msg_title = ds.Tables[0].Rows[0]["msg_title"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static IMSG GetModelByTo(int msg_to)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,msg_from,msg_to,msg_content,msg_addtime,msg_title from Msg ");
            strSql.Append(" where msg_to=@msg_to");
            SqlParameter[] parameters = {
					new SqlParameter("@msg_to", SqlDbType.Int,4)
};
            parameters[0].Value = msg_to;

            IMSG model = new IMSG();
            DataSet ds = DataBase.ExecuteDataset(CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["msg_from"].ToString() != "")
                {
                    model.msg_from = int.Parse(ds.Tables[0].Rows[0]["msg_from"].ToString());
                }
                if (ds.Tables[0].Rows[0]["msg_to"].ToString() != "")
                {
                    model.msg_to = int.Parse(ds.Tables[0].Rows[0]["msg_to"].ToString());
                }
                model.msg_content = ds.Tables[0].Rows[0]["msg_content"].ToString();
                if (ds.Tables[0].Rows[0]["msg_addtime"].ToString() != "")
                {
                    model.msg_addtime = DateTime.Parse(ds.Tables[0].Rows[0]["msg_addtime"].ToString());
                }
                model.msg_title = ds.Tables[0].Rows[0]["msg_title"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }

        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        //public static DataSet GetList(string strWhere)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select ID,msg_from,msg_to,msg_content,msg_addtime,msg_title ");
        //    strSql.Append(" FROM Msg ");
        //    if (strWhere.Trim() != "")
        //    {
        //        strSql.Append(" where " + strWhere);
        //    }
        //    return Database.ExecuteDataset(CommandType.Text, strSql.ToString());
        //}

        ///// <summary>
        ///// 获得前几行数据
        ///// </summary>
        //public DataSet GetList(int Top, string strWhere, string filedOrder)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select ");
        //    if (Top > 0)
        //    {
        //        strSql.Append(" top " + Top.ToString());
        //    }
        //    strSql.Append(" ID,msg_from,msg_to,msg_content,msg_addtime,msg_title ");
        //    strSql.Append(" FROM Msg ");
        //    if (strWhere.Trim() != "")
        //    {
        //        strSql.Append(" where " + strWhere);
        //    }
        //    strSql.Append(" order by " + filedOrder);
        //    return Database.ExecuteDataset(CommandType.Text, strSql.ToString());
        //}

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "Msg";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("proc_UP_GetRecordByPage",parameters,"ds");
        }*/

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
                string key = "[ID]";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "ID desc";
                }

                List<SqlParameter> paramList = new List<SqlParameter>();
                string searchWhere = BuilderWhere(searchParams, paramList);

                string sql = SqlHelper.GetCountSQL(tables, searchWhere, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL(SQL_TABLE_FIELD, tables, searchWhere, orderby, key, pageSize, page, false);
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
                        case "msg_to":
                            builder.Append(" AND [msg_to] = @msg_to");
                            parameter = new SqlParameter("@msg_to", SqlDbType.Int);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "msg_from":
                            builder.Append(" AND [msg_from] = @msg_from");
                            parameter = new SqlParameter("@msg_from", SqlDbType.Int);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;                      
                    }
                }
            }
            return builder.ToString();
        }

        #endregion  Method
    }
}

