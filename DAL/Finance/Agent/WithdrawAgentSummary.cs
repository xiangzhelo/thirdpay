//Please add references
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Caching;
using DBAccess;
using viviapi.Model.Finance.Agent;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.DAL.Finance.Agent
{
	/// <summary>
	/// 数据访问类:withdrawAgentSummary
	/// </summary>
	public partial class WithdrawAgentSummary
	{
        internal string FIELDS = @"[id]
      ,[userid]
      ,[lotno]
      ,[qty]
      ,[succqty]
      ,[amt]
      ,[succamt]
      ,[fee]
      ,[realfee]
      ,[totalamt]
      ,[totalsuccamt]
      ,[status]
      ,[success]
      ,[audit_status]
      ,[auditTime]
      ,[auditUser]
      ,[auditUserName]
      ,[addtime]
      ,[updatetime]
      ,[remark]
      ,[username]";
        internal string SQL_TABLE = "v_withdrawAgentSummary";

		public WithdrawAgentSummary()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string lotno)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@lotno", SqlDbType.VarChar,30)			};
			parameters[0].Value = lotno;

			int result= DbHelperSQL.RunProcedure("withdrawAgentSummary_Exists",parameters,out rowsAffected);
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
		public int Add(Model.Finance.Agent.WithdrawAgentSummary model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@lotno", SqlDbType.VarChar,30),
					new SqlParameter("@qty", SqlDbType.Int,4),
					new SqlParameter("@succqty", SqlDbType.Int,4),
					new SqlParameter("@amt", SqlDbType.Decimal,9),
					new SqlParameter("@succamt", SqlDbType.Decimal,9),
					new SqlParameter("@fee", SqlDbType.Decimal,9),
					new SqlParameter("@realfee", SqlDbType.Decimal,9),
					new SqlParameter("@totalamt", SqlDbType.Decimal,9),
					new SqlParameter("@totalsuccamt", SqlDbType.Decimal,13),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@success", SqlDbType.TinyInt,1),
					new SqlParameter("@audit_status", SqlDbType.TinyInt,1),
					new SqlParameter("@auditTime", SqlDbType.DateTime),
					new SqlParameter("@auditUser", SqlDbType.Int,4),
					new SqlParameter("@auditUserName", SqlDbType.VarChar,50),
					new SqlParameter("@addtime", SqlDbType.DateTime),
					new SqlParameter("@updatetime", SqlDbType.DateTime),
					new SqlParameter("@remark", SqlDbType.NVarChar,100)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.userid;
			parameters[2].Value = model.lotno;
			parameters[3].Value = model.qty;
			parameters[4].Value = model.succqty;
			parameters[5].Value = model.amt;
			parameters[6].Value = model.succamt;
			parameters[7].Value = model.fee;
			parameters[8].Value = model.realfee;
			parameters[9].Value = model.totalamt;
			parameters[10].Value = model.totalsuccamt;
			parameters[11].Value = model.status;
			parameters[12].Value = model.success;
			parameters[13].Value = model.audit_status;
			parameters[14].Value = model.auditTime;
			parameters[15].Value = model.auditUser;
			parameters[16].Value = model.auditUserName;
			parameters[17].Value = model.addtime;
			parameters[18].Value = model.updatetime;
			parameters[19].Value = model.remark;
            
            DbHelperSQL.RunProcedure("proc_withdrawAgentSummary_ADD", parameters, out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public bool Update(Model.Finance.Agent.WithdrawAgentSummary model)
		{
			int rowsAffected=0;
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@lotno", SqlDbType.VarChar,30),
					new SqlParameter("@qty", SqlDbType.Int,4),
					new SqlParameter("@succqty", SqlDbType.Int,4),
					new SqlParameter("@amt", SqlDbType.Decimal,9),
					new SqlParameter("@succamt", SqlDbType.Decimal,9),
					new SqlParameter("@fee", SqlDbType.Decimal,9),
					new SqlParameter("@realfee", SqlDbType.Decimal,9),
					new SqlParameter("@totalamt", SqlDbType.Decimal,9),
					new SqlParameter("@totalsuccamt", SqlDbType.Decimal,13),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@success", SqlDbType.TinyInt,1),
					new SqlParameter("@audit_status", SqlDbType.TinyInt,1),
					new SqlParameter("@auditTime", SqlDbType.DateTime),
					new SqlParameter("@auditUser", SqlDbType.Int,4),
					new SqlParameter("@auditUserName", SqlDbType.VarChar,50),
					new SqlParameter("@addtime", SqlDbType.DateTime),
					new SqlParameter("@updatetime", SqlDbType.DateTime),
					new SqlParameter("@remark", SqlDbType.NVarChar,100)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.userid;
			parameters[2].Value = model.lotno;
			parameters[3].Value = model.qty;
			parameters[4].Value = model.succqty;
			parameters[5].Value = model.amt;
			parameters[6].Value = model.succamt;
			parameters[7].Value = model.fee;
			parameters[8].Value = model.realfee;
			parameters[9].Value = model.totalamt;
			parameters[10].Value = model.totalsuccamt;
			parameters[11].Value = model.status;
			parameters[12].Value = model.success;
			parameters[13].Value = model.audit_status;
			parameters[14].Value = model.auditTime;
			parameters[15].Value = model.auditUser;
			parameters[16].Value = model.auditUserName;
			parameters[17].Value = model.addtime;
			parameters[18].Value = model.updatetime;
			parameters[19].Value = model.remark;

			DbHelperSQL.RunProcedure("withdrawAgentSummary_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("withdrawAgentSummary_Delete",parameters,out rowsAffected);
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
		public bool Delete(string lotno)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from withdrawAgentSummary ");
			strSql.Append(" where lotno=@lotno ");
			SqlParameter[] parameters = {
					new SqlParameter("@lotno", SqlDbType.VarChar,30)			};
			parameters[0].Value = lotno;

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
			strSql.Append("delete from withdrawAgentSummary ");
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
		public Model.Finance.Agent.WithdrawAgentSummary GetModel(int id)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			Model.Finance.Agent.WithdrawAgentSummary model=new Model.Finance.Agent.WithdrawAgentSummary();
			DataSet ds= DbHelperSQL.RunProcedure("withdrawAgentSummary_GetModel",parameters,"ds");
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
		public Model.Finance.Agent.WithdrawAgentSummary DataRowToModel(DataRow row)
		{
            Model.Finance.Agent.WithdrawAgentSummary model = new Model.Finance.Agent.WithdrawAgentSummary();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["userid"]!=null && row["userid"].ToString()!="")
				{
					model.userid=int.Parse(row["userid"].ToString());
				}
				if(row["lotno"]!=null)
				{
					model.lotno=row["lotno"].ToString();
				}
				if(row["qty"]!=null && row["qty"].ToString()!="")
				{
					model.qty=int.Parse(row["qty"].ToString());
				}
				if(row["succqty"]!=null && row["succqty"].ToString()!="")
				{
					model.succqty=int.Parse(row["succqty"].ToString());
				}
				if(row["amt"]!=null && row["amt"].ToString()!="")
				{
					model.amt=decimal.Parse(row["amt"].ToString());
				}
				if(row["succamt"]!=null && row["succamt"].ToString()!="")
				{
					model.succamt=decimal.Parse(row["succamt"].ToString());
				}
				if(row["fee"]!=null && row["fee"].ToString()!="")
				{
					model.fee=decimal.Parse(row["fee"].ToString());
				}
				if(row["realfee"]!=null && row["realfee"].ToString()!="")
				{
					model.realfee=decimal.Parse(row["realfee"].ToString());
				}
				if(row["totalamt"]!=null && row["totalamt"].ToString()!="")
				{
					model.totalamt=decimal.Parse(row["totalamt"].ToString());
				}
				if(row["totalsuccamt"]!=null && row["totalsuccamt"].ToString()!="")
				{
					model.totalsuccamt=decimal.Parse(row["totalsuccamt"].ToString());
				}
				if(row["status"]!=null && row["status"].ToString()!="")
				{
					model.status=int.Parse(row["status"].ToString());
				}
				if(row["success"]!=null && row["success"].ToString()!="")
				{
					model.success=int.Parse(row["success"].ToString());
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
				if(row["addtime"]!=null && row["addtime"].ToString()!="")
				{
					model.addtime=DateTime.Parse(row["addtime"].ToString());
				}
				if(row["updatetime"]!=null && row["updatetime"].ToString()!="")
				{
					model.updatetime=DateTime.Parse(row["updatetime"].ToString());
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
			strSql.Append("select id,userid,lotno,qty,succqty,amt,succamt,fee,realfee,totalamt,totalsuccamt,status,success,audit_status,auditTime,auditUser,auditUserName,addtime,updatetime,remark ");
			strSql.Append(" FROM withdrawAgentSummary ");
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
			strSql.Append(" id,userid,lotno,qty,succqty,amt,succamt,fee,realfee,totalamt,totalsuccamt,status,success,audit_status,auditTime,auditUser,auditUserName,addtime,updatetime,remark ");
			strSql.Append(" FROM withdrawAgentSummary ");
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
			strSql.Append("select count(1) FROM withdrawAgentSummary ");
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
			strSql.Append(")AS Row, T.*  from withdrawAgentSummary T ");
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
			parameters[0].Value = "withdrawAgentSummary";
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
        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby, bool isstat)
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

                if (isstat)
                {
                    //sql += "\r\n" +
                    //       "select sum(amount) as amount,sum(charge) as charge,sum(amount+charge) as totalpay from v_settledAgent where " +
                    //       where;
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
                        case "lotno": //批号
                            builder.Append(" AND [lotno] like @lotno");
                            parameter = new SqlParameter("@lotno", SqlDbType.VarChar);
                            parameter.Value = iparam.ParamValue + "%";
                            paramList.Add(parameter);
                            break;

                        case "status": //
                            builder.Append(" AND [status] = @status");
                            parameter = new SqlParameter("@status", SqlDbType.TinyInt);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "is_cancel": //
                            builder.Append(" AND [is_cancel] = @is_cancel");
                            parameter = new SqlParameter("@is_cancel", SqlDbType.Bit);
                            parameter.Value = (bool)iparam.ParamValue;
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
       /// 
       /// </summary>
       /// <param name="userid"></param>
       /// <param name="tamount"></param>
       /// <returns>
        ///     0 检测正常
        ///     1 用户状态不正常
        ///     2 商户未签约当前产品
        ///     3 未设置提现方案
        ///     4 不能低于最小允许金额
        ///     5 不能超过最大允许金额
        ///     6 提现金额超过余额 
       /// </returns>
        public int ChkParms(int userid, decimal tamount)
        {
            SqlParameter[] parameters =
            {
                  new SqlParameter("@userid", SqlDbType.Int, 4)
                , new SqlParameter("@i_amount", SqlDbType.Decimal, 18)
                , new SqlParameter("@checkTime", SqlDbType.DateTime, 8)
                , new SqlParameter("@result", SqlDbType.TinyInt)
            };

            parameters[0].Value = userid;
            parameters[1].Value = tamount;
            parameters[2].Value = DateTime.Now;
            parameters[3].Direction = ParameterDirection.Output;

            DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_withdrawAgentSummary_chkParms", parameters);

            int chkResult = Convert.ToInt32(parameters[3].Value);
            return chkResult;
        }

        #endregion


        #region Affirm

        /// <summary>
        /// </summary>
        /// <param name="lot_no"></param>
        /// <param name="auditstatus"></param>
        /// <param name="auditUser"></param>
        /// <param name="auditUserName"></param>
        /// <param name="clientip"></param>
        /// <returns>
        ///     99 未知错误
        ///     1 不存在此单
        ///     2 此单已处理，不可重复操作
        ///     3 输入状态不正确
        ///     4 系统出错
        ///     0 审核成功
        /// </returns>
        public int Affirm(string lot_no, int auditstatus, int auditUser, string auditUserName, string clientip)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@lot_no", SqlDbType.VarChar, 40)
                , new SqlParameter("@auditstatus", SqlDbType.TinyInt, 1)
                , new SqlParameter("@auditUser", SqlDbType.Int)
                , new SqlParameter("@auditTime", SqlDbType.DateTime)
                , new SqlParameter("@auditUserName", SqlDbType.VarChar, 50)
                , new SqlParameter("@clientip", SqlDbType.VarChar, 50)
                , new SqlParameter("@result", SqlDbType.TinyInt, 1)
            };

            parameters[0].Value = lot_no;
            parameters[1].Value = auditstatus;
            parameters[2].Value = auditUser;
            parameters[3].Value = DateTime.Now;
            parameters[4].Value = auditUserName;
            parameters[5].Value = clientip;
            parameters[6].Direction = ParameterDirection.Output;

            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_withdrawAgentSummary_audit",
                parameters);
            int chkResult = Convert.ToInt32(parameters[6].Value);

            return chkResult;
        }

        #endregion

	    public void Insert(Model.Finance.Agent.WithdrawAgentSummary summarymodel
	        , List<Model.Finance.Agent.WithdrawAgent> itemlist)
	    {
            Hashtable sqlStringList = new Hashtable();

            #region WithdrawAgentSummary
            var strSql = new StringBuilder();
            strSql.Append("insert into withdrawAgentSummary(");
            strSql.Append("userid,lotno,qty,succqty,amt,succamt,fee,realfee,status,success,audit_status,auditTime,auditUser,auditUserName,addtime,updatetime,remark)");
            strSql.Append(" values (");
            strSql.Append("@userid,@lotno,@qty,@succqty,@amt,@succamt,@fee,@realfee,@status,@success,@audit_status,@auditTime,@auditUser,@auditUserName,@addtime,@updatetime,@remark)");
            strSql.Append(";select @@IDENTITY");

            SqlParameter[] parameters1 = {
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@lotno", SqlDbType.VarChar,30),
					new SqlParameter("@qty", SqlDbType.Int,4),
					new SqlParameter("@succqty", SqlDbType.Int,4),
					new SqlParameter("@amt", SqlDbType.Decimal,9),
					new SqlParameter("@succamt", SqlDbType.Decimal,9),
					new SqlParameter("@fee", SqlDbType.Decimal,9),
					new SqlParameter("@realfee", SqlDbType.Decimal,9),
					new SqlParameter("@totalamt", SqlDbType.Decimal,9),
					new SqlParameter("@totalsuccamt", SqlDbType.Decimal,13),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@success", SqlDbType.TinyInt,1),
					new SqlParameter("@audit_status", SqlDbType.TinyInt,1),
					new SqlParameter("@auditTime", SqlDbType.DateTime),
					new SqlParameter("@auditUser", SqlDbType.Int,4),
					new SqlParameter("@auditUserName", SqlDbType.VarChar,50),
					new SqlParameter("@addtime", SqlDbType.DateTime),
					new SqlParameter("@updatetime", SqlDbType.DateTime),
					new SqlParameter("@remark", SqlDbType.NVarChar,100)};
            parameters1[0].Value = summarymodel.userid;
            parameters1[1].Value = summarymodel.lotno;
            parameters1[2].Value = summarymodel.qty;
            parameters1[3].Value = summarymodel.succqty;
            parameters1[4].Value = summarymodel.amt;
            parameters1[5].Value = summarymodel.succamt;
            parameters1[6].Value = summarymodel.fee;
            parameters1[7].Value = summarymodel.realfee;
            parameters1[8].Value = summarymodel.totalamt;
            parameters1[9].Value = summarymodel.totalsuccamt;
            parameters1[10].Value = summarymodel.status;
            parameters1[11].Value = summarymodel.success;
            parameters1[12].Value = summarymodel.audit_status;
            parameters1[13].Value = summarymodel.auditTime;
            parameters1[14].Value = summarymodel.auditUser;
            parameters1[15].Value = summarymodel.auditUserName;
            parameters1[16].Value = summarymodel.addtime;
            parameters1[17].Value = summarymodel.updatetime;
            parameters1[18].Value = summarymodel.remark;

            sqlStringList.Add(strSql.ToString(), parameters1);
            #endregion

	        foreach (var model in itemlist)
            {
                #region item
                strSql = new StringBuilder();
                strSql.Append("insert into withdrawAgent(");
                strSql.Append("mode,lotno,serial,trade_no,out_trade_no,service,input_charset,userid,sign_type,return_url,provinceCode,cityCode,bankProvince,bankCity,bankCode,bankName,bankBranch,bankAccountName,bankaccoutType,bankAccount,amount,charge,addTime,processingTime,audit_status,auditTime,auditUser,auditUserName,payment_status,is_cancel,ext1,ext2,ext3,remark,tranApi,suppstatus,notifyTimes,notifystatus,callbackText,issure,suretime,sureclientip,sureuser)");
                strSql.Append(" values (");
                strSql.Append("@mode,@lotno,@serial,@trade_no,@out_trade_no,@service,@input_charset,@userid,@sign_type,@return_url,@provinceCode,@cityCode,@bankProvince,@bankCity,@bankCode,@bankName,@bankBranch,@bankAccountName,@bankaccoutType,@bankAccount,@amount,@charge,@addTime,@processingTime,@audit_status,@auditTime,@auditUser,@auditUserName,@payment_status,@is_cancel,@ext1,@ext2,@ext3,@remark,@tranApi,@suppstatus,@notifyTimes,@notifystatus,@callbackText,@issure,@suretime,@sureclientip,@sureuser)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
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
                parameters[0].Value = model.mode;
                parameters[1].Value = model.lotno;
                parameters[2].Value = model.serial;
                parameters[3].Value = model.trade_no;
                parameters[4].Value = model.out_trade_no;
                parameters[5].Value = model.service;
                parameters[6].Value = model.input_charset;
                parameters[7].Value = model.userid;
                parameters[8].Value = model.sign_type;
                parameters[9].Value = model.return_url;
                parameters[10].Value = model.provinceCode;
                parameters[11].Value = model.cityCode;
                parameters[12].Value = model.bankProvince;
                parameters[13].Value = model.bankCity;
                parameters[14].Value = model.bankCode;
                parameters[15].Value = model.bankName;
                parameters[16].Value = model.bankBranch;
                parameters[17].Value = model.bankAccountName;
                parameters[18].Value = model.bankaccoutType;
                parameters[19].Value = model.bankAccount;
                parameters[20].Value = model.amount;
                parameters[21].Value = model.charge;
                parameters[22].Value = model.totalamt;
                parameters[23].Value = model.addTime;
                parameters[24].Value = model.processingTime;
                parameters[25].Value = model.audit_status;
                parameters[26].Value = model.auditTime;
                parameters[27].Value = model.auditUser;
                parameters[28].Value = model.auditUserName;
                parameters[29].Value = model.payment_status;
                parameters[30].Value = model.is_cancel;
                parameters[31].Value = model.ext1;
                parameters[32].Value = model.ext2;
                parameters[33].Value = model.ext3;
                parameters[34].Value = model.remark;
                parameters[35].Value = model.tranApi;
                parameters[36].Value = model.suppstatus;
                parameters[37].Value = model.notifyTimes;
                parameters[38].Value = model.notifystatus;
                parameters[39].Value = model.callbackText;
                parameters[40].Value = model.issure;
                parameters[41].Value = model.suretime;
                parameters[42].Value = model.sureclientip;
                parameters[43].Value = model.sureuser;
                sqlStringList.Add(strSql.ToString(), parameters);
	       
            
                #endregion
            }

	        DbHelperSQL.ExecuteSqlTran(sqlStringList);
	    }

	    #endregion  MethodEx
	}
}

