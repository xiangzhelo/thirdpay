using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBAccess;

namespace viviapi.DAL.Sys
{
    /// <summary>
    ///     数据访问类:sysMailConfig
    /// </summary>
    public class SysMailConfig
    {
        #region  BasicMethod

        public int Insert(Model.Sys.SysMailConfig model)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@address", SqlDbType.VarChar, 255),
                new SqlParameter("@displayName", SqlDbType.VarChar, 100),
                new SqlParameter("@host", SqlDbType.VarChar, 50),
                new SqlParameter("@port", SqlDbType.Int, 4),
                new SqlParameter("@username", SqlDbType.VarChar, 255),
                new SqlParameter("@password", SqlDbType.VarChar, 50),
                new SqlParameter("@enableSsl", SqlDbType.TinyInt, 1),
                new SqlParameter("@useDefaultCredentials", SqlDbType.TinyInt, 1),
                new SqlParameter("@used", SqlDbType.TinyInt, 1),
                new SqlParameter("@sort", SqlDbType.Int, 4),
                new SqlParameter("@id", SqlDbType.Int, 4)
            };
            parameters[0].Value = model.address;
            parameters[1].Value = model.displayName;
            parameters[2].Value = model.host;
            parameters[3].Value = model.port;
            parameters[4].Value = model.username;
            parameters[5].Value = model.password;
            parameters[6].Value = model.enableSsl;
            parameters[7].Value = model.useDefaultCredentials;
            parameters[8].Value = model.used;
            parameters[9].Value = model.sort;
            parameters[10].Value = model.id;

            object obj = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_sysMailConfig_insert", parameters);
            if (obj == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj);
        }


        /// <summary>
        ///     增加一条数据
        /// </summary>
        public int Add(Model.Sys.SysMailConfig model)
        {
            var strSql = new StringBuilder();
            strSql.Append("insert into sysMailConfig(");
            strSql.Append("host,port,username,password,enableSsl,useDefaultCredentials,sort)");
            strSql.Append(" values (");
            strSql.Append("@host,@port,@username,@password,@enableSsl,@useDefaultCredentials,@sort)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters =
            {
                new SqlParameter("@host", SqlDbType.VarChar, 50),
                new SqlParameter("@port", SqlDbType.Int, 4),
                new SqlParameter("@username", SqlDbType.VarChar, 255),
                new SqlParameter("@password", SqlDbType.VarChar, 50),
                new SqlParameter("@enableSsl", SqlDbType.TinyInt, 1),
                new SqlParameter("@useDefaultCredentials", SqlDbType.TinyInt, 1),
                new SqlParameter("@sort", SqlDbType.Int, 4)
            };
            parameters[0].Value = model.host;
            parameters[1].Value = model.port;
            parameters[2].Value = model.username;
            parameters[3].Value = model.password;
            parameters[4].Value = model.enableSsl;
            parameters[5].Value = model.useDefaultCredentials;
            parameters[6].Value = model.sort;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj);
        }

        /// <summary>
        ///     更新一条数据
        /// </summary>
        public bool Update(Model.Sys.SysMailConfig model)
        {
            var strSql = new StringBuilder();
            strSql.Append("update sysMailConfig set ");
            strSql.Append("host=@host,");
            strSql.Append("port=@port,");
            strSql.Append("username=@username,");
            strSql.Append("password=@password,");
            strSql.Append("enableSsl=@enableSsl,");
            strSql.Append("useDefaultCredentials=@useDefaultCredentials,");
            strSql.Append("sort=@sort");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters =
            {
                new SqlParameter("@host", SqlDbType.VarChar, 50),
                new SqlParameter("@port", SqlDbType.Int, 4),
                new SqlParameter("@username", SqlDbType.VarChar, 255),
                new SqlParameter("@password", SqlDbType.VarChar, 50),
                new SqlParameter("@enableSsl", SqlDbType.TinyInt, 1),
                new SqlParameter("@useDefaultCredentials", SqlDbType.TinyInt, 1),
                new SqlParameter("@sort", SqlDbType.Int, 4),
                new SqlParameter("@id", SqlDbType.Int, 4)
            };
            parameters[0].Value = model.host;
            parameters[1].Value = model.port;
            parameters[2].Value = model.username;
            parameters[3].Value = model.password;
            parameters[4].Value = model.enableSsl;
            parameters[5].Value = model.useDefaultCredentials;
            parameters[6].Value = model.sort;
            parameters[7].Value = model.id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            var strSql = new StringBuilder();
            strSql.Append("delete from sysMailConfig ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int, 4)
            };
            parameters[0].Value = id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     批量删除数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            var strSql = new StringBuilder();
            strSql.Append("delete from sysMailConfig ");
            strSql.Append(" where id in (" + idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        ///     得到一个对象实体
        /// </summary>
        public Model.Sys.SysMailConfig GetDefaultModel()
        {
            var strSql = new StringBuilder();
            strSql.Append(
                "select  top 1 id,address,displayName,host,port,username,password,enableSsl,useDefaultCredentials,used,sort from sysMailConfig ");
            strSql.Append(" where used=1");

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            return null;
        }

        /// <summary>
        ///     得到一个对象实体
        /// </summary>
        public Model.Sys.SysMailConfig GetModel(int id)
        {
            var strSql = new StringBuilder();
            strSql.Append(
                "select  top 1 id,address,displayName,host,port,username,password,enableSsl,useDefaultCredentials,used,sort from sysMailConfig ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int, 4)
            };
            parameters[0].Value = id;

            var model = new Model.Sys.SysMailConfig();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            return null;
        }


        /// <summary>
        ///     得到一个对象实体
        /// </summary>
        public Model.Sys.SysMailConfig DataRowToModel(DataRow row)
        {
            var model = new Model.Sys.SysMailConfig();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["address"] != null)
                {
                    model.address = row["address"].ToString();
                }
                if (row["displayName"] != null)
                {
                    model.displayName = row["displayName"].ToString();
                }
                if (row["host"] != null)
                {
                    model.host = row["host"].ToString();
                }
                if (row["port"] != null && row["port"].ToString() != "")
                {
                    model.port = int.Parse(row["port"].ToString());
                }
                if (row["username"] != null)
                {
                    model.username = row["username"].ToString();
                }
                if (row["password"] != null)
                {
                    model.password = row["password"].ToString();
                }
                if (row["enableSsl"] != null && row["enableSsl"].ToString() != "")
                {
                    model.enableSsl = byte.Parse(row["enableSsl"].ToString());
                }
                if (row["useDefaultCredentials"] != null && row["useDefaultCredentials"].ToString() != "")
                {
                    model.useDefaultCredentials = byte.Parse(row["useDefaultCredentials"].ToString());
                }
                if (row["used"] != null && row["used"].ToString() != "")
                {
                    model.used = byte.Parse(row["used"].ToString());
                }
                if (row["sort"] != null && row["sort"].ToString() != "")
                {
                    model.sort = int.Parse(row["sort"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        ///     获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            var strSql = new StringBuilder();
            strSql.Append("select id,address,host,port,username,password,enableSsl,useDefaultCredentials,sort,displayName,used ");
            strSql.Append(" FROM sysMailConfig ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        ///     获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            var strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top);
            }
            strSql.Append(" id,address,host,port,username,password,enableSsl,useDefaultCredentials,sort,displayName ");
            strSql.Append(" FROM sysMailConfig ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        ///     获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            var strSql = new StringBuilder();
            strSql.Append("select count(1) FROM sysMailConfig ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj);
        }

        /// <summary>
        ///     分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.id desc");
            }
            strSql.Append(")AS Row, T.*  from sysMailConfig T ");
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
            parameters[0].Value = "sysMailConfig";
            parameters[1].Value = "id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod

        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}