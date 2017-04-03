using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace viviapi.web.Manage
{
    public partial class login2 : viviapi.WebComponents.Web.ManagePageBase
    {
        public string RedirectUrl
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringString("RedirectUrl", "");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOk_ServerClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtPsec.Value.Trim()))
            {
                this.lblMessage.InnerText = "请输入密码";
                return;
            }
            if (BLL.ManageFactory.SecPwdVaild(this.txtPsec.Value.Trim()))
            {
                Response.Redirect(RedirectUrl);
            }
            else
            {
                this.lblMessage.InnerText = "密码错误";
            }
        }
    }
}