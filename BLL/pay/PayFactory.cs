using viviLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using viviapi.Model;
using DBAccess;
namespace viviapi.BLL
{
    /// <summary>
    /// 
    /// </summary>
    public class PayFactory
    {
        public static int Add(PayListInfo model)
        {
            SqlParameter idoutparam = DataBase.MakeOutParam("@ID", SqlDbType.Int, 4);
            SqlParameter[] prams = new SqlParameter[] { idoutparam, DataBase.MakeInParam("@Uid", SqlDbType.Int, 4, model.Uid), DataBase.MakeInParam("@Money", SqlDbType.Money, 8, model.Money), DataBase.MakeInParam("@Status", SqlDbType.Int, 4, model.Status), DataBase.MakeInParam("@AddTime", SqlDbType.DateTime, 8, model.AddTime), DataBase.MakeInParam("@PayTime", SqlDbType.DateTime, 8, model.PayTime), DataBase.MakeInParam("@Tax", SqlDbType.Money, 8, model.Tax), DataBase.MakeInParam("@Charges", SqlDbType.Money, 8, model.Charges) };
            if (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "UP_PayList_ADD", prams) == 1)
            {
                return (int) idoutparam.Value;
            }
            return 0;
        }

        //public static DataSet GetList(string strWhere)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select ID,Uid,Money,Status,AddTime,PayTime,Tax,Charges ");
        //    strSql.Append(" FROM PayList ");
        //    if (strWhere.Trim() != "")
        //    {
        //        strSql.Append(" where " + strWhere);
        //    }
        //    return Database.ExecuteDataset(CommandType.Text, strSql.ToString());
        //}

        public static List<PayListInfo> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Uid,Money,Status,AddTime,PayTime,Tax,Charges ");
            strSql.Append(" FROM PayList ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<PayListInfo> list = new List<PayListInfo>();
            using (SqlDataReader dataReader = DataBase.ExecuteReader(CommandType.Text, strSql.ToString()))
            {
                while (dataReader.Read())
                {
                    list.Add(ReaderBind(dataReader));
                }
            }
            return list;
        }

        public static PayListInfo GetModel(int id)
        {
            SqlParameter[] prams = new SqlParameter[] { DataBase.MakeInParam("@ID", SqlDbType.Int, 4, id) };
            PayListInfo model = null;
            using (SqlDataReader dataReader = DataBase.ExecuteReader(CommandType.StoredProcedure, "UP_PayList_GetModel", prams))
            {
                if (dataReader.Read())
                {
                    model = ReaderBind(dataReader);
                }
            }
            return model;
        }

        public static decimal GetPayDayMoney(int uid)
        {
            string sqlstr = "SELECT ISNULL(SUM([Amount]*[Pay_Price]),0) FROM [User_Pay_Order] where Status = 2 and datediff(day,CompleteTime,getdate())=0 and UserId=" + uid.ToString();
            return decimal.Parse(DataBase.ExecuteScalarToStr(CommandType.Text, sqlstr));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static decimal Getpayingmoney(int uid)
        {
            string sqlstr = "SELECT ISNULL(SUM([Money]),0) FROM [PayList] WHERE Status IN(0,1) AND [Uid]=" + uid.ToString();
            return decimal.Parse(DataBase.ExecuteScalarToStr(CommandType.Text, sqlstr));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public static PayListInfo ReaderBind(SqlDataReader dataReader)
        {
            PayListInfo model = new PayListInfo();
            object ojb = dataReader["ID"];
            if ((ojb != null) && (ojb != DBNull.Value))
            {
                model.ID = (int) ojb;
            }
            ojb = dataReader["Uid"];
            if ((ojb != null) && (ojb != DBNull.Value))
            {
                model.Uid = (int) ojb;
            }
            ojb = dataReader["Money"];
            if ((ojb != null) && (ojb != DBNull.Value))
            {
                model.Money = (decimal) ojb;
            }
            ojb = dataReader["Status"];
            if ((ojb != null) && (ojb != DBNull.Value))
            {
                model.Status = (int) ojb;
            }
            ojb = dataReader["AddTime"];
            if ((ojb != null) && (ojb != DBNull.Value))
            {
                model.AddTime = (DateTime) ojb;
            }
            ojb = dataReader["PayTime"];
            if ((ojb != null) && (ojb != DBNull.Value))
            {
                model.PayTime = (DateTime) ojb;
            }
            ojb = dataReader["Tax"];
            if ((ojb != null) && (ojb != DBNull.Value))
            {
                model.Tax = (decimal) ojb;
            }
            ojb = dataReader["Charges"];
            if ((ojb != null) && (ojb != DBNull.Value))
            {
                model.Charges = (decimal) ojb;
            }
            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Update(PayListInfo model)
        {
            SqlParameter[] prams = new SqlParameter[] { DataBase.MakeInParam("@ID", SqlDbType.Int, 4, model.ID), DataBase.MakeInParam("@Uid", SqlDbType.Int, 4, model.Uid), DataBase.MakeInParam("@Money", SqlDbType.Money, 8, model.Money), DataBase.MakeInParam("@Status", SqlDbType.Int, 4, model.Status), DataBase.MakeInParam("@AddTime", SqlDbType.DateTime, 8, model.AddTime), DataBase.MakeInParam("@PayTime", SqlDbType.DateTime, 8, model.PayTime), DataBase.MakeInParam("@Tax", SqlDbType.Money, 8, model.Tax), DataBase.MakeInParam("@Charges", SqlDbType.Money, 8, model.Charges) };
            return (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "UP_PayList_Update", prams) > 0);
        }
    }
}

