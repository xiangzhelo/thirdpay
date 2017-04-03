using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
//Please add references
using DBAccess;

namespace viviapi.DAL.User
{
    /// <summary>
    /// 数据访问类:UserLoginByPartner
    /// </summary>
    public partial class UserLoginByPartner
    {
        public UserLoginByPartner()
        { }

        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("id", "UserLoginByPartner");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public int Exists(int plant, string openid)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@plant", SqlDbType.Int,4),
                    new SqlParameter("@openid", SqlDbType.VarChar,100)
			};
            parameters[0].Value = plant;
            parameters[1].Value = openid;

            object userId = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_userLoginByPartner_Exists", parameters);

            if (userId == DBNull.Value)
                return 0;

            return Convert.ToInt32(userId);
        }

        /// <summary>
        ///  增加一条数据
        /// </summary>
        public int Insert(Model.User.UserLoginByPartner model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@plant", SqlDbType.TinyInt,1),
					new SqlParameter("@plantname", SqlDbType.VarChar,50),
					new SqlParameter("@openid", SqlDbType.VarChar,100),
					new SqlParameter("@available", SqlDbType.TinyInt,1)};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.userid;
            parameters[2].Value = model.plant;
            parameters[3].Value = model.plantname;
            parameters[4].Value = model.openid;
            parameters[5].Value = model.available;

            DbHelperSQL.RunProcedure("proc_userLoginByPartner_insert", parameters, out rowsAffected);
            return (int)parameters[0].Value;
        }

        /// <summary>
        /// 登录解绑
        /// </summary>
        public bool Unbind(int plant, int userid)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@plant", SqlDbType.Int,4),
                    new SqlParameter("@userid", SqlDbType.VarChar,100)
			};
            parameters[0].Value = plant;
            parameters[1].Value = userid;

             DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_userLoginByPartner_Unbind", parameters);

            return true;
        }

        /// <summary>
        ///  增加一条数据
        /// </summary>
        public int Add(viviapi.Model.User.UserLoginByPartner model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@plant", SqlDbType.TinyInt,1),
					new SqlParameter("@plantname", SqlDbType.VarChar,50),
					new SqlParameter("@openid", SqlDbType.VarChar,100),
					new SqlParameter("@available", SqlDbType.TinyInt,1)};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.userid;
            parameters[2].Value = model.plant;
            parameters[3].Value = model.plantname;
            parameters[4].Value = model.openid;
            parameters[5].Value = model.available;

            DbHelperSQL.RunProcedure("proc_UserLoginByPartner_ADD", parameters, out rowsAffected);
            return (int)parameters[0].Value;
        }

        /// <summary>
        ///  更新一条数据
        /// </summary>
        public bool Update(viviapi.Model.User.UserLoginByPartner model)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@plant", SqlDbType.TinyInt,1),
					new SqlParameter("@plantname", SqlDbType.VarChar,50),
					new SqlParameter("@openid", SqlDbType.VarChar,100),
					new SqlParameter("@available", SqlDbType.TinyInt,1)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.userid;
            parameters[2].Value = model.plant;
            parameters[3].Value = model.plantname;
            parameters[4].Value = model.openid;
            parameters[5].Value = model.available;

            DbHelperSQL.RunProcedure("proc_UserLoginByPartner_Update", parameters, out rowsAffected);
            if (rowsAffected > 0)
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
            int rowsAffected = 0;
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            DbHelperSQL.RunProcedure("proc_UserLoginByPartner_Delete", parameters, out rowsAffected);
            if (rowsAffected > 0)
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
            strSql.Append("delete from UserLoginByPartner ");
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
        public viviapi.Model.User.UserLoginByPartner GetModel(int id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            viviapi.Model.User.UserLoginByPartner model = new viviapi.Model.User.UserLoginByPartner();
            DataSet ds = DbHelperSQL.RunProcedure("proc_UserLoginByPartner_GetModel", parameters, "ds");
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
        public viviapi.Model.User.UserLoginByPartner GetModel(int plant, string openid)
        {
            IDataParameter[] parameters = {
					new SqlParameter("@plant", SqlDbType.Int,4),
                    new SqlParameter("@openid", SqlDbType.VarChar,100)
			};
            parameters[0].Value = plant;
            parameters[1].Value = openid;

            var model = new viviapi.Model.User.UserLoginByPartner();
            DataSet ds = DbHelperSQL.RunProcedure("proc_userLoginByPartner_GetModelByOpenId", parameters, "ds");
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
        public viviapi.Model.User.UserLoginByPartner DataRowToModel(DataRow row)
        {
            viviapi.Model.User.UserLoginByPartner model = new viviapi.Model.User.UserLoginByPartner();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["userid"] != null && row["userid"].ToString() != "")
                {
                    model.userid = int.Parse(row["userid"].ToString());
                }
                if (row["plant"] != null && row["plant"].ToString() != "")
                {
                    model.plant = int.Parse(row["plant"].ToString());
                }
                if (row["plantname"] != null)
                {
                    model.plantname = row["plantname"].ToString();
                }
                if (row["openid"] != null)
                {
                    model.openid = row["openid"].ToString();
                }
                if (row["available"] != null && row["available"].ToString() != "")
                {
                    model.available = int.Parse(row["available"].ToString());
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
            strSql.Append("select id,userid,plant,plantname,openid,available ");
            strSql.Append(" FROM UserLoginByPartner ");
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
            strSql.Append(" id,userid,plant,plantname,openid,available ");
            strSql.Append(" FROM UserLoginByPartner ");
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
            strSql.Append("select count(1) FROM UserLoginByPartner ");
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
            strSql.Append(")AS Row, T.*  from UserLoginByPartner T ");
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
            parameters[0].Value = "UserLoginByPartner";
            parameters[1].Value = "id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method
        #region  MethodEx

        #endregion  MethodEx
    }
}

