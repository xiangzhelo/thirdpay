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
    /// 商户操作类 
    /// </summary>
    public class Factory
    {
        static viviapi.DAL.User.Factory dal = new viviapi.DAL.User.Factory();



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

        public static string GenerateAPIKey()
        {
            return Guid.NewGuid().ToString("N").ToLower();
            //viviLib.Text.Strings.GetRnd(128, true, true, true, false, "")
        }

        #region chkAgent
        /// <summary>
        /// 
        /// </summary>
        /// <param name="agentid"></param>
        /// <returns></returns>
        public static bool ChkAgent(int agentid)
        {
            try
            {
                return dal.ChkAgent(agentid);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }
        #endregion

        #region Add
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        public static int Add(UserInfo userinfo)
        {
            try
            {
                return dal.Add(userinfo);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return 0;
            }
        }
        #endregion

        #region Update
        #region Update
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        public static bool Update1(UserInfo userinfo)
        {
            try
            {
                return dal.Update1(userinfo);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }

        }
        #endregion

        #region Update
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userinfo"></param>
        /// <param name="changeList"></param>
        /// <returns></returns>
        public static bool Update(UserInfo userinfo, List<UsersUpdateLog> changeList)
        {
            try
            {
                return dal.Update(userinfo, changeList);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }
        #endregion

        #region Update

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="manageid"></param>
        /// <param name="manageid"></param>
        /// <returns></returns>
        public static bool Update(int userid, int manageid, byte status)
        {

            try
            {
                return dal.Update(userid, manageid, status);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }

        }
        #endregion
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
                return dal.Exists(username);
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
        /// <param name="email"></param>
        /// <returns></returns>
        public static int EmailExists(string email)
        {
            try
            {
                return dal.EmailExists(email);
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
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool Exists(int userId)
        {
            try
            {
                return dal.Exists(userId);
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
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool Del(int userId)
        {
            try
            {
                return dal.Del(userId);
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
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string GetUserApiKey(int userId)
        {
            try
            {
                return dal.GetUserApiKey(userId);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return string.Empty;
            }
        }

        #region GetUserBaseInfo
        /// <summary>
        /// 获取基本信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static UserInfo GetCacheUserBaseInfo(int uid)
        {
            var model = new UserInfo();
            string cacheKey = string.Format(USER_CACHE_KEY, uid);
            model = (UserInfo)viviapi.Cache.WebCache.GetCacheService().RetrieveObject(cacheKey);

            if (model == null)
            {
                IDictionary<string, object> sqldepparms = new Dictionary<string, object>();
                sqldepparms.Add("id", uid);

                SqlDependency sqlDep = DataBase.AddSqlDependency(cacheKey, SQL_BASE_TABLE, SQL_BASE_TABLE_FIELD, "[id]=@id", sqldepparms);

                model = GetModel(uid);
                Cache.WebCache.GetCacheService().AddObject(cacheKey, model);
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
            var model = new UserInfo();

            string cacheKey = string.Format(USER_CACHE_KEY, uid);
            model = (UserInfo)viviapi.Cache.WebCache.GetCacheService().RetrieveObject(cacheKey);

            if (model == null)
            {
                IDictionary<string, object> sqldepparms = new Dictionary<string, object>();
                sqldepparms.Add("id", uid);
                SqlDependency sqlDep = DataBase.AddSqlDependency(cacheKey, SQL_BASE_TABLE, SQL_BASE_TABLE_FIELD, "[id]=@id", sqldepparms);
                SqlDependency sqlDep2 = DataBase.AddSqlDependency(cacheKey, SQL_PAYBANK_TABLE, SQL_PAYBANK_TABLE_FIELD, "[userid]=@id", sqldepparms);

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
            try
            {
                return dal.GetModel(uid);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
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
            try
            {
                return dal.GetModelByName(userName);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
        #endregion

        #region GetPromSuperior
        /// <summary>
        /// 取用户的上级代理
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static UserInfo GetPromSuperior(int userId)
        {
            try
            {
                return dal.GetPromSuperior(userId);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }

        public static int GetPromID(int userid)
        {
            try
            {
                return dal.GetPromID(userid);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
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

        #region 查询有关
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static List<int> GetUsers(string where)
        {
            try
            {
                return dal.GetUsers(where);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
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
        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            try
            {
                return dal.PageSearch(searchParams, pageSize, page, orderby);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
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
                    return dal.TotalBalance;
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
                    return dal.TotalPayment;
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
                return dal.DelUpdateLog(id);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
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
            try
            {
                return dal.UpdateLogPageSearch(searchParams, pageSize, page, orderby);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }
        #endregion

        public static DataTable GetAgentList()
        {
            try
            {
                return dal.GetAgentList();
            }
            catch
            {
                return null;
            }
        }



        internal static void ClearCache(int userId)
        {
            string cahcekey = string.Format(USER_CACHE_KEY, userId);
            viviapi.Cache.WebCache.GetCacheService().RemoveObject(cahcekey);
        }
    }
}

