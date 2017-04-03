using System;
using System.Web;

namespace viviAPI.WebUI7uka.agent
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            viviapi.BLL.User.Login.SignOut();

            string script = string.Format(@"
<SCRIPT LANGUAGE='javascript'><!--
top.location.href=""{0}"";
//--></SCRIPT>", "/agent/Login.aspx");

            HttpContext.Current.Response.Write(script);  
        }
    }
}

