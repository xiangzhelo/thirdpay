using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.ETAPI.Alipay;

namespace viviAPI.Gateway2018.Receive.Alipay
{
    public partial class diytool : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var api = new AliPayTools();
            api.Notify2();
        }
    }
}