using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.ETAPI.YeePay;

namespace viviAPI.Gateway2018.Receive.YeePay
{
    public partial class Bank : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RMB.Default.Return();
        }
    }
}
