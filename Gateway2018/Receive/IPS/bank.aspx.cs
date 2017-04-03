using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace viviAPI.Gateway2018.Receive.IPS
{
    public partial class Bank : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            viviapi.ETAPI.IPS.Bank.Instance.Notify();
        }
    }
}
