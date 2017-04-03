using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL.News;
using viviapi.BLL.Sys;
using viviapi.Model.News;
using viviapi.WebComponents;
using viviLib.ExceptionHandling;
using viviLib.Security;
using viviLib.Web;


namespace viviAPI.WebUI2015.longbao
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["m"] == "login")
            {
                string errMsg = SignIn();

                if (errMsg == "success")
                {
                    Response.Redirect("/usermodule/account/index.aspx");
                }
                else
                {
                    ShowError(errMsg);
                }
            }
        }

        public void ShowError(string msg)
        {
            string script = string.Format(@"
                <SCRIPT LANGUAGE='javascript'><!--
                alert('{0}');location.href = '/index.aspx';
                //--></SCRIPT>", msg);

            HttpContext.Current.Response.Write(script);
            //this.ClientScript.RegisterStartupScript(this.GetType(), "ShowError", script);
        }

        public string SignIn()
        {
            string message = "";

            try
            {
                string userName = WebBase.GetFormString("username", "");
                string userPwd = WebBase.GetFormString("password", "");
                string ispass = WebBase.GetFormString("ckbsavepass", "");
                string code = WebBase.GetFormString("imycode", "");

                if (string.IsNullOrEmpty(code))
                {
                    message = ("请输入验证码!");
                }
                else if (string.IsNullOrEmpty(userName))
                {
                    message = ("请输入商户名!");
                }
                else if (string.IsNullOrEmpty(userPwd))
                {
                    message = ("请输入商户密码!");
                }
                else
                {
                    message = WebUtility.CheckValiDateCode(code);
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
                        if (ispass != null)
                        {
                            HttpCookie hc = new HttpCookie("yklm_user");
                            DateTime dt = DateTime.Now;
                            TimeSpan ts = new TimeSpan(90, 0, 0, 0, 0);//过期时间为1分钟
                            hc.Expires = dt.Add(ts);//设置过期时间

                            hc.Values.Add("username", userName);
                            hc.Values.Add("userpass", Cryptography.MD5(userPwd));
                            Response.AppendCookie(hc);
                        }
                        else
                        {
                            HttpCookie hc = new HttpCookie("yklm_user");
                            hc.Expires = DateTime.Now.AddMonths(-24);
                            Response.Cookies.Add(hc);
                        }
                    }

                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                message = exception.Message;
            }

            return message;
        }
    }
}