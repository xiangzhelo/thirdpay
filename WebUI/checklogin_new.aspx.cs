using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.WebComponents;
using viviLib.ExceptionHandling;
using viviLib.Security;
using viviLib.Web;

namespace viviAPI.WebUI7uka
{
    public class loginresult
    {
        //
        public bool res { get; set; }
        public string thirdreturn { get; set; }
        public bool needValidateCode { get; set; }

    }

    public partial class CheckloginNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SignIn();
        }

        public string SignIn()
        {
            string message = "";

            try
            {
                string userName = WebBase.GetFormString("username", "");
                string userPwd = WebBase.GetFormString("password", "");
                string ispass = WebBase.GetFormString("PersistentCookie", "");
                string code = WebBase.GetFormString("validateCode", "");

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

                    message = viviapi.BLL.User.Login.SignIn(0, 0, userName, Cryptography.MD5(userPwd), lastLoginIp, lastLoginAddress);

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
