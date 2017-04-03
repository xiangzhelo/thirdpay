using System;
using viviapi.ETAPI.Alipay;

namespace viviAPI.Gateway2018.Receive.Alipay
{
    public partial class MApi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AliPayMApi.Default.Notify();
        }
    }
}
