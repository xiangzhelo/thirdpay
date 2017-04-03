using System;
using System.Web;
using viviapi.Model.User;
using viviapi.WebComponents;
using viviapi.WebComponents.Web;
using viviLib;
using viviLib.Security;
using viviLib.Web;

namespace viviAPI.WebUI7uka.agent
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Login : PageBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Cookies["yklm_agent"] != null)
                {
                    //UserNameBox.Value = Request.Cookies["yklm_agent"]["username"].ToString();
                    //password.Attributes["value"] = "**********";

                }
            }

            if (XRequest.IsPost())
            {
                SignIn();
            }
        }

        private void SignIn()
        {
            string message = "";

            string userName = XRequest.GetString("username");
            string passWord = XRequest.GetString("password");
            string code = XRequest.GetString("CCode");
            string RememberMe = WebBase.GetFormString("RememberMe", "");
            if (string.IsNullOrEmpty(code))
            {
                message = ("请输入验证码!");
            }
            else if (string.IsNullOrEmpty(userName))
            {
                message = ("请输入代理账号!");
            }
            else if (string.IsNullOrEmpty(passWord))
            {
                message = ("请输入代理密码!");
            }
            else
            {
                message = WebUtility.CheckValiDateCode(code);
            }

            if (string.IsNullOrEmpty(message))
            {

                string lastLoginIp = ServerVariables.TrueIP;
                string lastLoginAddress = WebUtility.GetIPAddress(lastLoginIp);

                message = viviapi.BLL.User.Login.SignIn(0, 1, userName, Cryptography.MD5(passWord), lastLoginIp,
                    lastLoginAddress);

                if (message == "success")
                {
                    if (viviapi.BLL.User.Login.CurrentMember.UserType == UserTypeEnum.代理)
                    {
                        if (RememberMe != null)
                        {
                            HttpCookie hc = new HttpCookie("yklm_agent");
                            DateTime dt = DateTime.Now;
                            TimeSpan ts = new TimeSpan(90, 0, 0, 0, 0);//过期时间为1分钟
                            hc.Expires = dt.Add(ts);//设置过期时间

                            hc.Values.Add("username", userName);

                            Response.AppendCookie(hc);
                        }
                        else
                        {
                            HttpCookie hc = new HttpCookie("yklm_agent");
                            hc.Expires = DateTime.Now.AddMonths(-24);
                            Response.Cookies.Add(hc);
                        }
                    }
                    else
                        message=("非代理权限，无法登录！");
                }
            }

            if (message == "success")
            {
                Response.Redirect("main.aspx");
            }
            else
            {
                ShowMessageBox(message);
            }
        }
       


    }

}
