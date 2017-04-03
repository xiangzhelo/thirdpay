using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using DBAccess;
using viviapi.Model;
using viviLib;
using viviLib.ExceptionHandling;
using viviLib.Data;

namespace viviapi.BLL
{
 
    /// <summary>
    /// 
    /// </summary>
    public class ManageFactory
    {
        public static string MANAGE_LOGIN_SESSIONID = "{90F37739-31E2-4b92-A35E-013313CE553D}";
        internal const string MANAGE_CONTEXT_KEY = "{F25E0AC4-032C-42ba-B123-2289C6DBE4F1}";
        internal const string MANAGE_SECOND_SESSIONID = "{36147A08-17F3-477a-8449-75AC0EF9299F}";        

        #region Update
        /// <summary>
        ///  增加一条数据
        /// </summary>
        public static int Add(Manage model)
        {
            try
            {
                int rowsAffected;
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@username", SqlDbType.VarChar,20),
					new SqlParameter("@password", SqlDbType.VarChar,100),
					new SqlParameter("@role", SqlDbType.Int,4),
					new SqlParameter("@status", SqlDbType.Int,4),
					new SqlParameter("@relname", SqlDbType.NVarChar,50),
					new SqlParameter("@lastLoginIp", SqlDbType.VarChar,50),
					new SqlParameter("@lastLoginTime", SqlDbType.DateTime),
					new SqlParameter("@sessionid", SqlDbType.VarChar,100),
                    new SqlParameter("@secondpwd", SqlDbType.VarChar,100),
                    new SqlParameter("@commissiontype", SqlDbType.TinyInt),
                    new SqlParameter("@commission", SqlDbType.Decimal,9),
                    new SqlParameter("@cardcommission", SqlDbType.Decimal,9),
                    new SqlParameter("@isSuperAdmin", SqlDbType.TinyInt,1),
                   new SqlParameter("@isAgent", SqlDbType.TinyInt,1)};
                parameters[0].Direction = ParameterDirection.Output;
                parameters[1].Value = model.username;
                parameters[2].Value = model.password;
                parameters[3].Value = model.role;
                parameters[4].Value = model.status;
                parameters[5].Value = model.relname;
                parameters[6].Value = model.lastLoginIp;
                parameters[7].Value = model.lastLoginTime;
                parameters[8].Value = model.sessionid;
                parameters[9].Value = model.secondpwd;
                parameters[10].Value = model.commissiontype;
                parameters[11].Value = model.commission;
                parameters[12].Value = model.cardcommission;
                parameters[13].Value = model.isSuperAdmin;
                parameters[14].Value = model.isAgent;


                rowsAffected = DataBase.ExecuteNonQuery(CommandType.StoredProcedure,"proc_manage_add", parameters);
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
        public static bool Update(Manage model)
        {
            try
            {
                int rowsAffected = 0;
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@username", SqlDbType.VarChar,20),
					new SqlParameter("@password", SqlDbType.VarChar,100),
					new SqlParameter("@role", SqlDbType.Int,4),
					new SqlParameter("@status", SqlDbType.Int,4),
					new SqlParameter("@relname", SqlDbType.NVarChar,50),
					new SqlParameter("@lastLoginIp", SqlDbType.VarChar,50),
					new SqlParameter("@lastLoginTime", SqlDbType.DateTime),
					new SqlParameter("@sessionid", SqlDbType.VarChar,50),
                    new SqlParameter("@secondpwd", SqlDbType.VarChar,100),
                    new SqlParameter("@commissiontype", SqlDbType.TinyInt),
                    new SqlParameter("@commission", SqlDbType.Decimal,9),
                    new SqlParameter("@cardcommission", SqlDbType.Decimal,9),
                    new SqlParameter("@isSuperAdmin", SqlDbType.TinyInt,1),                        
                    new SqlParameter("@isAgent", SqlDbType.TinyInt,1)
                                            };
                parameters[0].Value = model.id;
                parameters[1].Value = model.username;
                parameters[2].Value = model.password;
                parameters[3].Value = model.role;
                parameters[4].Value = model.status;
                parameters[5].Value = model.relname;
                parameters[6].Value = model.lastLoginIp;
                parameters[7].Value = model.lastLoginTime;
                parameters[8].Value = model.sessionid;
                parameters[9].Value = model.secondpwd;
                parameters[10].Value = model.commissiontype;
                parameters[11].Value = model.commission;
                parameters[12].Value = model.cardcommission;

                parameters[13].Value = model.isSuperAdmin;
                parameters[14].Value = model.isAgent;

                rowsAffected = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_manage_Update", parameters);
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
        public static Manage GetModel(int id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Manage model = new Manage();
            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_manage_get", parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.username = ds.Tables[0].Rows[0]["username"].ToString();
                model.password = ds.Tables[0].Rows[0]["password"].ToString();
                model.secondpwd = ds.Tables[0].Rows[0]["secondpwd"].ToString();
                if (ds.Tables[0].Rows[0]["role"].ToString() != "")
                {
                    model.role = (ManageRole)int.Parse(ds.Tables[0].Rows[0]["role"].ToString());
                }
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                }
                model.relname = ds.Tables[0].Rows[0]["relname"].ToString();
                model.lastLoginIp = ds.Tables[0].Rows[0]["lastLoginIp"].ToString();
                if (ds.Tables[0].Rows[0]["lastLoginTime"].ToString() != "")
                {
                    model.lastLoginTime = DateTime.Parse(ds.Tables[0].Rows[0]["lastLoginTime"].ToString());
                }
                model.sessionid = ds.Tables[0].Rows[0]["sessionid"].ToString();
                if (ds.Tables[0].Rows[0]["commissiontype"].ToString() != "")
                {
                    model.commissiontype = int.Parse(ds.Tables[0].Rows[0]["commissiontype"].ToString());
                }
                if (ds.Tables[0].Rows[0]["commission"].ToString() != "")
                {
                    model.commission = Decimal.Parse(ds.Tables[0].Rows[0]["commission"].ToString());
                }
                if (ds.Tables[0].Rows[0]["cardcommission"].ToString() != "")
                {
                    model.cardcommission = Decimal.Parse(ds.Tables[0].Rows[0]["cardcommission"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Balance"].ToString() != "")
                {
                    model.balance = Decimal.Parse(ds.Tables[0].Rows[0]["Balance"].ToString());
                }

                if (ds.Tables[0].Rows[0]["isSuperAdmin"].ToString() != "")
                {
                    model.isSuperAdmin = int.Parse(ds.Tables[0].Rows[0]["isSuperAdmin"].ToString());
                }
                if (ds.Tables[0].Rows[0]["isAgent"].ToString() != "")
                {
                    model.isAgent = int.Parse(ds.Tables[0].Rows[0]["isAgent"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region SignIn
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string SignIn(Manage manage)
        {
            string msg = string.Empty;

            try
            {
                if (manage == null || string.IsNullOrEmpty(manage.username) || string.IsNullOrEmpty(manage.password))
                {
                    msg = "请输入账号密码";
                    return msg;
                }

                string sessionId = Guid.NewGuid().ToString("b");

                SqlParameter[] prams = new SqlParameter[]{
                      DataBase.MakeInParam("@username", SqlDbType.VarChar, 50, manage.username)
                    , DataBase.MakeInParam("@password", SqlDbType.VarChar, 100, manage.password)
                    , DataBase.MakeInParam("@loginip", SqlDbType.VarChar, 50, manage.lastLoginIp)
                    , DataBase.MakeInParam("@logintime", SqlDbType.DateTime, 8, DateTime.Now) 
                    , DataBase.MakeInParam("@sessionId", SqlDbType.VarChar, 100, sessionId)   
                    , DataBase.MakeInParam("@address", SqlDbType.VarChar, 20, manage.LastLoginAddress) 
                    , DataBase.MakeInParam("@remark", SqlDbType.VarChar, 100, manage.LastLoginRemark) 
                };

                object success = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_manage_Login", prams);
                if (success != null && success != DBNull.Value)
                {
                    manage.id = (int)success;

                    HttpContext.Current.Session[MANAGE_LOGIN_SESSIONID] = sessionId;
                    msg = "登录成功";
                }
                else
                {
                    msg = "用户名或者密码错误!";
                }
                return msg;
            }
            catch (Exception ex)
            {
                msg = "登录失败";
                ExceptionHandler.HandleException(ex);
                return msg;
            }
        }

        #region GetCurrent
        /// <summary>
        /// 返回当前会话的会员信息。
        /// </summary>
        /// <returns>会员信息。</returns>
        public static int GetCurrent()
        {
            try
            {
             // return 37;

             //   return 9;

                object sessionId = HttpContext.Current.Session[MANAGE_LOGIN_SESSIONID];
                if (sessionId == null)
                    return 0;

                return GetIdBySession(sessionId);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public static int GetIdBySession(object sessionId)
        {
            try
            {
                var prams = new SqlParameter[]
                {
                    DataBase.MakeInParam("@sessionId", SqlDbType.VarChar, 100, sessionId)
                };

                object result = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_manage_getIdBySession", prams);
                if (result == DBNull.Value)
                    return 0;
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static object GetSessionId()
        {
            return  HttpContext.Current.Session[MANAGE_LOGIN_SESSIONID];
        }

        #endregion

        public static string GetManageRoleView(ManageRole role)
        {
            string viewName = string.Empty;
            switch (role)
            {
                case ManageRole.None:
                    viewName = "未知";
                    break;
                case ManageRole.News:
                    viewName = "新闻管理";
                    break;
                case ManageRole.Financial:
                    viewName = "财务管理";
                    break;
                case ManageRole.Interfaces:
                    viewName = "接口管理";
                    break;
                case ManageRole.Merchant:
                    viewName = "商户管理";
                    break;
                case ManageRole.Orders:
                    viewName = "订单管理";
                    break;
                case ManageRole.Report:
                    viewName = "统计报表";
                    break;
                case ManageRole.System:
                    viewName = "系统管理";
                    break;
                //case ManageRole.SuperAdmin:
                //    viewName = "超级管理员";
                //    break;

            }
            return viewName;

        }
        /// <summary>
        /// 返回当前登录商户。
        /// </summary>
        public static Manage CurrentManage
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.Items[MANAGE_CONTEXT_KEY] == null)
                    {
                        int manageId = GetCurrent();

                        if (manageId > 0)
                        {
                            HttpContext.Current.Items[MANAGE_CONTEXT_KEY] = GetModel(manageId);
                        }
                        else
                        {
                            return null;
                        }
                    }
                    return HttpContext.Current.Items[MANAGE_CONTEXT_KEY] as Manage;
                }
                return null;
            }
        }
        #endregion

        #region Delete
        public static bool Delete(int id)
        {
            try
            {
                int rowsAffected = 0;
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
                parameters[0].Value = id;

                rowsAffected = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_manage_del", parameters);
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

        #region SignOut
        /// <summary>
        /// 登出。
        /// </summary>
        public static void SignOut()
        {
            //if (CurrentMember != null)
            //{
            //    object[] paramValues = new object[]{
            //                                              Member.CurrentMember.MemberId ,
            //                                              SqlHelper.CleanString(HttpContext.Current.User.Identity.Name	, 50) ,
            //                                              SqlHelper.FormatDateTime(DateTime.Now)
            //                                          };

            //    SqlHelper.ExecuteNonQuery(SqlHelper.DefaultDatabase, SP_SIGNOUT, paramValues);
            //}
            HttpContext.Current.Items[MANAGE_CONTEXT_KEY] = null;
            HttpContext.Current.Session[MANAGE_LOGIN_SESSIONID] = null;
            HttpContext.Current.Session[MANAGE_SECOND_SESSIONID] = null;
        }
        #endregion

        #region GetList
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetList(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,commissiontype,commission,username,password,role,status,relname,lastLoginIp,lastLoginTime,sessionid,commissiontype,commission,CardCommission,Balance ");
            strSql.Append(" FROM V_manage ");

            if(!string.IsNullOrEmpty(where))
            {
                strSql.AppendFormat(" where {0}", where);
            }
            
            return DataBase.ExecuteDataset(CommandType.Text,strSql.ToString());
        }
        #endregion

        #region 二级密码
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsSecondPwdValid()
        {
            if (HttpContext.Current.Session[MANAGE_SECOND_SESSIONID] == null)
                return false;

            return Convert.ToBoolean(HttpContext.Current.Session[MANAGE_SECOND_SESSIONID]);
        }

        public static bool SecPwdVaild(string sedpwd)
        {
            if (string.IsNullOrEmpty(sedpwd))
                return false;

            if (viviLib.Security.Cryptography.MD5(sedpwd) == CurrentManage.secondpwd)
            {
                HttpContext.Current.Session[MANAGE_SECOND_SESSIONID] = true;
                return true;
            }
            return false;
        }

        public static void CheckSecondPwd()
        {
            if (!IsSecondPwdValid())
            {
                string url = HttpUtility.UrlEncode(HttpContext.Current.Request.Url.ToString());
                HttpContext.Current.Response.Redirect(string.Format("/{0}/login2.aspx" + "?RedirectUrl=" + url, viviapi.SysConfig.RuntimeSetting.ManagePagePath));
            }            
        }
        #endregion

        /// <summary>
        /// 删除登录日志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool LoginLogDel(int id)
        {
            try
            {
                int rowsAffected = 0;
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
                parameters[0].Value = id;

                rowsAffected = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_manageLoginLog_del", parameters);
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
        /// 
        /// </summary>
        /// <param name="searchParams"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet ds = new DataSet();
            try
            {
                string tables = "V_manageLoginLog";
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "lastTime desc";
                }

                List<SqlParameter> paramList = new List<SqlParameter>();
                string where = BuilderWhere(searchParams, paramList);

                string sql = SqlHelper.GetCountSQL(tables, where, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL(@"[id]
      ,[type]
      ,[manageID]
      ,[lastIP]
      ,[address]
      ,[remark]
      ,[lastTime]
      ,[sessionId]
      ,[username]
      ,[relname]", tables, where, orderby, key, pageSize, page, false);

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
                        case "manageid":
                            builder.Append(" AND [manageID] = @manageID");
                            parameter = new SqlParameter("@manageID", SqlDbType.Int);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "username":
                            builder.Append(" AND [userName] like @userName");
                            parameter = new SqlParameter("@userName", SqlDbType.VarChar, 20);
                            parameter.Value = "%" + SqlHelper.CleanString((string)iparam.ParamValue, 100) + "%";
                            paramList.Add(parameter);
                            break;
                        case "starttime":
                            builder.Append(" AND [lastTime] > @starttime");
                            parameter = new SqlParameter("@starttime", SqlDbType.DateTime);
                            parameter.Value = Convert.ToDateTime(iparam.ParamValue);
                            paramList.Add(parameter);
                            break;
                        case "endtime":
                            builder.Append(" AND [lastTime] < @endtime");
                            parameter = new SqlParameter("@endtime", SqlDbType.DateTime);
                            parameter.Value = Convert.ToDateTime(iparam.ParamValue);
                            paramList.Add(parameter);
                            break;
                    }
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int GetManageUsers(int id)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
                parameters[0].Value = id;

                return Convert.ToInt32(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_manage_getusers", parameters));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }


        /// <summary>
        /// 取业绩
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool GetManagePerformance(int id, DateTime begin, DateTime end, out decimal totalAmt, out decimal commission)
        {
            try
            {
                totalAmt = 0M;
                commission = 0M;

                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
                    new SqlParameter("@begin", SqlDbType.DateTime,8),
                    new SqlParameter("@end", SqlDbType.DateTime,8)};
                parameters[0].Value = id;
                parameters[1].Value = begin;
                parameters[2].Value = end;

                SqlDataReader dr =  DataBase.ExecuteReader(CommandType.StoredProcedure, "proc_manage_orderAmt", parameters);
                if (dr.Read())
                {
                    totalAmt = Convert.ToDecimal(dr["totalAmt"]);
                    commission = Convert.ToDecimal(dr["commission"]);
                    return true;
                }
                return false;
              
            }
            catch (Exception ex)
            {
                totalAmt = 0M;
                commission = 0M;
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }


        #region CheckCurrentPermission
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shouldSupper"></param>
        /// <param name="allowPermission"></param>
        /// <returns></returns>
        public static bool CheckCurrentPermission(bool shouldSupper, Model.ManageRole allowPermission)
        {
            if (CurrentManage == null)
                return false;

            if (shouldSupper == true)
                return CurrentManage.isSuperAdmin > 0;

            return CheckAdminPermission(CurrentManage.isSuperAdmin > 0,
                allowPermission,
                CurrentManage.role);
        }
        #endregion

        #region CheckAdminPermission
        /// <summary>
        /// 检查管理员权限
        /// </summary>
        /// <param name="isSupper">是否为超级管理员</param>
        /// <param name="allowPermission">需要权限</param>
        /// <param name="adminPermission">管理拥有权限</param>
        /// <returns></returns>
        public static bool CheckAdminPermission(bool isSupper
            , Model.ManageRole allowPermission
            , Model.ManageRole adminPermission)
        {
            if (isSupper)
                return true;

            //if ((allowPermission & ManageRole.None) == ManageRole.None)
            //    return true;

            if ((allowPermission & adminPermission) == allowPermission)
                return true;

            return false;
        }
        #endregion
    }
}

