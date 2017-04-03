using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using DBAccess;
using viviapi.Model.APP;
using viviLib.ExceptionHandling;
using viviLib.Data;
namespace viviapi.BLL.APP
{
	/// <summary>
	/// 数据访问类:transfer
	/// </summary>
	public partial class transfer
	{
        internal const string SQL_TABLE = "transfer";
        internal const string SQL_FIELDS = @"id,userid,status,billingName,bankname,province,city,branch,cardnum,payee,tranAmt,charges,paybank,email,mobile,iswarn,warnday,processstatus,processtime,smsnotification,field1,field2,remark ";
		public transfer()
		{}

        #region  Method
       
        /// <summary>
        ///  增加一条数据
        /// </summary>
        public int Add(TransferInfo model)
        {
            try
            {
                int rowsAffected;
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@billingName", SqlDbType.NVarChar,100),
					new SqlParameter("@bankname", SqlDbType.TinyInt,1),
					new SqlParameter("@province", SqlDbType.NVarChar,15),
					new SqlParameter("@city", SqlDbType.NVarChar,15),
					new SqlParameter("@branch", SqlDbType.NVarChar,100),
					new SqlParameter("@cardnum", SqlDbType.VarChar,20),
					new SqlParameter("@payee", SqlDbType.NVarChar,10),
					new SqlParameter("@tranAmt", SqlDbType.Decimal,9),
					new SqlParameter("@charges", SqlDbType.Decimal,9),
					new SqlParameter("@paybank", SqlDbType.TinyInt,1),
					new SqlParameter("@email", SqlDbType.VarChar,22),
					new SqlParameter("@mobile", SqlDbType.VarChar,20),
					new SqlParameter("@iswarn", SqlDbType.TinyInt,1),
					new SqlParameter("@warnday", SqlDbType.Int,4),
					new SqlParameter("@processstatus", SqlDbType.TinyInt,1),
					new SqlParameter("@processtime", SqlDbType.DateTime),
					new SqlParameter("@smsnotification", SqlDbType.Bit,1),
					new SqlParameter("@field1", SqlDbType.NVarChar,50),
					new SqlParameter("@field2", SqlDbType.NVarChar,50),
					new SqlParameter("@remark", SqlDbType.NVarChar,200)};
                parameters[0].Direction = ParameterDirection.Output;
                parameters[1].Value = model.userid;
                parameters[2].Value = model.status;
                parameters[3].Value = model.billingName;
                parameters[4].Value = model.bankname;
                parameters[5].Value = model.province;
                parameters[6].Value = model.city;
                parameters[7].Value = model.branch;
                parameters[8].Value = model.cardnum;
                parameters[9].Value = model.payee;
                parameters[10].Value = model.tranAmt;
                parameters[11].Value = model.charges;
                parameters[12].Value = model.paybank;
                parameters[13].Value = model.email;
                parameters[14].Value = model.mobile;
                parameters[15].Value = model.iswarn;
                parameters[16].Value = model.warnday;
                parameters[17].Value = model.processstatus;
                parameters[18].Value = model.processtime;
                parameters[19].Value = model.smsnotification;
                parameters[20].Value = model.field1;
                parameters[21].Value = model.field2;
                parameters[22].Value = model.remark;

                DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_transfer_ADD", parameters);
                return (int)parameters[0].Value;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        /// <summary>
        ///  更新一条数据
        /// </summary>
        public bool Update(TransferInfo model)
        {
            try
            {
                int rowsAffected = 0;
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@billingName", SqlDbType.NVarChar,100),
					new SqlParameter("@bankname", SqlDbType.TinyInt,1),
					new SqlParameter("@province", SqlDbType.NVarChar,15),
					new SqlParameter("@city", SqlDbType.NVarChar,15),
					new SqlParameter("@branch", SqlDbType.NVarChar,100),
					new SqlParameter("@cardnum", SqlDbType.VarChar,20),
					new SqlParameter("@payee", SqlDbType.NVarChar,10),
					new SqlParameter("@tranAmt", SqlDbType.Decimal,9),
					new SqlParameter("@charges", SqlDbType.Decimal,9),
					new SqlParameter("@paybank", SqlDbType.TinyInt,1),
					new SqlParameter("@email", SqlDbType.VarChar,22),
					new SqlParameter("@mobile", SqlDbType.VarChar,20),
					new SqlParameter("@iswarn", SqlDbType.TinyInt,1),
					new SqlParameter("@warnday", SqlDbType.Int,4),
					new SqlParameter("@processstatus", SqlDbType.TinyInt,1),
					new SqlParameter("@processtime", SqlDbType.DateTime),
					new SqlParameter("@smsnotification", SqlDbType.Bit,1),
					new SqlParameter("@field1", SqlDbType.NVarChar,50),
					new SqlParameter("@field2", SqlDbType.NVarChar,50),
					new SqlParameter("@remark", SqlDbType.NVarChar,200)};
                parameters[0].Value = model.id;
                parameters[1].Value = model.userid;
                parameters[2].Value = model.status;
                parameters[3].Value = model.billingName;
                parameters[4].Value = model.bankname;
                parameters[5].Value = model.province;
                parameters[6].Value = model.city;
                parameters[7].Value = model.branch;
                parameters[8].Value = model.cardnum;
                parameters[9].Value = model.payee;
                parameters[10].Value = model.tranAmt;
                parameters[11].Value = model.charges;
                parameters[12].Value = model.paybank;
                parameters[13].Value = model.email;
                parameters[14].Value = model.mobile;
                parameters[15].Value = model.iswarn;
                parameters[16].Value = model.warnday;
                parameters[17].Value = model.processstatus;
                parameters[18].Value = model.processtime;
                parameters[19].Value = model.smsnotification;
                parameters[20].Value = model.field1;
                parameters[21].Value = model.field2;
                parameters[22].Value = model.remark;
                rowsAffected = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_transfer_Update", parameters);
                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            try
            {
                int rowsAffected = 0;
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
                parameters[0].Value = id;

                rowsAffected = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_transfer_Delete", parameters); 
                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }
       

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TransferInfo GetModel(int id)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
                parameters[0].Value = id;

                DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_transfer_GetModel", parameters);
                return GetModelFromDs(ds);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }

        #region GetModelFromDs
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static TransferInfo GetModelFromDs(DataSet ds)
        {
            TransferInfo model = new TransferInfo();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["userid"] != null && ds.Tables[0].Rows[0]["userid"].ToString() != "")
                {
                    model.userid = int.Parse(ds.Tables[0].Rows[0]["userid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["status"] != null && ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["billingName"] != null && ds.Tables[0].Rows[0]["billingName"].ToString() != "")
                {
                    model.billingName = ds.Tables[0].Rows[0]["billingName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["bankname"] != null && ds.Tables[0].Rows[0]["bankname"].ToString() != "")
                {
                    model.bankname = int.Parse(ds.Tables[0].Rows[0]["bankname"].ToString());
                }
                if (ds.Tables[0].Rows[0]["province"] != null && ds.Tables[0].Rows[0]["province"].ToString() != "")
                {
                    model.province = ds.Tables[0].Rows[0]["province"].ToString();
                }
                if (ds.Tables[0].Rows[0]["city"] != null && ds.Tables[0].Rows[0]["city"].ToString() != "")
                {
                    model.city = ds.Tables[0].Rows[0]["city"].ToString();
                }
                if (ds.Tables[0].Rows[0]["branch"] != null && ds.Tables[0].Rows[0]["branch"].ToString() != "")
                {
                    model.branch = ds.Tables[0].Rows[0]["branch"].ToString();
                }
                if (ds.Tables[0].Rows[0]["cardnum"] != null && ds.Tables[0].Rows[0]["cardnum"].ToString() != "")
                {
                    model.cardnum = ds.Tables[0].Rows[0]["cardnum"].ToString();
                }
                if (ds.Tables[0].Rows[0]["payee"] != null && ds.Tables[0].Rows[0]["payee"].ToString() != "")
                {
                    model.payee = ds.Tables[0].Rows[0]["payee"].ToString();
                }
                if (ds.Tables[0].Rows[0]["tranAmt"] != null && ds.Tables[0].Rows[0]["tranAmt"].ToString() != "")
                {
                    model.tranAmt = decimal.Parse(ds.Tables[0].Rows[0]["tranAmt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["charges"] != null && ds.Tables[0].Rows[0]["charges"].ToString() != "")
                {
                    model.charges = decimal.Parse(ds.Tables[0].Rows[0]["charges"].ToString());
                }
                if (ds.Tables[0].Rows[0]["paybank"] != null && ds.Tables[0].Rows[0]["paybank"].ToString() != "")
                {
                    model.paybank = int.Parse(ds.Tables[0].Rows[0]["paybank"].ToString());
                }
                if (ds.Tables[0].Rows[0]["email"] != null && ds.Tables[0].Rows[0]["email"].ToString() != "")
                {
                    model.email = ds.Tables[0].Rows[0]["email"].ToString();
                }
                if (ds.Tables[0].Rows[0]["mobile"] != null && ds.Tables[0].Rows[0]["mobile"].ToString() != "")
                {
                    model.mobile = ds.Tables[0].Rows[0]["mobile"].ToString();
                }
                if (ds.Tables[0].Rows[0]["iswarn"] != null && ds.Tables[0].Rows[0]["iswarn"].ToString() != "")
                {
                    model.iswarn = int.Parse(ds.Tables[0].Rows[0]["iswarn"].ToString());
                }
                if (ds.Tables[0].Rows[0]["warnday"] != null && ds.Tables[0].Rows[0]["warnday"].ToString() != "")
                {
                    model.warnday = int.Parse(ds.Tables[0].Rows[0]["warnday"].ToString());
                }
                if (ds.Tables[0].Rows[0]["processstatus"] != null && ds.Tables[0].Rows[0]["processstatus"].ToString() != "")
                {
                    model.processstatus = int.Parse(ds.Tables[0].Rows[0]["processstatus"].ToString());
                }
                if (ds.Tables[0].Rows[0]["processtime"] != null && ds.Tables[0].Rows[0]["processtime"].ToString() != "")
                {
                    model.processtime = DateTime.Parse(ds.Tables[0].Rows[0]["processtime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["smsnotification"] != null && ds.Tables[0].Rows[0]["smsnotification"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["smsnotification"].ToString() == "1") || (ds.Tables[0].Rows[0]["smsnotification"].ToString().ToLower() == "true"))
                    {
                        model.smsnotification = true;
                    }
                    else
                    {
                        model.smsnotification = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["field1"] != null && ds.Tables[0].Rows[0]["field1"].ToString() != "")
                {
                    model.field1 = ds.Tables[0].Rows[0]["field1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["field2"] != null && ds.Tables[0].Rows[0]["field2"].ToString() != "")
                {
                    model.field2 = ds.Tables[0].Rows[0]["field2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["remark"] != null && ds.Tables[0].Rows[0]["remark"].ToString() != "")
                {
                    model.remark = ds.Tables[0].Rows[0]["remark"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        #endregion

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,userid,status,billingName,bankname,province,city,branch,cardnum,payee,tranAmt,charges,paybank,email,mobile,iswarn,warnday,processstatus,processtime,smsnotification,field1,field2,remark ");
            strSql.Append(" FROM transfer ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DataSet ds = DataBase.ExecuteDataset(CommandType.Text, strSql.ToString(), null);
            return ds;
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
            strSql.Append(" id,userid,status,billingName,bankname,province,city,branch,cardnum,payee,tranAmt,charges,paybank,email,mobile,iswarn,warnday,processstatus,processtime,smsnotification,field1,field2,remark ");
            strSql.Append(" FROM transfer ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            DataSet ds = DataBase.ExecuteDataset(CommandType.Text, strSql.ToString(), null);
            return ds;
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM transfer ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DataBase.ExecuteScalar(CommandType.Text, strSql.ToString(), null);
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
            strSql.Append(")AS Row, T.*  from transfer T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            DataSet ds = DataBase.ExecuteDataset(CommandType.Text, strSql.ToString(), null);
            return ds;
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
            parameters[0].Value = "transfer";
            parameters[1].Value = "id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("proc_UP_GetRecordByPage",parameters,"ds");
        }*/

        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet ds = new DataSet();
            try
            {
                string tables = SQL_TABLE;
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "id desc";
                }

                List<SqlParameter> paramList = new List<SqlParameter>();
                string userSearchWhere = BuilderWhere(searchParams, paramList);

                string sql = SqlHelper.GetCountSQL(tables, userSearchWhere, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL(SQL_FIELDS, tables, userSearchWhere, orderby, key, pageSize, page, false);

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
            StringBuilder builder = new StringBuilder(" 1 = 1");

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
                        case "username":
                            builder.Append(" AND [userName] like @UserName");
                            parameter = new SqlParameter("@UserName", SqlDbType.VarChar, 20);
                            parameter.Value = "%" + SqlHelper.CleanString((string)iparam.ParamValue, 20) + "%";
                            paramList.Add(parameter);
                            break;
                        case "status":
                            builder.Append(" AND [status] = @status");
                            parameter = new SqlParameter("@status", SqlDbType.TinyInt);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "stime":
                            builder.Append(" AND [addtime] >= @stime");
                            parameter = new SqlParameter("@stime", SqlDbType.DateTime);
                            parameter.Value = iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "etime":
                            builder.Append(" AND [addtime] <= @etime");
                            parameter = new SqlParameter("@etime", SqlDbType.DateTime);
                            parameter.Value = iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                    }
                }
            }
            return builder.ToString();
        }
        #endregion  Method
        
	}
}

