using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBAccess;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.DAL.Finance
{
	/// <summary>
	/// 数据访问类:withdrawSuppTranLog
	/// </summary>
	public partial class WithdrawSuppTranLog
	{
        internal string SQL_TABLE = "withdrawSuppTranLog";
        internal string SQL_TABLE_FIELD = @"[id]
      ,[suppid]
      ,[mode]
      ,[settledId]
      ,[withdrawNo]
      ,[trade_no]
      ,[batchNo]
      ,[supp_trade_no]
      ,[userid]
      ,[balance]
      ,[bankCode]
      ,[bankName]
      ,[bankBranch]
      ,[bankAccountName]
      ,[bankAccount]
      ,[amount]
      ,[charges]
      ,[balance2]
      ,[addTime]
      ,[processingTime]
      ,[supp_message]
      ,[is_cancel]
      ,[status]
      ,[ext1]
      ,[ext2]
      ,[ext3]
      ,[remark]
      ,isnull(amount,0)+isnull(charges,0) as realpay";

        public WithdrawSuppTranLog()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string trade_no)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@trade_no", SqlDbType.VarChar,30)			};
			parameters[0].Value = trade_no;

			int result= DbHelperSQL.RunProcedure("withdrawSuppTranLog_Exists",parameters,out rowsAffected);
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
		public int Add(Model.Finance.WithdrawSuppTranLog model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@suppid", SqlDbType.Int,4),
					new SqlParameter("@mode", SqlDbType.TinyInt,1),
					new SqlParameter("@settledId", SqlDbType.Int,4),
					new SqlParameter("@withdrawNo", SqlDbType.VarChar,30),
					new SqlParameter("@trade_no", SqlDbType.VarChar,30),
					new SqlParameter("@batchNo", SqlDbType.Int,4),
					new SqlParameter("@supp_trade_no", SqlDbType.VarChar,50),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@balance", SqlDbType.Decimal,9),
					new SqlParameter("@bankCode", SqlDbType.VarChar,50),
					new SqlParameter("@bankName", SqlDbType.NVarChar,20),
					new SqlParameter("@bankBranch", SqlDbType.NVarChar,255),
					new SqlParameter("@bankAccountName", SqlDbType.NVarChar,20),
					new SqlParameter("@bankAccount", SqlDbType.VarChar,50),
					new SqlParameter("@amount", SqlDbType.Decimal,9),
					new SqlParameter("@charges", SqlDbType.Decimal,9),
					new SqlParameter("@balance2", SqlDbType.Decimal,9),
					new SqlParameter("@addTime", SqlDbType.DateTime),
					new SqlParameter("@processingTime", SqlDbType.DateTime),
					new SqlParameter("@supp_message", SqlDbType.NVarChar,200),
					new SqlParameter("@is_cancel", SqlDbType.Bit,1),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@ext1", SqlDbType.VarChar,50),
					new SqlParameter("@ext2", SqlDbType.VarChar,50),
					new SqlParameter("@ext3", SqlDbType.VarChar,50),
					new SqlParameter("@remark", SqlDbType.NVarChar,500)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.suppid;
			parameters[2].Value = model.mode;
			parameters[3].Value = model.settledId;
			parameters[4].Value = model.withdrawNo;
			parameters[5].Value = model.trade_no;
			parameters[6].Value = model.batchNo;
			parameters[7].Value = model.supp_trade_no;
			parameters[8].Value = model.userid;
			parameters[9].Value = model.balance;
			parameters[10].Value = model.bankCode;
			parameters[11].Value = model.bankName;
			parameters[12].Value = model.bankBranch;
			parameters[13].Value = model.bankAccountName;
			parameters[14].Value = model.bankAccount;
			parameters[15].Value = model.amount;
			parameters[16].Value = model.charges;
			parameters[17].Value = model.balance2;
			parameters[18].Value = model.addTime;
			parameters[19].Value = model.processingTime;
			parameters[20].Value = model.supp_message;
			parameters[21].Value = model.is_cancel;
			parameters[22].Value = model.status;
			parameters[23].Value = model.ext1;
			parameters[24].Value = model.ext2;
			parameters[25].Value = model.ext3;
			parameters[26].Value = model.remark;

            DbHelperSQL.RunProcedure("proc_withdrawSuppTranLog_add", parameters, out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public bool Update(Model.Finance.WithdrawSuppTranLog model)
		{
			int rowsAffected=0;
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@suppid", SqlDbType.Int,4),
					new SqlParameter("@mode", SqlDbType.TinyInt,1),
					new SqlParameter("@settledId", SqlDbType.Int,4),
					new SqlParameter("@withdrawNo", SqlDbType.VarChar,30),
					new SqlParameter("@trade_no", SqlDbType.VarChar,30),
					new SqlParameter("@batchNo", SqlDbType.Int,4),
					new SqlParameter("@supp_trade_no", SqlDbType.VarChar,50),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@balance", SqlDbType.Decimal,9),
					new SqlParameter("@bankCode", SqlDbType.VarChar,50),
					new SqlParameter("@bankName", SqlDbType.NVarChar,20),
					new SqlParameter("@bankBranch", SqlDbType.NVarChar,255),
					new SqlParameter("@bankAccountName", SqlDbType.NVarChar,20),
					new SqlParameter("@bankAccount", SqlDbType.VarChar,50),
					new SqlParameter("@amount", SqlDbType.Decimal,9),
					new SqlParameter("@charges", SqlDbType.Decimal,9),
					new SqlParameter("@balance2", SqlDbType.Decimal,9),
					new SqlParameter("@addTime", SqlDbType.DateTime),
					new SqlParameter("@processingTime", SqlDbType.DateTime),
					new SqlParameter("@supp_message", SqlDbType.NVarChar,200),
					new SqlParameter("@is_cancel", SqlDbType.Bit,1),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@ext1", SqlDbType.VarChar,50),
					new SqlParameter("@ext2", SqlDbType.VarChar,50),
					new SqlParameter("@ext3", SqlDbType.VarChar,50),
					new SqlParameter("@remark", SqlDbType.NVarChar,500)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.suppid;
			parameters[2].Value = model.mode;
			parameters[3].Value = model.settledId;
			parameters[4].Value = model.withdrawNo;
			parameters[5].Value = model.trade_no;
			parameters[6].Value = model.batchNo;
			parameters[7].Value = model.supp_trade_no;
			parameters[8].Value = model.userid;
			parameters[9].Value = model.balance;
			parameters[10].Value = model.bankCode;
			parameters[11].Value = model.bankName;
			parameters[12].Value = model.bankBranch;
			parameters[13].Value = model.bankAccountName;
			parameters[14].Value = model.bankAccount;
			parameters[15].Value = model.amount;
			parameters[16].Value = model.charges;
			parameters[17].Value = model.balance2;
			parameters[18].Value = model.addTime;
			parameters[19].Value = model.processingTime;
			parameters[20].Value = model.supp_message;
			parameters[21].Value = model.is_cancel;
			parameters[22].Value = model.status;
			parameters[23].Value = model.ext1;
			parameters[24].Value = model.ext2;
			parameters[25].Value = model.ext3;
			parameters[26].Value = model.remark;

			DbHelperSQL.RunProcedure("withdrawSuppTranLog_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("withdrawSuppTranLog_Delete",parameters,out rowsAffected);
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
		public bool Delete(string trade_no)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from withdrawSuppTranLog ");
			strSql.Append(" where trade_no=@trade_no ");
			SqlParameter[] parameters = {
					new SqlParameter("@trade_no", SqlDbType.VarChar,30)			};
			parameters[0].Value = trade_no;

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
			strSql.Append("delete from withdrawSuppTranLog ");
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
		public Model.Finance.WithdrawSuppTranLog GetModel(int id)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			Model.Finance.WithdrawSuppTranLog model=new Model.Finance.WithdrawSuppTranLog();
			DataSet ds= DbHelperSQL.RunProcedure("withdrawSuppTranLog_GetModel",parameters,"ds");
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
		public Model.Finance.WithdrawSuppTranLog DataRowToModel(DataRow row)
		{
			Model.Finance.WithdrawSuppTranLog model=new Model.Finance.WithdrawSuppTranLog();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["suppid"]!=null && row["suppid"].ToString()!="")
				{
					model.suppid=int.Parse(row["suppid"].ToString());
				}
				if(row["mode"]!=null && row["mode"].ToString()!="")
				{
					model.mode=int.Parse(row["mode"].ToString());
				}
				if(row["settledId"]!=null && row["settledId"].ToString()!="")
				{
					model.settledId=int.Parse(row["settledId"].ToString());
				}
				if(row["withdrawNo"]!=null)
				{
					model.withdrawNo=row["withdrawNo"].ToString();
				}
				if(row["trade_no"]!=null)
				{
					model.trade_no=row["trade_no"].ToString();
				}
				if(row["batchNo"]!=null && row["batchNo"].ToString()!="")
				{
					model.batchNo=int.Parse(row["batchNo"].ToString());
				}
				if(row["supp_trade_no"]!=null)
				{
					model.supp_trade_no=row["supp_trade_no"].ToString();
				}
				if(row["userid"]!=null && row["userid"].ToString()!="")
				{
					model.userid=int.Parse(row["userid"].ToString());
				}
				if(row["balance"]!=null && row["balance"].ToString()!="")
				{
					model.balance=decimal.Parse(row["balance"].ToString());
				}
				if(row["bankCode"]!=null)
				{
					model.bankCode=row["bankCode"].ToString();
				}
				if(row["bankName"]!=null)
				{
					model.bankName=row["bankName"].ToString();
				}
				if(row["bankBranch"]!=null)
				{
					model.bankBranch=row["bankBranch"].ToString();
				}
				if(row["bankAccountName"]!=null)
				{
					model.bankAccountName=row["bankAccountName"].ToString();
				}
				if(row["bankAccount"]!=null)
				{
					model.bankAccount=row["bankAccount"].ToString();
				}
				if(row["amount"]!=null && row["amount"].ToString()!="")
				{
					model.amount=decimal.Parse(row["amount"].ToString());
				}
				if(row["charges"]!=null && row["charges"].ToString()!="")
				{
					model.charges=decimal.Parse(row["charges"].ToString());
				}
				if(row["balance2"]!=null && row["balance2"].ToString()!="")
				{
					model.balance2=decimal.Parse(row["balance2"].ToString());
				}
				if(row["addTime"]!=null && row["addTime"].ToString()!="")
				{
					model.addTime=DateTime.Parse(row["addTime"].ToString());
				}
				if(row["processingTime"]!=null && row["processingTime"].ToString()!="")
				{
					model.processingTime=DateTime.Parse(row["processingTime"].ToString());
				}
				if(row["supp_message"]!=null)
				{
					model.supp_message=row["supp_message"].ToString();
				}
				if(row["is_cancel"]!=null && row["is_cancel"].ToString()!="")
				{
					if((row["is_cancel"].ToString()=="1")||(row["is_cancel"].ToString().ToLower()=="true"))
					{
						model.is_cancel=true;
					}
					else
					{
						model.is_cancel=false;
					}
				}
				if(row["status"]!=null && row["status"].ToString()!="")
				{
					model.status=int.Parse(row["status"].ToString());
				}
				if(row["ext1"]!=null)
				{
					model.ext1=row["ext1"].ToString();
				}
				if(row["ext2"]!=null)
				{
					model.ext2=row["ext2"].ToString();
				}
				if(row["ext3"]!=null)
				{
					model.ext3=row["ext3"].ToString();
				}
				if(row["remark"]!=null)
				{
					model.remark=row["remark"].ToString();
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
			strSql.Append("select id,suppid,mode,settledId,withdrawNo,trade_no,batchNo,supp_trade_no,userid,balance,bankCode,bankName,bankBranch,bankAccountName,bankAccount,amount,charges,balance2,addTime,processingTime,supp_message,is_cancel,status,ext1,ext2,ext3,remark ");
			strSql.Append(" FROM withdrawSuppTranLog ");
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
			strSql.Append(" id,suppid,mode,settledId,withdrawNo,trade_no,batchNo,supp_trade_no,userid,balance,bankCode,bankName,bankBranch,bankAccountName,bankAccount,amount,charges,balance2,addTime,processingTime,supp_message,is_cancel,status,ext1,ext2,ext3,remark ");
			strSql.Append(" FROM withdrawSuppTranLog ");
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
			strSql.Append("select count(1) FROM withdrawSuppTranLog ");
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
			strSql.Append(")AS Row, T.*  from withdrawSuppTranLog T ");
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
			parameters[0].Value = "withdrawSuppTranLog";
			parameters[1].Value = "id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method

        #region Process
        /// <summary>
        /// 
        /// </summary>
        /// <param name="suppId"></param>
        /// <param name="tradeNo"></param>
        /// <param name="isCancel"></param>
        /// <param name="status">
        /// 2 处理成功
        /// 4 处理失败
        /// </param>
        /// <param name="amount"></param>
        /// <param name="suppTradeNo"></param>
        /// <param name="message"></param>
        /// <param name="billTradeNo"></param>
        /// <returns>
        /// 0 -- 处理成功
        /// 1 -- 无效单
        /// 2 -- 无效接口商
        /// 3 -- 已取消
        /// 4 -- 已处理完成
        /// 
        /// 99 --处理失败
        /// </returns>
        public int Process(int suppId, string tradeNo, bool isCancel, int status, string amount, string suppTradeNo, string message, out string billTradeNo)
        {
            billTradeNo = string.Empty;

            SqlParameter[] parameters = {					
					new SqlParameter("@suppid", SqlDbType.Int,4),	
					new SqlParameter("@trade_no", SqlDbType.VarChar,30),					
					new SqlParameter("@supp_trade_no", SqlDbType.VarChar,50),	
                    new SqlParameter("@is_cancel", SqlDbType.Bit,1),
				    new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@amount", SqlDbType.Decimal,9),
                    new SqlParameter("@processingTime", SqlDbType.DateTime,8),
					new SqlParameter("@supp_message", SqlDbType.NVarChar,200)};
            parameters[0].Value = suppId;
            parameters[1].Value = tradeNo;
            parameters[2].Value = suppTradeNo;
            parameters[3].Value = isCancel;
            parameters[4].Value = status;
            parameters[5].Value = decimal.Parse(amount);
            parameters[6].Value = DateTime.Now;
            parameters[7].Value = message;

            DataTable data = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_withdrawSuppTranLog_process", parameters).Tables[0];

            if (data != null && data.Rows.Count > 0)
            {
                DataRow row = data.Rows[0];

                billTradeNo = Convert.ToString(row["bill_trade_no"]);

                return Convert.ToInt32(row["result"]);
            }

            return 99;
        }
        #endregion

        #region  MethodEx

        #endregion  MethodEx

        #region PageSearch
        /// <summary>
        /// </summary>
        /// <param name="searchParams"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            var ds = new DataSet();
            try
            {
                string tables = SQL_TABLE;
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "id desc";
                }

                var paramList = new List<SqlParameter>();
                string userSearchWhere = BuilderWhere(searchParams, paramList);

                string sql = SqlHelper.GetCountSQL(tables, userSearchWhere, string.Empty) + "\r\n" +
                             SqlHelper.GetPageSelectSQL(SQL_TABLE_FIELD, tables, userSearchWhere, orderby, key, pageSize,
                                 page, false);


                ds = DataBase.ExecuteDataset(CommandType.Text, sql, paramList.ToArray());

                return ds;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return ds;
            }
        }

        private string BuilderWhere(List<SearchParam> param, List<SqlParameter> paramList)
        {
            var builder = new StringBuilder(" 1 = 1");

            if ((param != null) && (param.Count > 0))
            {
                for (int i = 0; i < param.Count; i++)
                {
                    SqlParameter parameter;
                    SearchParam iparam = param[i];
                    switch (iparam.ParamKey.Trim().ToLower())
                    {
                        case "userid":
                            builder.Append(" AND [userid] = @userid");
                            parameter = new SqlParameter("@userid", SqlDbType.Int);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "trade_no":
                            builder.Append(" AND [trade_no] like @trade_no");
                            parameter = new SqlParameter("@trade_no", SqlDbType.VarChar, 30);
                            parameter.Value = "%" + SqlHelper.CleanString((string)iparam.ParamValue, 30) + "%";
                            paramList.Add(parameter);
                            break;
                        case "bankaccount":
                            builder.Append(" AND [bankAccount] like @bankAccount");
                            parameter = new SqlParameter("@bankAccount", SqlDbType.VarChar, 30);
                            parameter.Value = "%" + SqlHelper.CleanString((string)iparam.ParamValue, 30) + "%";
                            paramList.Add(parameter);
                            break;
                        case "bankcode":
                            builder.Append(" AND [bankCode] = @bankCode");
                            parameter = new SqlParameter("@bankCode", SqlDbType.VarChar, 20);
                            parameter.Value = "%" + SqlHelper.CleanString((string)iparam.ParamValue, 20) + "%";
                            paramList.Add(parameter);
                            break;
                        case "stime":
                            builder.Append(" AND [processingTime] >= @stime");
                            parameter = new SqlParameter("@stime", SqlDbType.DateTime);
                            parameter.Value = iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "etime":
                            builder.Append(" AND [processingTime] <= @etime");
                            parameter = new SqlParameter("@etime", SqlDbType.DateTime);
                            parameter.Value = iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                    }
                }
            }
            return builder.ToString();
        }
        #endregion
    }
}

