using System;
using System.Web;
using System.Web.UI;
using viviapi.Model.User;
using viviapi.WebComponents;
using viviapi.WebComponents.Web;
using viviLib.ExceptionHandling;
using viviLib.Security;
using viviLib.Web;

namespace viviAPI.WebUI7uka
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UserLoginx : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Cookies["yklm_user"] != null)
                {
                    //username.Value = Request.Cookies["yklm_user"]["username"].ToString();
                    //password.Attributes["value"] = "**********";

                }
            }
        }

        public void Redirect(string url)
        {
            string script = string.Format(@"
<SCRIPT LANGUAGE='javascript'><!--

top.location.href=""{0}"";
//--></SCRIPT>", url);

            this.ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script);
        }

        public void ShowError(string msg)
        {
            string script = string.Format(@"
<SCRIPT LANGUAGE='javascript'><!--
parent.msg();
//--></SCRIPT>", msg);

            this.ClientScript.RegisterStartupScript(this.GetType(), "ShowError", script);
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            string errMsg = SignIn();

            if (errMsg == "success")
            {
                Redirect("/usermodule/account/index.aspx");
            }
            else
            {
                ShowError(errMsg);
            }
        }


        public string SignIn()
        {
            string message = "";

            try
            {
                string userName = WebBase.GetFormString("username", "");
                string userPwd = WebBase.GetFormString("password", "");
                string ispass = WebBase.GetFormString("ckbsavepass", "");
                string code =  WebBase.GetFormString("imycode", "");

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
                    string lastLoginIp = ServerVariables.TrueIP;
                    string lastLoginAddress = WebUtility.GetIPAddress(lastLoginIp);

                    message = viviapi.BLL.User.Login.SignIn(0, 0,userName, Cryptography.MD5(userPwd), lastLoginIp, lastLoginAddress);

                    if (message == "success")
                    {
                        if (ispass != null)
                        {
                            HttpCookie hc = new HttpCookie("yklm_user");
                            DateTime dt = DateTime.Now;
                            TimeSpan ts = new TimeSpan(90, 0, 0, 0, 0);//过期时间为1分钟
                            hc.Expires = dt.Add(ts);//设置过期时间

                            //hc.Values.Add("username", userName);
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