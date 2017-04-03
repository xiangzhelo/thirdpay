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

namespace viviapi.web.Business
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
            if (XRequest.IsPost())
            {                
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
                        AlertAndRedirect(string.Empty, "Default.aspx");
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
