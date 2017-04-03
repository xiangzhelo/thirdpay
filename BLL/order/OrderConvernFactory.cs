using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using viviLib;
using viviapi.Model.Payment;
using DBAccess;

namespace viviapi.BLL.Payment
{
	/// <summary>
	/// 数据访问类:OrderConvern
	/// </summary>
	public partial class OrderConvernFactory
	{
		public OrderConvernFactory()
		{}
		#region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool Exists(int OkxrOrderId)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@OkxrOrderId ", SqlDbType.BigInt,8)};
            parameters[0].Value = OkxrOrderId;

            object result;
            DataBase.RunProc("OrderConvern_Exists", parameters, out result);
            int intrest = Convert.ToInt32(result.ToString());
            if (intrest == 1)
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
		public static int Add(OrderConvern model)
		{
            int result = 0;
            try
            {                
                SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@OkxrOrderId", SqlDbType.BigInt,8),
					new SqlParameter("@OrigOutOrderId", SqlDbType.VarChar,500),
					new SqlParameter("@OrigPayType", SqlDbType.Int,4),
					new SqlParameter("@OrigPayPrice", SqlDbType.Money,8),
					new SqlParameter("@OrigPromoney", SqlDbType.Money,8),
					new SqlParameter("@OrigAgmoney", SqlDbType.Money,8),
					new SqlParameter("@OrigProfit", SqlDbType.Money,8),
					new SqlParameter("@Amount", SqlDbType.Money,8),
					new SqlParameter("@Created", SqlDbType.DateTime),
					new SqlParameter("@ConvtPayType", SqlDbType.Int,4),
					new SqlParameter("@ConvtOutOrderId", SqlDbType.VarChar,500),
					new SqlParameter("@ConvtPayPrice", SqlDbType.Money,4),
					new SqlParameter("@ConvtAgmoney", SqlDbType.Money,8),
					new SqlParameter("@ConvtPromoney", SqlDbType.Money,8),
					new SqlParameter("@ConvtProfit", SqlDbType.Money,8),
					new SqlParameter("@DiffProfit", SqlDbType.Money,8)};
                parameters[0].Direction = ParameterDirection.Output;
                parameters[1].Value = model.OkxrOrderId;
                parameters[2].Value = model.OrigOutOrderId;
                parameters[3].Value = model.OrigPayType;
                parameters[4].Value = model.OrigPayPrice;
                parameters[5].Value = model.OrigPromoney;
                parameters[6].Value = model.OrigAgmoney;
                parameters[7].Value = model.OrigProfit;
                parameters[8].Value = model.Amount;
                parameters[9].Value = model.Created;
                parameters[10].Value = model.ConvtPayType;
                parameters[11].Value = model.ConvtOutOrderId;
                parameters[12].Value = model.ConvtPayPrice;
                parameters[13].Value = model.ConvtAgmoney;
                parameters[14].Value = model.ConvtPromoney;
                parameters[15].Value = model.ConvtProfit;
                parameters[16].Value = model.DiffProfit;

                DataBase.RunProc("OrderConvern_ADD", parameters);
                result = Convert.ToInt32(parameters[0].Value);
               
            }
            catch(Exception ex)
            {  
                result = 0;
            }
            return result;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
        public static bool Update(OrderConvern model)
		{
			int rowsAffected=0;
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@OkxrOrderId", SqlDbType.BigInt,8),
					new SqlParameter("@OrigOutOrderId", SqlDbType.VarChar,500),
					new SqlParameter("@OrigPayType", SqlDbType.Int,4),
					new SqlParameter("@OrigPayPrice", SqlDbType.Money,8),
					new SqlParameter("@OrigPromoney", SqlDbType.Money,8),
					new SqlParameter("@OrigAgmoney", SqlDbType.Money,8),
					new SqlParameter("@OrigProfit", SqlDbType.Money,8),
					new SqlParameter("@Amount", SqlDbType.Money,8),
					new SqlParameter("@Created", SqlDbType.DateTime),
					new SqlParameter("@ConvtPayType", SqlDbType.Int,4),
					new SqlParameter("@ConvtOutOrderId", SqlDbType.VarChar,500),
					new SqlParameter("@ConvtPayPrice", SqlDbType.Money,4),
					new SqlParameter("@ConvtAgmoney", SqlDbType.Money,8),
					new SqlParameter("@ConvtPromoney", SqlDbType.Money,8),
					new SqlParameter("@ConvtProfit", SqlDbType.Money,8),
					new SqlParameter("@DiffProfit", SqlDbType.Money,8)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.OkxrOrderId;
			parameters[2].Value = model.OrigOutOrderId;
			parameters[3].Value = model.OrigPayType;
			parameters[4].Value = model.OrigPayPrice;
			parameters[5].Value = model.OrigPromoney;
			parameters[6].Value = model.OrigAgmoney;
			parameters[7].Value = model.OrigProfit;
			parameters[8].Value = model.Amount;
			parameters[9].Value = model.Created;
			parameters[10].Value = model.ConvtPayType;
			parameters[11].Value = model.ConvtOutOrderId;
			parameters[12].Value = model.ConvtPayPrice;
			parameters[13].Value = model.ConvtAgmoney;
			parameters[14].Value = model.ConvtPromoney;
			parameters[15].Value = model.ConvtProfit;
			parameters[16].Value = model.DiffProfit;

            rowsAffected = DataBase.RunProc("OrderConvern_Update", parameters);
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
        public static bool Delete(int ID)
		{
			int rowsAffected=0;
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
};
			parameters[0].Value = ID;

            rowsAffected = DataBase.RunProc("OrderConvern_Delete", parameters);
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
        public static bool DeleteList(string IDlist)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from OrderConvern ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			int rows= DataBase.ExecuteNonQuery( CommandType.Text, strSql.ToString());
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
        public static OrderConvern GetModel(ulong OkxrOrderId)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@OkxrOrderId", SqlDbType.BigInt,8)
};
            parameters[0].Value = OkxrOrderId;

			OrderConvern model=new OrderConvern();
            DataSet ds = null;
            DataBase.RunProc("OrderConvern_GetModel", parameters, out ds);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OkxrOrderId"].ToString()!="")
				{
					model.OkxrOrderId= ulong.Parse(ds.Tables[0].Rows[0]["OkxrOrderId"].ToString());
				}
				model.OrigOutOrderId=ds.Tables[0].Rows[0]["OrigOutOrderId"].ToString();
				if(ds.Tables[0].Rows[0]["OrigPayType"].ToString()!="")
				{
					model.OrigPayType=int.Parse(ds.Tables[0].Rows[0]["OrigPayType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OrigPayPrice"].ToString()!="")
				{
					model.OrigPayPrice=decimal.Parse(ds.Tables[0].Rows[0]["OrigPayPrice"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OrigPromoney"].ToString()!="")
				{
					model.OrigPromoney=decimal.Parse(ds.Tables[0].Rows[0]["OrigPromoney"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OrigAgmoney"].ToString()!="")
				{
					model.OrigAgmoney=decimal.Parse(ds.Tables[0].Rows[0]["OrigAgmoney"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OrigProfit"].ToString()!="")
				{
					model.OrigProfit=decimal.Parse(ds.Tables[0].Rows[0]["OrigProfit"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Amount"].ToString()!="")
				{
					model.Amount=decimal.Parse(ds.Tables[0].Rows[0]["Amount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Created"].ToString()!="")
				{
					model.Created=DateTime.Parse(ds.Tables[0].Rows[0]["Created"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ConvtPayType"].ToString()!="")
				{
					model.ConvtPayType=int.Parse(ds.Tables[0].Rows[0]["ConvtPayType"].ToString());
				}
				model.ConvtOutOrderId=ds.Tables[0].Rows[0]["ConvtOutOrderId"].ToString();
				if(ds.Tables[0].Rows[0]["ConvtPayPrice"].ToString()!="")
				{
                    model.ConvtPayPrice = decimal.Parse(ds.Tables[0].Rows[0]["ConvtPayPrice"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ConvtAgmoney"].ToString()!="")
				{
					model.ConvtAgmoney = decimal.Parse(ds.Tables[0].Rows[0]["ConvtAgmoney"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ConvtPromoney"].ToString()!="")
				{
					model.ConvtPromoney=decimal.Parse(ds.Tables[0].Rows[0]["ConvtPromoney"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ConvtProfit"].ToString()!="")
				{
					model.ConvtProfit=decimal.Parse(ds.Tables[0].Rows[0]["ConvtProfit"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DiffProfit"].ToString()!="")
				{
					model.DiffProfit=decimal.Parse(ds.Tables[0].Rows[0]["DiffProfit"].ToString());
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
        public static DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,OkxrOrderId,OrigOutOrderId,OrigPayType,OrigPayPrice,OrigPromoney,OrigAgmoney,OrigProfit,Amount,Created,ConvtPayType,ConvtOutOrderId,ConvtPayPrice,ConvtAgmoney,ConvtPromoney,ConvtProfit,DiffProfit ");
			strSql.Append(" FROM OrderConvern ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            return DataBase.ExecuteDataset(CommandType.Text, strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
        public static DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" ID,OkxrOrderId,OrigOutOrderId,OrigPayType,OrigPayPrice,OrigPromoney,OrigAgmoney,OrigProfit,Amount,Created,ConvtPayType,ConvtOutOrderId,ConvtPayPrice,ConvtAgmoney,ConvtPromoney,ConvtProfit,DiffProfit ");
			strSql.Append(" FROM OrderConvern ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			parameters[0].Value = "OrderConvern";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method
	}
}



