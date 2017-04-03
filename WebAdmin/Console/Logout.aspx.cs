using System;
using System.Web;

namespace viviAPI.WebAdmin.Console
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            viviapi.BLL.ManageFactory.SignOut();

            string url = string.Format("/{0}/Login.aspx", viviapi.SysConfig.RuntimeSetting.ManagePagePath);
            string script = string.Format(@"
<SCRIPT LANGUAGE='javascript'><!--
top.location.href=""{0}"";
//--></SCRIPT>", url);

            HttpContext.Current.Response.Write(script);  
        }
    }
}

