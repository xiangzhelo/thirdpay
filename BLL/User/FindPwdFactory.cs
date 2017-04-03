using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using viviLib.ExceptionHandling;
using viviapi.Model.User;
using viviLib;
using DBAccess;

namespace viviapi.BLL.User
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class FindPwdFactory
    {
        #region  Exists
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool Exists(int id)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
                parameters[0].Value = id;

                int result = Convert.ToInt32(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_findpwd_Exists", parameters));
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

        #region Add
        /// <summary>
        ///  增加一条数据
        /// </summary>
        public static int Add(FindPwd model)
        {
            try
            {
                int rowsAffected;
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@uid", SqlDbType.Int,4),
					new SqlParameter("@username", SqlDbType.VarChar,50),
					new SqlParameter("@oldpwd", SqlDbType.VarChar,100),
					new SqlParameter("@newpwd", SqlDbType.VarChar,100),
					new SqlParameter("@status", SqlDbType.Int,4),
					new SqlParameter("@addtimer", SqlDbType.DateTime)};
                parameters[0].Direction = ParameterDirection.Output;
                parameters[1].Value = model.uid;
                parameters[2].Value = model.username;
                parameters[3].Value = model.oldpwd;
                parameters[4].Value = model.newpwd;
                parameters[5].Value = model.status;
                parameters[6].Value = model.addtimer;

                rowsAffected = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_findpwd_add", parameters);
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
        public bool Update(FindPwd model)
        {
            try
            {
                int rowsAffected = 0;
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@uid", SqlDbType.Int,4),
					new SqlParameter("@username", SqlDbType.VarChar,50),
					new SqlParameter("@oldpwd", SqlDbType.VarChar,100),
					new SqlParameter("@newpwd", SqlDbType.VarChar,100),
					new SqlParameter("@status", SqlDbType.Int,4),
					new SqlParameter("@addtimer", SqlDbType.DateTime)};
                parameters[0].Value = model.id;
                parameters[1].Value = model.uid;
                parameters[2].Value = model.username;
                parameters[3].Value = model.oldpwd;
                parameters[4].Value = model.newpwd;
                parameters[5].Value = model.status;
                parameters[6].Value = model.addtimer;

                rowsAffected = DataBase.ExecuteNonQuery("proc_findpwd_Update", parameters);
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
        public static FindPwd GetModel(int id)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@id", SqlDbType.Int, 4) };
                parameters[0].Value = id;

                FindPwd model = new FindPwd();
                DataSet ds = DataBase.ExecuteDataset( CommandType.StoredProcedure,"proc_findpwd_GetModel", parameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                    {
                        model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["uid"].ToString() != "")
                    {
                        model.uid = int.Parse(ds.Tables[0].Rows[0]["uid"].ToString());
                    }
                    model.username = ds.Tables[0].Rows[0]["username"].ToString();
                    model.oldpwd = ds.Tables[0].Rows[0]["oldpwd"].ToString();
                    model.newpwd = ds.Tables[0].Rows[0]["newpwd"].ToString();
                    if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                    {
                        model.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["addtimer"].ToString() != "")
                    {
                        model.addtimer = DateTime.Parse(ds.Tables[0].Rows[0]["addtimer"].ToString());
                    }
                    return model;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
        #endregion

        #region Update
        /// <summary>
        ///  更新一条数据
        /// </summary>
        public static bool FindSucess(FindPwd model)
        {
            try
            {
                int rowsAffected = 0;
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@uid", SqlDbType.Int,4),
					new SqlParameter("@newpwd", SqlDbType.VarChar,100),
					new SqlParameter("@status", SqlDbType.Int,4)};
                parameters[0].Value = model.id;
                parameters[1].Value = model.uid;
                parameters[2].Value = model.newpwd;
                parameters[3].Value = model.status;

                rowsAffected = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_findpwd_success", parameters);
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
    }
}
