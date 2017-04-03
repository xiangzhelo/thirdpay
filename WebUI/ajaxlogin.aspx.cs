using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviLib.Web;
using viviapi.BLL.Sys;
using viviapi.WebComponents;
using viviLib.Security;

namespace viviAPI.WebUI2015
{
    public partial class ajaxlogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string message = "";
            string userName = WebBase.GetFormString("username", "");
            string userPwd = WebBase.GetFormString("password", "");

            string code = WebBase.GetFormString("login_yzcode", "");

            if (string.IsNullOrEmpty(userName))
            {
                message = ("请输入用户名!");
            }
            else if (string.IsNullOrEmpty(userPwd))
            {
                message = ("请输入密码!");
            }


            if (string.IsNullOrEmpty(message))
            {
                if (viviLib.Text.PageValidate.IsEmail(userName))
                {
                    if (!RegisterSettings.AllowUserloginByEmail)
                    {
                        message = ("平台不允许通过邮箱登录!");
                    }
                }
                else if (viviLib.Text.PageValidate.IsMobile(userName))
                {
                    if (!RegisterSettings.AllowUserloginByPhone)
                    {
                        message = ("平台不允许通过手机号码登录!");
                    }
                }
            }

            if (string.IsNullOrEmpty(message))
            {

                string lastLoginIp = ServerVariables.TrueIP;
                string lastLoginAddress = WebUtility.GetIPAddress(lastLoginIp);

                message = viviapi.BLL.User.Login.SignIn(0, 0, userName, Cryptography.MD5(userPwd), lastLoginIp, lastLoginAddress);

                if (message == "success")
                {

                    HttpCookie hc = new HttpCookie("yklm_user");
                    DateTime dt = DateTime.Now;
                    TimeSpan ts = new TimeSpan(90, 0, 0, 0, 0);//过期时间为1分钟
                    hc.Expires = dt.Add(ts);//设置过期时间

                    hc.Values.Add("username", userName);
                    hc.Values.Add("userpass", Cryptography.MD5(userPwd));
                    HttpContext.Current.Response.AppendCookie(hc);
                    string response ="{\"code\":200,\"error_num\":0}";
                    Response.Write(response);
                }
                else
                {
                    string response = "{\"code\":0,\"data\":{\"error_messages\":{\"result\":\"error2\"}},\"error_num\":0}";
                    Response.Write(response);
                }
            }
            else
            Response.Write("{\"code\":0,\"data\":{\"error_messages\":{\"result\":\"error\"}},\"error_num\":0}");
        }
    }
}