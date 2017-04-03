using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
//Please add references
using DBAccess;

namespace viviapi.DAL.User
{
    /// <summary>
    /// 数据访问类:userLevel
    /// </summary>
    public partial class UserLevel
    {
        public UserLevel()
        { }
        #region  BasicMethod

        public int Insert(viviapi.Model.User.UserLevel model)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@rateType", SqlDbType.TinyInt,1),
					new SqlParameter("@level", SqlDbType.Int,4),
					new SqlParameter("@levName", SqlDbType.VarChar,50)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.rateType;
            parameters[2].Value = model.level;
            parameters[3].Value = model.levName;

            object obj = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_userLevel_insert", parameters);
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
        /// 增加一条数据
        /// </summary>
        public int Add(viviapi.Model.User.UserLevel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into userLevel(");
            strSql.Append("rateType,level,levName)");
            strSql.Append(" values (");
            strSql.Append("@rateType,@level,@levName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@rateType", SqlDbType.TinyInt,1),
					new SqlParameter("@level", SqlDbType.Int,4),
					new SqlParameter("@levName", SqlDbType.VarChar,50)};
            parameters[0].Value = model.rateType;
            parameters[1].Value = model.level;
            parameters[2].Value = model.levName;

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
        public bool Update(viviapi.Model.User.UserLevel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update userLevel set ");
            strSql.Append("rateType=@rateType,");
            strSql.Append("level=@level,");
            strSql.Append("levName=@levName");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@rateType", SqlDbType.TinyInt,1),
					new SqlParameter("@level", SqlDbType.Int,4),
					new SqlParameter("@levName", SqlDbType.VarChar,50),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.rateType;
            parameters[1].Value = model.level;
            parameters[2].Value = model.levName;
            parameters[3].Value = model.id;

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
        public bool Delete(int id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            int rows =
                Convert.ToInt32(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_userLevel_del", parameters));
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
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from userLevel ");
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

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public string GetLevelName(int level)
        {
            var strSql = new StringBuilder();
            strSql.Append("select levName from userLevel ");
            strSql.Append(" where level=@level");
            SqlParameter[] parameters = {
					new SqlParameter("@level", SqlDbType.Int,4)
			};
            parameters[0].Value = level;

            return DbHelperSQL.GetSingle(strSql.ToString(), parameters).ToString();
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public viviapi.Model.User.UserLevel GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,rateType,level,levName from userLevel ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            viviapi.Model.User.UserLevel model = new viviapi.Model.User.UserLevel();
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
        public viviapi.Model.User.UserLevel DataRowToModel(DataRow row)
        {
            viviapi.Model.User.UserLevel model = new viviapi.Model.User.UserLevel();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["rateType"] != null && row["rateType"].ToString() != "")
                {
                    model.rateType = int.Parse(row["rateType"].ToString());
                }
                if (row["level"] != null && row["level"].ToString() != "")
                {
                    model.level = int.Parse(row["level"].ToString());
                }
                if (row["levName"] != null)
                {
                    model.levName = row["levName"].ToString();
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
            strSql.Append("select id,rateType,level,levName ");
            strSql.Append(" FROM userLevel ");
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
            strSql.Append(" id,rateType,level,levName ");
            strSql.Append(" FROM userLevel ");
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
            strSql.Append("select count(1) FROM userLevel ");
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
                strSql.Append("order by T.id desc");
            }
            strSql.Append(")AS Row, T.*  from userLevel T ");
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
            parameters[0].Value = "userLevel";
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

