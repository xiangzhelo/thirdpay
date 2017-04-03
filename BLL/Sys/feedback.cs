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

namespace viviapi.BLL
{
    /// <summary>
    /// 
    /// </summary>
    public class JuBao
    {
        internal const string SQL_TABLE = "JuBao";
        internal const string SQL_FIELDS = @"id,[name],email,tel,url,[type],remark,addtime,status,checktime,[check],checkremark,pwd,field1,field2,field3";

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

                return Convert.ToInt32(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_JuBao_Exists", parameters)) == 1;
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
        public int Add(JuBaoInfo model)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@name", SqlDbType.NVarChar,50),
					new SqlParameter("@email", SqlDbType.NVarChar,30),
					new SqlParameter("@tel", SqlDbType.VarChar,20),
					new SqlParameter("@url", SqlDbType.NVarChar,200),
					new SqlParameter("@type", SqlDbType.TinyInt,1),
					new SqlParameter("@remark", SqlDbType.NVarChar,500),
					new SqlParameter("@addtime", SqlDbType.DateTime),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@checktime", SqlDbType.DateTime),
					new SqlParameter("@check", SqlDbType.Int,4),
					new SqlParameter("@checkremark", SqlDbType.NVarChar,500),
					new SqlParameter("@pwd", SqlDbType.NVarChar,20),
					new SqlParameter("@field1", SqlDbType.NVarChar,50),
					new SqlParameter("@field2", SqlDbType.NVarChar,50),
					new SqlParameter("@field3", SqlDbType.NVarChar,200)};
                parameters[0].Direction = ParameterDirection.Output;
                parameters[1].Value = model.name;
                parameters[2].Value = model.email;
                parameters[3].Value = model.tel;
                parameters[4].Value = model.url;
                parameters[5].Value = (int)model.type;
                parameters[6].Value = model.remark;
                parameters[7].Value = model.addtime;
                parameters[8].Value = (int)model.status;
                parameters[9].Value = model.checktime;
                parameters[10].Value = model.check;
                parameters[11].Value = model.checkremark;
                parameters[12].Value = model.pwd;
                parameters[13].Value = model.field1;
                parameters[14].Value = model.field2;
                parameters[15].Value = model.field3;

                DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_JuBao_add", parameters);
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
        ///  增加一条数据
        /// </summary>
        public bool Update(JuBaoInfo model)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@name", SqlDbType.NVarChar,50),
					new SqlParameter("@email", SqlDbType.NVarChar,30),
					new SqlParameter("@tel", SqlDbType.VarChar,20),
					new SqlParameter("@url", SqlDbType.NVarChar,200),
					new SqlParameter("@type", SqlDbType.TinyInt,1),
					new SqlParameter("@remark", SqlDbType.NVarChar,500),
					new SqlParameter("@addtime", SqlDbType.DateTime),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@checktime", SqlDbType.DateTime),
					new SqlParameter("@check", SqlDbType.Int,4),
					new SqlParameter("@checkremark", SqlDbType.NVarChar,500),
					new SqlParameter("@pwd", SqlDbType.NVarChar,20),
					new SqlParameter("@field1", SqlDbType.NVarChar,50),
					new SqlParameter("@field2", SqlDbType.NVarChar,50),
					new SqlParameter("@field3", SqlDbType.NVarChar,200)};
                parameters[0].Value = model.id;
                parameters[1].Value = model.name;
                parameters[2].Value = model.email;
                parameters[3].Value = model.tel;
                parameters[4].Value = model.url;
                parameters[5].Value = (int)model.type;
                parameters[6].Value = model.remark;
                parameters[7].Value = model.addtime;
                parameters[8].Value = (int)model.status;
                parameters[9].Value = model.checktime;
                parameters[10].Value = model.check;
                parameters[11].Value = model.checkremark;
                parameters[12].Value = model.pwd;
                parameters[13].Value = model.field1;
                parameters[14].Value = model.field2;
                parameters[15].Value = model.field3;

                bool success = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_JuBao_update", parameters) > 0;                
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
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
                parameters[0].Value = id;

                bool success = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_JuBao_Delete", parameters) > 0;             
                return success;
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
        public JuBaoInfo GetModel(int id)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
                parameters[0].Value = id;

                DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_JuBao_GetModel", parameters);
                return GetModelFromDs(ds);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public JuBaoInfo GetModelByPwd(string pwd)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@pwd", SqlDbType.NVarChar,20)};
                parameters[0].Value = pwd;

                DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_JuBao_GetModelBypwd", parameters);
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
        public JuBaoInfo GetModelFromDs(DataSet ds)
        {
            JuBaoInfo model = new JuBaoInfo();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["name"] != null && ds.Tables[0].Rows[0]["name"].ToString() != "")
                {
                    model.name = ds.Tables[0].Rows[0]["name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["email"] != null && ds.Tables[0].Rows[0]["email"].ToString() != "")
                {
                    model.email = ds.Tables[0].Rows[0]["email"].ToString();
                }
                if (ds.Tables[0].Rows[0]["tel"] != null && ds.Tables[0].Rows[0]["tel"].ToString() != "")
                {
                    model.tel = ds.Tables[0].Rows[0]["tel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["url"] != null && ds.Tables[0].Rows[0]["url"].ToString() != "")
                {
                    model.url = ds.Tables[0].Rows[0]["url"].ToString();
                }
                if (ds.Tables[0].Rows[0]["type"] != null && ds.Tables[0].Rows[0]["type"].ToString() != "")
                {
                    model.type = (JuBaoEnum)int.Parse(ds.Tables[0].Rows[0]["type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["remark"] != null && ds.Tables[0].Rows[0]["remark"].ToString() != "")
                {
                    model.remark = ds.Tables[0].Rows[0]["remark"].ToString();
                }
                if (ds.Tables[0].Rows[0]["addtime"] != null && ds.Tables[0].Rows[0]["addtime"].ToString() != "")
                {
                    model.addtime = DateTime.Parse(ds.Tables[0].Rows[0]["addtime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["status"] != null && ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.status =(viviapi.Model.JuBaoStatusEnum)int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["checktime"] != null && ds.Tables[0].Rows[0]["checktime"].ToString() != "")
                {
                    model.checktime = DateTime.Parse(ds.Tables[0].Rows[0]["checktime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["check"] != null && ds.Tables[0].Rows[0]["check"].ToString() != "")
                {
                    model.check = int.Parse(ds.Tables[0].Rows[0]["check"].ToString());
                }
                if (ds.Tables[0].Rows[0]["checkremark"] != null && ds.Tables[0].Rows[0]["checkremark"].ToString() != "")
                {
                    model.checkremark = ds.Tables[0].Rows[0]["checkremark"].ToString();
                }
                if (ds.Tables[0].Rows[0]["pwd"] != null && ds.Tables[0].Rows[0]["pwd"].ToString() != "")
                {
                    model.pwd = ds.Tables[0].Rows[0]["pwd"].ToString();
                }
                if (ds.Tables[0].Rows[0]["field1"] != null && ds.Tables[0].Rows[0]["field1"].ToString() != "")
                {
                    model.field1 = ds.Tables[0].Rows[0]["field1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["field2"] != null && ds.Tables[0].Rows[0]["field2"].ToString() != "")
                {
                    model.field2 = ds.Tables[0].Rows[0]["field2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["field3"] != null && ds.Tables[0].Rows[0]["field3"].ToString() != "")
                {
                    model.field3 = ds.Tables[0].Rows[0]["field3"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region GetList
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public  DataTable GetList(int userId)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@userId", SqlDbType.Int) };
                parameters[0].Value = userId;
                return DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_JuBao_GetList", parameters).Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
        #endregion

        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet ds = new DataSet();
            try
            {
                string tables = SQL_TABLE;
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "addtime desc";
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
        private  string BuilderWhere(List<SearchParam> param, List<SqlParameter> paramList)
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