using System;
using System.Configuration;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using viviapi.Model;
using viviapi.WebComponents;
using viviLib;
using viviLib.Security;
using viviLib.Web;

namespace viviapi.web.Manage
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Login : viviapi.WebComponents.Web.PageBase
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
                if (Request.Cookies["yklm_admin"] != null)
                {
                    //UserNameBox.Value = Request.Cookies["yklm_admin"]["username"].ToString();
                    //password.Attributes["value"] = "**********";

                }
            }

            if (XRequest.IsPost())
            {
                string code = XRequest.GetString("CCode").ToUpper();
                string secode = this.Session["CCode"].ToString().ToUpper();
                if (this.Session["CCode"] == null)
                {
                    AlertAndRedirect("验证码已失效!");
                    return;
                }
                else if (XRequest.GetString("CCode").ToUpper() != this.Session["CCode"].ToString().ToUpper())
                {
                    AlertAndRedirect("验证码错误!");
                    return;
                }
                else
                {
                    string userName = XRequest.GetString("UserNameBox");
                    string passWord = Cryptography.MD5(XRequest.GetString("pas"));
                    string RememberMe = WebBase.GetFormString("RememberMe", "");

                    Model.Manage manage = new viviapi.Model.Manage();
                    manage.username = userName;
                    manage.password = passWord;
                    manage.lastLoginTime = DateTime.Now;
                    manage.lastLoginIp = viviLib.Web.ServerVariables.TrueIP;
                    manage.LastLoginAddress = WebUtility.GetIPAddress(manage.lastLoginIp);
                    manage.LastLoginRemark = WebUtility.GetIPAddressInfo(manage.lastLoginIp);

                    string message = BLL.ManageFactory.SignIn(manage);

                    if (manage.id > 0)
                    {
                        if (RememberMe != null)
                        {
                            HttpCookie hc = new HttpCookie("yklm_admin");
                            DateTime dt = DateTime.Now;
                            TimeSpan ts = new TimeSpan(90, 0, 0, 0, 0);//过期时间为1分钟
                            hc.Expires = dt.Add(ts);//设置过期时间

                            hc.Values.Add("username", userName);
                            
                            Response.AppendCookie(hc);
                        }
                        else
                        {
                            HttpCookie hc = new HttpCookie("yklm_admin");
                            hc.Expires = DateTime.Now.AddMonths(-24);
                            Response.Cookies.Add(hc);
                        }

                        AlertAndRedirect(string.Empty, "main.aspx");
                    }
                    else
                    {
                        AlertAndRedirect(message);
                    }
                }
            }
        }
    }

}
