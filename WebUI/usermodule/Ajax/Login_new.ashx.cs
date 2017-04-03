using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using viviapi.WebComponents;
using viviLib;
using viviLib.Security;
using viviapi.Model;
using viviapi.Model.User;
using viviapi.web;

namespace viviAPI.WebUI7uka.usermodule.Ajax
{
    public class Login_new : IHttpHandler, IRequiresSessionState
    {
        #region SignIn
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        protected string SignIn(HttpContext context, string userName, string userPwd, string code)
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(code))
            {
                msg = ("请输入验证码!");
            }
            else if (string.IsNullOrEmpty(userName))
            {
                msg = ("请输入商户名!");
            }
            else if (string.IsNullOrEmpty(userPwd))
            {
                msg = ("请输入商户密码!");
            }
            else if (context.Session["CCode"] == null)
            {
                msg = ("验证码失效!");
            }
            else if (context.Session["CCode"].ToString().ToUpper() != code.ToUpper())
            {
                msg = ("验证码不正确!");
            }
            else
            {
                UserInfo userInfo = new UserInfo();
                userInfo.UserName = userName;
                userInfo.Password = Cryptography.MD5(userPwd);
                userInfo.LastLoginIp = viviLib.Web.ServerVariables.TrueIP;
                userInfo.LastLoginTime = System.DateTime.Now;
                userInfo.LastLoginAddress = WebUtility.GetIPAddress(userInfo.LastLoginIp);
                userInfo.LastLoginRemark = WebUtility.GetIPAddressInfo(userInfo.LastLoginIp);
                //msg = viviapi.BLL.User.Login.SignIn(userInfo);
                if (userInfo.ID > 0)
                {
                    msg = "ok";
                }
            }
            return msg;
        }
        #endregion
        public void ProcessRequest(HttpContext context)
        {
            string acct = viviLib.Web.WebBase.GetFormString("username", string.Empty);
            string pwd = viviLib.Web.WebBase.GetFormString("password", string.Empty);
            string code = viviLib.Web.WebBase.GetFormString("imycode", string.Empty);

            string result = SignIn(context, acct, pwd, code);

            context.Response.ContentType = "text/plain";
            context.Response.Write(result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
