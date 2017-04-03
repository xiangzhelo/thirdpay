using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
//Please add references
using DBAccess;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.DAL.Finance.Agent
{
	/// <summary>
	/// 数据访问类:withdrawAgent
	/// </summary>
	public partial class WithdrawAgent
	{
        internal string FIELDS = @"[id]
      ,[mode]
      ,[trade_no]
      ,[out_trade_no]
      ,[service]
      ,[userid]
      ,[sign_type]
      ,[return_url]
      ,[bankCode]
      ,[bankName]
      ,[bankBranch]
      ,[bankAccountName]
      ,[bankAccount]
      ,[amount]
      ,[charge]
      ,[addTime]
      ,[processingTime]
      ,[audit_status]
      ,[payment_status]
      ,[is_cancel]
      ,[ext1]
      ,[ext2]
      ,[ext3]
      ,[remark]
      ,[tranApi]
      ,[notifyTimes]
      ,[notifystatus]
      ,[callbackText]
      ,[username]
      ,[input_charset]
      ,[suppstatus]
      ,[lotno]
      ,[serial]
      ,[totalamt]
      ,[issure]";
        internal string SQL_TABLE = "v_withdrawAgent";

		public WithdrawAgent()
		{}
		#region  Method

        #region Exists
        /// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string trade_no)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@trade_no", SqlDbType.VarChar,40)			};
			parameters[0].Value = trade_no;

            int result = DbHelperSQL.RunProcedure("proc_withdrawAgent_Exists", parameters, out rowsAffected);
			if(result==1)
			{
				return true;
			}
			else
			{
				return false;
			}
        }
        #endregion

        #region Add
        /// <summary>
		///  增加一条数据
		/// </summary>
		public int Add(Model.Finance.Agent.WithdrawAgent model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@mode", SqlDbType.TinyInt,1),
					new SqlParameter("@lotno", SqlDbType.VarChar,30),
					new SqlParameter("@serial", SqlDbType.Int,4),
					new SqlParameter("@trade_no", SqlDbType.VarChar,40),
					new SqlParameter("@out_trade_no", SqlDbType.VarChar,64),
					new SqlParameter("@service", SqlDbType.VarChar,40),
					new SqlParameter("@input_charset", SqlDbType.VarChar,20),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@sign_type", SqlDbType.VarChar,8),
					new SqlParameter("@return_url", SqlDbType.VarChar,256),
					new SqlParameter("@provinceCode", SqlDbType.VarChar,50),
					new SqlParameter("@cityCode", SqlDbType.VarChar,50),
					new SqlParameter("@bankProvince", SqlDbType.VarChar,50),
					new SqlParameter("@bankCity", SqlDbType.VarChar,50),
					new SqlParameter("@bankCode", SqlDbType.VarChar,10),
					new SqlParameter("@bankName", SqlDbType.NVarChar,20),
					new SqlParameter("@bankBranch", SqlDbType.NVarChar,255),
					new SqlParameter("@bankAccountName", SqlDbType.NVarChar,20),
					new SqlParameter("@bankaccoutType", SqlDbType.TinyInt,1),
					new SqlParameter("@bankAccount", SqlDbType.VarChar,50),
					new SqlParameter("@amount", SqlDbType.Decimal,9),
					new SqlParameter("@charge", SqlDbType.Decimal,9),
					new SqlParameter("@totalamt", SqlDbType.Decimal,9),
					new SqlParameter("@addTime", SqlDbType.DateTime),
					new SqlParameter("@processingTime", SqlDbType.DateTime),
					new SqlParameter("@audit_status", SqlDbType.TinyInt,1),
					new SqlParameter("@auditTime", SqlDbType.DateTime),
					new SqlParameter("@auditUser", SqlDbType.Int,4),
					new SqlParameter("@auditUserName", SqlDbType.VarChar,50),
					new SqlParameter("@payment_status", SqlDbType.TinyInt,1),
					new SqlParameter("@is_cancel", SqlDbType.Bit,1),
					new SqlParameter("@ext1", SqlDbType.VarChar,50),
					new SqlParameter("@ext2", SqlDbType.VarChar,50),
					new SqlParameter("@ext3", SqlDbType.VarChar,50),
					new SqlParameter("@remark", SqlDbType.NVarChar,500),
					new SqlParameter("@tranApi", SqlDbType.Int,4),
					new SqlParameter("@suppstatus", SqlDbType.TinyInt,1),
					new SqlParameter("@notifyTimes", SqlDbType.Int,4),
					new SqlParameter("@notifystatus", SqlDbType.TinyInt,1),
					new SqlParameter("@callbackText", SqlDbType.NVarChar,50),
					new SqlParameter("@issure", SqlDbType.TinyInt,1),
					new SqlParameter("@suretime", SqlDbType.DateTime),
					new SqlParameter("@sureclientip", SqlDbType.VarChar,50),
					new SqlParameter("@sureuser", SqlDbType.NVarChar,50)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.mode;
			parameters[2].Value = model.lotno;
			parameters[3].Value = model.serial;
			parameters[4].Value = model.trade_no;
			parameters[5].Value = model.out_trade_no;
			parameters[6].Value = model.service;
			parameters[7].Value = model.input_charset;
			parameters[8].Value = model.userid;
			parameters[9].Value = model.sign_type;
			parameters[10].Value = model.return_url;
			parameters[11].Value = model.provinceCode;
			parameters[12].Value = model.cityCode;
			parameters[13].Value = model.bankProvince;
			parameters[14].Value = model.bankCity;
			parameters[15].Value = model.bankCode;
			parameters[16].Value = model.bankName;
			parameters[17].Value = model.bankBranch;
			parameters[18].Value = model.bankAccountName;
			parameters[19].Value = model.bankaccoutType;
			parameters[20].Value = model.bankAccount;
			parameters[21].Value = model.amount;
			parameters[22].Value = model.charge;
			parameters[23].Value = model.totalamt;
			parameters[24].Value = model.addTime;
			parameters[25].Value = model.processingTime;
			parameters[26].Value = model.audit_status;
			parameters[27].Value = model.auditTime;
			parameters[28].Value = model.auditUser;
			parameters[29].Value = model.auditUserName;
			parameters[30].Value = model.payment_status;
			parameters[31].Value = model.is_cancel;
			parameters[32].Value = model.ext1;
			parameters[33].Value = model.ext2;
			parameters[34].Value = model.ext3;
			parameters[35].Value = model.remark;
			parameters[36].Value = model.tranApi;
			parameters[37].Value = model.suppstatus;
			parameters[38].Value = model.notifyTimes;
			parameters[39].Value = model.notifystatus;
			parameters[40].Value = model.callbackText;
			parameters[41].Value = model.issure;
			parameters[42].Value = model.suretime;
			parameters[43].Value = model.sureclientip;
			parameters[44].Value = model.sureuser;

            DbHelperSQL.RunProcedure("proc_withdrawAgent_add", parameters, out rowsAffected);
			return (int)parameters[0].Value;
        }
        #endregion

        #region Update
        /// <summary>
		///  更新一条数据
		/// </summary>
		public bool Update(Model.Finance.Agent.WithdrawAgent model)
		{
			int rowsAffected=0;
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@mode", SqlDbType.TinyInt,1),
					new SqlParameter("@lotno", SqlDbType.VarChar,30),
					new SqlParameter("@serial", SqlDbType.Int,4),
					new SqlParameter("@trade_no", SqlDbType.VarChar,40),
					new SqlParameter("@out_trade_no", SqlDbType.VarChar,64),
					new SqlParameter("@service", SqlDbType.VarChar,40),
					new SqlParameter("@input_charset", SqlDbType.VarChar,20),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@sign_type", SqlDbType.VarChar,8),
					new SqlParameter("@return_url", SqlDbType.VarChar,256),
					new SqlParameter("@provinceCode", SqlDbType.VarChar,50),
					new SqlParameter("@cityCode", SqlDbType.VarChar,50),
					new SqlParameter("@bankProvince", SqlDbType.VarChar,50),
					new SqlParameter("@bankCity", SqlDbType.VarChar,50),
					new SqlParameter("@bankCode", SqlDbType.VarChar,10),
					new SqlParameter("@bankName", SqlDbType.NVarChar,20),
					new SqlParameter("@bankBranch", SqlDbType.NVarChar,255),
					new SqlParameter("@bankAccountName", SqlDbType.NVarChar,20),
					new SqlParameter("@bankaccoutType", SqlDbType.TinyInt,1),
					new SqlParameter("@bankAccount", SqlDbType.VarChar,50),
					new SqlParameter("@amount", SqlDbType.Decimal,9),
					new SqlParameter("@charge", SqlDbType.Decimal,9),
					new SqlParameter("@totalamt", SqlDbType.Decimal,9),
					new SqlParameter("@addTime", SqlDbType.DateTime),
					new SqlParameter("@processingTime", SqlDbType.DateTime),
					new SqlParameter("@audit_status", SqlDbType.TinyInt,1),
					new SqlParameter("@auditTime", SqlDbType.DateTime),
					new SqlParameter("@auditUser", SqlDbType.Int,4),
					new SqlParameter("@auditUserName", SqlDbType.VarChar,50),
					new SqlParameter("@payment_status", SqlDbType.TinyInt,1),
					new SqlParameter("@is_cancel", SqlDbType.Bit,1),
					new SqlParameter("@ext1", SqlDbType.VarChar,50),
					new SqlParameter("@ext2", SqlDbType.VarChar,50),
					new SqlParameter("@ext3", SqlDbType.VarChar,50),
					new SqlParameter("@remark", SqlDbType.NVarChar,500),
					new SqlParameter("@tranApi", SqlDbType.Int,4),
					new SqlParameter("@suppstatus", SqlDbType.TinyInt,1),
					new SqlParameter("@notifyTimes", SqlDbType.Int,4),
					new SqlParameter("@notifystatus", SqlDbType.TinyInt,1),
					new SqlParameter("@callbackText", SqlDbType.NVarChar,50),
					new SqlParameter("@issure", SqlDbType.TinyInt,1),
					new SqlParameter("@suretime", SqlDbType.DateTime),
					new SqlParameter("@sureclientip", SqlDbType.VarChar,50),
					new SqlParameter("@sureuser", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.mode;
			parameters[2].Value = model.lotno;
			parameters[3].Value = model.serial;
			parameters[4].Value = model.trade_no;
			parameters[5].Value = model.out_trade_no;
			parameters[6].Value = model.service;
			parameters[7].Value = model.input_charset;
			parameters[8].Value = model.userid;
			parameters[9].Value = model.sign_type;
			parameters[10].Value = model.return_url;
			parameters[11].Value = model.provinceCode;
			parameters[12].Value = model.cityCode;
			parameters[13].Value = model.bankProvince;
			parameters[14].Value = model.bankCity;
			parameters[15].Value = model.bankCode;
			parameters[16].Value = model.bankName;
			parameters[17].Value = model.bankBranch;
			parameters[18].Value = model.bankAccountName;
			parameters[19].Value = model.bankaccoutType;
			parameters[20].Value = model.bankAccount;
			parameters[21].Value = model.amount;
			parameters[22].Value = model.charge;
			parameters[23].Value = model.totalamt;
			parameters[24].Value = model.addTime;
			parameters[25].Value = model.processingTime;
			parameters[26].Value = model.audit_status;
			parameters[27].Value = model.auditTime;
			parameters[28].Value = model.auditUser;
			parameters[29].Value = model.auditUserName;
			parameters[30].Value = model.payment_status;
			parameters[31].Value = model.is_cancel;
			parameters[32].Value = model.ext1;
			parameters[33].Value = model.ext2;
			parameters[34].Value = model.ext3;
			parameters[35].Value = model.remark;
			parameters[36].Value = model.tranApi;
			parameters[37].Value = model.suppstatus;
			parameters[38].Value = model.notifyTimes;
			parameters[39].Value = model.notifystatus;
			parameters[40].Value = model.callbackText;
			parameters[41].Value = model.issure;
			parameters[42].Value = model.suretime;
			parameters[43].Value = model.sureclientip;
			parameters[44].Value = model.sureuser;

            DbHelperSQL.RunProcedure("proc_withdrawAgent_Update", parameters, out rowsAffected);
			if (rowsAffected > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
        }
        #endregion

        #region Delete
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

            DbHelperSQL.RunProcedure("proc_withdrawAgent_Delete", parameters, out rowsAffected);
			if (rowsAffected > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
        }
        #endregion

        #region Delete trade_no
        /// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string trade_no)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from withdrawAgent ");
			strSql.Append(" where trade_no=@trade_no ");
			SqlParameter[] parameters = {
					new SqlParameter("@trade_no", SqlDbType.VarChar,40)			};
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
        #endregion 

        #region DeleteList
        /// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from withdrawAgent ");
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
        #endregion

        #region GetModel
        /// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.Finance.Agent.WithdrawAgent GetModel(int id)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			Model.Finance.Agent.WithdrawAgent model=new Model.Finance.Agent.WithdrawAgent();
            DataSet ds = DbHelperSQL.RunProcedure("proc_withdrawAgent_getmodel", parameters, "ds");
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
        ///     得到一个对象实体
        /// </summary>
        public Model.Finance.Agent.WithdrawAgent GetModel(string trade_no)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@trade_no", SqlDbType.VarChar, 40)
            };
            parameters[0].Value = trade_no;

            DataSet ds = DbHelperSQL.RunProcedure("proc_withdrawAgent_getmodel2", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            return null;
        }


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.Finance.Agent.WithdrawAgent DataRowToModel(DataRow row)
		{
			Model.Finance.Agent.WithdrawAgent model=new Model.Finance.Agent.WithdrawAgent();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["mode"]!=null && row["mode"].ToString()!="")
				{
					model.mode=int.Parse(row["mode"].ToString());
				}
				if(row["lotno"]!=null)
				{
					model.lotno=row["lotno"].ToString();
				}
				if(row["serial"]!=null && row["serial"].ToString()!="")
				{
					model.serial=int.Parse(row["serial"].ToString());
				}
				if(row["trade_no"]!=null)
				{
					model.trade_no=row["trade_no"].ToString();
				}
				if(row["out_trade_no"]!=null)
				{
					model.out_trade_no=row["out_trade_no"].ToString();
				}
				if(row["service"]!=null)
				{
					model.service=row["service"].ToString();
				}
				if(row["input_charset"]!=null)
				{
					model.input_charset=row["input_charset"].ToString();
				}
				if(row["userid"]!=null && row["userid"].ToString()!="")
				{
					model.userid=int.Parse(row["userid"].ToString());
				}
				if(row["sign_type"]!=null)
				{
					model.sign_type=row["sign_type"].ToString();
				}
				if(row["return_url"]!=null)
				{
					model.return_url=row["return_url"].ToString();
				}
				if(row["provinceCode"]!=null)
				{
					model.provinceCode=row["provinceCode"].ToString();
				}
				if(row["cityCode"]!=null)
				{
					model.cityCode=row["cityCode"].ToString();
				}
				if(row["bankProvince"]!=null)
				{
					model.bankProvince=row["bankProvince"].ToString();
				}
				if(row["bankCity"]!=null)
				{
					model.bankCity=row["bankCity"].ToString();
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
				if(row["bankaccoutType"]!=null && row["bankaccoutType"].ToString()!="")
				{
					model.bankaccoutType=int.Parse(row["bankaccoutType"].ToString());
				}
				if(row["bankAccount"]!=null)
				{
					model.bankAccount=row["bankAccount"].ToString();
				}
				if(row["amount"]!=null && row["amount"].ToString()!="")
				{
					model.amount=decimal.Parse(row["amount"].ToString());
				}
				if(row["charge"]!=null && row["charge"].ToString()!="")
				{
					model.charge=decimal.Parse(row["charge"].ToString());
				}
				if(row["totalamt"]!=null && row["totalamt"].ToString()!="")
				{
					model.totalamt=decimal.Parse(row["totalamt"].ToString());
				}
				if(row["addTime"]!=null && row["addTime"].ToString()!="")
				{
					model.addTime=DateTime.Parse(row["addTime"].ToString());
				}
				if(row["processingTime"]!=null && row["processingTime"].ToString()!="")
				{
					model.processingTime=DateTime.Parse(row["processingTime"].ToString());
				}
				if(row["audit_status"]!=null && row["audit_status"].ToString()!="")
				{
					model.audit_status=int.Parse(row["audit_status"].ToString());
				}
				if(row["auditTime"]!=null && row["auditTime"].ToString()!="")
				{
					model.auditTime=DateTime.Parse(row["auditTime"].ToString());
				}
				if(row["auditUser"]!=null && row["auditUser"].ToString()!="")
				{
					model.auditUser=int.Parse(row["auditUser"].ToString());
				}
				if(row["auditUserName"]!=null)
				{
					model.auditUserName=row["auditUserName"].ToString();
				}
				if(row["payment_status"]!=null && row["payment_status"].ToString()!="")
				{
					model.payment_status=int.Parse(row["payment_status"].ToString());
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
				if(row["tranApi"]!=null && row["tranApi"].ToString()!="")
				{
					model.tranApi=int.Parse(row["tranApi"].ToString());
				}
				if(row["suppstatus"]!=null && row["suppstatus"].ToString()!="")
				{
					model.suppstatus=int.Parse(row["suppstatus"].ToString());
				}
				if(row["notifyTimes"]!=null && row["notifyTimes"].ToString()!="")
				{
					model.notifyTimes=int.Parse(row["notifyTimes"].ToString());
				}
				if(row["notifystatus"]!=null && row["notifystatus"].ToString()!="")
				{
					model.notifystatus=int.Parse(row["notifystatus"].ToString());
				}
				if(row["callbackText"]!=null)
				{
					model.callbackText=row["callbackText"].ToString();
				}
				if(row["issure"]!=null && row["issure"].ToString()!="")
				{
					model.issure=int.Parse(row["issure"].ToString());
				}
				if(row["suretime"]!=null && row["suretime"].ToString()!="")
				{
					model.suretime=DateTime.Parse(row["suretime"].ToString());
				}
				if(row["sureclientip"]!=null)
				{
					model.sureclientip=row["sureclientip"].ToString();
				}
				if(row["sureuser"]!=null)
				{
					model.sureuser=row["sureuser"].ToString();
				}
			}
			return model;
        }
        #endregion

        #region GetList
        /// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,mode,lotno,serial,trade_no,out_trade_no,service,input_charset,userid,sign_type,return_url,provinceCode,cityCode,bankProvince,bankCity,bankCode,bankName,bankBranch,bankAccountName,bankaccoutType,bankAccount,amount,charge,totalamt,addTime,processingTime,audit_status,auditTime,auditUser,auditUserName,payment_status,is_cancel,ext1,ext2,ext3,remark,tranApi,suppstatus,notifyTimes,notifystatus,callbackText,issure,suretime,sureclientip,sureuser ");
			strSql.Append(" FROM withdrawAgent ");
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
			strSql.Append(" id,mode,lotno,serial,trade_no,out_trade_no,service,input_charset,userid,sign_type,return_url,provinceCode,cityCode,bankProvince,bankCity,bankCode,bankName,bankBranch,bankAccountName,bankaccoutType,bankAccount,amount,charge,totalamt,addTime,processingTime,audit_status,auditTime,auditUser,auditUserName,payment_status,is_cancel,ext1,ext2,ext3,remark,tranApi,suppstatus,notifyTimes,notifystatus,callbackText,issure,suretime,sureclientip,sureuser ");
			strSql.Append(" FROM withdrawAgent ");
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
			strSql.Append("select count(1) FROM withdrawAgent ");
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
			strSql.Append(")AS Row, T.*  from withdrawAgent T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion
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
			parameters[0].Value = "withdrawAgent";
			parameters[1].Value = "id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method

        #region 分页

        /// <summary>
        /// </summary>
        /// <param name="searchParams"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby, byte isstat)
        {
            var ds = new DataSet();
            try
            {
                string tables = SQL_TABLE;
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "addTime desc";
                }

                var paramList = new List<SqlParameter>();
                string where = BuilderWhere(searchParams, paramList);

                string sql = SqlHelper.GetCountSQL(tables, where, string.Empty) + "\r\n" +
                             SqlHelper.GetPageSelectSQL(FIELDS, tables, where, orderby, key, pageSize, page, false);

                //+ "\r\n" + "select ISNULL(sum(amount),0) from V_Settled where " + where;

                if (isstat == 1)
                {
                    sql += "\r\n" +
                           "select sum(amount) as amount,sum(charge) as charge,sum(amount+charge) as totalpay from v_withdrawAgent where " +
                           where;
                }
                else if (isstat == 2)
                {
                    sql += "\r\n" + @"select count(0) as qty
,sum(case when is_cancel=1 then 1 else 0 end) as cancel_qty
,sum(case when audit_status=3 then 1 else 0 end) as qty1
,sum(case when audit_status=3 then amount else 0 end) as amt1
,sum(case when is_cancel=0 then 1 else 0 end) as qty2
,sum(case when is_cancel=0 then amount else 0 end) as amt2
,sum(case when audit_status=2 and payment_status=2 then 1 else 0 end) as qty3
,sum(case when audit_status=2 and payment_status=2 then amount else 0 end) as amt3
,sum(case when audit_status=2 and payment_status=3 then 1 else 0 end) as qty4
,sum(case when audit_status=2 and payment_status=3 then amount else 0 end) as amt4 from v_withdrawAgent where " + where;
                }


                ds = DataBase.ExecuteDataset(CommandType.Text, sql, paramList.ToArray());
                return ds;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return ds;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="param"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        private static string BuilderWhere(List<SearchParam> param, List<SqlParameter> paramList)
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
                        case "trade_no": //系统交易号
                            builder.Append(" AND [trade_no] like @trade_no");
                            parameter = new SqlParameter("@trade_no", SqlDbType.VarChar);
                            parameter.Value = iparam.ParamValue + "%";
                            paramList.Add(parameter);
                            break;
                        case "out_trade_no": //系统交易号
                            builder.Append(" AND [out_trade_no] like @out_trade_no");
                            parameter = new SqlParameter("@out_trade_no", SqlDbType.VarChar);
                            parameter.Value = iparam.ParamValue + "%";
                            paramList.Add(parameter);
                            break;
                        case "lotno": //批号
                            builder.Append(" AND [lotno] like @lotno");
                            parameter = new SqlParameter("@lotno", SqlDbType.VarChar);
                            parameter.Value = iparam.ParamValue + "%";
                            paramList.Add(parameter);
                            break;
                        case "bankcode": //收款银行
                            builder.Append(" AND [bankCode] like @bankCode");
                            parameter = new SqlParameter("@bankCode", SqlDbType.VarChar);
                            parameter.Value = iparam.ParamValue + "%";
                            paramList.Add(parameter);
                            break;
                        case "bankname":
                            builder.Append(" AND [bankName] like @bankName");
                            parameter = new SqlParameter("@bankName", SqlDbType.VarChar);
                            parameter.Value = iparam.ParamValue + "%";
                            paramList.Add(parameter);
                            break;
                        case "bankaccount": //收款账户
                            builder.Append(" AND [bankAccount] like @bankAccount");
                            parameter = new SqlParameter("@bankAccount", SqlDbType.VarChar);
                            parameter.Value = iparam.ParamValue + "%";
                            paramList.Add(parameter);
                            break;
                        case "bankaccountname":
                            builder.Append(" AND [bankAccountName] like @bankAccountName");
                            parameter = new SqlParameter("@bankAccountName", SqlDbType.VarChar);
                            parameter.Value = iparam.ParamValue + "%";
                            paramList.Add(parameter);
                            break;
                        case "tranapi":
                            builder.Append(" AND [tranapi] = @tranapi");
                            parameter = new SqlParameter("@tranapi", SqlDbType.Int);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "audit_status": //
                            builder.Append(" AND [audit_status] = @audit_status");
                            parameter = new SqlParameter("@audit_status", SqlDbType.TinyInt);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "payment_status": //
                            builder.Append(" AND [payment_status] = @payment_status");
                            parameter = new SqlParameter("@payment_status", SqlDbType.TinyInt);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "notifystatus": //
                            builder.Append(" AND [notifystatus] = @notifystatus");
                            parameter = new SqlParameter("@notifystatus", SqlDbType.TinyInt);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "is_cancel": //
                            builder.Append(" AND [is_cancel] = @is_cancel");
                            parameter = new SqlParameter("@is_cancel", SqlDbType.Bit);
                            parameter.Value = (bool)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;

                        case "id":
                            builder.Append(" AND [id] = @id");
                            parameter = new SqlParameter("@id", SqlDbType.Int);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;


                        case "mode":
                            builder.Append(" AND [mode] = @mode");
                            parameter = new SqlParameter("@mode", SqlDbType.TinyInt);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;

                        case "amount_from":
                            builder.Append(" AND [amount] <= @amount_from");
                            parameter = new SqlParameter("@amount_from", SqlDbType.Decimal);
                            parameter.Value = (decimal)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;

                        case "amount_to":
                            builder.Append(" AND [amount] <= @amount_to");
                            parameter = new SqlParameter("@amount_to", SqlDbType.Decimal);
                            parameter.Value = (decimal)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;


                        case "begindate":
                            builder.Append(" AND [processingTime] >= @beginpaytime");
                            parameter = new SqlParameter("@beginpaytime", SqlDbType.DateTime);
                            parameter.Value = (DateTime)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "enddate":
                            builder.Append(" AND [processingTime] <= @endpaytime");
                            parameter = new SqlParameter("@endpaytime", SqlDbType.DateTime);
                            parameter.Value = (DateTime)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "saddtime":
                            builder.Append(" AND [addTime] >= @saddtime");
                            parameter = new SqlParameter("@saddtime", SqlDbType.DateTime);
                            parameter.Value = iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "eaddtime":
                            builder.Append(" AND [addTime] <= @eaddtime");
                            parameter = new SqlParameter("@eaddtime", SqlDbType.DateTime);
                            parameter.Value = iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                    }
                }
            }
            return builder.ToString();
        }

        #endregion

		#region  MethodEx

        #region ChkParms
        /// <summary>
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="backcode"></param>
        /// <param name="amount"></param>
        /// <param name="row"></param>
        /// <returns>
        ///     0 检测正常
        ///     1 不存在此用户
        ///     2 用户状态不正常
        ///     3 商户未签约当前产品
        ///     4 未设置提现方案
        ///     5 不能低于最小允许金额
        ///     6 不能超过最大允许金额
        ///     7 提现金额超过余额
        /// </returns>
        public int ChkParms(int userid, string backcode, decimal amount, out DataRow row)
        {
            row = null;

            SqlParameter[] parameters =
            {
                  new SqlParameter("@i_userid", SqlDbType.Int, 4)
                , new SqlParameter("@i_amount", SqlDbType.Decimal, 18)
                , new SqlParameter("@i_bankCode", SqlDbType.VarChar, 10)
                , new SqlParameter("@checkTime", SqlDbType.DateTime, 8)
                , new SqlParameter("@result", SqlDbType.TinyInt)
            };

            parameters[0].Value = userid;
            parameters[1].Value = amount;
            parameters[2].Value = backcode;
            parameters[3].Value = DateTime.Now;
            parameters[4].Direction = ParameterDirection.Output;

            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_withdrawAgent_chkParms", parameters);

            int chkResult = Convert.ToInt32(parameters[4].Value);

            if (chkResult == 0 && ds != null && ds.Tables.Count > 0)
                row = ds.Tables[0].Rows[0];

            return chkResult;
        }

        #endregion

        #region 取消

        /// <summary>
        /// </summary>
        /// <param name="trade_no"></param>
        /// <returns>
        ///     99 未知错误
        ///     1 不存在此单
        ///     2 此单已取消
        ///     3 已审核，不可取消
        ///     4 系统出错
        ///     0 审核成功
        /// </returns>
        public int Cancel(string trade_no)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@trade_no", SqlDbType.VarChar, 40)
                , new SqlParameter("@result", SqlDbType.TinyInt, 0)
            };

            parameters[0].Value = trade_no;
            parameters[1].Direction = ParameterDirection.Output;

            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_withdrawAgent_cancel", parameters);

            int chkResult = Convert.ToInt32(parameters[1].Value);

            return chkResult;
        }

        #endregion

        #region 审核
        /// <summary>
        /// </summary>
        /// <param name="trade_no"></param>
        /// <param name="auditstatus"></param>
        /// <param name="auditUser"></param>
        /// <param name="auditUserName"></param>
        /// <returns>
        ///     99 未知错误
        ///     1 不存在此单
        ///     2 此单已取消
        ///     3 已审核过，不可重复操作
        ///     4 输入状态不正确
        ///     5 系统出错
        ///     0 审核成功
        /// </returns>
        public int Audit(string trade_no, int auditstatus, int auditUser, string auditUserName)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@trade_no", SqlDbType.VarChar, 40)
                , new SqlParameter("@auditstatus", SqlDbType.TinyInt, 1)
                , new SqlParameter("@auditUser", SqlDbType.Int)
                , new SqlParameter("@auditTime", SqlDbType.DateTime)
                , new SqlParameter("@auditUserName", SqlDbType.VarChar, 50)
                , new SqlParameter("@result", SqlDbType.TinyInt, 1)
            };

            parameters[0].Value = trade_no;
            parameters[1].Value = auditstatus;
            parameters[2].Value = auditUser;
            parameters[3].Value = DateTime.Now;
            parameters[4].Value = auditUserName;
            parameters[5].Direction = ParameterDirection.Output;

            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_withdrawAgent_audit", parameters);

            int chkResult = Convert.ToInt32(parameters[5].Value);

            return chkResult;
        }

        #endregion

        #region 确认

        /// <summary>
        /// </summary>
        /// <param name="trade_no"></param>
        /// <param name="sure"></param>
        /// <param name="clientip"></param>
        /// <returns>
        ///     1 不存在此单
        ///     2 此单已处理，不可重复操作
        ///     3 输入不正确
        ///     4 系统出错
        ///     0 确认成功
        /// </returns>
        public int Affirm(string trade_no, byte sure, string clientip)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@trade_no", SqlDbType.VarChar, 40)
                , new SqlParameter("@sure", SqlDbType.TinyInt, 1)
                , new SqlParameter("@sureTime", SqlDbType.DateTime, 8)
                , new SqlParameter("@clientip", SqlDbType.VarChar, 50)
                , new SqlParameter("@result", SqlDbType.TinyInt, 1)
            };

            parameters[0].Value = trade_no;
            parameters[1].Value = sure;
            parameters[3].Value = DateTime.Now;
            parameters[4].Value = clientip;
            parameters[5].Direction = ParameterDirection.Output;

            DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_withdrawAgent_affirm", parameters);
            int chkResult = Convert.ToInt32(parameters[5].Value);

            return chkResult;
        }

        #endregion

        #region 结案

        /// <summary>
        /// </summary>
        /// <param name="trade_no"></param>
        /// <returns>
        ///     99 未知错误
        ///     1 不存在此单
        ///     2 此单已取消
        ///     3 此单未审核，不可完成此操作
        ///     4 此单已结案
        ///     5 系统出错
        ///     0 操作成功
        /// </returns>
        public int Complete(string trade_no, int pstatus)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@trade_no", SqlDbType.VarChar, 40)
                , new SqlParameter("@pstatus", SqlDbType.TinyInt, 1)
                , new SqlParameter("@processingTime", SqlDbType.DateTime)
                , new SqlParameter("@result", SqlDbType.TinyInt, 1)
            };

            parameters[0].Value = trade_no;
            parameters[1].Value = pstatus;
            parameters[2].Value = DateTime.Now;
            parameters[3].Direction = ParameterDirection.Output;

            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_withdrawAgent_complete", parameters);

            int chkResult = Convert.ToInt32(parameters[3].Value);

            return chkResult;
        }

        #endregion

		#endregion  MethodEx
	}
}

