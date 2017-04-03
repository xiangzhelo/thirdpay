using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace viviAPI.Gateway2018.Return.TaoShang
{
    public partial class bank : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            viviapi.ETAPI.TaoShang.Bank api = new viviapi.ETAPI.TaoShang.Bank();
            api.ReturnBank();
        }
    }
}