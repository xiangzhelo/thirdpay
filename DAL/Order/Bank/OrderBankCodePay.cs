using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBAccess;

namespace viviapi.DAL.Order.Bank
{
    /// <summary>
    /// 数据访问类:orderbankcodepay
    /// </summary>
    public partial class OrderBankCodePay
    {
        public OrderBankCodePay()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("id", "orderbankcodepay");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            int result = DbHelperSQL.RunProcedure("proc_orderbankcodepay_Exists", parameters, out rowsAffected);
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsBySysOrderNo(string orderNo)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@orderNo", SqlDbType.Int,4)
			};
            parameters[0].Value = orderNo;

            int result = DbHelperSQL.RunProcedure("proc_orderbankcodepay_ExistsByOrderNo", parameters, out rowsAffected);
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///  增加一条数据
        /// </summary>
        public int Add(Model.Order.Bank.OrderBankCodePay model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@sysOrderNo", SqlDbType.VarChar,30),
					new SqlParameter("@channel", SqlDbType.Int,4),
					new SqlParameter("@codeImgUrl", SqlDbType.VarChar,400),
					new SqlParameter("@addTime", SqlDbType.DateTime),
					new SqlParameter("@updateTime", SqlDbType.DateTime)};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.sysOrderNo;
            parameters[2].Value = model.channel;
            parameters[3].Value = model.codeImgUrl;
            parameters[4].Value = model.addTime;
            parameters[5].Value = model.updateTime;

            DbHelperSQL.RunProcedure("proc_orderbankcodepay_add", parameters, out rowsAffected);
            return (int)parameters[0].Value;
        }

        /// <summary>
        ///  更新一条数据
        /// </summary>
        public bool Update(Model.Order.Bank.OrderBankCodePay model)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@sysOrderNo", SqlDbType.VarChar,30),
					new SqlParameter("@channel", SqlDbType.Int,4),
					new SqlParameter("@codeImgUrl", SqlDbType.VarChar,400),
					new SqlParameter("@addTime", SqlDbType.DateTime),
					new SqlParameter("@updateTime", SqlDbType.DateTime)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.sysOrderNo;
            parameters[2].Value = model.channel;
            parameters[3].Value = model.codeImgUrl;
            parameters[4].Value = model.addTime;
            parameters[5].Value = model.updateTime;

            DbHelperSQL.RunProcedure("proc_orderbankcodepay_Update", parameters, out rowsAffected);
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

            DbHelperSQL.RunProcedure("proc_orderbankcodepay_Delete", parameters, out rowsAffected);
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
            strSql.Append("delete from orderbankcodepay ");
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
        public Model.Order.Bank.OrderBankCodePay GetModel(string orderno)
        {
            IDataParameter[] parameters = {
					new SqlParameter("@orderno", SqlDbType.VarChar,30)
			};
            parameters[0].Value = orderno;

            var model = new Model.Order.Bank.OrderBankCodePay();
            DataSet ds = DbHelperSQL.RunProcedure("proc_orderbankcodepay_GetModel", parameters, "ds");
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
        public Model.Order.Bank.OrderBankCodePay DataRowToModel(DataRow row)
        {
            var model = new Model.Order.Bank.OrderBankCodePay();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["sysOrderNo"] != null)
                {
                    model.sysOrderNo = row["sysOrderNo"].ToString();
                }
                if (row["channel"] != null && row["channel"].ToString() != "")
                {
                    model.channel = int.Parse(row["channel"].ToString());
                }
                if (row["codeImgUrl"] != null)
                {
                    model.codeImgUrl = row["codeImgUrl"].ToString();
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,sysOrderNo,channel,codeImgUrl,addTime,updateTime ");
            strSql.Append(" FROM orderbankcodepay ");
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
            strSql.Append(" id,sysOrderNo,channel,codeImgUrl,addTime,updateTime ");
            strSql.Append(" FROM orderbankcodepay ");
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
            strSql.Append("select count(1) FROM orderbankcodepay ");
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
            strSql.Append(")AS Row, T.*  from orderbankcodepay T ");
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
            parameters[0].Value = "orderbankcodepay";
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

