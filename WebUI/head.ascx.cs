using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace viviAPI.WebUI7uka
{
    public partial class Head : System.Web.UI.UserControl
    {
        public bool IsShowQQLogin
        {
            get
            {
                if(ViewState["IsShowQQLogin"] == null)
                    return true;

                return Convert.ToBoolean(ViewState["IsShowQQLogin"]);
            }
            set
            {
                ViewState["IsShowQQLogin"] = value;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //lit_qq.Visible = IsShowQQLogin;
            }
        }
    }
}