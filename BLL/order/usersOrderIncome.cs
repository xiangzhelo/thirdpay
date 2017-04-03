using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Web;
using viviLib.ExceptionHandling;
using viviapi.Model;
using viviapi.Model.User;
using viviLib;
using DBAccess;
using viviLib.Data;

namespace viviapi.BLL.User
{
    /// <summary>
    /// 
    /// </summary>
    public class usersOrderIncome
    {
        internal const string USER_CONTEXT_KEY = "{FD7BE212-8537-427f-9EF6-1D1AABCA8EA3}";
        internal const string USER_LOGIN_SESSIONID = "{10E6C4EE-54C1-4895-8CDE-202A5B3DD9E9}";
        public static string USER_CACHE_KEY = Sys.Constant.CacheMark + "USER_{0}";
        
        internal const string SQL_BASE_TABLE = "userbase";
        internal const string SQL_BASE_TABLE_FIELD = "[id],[pwd2],[full_name],[userName],[password],[CPSDrate],[CVSNrate],[email],[qq],[tel],[idCard],[settles],[status],[regTime],[company],[linkMan],[agentId],[siteName],[siteUrl],[userType],[userLevel],[maxdaytocashTimes],[apiaccount],[apikey],[updatetime],[DESC],isRealNamePass,isEmailPass,isPhonePass,[classid],[isdebug]";

        internal const string SQL_PAYBANK_TABLE = "userspaybank";
        internal const string SQL_PAYBANK_TABLE_FIELD = "[userid],[pmode],[account],[payeeName],[payeeBank],[bankProvince],[bankCity],[bankAddress],[status]";


        internal const string SQL_TABLE = "V_Users";
        //money,pay,nopay,Integral
        internal const string SQL_TABLE_FIELD = "id,userName,password,CPSDrate,CVSNrate,email,qq,tel,idCard,pmode,settles,account,payeeName,payeeBank,bankProvince,bankCity,bankAddress,status,regTime,company,linkMan,agentId,siteName,siteUrl,userType,userLevel,maxdaytocashTimes,apiaccount,apikey,lastLoginIp,lastLoginTime,sessionId,manageId,isRealNamePass,full_name,classid,isdebug";
        internal const string FIELD_USER = @"[id]
      ,[userName]
      ,[password]
      ,[CPSDrate]
      ,[CVSNrate]
      ,[email]
      ,[qq]
      ,[tel]
      ,[idCard]
      ,[settles]
      ,[status]
      ,[regTime]
      ,[company]
      ,[linkMan]
      ,[agentId]
      ,[siteName]
      ,[siteUrl]
      ,[userType]
      ,[userLevel]
      ,[maxdaytocashTimes]
      ,[apiaccount]
      ,[apikey]
      ,[lastLoginIp]
      ,[lastLoginTime]
      ,[sessionId]
      ,[updatetime]
      ,[DESC]
      ,[userid]
      ,[pmode]
      ,[account]
      ,[payeeName]
      ,[payeeBank]
      ,[bankProvince]
      ,[bankCity]
      ,[bankAddress]
      ,[Integral]
      ,[balance]
      ,[payment]
      ,[unpayment]
      ,[enableAmt]
      ,[manageId]
      ,[isRealNamePass]
      ,[isPhonePass]
      ,[isEmailPass]
      ,[question]
      ,[answer]
      ,[smsNotifyUrl]
      ,[full_name]
      ,[classid]
      ,[Freeze]
      ,[schemename]
      ,[idCardtype]
      ,[msn]
      ,[fax]
      ,[province]
      ,[city]
      ,[zip]
      ,[field1],[levName]";

        #region chkAgent
        /// <summary>
        /// 
        /// </summary>
        /// <param name="agentid"></param>
        /// <returns></returns>
        public static bool chkAgent(int agentid)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@agentid", SqlDbType.Int,4)};

                parameters[0].Value = agentid;

                return Convert.ToBoolean(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_user_chkagent", parameters));

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
        /// 添加用户
        /// </summary>
        /// <param name="_userinfo"></param>
        /// <returns></returns>
        public static int Add(UserInfo _userinfo)
        {
            try
            {
                SqlParameter uidoutparam = DataBase.MakeOutParam("@id", SqlDbType.Int, 4);
                SqlParameter[] prams = new SqlParameter[] { 
                  uidoutparam
                , DataBase.MakeInParam("@userName", SqlDbType.VarChar, 50, _userinfo.UserName)
                , DataBase.MakeInParam("@password", SqlDbType.VarChar, 100, _userinfo.Password)                
                , DataBase.MakeInParam("@cpsdrate", SqlDbType.Int, 4, _userinfo.CPSDrate)
                , DataBase.MakeInParam("@cvsnrate", SqlDbType.Int, 4, _userinfo.CVSNrate)
                , DataBase.MakeInParam("@email", SqlDbType.VarChar, 50, _userinfo.Email)
                , DataBase.MakeInParam("@qq", SqlDbType.VarChar, 50, _userinfo.QQ)
                , DataBase.MakeInParam("@tel", SqlDbType.VarChar, 50, _userinfo.Tel)
                , DataBase.MakeInParam("@idCard", SqlDbType.VarChar, 50, _userinfo.IdCard)
                , DataBase.MakeInParam("@account", SqlDbType.VarChar, 50, _userinfo.Account)
                , DataBase.MakeInParam("@payeeName", SqlDbType.VarChar, 50, _userinfo.PayeeName)                
                , DataBase.MakeInParam("@payeeBank", SqlDbType.VarChar, 50, _userinfo.PayeeBank)
                , DataBase.MakeInParam("@bankProvince", SqlDbType.VarChar, 50, _userinfo.BankProvince)                
                , DataBase.MakeInParam("@bankCity", SqlDbType.VarChar, 50, _userinfo.BankCity)
                , DataBase.MakeInParam("@bankAddress", SqlDbType.VarChar, 50, _userinfo.BankAddress)
                , DataBase.MakeInParam("@status", SqlDbType.TinyInt, 1, _userinfo.Status)
                , DataBase.MakeInParam("@lastloginip", SqlDbType.VarChar, 50, _userinfo.LastLoginIp)
                , DataBase.MakeInParam("@lastlogintime", SqlDbType.DateTime, 8, _userinfo.LastLoginTime)
                , DataBase.MakeInParam("@regtime", SqlDbType.DateTime, 8, _userinfo.RegTime)                
                , DataBase.MakeInParam("@agentId", SqlDbType.Int, 4, _userinfo.AgentId)
                , DataBase.MakeInParam("@siteName", SqlDbType.VarChar, 50, _userinfo.SiteName)
                , DataBase.MakeInParam("@siteUrl", SqlDbType.VarChar, 100, _userinfo.SiteUrl)
                , DataBase.MakeInParam("@userType", SqlDbType.Int, 4, (int)_userinfo.UserType)
                , DataBase.MakeInParam("@userLevel", SqlDbType.Int, 4, (int)_userinfo.UserLevel)                               
                , DataBase.MakeInParam("@maxdaytocashTimes", SqlDbType.Int, 4, _userinfo.MaxDayToCashTimes)
                , DataBase.MakeInParam("@apiaccount", SqlDbType.BigInt, 8, _userinfo.APIAccount)
                , DataBase.MakeInParam("@apikey", SqlDbType.VarChar, 50, _userinfo.APIKey)  
                , DataBase.MakeInParam("@pmode", SqlDbType.TinyInt, 1, _userinfo.PMode) 
                , DataBase.MakeInParam("@settles", SqlDbType.TinyInt, 1, _userinfo.Settles) 
                , DataBase.MakeInParam("@DESC", SqlDbType.VarChar, 4000, _userinfo.Desc)
                , DataBase.MakeInParam("@manageId", SqlDbType.Int,4, _userinfo.manageId)
                , DataBase.MakeInParam("@question", SqlDbType.NVarChar,150, _userinfo.question)
                , DataBase.MakeInParam("@answer", SqlDbType.NVarChar,100, _userinfo.answer)
                , DataBase.MakeInParam("@full_name", SqlDbType.NVarChar,100, _userinfo.full_name)
                , DataBase.MakeInParam("@classid", SqlDbType.TinyInt,1, _userinfo.classid)
                , DataBase.MakeInParam("@pwd2", SqlDbType.NVarChar,50, _userinfo.Password2)
                , DataBase.MakeInParam("@linkman", SqlDbType.NVarChar,50, _userinfo.LinkMan)
                , DataBase.MakeInParam("@isdebug", SqlDbType.TinyInt,1, _userinfo.isdebug)
                , DataBase.MakeInParam("@idCardtype", SqlDbType.TinyInt,1, _userinfo.IdCardType)
                , DataBase.MakeInParam("@msn", SqlDbType.VarChar,30, _userinfo.msn)
                , DataBase.MakeInParam("@fax", SqlDbType.VarChar,20, _userinfo.fax)
                , DataBase.MakeInParam("@province", SqlDbType.VarChar,20, _userinfo.province)
                , DataBase.MakeInParam("@city", SqlDbType.VarChar,20, _userinfo.city)
                , DataBase.MakeInParam("@zip", SqlDbType.VarChar,8, _userinfo.zip)
                , DataBase.MakeInParam("@field1", SqlDbType.NVarChar,50, _userinfo.field1)
             };
                if (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_users_add", prams) > 0)
                {
                    _userinfo.ID = (int)uidoutparam.Value;
                    return _userinfo.ID;
                }
                return 0;
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
        /// 更新用户信息
        /// </summary>
        /// <param name="_userinfo"></param>
        /// <param name="changeList"></param>
        /// <returns></returns>
        public static bool Update1(UserInfo _userinfo)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                  DataBase.MakeInParam("@id", SqlDbType.Int, 4, _userinfo.ID)
                , DataBase.MakeInParam("@userName", SqlDbType.VarChar, 50, _userinfo.UserName)
                , DataBase.MakeInParam("@password", SqlDbType.VarChar, 100, _userinfo.Password)                
                , DataBase.MakeInParam("@cpsdrate", SqlDbType.Int, 4, _userinfo.CPSDrate)
                , DataBase.MakeInParam("@cvsnrate", SqlDbType.Int, 4, _userinfo.CVSNrate)
                , DataBase.MakeInParam("@email", SqlDbType.VarChar, 50, _userinfo.Email)
                , DataBase.MakeInParam("@qq", SqlDbType.VarChar, 50, _userinfo.QQ)
                , DataBase.MakeInParam("@tel", SqlDbType.VarChar, 50, _userinfo.Tel)
                , DataBase.MakeInParam("@idCard", SqlDbType.VarChar, 50, _userinfo.IdCard)
                , DataBase.MakeInParam("@account", SqlDbType.VarChar, 50, _userinfo.Account)
                , DataBase.MakeInParam("@payeeName", SqlDbType.VarChar, 50, _userinfo.PayeeName)                
                , DataBase.MakeInParam("@payeeBank", SqlDbType.VarChar, 50, _userinfo.PayeeBank)
                , DataBase.MakeInParam("@bankProvince", SqlDbType.VarChar, 50, _userinfo.BankProvince)                
                , DataBase.MakeInParam("@bankCity", SqlDbType.VarChar, 50, _userinfo.BankCity)
                , DataBase.MakeInParam("@bankAddress", SqlDbType.VarChar, 50, _userinfo.BankAddress)
                , DataBase.MakeInParam("@status", SqlDbType.TinyInt, 1, _userinfo.Status)               
                , DataBase.MakeInParam("@agentId", SqlDbType.Int, 4, _userinfo.AgentId)
                , DataBase.MakeInParam("@siteName", SqlDbType.VarChar, 50, _userinfo.SiteName)
                , DataBase.MakeInParam("@siteUrl", SqlDbType.VarChar, 100, _userinfo.SiteUrl)
                , DataBase.MakeInParam("@userType", SqlDbType.Int, 4, (int)_userinfo.UserType)
                , DataBase.MakeInParam("@userLevel", SqlDbType.Int, 4, (int)_userinfo.UserLevel)                               
                , DataBase.MakeInParam("@maxdaytocashTimes", SqlDbType.Int, 4, _userinfo.MaxDayToCashTimes)
                , DataBase.MakeInParam("@apiaccount", SqlDbType.BigInt, 8, _userinfo.APIAccount)
                , DataBase.MakeInParam("@apikey", SqlDbType.VarChar, 50, _userinfo.APIKey)    
                , DataBase.MakeInParam("@DESC", SqlDbType.VarChar, 4000, _userinfo.Desc)
                , DataBase.MakeInParam("@pmode", SqlDbType.Int, 4, _userinfo.PMode)
                , DataBase.MakeInParam("@updatetime", SqlDbType.DateTime, 8, DateTime.Now)
                , DataBase.MakeInParam("@manageId", SqlDbType.Int,4, _userinfo.manageId)
                , DataBase.MakeInParam("@isRealNamePass", SqlDbType.TinyInt,1, _userinfo.IsRealNamePass)
                , DataBase.MakeInParam("@isEmailPass", SqlDbType.TinyInt,1, _userinfo.IsEmailPass)
                , DataBase.MakeInParam("@isPhonePass", SqlDbType.TinyInt,1, _userinfo.IsPhonePass)
                , DataBase.MakeInParam("@smsNotifyUrl", SqlDbType.NVarChar,255, _userinfo.smsNotifyUrl)
                , DataBase.MakeInParam("@full_name", SqlDbType.NVarChar,50, _userinfo.full_name)
                , DataBase.MakeInParam("@male", SqlDbType.NVarChar,4, _userinfo.male)           
                //, DataBase.MakeInParam("@birthday", SqlDbType.DateTime,8, birtdate)
                , DataBase.MakeInParam("@addtress", SqlDbType.NVarChar,30, _userinfo.addtress)
                , DataBase.MakeInParam("@question", SqlDbType.NVarChar,150, _userinfo.question)
                , DataBase.MakeInParam("@answer", SqlDbType.NVarChar,100, _userinfo.answer)
                , DataBase.MakeInParam("@pwd2", SqlDbType.NVarChar,50, _userinfo.Password2)
                , DataBase.MakeInParam("@linkman", SqlDbType.NVarChar,50, _userinfo.LinkMan)
                , DataBase.MakeInParam("@classid", SqlDbType.TinyInt,1, _userinfo.classid)
                , DataBase.MakeInParam("@settles", SqlDbType.TinyInt, 1, _userinfo.Settles) 
                , DataBase.MakeInParam("@isdebug", SqlDbType.TinyInt,1, _userinfo.isdebug)
                , DataBase.MakeInParam("@idCardtype", SqlDbType.TinyInt,1, _userinfo.IdCardType)
                , DataBase.MakeInParam("@msn", SqlDbType.VarChar,30, _userinfo.msn)
                , DataBase.MakeInParam("@fax", SqlDbType.VarChar,20, _userinfo.fax)
                , DataBase.MakeInParam("@province", SqlDbType.VarChar,20, _userinfo.province)
                , DataBase.MakeInParam("@city", SqlDbType.VarChar,20, _userinfo.city)
                , DataBase.MakeInParam("@zip", SqlDbType.VarChar,8, _userinfo.zip)
                , DataBase.MakeInParam("@field1", SqlDbType.NVarChar,50, _userinfo.field1)
             };

            return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_users_Update", prams) > 0;

        }
        #endregion

        #region Update
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="_userinfo"></param>
        /// <param name="changeList"></param>
        /// <returns></returns>
        public static bool Update(UserInfo _userinfo, List<UsersUpdateLog> changeList)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                  DataBase.MakeInParam("@id", SqlDbType.Int, 4, _userinfo.ID)
                , DataBase.MakeInParam("@userName", SqlDbType.VarChar, 50, _userinfo.UserName)
                , DataBase.MakeInParam("@password", SqlDbType.VarChar, 100, _userinfo.Password)                
                , DataBase.MakeInParam("@cpsdrate", SqlDbType.Int, 4, _userinfo.CPSDrate)
                , DataBase.MakeInParam("@cvsnrate", SqlDbType.Int, 4, _userinfo.CVSNrate)
                , DataBase.MakeInParam("@email", SqlDbType.VarChar, 50, _userinfo.Email)
                , DataBase.MakeInParam("@qq", SqlDbType.VarChar, 50, _userinfo.QQ)
                , DataBase.MakeInParam("@tel", SqlDbType.VarChar, 50, _userinfo.Tel)
                , DataBase.MakeInParam("@idCard", SqlDbType.VarChar, 50, _userinfo.IdCard)
                , DataBase.MakeInParam("@account", SqlDbType.VarChar, 50, _userinfo.Account)
                , DataBase.MakeInParam("@payeeName", SqlDbType.VarChar, 50, _userinfo.PayeeName)                
                , DataBase.MakeInParam("@payeeBank", SqlDbType.VarChar, 50, _userinfo.PayeeBank)
                , DataBase.MakeInParam("@bankProvince", SqlDbType.VarChar, 50, _userinfo.BankProvince)                
                , DataBase.MakeInParam("@bankCity", SqlDbType.VarChar, 50, _userinfo.BankCity)
                , DataBase.MakeInParam("@bankAddress", SqlDbType.VarChar, 50, _userinfo.BankAddress)
                , DataBase.MakeInParam("@status", SqlDbType.TinyInt, 1, _userinfo.Status)               
                , DataBase.MakeInParam("@agentId", SqlDbType.Int, 4, _userinfo.AgentId)
                , DataBase.MakeInParam("@siteName", SqlDbType.VarChar, 50, _userinfo.SiteName)
                , DataBase.MakeInParam("@siteUrl", SqlDbType.VarChar, 100, _userinfo.SiteUrl)
                , DataBase.MakeInParam("@userType", SqlDbType.Int, 4, (int)_userinfo.UserType)
                , DataBase.MakeInParam("@userLevel", SqlDbType.Int, 4, (int)_userinfo.UserLevel)                               
                , DataBase.MakeInParam("@maxdaytocashTimes", SqlDbType.Int, 4, _userinfo.MaxDayToCashTimes)
                , DataBase.MakeInParam("@apiaccount", SqlDbType.BigInt, 8, _userinfo.APIAccount)
                , DataBase.MakeInParam("@apikey", SqlDbType.VarChar, 50, _userinfo.APIKey)    
                , DataBase.MakeInParam("@DESC", SqlDbType.VarChar, 4000, _userinfo.Desc)
                , DataBase.MakeInParam("@pmode", SqlDbType.Int, 4, _userinfo.PMode)
                , DataBase.MakeInParam("@updatetime", SqlDbType.DateTime, 8, DateTime.Now)
                , DataBase.MakeInParam("@manageId", SqlDbType.Int,4, _userinfo.manageId)
                , DataBase.MakeInParam("@isRealNamePass", SqlDbType.TinyInt,1, _userinfo.IsRealNamePass)
                , DataBase.MakeInParam("@isEmailPass", SqlDbType.TinyInt,1, _userinfo.IsEmailPass)
                , DataBase.MakeInParam("@isPhonePass", SqlDbType.TinyInt,1, _userinfo.IsPhonePass)
                , DataBase.MakeInParam("@smsNotifyUrl", SqlDbType.NVarChar,255, _userinfo.smsNotifyUrl)
                , DataBase.MakeInParam("@full_name", SqlDbType.NVarChar,50, _userinfo.full_name)
                , DataBase.MakeInParam("@male", SqlDbType.NVarChar,4, _userinfo.male)           
                //, DataBase.MakeInParam("@birthday", SqlDbType.DateTime,8, birtdate)
                , DataBase.MakeInParam("@addtress", SqlDbType.NVarChar,30, _userinfo.addtress)
                , DataBase.MakeInParam("@question", SqlDbType.NVarChar,150, _userinfo.question)
                , DataBase.MakeInParam("@answer", SqlDbType.NVarChar,100, _userinfo.answer)
                , DataBase.MakeInParam("@pwd2", SqlDbType.NVarChar,50, _userinfo.Password2)
                , DataBase.MakeInParam("@linkman", SqlDbType.NVarChar,50, _userinfo.LinkMan)
                , DataBase.MakeInParam("@classid", SqlDbType.TinyInt,1, _userinfo.classid)
                , DataBase.MakeInParam("@settles", SqlDbType.TinyInt, 1, _userinfo.Settles) 
                , DataBase.MakeInParam("@isdebug", SqlDbType.TinyInt,1, _userinfo.isdebug)
                , DataBase.MakeInParam("@idCardtype", SqlDbType.TinyInt,1, _userinfo.IdCardType)
                , DataBase.MakeInParam("@msn", SqlDbType.VarChar,30, _userinfo.msn)
                , DataBase.MakeInParam("@fax", SqlDbType.VarChar,20, _userinfo.fax)
                , DataBase.MakeInParam("@province", SqlDbType.VarChar,20, _userinfo.province)
                , DataBase.MakeInParam("@city", SqlDbType.VarChar,20, _userinfo.city)
                , DataBase.MakeInParam("@zip", SqlDbType.VarChar,8, _userinfo.zip)
                , DataBase.MakeInParam("@field1", SqlDbType.NVarChar,50, _userinfo.field1)
             };

            using (SqlConnection conn = new SqlConnection(DataBase.ConnectionString))
            {
                conn.Open();

                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        if (changeList != null)
                        {
                            foreach (UsersUpdateLog model in changeList)
                            {
                                SqlParameter[] parameters = {
					             new SqlParameter("@userid", SqlDbType.Int,4)
                                ,new SqlParameter("@field", SqlDbType.VarChar,20)
                                ,new SqlParameter("@oldValue", SqlDbType.VarChar,100)
                                ,new SqlParameter("@newvalue", SqlDbType.VarChar,100)
                                ,new SqlParameter("@Addtime", SqlDbType.DateTime)
                                ,new SqlParameter("@editor", SqlDbType.VarChar,50)
                                ,new SqlParameter("@oIp", SqlDbType.VarChar,50)
                                ,new SqlParameter("@desc", SqlDbType.VarChar,4000)};
                                parameters[0].Value = model.userid;
                                parameters[1].Value = model.field;
                                parameters[2].Value = model.oldValue;
                                parameters[3].Value = model.newvalue;
                                parameters[4].Value = model.Addtime;
                                parameters[5].Value = model.Editor;
                                parameters[6].Value = model.OIp;
                                parameters[7].Value = model.Desc;

                                if (DataBase.ExecuteNonQuery(trans, "proc_usersupdate_add", parameters) < 0)
                                {
                                    trans.Rollback();
                                    conn.Close();
                                    return false;
                                }
                            }
                        }
                        if (DataBase.ExecuteNonQuery(trans, "proc_users_Update", prams) > 0)
                        {
                            HttpContext.Current.Items[USER_CONTEXT_KEY] = null;
                            trans.Commit();
                            conn.Close();

                            //清除日志
                            ClearCache(_userinfo.ID);
                            return true;
                        }
                        else
                        {
                            trans.Rollback();
                            conn.Close();
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        ExceptionHandler.HandleException(ex);
                        return false;
                    }
                }

            }

        }
        #endregion

        #region Exists
        /// <summary>
        ///  是否已经存在用户账号
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static bool Exists(string username)
        {
            try
            {
                bool result = false;

                SqlParameter[] prams = new SqlParameter[] { 
                DataBase.MakeInParam("@userName", SqlDbType.NVarChar, 50, username)
            };
                object exists = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_users_Exists", prams);
                if (exists != null && exists != DBNull.Value)
                    result = Convert.ToBoolean(exists);

                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        /// <summary>
        ///  999 代表不存在
        ///  0 存在未审核
        ///  1	--待审核
        ///  2	--正常
        ///  3	--锁定
        ///  4	--审核失败
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static int EmailExists(string email)
        {
            try
            {
                int result = 999;

                SqlParameter[] prams = new SqlParameter[] { 
                DataBase.MakeInParam("@email", SqlDbType.NVarChar, 50, email)
            };
                object exists = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_users_EmailExists", prams);
                if (exists != null && exists != DBNull.Value)
                    result = Convert.ToInt32(exists);

                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 999;
            }
        }

        /// <summary>
        ///  是否已经存在用户账号
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static bool Exists(int userId)
        {
            try
            {
                bool result = false;

                SqlParameter[] prams = new SqlParameter[] { 
                DataBase.MakeInParam("@userId", SqlDbType.Int,4, userId)
            };
                object exists = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_users_ExistsId", prams);
                if (exists != null && exists != DBNull.Value)
                    result = Convert.ToBoolean(exists);

                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }
        #endregion

        #region Del
        /// <summary>
        ///  是否已经存在用户账号
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static bool Del(int userId)
        {
            try
            {
                bool result = false;

                SqlParameter[] prams = new SqlParameter[] { 
                DataBase.MakeInParam("@id", SqlDbType.Int,4, userId)
            };
               
                result = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_users_del", prams) > 0;
                if (result)
                {
                    ClearCache(userId);
                }
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }
        #endregion

        #region GetModel
        #region GetUserBaseInfo
        /// <summary>
        /// 获取基本信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static UserInfo GetCacheUserBaseInfo(int uid)
        {
            UserInfo model = new UserInfo();
            string cacheKey = string.Format(USER_CACHE_KEY, uid);
            model = (UserInfo)viviapi.Cache.WebCache.GetCacheService().RetrieveObject(cacheKey);

            if (model == null)
            {
                IDictionary<string, object> sqldepparms = new Dictionary<string, object>();
                sqldepparms.Add("id", uid);
                SqlDependency sqlDep = DataBase.AddSqlDependency(cacheKey, SQL_BASE_TABLE, SQL_BASE_TABLE_FIELD, "[id]=@id", sqldepparms);

                model = GetModel(uid);
                viviapi.Cache.WebCache.GetCacheService().AddObject(cacheKey, model);
            }

            return model;
        }

        #region GetBaseModel
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static UserInfo GetBaseModel(int uid)
        {
            SqlParameter[] parameters = { new SqlParameter("@id", SqlDbType.Int, 4) };
            parameters[0].Value = uid;

            UserInfo model = new UserInfo();
            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_userbase_get", parameters);
            model = GetBaseModelFromDs(ds);

            return model;
        }
        #endregion

        #region GetBaseModelFromDs
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static UserInfo GetBaseModelFromDs(DataSet ds)
        {
            UserInfo model = new UserInfo();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["classid"].ToString() != "")
                {
                    model.classid = int.Parse(ds.Tables[0].Rows[0]["classid"].ToString());
                }
               
                model.UserName = ds.Tables[0].Rows[0]["userName"].ToString();
                model.Password = ds.Tables[0].Rows[0]["password"].ToString();
                model.Password2 = ds.Tables[0].Rows[0]["pwd2"].ToString();
                if (ds.Tables[0].Rows[0]["CPSDrate"].ToString() != "")
                {
                    model.CPSDrate = int.Parse(ds.Tables[0].Rows[0]["CPSDrate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CVSNrate"].ToString() != "")
                {
                    model.CVSNrate = int.Parse(ds.Tables[0].Rows[0]["CVSNrate"].ToString());
                }
                model.Email = ds.Tables[0].Rows[0]["email"].ToString();
                model.QQ = ds.Tables[0].Rows[0]["qq"].ToString();
                model.Tel = ds.Tables[0].Rows[0]["tel"].ToString();
                model.IdCard = ds.Tables[0].Rows[0]["idCard"].ToString();
                model.full_name = ds.Tables[0].Rows[0]["full_name"].ToString();
                model.LinkMan = ds.Tables[0].Rows[0]["LinkMan"].ToString();
                //model.Account = ds.Tables[0].Rows[0]["account"].ToString();
                //model.PayeeName = ds.Tables[0].Rows[0]["payeeName"].ToString();
                //model.PayeeBank = ds.Tables[0].Rows[0]["payeeBank"].ToString();
                //model.BankProvince = ds.Tables[0].Rows[0]["bankProvince"].ToString();
                //model.BankCity = ds.Tables[0].Rows[0]["bankCity"].ToString();
                //model.BankAddress = ds.Tables[0].Rows[0]["bankAddress"].ToString();
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["regTime"].ToString() != "")
                {
                    model.RegTime = DateTime.Parse(ds.Tables[0].Rows[0]["regTime"].ToString());
                }               
                if (ds.Tables[0].Rows[0]["agentId"].ToString() != "")
                {
                    model.AgentId = int.Parse(ds.Tables[0].Rows[0]["agentId"].ToString());
                }
                model.SiteName = ds.Tables[0].Rows[0]["siteName"].ToString();
                model.SiteUrl = ds.Tables[0].Rows[0]["siteUrl"].ToString();
                if (ds.Tables[0].Rows[0]["userType"].ToString() != "")
                {
                    model.UserType = (UserTypeEnum)int.Parse(ds.Tables[0].Rows[0]["userType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["userLevel"].ToString() != "")
                {
                    model.UserLevel = int.Parse(ds.Tables[0].Rows[0]["userLevel"].ToString());
                }               
                if (ds.Tables[0].Rows[0]["maxdaytocashTimes"].ToString() != "")
                {
                    model.MaxDayToCashTimes = int.Parse(ds.Tables[0].Rows[0]["maxdaytocashTimes"].ToString());
                }
                if (ds.Tables[0].Rows[0]["apiaccount"].ToString() != "")
                {
                    model.APIAccount = long.Parse(ds.Tables[0].Rows[0]["apiaccount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["manageId"].ToString() != "")
                {
                    model.manageId = int.Parse(ds.Tables[0].Rows[0]["manageId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["isRealNamePass"].ToString() != "")
                {
                    model.IsRealNamePass = int.Parse(ds.Tables[0].Rows[0]["isRealNamePass"].ToString());
                }
                else
                {
                    model.IsRealNamePass = 0;
                }

                if (ds.Tables[0].Rows[0]["isEmailPass"].ToString() != "")
                {
                    model.IsEmailPass = int.Parse(ds.Tables[0].Rows[0]["isEmailPass"].ToString());
                }
                else
                {
                    model.IsEmailPass = 0;
                }
                if (ds.Tables[0].Rows[0]["isPhonePass"].ToString() != "")
                {
                    model.IsPhonePass = int.Parse(ds.Tables[0].Rows[0]["isPhonePass"].ToString());
                }
                else
                {
                    model.IsPhonePass = 0;
                }
                if (ds.Tables[0].Rows[0]["settles"].ToString() != "")
                {
                    model.Settles = int.Parse(ds.Tables[0].Rows[0]["settles"].ToString());
                }
                else
                {
                    model.Settles = 1;
                }
                model.question = ds.Tables[0].Rows[0]["question"].ToString();
                model.answer = ds.Tables[0].Rows[0]["answer"].ToString();
                model.APIKey = ds.Tables[0].Rows[0]["APIkey"].ToString();
                model.smsNotifyUrl = ds.Tables[0].Rows[0]["smsNotifyUrl"].ToString();
                if (ds.Tables[0].Rows[0]["isdebug"].ToString() != "")
                {
                    model.isdebug = int.Parse(ds.Tables[0].Rows[0]["isdebug"].ToString());
                }
            }
            return model;
        }
        #endregion

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static UserInfo GetCacheModel(int uid)
        { 
            UserInfo model = new UserInfo();

            string cacheKey = string.Format(USER_CACHE_KEY, uid);
            model = (UserInfo)viviapi.Cache.WebCache.GetCacheService().RetrieveObject(cacheKey);

            if (model == null)
            {
                IDictionary<string, object> sqldepparms = new Dictionary<string, object>();
                sqldepparms.Add("id", uid);
                SqlDependency sqlDep    = DataBase.AddSqlDependency(cacheKey, SQL_BASE_TABLE, SQL_BASE_TABLE_FIELD, "[id]=@id", sqldepparms);
                SqlDependency sqlDep2   = DataBase.AddSqlDependency(cacheKey, SQL_PAYBANK_TABLE, SQL_PAYBANK_TABLE_FIELD, "[userid]=@id", sqldepparms);

                model = GetModel(uid);
                viviapi.Cache.WebCache.GetCacheService().AddObject(cacheKey, model);
            }

            return model;
        }

        #region GetModel
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static UserInfo GetModel(int uid)
        {
            SqlParameter[] parameters = { new SqlParameter("@id", SqlDbType.Int, 4) };
            parameters[0].Value = uid;

            UserInfo model = new UserInfo();
            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_users_get", parameters);
            model = GetModelFromDs(ds);

            return model;
        }
        #endregion

        #region GetModelByName
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static UserInfo GetModelByName(string userName)
        {
            SqlParameter[] parameters = { new SqlParameter("@userName", SqlDbType.VarChar, 20) };
            parameters[0].Value = userName;

            UserInfo model = new UserInfo();
            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_users_getbyname", parameters);
            model = GetModelFromDs(ds);

            return model;
        }
        #endregion

        #region GetPromSuperior
        /// <summary>
        /// 取用户的上级代理
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static UserInfo GetPromSuperior(int userId)
        {
            string sql = @"SELECT u.* FROM userbase u inner JOIN PromotionUser pu ON u.id = pu.PID
WHERE pu.RegId = @RegId";

            SqlParameter[] parameters = { new SqlParameter("@RegId", SqlDbType.Int, 4) };
            parameters[0].Value = userId;

            UserInfo model = new UserInfo();
            DataSet ds = DataBase.ExecuteDataset(CommandType.Text, sql, parameters);
            model = GetBaseModelFromDs(ds);

            return model;
        }

        public static int GetPromID(int userid)
        {
            try
            {
                string sql = @"SELECT top 1 pid FROM PromotionUser with(nolock) WHERE regid=@userid ";

                SqlParameter[] parameters = { new SqlParameter("@userid", SqlDbType.Int, 4) };
                parameters[0].Value = userid;

                return Convert.ToInt32(DataBase.ExecuteScalar(CommandType.Text, sql, parameters));
            }
            catch (Exception ex)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
                return 0;
            }
        }
        #endregion

        #region GetModelFromDs
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static UserInfo GetModelFromDs(DataSet ds)
        {
            UserInfo model = new UserInfo();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["classid"].ToString() != "")
                {
                    model.classid = int.Parse(ds.Tables[0].Rows[0]["classid"].ToString());
                }
                model.UserName = ds.Tables[0].Rows[0]["userName"].ToString();
                model.Password = ds.Tables[0].Rows[0]["password"].ToString();
                model.Password2 = ds.Tables[0].Rows[0]["pwd2"].ToString();
                if (ds.Tables[0].Rows[0]["CPSDrate"].ToString() != "")
                {
                    model.CPSDrate = int.Parse(ds.Tables[0].Rows[0]["CPSDrate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CVSNrate"].ToString() != "")
                {
                    model.CVSNrate = int.Parse(ds.Tables[0].Rows[0]["CVSNrate"].ToString());
                }
                model.LinkMan = ds.Tables[0].Rows[0]["LinkMan"].ToString();
                model.Email = ds.Tables[0].Rows[0]["email"].ToString();
                model.QQ = ds.Tables[0].Rows[0]["qq"].ToString();
                model.Tel = ds.Tables[0].Rows[0]["tel"].ToString();
                model.IdCard = ds.Tables[0].Rows[0]["idCard"].ToString();
                model.Account = ds.Tables[0].Rows[0]["account"].ToString();
                model.PayeeName = ds.Tables[0].Rows[0]["payeeName"].ToString();
                model.PayeeBank = ds.Tables[0].Rows[0]["payeeBank"].ToString();
                model.BankProvince = ds.Tables[0].Rows[0]["bankProvince"].ToString();
                model.BankCity = ds.Tables[0].Rows[0]["bankCity"].ToString();
                model.BankAddress = ds.Tables[0].Rows[0]["bankAddress"].ToString();
                model.full_name = ds.Tables[0].Rows[0]["full_name"].ToString();
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["regTime"].ToString() != "")
                {
                    model.RegTime = DateTime.Parse(ds.Tables[0].Rows[0]["regTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["balance"].ToString() != "")
                {
                    model.Balance = decimal.Parse(ds.Tables[0].Rows[0]["balance"].ToString());
                }
                if (ds.Tables[0].Rows[0]["payment"].ToString() != "")
                {
                    model.Payment = decimal.Parse(ds.Tables[0].Rows[0]["payment"].ToString());
                }
                if (ds.Tables[0].Rows[0]["unpayment"].ToString() != "")
                {
                    model.Unpayment = decimal.Parse(ds.Tables[0].Rows[0]["unpayment"].ToString());
                }
                if (ds.Tables[0].Rows[0]["enableAmt"].ToString() != "")
                {
                    model.enableAmt = decimal.Parse(ds.Tables[0].Rows[0]["enableAmt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["agentId"].ToString() != "")
                {
                    model.AgentId = int.Parse(ds.Tables[0].Rows[0]["agentId"].ToString());
                }
                model.SiteName = ds.Tables[0].Rows[0]["siteName"].ToString();
                model.SiteUrl = ds.Tables[0].Rows[0]["siteUrl"].ToString();
                if (ds.Tables[0].Rows[0]["userType"].ToString() != "")
                {
                    model.UserType = (UserTypeEnum)int.Parse(ds.Tables[0].Rows[0]["userType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["userLevel"].ToString() != "")
                {
                    model.UserLevel = int.Parse(ds.Tables[0].Rows[0]["userLevel"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Integral"].ToString() != "")
                {
                    model.Integral = int.Parse(ds.Tables[0].Rows[0]["Integral"].ToString());
                }
                if (ds.Tables[0].Rows[0]["maxdaytocashTimes"].ToString() != "")
                {
                    model.MaxDayToCashTimes = int.Parse(ds.Tables[0].Rows[0]["maxdaytocashTimes"].ToString());
                }
                if (ds.Tables[0].Rows[0]["apiaccount"].ToString() != "")
                {
                    model.APIAccount = long.Parse(ds.Tables[0].Rows[0]["apiaccount"].ToString());
                }
                model.APIKey = ds.Tables[0].Rows[0]["APIkey"].ToString();
                model.LastLoginIp = ds.Tables[0].Rows[0]["lastLoginIp"].ToString();
                if (ds.Tables[0].Rows[0]["lastLoginTime"].ToString() != "")
                {
                    model.LastLoginTime = DateTime.Parse(ds.Tables[0].Rows[0]["lastLoginTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["pmode"].ToString() != "")
                {
                    model.PMode = int.Parse(ds.Tables[0].Rows[0]["pmode"].ToString());
                }
                model.Desc = ds.Tables[0].Rows[0]["Desc"].ToString();
                if (ds.Tables[0].Rows[0]["manageId"].ToString() != "")
                {
                    model.manageId = int.Parse(ds.Tables[0].Rows[0]["manageId"].ToString());
                }

                if (ds.Tables[0].Rows[0]["isRealNamePass"].ToString() != "")
                {
                    model.IsRealNamePass = int.Parse(ds.Tables[0].Rows[0]["isRealNamePass"].ToString());
                }
                else
                {
                    model.IsRealNamePass = 0;
                }

                if (ds.Tables[0].Rows[0]["isEmailPass"].ToString() != "")
                {
                    model.IsEmailPass = int.Parse(ds.Tables[0].Rows[0]["isEmailPass"].ToString());
                }
                else
                {
                    model.IsEmailPass = 0;
                }
                if (ds.Tables[0].Rows[0]["isPhonePass"].ToString() != "")
                {
                    model.IsPhonePass = int.Parse(ds.Tables[0].Rows[0]["isPhonePass"].ToString());
                }
                else
                {
                    model.IsPhonePass = 0;
                }
                if (ds.Tables[0].Rows[0]["settles"].ToString() != "")
                {
                    model.Settles = int.Parse(ds.Tables[0].Rows[0]["settles"].ToString());
                }
                else
                {
                    model.Settles = 1;
                }
                model.question = ds.Tables[0].Rows[0]["question"].ToString();
                model.answer = ds.Tables[0].Rows[0]["answer"].ToString();
                model.smsNotifyUrl = ds.Tables[0].Rows[0]["smsNotifyUrl"].ToString();
                if (ds.Tables[0].Rows[0]["isdebug"].ToString() != "")
                {
                    model.isdebug = int.Parse(ds.Tables[0].Rows[0]["isdebug"].ToString());
                }
                if (ds.Tables[0].Rows[0]["idCardtype"] != null && ds.Tables[0].Rows[0]["idCardtype"].ToString() != "")
                {
                    model.IdCardType = int.Parse(ds.Tables[0].Rows[0]["idCardtype"].ToString());
                }
                if (ds.Tables[0].Rows[0]["msn"] != null && ds.Tables[0].Rows[0]["msn"].ToString() != "")
                {
                    model.msn = ds.Tables[0].Rows[0]["msn"].ToString();
                }
                if (ds.Tables[0].Rows[0]["fax"] != null && ds.Tables[0].Rows[0]["fax"].ToString() != "")
                {
                    model.fax = ds.Tables[0].Rows[0]["fax"].ToString();
                }
                if (ds.Tables[0].Rows[0]["province"] != null && ds.Tables[0].Rows[0]["province"].ToString() != "")
                {
                    model.province = ds.Tables[0].Rows[0]["province"].ToString();
                }
                if (ds.Tables[0].Rows[0]["city"] != null && ds.Tables[0].Rows[0]["city"].ToString() != "")
                {
                    model.city = ds.Tables[0].Rows[0]["city"].ToString();
                }
                if (ds.Tables[0].Rows[0]["zip"] != null && ds.Tables[0].Rows[0]["zip"].ToString() != "")
                {
                    model.zip = ds.Tables[0].Rows[0]["zip"].ToString();
                }
                if (ds.Tables[0].Rows[0]["field1"] != null && ds.Tables[0].Rows[0]["field1"].ToString() != "")
                {
                    model.field1 = ds.Tables[0].Rows[0]["field1"].ToString();
                }
               
            }
            return model;
        }
        #endregion

        public static string GetClassViewName(int classid)
        {
            string viewName = string.Empty;
            if (classid == 0)
            {
                viewName = "个人";
            }
            else if (classid == 1)
            {
                viewName = "企业";
            }
            return viewName;
        }
        public static string GetClassViewName(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return string.Empty;
            int classid = Convert.ToInt32(obj);
            return GetClassViewName(classid);
        }
        #endregion

        #region GetCurrent
        /// <summary>
        /// 返回当前会话的会员信息。
        /// </summary>
        /// <returns>会员信息。</returns>
        public static int GetCurrent()
        {
            try
            {
                //return 1175;
                object sessionId = HttpContext.Current.Session[USER_LOGIN_SESSIONID];
                if (sessionId == null)
                    return 0;

                SqlParameter[] prams = new SqlParameter[]{
                      DataBase.MakeInParam("@sessionId", SqlDbType.VarChar, 100, sessionId) 
            };
                object result = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_users_getIdBySession", prams);
                if (result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
                return 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public static int GetUserIdBySession(string _sessionId)
        {
            try
            {
                SqlParameter[] prams = new SqlParameter[]{
                      DataBase.MakeInParam("@sessionId", SqlDbType.VarChar, 100, _sessionId) 
            };
                object result = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_users_getIdBySession", prams);
                if (result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
                return 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }
        #endregion

        

        #region ReaderBind
        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public static UserInfo ReaderBind(IDataReader dataReader)
        {
            UserInfo model = new UserInfo();
            object ojb;
            ojb = dataReader["id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ID = (int)ojb;
            }
            model.UserName = dataReader["userName"].ToString();
            model.Password = dataReader["password"].ToString();
            ojb = dataReader["CPSDrate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CPSDrate = (int)ojb;
            }
            ojb = dataReader["CVSNrate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CVSNrate = (int)ojb;
            }
            model.Email = dataReader["email"].ToString();
            model.QQ = dataReader["qq"].ToString();
            model.Tel = dataReader["tel"].ToString();
            model.IdCard = dataReader["idCard"].ToString();
            model.Account = dataReader["account"].ToString();
            model.PayeeName = dataReader["payeeName"].ToString();
            model.PayeeBank = dataReader["payeeBank"].ToString();
            model.BankProvince = dataReader["bankProvince"].ToString();
            model.BankCity = dataReader["bankCity"].ToString();
            model.BankAddress = dataReader["bankAddress"].ToString();
            ojb = dataReader["status"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Status = (int)ojb;
            }
            ojb = dataReader["regTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.RegTime = (DateTime)ojb;
            }
            ojb = dataReader["balance"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Balance = (decimal)ojb;
            }
            ojb = dataReader["payment"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Payment = (decimal)ojb;
            }
            ojb = dataReader["unpayment"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Unpayment = (decimal)ojb;
            }
            ojb = dataReader["agentId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AgentId = (int)ojb;
            }
            model.SiteName = dataReader["siteName"].ToString();
            model.SiteUrl = dataReader["siteUrl"].ToString();
            ojb = dataReader["userType"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.UserType = (UserTypeEnum)ojb;
            }
            ojb = dataReader["userLevel"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.UserLevel = (int)ojb;
            }
            ojb = dataReader["Integral"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Integral = (int)ojb;
            }
            ojb = dataReader["maxdaytocashTimes"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.MaxDayToCashTimes = (int)ojb;
            }
            ojb = dataReader["apiaccount"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.APIAccount = (long)ojb;
            }
            model.APIKey = dataReader["apikey"].ToString();
            model.LastLoginIp = dataReader["lastLoginIp"].ToString();
            ojb = dataReader["lastLoginTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.LastLoginTime = (DateTime)ojb;
            }
            model.smsNotifyUrl = dataReader["smsNotifyUrl"].ToString();
            model.question = dataReader["question"].ToString();
            model.answer = dataReader["answer"].ToString();
            ojb = dataReader["manageId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.manageId = (int)ojb;
            }
            return model;
        }
        #endregion

        #region 查询有关
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static List<int> GetUsers(string where)
        {
            List<int> users = new List<int>();

            if (string.IsNullOrEmpty(where))
                where = "1=1";
            string sql = "select id from dbo.userbase where " + where;

            SqlDataReader sdr = DataBase.ExecuteReader(CommandType.Text, sql);
            while (sdr.Read())
            {
                users.Add(sdr.GetInt32(0));
            }

            return users;
        }
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
                string userSearchWhere = BuilderWhere(searchParams, paramList);

                string sql = SqlHelper.GetCountSQL(tables, userSearchWhere, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL(FIELD_USER, tables, userSearchWhere, orderby, key, pageSize, page, false);
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
                        case "id":
                            builder.Append(" AND [id] = @id");
                            parameter = new SqlParameter("@id", SqlDbType.Int);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "username":
                            builder.Append(" AND [userName] like @UserName");
                            parameter = new SqlParameter("@UserName", SqlDbType.VarChar, 20);
                            parameter.Value = "%" + SqlHelper.CleanString((string)iparam.ParamValue, 20) + "%";
                            paramList.Add(parameter);
                            break;
                        case "qq":
                            builder.Append(" AND [qq] like @qq");
                            parameter = new SqlParameter("@qq", SqlDbType.VarChar, 50);
                            parameter.Value = "%" + SqlHelper.CleanString((string)iparam.ParamValue, 50) + "%";
                            paramList.Add(parameter);
                            break;
                        case "tel":
                            builder.Append(" AND [Tel] like @tel");
                            parameter = new SqlParameter("@tel", SqlDbType.VarChar, 50);
                            parameter.Value = "%" + SqlHelper.CleanString((string)iparam.ParamValue, 50) + "%";
                            paramList.Add(parameter);
                            break;
                        case "email":
                            builder.Append(" AND [email] like @email");
                            parameter = new SqlParameter("@email", SqlDbType.VarChar, 50);
                            parameter.Value = "%" + SqlHelper.CleanString((string)iparam.ParamValue, 50) + "%";
                            paramList.Add(parameter);
                            break;
                        case "full_name":
                            builder.Append(" AND [full_name] like @full_name");
                            parameter = new SqlParameter("@full_name", SqlDbType.VarChar, 50);
                            parameter.Value = "%" + SqlHelper.CleanString((string)iparam.ParamValue, 50) + "%";
                            paramList.Add(parameter);
                            break;
                        case "status":
                            builder.Append(" AND [status] = @status");
                            parameter = new SqlParameter("@status", SqlDbType.TinyInt);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "usertype":
                            builder.Append(" AND [userType] = @userType");
                            parameter = new SqlParameter("@userType", SqlDbType.TinyInt);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "proid":
                            builder.Append(" AND Exists(select 0 from PromotionUser where PromotionUser.PID = @proid and PromotionUser.RegId=v_users.id)");
                            parameter = new SqlParameter("@proid", SqlDbType.Int);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "manageid":
                            builder.Append(" AND [manageId] = @manageId");
                            parameter = new SqlParameter("@manageId", SqlDbType.Int);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "balance":
                            builder.AppendFormat(" AND [balance] {0} @balance",iparam.CmpOperator);
                            parameter = new SqlParameter("@balance", SqlDbType.Decimal);
                            parameter.Value = iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "enableamt":
                            builder.AppendFormat(" AND [enableAmt] {0} @enableAmt", iparam.CmpOperator);
                            parameter = new SqlParameter("@enableAmt", SqlDbType.Decimal);
                            parameter.Value = iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                    }
                }
            }
            return builder.ToString();
        }

        #region 总账上余额
        /// <summary>
        /// 总账上余额
        /// </summary>
        public decimal TotalBalance
        {
            get
            {
                try
                {
                    return Convert.ToDecimal(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_users_gettotalbalance"));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                    return decimal.Zero;
                }
            }
        }
        #endregion

        #region 总支付的金额
        /// <summary>
        /// 
        /// </summary>
        public decimal TotalPayment
        {
            get
            {
                try
                {
                    return Convert.ToDecimal(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_users_gettotalpayment"));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                    return decimal.Zero;
                }
            }
        }
        #endregion

        public static bool DelUpdateLog(int id)
        {
            try
            {
                bool result = false;

                SqlParameter[] prams = new SqlParameter[] { 
                DataBase.MakeInParam("@id", SqlDbType.Int,4, id)            };

                string sql = "delete from usersupdate where id=@id";
                result = DataBase.ExecuteNonQuery(CommandType.Text, sql, prams) > 0;
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }
        /// <summary>
        /// 根据搜索条件返回指定分页的商户信息。
        /// </summary>
        /// <param name="searchParams">搜索条件数组。</param>
        /// <param name="pageSize">分页大小。</param>
        /// <param name="page">页码。</param>
        /// <param name="orderby">排序方式。</param>
        /// <returns>分页数据。</returns>
        public static DataSet UpdateLogPageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet ds = new DataSet();
            try
            {
                string tables = "usersupdate";
                string key = "[id]";
                string fields = @"id,
userid,
field,
oldValue,
newvalue,
Addtime,
editor,
oIp";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "Addtime desc";
                }

                List<SqlParameter> paramList = new List<SqlParameter>();
                string where = BuilderUpdateLogWhere(searchParams, paramList);

                string sql = SqlHelper.GetCountSQL(tables, where, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL(fields, tables, where, orderby, key, pageSize, page, false);
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
        private static string BuilderUpdateLogWhere(List<SearchParam> param, List<SqlParameter> paramList)
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
                        case "id":
                            builder.Append(" AND [id] = @id");
                            parameter = new SqlParameter("@id", SqlDbType.Int);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;                       
                        case "userid":
                            builder.Append(" AND [userid] = @userid");
                            parameter = new SqlParameter("@userid", SqlDbType.Int);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "usertype":
                            builder.Append(" AND [userType] = @userType");
                            parameter = new SqlParameter("@userType", SqlDbType.Int);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;                       
                    }
                }
            }
            return builder.ToString();
        }
        #endregion

        public static DataTable getAgentList()
        {
            try
            {
                string sqlText = "select id,userName from userbase with(nolock) where userType = 4";
                return DataBase.ExecuteDataset(CommandType.Text, sqlText).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        //#region GetUsersKey
        ///// <summary>
        ///// 获取通信密钥
        ///// </summary>
        ///// <returns></returns>
        //public static UserInfo GetUsersKey(int uid)
        //{
        //    UserInfo user = new UserInfo();
            
        //    string cacheKey = string.Format(USERKEY_CACHE_KEY, uid);
        //    user = viviapi.Cache.WebCache.GetCacheService().RetrieveObject(cacheKey) as UserInfo;

        //    if (user == null)
        //    {
        //        user = new UserInfo();

        //        SqlParameter[] prams = new SqlParameter[] { DataBase.MakeInParam("@userId", SqlDbType.Int, 4, uid) };
        //        SqlDataReader dr = DataBase.ExecuteReader(CommandType.StoredProcedure, "proc_userskey_get", prams);
        //        if (dr.Read())
        //        {
        //            user.ID = uid;
        //            user.APIKey = dr["APIkey"].ToString();
        //            user.Status = Convert.ToInt32(dr["status"]);
        //            user.UserLevel = (UserLevelEnum)Convert.ToInt32(dr["userLevel"]);
        //        }
        //        dr.Dispose();

        //        if (user != null)
        //        {
        //            IDictionary<string, object> sqldepparms = new Dictionary<string, object>();
        //            sqldepparms.Add("userId", uid);
        //            SqlDependency sqlDep = DataBase.AddSqlDependency(cacheKey, SQL_TABLE_KEY, FIELD_USERKEYS, "[userId]=@userId", sqldepparms);
        //            viviapi.Cache.WebCache.GetCacheService().AddObject(cacheKey, user);
        //        }
        //    }
        //    return user;
        //}
        //#endregion

        #region CheckUserOrderId
        /// <summary>
        /// 获取通信密钥
        /// </summary>
        /// <returns></returns>
        public static bool CheckUserOrderId(int userid,string OrderId)
        {
            bool pass = false;
                        
            SqlParameter[] prams = new SqlParameter[] { 
                DataBase.MakeInParam("@orderNo", SqlDbType.VarChar, 30, OrderId),
                DataBase.MakeInParam("@userid", SqlDbType.Int, 4, userid) 
            };
            pass = Convert.ToBoolean(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_usersorderid_check", prams));
         
            return pass;
        }
        #endregion

        internal static void ClearCache(int userId)
        {
            string cahcekey = string.Format(USER_CACHE_KEY, userId);
            viviapi.Cache.WebCache.GetCacheService().RemoveObject(cahcekey);
        }
    }
}

