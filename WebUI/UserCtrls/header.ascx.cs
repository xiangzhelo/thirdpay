using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace viviapi.web.UserCtrls
{
    public partial class header : System.Web.UI.UserControl
    {
        protected string indexclass = string.Empty;
        protected string indexclass1 = string.Empty;
        protected string productclass = string.Empty;
        protected string productclass1 = string.Empty;

        protected string solutionclass = string.Empty;
        protected string solutionclass1 = string.Empty;

        protected string newsclass = string.Empty;
        protected string introdclass = string.Empty;
        protected string contactclass = string.Empty;
        protected string contactclass1 = string.Empty;
        protected string searchclass = string.Empty;

        public string showtype
        {
            set
            {
                ViewState["UserCtrls_header_showtype"] = value;
            }
            get
            {
                if (ViewState["UserCtrls_header_showtype"] == null)
                    return "index";
                return ViewState["UserCtrls_header_showtype"].ToString();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (showtype == "index")
            {
                indexclass = "class=\"b-over-font\"";
                indexclass1 = "class=\"b-over\"";
            }
            else if (showtype == "product")
            {
                productclass = "class=\"b-over-font\"";
                productclass1 = "class=\"b-over\"";
            }
            else if (showtype == "solution")
            {
                solutionclass = "class=\"b-over-font\"";
                solutionclass1 = "class=\"b-over\"";
            }
            else if (showtype == "contact")
            {
                contactclass = "class=\"b-over-font\"";
                contactclass1 = "class=\"b-over\"";
            }

            else if (showtype == "news")
                newsclass = "class=\"b-over-font\"";
            else if (showtype == "introd")
                introdclass = "class=\"b-over-font\"";

            else if (showtype == "search")
                searchclass = "class=\"b-over-font\"";
        }
    }
}