using System;

namespace viviAPI.WebAdmin.Console.Tools
{
    public partial class Md5 : viviapi.WebComponents.Web.ManagePageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtMd5Str.Text.Trim()))
            {
                AlertAndRedirect("请输入要加密的字符串");
                return;
            }
            string str = this.txtMd5Str.Text.Trim();

            this.txtresult.Text = viviLib.Security.Cryptography.MD5(str, this.ddlencode.SelectedValue);
        }
    }
}