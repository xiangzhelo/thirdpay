using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBAccess;
using viviapi.Model;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.DAL.Communication
{
    /// <summary>
    /// 数据访问类:Msg
    /// </summary>
    public partial class InternalMessage
    {
        internal const string SQL_TABLE = "msg";
        //money,pay,nopay,Integral
        internal const string SQL_TABLE_FIELD = @"[ID]
      ,[senderUserType]
      ,[sendId]
      ,[sender]
      ,[receiverType]
      ,[receiverId]
      ,[receiver]
      ,[msgtitle]
      ,[msgContent]
      ,[addtime]
      ,[isRead]
      ,[readTime]";


        public InternalMessage()
        { }

        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from msg");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.News.InternalMessage model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into msg(");
            strSql.Append("senderUserType,sendId,sender,receiverType,receiverId,receiver,msgtitle,msgContent,addtime,isRead,readTime)");
            strSql.Append(" values (");
            strSql.Append("@senderUserType,@sendId,@sender,@receiverType,@receiverId,@receiver,@msgtitle,@msgContent,@addtime,@isRead,@readTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@senderUserType", SqlDbType.TinyInt,1),
					new SqlParameter("@sendId", SqlDbType.Int,4),
					new SqlParameter("@sender", SqlDbType.NVarChar,50),
					new SqlParameter("@receiverType", SqlDbType.TinyInt,1),
					new SqlParameter("@receiverId", SqlDbType.Int,4),
					new SqlParameter("@receiver", SqlDbType.NVarChar,50),
					new SqlParameter("@msgtitle", SqlDbType.NVarChar,50),
					new SqlParameter("@msgContent", SqlDbType.NVarChar,2000),
					new SqlParameter("@addtime", SqlDbType.DateTime),
					new SqlParameter("@isRead", SqlDbType.Bit,1),
					new SqlParameter("@readTime", SqlDbType.DateTime)};
            parameters[0].Value = model.senderUserType;
            parameters[1].Value = model.sendId;
            parameters[2].Value = model.sender;
            parameters[3].Value = model.receiverType;
            parameters[4].Value = model.receiverId;
            parameters[5].Value = model.receiver;
            parameters[6].Value = model.msgtitle;
            parameters[7].Value = model.msgContent;
            parameters[8].Value = model.addtime;
            parameters[9].Value = model.isRead;
            parameters[10].Value = model.readTime;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        public bool Update(Model.News.InternalMessage model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update msg set ");
            strSql.Append("senderUserType=@senderUserType,");
            strSql.Append("sendId=@sendId,");
            strSql.Append("sender=@sender,");
            strSql.Append("receiverType=@receiverType,");
            strSql.Append("receiverId=@receiverId,");
            strSql.Append("receiver=@receiver,");
            strSql.Append("msgtitle=@msgtitle,");
            strSql.Append("msgContent=@msgContent,");
            strSql.Append("addtime=@addtime,");
            strSql.Append("isRead=@isRead,");
            strSql.Append("readTime=@readTime");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@senderUserType", SqlDbType.TinyInt,1),
					new SqlParameter("@sendId", SqlDbType.Int,4),
					new SqlParameter("@sender", SqlDbType.NVarChar,50),
					new SqlParameter("@receiverType", SqlDbType.TinyInt,1),
					new SqlParameter("@receiverId", SqlDbType.Int,4),
					new SqlParameter("@receiver", SqlDbType.NVarChar,50),
					new SqlParameter("@msgtitle", SqlDbType.NVarChar,50),
					new SqlParameter("@msgContent", SqlDbType.NVarChar,2000),
					new SqlParameter("@addtime", SqlDbType.DateTime),
					new SqlParameter("@isRead", SqlDbType.Bit,1),
					new SqlParameter("@readTime", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.senderUserType;
            parameters[1].Value = model.sendId;
            parameters[2].Value = model.sender;
            parameters[3].Value = model.receiverType;
            parameters[4].Value = model.receiverId;
            parameters[5].Value = model.receiver;
            parameters[6].Value = model.msgtitle;
            parameters[7].Value = model.msgContent;
            parameters[8].Value = model.addtime;
            parameters[9].Value = model.isRead;
            parameters[10].Value = model.readTime;
            parameters[11].Value = model.ID;

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

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from msg ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

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
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from msg ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
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


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.News.InternalMessage GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,senderUserType,sendId,sender,receiverType,receiverId,receiver,msgtitle,msgContent,addtime,isRead,readTime from msg ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            Model.News.InternalMessage model = new Model.News.InternalMessage();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.News.InternalMessage GetModel(int ID, int msg_to)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,senderUserType,sendId,sender,receiverType,receiverId,receiver,msgtitle,msgContent,addtime,isRead,readTime from msg ");
            strSql.Append(" where ID=@ID and msg_to=@msg_to");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
                    new SqlParameter("@msg_to", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;
            parameters[1].Value = msg_to;

            Model.News.InternalMessage model = new Model.News.InternalMessage();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        public Model.News.InternalMessage GetModelByTo(int msg_to)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,senderUserType,sendId,sender,receiverType,receiverId,receiver,msgtitle,msgContent,addtime,isRead,readTime from msg ");
            strSql.Append(" where msg_to=@msg_to");
            SqlParameter[] parameters = {
                    new SqlParameter("@msg_to", SqlDbType.Int,4)
			};
            parameters[0].Value = msg_to;

            Model.News.InternalMessage model = new Model.News.InternalMessage();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.News.InternalMessage DataRowToModel(DataRow row)
        {
            Model.News.InternalMessage model = new Model.News.InternalMessage();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["senderUserType"] != null && row["senderUserType"].ToString() != "")
                {
                    model.senderUserType = byte.Parse(row["senderUserType"].ToString());
                }
                if (row["sendId"] != null && row["sendId"].ToString() != "")
                {
                    model.sendId = int.Parse(row["sendId"].ToString());
                }
                if (row["sender"] != null)
                {
                    model.sender = row["sender"].ToString();
                }
                if (row["receiverType"] != null && row["receiverType"].ToString() != "")
                {
                    model.receiverType = byte.Parse(row["receiverType"].ToString());
                }
                if (row["receiverId"] != null && row["receiverId"].ToString() != "")
                {
                    model.receiverId = int.Parse(row["receiverId"].ToString());
                }
                if (row["receiver"] != null)
                {
                    model.receiver = row["receiver"].ToString();
                }
                if (row["msgtitle"] != null)
                {
                    model.msgtitle = row["msgtitle"].ToString();
                }
                if (row["msgContent"] != null)
                {
                    model.msgContent = row["msgContent"].ToString();
                }
                if (row["addtime"] != null && row["addtime"].ToString() != "")
                {
                    model.addtime = DateTime.Parse(row["addtime"].ToString());
                }
                if (row["isRead"] != null && row["isRead"].ToString() != "")
                {
                    if ((row["isRead"].ToString() == "1") || (row["isRead"].ToString().ToLower() == "true"))
                    {
                        model.isRead = true;
                    }
                    else
                    {
                        model.isRead = false;
                    }
                }
                if (row["readTime"] != null && row["readTime"].ToString() != "")
                {
                    model.readTime = DateTime.Parse(row["readTime"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,senderUserType,sendId,sender,receiverType,receiverId,receiver,msgtitle,msgContent,addtime,isRead,readTime ");
            strSql.Append(" FROM msg ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ID,senderUserType,sendId,sender,receiverType,receiverId,receiver,msgtitle,msgContent,addtime,isRead,readTime ");
            strSql.Append(" FROM msg ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM msg ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
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
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from msg T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

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
            parameters[0].Value = "msg";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod

        
        #region  ExtensionMethod

        public bool IsRead(int uid)
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
        public int GetUserMsgCount(int userId)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.Int,4)
};
            parameters[0].Value = userId;

            return Convert.ToInt32(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_msg_getusercount", parameters));
        }

        /// <summary>
        /// 根据搜索条件返回指定分页的商户信息。
        /// </summary>
        /// <param name="searchParams">搜索条件数组。</param>
        /// <param name="pageSize">分页大小。</param>
        /// <param name="page">页码。</param>
        /// <param name="orderby">排序方式。</param>
        /// <returns>分页数据。</returns>
        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
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
                        case "sendid":
                            builder.Append(" AND [sendId] = @sendId");
                            parameter = new SqlParameter("@sendId", SqlDbType.Int);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "receiverid":
                            builder.Append(" AND [receiverId] = @receiverId");
                            parameter = new SqlParameter("@receiverId", SqlDbType.Int);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "msgtitle":
                            builder.Append(" AND [msgtitle] like @msgtitle");
                            parameter = new SqlParameter("@msgtitle", SqlDbType.VarChar, 100);
                            parameter.Value = "%" + SqlHelper.CleanString((string)iparam.ParamValue, 100) + "%";
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
        #endregion  ExtensionMethod

        
    }
}

