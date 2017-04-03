using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using DBAccess;
using viviLib.ExceptionHandling;
using viviapi.Model.APP;
using viviLib.Data;
using viviLib.Data;
namespace viviapi.BLL.APP
{
	/// <summary>
	/// 数据访问类:recharge
	/// </summary>
	public partial class recharge
	{
        internal const string SQL_TABLE = "recharge";
        internal const string SQL_FIELDS = @"id,userid,orderno,rechargeAmt,balance,addtime,status,paytime,transNo,remark ";

		public recharge()
		{}
        #region  Method

        #region Add
        /// <summary>
        ///  增加一条数据
        /// </summary>
        public int Add(RechargeInfo model)
        {
            try
            {
                int rowsAffected;
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@orderno", SqlDbType.NVarChar,30),
					new SqlParameter("@rechargeAmt", SqlDbType.Decimal,9),
					new SqlParameter("@balance", SqlDbType.Decimal,9),
					new SqlParameter("@addtime", SqlDbType.DateTime),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@paytime", SqlDbType.DateTime),
					new SqlParameter("@transNo", SqlDbType.NVarChar,50),
					new SqlParameter("@remark", SqlDbType.NVarChar,200)};
                parameters[0].Direction = ParameterDirection.Output;
                parameters[1].Value = model.userid;
                parameters[2].Value = model.orderno;
                parameters[3].Value = model.rechargeAmt;
                parameters[4].Value = model.balance;
                parameters[5].Value = model.addtime;
                parameters[6].Value = model.status;
                parameters[7].Value = model.paytime;
                parameters[8].Value = model.transNo;
                parameters[9].Value = model.remark;

                DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_recharge_add", parameters);
                return (int)parameters[0].Value;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }
        #endregion

        #region Update
        /// <summary>
        ///  更新一条数据
        /// </summary>
        public bool Update(RechargeInfo model)
        {
            try
            {
                int rowsAffected = 0;
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@orderno", SqlDbType.NVarChar,30),
					new SqlParameter("@rechargeAmt", SqlDbType.Decimal,9),
					new SqlParameter("@balance", SqlDbType.Decimal,9),
					new SqlParameter("@addtime", SqlDbType.DateTime),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@paytime", SqlDbType.DateTime),
					new SqlParameter("@transNo", SqlDbType.NVarChar,50),
					new SqlParameter("@remark", SqlDbType.NVarChar,200)};
                parameters[0].Value = model.id;
                parameters[1].Value = model.userid;
                parameters[2].Value = model.orderno;
                parameters[3].Value = model.rechargeAmt;
                parameters[4].Value = model.balance;
                parameters[5].Value = model.addtime;
                parameters[6].Value = model.status;
                parameters[7].Value = model.paytime;
                parameters[8].Value = model.transNo;
                parameters[9].Value = model.remark;

                rowsAffected = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_recharge_Update", parameters);
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
        #endregion

        #region Delete
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

                rowsAffected = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_recharge_Delete", parameters); 
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
        #endregion       

        #region GetModel
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public RechargeInfo GetModel(int id)
        {           
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
                parameters[0].Value = id;

                DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_recharge_GetModel", parameters);
                return GetModelFromDs(ds);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static RechargeInfo GetModelFromDs(DataSet ds)
        {
            RechargeInfo model = new RechargeInfo();

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
                if (ds.Tables[0].Rows[0]["orderno"] != null && ds.Tables[0].Rows[0]["orderno"].ToString() != "")
                {
                    model.orderno = ds.Tables[0].Rows[0]["orderno"].ToString();
                }
                if (ds.Tables[0].Rows[0]["rechargeAmt"] != null && ds.Tables[0].Rows[0]["rechargeAmt"].ToString() != "")
                {
                    model.rechargeAmt = decimal.Parse(ds.Tables[0].Rows[0]["rechargeAmt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["balance"] != null && ds.Tables[0].Rows[0]["balance"].ToString() != "")
                {
                    model.balance = decimal.Parse(ds.Tables[0].Rows[0]["balance"].ToString());
                }
                if (ds.Tables[0].Rows[0]["addtime"] != null && ds.Tables[0].Rows[0]["addtime"].ToString() != "")
                {
                    model.addtime = DateTime.Parse(ds.Tables[0].Rows[0]["addtime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["status"] != null && ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["paytime"] != null && ds.Tables[0].Rows[0]["paytime"].ToString() != "")
                {
                    model.paytime = DateTime.Parse(ds.Tables[0].Rows[0]["paytime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["transNo"] != null && ds.Tables[0].Rows[0]["transNo"].ToString() != "")
                {
                    model.transNo = ds.Tables[0].Rows[0]["transNo"].ToString();
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
            strSql.Append("select id,userid,orderno,rechargeAmt,balance,addtime,status,paytime,transNo,remark ");
            strSql.Append(" FROM recharge ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            DataSet ds = DataBase.ExecuteDataset(CommandType.Text, strSql.ToString(), null);
            return ds;
        }

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

