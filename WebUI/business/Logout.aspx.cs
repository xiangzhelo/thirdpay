using System;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;

namespace viviapi.web.Business
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL.ManageFactory.SignOut();

            string script = string.Format(@"
<SCRIPT LANGUAGE='javascript'><!--
top.location.href=""{0}"";
//--></SCRIPT>", "/business/Login.aspx");

            HttpContext.Current.Response.Write(script);  
        }
    }
}

