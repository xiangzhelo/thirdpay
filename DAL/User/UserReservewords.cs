using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBAccess;

namespace viviapi.DAL.User
{
    /// <summary>
    ///     数据访问类:userreservewords
    /// </summary>
    public class UserReservewords
    {
        #region  BasicMethod

        public bool Exists(int userID)
        {
            int rowsAffected;
            IDataParameter[] parameters =
            {
                new SqlParameter("@userID", SqlDbType.Int, 4)
            };
            parameters[0].Value = userID;

            int result = DbHelperSQL.RunProcedure("proc_userreservewords_exists", parameters, out rowsAffected);
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     增加一条数据
        /// </summary>
        public bool Add(Model.User.UserReservewords model)
        {
            var strSql = new StringBuilder();
            strSql.Append("insert into userreservewords(");
            strSql.Append("userId,reservewords,addTime,updateTime)");
            strSql.Append(" values (");
            strSql.Append("@userId,@reservewords,@addTime,@updateTime)");
            SqlParameter[] parameters =
            {
                new SqlParameter("@userId", SqlDbType.Int, 4),
                new SqlParameter("@reservewords", SqlDbType.VarChar, 50),
                new SqlParameter("@addTime", SqlDbType.DateTime),
                new SqlParameter("@updateTime", SqlDbType.DateTime)
            };
            parameters[0].Value = model.userId;
            parameters[1].Value = model.reservewords;
            parameters[2].Value = model.addTime;
            parameters[3].Value = model.updateTime;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            return false;
        }

        public bool Insert(Model.User.UserReservewords model)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@userId", SqlDbType.Int, 4),
                new SqlParameter("@reservewords", SqlDbType.VarChar, 50),
                new SqlParameter("@addTime", SqlDbType.DateTime)
            };
            parameters[0].Value = model.userId;
            parameters[1].Value = model.reservewords;
            parameters[2].Value = model.addTime;

            int rows = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_userreservewords_insert", parameters);
            if (rows > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     更新一条数据
        /// </summary>
        public bool Update(Model.User.UserReservewords model)
        {
            var strSql = new StringBuilder();
            strSql.Append("update userreservewords set ");
            strSql.Append("userId=@userId,");
            strSql.Append("reservewords=@reservewords,");
            strSql.Append("addTime=@addTime,");
            strSql.Append("updateTime=@updateTime");
            strSql.Append(" where ");
            SqlParameter[] parameters =
            {
                new SqlParameter("@userId", SqlDbType.Int, 4),
                new SqlParameter("@reservewords", SqlDbType.VarChar, 50),
                new SqlParameter("@addTime", SqlDbType.DateTime),
                new SqlParameter("@updateTime", SqlDbType.DateTime)
            };
            parameters[0].Value = model.userId;
            parameters[1].Value = model.reservewords;
            parameters[2].Value = model.addTime;
            parameters[3].Value = model.updateTime;

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
        public bool Delete()
        {
            //该表无主键信息，请自定义主键/条件字段
            var strSql = new StringBuilder();
            strSql.Append("delete from userreservewords ");
            strSql.Append(" where ");
            SqlParameter[] parameters =
            {
            };

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        ///     得到一个对象实体
        /// </summary>
        public Model.User.UserReservewords GetModel(int userId)
        {
            //该表无主键信息，请自定义主键/条件字段
            var strSql = new StringBuilder();
            strSql.Append("select  top 1 userId,reservewords,addTime,updateTime from userreservewords ");
            strSql.Append(" where userId=@userId");
            SqlParameter[] parameters =
            {
                new SqlParameter("@userId", SqlDbType.Int, 4)
            };
            parameters[0].Value = userId;

            var model = new Model.User.UserReservewords();
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
        public Model.User.UserReservewords DataRowToModel(DataRow row)
        {
            var model = new Model.User.UserReservewords();
            if (row != null)
            {
                if (row["userId"] != null && row["userId"].ToString() != "")
                {
                    model.userId = int.Parse(row["userId"].ToString());
                }
                if (row["reservewords"] != null)
                {
                    model.reservewords = row["reservewords"].ToString();
                }
                if (row["addTime"] != null && row["addTime"].ToString() != "")
                {
                    model.addTime = DateTime.Parse(row["addTime"].ToString());
                }
                if (row["updateTime"] != null && row["updateTime"].ToString() != "")
                {
                    model.updateTime = DateTime.Parse(row["updateTime"].ToString());
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
            strSql.Append("select userId,reservewords,addTime,updateTime ");
            strSql.Append(" FROM userreservewords ");
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
            strSql.Append(" userId,reservewords,addTime,updateTime ");
            strSql.Append(" FROM userreservewords ");
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
            strSql.Append("select count(1) FROM userreservewords ");
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
                strSql.Append("order by T. desc");
            }
            strSql.Append(")AS Row, T.*  from userreservewords T ");
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
			parameters[0].Value = "userreservewords";
			parameters[1].Value = "";
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