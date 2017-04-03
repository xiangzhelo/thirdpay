using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using DBAccess;
using viviapi.BLL.Sys;
using viviapi.Model.User;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.User
{
    public class Login
    {
        static viviapi.DAL.User.Login dal = new viviapi.DAL.User.Login();

        public static string UserContextKey = "{FD7BE212-8537-427f-9EF6-1D1AABCA8EA3}";
        public static string UserLoginSessionid = "{10E6C4EE-54C1-4895-8CDE-202A5B3DD9E9}";
        public static string UserLoginClientSessionid = "{2A1FA22C-201B-471c-B668-2FCC1C4A121A}";

        protected static string EmailRegex = "^\\w+((-\\w+)|(\\.\\w+))*\\@[A-Za-z0-9]+((\\.|-)[A-Za-z0-9]+)*\\.[A-Za-z0-9]+$";

        public static string SignIn(byte isClient,byte isagentlogin, string username, string password, string loginip, string address)
        {
            string msg = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(username)
                    || string.IsNullOrEmpty(password))
                {
                    msg = "请输入账号密码";
                    return msg;
                }
                bool isEmail =viviLib.Text.PageValidate.IsEmail(username);
                bool isMobile =viviLib.Text.PageValidate.IsMobile(username);

                string sessionId = Guid.NewGuid().ToString("b");

                DataRow row = null;

                if (isEmail)
                {
                    row = dal.SignInByEmail(isClient, sessionId, username, password, loginip, address);
                }
                else if (isMobile)
                {
                    row = dal.SignInByMobile(isClient, sessionId, username, password, loginip, address);
                }
                else
                {
                    row = dal.SignIn(isClient,isagentlogin, sessionId, username, password, loginip, address);
                }
                if (row != null)
                {
                    byte result = Convert.ToByte(row["result"]);
                    if (result == 0)
                    {
                        msg = "success";
                        HttpContext.Current.Session[UserLoginSessionid] = sessionId;
                        if (isClient == 1)
                        {
                            HttpContext.Current.Session[UserLoginClientSessionid] = row["userId"];
                        }
                    }
                    else
                    {
                        msg = GetMsg(result);
                    }

                }
                else
                {
                    msg = "登录失败";
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


   


        public static string SignIn(int plant, string openid, string loginip, string address)
        {
            string msg = string.Empty;

            try
            {
                string sessionId = Guid.NewGuid().ToString("b");

                DataRow row = dal.SignInByPartner(plant, openid, sessionId, loginip, address);

                if (row != null)
                {
                    byte result = Convert.ToByte(row["result"]);
                    if (result == 0)
                    {
                        msg = "success";
                        HttpContext.Current.Session[UserLoginSessionid] = sessionId;

                    }
                    else
                    {
                        msg = GetMsg(result);
                    }

                }
                else
                {
                    msg = "登录失败";
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

        public static string GetMsg(byte result)
        {
            string msg = "";

            switch (result)
            {
                case 1:
                    msg = "不存在此账户";
                    break;
                case 2:
                    msg = "密码不正确";
                    break;
                case 3:
                    msg = RegisterSettings.LoginMsgForCheckfail;
                    break;
                case 4:
                    msg = RegisterSettings.LoginMsgForlock;
                    break;
                case 5:
                    msg = "未知错误";
                    break;
                case 6:
                    msg = "邮箱账号未认证，无法登录。请先进行认证操作。";
                    break;
                case 7:
                    msg = "不允许使用邮箱账号登录。";
                    break;
                case 8:
                    msg = "邮箱未激活，不能登录。";
                    break;
                case 17:
                    msg = "不允许用手机账号登录。";
                    break;
                case 18:
                    msg = "手机未通过认证，不能登录。";
                    break;
            }
            return msg;
        }

        /// <summary>
        /// 返回当前登录商户。
        /// </summary>
        public static UserInfo CurrentMember
        {
            get
            {
                if (HttpContext.Current == null) return null;

                if (HttpContext.Current.Items[UserContextKey] != null)
                    return HttpContext.Current.Items[UserContextKey] as UserInfo;

                int userId = GetCurrent();

                if (userId > 0)
                {
                    HttpContext.Current.Items[UserContextKey] = Factory.GetCacheModel(userId);
                }
                else
                {
                    return null;
                }

                return HttpContext.Current.Items[UserContextKey] as UserInfo;
            }
        }

        #region GetCurrent
        /// <summary>
        /// 返回当前会话的会员信息。
        /// </summary>
        /// <returns>会员信息。</returns>
        public static int GetCurrent()
        {
            //return 1081;

            try
            {
                int userId = 0;

                //用户登录
                object sessionId = HttpContext.Current.Session[UserLoginSessionid];
                if (sessionId != null)
                {
                    userId = GetUserIdBySession(sessionId.ToString());
                }
                else
                {
                    //客户端已登录
                    object obj = HttpContext.Current.Session[UserLoginClientSessionid];

                    if (obj != null)
                    {
                        userId = Convert.ToInt32(obj);
                    }
                    else
                    {
                        //管理员
                        if (IsManageLogin())
                        {
                            obj = HttpContext.Current.Session[Constant.ManageGOTOUserAdminKey];

                            if (obj != null)
                                userId = Convert.ToInt32(obj);
                        }
                    }
                }

                return userId;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public static bool IsManageLogin()
        {
            object userid = HttpContext.Current.Session[Sys.Constant.ManageGOTOUserAdminKey];

            return userid != null;
        }

        public static bool IsLogin
        {
            get
            {
                //return true;

                if (HttpContext.Current.Session[UserLoginSessionid] != null)
                    return true;

                if (HttpContext.Current.Session[Sys.Constant.ManageGOTOUserAdminKey] != null)
                    return true;

                if (HttpContext.Current.Session[UserLoginClientSessionid] != null)
                    return true;


                return false;
            }
        }

        public static int GetUserIdBySession(string sessionId)
        {
            try
            {
                return dal.GetUserIdBySession(sessionId);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public static int GetUserIdByToken(string token)
        {
            try
            {
                return dal.GetUserIdByToken(token);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }
        #endregion

        public static void SignOut()
        {
            HttpContext.Current.Items[UserContextKey] = null;
            HttpContext.Current.Session[UserLoginSessionid] = null;
        }
    }
}
