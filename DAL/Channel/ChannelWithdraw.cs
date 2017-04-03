using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBAccess;

namespace viviapi.DAL.Withdraw
{
    /// <summary>
    ///     数据访问类:channelwithdraw
    /// </summary>
    public class ChannelWithdraw
    {
        #region  Method

        /// <summary>
        ///     增加一条数据
        /// </summary>
        public int Add(Model.Channel.ChannelWithdraw model)
        {
            int rowsAffected;
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int, 4),
                new SqlParameter("@bankCode", SqlDbType.VarChar, 10),
                new SqlParameter("@bankName", SqlDbType.VarChar, 30),
                new SqlParameter("@supplier", SqlDbType.Int, 4),
                new SqlParameter("@sort", SqlDbType.Int, 4)
            };
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.bankCode;
            parameters[2].Value = model.bankName;
            parameters[3].Value = model.supplier;
            parameters[4].Value = model.sort;

            DbHelperSQL.RunProcedure("proc_channelwithdraw_ADD", parameters, out rowsAffected);
            return (int) parameters[0].Value;
        }

        /// <summary>
        ///     更新一条数据
        /// </summary>
        public bool Update(Model.Channel.ChannelWithdraw model)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int, 4),
                new SqlParameter("@bankCode", SqlDbType.VarChar, 10),
                new SqlParameter("@bankName", SqlDbType.VarChar, 30),
                new SqlParameter("@supplier", SqlDbType.Int, 4),
                new SqlParameter("@sort", SqlDbType.Int, 4)
            };
            parameters[0].Value = model.id;
            parameters[1].Value = model.bankCode;
            parameters[2].Value = model.bankName;
            parameters[3].Value = model.supplier;
            parameters[4].Value = model.sort;

            DbHelperSQL.RunProcedure("proc_channelwithdraw_Update", parameters, out rowsAffected);
            if (rowsAffected > 0)
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
            int rowsAffected = 0;
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int, 4)
            };
            parameters[0].Value = id;

            DbHelperSQL.RunProcedure("proc_channelwithdraw_Delete", parameters, out rowsAffected);
            if (rowsAffected > 0)
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
            strSql.Append("delete from channelwithdraw ");
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
        public Model.Channel.ChannelWithdraw GetModel(int id)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int, 4)
            };
            parameters[0].Value = id;

            var model = new Model.Channel.ChannelWithdraw();
            DataSet ds = DbHelperSQL.RunProcedure("proc_channelwithdraw_GetModel", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            return null;
        }

        /// <summary>
        ///     得到一个对象实体
        /// </summary>
        public Model.Channel.ChannelWithdraw GetModelByBankName(string bankName)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@bankName", SqlDbType.VarChar, 30)
            };
            parameters[0].Value = bankName;

            var model = new Model.Channel.ChannelWithdraw();
            DataSet ds = DbHelperSQL.RunProcedure("proc_channelwithdraw_GetModelBybankName", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            return null;
        }


        /// <summary>
        ///     得到一个对象实体
        /// </summary>
        public Model.Channel.ChannelWithdraw DataRowToModel(DataRow row)
        {
            var model = new Model.Channel.ChannelWithdraw();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["bankCode"] != null)
                {
                    model.bankCode = row["bankCode"].ToString();
                }
                if (row["bankName"] != null)
                {
                    model.bankName = row["bankName"].ToString();
                }
                if (row["supplier"] != null && row["supplier"].ToString() != "")
                {
                    model.supplier = int.Parse(row["supplier"].ToString());
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
            strSql.Append("select id,bankCode,bankName,supplier,sort ");
            strSql.Append(" FROM channelwithdraw ");
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
            strSql.Append(" id,bankCode,bankName,supplier,sort ");
            strSql.Append(" FROM channelwithdraw ");
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
            strSql.Append("select count(1) FROM channelwithdraw ");
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
            strSql.Append(")AS Row, T.*  from channelwithdraw T ");
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
			parameters[0].Value = "channelwithdraw";
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

        /// <summary>
        ///     得到一个对象实体
        /// </summary>
        public int GetSupplier(string bankCode)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@bankCode", SqlDbType.VarChar, 10)
            };
            parameters[0].Value = bankCode;


            object ds = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_channelwithdraw_GetSup", parameters);
            if (ds != DBNull.Value)
            {
                return Convert.ToInt32(ds);
            }
            return 0;
        }

        #endregion  MethodEx
    }
}