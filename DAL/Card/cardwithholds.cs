using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBAccess;
using viviapi.Model.Order;

namespace viviapi.DAL.Order.Card
{
    /// <summary>
    ///     数据访问类:cardwithholds
    /// </summary>
    public class cardwithholds
    {
        #region  Method

        /// <summary>
        ///     增加一条数据
        /// </summary>
        public int Add(Model.Order.Cardwithholds model)
        {
            int rowsAffected;
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int, 4),
                new SqlParameter("@withholdid", SqlDbType.Int, 4),
                new SqlParameter("@orderid", SqlDbType.VarChar, 30),
                new SqlParameter("@withhold_type", SqlDbType.TinyInt, 1),
                new SqlParameter("@method", SqlDbType.TinyInt, 1),
                new SqlParameter("@refervalue", SqlDbType.Decimal, 9),
                new SqlParameter("@settle", SqlDbType.Decimal, 9),
                new SqlParameter("@withhold", SqlDbType.Decimal, 9),
                new SqlParameter("@backamt", SqlDbType.Decimal, 9),
                new SqlParameter("@addtime", SqlDbType.DateTime)
            };
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.withholdid;
            parameters[2].Value = model.orderid;
            parameters[3].Value = model.withhold_type;
            parameters[4].Value = model.method;
            parameters[5].Value = model.refervalue;
            parameters[6].Value = model.settle;
            parameters[7].Value = model.withhold;
            parameters[8].Value = model.backamt;
            parameters[9].Value = model.addtime;

            DbHelperSQL.RunProcedure("proc_cardwithholds_ADD", parameters, out rowsAffected);
            return (int) parameters[0].Value;
        }

        /// <summary>
        ///     增加一条数据
        /// </summary>
        public int Insert(OrderCardInfo orderInfo)
        {
            int rowsAffected;
            SqlParameter[] parameters =
            {
                new SqlParameter("@userid", SqlDbType.Int, 4),
                new SqlParameter("@cardtype", SqlDbType.Int, 4),
                new SqlParameter("@cardno", SqlDbType.VarChar, 40),
                new SqlParameter("@cardpwd", SqlDbType.VarChar, 40),
                new SqlParameter("@orderid", SqlDbType.VarChar, 30),
                new SqlParameter("@withhold_type", SqlDbType.TinyInt, 1),
                new SqlParameter("@method", SqlDbType.TinyInt, 1),
                new SqlParameter("@facevalue", SqlDbType.Decimal, 9),
                new SqlParameter("@refervalue", SqlDbType.Decimal, 9),
                new SqlParameter("@settle", SqlDbType.Decimal, 9),
                new SqlParameter("@withhold", SqlDbType.Decimal, 9),
                new SqlParameter("@supplierid", SqlDbType.Int, 4),
                new SqlParameter("@supprate", SqlDbType.Decimal, 9)
            };
            parameters[0].Value = orderInfo.userid;
            parameters[1].Value = orderInfo.typeId;
            parameters[2].Value = orderInfo.cardNo;
            parameters[3].Value = orderInfo.cardPwd;
            parameters[4].Value = orderInfo.orderid;
            parameters[5].Value = 3;
            parameters[6].Value = 1;
            parameters[7].Value = orderInfo.faceValue;
            parameters[8].Value = orderInfo.refervalue;
            parameters[9].Value = 0;
            parameters[10].Value = orderInfo.faceValue;
            parameters[11].Value = orderInfo.supplierId;
            parameters[12].Value = orderInfo.supplierRate;

            DbHelperSQL.RunProcedure("proc_cardwithholds_insert", parameters, out rowsAffected);
            return (int) parameters[0].Value;
        }

        /// <summary>
        ///     更新一条数据
        /// </summary>
        public bool Update(Model.Order.Cardwithholds model)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int, 4),
                new SqlParameter("@withholdid", SqlDbType.Int, 4),
                new SqlParameter("@orderid", SqlDbType.VarChar, 30),
                new SqlParameter("@withhold_type", SqlDbType.TinyInt, 1),
                new SqlParameter("@method", SqlDbType.TinyInt, 1),
                new SqlParameter("@refervalue", SqlDbType.Decimal, 9),
                new SqlParameter("@settle", SqlDbType.Decimal, 9),
                new SqlParameter("@withhold", SqlDbType.Decimal, 9),
                new SqlParameter("@backamt", SqlDbType.Decimal, 9),
                new SqlParameter("@addtime", SqlDbType.DateTime)
            };
            parameters[0].Value = model.id;
            parameters[1].Value = model.withholdid;
            parameters[2].Value = model.orderid;
            parameters[3].Value = model.withhold_type;
            parameters[4].Value = model.method;
            parameters[5].Value = model.refervalue;
            parameters[6].Value = model.settle;
            parameters[7].Value = model.withhold;
            parameters[8].Value = model.backamt;
            parameters[9].Value = model.addtime;

            DbHelperSQL.RunProcedure("proc_cardwithholds_Update", parameters, out rowsAffected);
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

            DbHelperSQL.RunProcedure("proc_cardwithholds_Delete", parameters, out rowsAffected);
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
            strSql.Append("delete from cardwithholds ");
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
        public Model.Order.Cardwithholds GetModel(int id)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int, 4)
            };
            parameters[0].Value = id;

            var model = new Model.Order.Cardwithholds();
            DataSet ds = DbHelperSQL.RunProcedure("proc_cardwithholds_GetModel", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            return null;
        }


        /// <summary>
        ///     得到一个对象实体
        /// </summary>
        public Model.Order.Cardwithholds DataRowToModel(DataRow row)
        {
            var model = new Model.Order.Cardwithholds();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["withholdid"] != null && row["withholdid"].ToString() != "")
                {
                    model.withholdid = int.Parse(row["withholdid"].ToString());
                }
                if (row["orderid"] != null)
                {
                    model.orderid = row["orderid"].ToString();
                }
                if (row["withhold_type"] != null && row["withhold_type"].ToString() != "")
                {
                    model.withhold_type = int.Parse(row["withhold_type"].ToString());
                }
                if (row["method"] != null && row["method"].ToString() != "")
                {
                    model.method = int.Parse(row["method"].ToString());
                }
                if (row["refervalue"] != null && row["refervalue"].ToString() != "")
                {
                    model.refervalue = decimal.Parse(row["refervalue"].ToString());
                }
                if (row["settle"] != null && row["settle"].ToString() != "")
                {
                    model.settle = decimal.Parse(row["settle"].ToString());
                }
                if (row["withhold"] != null && row["withhold"].ToString() != "")
                {
                    model.withhold = decimal.Parse(row["withhold"].ToString());
                }
                if (row["backamt"] != null && row["backamt"].ToString() != "")
                {
                    model.backamt = decimal.Parse(row["backamt"].ToString());
                }
                if (row["addtime"] != null && row["addtime"].ToString() != "")
                {
                    model.addtime = DateTime.Parse(row["addtime"].ToString());
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
            strSql.Append(
                "select id,withholdid,orderid,withhold_type,method,refervalue,settle,withhold,backamt,addtime ");
            strSql.Append(" FROM cardwithholds ");
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
            strSql.Append(" id,withholdid,orderid,withhold_type,method,refervalue,settle,withhold,backamt,addtime ");
            strSql.Append(" FROM cardwithholds ");
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
            strSql.Append("select count(1) FROM cardwithholds ");
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
            strSql.Append(")AS Row, T.*  from cardwithholds T ");
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
            parameters[0].Value = "cardwithholds";
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