using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.ETAPI.Ecpss;

namespace viviAPI.Gateway2018.Receive.Ecpss
{
    public partial class bank : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            viviapi.ETAPI.Ecpss.Bank api = new Bank();
            api.Notify();
        }
    }
}