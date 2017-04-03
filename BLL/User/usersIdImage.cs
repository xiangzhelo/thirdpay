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
    /// 
    /// </summary>
    public class usersIdImage
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

                return Convert.ToInt32(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_usersIdImage_Exists", parameters)) == 1;
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
        public int Add(usersIdImageInfo model)
        {
            try
            {
                int rowsAffected;
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@userId", SqlDbType.Int,4),
					new SqlParameter("@image_on", SqlDbType.Image),
					new SqlParameter("@image_down", SqlDbType.Image),
					new SqlParameter("@ptype", SqlDbType.NVarChar,20),
					new SqlParameter("@filesize", SqlDbType.Int,4),
					new SqlParameter("@ptype1", SqlDbType.NVarChar,20),
					new SqlParameter("@filesize1", SqlDbType.Int,4),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					//new SqlParameter("@why", SqlDbType.NVarChar,150),
					//new SqlParameter("@admin", SqlDbType.Int,4),
					//new SqlParameter("@checktime", SqlDbType.DateTime),
					new SqlParameter("@addtime", SqlDbType.DateTime)};
                parameters[0].Direction = ParameterDirection.Output;
                parameters[1].Value = model.userId;
                parameters[2].Value = model.image_on;
                parameters[3].Value = model.image_down;
                parameters[4].Value = model.ptype;
                parameters[5].Value = model.filesize;
                parameters[6].Value = model.ptype1;
                parameters[7].Value = model.filesize1;
                parameters[8].Value = (int)model.status;
                //parameters[9].Value = model.why;
                //parameters[10].Value = model.admin;
               // parameters[11].Value = model.checktime;
                parameters[9].Value = model.addtime;

                DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_usersIdImage_add", parameters);
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
        public bool Check(usersIdImageInfo model)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					//new SqlParameter("@userId", SqlDbType.Int,4),
                    //new SqlParameter("@image_on", SqlDbType.Image,16),
                    //new SqlParameter("@image_down", SqlDbType.Image,16),
                    //new SqlParameter("@ptype", SqlDbType.NVarChar,20),
                    //new SqlParameter("@filesize", SqlDbType.Int,4),
                    //new SqlParameter("@ptype1", SqlDbType.NVarChar,20),
                    //new SqlParameter("@filesize1", SqlDbType.Int,4),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@why", SqlDbType.NVarChar,150),
					new SqlParameter("@admin", SqlDbType.Int,4),
					new SqlParameter("@checktime", SqlDbType.DateTime),
                    //new SqlParameter("@addtime", SqlDbType.DateTime)
                                            };
                parameters[0].Value = model.id;
                //parameters[1].Value = model.userId;
                //parameters[2].Value = model.image_on;
                //parameters[3].Value = model.image_down;
                //parameters[4].Value = model.ptype;
                //parameters[5].Value = model.filesize;
                //parameters[6].Value = model.ptype1;
                //parameters[7].Value = model.filesize1;
                parameters[1].Value = (int)model.status;
                parameters[2].Value = model.why;
                parameters[3].Value = model.admin;
                parameters[4].Value = model.checktime;

                bool success = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_usersIdImage_update", parameters) > 0;
                if (success && model.status == IdImageStatus.审核成功)
                {
                    Factory.ClearCache(model.userId.Value);
                }
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

                return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_usersIdImage_Delete", parameters) > 0;
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
        public usersIdImageInfo GetModel(int id)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
                parameters[0].Value = id;

                DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_usersIdImage_GetModel", parameters);
                return GetModelFromDs(ds);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }

        public usersIdImageInfo GetModelByUser(int userid)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.Int,4)};
                parameters[0].Value = userid;

                DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_usersIdImage_GetByUser", parameters);
                return GetModelFromDs(ds);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }


        public usersIdImageInfo Get(int id)
        {
            usersIdImageInfo model = new usersIdImageInfo();
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            IDataReader dataReader = DataBase.ExecuteReader(CommandType.StoredProcedure, "proc_usersIdImage_GetModel", parameters);
            if (dataReader.Read())
            {
                object ojb = dataReader["image_on"];
                if (ojb != null && ojb != DBNull.Value)
                {
                    model.image_on = (byte[])ojb;
                }
                ojb = dataReader["image_down"];
                if (ojb != null && ojb != DBNull.Value)
                {
                    model.image_down = (byte[])ojb;
                }
                model.ptype = dataReader["ptype"].ToString();
                ojb = dataReader["filesize"];
                if (ojb != null && ojb != DBNull.Value)
                {
                    model.filesize = (int)ojb;
                }
                model.ptype1 = dataReader["ptype1"].ToString();
                ojb = dataReader["filesize1"];
                if (ojb != null && ojb != DBNull.Value)
                {
                    model.filesize1 = (int)ojb;
                }
            }
            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static usersIdImageInfo GetModelFromDs(DataSet ds)
        {
            usersIdImageInfo model = new usersIdImageInfo();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["userId"].ToString() != "")
                {
                    model.userId = int.Parse(ds.Tables[0].Rows[0]["userId"].ToString());
                }
                //if (ds.Tables[0].Rows[0]["image_on"].ToString() != "")
                //{
                //    model.image_on = (byte[])ds.Tables[0].Rows[0]["image_on"];
                //}
                //if (ds.Tables[0].Rows[0]["image_down"].ToString() != "")
                //{
                //    model.image_down = (byte[])ds.Tables[0].Rows[0]["image_down"];
                //}
                model.ptype = ds.Tables[0].Rows[0]["ptype"].ToString();
                if (ds.Tables[0].Rows[0]["filesize"].ToString() != "")
                {
                    model.filesize = int.Parse(ds.Tables[0].Rows[0]["filesize"].ToString());
                }
                model.ptype1 = ds.Tables[0].Rows[0]["ptype1"].ToString();
                if (ds.Tables[0].Rows[0]["filesize1"].ToString() != "")
                {
                    model.filesize1 = int.Parse(ds.Tables[0].Rows[0]["filesize1"].ToString());
                }
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.status = (IdImageStatus)int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                }
                else
                {
                    model.status = IdImageStatus.未知;
                }
                model.why = ds.Tables[0].Rows[0]["why"].ToString();
                if (ds.Tables[0].Rows[0]["admin"].ToString() != "")
                {
                    model.admin = int.Parse(ds.Tables[0].Rows[0]["admin"].ToString());
                }
                if (ds.Tables[0].Rows[0]["checktime"].ToString() != "")
                {
                    model.checktime = DateTime.Parse(ds.Tables[0].Rows[0]["checktime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["addtime"].ToString() != "")
                {
                    model.addtime = DateTime.Parse(ds.Tables[0].Rows[0]["addtime"].ToString());
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