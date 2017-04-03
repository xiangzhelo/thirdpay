using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBAccess;//Please add references
using viviapi.Model.APP;
using viviLib.ExceptionHandling;
using viviLib.Data;
namespace viviapi.BLL.APP
{
	/// <summary>
	/// 数据访问类:entrust
	/// </summary>
	public partial class entrust
	{
		public entrust()
		{}

        internal const string SQL_TABLE = "entrust";
        internal const string SQL_FIELDS = @"id,userid,status,bankcardnum,bankname,payee,amount,rate,remittancefee,totalAmt,addtime,cdate,cadmin,remark ";
	
        #region  Method

       

        /// <summary>
        ///  增加一条数据
        /// </summary>
        public int Add(EntrustInfo model)
        {
            try
            {
                int rowsAffected;
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@bankcardnum", SqlDbType.VarChar,30),
					new SqlParameter("@bankname", SqlDbType.NVarChar,150),
					new SqlParameter("@payee", SqlDbType.NVarChar,10),
					new SqlParameter("@amount", SqlDbType.Decimal,9),
					new SqlParameter("@rate", SqlDbType.Decimal,9),
					new SqlParameter("@remittancefee", SqlDbType.Decimal,9),
					new SqlParameter("@totalAmt", SqlDbType.Decimal,9),
					new SqlParameter("@addtime", SqlDbType.DateTime),
					new SqlParameter("@cdate", SqlDbType.DateTime),
					new SqlParameter("@cadmin", SqlDbType.Int,4),
					new SqlParameter("@remark", SqlDbType.NVarChar,200)};
                parameters[0].Direction = ParameterDirection.Output;
                parameters[1].Value = model.userid;
                parameters[2].Value = model.status;
                parameters[3].Value = model.bankcardnum;
                parameters[4].Value = model.bankname;
                parameters[5].Value = model.payee;
                parameters[6].Value = model.amount;
                parameters[7].Value = model.rate;
                parameters[8].Value = model.remittancefee;
                parameters[9].Value = model.totalAmt;
                parameters[10].Value = model.addtime;
                parameters[11].Value = model.cdate;
                parameters[12].Value = model.cadmin;
                parameters[13].Value = model.remark;

                DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_entrust_ADD", parameters);
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
        public bool Update(EntrustInfo model)
        {
            try
            {
                int rowsAffected = 0;
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@bankcardnum", SqlDbType.VarChar,30),
					new SqlParameter("@bankname", SqlDbType.NVarChar,150),
					new SqlParameter("@payee", SqlDbType.NVarChar,10),
					new SqlParameter("@amount", SqlDbType.Decimal,9),
					new SqlParameter("@rate", SqlDbType.Decimal,9),
					new SqlParameter("@remittancefee", SqlDbType.Decimal,9),
					new SqlParameter("@totalAmt", SqlDbType.Decimal,9),
					new SqlParameter("@addtime", SqlDbType.DateTime),
					new SqlParameter("@cdate", SqlDbType.DateTime),
					new SqlParameter("@cadmin", SqlDbType.Int,4),
					new SqlParameter("@remark", SqlDbType.NVarChar,200)};
                parameters[0].Value = model.id;
                parameters[1].Value = model.userid;
                parameters[2].Value = model.status;
                parameters[3].Value = model.bankcardnum;
                parameters[4].Value = model.bankname;
                parameters[5].Value = model.payee;
                parameters[6].Value = model.amount;
                parameters[7].Value = model.rate;
                parameters[8].Value = model.remittancefee;
                parameters[9].Value = model.totalAmt;
                parameters[10].Value = model.addtime;
                parameters[11].Value = model.cdate;
                parameters[12].Value = model.cadmin;
                parameters[13].Value = model.remark;

                rowsAffected = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_entrust_Update", parameters);
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

                rowsAffected = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_entrust_Delete", parameters); 
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
        public EntrustInfo GetModel(int id)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
                parameters[0].Value = id;

                DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_entrust_GetModel", parameters);
                return GetModelFromDs(ds);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }

        #region GetModelFromDs
        public static EntrustInfo GetModelFromDs(DataSet ds)
        {
            EntrustInfo model = new EntrustInfo();

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
                if (ds.Tables[0].Rows[0]["bankcardnum"] != null && ds.Tables[0].Rows[0]["bankcardnum"].ToString() != "")
                {
                    model.bankcardnum = ds.Tables[0].Rows[0]["bankcardnum"].ToString();
                }
                if (ds.Tables[0].Rows[0]["bankname"] != null && ds.Tables[0].Rows[0]["bankname"].ToString() != "")
                {
                    model.bankname = ds.Tables[0].Rows[0]["bankname"].ToString();
                }
                if (ds.Tables[0].Rows[0]["payee"] != null && ds.Tables[0].Rows[0]["payee"].ToString() != "")
                {
                    model.payee = ds.Tables[0].Rows[0]["payee"].ToString();
                }
                if (ds.Tables[0].Rows[0]["amount"] != null && ds.Tables[0].Rows[0]["amount"].ToString() != "")
                {
                    model.amount = decimal.Parse(ds.Tables[0].Rows[0]["amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["rate"] != null && ds.Tables[0].Rows[0]["rate"].ToString() != "")
                {
                    model.rate = decimal.Parse(ds.Tables[0].Rows[0]["rate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["remittancefee"] != null && ds.Tables[0].Rows[0]["remittancefee"].ToString() != "")
                {
                    model.remittancefee = decimal.Parse(ds.Tables[0].Rows[0]["remittancefee"].ToString());
                }
                if (ds.Tables[0].Rows[0]["totalAmt"] != null && ds.Tables[0].Rows[0]["totalAmt"].ToString() != "")
                {
                    model.totalAmt = decimal.Parse(ds.Tables[0].Rows[0]["totalAmt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["addtime"] != null && ds.Tables[0].Rows[0]["addtime"].ToString() != "")
                {
                    model.addtime = DateTime.Parse(ds.Tables[0].Rows[0]["addtime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["cdate"] != null && ds.Tables[0].Rows[0]["cdate"].ToString() != "")
                {
                    model.cdate = DateTime.Parse(ds.Tables[0].Rows[0]["cdate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["cadmin"] != null && ds.Tables[0].Rows[0]["cadmin"].ToString() != "")
                {
                    model.cadmin = int.Parse(ds.Tables[0].Rows[0]["cadmin"].ToString());
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
            strSql.Append("select id,userid,status,bankcardnum,bankname,payee,amount,rate,remittancefee,totalAmt,addtime,cdate,cadmin,remark ");
            strSql.Append(" FROM entrust ");
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
            strSql.Append(" id,userid,status,bankcardnum,bankname,payee,amount,rate,remittancefee,totalAmt,addtime,cdate,cadmin,remark ");
            strSql.Append(" FROM entrust ");
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
            strSql.Append("select count(1) FROM entrust ");
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
            strSql.Append(")AS Row, T.*  from entrust T ");
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
            parameters[0].Value = "entrust";
            parameters[1].Value = "id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
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

