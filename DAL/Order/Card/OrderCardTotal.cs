using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBAccess;

//Please add references
namespace viviapi.DAL.Order.Card
{
	/// <summary>
	/// 数据访问类:ordercardtotal
	/// </summary>
	public partial class OrderCardTotal
	{
        public OrderCardTotal()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string orderid)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@orderid", SqlDbType.VarChar,30)			};
			parameters[0].Value = orderid;

			int result= DbHelperSQL.RunProcedure("proc_ordercardtotal_Exists",parameters,out rowsAffected);
			if(result==1)
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
		public int Add(viviapi.Model.Order.Card.OrderCardTotal model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@orderid", SqlDbType.VarChar,30),
					new SqlParameter("@userorderid", SqlDbType.VarChar,30),
					new SqlParameter("@userId", SqlDbType.Int,4),
					new SqlParameter("@typeId", SqlDbType.Int,4),
					new SqlParameter("@cardType", SqlDbType.Int,4),
					new SqlParameter("@cardNos", SqlDbType.VarChar,500),
					new SqlParameter("@cardPwds", SqlDbType.VarChar,500),
					new SqlParameter("@cardNum", SqlDbType.Int,4),
					new SqlParameter("@success", SqlDbType.Int,4),
					new SqlParameter("@orderAmt", SqlDbType.Decimal,9),
					new SqlParameter("@successAmt", SqlDbType.Decimal,9),
					new SqlParameter("@cardStatus", SqlDbType.VarChar,500),
					new SqlParameter("@realAmts", SqlDbType.VarChar,100),
					new SqlParameter("@status", SqlDbType.Int,4),
					new SqlParameter("@notifystatus", SqlDbType.Int,4),
					new SqlParameter("@version", SqlDbType.VarChar,10),
					new SqlParameter("@attach", SqlDbType.VarChar,255),
					new SqlParameter("@filed1", SqlDbType.VarChar,100),
					new SqlParameter("@filed2", SqlDbType.VarChar,100),
					new SqlParameter("@filed3", SqlDbType.VarChar,100),
					new SqlParameter("@filed4", SqlDbType.VarChar,100),
					new SqlParameter("@addTime", SqlDbType.DateTime),
					new SqlParameter("@completionTime", SqlDbType.DateTime),
					new SqlParameter("@referUrl", SqlDbType.VarChar,255),
					new SqlParameter("@notifyUrl", SqlDbType.VarChar,255)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.orderid;
			parameters[2].Value = model.userorderid;
			parameters[3].Value = model.userId;
			parameters[4].Value = model.typeId;
			parameters[5].Value = model.cardType;
			parameters[6].Value = model.cardNos;
			parameters[7].Value = model.cardPwds;
			parameters[8].Value = model.cardNum;
			parameters[9].Value = model.success;
			parameters[10].Value = model.orderAmt;
			parameters[11].Value = model.successAmt;
			parameters[12].Value = model.cardStatus;
			parameters[13].Value = model.realAmts;
			parameters[14].Value = model.status;
			parameters[15].Value = model.notifystatus;
			parameters[16].Value = model.version;
			parameters[17].Value = model.attach;
			parameters[18].Value = model.filed1;
			parameters[19].Value = model.filed2;
			parameters[20].Value = model.filed3;
			parameters[21].Value = model.filed4;
			parameters[22].Value = model.addTime;
			parameters[23].Value = model.completionTime;
			parameters[24].Value = model.referUrl;
			parameters[25].Value = model.notifyUrl;

			DbHelperSQL.RunProcedure("proc_ordercardtotal_add",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public bool Update(viviapi.Model.Order.Card.OrderCardTotal model)
		{
			int rowsAffected=0;
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@orderid", SqlDbType.VarChar,30),
					new SqlParameter("@userorderid", SqlDbType.VarChar,30),
					new SqlParameter("@userId", SqlDbType.Int,4),
					new SqlParameter("@typeId", SqlDbType.Int,4),
					new SqlParameter("@cardType", SqlDbType.Int,4),
					new SqlParameter("@cardNos", SqlDbType.VarChar,500),
					new SqlParameter("@cardPwds", SqlDbType.VarChar,500),
					new SqlParameter("@cardNum", SqlDbType.Int,4),
					new SqlParameter("@success", SqlDbType.Int,4),
					new SqlParameter("@orderAmt", SqlDbType.Decimal,9),
					new SqlParameter("@successAmt", SqlDbType.Decimal,9),
					new SqlParameter("@cardStatus", SqlDbType.VarChar,500),
					new SqlParameter("@realAmts", SqlDbType.VarChar,100),
					new SqlParameter("@status", SqlDbType.Int,4),
					new SqlParameter("@notifystatus", SqlDbType.Int,4),
					new SqlParameter("@version", SqlDbType.VarChar,10),
					new SqlParameter("@attach", SqlDbType.VarChar,255),
					new SqlParameter("@filed1", SqlDbType.VarChar,100),
					new SqlParameter("@filed2", SqlDbType.VarChar,100),
					new SqlParameter("@filed3", SqlDbType.VarChar,100),
					new SqlParameter("@filed4", SqlDbType.VarChar,100),
					new SqlParameter("@addTime", SqlDbType.DateTime),
					new SqlParameter("@completionTime", SqlDbType.DateTime),
					new SqlParameter("@referUrl", SqlDbType.VarChar,255),
					new SqlParameter("@notifyUrl", SqlDbType.VarChar,255)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.orderid;
			parameters[2].Value = model.userorderid;
			parameters[3].Value = model.userId;
			parameters[4].Value = model.typeId;
			parameters[5].Value = model.cardType;
			parameters[6].Value = model.cardNos;
			parameters[7].Value = model.cardPwds;
			parameters[8].Value = model.cardNum;
			parameters[9].Value = model.success;
			parameters[10].Value = model.orderAmt;
			parameters[11].Value = model.successAmt;
			parameters[12].Value = model.cardStatus;
			parameters[13].Value = model.realAmts;
			parameters[14].Value = model.status;
			parameters[15].Value = model.notifystatus;
			parameters[16].Value = model.version;
			parameters[17].Value = model.attach;
			parameters[18].Value = model.filed1;
			parameters[19].Value = model.filed2;
			parameters[20].Value = model.filed3;
			parameters[21].Value = model.filed4;
			parameters[22].Value = model.addTime;
			parameters[23].Value = model.completionTime;
			parameters[24].Value = model.referUrl;
			parameters[25].Value = model.notifyUrl;

			DbHelperSQL.RunProcedure("proc_ordercardtotal_Update",parameters,out rowsAffected);
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
        ///  更新一条数据
        /// </summary>
        public bool Notify(string orderid,string notifyUrl,int notifystatus)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = {
					new SqlParameter("@orderid", SqlDbType.VarChar,30),
					
					new SqlParameter("@notifystatus", SqlDbType.Int,4),
					
					new SqlParameter("@notifyUrl", SqlDbType.VarChar,255)};
            parameters[0].Value = orderid;
           
            parameters[1].Value = notifystatus;

            parameters[2].Value = notifyUrl;

            DbHelperSQL.RunProcedure("proc_ordercardtotal_notify", parameters, out rowsAffected);
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
			int rowsAffected=0;
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			DbHelperSQL.RunProcedure("proc_ordercardtotal_Delete",parameters,out rowsAffected);
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
		public bool Delete(string orderid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ordercardtotal ");
			strSql.Append(" where orderid=@orderid ");
			SqlParameter[] parameters = {
					new SqlParameter("@orderid", SqlDbType.VarChar,30)			};
			parameters[0].Value = orderid;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ordercardtotal ");
			strSql.Append(" where id in ("+idlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public viviapi.Model.Order.Card.OrderCardTotal GetModel(int id)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			viviapi.Model.Order.Card.OrderCardTotal model=new viviapi.Model.Order.Card.OrderCardTotal();
			DataSet ds= DbHelperSQL.RunProcedure("proc_ordercardtotal_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
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
        public viviapi.Model.Order.Card.OrderCardTotal GetModelByOrderId(string orderId)
        {
            IDataParameter[] parameters = {
					new SqlParameter("@orderid", SqlDbType.VarChar,30)
			};
            parameters[0].Value = orderId;

            viviapi.Model.Order.Card.OrderCardTotal model = new viviapi.Model.Order.Card.OrderCardTotal();
            DataSet ds = DbHelperSQL.RunProcedure("proc_ordercardtotal_GetModelByOrderId", parameters, "ds");
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
		public viviapi.Model.Order.Card.OrderCardTotal DataRowToModel(DataRow row)
		{
			viviapi.Model.Order.Card.OrderCardTotal model=new viviapi.Model.Order.Card.OrderCardTotal();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["orderid"]!=null)
				{
					model.orderid=row["orderid"].ToString();
				}
				if(row["userorderid"]!=null)
				{
					model.userorderid=row["userorderid"].ToString();
				}
				if(row["userId"]!=null && row["userId"].ToString()!="")
				{
					model.userId=int.Parse(row["userId"].ToString());
				}
				if(row["typeId"]!=null && row["typeId"].ToString()!="")
				{
					model.typeId=int.Parse(row["typeId"].ToString());
				}
				if(row["cardType"]!=null && row["cardType"].ToString()!="")
				{
					model.cardType=int.Parse(row["cardType"].ToString());
				}
				if(row["cardNos"]!=null)
				{
					model.cardNos=row["cardNos"].ToString();
				}
				if(row["cardPwds"]!=null)
				{
					model.cardPwds=row["cardPwds"].ToString();
				}
				if(row["cardNum"]!=null && row["cardNum"].ToString()!="")
				{
					model.cardNum=int.Parse(row["cardNum"].ToString());
				}
				if(row["success"]!=null && row["success"].ToString()!="")
				{
					model.success=int.Parse(row["success"].ToString());
				}
				if(row["orderAmt"]!=null && row["orderAmt"].ToString()!="")
				{
					model.orderAmt=decimal.Parse(row["orderAmt"].ToString());
				}
				if(row["successAmt"]!=null && row["successAmt"].ToString()!="")
				{
					model.successAmt=decimal.Parse(row["successAmt"].ToString());
				}
				if(row["cardStatus"]!=null)
				{
					model.cardStatus=row["cardStatus"].ToString();
				}
				if(row["realAmts"]!=null)
				{
					model.realAmts=row["realAmts"].ToString();
				}
				if(row["status"]!=null && row["status"].ToString()!="")
				{
					model.status=int.Parse(row["status"].ToString());
				}
				if(row["notifystatus"]!=null && row["notifystatus"].ToString()!="")
				{
					model.notifystatus=int.Parse(row["notifystatus"].ToString());
				}
				if(row["version"]!=null)
				{
					model.version=row["version"].ToString();
				}
				if(row["attach"]!=null)
				{
					model.attach=row["attach"].ToString();
				}
				if(row["filed1"]!=null)
				{
					model.filed1=row["filed1"].ToString();
				}
				if(row["filed2"]!=null)
				{
					model.filed2=row["filed2"].ToString();
				}
				if(row["filed3"]!=null)
				{
					model.filed3=row["filed3"].ToString();
				}
				if(row["filed4"]!=null)
				{
					model.filed4=row["filed4"].ToString();
				}
				if(row["addTime"]!=null && row["addTime"].ToString()!="")
				{
					model.addTime=DateTime.Parse(row["addTime"].ToString());
				}
				if(row["completionTime"]!=null && row["completionTime"].ToString()!="")
				{
					model.completionTime=DateTime.Parse(row["completionTime"].ToString());
				}
				if(row["referUrl"]!=null)
				{
					model.referUrl=row["referUrl"].ToString();
				}
				if(row["notifyUrl"]!=null)
				{
					model.notifyUrl=row["notifyUrl"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,orderid,userorderid,userId,typeId,cardType,cardNos,cardPwds,cardNum,success,orderAmt,successAmt,cardStatus,realAmts,status,notifystatus,version,attach,filed1,filed2,filed3,filed4,addTime,completionTime,referUrl,notifyUrl ");
			strSql.Append(" FROM ordercardtotal ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" id,orderid,userorderid,userId,typeId,cardType,cardNos,cardPwds,cardNum,success,orderAmt,successAmt,cardStatus,realAmts,status,notifystatus,version,attach,filed1,filed2,filed3,filed4,addTime,completionTime,referUrl,notifyUrl ");
			strSql.Append(" FROM ordercardtotal ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM ordercardtotal ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.id desc");
			}
			strSql.Append(")AS Row, T.*  from ordercardtotal T ");
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
			parameters[0].Value = "ordercardtotal";
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

