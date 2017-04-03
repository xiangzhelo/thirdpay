using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace viviAPI.Gateway2018.Return.Dinpay
{
    public partial class Bank : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var api = new viviapi.ETAPI.Dinpay.Bank();
            api.ReturnBank();
        }
    }
}
