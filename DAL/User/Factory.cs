using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using DBAccess;
using viviapi.Model.User;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.DAL.User
{
    /// <summary>
    ///     商户操作类
    /// </summary>
    public class Factory
    {
        internal const string USER_CONTEXT_KEY = "{FD7BE212-8537-427f-9EF6-1D1AABCA8EA3}";

        internal const string SQL_BASE_TABLE = "userbase";

        internal const string SQL_BASE_TABLE_FIELD =
            "[id],[pwd2],[full_name],[userName],[password],[CPSDrate],[CVSNrate],[email],[qq],[tel],[idCard],[settles],[status],[regTime],[company],[linkMan],[agentId],[siteName],[siteUrl],[userType],[userLevel],[maxdaytocashTimes],[apiaccount],[apikey],[updatetime],[DESC],isRealNamePass,isEmailPass,isPhonePass,[classid],[isdebug]";

        internal const string SQL_PAYBANK_TABLE = "userspaybank";

        internal const string SQL_PAYBANK_TABLE_FIELD =
            "[userid],[pmode],[account],[payeeName],[payeeBank],[bankProvince],[bankCity],[bankAddress],[status]";


        internal const string SQL_TABLE = "V_Users";
        //money,pay,nopay,Integral
        internal const string SQL_TABLE_FIELD =
            "id,userName,password,CPSDrate,CVSNrate,email,qq,tel,idCard,pmode,settles,account,payeeName,payeeBank,bankProvince,bankCity,bankAddress,status,regTime,company,linkMan,agentId,siteName,siteUrl,userType,userLevel,maxdaytocashTimes,apiaccount,apikey,lastLoginIp,lastLoginTime,sessionId,manageId,isRealNamePass,full_name,classid,isdebug";

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
        /// </summary>
        /// <param name="agentid"></param>
        /// <returns></returns>
        public bool ChkAgent(int agentid)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@agentid", SqlDbType.Int, 4)
            };

            parameters[0].Value = agentid;

            return
                Convert.ToBoolean(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_user_chkagent", parameters));
        }

        #endregion

        #region Add

        /// <summary>
        ///     添加用户
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        public int Add(UserInfo userinfo)
        {
            SqlParameter uidoutparam = DataBase.MakeOutParam("@id", SqlDbType.Int, 4);
            SqlParameter[] prams =
            {
                uidoutparam
                , DataBase.MakeInParam("@userName", SqlDbType.VarChar, 50, userinfo.UserName)
                , DataBase.MakeInParam("@password", SqlDbType.VarChar, 100, userinfo.Password)
                , DataBase.MakeInParam("@cpsdrate", SqlDbType.Int, 4, userinfo.CPSDrate)
                , DataBase.MakeInParam("@cvsnrate", SqlDbType.Int, 4, userinfo.CVSNrate)
                , DataBase.MakeInParam("@email", SqlDbType.VarChar, 50, userinfo.Email)
                , DataBase.MakeInParam("@qq", SqlDbType.VarChar, 50, userinfo.QQ)
                , DataBase.MakeInParam("@tel", SqlDbType.VarChar, 50, userinfo.Tel)
                , DataBase.MakeInParam("@idCard", SqlDbType.VarChar, 50, userinfo.IdCard)
                , DataBase.MakeInParam("@account", SqlDbType.VarChar, 50, userinfo.Account)
                , DataBase.MakeInParam("@payeeName", SqlDbType.VarChar, 50, userinfo.PayeeName)
                , DataBase.MakeInParam("@payeeBank", SqlDbType.VarChar, 50, userinfo.PayeeBank)
                , DataBase.MakeInParam("@bankProvince", SqlDbType.VarChar, 50, userinfo.BankProvince)
                , DataBase.MakeInParam("@bankCity", SqlDbType.VarChar, 50, userinfo.BankCity)
                , DataBase.MakeInParam("@bankAddress", SqlDbType.VarChar, 50, userinfo.BankAddress)
                , DataBase.MakeInParam("@status", SqlDbType.TinyInt, 1, userinfo.Status)
                , DataBase.MakeInParam("@lastloginip", SqlDbType.VarChar, 50, userinfo.LastLoginIp)
                , DataBase.MakeInParam("@lastlogintime", SqlDbType.DateTime, 8, userinfo.LastLoginTime)
                , DataBase.MakeInParam("@regtime", SqlDbType.DateTime, 8, userinfo.RegTime)
                , DataBase.MakeInParam("@agentId", SqlDbType.Int, 4, userinfo.AgentId)
                , DataBase.MakeInParam("@siteName", SqlDbType.VarChar, 50, userinfo.SiteName)
                , DataBase.MakeInParam("@siteUrl", SqlDbType.VarChar, 100, userinfo.SiteUrl)
                , DataBase.MakeInParam("@userType", SqlDbType.Int, 4, (int) userinfo.UserType)
                , DataBase.MakeInParam("@userLevel", SqlDbType.Int, 4, (int) userinfo.UserLevel)
                , DataBase.MakeInParam("@maxdaytocashTimes", SqlDbType.Int, 4, userinfo.MaxDayToCashTimes)
                , DataBase.MakeInParam("@apiaccount", SqlDbType.BigInt, 8, userinfo.APIAccount)
                , DataBase.MakeInParam("@apikey", SqlDbType.VarChar, 400, userinfo.APIKey)
                , DataBase.MakeInParam("@pmode", SqlDbType.TinyInt, 1, userinfo.PMode)
                , DataBase.MakeInParam("@settles", SqlDbType.TinyInt, 1, userinfo.Settles)
                , DataBase.MakeInParam("@DESC", SqlDbType.VarChar, 4000, userinfo.Desc)
                , DataBase.MakeInParam("@manageId", SqlDbType.Int, 4, userinfo.manageId)
                , DataBase.MakeInParam("@question", SqlDbType.NVarChar, 150, userinfo.question)
                , DataBase.MakeInParam("@answer", SqlDbType.NVarChar, 100, userinfo.answer)
                , DataBase.MakeInParam("@full_name", SqlDbType.NVarChar, 100, userinfo.full_name)
                , DataBase.MakeInParam("@classid", SqlDbType.TinyInt, 1, userinfo.classid)
                , DataBase.MakeInParam("@pwd2", SqlDbType.NVarChar, 50, userinfo.Password2)
                , DataBase.MakeInParam("@linkman", SqlDbType.NVarChar, 50, userinfo.LinkMan)
                , DataBase.MakeInParam("@isdebug", SqlDbType.TinyInt, 1, userinfo.isdebug)
                , DataBase.MakeInParam("@idCardtype", SqlDbType.TinyInt, 1, userinfo.IdCardType)
                , DataBase.MakeInParam("@msn", SqlDbType.VarChar, 30, userinfo.msn)
                , DataBase.MakeInParam("@fax", SqlDbType.VarChar, 20, userinfo.fax)
                , DataBase.MakeInParam("@province", SqlDbType.VarChar, 20, userinfo.province)
                , DataBase.MakeInParam("@city", SqlDbType.VarChar, 20, userinfo.city)
                , DataBase.MakeInParam("@zip", SqlDbType.VarChar, 8, userinfo.zip)
                , DataBase.MakeInParam("@field1", SqlDbType.NVarChar, 50, userinfo.field1)
                , DataBase.MakeInParam("@isagentDistribution", SqlDbType.TinyInt, 1, userinfo.isagentDistribution)
                , DataBase.MakeInParam("@cardversion", SqlDbType.TinyInt, 1, userinfo.cardversion)
                , DataBase.MakeInParam("@parter", SqlDbType.Int, 1, userinfo.parter)
                , DataBase.MakeInParam("@openid", SqlDbType.VarChar, 100, userinfo.openid)
            };
            if (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_users_add", prams) > 0)
            {
                userinfo.ID = (int)uidoutparam.Value;
                return userinfo.ID;
            }
            return 0;
        }

        #endregion

        #region Update

        /// <summary>
        ///     更新用户信息
        /// </summary>
        /// <param name="userinfo"></param>
        /// <param name="changeList"></param>
        /// <returns></returns>
        public bool Update1(UserInfo userinfo)
        {
            SqlParameter[] prams =
            {
                DataBase.MakeInParam("@id", SqlDbType.Int, 4, userinfo.ID)
                , DataBase.MakeInParam("@userName", SqlDbType.VarChar, 50, userinfo.UserName)
                , DataBase.MakeInParam("@password", SqlDbType.VarChar, 100, userinfo.Password)
                , DataBase.MakeInParam("@cpsdrate", SqlDbType.Int, 4, userinfo.CPSDrate)
                , DataBase.MakeInParam("@cvsnrate", SqlDbType.Int, 4, userinfo.CVSNrate)
                , DataBase.MakeInParam("@email", SqlDbType.VarChar, 50, userinfo.Email)
                , DataBase.MakeInParam("@qq", SqlDbType.VarChar, 50, userinfo.QQ)
                , DataBase.MakeInParam("@tel", SqlDbType.VarChar, 50, userinfo.Tel)
                , DataBase.MakeInParam("@idCard", SqlDbType.VarChar, 50, userinfo.IdCard)
                , DataBase.MakeInParam("@account", SqlDbType.VarChar, 50, userinfo.Account)
                , DataBase.MakeInParam("@payeeName", SqlDbType.VarChar, 50, userinfo.PayeeName)
                , DataBase.MakeInParam("@payeeBank", SqlDbType.VarChar, 50, userinfo.PayeeBank)
                , DataBase.MakeInParam("@bankProvince", SqlDbType.VarChar, 50, userinfo.BankProvince)
                , DataBase.MakeInParam("@bankCity", SqlDbType.VarChar, 50, userinfo.BankCity)
                , DataBase.MakeInParam("@bankAddress", SqlDbType.VarChar, 50, userinfo.BankAddress)
                , DataBase.MakeInParam("@status", SqlDbType.TinyInt, 1, userinfo.Status)
                , DataBase.MakeInParam("@agentId", SqlDbType.Int, 4, userinfo.AgentId)
                , DataBase.MakeInParam("@siteName", SqlDbType.VarChar, 50, userinfo.SiteName)
                , DataBase.MakeInParam("@siteUrl", SqlDbType.VarChar, 100, userinfo.SiteUrl)
                , DataBase.MakeInParam("@userType", SqlDbType.Int, 4, (int) userinfo.UserType)
                , DataBase.MakeInParam("@userLevel", SqlDbType.Int, 4, (int) userinfo.UserLevel)
                , DataBase.MakeInParam("@maxdaytocashTimes", SqlDbType.Int, 4, userinfo.MaxDayToCashTimes)
                , DataBase.MakeInParam("@apiaccount", SqlDbType.BigInt, 8, userinfo.APIAccount)
                , DataBase.MakeInParam("@apikey", SqlDbType.VarChar, 50, userinfo.APIKey)
                , DataBase.MakeInParam("@DESC", SqlDbType.VarChar, 4000, userinfo.Desc)
                , DataBase.MakeInParam("@pmode", SqlDbType.Int, 4, userinfo.PMode)
                , DataBase.MakeInParam("@updatetime", SqlDbType.DateTime, 8, DateTime.Now)
                , DataBase.MakeInParam("@manageId", SqlDbType.Int, 4, userinfo.manageId)
                , DataBase.MakeInParam("@isRealNamePass", SqlDbType.TinyInt, 1, userinfo.IsRealNamePass)
                , DataBase.MakeInParam("@isEmailPass", SqlDbType.TinyInt, 1, userinfo.IsEmailPass)
                , DataBase.MakeInParam("@isPhonePass", SqlDbType.TinyInt, 1, userinfo.IsPhonePass)
                , DataBase.MakeInParam("@smsNotifyUrl", SqlDbType.NVarChar, 255, userinfo.smsNotifyUrl)
                , DataBase.MakeInParam("@full_name", SqlDbType.NVarChar, 50, userinfo.full_name)
                , DataBase.MakeInParam("@male", SqlDbType.NVarChar, 4, userinfo.male)           
                //, DataBase.MakeInParam("@birthday", SqlDbType.DateTime,8, birtdate)
                , DataBase.MakeInParam("@addtress", SqlDbType.NVarChar, 30, userinfo.addtress)
                , DataBase.MakeInParam("@question", SqlDbType.NVarChar, 150, userinfo.question)
                , DataBase.MakeInParam("@answer", SqlDbType.NVarChar, 100, userinfo.answer)
                , DataBase.MakeInParam("@pwd2", SqlDbType.NVarChar, 50, userinfo.Password2)
                , DataBase.MakeInParam("@linkman", SqlDbType.NVarChar, 50, userinfo.LinkMan)
                , DataBase.MakeInParam("@classid", SqlDbType.TinyInt, 1, userinfo.classid)
                , DataBase.MakeInParam("@settles", SqlDbType.TinyInt, 1, userinfo.Settles)
                , DataBase.MakeInParam("@isdebug", SqlDbType.TinyInt, 1, userinfo.isdebug)
                , DataBase.MakeInParam("@idCardtype", SqlDbType.TinyInt, 1, userinfo.IdCardType)
                , DataBase.MakeInParam("@msn", SqlDbType.VarChar, 30, userinfo.msn)
                , DataBase.MakeInParam("@fax", SqlDbType.VarChar, 20, userinfo.fax)
                , DataBase.MakeInParam("@province", SqlDbType.VarChar, 20, userinfo.province)
                , DataBase.MakeInParam("@city", SqlDbType.VarChar, 20, userinfo.city)
                , DataBase.MakeInParam("@zip", SqlDbType.VarChar, 8, userinfo.zip)
                , DataBase.MakeInParam("@field1", SqlDbType.NVarChar, 50, userinfo.field1)
                , DataBase.MakeInParam("@accoutType", SqlDbType.TinyInt, 1, userinfo.accoutType)
                , DataBase.MakeInParam("@BankCode", SqlDbType.VarChar, 50, userinfo.BankCode)
                , DataBase.MakeInParam("@provinceCode", SqlDbType.VarChar, 50, userinfo.provinceCode)
                , DataBase.MakeInParam("@cityCode", SqlDbType.VarChar, 50, userinfo.cityCode)
                , DataBase.MakeInParam("@isagentDistribution", SqlDbType.TinyInt, 1, userinfo.isagentDistribution)
                , DataBase.MakeInParam("@cardversion", SqlDbType.TinyInt, 1, userinfo.cardversion)
            };

            return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_users_Update", prams) > 0;
        }

        #endregion

        #region Update

        /// <summary>
        ///     更新用户信息
        /// </summary>
        /// <param name="userinfo"></param>
        /// <param name="changeList"></param>
        /// <returns></returns>
        public bool Update(UserInfo userinfo, List<UsersUpdateLog> changeList)
        {
            SqlParameter[] prams =
            {
                DataBase.MakeInParam("@id", SqlDbType.Int, 4, userinfo.ID)
                , DataBase.MakeInParam("@userName", SqlDbType.VarChar, 50, userinfo.UserName)
                , DataBase.MakeInParam("@password", SqlDbType.VarChar, 100, userinfo.Password)
                , DataBase.MakeInParam("@cpsdrate", SqlDbType.Int, 4, userinfo.CPSDrate)
                , DataBase.MakeInParam("@cvsnrate", SqlDbType.Int, 4, userinfo.CVSNrate)
                , DataBase.MakeInParam("@email", SqlDbType.VarChar, 50, userinfo.Email)
                , DataBase.MakeInParam("@qq", SqlDbType.VarChar, 50, userinfo.QQ)
                , DataBase.MakeInParam("@tel", SqlDbType.VarChar, 50, userinfo.Tel)
                , DataBase.MakeInParam("@idCard", SqlDbType.VarChar, 50, userinfo.IdCard)
                , DataBase.MakeInParam("@account", SqlDbType.VarChar, 50, userinfo.Account)
                , DataBase.MakeInParam("@payeeName", SqlDbType.VarChar, 50, userinfo.PayeeName)
                , DataBase.MakeInParam("@payeeBank", SqlDbType.VarChar, 50, userinfo.PayeeBank)
                , DataBase.MakeInParam("@bankProvince", SqlDbType.VarChar, 50, userinfo.BankProvince)
                , DataBase.MakeInParam("@bankCity", SqlDbType.VarChar, 50, userinfo.BankCity)
                , DataBase.MakeInParam("@bankAddress", SqlDbType.VarChar, 50, userinfo.BankAddress)
                , DataBase.MakeInParam("@status", SqlDbType.TinyInt, 1, userinfo.Status)
                , DataBase.MakeInParam("@agentId", SqlDbType.Int, 4, userinfo.AgentId)
                , DataBase.MakeInParam("@siteName", SqlDbType.VarChar, 50, userinfo.SiteName)
                , DataBase.MakeInParam("@siteUrl", SqlDbType.VarChar, 100, userinfo.SiteUrl)
                , DataBase.MakeInParam("@userType", SqlDbType.Int, 4, (int) userinfo.UserType)
                , DataBase.MakeInParam("@userLevel", SqlDbType.Int, 4, (int) userinfo.UserLevel)
                , DataBase.MakeInParam("@maxdaytocashTimes", SqlDbType.Int, 4, userinfo.MaxDayToCashTimes)
                , DataBase.MakeInParam("@apiaccount", SqlDbType.BigInt, 8, userinfo.APIAccount)
                , DataBase.MakeInParam("@apikey", SqlDbType.VarChar, 400, userinfo.APIKey)
                , DataBase.MakeInParam("@DESC", SqlDbType.VarChar, 4000, userinfo.Desc)
                , DataBase.MakeInParam("@pmode", SqlDbType.Int, 4, userinfo.PMode)
                , DataBase.MakeInParam("@updatetime", SqlDbType.DateTime, 8, DateTime.Now)
                , DataBase.MakeInParam("@manageId", SqlDbType.Int, 4, userinfo.manageId)
                , DataBase.MakeInParam("@isRealNamePass", SqlDbType.TinyInt, 1, userinfo.IsRealNamePass)
                , DataBase.MakeInParam("@isEmailPass", SqlDbType.TinyInt, 1, userinfo.IsEmailPass)
                , DataBase.MakeInParam("@isPhonePass", SqlDbType.TinyInt, 1, userinfo.IsPhonePass)
                , DataBase.MakeInParam("@smsNotifyUrl", SqlDbType.NVarChar, 255, userinfo.smsNotifyUrl)
                , DataBase.MakeInParam("@full_name", SqlDbType.NVarChar, 50, userinfo.full_name)
                , DataBase.MakeInParam("@male", SqlDbType.NVarChar, 4, userinfo.male)           
                //, DataBase.MakeInParam("@birthday", SqlDbType.DateTime,8, birtdate)
                , DataBase.MakeInParam("@addtress", SqlDbType.NVarChar, 30, userinfo.addtress)
                , DataBase.MakeInParam("@question", SqlDbType.NVarChar, 150, userinfo.question)
                , DataBase.MakeInParam("@answer", SqlDbType.NVarChar, 100, userinfo.answer)
                , DataBase.MakeInParam("@pwd2", SqlDbType.NVarChar, 50, userinfo.Password2)
                , DataBase.MakeInParam("@linkman", SqlDbType.NVarChar, 50, userinfo.LinkMan)
                , DataBase.MakeInParam("@classid", SqlDbType.TinyInt, 1, userinfo.classid)
                , DataBase.MakeInParam("@settles", SqlDbType.TinyInt, 1, userinfo.Settles)
                , DataBase.MakeInParam("@isdebug", SqlDbType.TinyInt, 1, userinfo.isdebug)
                , DataBase.MakeInParam("@idCardtype", SqlDbType.TinyInt, 1, userinfo.IdCardType)
                , DataBase.MakeInParam("@msn", SqlDbType.VarChar, 30, userinfo.msn)
                , DataBase.MakeInParam("@fax", SqlDbType.VarChar, 20, userinfo.fax)
                , DataBase.MakeInParam("@province", SqlDbType.VarChar, 20, userinfo.province)
                , DataBase.MakeInParam("@city", SqlDbType.VarChar, 20, userinfo.city)
                , DataBase.MakeInParam("@zip", SqlDbType.VarChar, 8, userinfo.zip)
                , DataBase.MakeInParam("@field1", SqlDbType.NVarChar, 50, userinfo.field1)
                , DataBase.MakeInParam("@accoutType", SqlDbType.TinyInt, 1, userinfo.accoutType)
                , DataBase.MakeInParam("@BankCode", SqlDbType.VarChar, 50, userinfo.BankCode)
                , DataBase.MakeInParam("@provinceCode", SqlDbType.VarChar, 50, userinfo.provinceCode)
                , DataBase.MakeInParam("@cityCode", SqlDbType.VarChar, 50, userinfo.cityCode)
                , DataBase.MakeInParam("@isagentDistribution", SqlDbType.TinyInt, 1, userinfo.isagentDistribution)
                , DataBase.MakeInParam("@agentDistscheme", SqlDbType.Int, 4, userinfo.agentDistscheme)
                , DataBase.MakeInParam("@cardversion", SqlDbType.TinyInt, 1, userinfo.cardversion)
            };

            using (var conn = new SqlConnection(DataBase.ConnectionString))
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
                                SqlParameter[] parameters =
                                {
                                    new SqlParameter("@userid", SqlDbType.Int, 4)
                                    , new SqlParameter("@field", SqlDbType.VarChar, 20)
                                    , new SqlParameter("@oldValue", SqlDbType.VarChar, 100)
                                    , new SqlParameter("@newvalue", SqlDbType.VarChar, 100)
                                    , new SqlParameter("@Addtime", SqlDbType.DateTime)
                                    , new SqlParameter("@editor", SqlDbType.VarChar, 50)
                                    , new SqlParameter("@oIp", SqlDbType.VarChar, 50)
                                    , new SqlParameter("@desc", SqlDbType.VarChar, 4000)
                                };
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
                            return true;
                        }
                        trans.Rollback();
                        conn.Close();
                        return false;
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

        #region Update

        /// <summary>
        ///     更新用户信息
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="manageid"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool Update(int userid, int manageid, byte status)
        {
            string sqlText = "update [userbase] set manageId=@manageId,[status]=@status where id=@userid";

            SqlParameter[] prams =
            {
                DataBase.MakeInParam("@userid", SqlDbType.Int, 4, userid)
                , DataBase.MakeInParam("@manageId", SqlDbType.Int, 4, manageid)
                , DataBase.MakeInParam("@status", SqlDbType.TinyInt, 1, status)
            };

            return DataBase.ExecuteNonQuery(CommandType.Text, sqlText, prams) > 0;
        }

        #endregion

        #region Exists

        /// <summary>
        ///     是否已经存在用户账号
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool Exists(string username)
        {
            bool result = false;

            SqlParameter[] prams =
            {
                DataBase.MakeInParam("@userName", SqlDbType.NVarChar, 50, username)
            };
            object exists = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_users_Exists", prams);
            if (exists != null && exists != DBNull.Value)
                result = Convert.ToBoolean(exists);

            return result;
        }

        /// <summary>
        ///     999 代表不存在
        ///     0 存在未审核
        ///     1	--待审核
        ///     2	--正常
        ///     3	--锁定
        ///     4	--审核失败
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public int EmailExists(string email)
        {
            int result = 999;

            SqlParameter[] prams =
            {
                DataBase.MakeInParam("@email", SqlDbType.NVarChar, 50, email)
            };
            object exists = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_users_EmailExists", prams);
            if (exists != null && exists != DBNull.Value)
                result = Convert.ToInt32(exists);

            return result;
        }

        /// <summary>
        ///     是否已经存在用户账号
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool Exists(int userId)
        {
            bool result = false;

            var prams = new[]
            {
                DataBase.MakeInParam("@userId", SqlDbType.Int, 4, userId)
            };
            object exists = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_users_ExistsId", prams);
            if (exists != null && exists != DBNull.Value)
                result = Convert.ToBoolean(exists);

            return result;
        }

        #endregion

        #region Del

        /// <summary>
        ///     是否已经存在用户账号
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool Del(int userId)
        {
            bool result = false;

            SqlParameter[] prams =
            {
                DataBase.MakeInParam("@id", SqlDbType.Int, 4, userId)
            };

            result = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_users_del", prams) > 0;

            return result;
        }

        #endregion

        #region GetModel

        /// <summary>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUserApiKey(int userId)
        {
            SqlParameter[] prams =
            {
                DataBase.MakeInParam("@user_id", SqlDbType.Int, 4, userId)
            };
            object apiKey = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_users_getApiKey", prams);


            return apiKey.ToString();
        }

        #region GetModel

        /// <summary>
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public UserInfo GetModel(int uid)
        {
            SqlParameter[] parameters = { new SqlParameter("@id", SqlDbType.Int, 4) };
            parameters[0].Value = uid;

            var model = new UserInfo();
            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_users_get", parameters);
            model = GetModelFromDs(ds);

            return model;
        }

        #endregion

        #region GetModelByName

        /// <summary>
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public UserInfo GetModelByName(string userName)
        {
            SqlParameter[] parameters = { new SqlParameter("@userName", SqlDbType.VarChar, 20) };
            parameters[0].Value = userName;

            var model = new UserInfo();
            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_users_getbyname", parameters);
            model = GetModelFromDs(ds);

            return model;
        }

        #endregion

        #region GetPromSuperior

        /// <summary>
        ///     取用户的上级代理
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public UserInfo GetPromSuperior(int userId)
        {
            string sql = @"SELECT u.* FROM userbase u inner JOIN PromotionUser pu ON u.id = pu.PID
WHERE pu.RegId = @RegId";

            SqlParameter[] parameters = { new SqlParameter("@RegId", SqlDbType.Int, 4) };
            parameters[0].Value = userId;

            var model = new UserInfo();
            DataSet ds = DataBase.ExecuteDataset(CommandType.Text, sql, parameters);
            model = GetBaseModelFromDs(ds);

            return model;
        }

        public int GetPromID(int userid)
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
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        #endregion

        #region GetModelFromDs

        /// <summary>
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public UserInfo GetModelFromDs(DataSet ds)
        {
            var model = new UserInfo();

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
                model.BankCode = ds.Tables[0].Rows[0]["BankCode"].ToString();
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

                if (ds.Tables[0].Rows[0]["isagentDistribution"].ToString() != "")
                {
                    model.isagentDistribution = int.Parse(ds.Tables[0].Rows[0]["isagentDistribution"].ToString());
                }
                else
                {
                    model.isagentDistribution = 0;
                }

                if (ds.Tables[0].Rows[0]["agentDistscheme"].ToString() != "")
                {
                    model.agentDistscheme = int.Parse(ds.Tables[0].Rows[0]["agentDistscheme"].ToString());
                }
                else
                {
                    model.agentDistscheme = 0;
                }

                if (ds.Tables[0].Rows[0]["cardversion"].ToString() != "")
                {
                    model.cardversion = byte.Parse(ds.Tables[0].Rows[0]["cardversion"].ToString());
                }

                if (ds.Tables[0].Rows[0]["parter"].ToString() != "")
                {
                    model.parter = int.Parse(ds.Tables[0].Rows[0]["parter"].ToString());
                }
            }
            return model;
        }

        #endregion

        #region GetBaseModelFromDs

        /// <summary>
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static UserInfo GetBaseModelFromDs(DataSet ds)
        {
            var model = new UserInfo();

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

        #region 查询有关

        /// <summary>
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<int> GetUsers(string where)
        {
            var users = new List<int>();

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
        ///     根据搜索条件返回指定分页的商户信息。
        /// </summary>
        /// <param name="searchParams">搜索条件数组。</param>
        /// <param name="pageSize">分页大小。</param>
        /// <param name="page">页码。</param>
        /// <param name="orderby">排序方式。</param>
        /// <returns>分页数据。</returns>
        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            var ds = new DataSet();
            try
            {
                string tables = SQL_TABLE;
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "id desc";
                }

                var paramList = new List<SqlParameter>();
                string userSearchWhere = BuilderWhere(searchParams, paramList);

                string sql = SqlHelper.GetCountSQL(tables, userSearchWhere, string.Empty) + "\r\n" +
                             SqlHelper.GetPageSelectSQL(FIELD_USER, tables, userSearchWhere, orderby, key, pageSize,
                                 page, false);


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
        /// </summary>
        /// <param name="param"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        private string BuilderWhere(List<SearchParam> param, List<SqlParameter> paramList)
        {
            var builder = new StringBuilder(" 1 = 1");

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
                            break; //
                        case "usertype":
                            builder.Append(" AND [userType] = @userType");
                            parameter = new SqlParameter("@userType", SqlDbType.TinyInt);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "specialchannel":
                            var v = (string)iparam.ParamValue;
                            if (v == "1")
                            {
                                builder.Append(
                                    " AND exists(select 0 from channeltypeusers where isnull(suppid,0)>0 and userid=v_users.id)");
                            }
                            else
                            {
                                builder.Append(
                                    " AND not exists(select 0 from channeltypeusers where isnull(suppid,0)>0 and userid=v_users.id)");
                            }
                            break;
                        case "special":
                            builder.Append(" AND [special] = @special");
                            parameter = new SqlParameter("@special", SqlDbType.TinyInt);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "userlevel":
                            builder.Append(" AND [userlevel] = @userlevel");
                            parameter = new SqlParameter("@userlevel", SqlDbType.Int);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "proid":
                            builder.Append(
                                " AND Exists(select 0 from PromotionUser where PromotionUser.PID = @proid and PromotionUser.RegId=v_users.id)");
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
                            builder.AppendFormat(" AND [balance] {0} @balance", iparam.CmpOperator);
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

        public bool DelUpdateLog(int id)
        {
            try
            {
                bool result = false;

                SqlParameter[] prams =
                {
                    DataBase.MakeInParam("@id", SqlDbType.Int, 4, id)
                };

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
        ///     根据搜索条件返回指定分页的商户信息。
        /// </summary>
        /// <param name="searchParams">搜索条件数组。</param>
        /// <param name="pageSize">分页大小。</param>
        /// <param name="page">页码。</param>
        /// <param name="orderby">排序方式。</param>
        /// <returns>分页数据。</returns>
        public DataSet UpdateLogPageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            var ds = new DataSet();
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

                var paramList = new List<SqlParameter>();
                string where = BuilderUpdateLogWhere(searchParams, paramList);

                string sql = SqlHelper.GetCountSQL(tables, where, string.Empty) + "\r\n" +
                             SqlHelper.GetPageSelectSQL(fields, tables, where, orderby, key, pageSize, page, false);
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
        /// </summary>
        /// <param name="param"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        private string BuilderUpdateLogWhere(List<SearchParam> param, List<SqlParameter> paramList)
        {
            var builder = new StringBuilder(" 1 = 1");

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
                        case "stime":
                            builder.Append(" AND [Addtime] >= @stime");
                            parameter = new SqlParameter("@stime", SqlDbType.DateTime);
                            parameter.Value = iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "etime":
                            builder.Append(" AND [Addtime] <= @etime");
                            parameter = new SqlParameter("@etime", SqlDbType.DateTime);
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
        ///     总账上余额
        /// </summary>
        public decimal TotalBalance
        {
            get
            {
                try
                {
                    return
                        Convert.ToDecimal(DataBase.ExecuteScalar(CommandType.StoredProcedure,
                            "proc_users_gettotalbalance"));
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
        /// </summary>
        public decimal TotalPayment
        {
            get
            {
                try
                {
                    return
                        Convert.ToDecimal(DataBase.ExecuteScalar(CommandType.StoredProcedure,
                            "proc_users_gettotalpayment"));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                    return decimal.Zero;
                }
            }
        }

        #endregion

        #endregion

        public DataTable GetAgentList()
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
    }
}