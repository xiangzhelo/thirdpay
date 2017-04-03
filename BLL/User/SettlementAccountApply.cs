using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DBAccess;
using viviapi.DAL.User;
using viviapi.Model.User;
using viviLib.ExceptionHandling;
using viviLib.Data;

namespace viviapi.BLL.User
{
    public class SettlementAccountApply
    {
        static viviapi.DAL.User.SettlementAccountApply _dal = new viviapi.DAL.User.SettlementAccountApply();

        internal const string SQL_TABLE = "V_userPayAcctChange";
        //money,pay,nopay,Integral
        internal const string SQL_TABLE_FIELDS = @"[id]
      ,[userid]
      ,[pmode]
      ,[account],accoutType
      ,[payeeName]
      ,[payeeBank]
      ,[bankProvince]
      ,[bankCity]
      ,[bankAddress]
      ,[status]
      ,[AddTime]
      ,[SureTime]
      ,[SureUser]
      ,[userName]
      ,[relname],manageId";

        #region Exists2
        /// <summary>
        /// 是否存在
        /// </summary>
        public static bool Exists2(int userId)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@userId", SqlDbType.Int,4)};
                parameters[0].Value = userId;

                int result = (int)DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_userspaybank_Exists", parameters);
                if (result == 1)
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

        #region Exists3
        /// <summary>
        /// 是否存在
        /// </summary>
        public static bool Exists3(int userId)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@userId", SqlDbType.Int,4)};
                parameters[0].Value = userId;

                int result = (int)DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_userspaybankapp_Exists", parameters);
                if (result == 1)
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

        #region Exists
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool Exists(int userId)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@userId", SqlDbType.Int,4)};
                parameters[0].Value = userId;

                int result = (int)DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_userspaybankapp_Exists", parameters);
                if (result == 1)
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

        public static int Insert(UserPayBankAppInfo model)
        {
            try
            {
                return _dal.Insert(model);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        #region Add
        /// <summary>
        ///  增加一条数据
        /// </summary>
        public static int Add(UserPayBankAppInfo model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into userspaybankapp(");
                strSql.Append("userid,accoutType,pmode,account,payeeName,payeeBank,bankProvince,bankCity,bankAddress,status,AddTime,SureTime,SureUser,BankCode,provinceCode,cityCode)");
                strSql.Append(" values (");
                strSql.Append("@userid,@accoutType,@pmode,@account,@payeeName,@payeeBank,@bankProvince,@bankCity,@bankAddress,@status,@AddTime,@SureTime,@SureUser,@BankCode,@provinceCode,@cityCode)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@accoutType", SqlDbType.TinyInt,1),
					new SqlParameter("@pmode", SqlDbType.TinyInt,1),
					new SqlParameter("@account", SqlDbType.VarChar,50),
					new SqlParameter("@payeeName", SqlDbType.VarChar,50),
					new SqlParameter("@payeeBank", SqlDbType.VarChar,50),
					new SqlParameter("@bankProvince", SqlDbType.VarChar,50),
					new SqlParameter("@bankCity", SqlDbType.VarChar,50),
					new SqlParameter("@bankAddress", SqlDbType.VarChar,100),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@AddTime", SqlDbType.DateTime),
					new SqlParameter("@SureTime", SqlDbType.DateTime),
					new SqlParameter("@SureUser", SqlDbType.Int,4),
					new SqlParameter("@BankCode", SqlDbType.VarChar,50),
					new SqlParameter("@provinceCode", SqlDbType.VarChar,50),
					new SqlParameter("@cityCode", SqlDbType.VarChar,50)};
                parameters[0].Value = model.userid;
                parameters[1].Value = model.accoutType;
                parameters[2].Value = model.pmode;
                parameters[3].Value = model.account;
                parameters[4].Value = model.payeeName;
                parameters[5].Value = model.payeeBank;
                parameters[6].Value = model.bankProvince;
                parameters[7].Value = model.bankCity;
                parameters[8].Value = model.bankAddress;
                parameters[9].Value = model.status;
                parameters[10].Value = model.AddTime;
                parameters[11].Value = model.SureTime;
                parameters[12].Value = model.SureUser;
                parameters[13].Value = model.BankCode;
                parameters[14].Value = model.provinceCode;
                parameters[15].Value = model.cityCode;

                object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(obj);
                }
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
        public static bool Update(UserPayBankAppInfo model)
        {
            try
            {
                int rowsAffected = 0;
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@pmode", SqlDbType.TinyInt,1),
					new SqlParameter("@account", SqlDbType.VarChar,50),
					new SqlParameter("@payeeName", SqlDbType.VarChar,50),
					new SqlParameter("@payeeBank", SqlDbType.VarChar,50),
					new SqlParameter("@bankProvince", SqlDbType.VarChar,50),
					new SqlParameter("@bankCity", SqlDbType.VarChar,50),
					new SqlParameter("@bankAddress", SqlDbType.VarChar,100),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@AddTime", SqlDbType.DateTime),
					new SqlParameter("@SureTime", SqlDbType.DateTime),
					new SqlParameter("@SureUser", SqlDbType.Int,4)};
                parameters[0].Value = model.id;
                parameters[1].Value = model.userid;
                parameters[2].Value = model.pmode;
                parameters[3].Value = model.account;
                parameters[4].Value = model.payeeName;
                parameters[5].Value = model.payeeBank;
                parameters[6].Value = model.bankProvince;
                parameters[7].Value = model.bankCity;
                parameters[8].Value = model.bankAddress;
                parameters[9].Value = model.status;
                parameters[10].Value = model.AddTime;
                parameters[11].Value = model.SureTime;
                parameters[12].Value = model.SureUser;

                rowsAffected = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_userspaybankapp_Update", parameters);
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

        #region Update
        /// <summary>
        ///  更新一条数据
        /// </summary>
        public static bool Check(UserPayBankAppInfo model)
        {
            try
            {
                int rowsAffected = 0;
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@SureTime", SqlDbType.DateTime),
					new SqlParameter("@SureUser", SqlDbType.Int,4)};
                parameters[0].Value = model.id;
                parameters[1].Value =(int)model.status;
                parameters[2].Value = model.SureTime;
                parameters[3].Value = model.SureUser;

                rowsAffected = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_userspaybankapp_Check", parameters);
                if (rowsAffected > 0)
                {
                    Factory.ClearCache(model.userid);
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
        public static bool Delete(int id)
        {
            try
            {
                int rowsAffected = 0;
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
};
                parameters[0].Value = id;

                rowsAffected = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_userspaybankapp_Delete", parameters);
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
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static UserPayBankAppInfo GetModel(int id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
};
            parameters[0].Value = id;

            UserPayBankAppInfo model = new UserPayBankAppInfo();
            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure,"proc_userspaybankapp_GetModel", parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["userid"].ToString() != "")
                {
                    model.userid = int.Parse(ds.Tables[0].Rows[0]["userid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["pmode"].ToString() != "")
                {
                    model.pmode = int.Parse(ds.Tables[0].Rows[0]["pmode"].ToString());
                }
                model.account = ds.Tables[0].Rows[0]["account"].ToString();
                model.payeeName = ds.Tables[0].Rows[0]["payeeName"].ToString();
                model.payeeBank = ds.Tables[0].Rows[0]["payeeBank"].ToString();
                model.bankProvince = ds.Tables[0].Rows[0]["bankProvince"].ToString();
                model.bankCity = ds.Tables[0].Rows[0]["bankCity"].ToString();
                model.bankAddress = ds.Tables[0].Rows[0]["bankAddress"].ToString();
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.status = (AcctChangeEnum)int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AddTime"].ToString() != "")
                {
                    model.AddTime = DateTime.Parse(ds.Tables[0].Rows[0]["AddTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SureTime"].ToString() != "")
                {
                    model.SureTime = DateTime.Parse(ds.Tables[0].Rows[0]["SureTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SureUser"].ToString() != "")
                {
                    model.SureUser = int.Parse(ds.Tables[0].Rows[0]["SureUser"].ToString());
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
        /// 根据搜索条件返回指定分页的商户信息。
        /// </summary>
        /// <param name="searchParams">搜索条件数组。</param>
        /// <param name="pageSize">分页大小。</param>
        /// <param name="page">页码。</param>
        /// <param name="orderby">排序方式。</param>
        /// <returns>分页数据。</returns>
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
                string where = BuilderWhere(searchParams, paramList);

                string sql = SqlHelper.GetCountSQL(tables, where, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL(SQL_TABLE_FIELDS, tables, where, orderby, key, pageSize, page, false);
                // PageData data = new PageData();

                ds = DataBase.ExecuteDataset(CommandType.Text, sql, paramList.ToArray());
                //using (IDataReader reader = DataBase.ExecuteReader(CommandType.Text, sql, paramList.ToArray()))
                //{
                //    if (!reader.Read())
                //    {
                //        return data;
                //    }
                //    data.RecordCount = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                //    if (!reader.NextResult())
                //    {
                //        return data;
                //    }
                //    while (reader.Read())
                //    {
                //        data.Items.Add(ReaderBind(reader));
                //    }
                //}
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
                        case "manageid":
                            builder.Append(" AND [manageId] = @manageId");
                            parameter = new SqlParameter("@manageId", SqlDbType.Int);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "status":
                            builder.Append(" AND [status] = @status");
                            parameter = new SqlParameter("@status", SqlDbType.TinyInt);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "stime":
                            builder.Append(" AND [AddTime] >= @stime");
                            parameter = new SqlParameter("@stime", SqlDbType.DateTime);
                            parameter.Value = iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "etime":
                            builder.Append(" AND [AddTime] <= @etime");
                            parameter = new SqlParameter("@etime", SqlDbType.DateTime);
                            parameter.Value = iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                    }
                }
            }
            return builder.ToString();
        }
    }
}
