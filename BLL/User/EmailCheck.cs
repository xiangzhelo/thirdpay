using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using viviapi.Model.User;
using DBAccess;
using viviLib.ExceptionHandling;
using viviapi.Model;
using viviLib.Data;

namespace viviapi.BLL.User
{
    /// <summary>
    /// 邮件验证
    /// </summary>
    public class EmailCheck
    {
        internal const string SQL_TABLE = "V_usersIdImage";
        internal const string SQL_FIELDS = @"[id]
      ,[userId]
      ,[ptype]
      ,[filesize]
      ,[ptype1]
      ,[filesize1]
      ,[status]
      ,[why]
      ,[admin]
      ,[checktime]
      ,[addtime]
      ,[userName],[payeeName],[account],[IdCard]";

        #region Exists
        /// <summary>
        ///  
        /// </summary>
        public bool Exists(int userid)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@userId", SqlDbType.Int,4)};
                parameters[0].Value = userid;

                return Convert.ToInt32(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_useremailcheck_Exists", parameters)) == 1;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }
        #endregion

        #region Add
        /// <summary>
        ///  增加一条数据
        /// </summary>
        public int Add(EmailCheckInfo model)
        {
            try
            {
                int rowsAffected;
                SqlParameter[] parameters = {                   
					new SqlParameter("@typeid", SqlDbType.TinyInt,1),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@email", SqlDbType.NVarChar,50),
					new SqlParameter("@addtime", SqlDbType.DateTime),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@checktime", SqlDbType.DateTime),
					new SqlParameter("@Expired", SqlDbType.DateTime),
                    new SqlParameter("@id", SqlDbType.Int,4)};
                parameters[0].Value = (int)model.typeid;
                parameters[1].Value = model.userid;
                parameters[2].Value = model.email;
                parameters[3].Value = model.addtime;
                parameters[4].Value = (int)model.status;
                parameters[5].Value = model.checktime;
                parameters[6].Value = model.Expired;
                parameters[7].Direction = ParameterDirection.Output;

                rowsAffected = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_useremailcheck_add", parameters);
                return (int)parameters[7].Value;
                //return model.id;
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
        ///  增加一条数据
        /// </summary>
        public bool Update(EmailCheckInfo model)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@typeid", SqlDbType.TinyInt,1),
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@email", SqlDbType.NVarChar,50),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@checktime", SqlDbType.DateTime),
                    new SqlParameter("@result", SqlDbType.TinyInt,1)
                                            };
                parameters[0].Value = model.id;
                parameters[1].Value = (int)model.typeid;
                parameters[2].Value = model.userid;
                parameters[3].Value = model.email;
                parameters[4].Value = (int)model.status;
                parameters[5].Value = model.checktime;
                parameters[6].Direction= ParameterDirection.Output;

                DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_useremailcheck_update", parameters);

                bool success = Convert.ToByte(parameters[6].Value) == 1;

                //bool success = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_useremailcheck_update", parameters) > 0;
                //if (success && model.status == EmailCheckStatus.已审核)
                //{
                //    Factory.ClearCache(model.userid);
                //}
                return success;
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
        ///  增加一条数据
        /// </summary>
        public bool Delete(int id)
        {
            try
            {
                int rowsAffected;
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
                parameters[0].Value = id;

                return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_useremailcheck_Delete", parameters) > 0;
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
        public EmailCheckInfo GetModel(int id)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
                parameters[0].Value = id;

                DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_useremailcheck_GetModel", parameters);
                return GetModelFromDs(ds);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }

        public EmailCheckInfo GetModelByUser(int userid)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.Int,4)};
                parameters[0].Value = userid;

                DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_useremailcheck_GetByUser", parameters);
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
        public static EmailCheckInfo GetModelFromDs(DataSet ds)
        {
            EmailCheckInfo model = new EmailCheckInfo();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["typeid"].ToString() != "")
                {
                    model.typeid = (EmailCheckType) int.Parse(ds.Tables[0].Rows[0]["typeid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["userid"].ToString() != "")
                {
                    model.userid = int.Parse(ds.Tables[0].Rows[0]["userid"].ToString());
                }
                model.email = ds.Tables[0].Rows[0]["email"].ToString();
                if (ds.Tables[0].Rows[0]["addtime"].ToString() != "")
                {
                    model.addtime = DateTime.Parse(ds.Tables[0].Rows[0]["addtime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.status = (EmailCheckStatus)int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["checktime"].ToString() != "")
                {
                    model.checktime = DateTime.Parse(ds.Tables[0].Rows[0]["checktime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Expired"].ToString() != "")
                {
                    model.Expired = DateTime.Parse(ds.Tables[0].Rows[0]["Expired"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        #endregion

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

    }
}