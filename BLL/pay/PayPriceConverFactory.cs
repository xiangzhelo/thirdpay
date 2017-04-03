using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Web;
using System.Text;
using viviapi.Model.Payment;
using viviLib;
using DBAccess;

namespace viviapi.BLL.Payment
{
    /// <summary>
    /// 数据访问类:PayPriceConver
    /// </summary>
    public partial class PayPriceConverFactory
    {
        #region  Method
        /// <summary>
        ///  增加一条数据
        /// </summary>
        public int Add(PayPriceConver model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Pri_Type", SqlDbType.Int,4),
					new SqlParameter("@Value", SqlDbType.Money,8),
					new SqlParameter("@Conv_PriType", SqlDbType.Int,4),
					new SqlParameter("@Created", SqlDbType.DateTime),
                    new SqlParameter("@IsOpen", SqlDbType.Bit)
            };
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.Pri_Type;
            parameters[2].Value = model.Value;
            parameters[3].Value = model.Conv_PriType;
            parameters[4].Value = model.Created;
            parameters[5].Value = model.IsOpen;

            rowsAffected = DataBase.RunProc("PayPriceConver_ADD", parameters);
            if (rowsAffected > 0)
                return (int)parameters[0].Value;
            else
                return 0;
        }

        /// <summary>
        ///  更新一条数据
        /// </summary>
        public bool Update(PayPriceConver model)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Pri_Type", SqlDbType.Int,4),
					new SqlParameter("@Value", SqlDbType.Money,8),
					new SqlParameter("@Conv_PriType", SqlDbType.Int,4),
					new SqlParameter("@Created", SqlDbType.DateTime),
                     new SqlParameter("@IsOpen", SqlDbType.Bit)
            };
            parameters[0].Value = model.ID;
            parameters[1].Value = model.Pri_Type;
            parameters[2].Value = model.Value;
            parameters[3].Value = model.Conv_PriType;
            parameters[4].Value = model.Created;
            parameters[5].Value = model.IsOpen;

            rowsAffected = DataBase.RunProc("PayPriceConver_Update", parameters);
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
        public bool Delete(int ID)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
};
            parameters[0].Value = ID;

            rowsAffected = DataBase.RunProc("PayPriceConver_Delete", parameters);
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
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from PayPriceConver ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
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
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pay"></param>
        /// <returns></returns>
        public PayPriceConver GetModel(int pay,decimal value)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@value", SqlDbType.Money,8),
                    new SqlParameter("@pay", SqlDbType.Int,4)
};
            parameters[0].Value = value;
            parameters[1].Value = pay;

            PayPriceConver model = new PayPriceConver();
            DataSet ds = null;
            DataBase.RunProc("PayPriceConver_GetModelByPayType", parameters, out ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Pri_Type"].ToString() != "")
                {
                    model.Pri_Type = int.Parse(ds.Tables[0].Rows[0]["Pri_Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Value"].ToString() != "")
                {
                    model.Value = decimal.Parse(ds.Tables[0].Rows[0]["Value"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Conv_PriType"].ToString() != "")
                {
                    model.Conv_PriType = int.Parse(ds.Tables[0].Rows[0]["Conv_PriType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Created"].ToString() != "")
                {
                    model.Created = DateTime.Parse(ds.Tables[0].Rows[0]["Created"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsOpen"].ToString() != "")
                {
                    model.IsOpen = bool.Parse(ds.Tables[0].Rows[0]["IsOpen"].ToString());
                }
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
        public PayPriceConver GetModel(int ID)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
};
            parameters[0].Value = ID;

            PayPriceConver model = new PayPriceConver();
            DataSet ds = null;
            DataBase.RunProc("PayPriceConver_GetModel", parameters, out ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Pri_Type"].ToString() != "")
                {
                    model.Pri_Type = int.Parse(ds.Tables[0].Rows[0]["Pri_Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Value"].ToString() != "")
                {
                    model.Value = decimal.Parse(ds.Tables[0].Rows[0]["Value"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Conv_PriType"].ToString() != "")
                {
                    model.Conv_PriType = int.Parse(ds.Tables[0].Rows[0]["Conv_PriType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Created"].ToString() != "")
                {
                    model.Created = DateTime.Parse(ds.Tables[0].Rows[0]["Created"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsOpen"].ToString() != "")
                {
                    model.IsOpen = bool.Parse(ds.Tables[0].Rows[0]["IsOpen"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetInitList(string PayType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.Value,b.ID,Pri_Type,Conv_PriType,Created,IsOpen from ");
            strSql.Append(" (select 30 Value ");
            strSql.Append(" union all select 50 ");
            strSql.Append(" union all select 100 ");
            strSql.Append(" union all select 300");
            strSql.Append(" union all select 500) a left join PayPriceConver b");
            strSql.AppendFormat(" on a.Value = b.Value and Pri_Type={0} order by a.Value asc", PayType);
            //if (strWhere.Trim() != "")
            //{
            //    strSql.Append(" where " + strWhere);
            //}
            return DataBase.ExecuteDataset(CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Pri_Type,Value,Conv_PriType,Created,IsOpen ");
            strSql.Append(" FROM PayPriceConver ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DataBase.ExecuteDataset(CommandType.Text, strSql.ToString());
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
            strSql.Append(" ID,Pri_Type,Value,Conv_PriType,Created,IsOpen ");
            strSql.Append(" FROM PayPriceConver ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DataBase.ExecuteDataset(CommandType.Text, strSql.ToString());
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
            parameters[0].Value = "PayPriceConver";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return Database.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method
    }
}


