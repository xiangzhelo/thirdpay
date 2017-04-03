using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
//Please add references
using DBAccess;
using viviapi.Model.Finance;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.DAL.Finance
{
    /// <summary>
    /// 数据访问类:withdraw
    /// </summary>
    public partial class Withdraw
    {
        internal const string SQL_TABLE = "v_withdraw";
        //money,pay,nopay,Integral
        internal const string SQL_TABLE_FIELDS = @"[id]
      ,[tranno]
      ,[settmode]
      ,[userid]
      ,[amount]
      ,[status]
      ,[suppId]
      ,[suppstatus]
      ,[addtime]
      ,[required]
      ,[paytime]
      ,[tax]
      ,[charges]
      ,[amount]-isnull(tax,0)-isnull(charges,0) as withdraw
      ,[apptype]
      ,[Paytype]
      ,[bankCode]
      ,[PayeeBank]
      ,[provinceCode]
      ,[bankProvince]
      ,[cityCode]
      ,[bankCity]
      ,[payeeName]
      ,[Payeeaddress]
      ,[accoutType]
      ,[account]
      ,[batchNo]
      ,[username]";

        public Withdraw()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("id", "withdraw");
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

            int result = DbHelperSQL.RunProcedure("withdraw_Exists", parameters, out rowsAffected);
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
        public int Add(viviapi.Model.Finance.Withdraw model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@tranno", SqlDbType.VarChar,30),
					new SqlParameter("@settmode", SqlDbType.TinyInt,1),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@amount", SqlDbType.Decimal,9),
					new SqlParameter("@status", SqlDbType.Int,4),
					new SqlParameter("@suppId", SqlDbType.Int,4),
					new SqlParameter("@suppstatus", SqlDbType.TinyInt,1),
					new SqlParameter("@addtime", SqlDbType.DateTime),
					new SqlParameter("@required", SqlDbType.DateTime),
					new SqlParameter("@paytime", SqlDbType.DateTime),
					new SqlParameter("@tax", SqlDbType.Decimal,9),
					new SqlParameter("@charges", SqlDbType.Decimal,9),
					new SqlParameter("@apptype", SqlDbType.TinyInt,1),
					new SqlParameter("@Paytype", SqlDbType.Int,4),
					new SqlParameter("@bankCode", SqlDbType.VarChar,50),
					new SqlParameter("@PayeeBank", SqlDbType.VarChar,50),
					new SqlParameter("@provinceCode", SqlDbType.VarChar,50),
					new SqlParameter("@bankProvince", SqlDbType.VarChar,50),
					new SqlParameter("@cityCode", SqlDbType.VarChar,50),
					new SqlParameter("@bankCity", SqlDbType.VarChar,50),
					new SqlParameter("@payeeName", SqlDbType.VarChar,50),
					new SqlParameter("@Payeeaddress", SqlDbType.VarChar,100),
					new SqlParameter("@accoutType", SqlDbType.TinyInt,1),
					new SqlParameter("@account", SqlDbType.VarChar,30),
					new SqlParameter("@batchNo", SqlDbType.VarChar,30)};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.Tranno;
            parameters[2].Value = model.Settmode;
            parameters[3].Value = model.Userid;
            parameters[4].Value = model.Amount;
            parameters[5].Value = model.Status;
            parameters[6].Value = model.SuppId;
            parameters[7].Value = model.Suppstatus;
            parameters[8].Value = model.Addtime;
            parameters[9].Value = model.Required;
            parameters[10].Value = model.Paytime;
            parameters[11].Value = model.Tax;
            parameters[12].Value = model.Charges;
            parameters[13].Value = model.Apptype;
            parameters[14].Value = model.Paytype;
            parameters[15].Value = model.BankCode;
            parameters[16].Value = model.PayeeBank;
            parameters[17].Value = model.ProvinceCode;
            parameters[18].Value = model.BankProvince;
            parameters[19].Value = model.CityCode;
            parameters[20].Value = model.BankCity;
            parameters[21].Value = model.PayeeName;
            parameters[22].Value = model.Payeeaddress;
            parameters[23].Value = model.AccoutType;
            parameters[24].Value = model.Account;
            parameters[25].Value = model.BatchNo;

            DbHelperSQL.RunProcedure("withdraw_ADD", parameters, out rowsAffected);
            return (int)parameters[0].Value;
        }

        #region Apply
        /// <summary>
        ///  增加一条数据
        /// </summary>
        public int Apply(viviapi.Model.Finance.Withdraw model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@tranno", SqlDbType.VarChar,30),
					new SqlParameter("@settmode", SqlDbType.TinyInt,1),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@amount", SqlDbType.Decimal,9),
					new SqlParameter("@status", SqlDbType.Int,4),
					new SqlParameter("@suppId", SqlDbType.Int,4),
					new SqlParameter("@suppstatus", SqlDbType.TinyInt,1),
					new SqlParameter("@addtime", SqlDbType.DateTime),
					new SqlParameter("@required", SqlDbType.DateTime),
					new SqlParameter("@paytime", SqlDbType.DateTime),
					new SqlParameter("@tax", SqlDbType.Decimal,9),
					new SqlParameter("@charges", SqlDbType.Decimal,9),
					new SqlParameter("@apptype", SqlDbType.TinyInt,1),
					new SqlParameter("@Paytype", SqlDbType.Int,4),
					new SqlParameter("@bankCode", SqlDbType.VarChar,50),
					new SqlParameter("@PayeeBank", SqlDbType.VarChar,50),
					new SqlParameter("@provinceCode", SqlDbType.VarChar,50),
					new SqlParameter("@bankProvince", SqlDbType.VarChar,50),
					new SqlParameter("@cityCode", SqlDbType.VarChar,50),
					new SqlParameter("@bankCity", SqlDbType.VarChar,50),
					new SqlParameter("@payeeName", SqlDbType.VarChar,50),
					new SqlParameter("@Payeeaddress", SqlDbType.VarChar,100),
					new SqlParameter("@accoutType", SqlDbType.TinyInt,1),
					new SqlParameter("@account", SqlDbType.VarChar,30),
					new SqlParameter("@batchNo", SqlDbType.VarChar,30)};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.Tranno;
            parameters[2].Value = model.Settmode;
            parameters[3].Value = model.Userid;
            parameters[4].Value = model.Amount;
            parameters[5].Value = model.Status;
            parameters[6].Value = model.SuppId;
            parameters[7].Value = model.Suppstatus;
            parameters[8].Value = model.Addtime;
            parameters[9].Value = model.Required;
            parameters[10].Value = model.Paytime;
            parameters[11].Value = model.Tax;
            parameters[12].Value = model.Charges;
            parameters[13].Value = model.Apptype;
            parameters[14].Value = model.Paytype;
            parameters[15].Value = model.BankCode;
            parameters[16].Value = model.PayeeBank;
            parameters[17].Value = model.ProvinceCode;
            parameters[18].Value = model.BankProvince;
            parameters[19].Value = model.CityCode;
            parameters[20].Value = model.BankCity;
            parameters[21].Value = model.PayeeName;
            parameters[22].Value = model.Payeeaddress;
            parameters[23].Value = model.AccoutType;
            parameters[24].Value = model.Account;
            parameters[25].Value = model.BatchNo;

            DbHelperSQL.RunProcedure("proc_withdraw_apply", parameters, out rowsAffected);
            return (int)parameters[0].Value;
        }
        #endregion

        #region Audit
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tranno"></param>
        /// <param name="batchNo"></param>
        /// <param name="auditResult">
        /// 0 审核退回
        /// 1 审核通过
        /// </param>
        /// <param name="remark">
        /// 
        /// </param>
        /// <returns></returns>
        public bool Audit(string tranno, string batchNo, byte auditResult, string remark)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@tranno", SqlDbType.VarChar,30),
					new SqlParameter("@auditresult", SqlDbType.TinyInt,1),
                    new SqlParameter("@audittime", SqlDbType.DateTime),
                    new SqlParameter("@auditremark", SqlDbType.VarChar,50),
					new SqlParameter("@batchNo", SqlDbType.VarChar,50)};
            parameters[0].Value = tranno;
            parameters[1].Value = auditResult;
            parameters[2].Value = DateTime.Now;
            parameters[3].Value = remark;
            parameters[4].Value = batchNo;


            object result = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_withdraw_audit", parameters);
            if (result == DBNull.Value)
            {
                return false;
            }

            return Convert.ToBoolean(result);
        }
        #endregion

        #region Update
        /// <summary>
        ///  更新一条数据
        /// </summary>
        public bool Update(viviapi.Model.Finance.Withdraw model)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = {
					new SqlParameter("@tranno", SqlDbType.VarChar,30),
					new SqlParameter("@tax", SqlDbType.Decimal,9),
					new SqlParameter("@charges", SqlDbType.Decimal,9)};

            parameters[0].Value = model.Tranno;

            parameters[1].Value = model.Tax;
            parameters[2].Value = model.Charges;

            DbHelperSQL.RunProcedure("proc_withdraw_update", parameters, out rowsAffected);
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

        #region Complete
        /// <summary>
        ///  
        /// </summary>
        public bool Complete(viviapi.Model.Finance.Withdraw model)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = {
					new SqlParameter("@tranno", SqlDbType.VarChar,30),
					new SqlParameter("@tax", SqlDbType.Decimal,9),
					new SqlParameter("@charges", SqlDbType.Decimal,9),
                    new SqlParameter("@paytime", SqlDbType.DateTime,8)};

            parameters[0].Value = model.Tranno;

            parameters[1].Value = model.Tax;
            parameters[2].Value = model.Charges;
            parameters[3].Value = DateTime.Now;

            DbHelperSQL.RunProcedure("proc_withdraw_complete", parameters, out rowsAffected);
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

        #region Cancel
        /// <summary>
        ///  
        /// </summary>
        public bool Cancel(string tranno)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@tranno", SqlDbType.VarChar, 30)
            };
            parameters[0].Value = tranno;

            object result = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_withdraw_cancel", parameters);

            return Convert.ToBoolean(result);
        }
        #endregion

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

            DbHelperSQL.RunProcedure("withdraw_Delete", parameters, out rowsAffected);
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
            strSql.Append("delete from withdraw ");
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
        public viviapi.Model.Finance.Withdraw GetModel(int id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            var model = new viviapi.Model.Finance.Withdraw();
            DataSet ds = DbHelperSQL.RunProcedure("proc_withdraw_getModel", parameters, "ds");
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
        public viviapi.Model.Finance.Withdraw GetModel(string tranno)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@tranno", SqlDbType.VarChar,30)
			};
            parameters[0].Value = tranno;

            var model = new viviapi.Model.Finance.Withdraw();
            DataSet ds = DbHelperSQL.RunProcedure("proc_withdraw_GetModelbytranno", parameters, "ds");
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
        public viviapi.Model.Finance.Withdraw DataRowToModel(DataRow row)
        {
            var model = new viviapi.Model.Finance.Withdraw();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.ID = int.Parse(row["id"].ToString());
                }
                if (row["tranno"] != null)
                {
                    model.Tranno = row["tranno"].ToString();
                }
                if (row["settmode"] != null && row["settmode"].ToString() != "")
                {
                    model.Settmode = (WithdrawMode)int.Parse(row["settmode"].ToString());
                }
                if (row["userid"] != null && row["userid"].ToString() != "")
                {
                    model.Userid = int.Parse(row["userid"].ToString());
                }
                if (row["amount"] != null && row["amount"].ToString() != "")
                {
                    model.Amount = decimal.Parse(row["amount"].ToString());
                }
                if (row["status"] != null && row["status"].ToString() != "")
                {
                    model.Status = (WithdrawStatus)int.Parse(row["status"].ToString());
                }
                if (row["suppId"] != null && row["suppId"].ToString() != "")
                {
                    model.SuppId = int.Parse(row["suppId"].ToString());
                }
                if (row["suppstatus"] != null && row["suppstatus"].ToString() != "")
                {
                    model.Suppstatus = int.Parse(row["suppstatus"].ToString());
                }
                if (row["addtime"] != null && row["addtime"].ToString() != "")
                {
                    model.Addtime = DateTime.Parse(row["addtime"].ToString());
                }
                if (row["required"] != null && row["required"].ToString() != "")
                {
                    model.Required = DateTime.Parse(row["required"].ToString());
                }
                if (row["paytime"] != null && row["paytime"].ToString() != "")
                {
                    model.Paytime = DateTime.Parse(row["paytime"].ToString());
                }
                if (row["tax"] != null && row["tax"].ToString() != "")
                {
                    model.Tax = decimal.Parse(row["tax"].ToString());
                }
                if (row["charges"] != null && row["charges"].ToString() != "")
                {
                    model.Charges = decimal.Parse(row["charges"].ToString());
                }
                if (row["apptype"] != null && row["apptype"].ToString() != "")
                {
                    model.Apptype = int.Parse(row["apptype"].ToString());
                }
                if (row["Paytype"] != null && row["Paytype"].ToString() != "")
                {
                    model.Paytype = int.Parse(row["Paytype"].ToString());
                }
                if (row["bankCode"] != null)
                {
                    model.BankCode = row["bankCode"].ToString();
                }
                if (row["PayeeBank"] != null)
                {
                    model.PayeeBank = row["PayeeBank"].ToString();
                }
                if (row["provinceCode"] != null)
                {
                    model.ProvinceCode = row["provinceCode"].ToString();
                }
                if (row["bankProvince"] != null)
                {
                    model.BankProvince = row["bankProvince"].ToString();
                }
                if (row["cityCode"] != null)
                {
                    model.CityCode = row["cityCode"].ToString();
                }
                if (row["bankCity"] != null)
                {
                    model.BankCity = row["bankCity"].ToString();
                }
                if (row["payeeName"] != null)
                {
                    model.PayeeName = row["payeeName"].ToString();
                }
                if (row["Payeeaddress"] != null)
                {
                    model.Payeeaddress = row["Payeeaddress"].ToString();
                }
                if (row["accoutType"] != null && row["accoutType"].ToString() != "")
                {
                    model.AccoutType = int.Parse(row["accoutType"].ToString());
                }
                if (row["account"] != null)
                {
                    model.Account = row["account"].ToString();
                }
                if (row["batchNo"] != null)
                {
                    model.BatchNo = row["batchNo"].ToString();
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
            strSql.Append("select id,tranno,settmode,userid,amount,[status],suppId,suppstatus,addtime,[required],paytime,tax,charges,apptype,Paytype,bankCode,PayeeBank,provinceCode,bankProvince,cityCode,bankCity,payeeName,Payeeaddress,accoutType,account,batchNo ");
            strSql.Append(" FROM withdraw ");
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
            strSql.Append(" id,tranno,settmode,userid,amount,status,suppId,suppstatus,addtime,required,paytime,tax,charges,apptype,Paytype,bankCode,PayeeBank,provinceCode,bankProvince,cityCode,bankCity,payeeName,Payeeaddress,accoutType,account,batchNo ");
            strSql.Append(" FROM withdraw ");
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
            strSql.Append("select count(1) FROM withdraw ");
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
            strSql.Append(")AS Row, T.*  from withdraw T ");
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
            parameters[0].Value = "withdraw";
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
               

        #region GetUserDaySettledTimes
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public int GetUserDaySettledTimes(int userid, string day)
        {
            try
            {
                SqlParameter[] prams = new SqlParameter[] { 
                    DataBase.MakeInParam("@userid", SqlDbType.Int, 4, userid),
                    DataBase.MakeInParam("@day", SqlDbType.VarChar, 20, day)
            };
                return Convert.ToInt32(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_withdraw_userdaytimes", prams));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }
        #endregion

        #region GetUserDaySettledAmt
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public decimal GetUserDaySettledAmt(int userid, string day)
        {
            try
            {
                SqlParameter[] prams = new SqlParameter[] { 
                    DataBase.MakeInParam("@userid", SqlDbType.Int, 4, userid),
                    DataBase.MakeInParam("@day", SqlDbType.VarChar, 20, day)
            };
                return Convert.ToDecimal(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_withdraw_userdayAmt", prams));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }
        #endregion

        #endregion  MethodEx

        /// <summary>
        /// 根据搜索条件返回指定分页的商户信息。
        /// </summary>
        /// <param name="searchParams">搜索条件数组。</param>
        /// <param name="pageSize">分页大小。</param>
        /// <param name="page">页码。</param>
        /// <param name="orderby">排序方式。</param>
        /// <param name="stat"></param>
        /// <returns>分页数据。</returns>
        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby,bool stat)
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
                string where = BuilderWhere(searchParams, paramList);

                string sql = SqlHelper.GetCountSQL(tables, where, string.Empty) + "\r\n" +
                             SqlHelper.GetPageSelectSQL(SQL_TABLE_FIELDS, tables, where, orderby, key, pageSize, page,
                                 false);

                if (stat)
                {
                    sql += " select sum(amount) as tapplyAmt,sum(charges) as tCharges,sum(amount-charges) as trealPay from " +
                           SQL_TABLE + " where " + where;
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
        /// 
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
                        case "id":
                            builder.Append(" AND [id] = @id");
                            parameter = new SqlParameter("@id", SqlDbType.Int);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "status":
                            builder.Append(" AND [status] = @status");
                            parameter = new SqlParameter("@status", SqlDbType.Int);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "suppid":
                            builder.Append(" AND [suppId] = @suppId");
                            parameter = new SqlParameter("@suppId", SqlDbType.Int);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;

                        case "userid":
                            builder.Append(" AND [userid] = @userid");
                            parameter = new SqlParameter("@userid", SqlDbType.Int);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "settmode":
                            builder.Append(" AND [settmode] = @settmode");
                            parameter = new SqlParameter("@settmode", SqlDbType.TinyInt);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "suppstatus":
                            builder.Append(" AND [suppstatus] = @suppstatus");
                            parameter = new SqlParameter("@suppstatus", SqlDbType.TinyInt);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "username":
                            builder.Append(" AND [UserName] like @UserName");
                            parameter = new SqlParameter("@UserName", SqlDbType.VarChar);
                            parameter.Value = "%" + iparam.ParamValue + "%";
                            paramList.Add(parameter);
                            break;
                        case "account":
                            builder.Append(" AND [account] like @account");
                            parameter = new SqlParameter("@account", SqlDbType.VarChar);
                            parameter.Value = iparam.ParamValue + "%";
                            paramList.Add(parameter);
                            break;
                        case "payeebank":
                            builder.Append(" AND [PayeeBank] like @PayeeBank");
                            parameter = new SqlParameter("@PayeeBank", SqlDbType.VarChar);
                            parameter.Value = iparam.ParamValue + "%";
                            paramList.Add(parameter);
                            break;
                        case "payeename":
                            builder.Append(" AND [payeeName] like @payeeName");
                            parameter = new SqlParameter("@payeeName", SqlDbType.VarChar);
                            parameter.Value = iparam.ParamValue + "%";
                            paramList.Add(parameter);
                            break;
                        case "begindate":
                            builder.Append(" AND [paytime] >= @beginpaytime");
                            parameter = new SqlParameter("@beginpaytime", SqlDbType.DateTime);
                            parameter.Value = (DateTime)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "enddate":
                            builder.Append(" AND [paytime] <= @endpaytime");
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
                        case "all":
                            builder.Append(" AND ([userid] = @id or [id] = @id)");
                            parameter = new SqlParameter("@id", SqlDbType.Int);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                    }
                }
            }
            return builder.ToString();
        }
    }
}

