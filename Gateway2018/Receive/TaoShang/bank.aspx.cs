using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.ETAPI.TaoShang;

namespace viviAPI.Gateway2018.Receive.TaoShang
{
    public partial class bank : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            viviapi.ETAPI.TaoShang.Bank api = new Bank();
            api.Notify();
        }
    }
}