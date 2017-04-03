using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace viviAPI.WebUI7uka.usermodule
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            viviapi.BLL.User.Login.SignOut();

            string script = string.Format(@"
<SCRIPT LANGUAGE='javascript'><!--
top.location.href=""{0}"";
//--></SCRIPT>", "/index.aspx");

            HttpContext.Current.Response.Write(script);
        }
    }
}
