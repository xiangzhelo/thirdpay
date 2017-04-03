using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace viviAPI.Gateway2018.Receive.YeePayZGT
{
    public partial class callback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["data"]))
            {
                string data = Request["data"];
                var zgtNoti = new viviapi.ETAPI.YeePay.ZGT.Bank();
                zgtNoti.Notify(data);
            }


        }
    }
}