using System;

namespace viviAPI.Gateway2018.Receive.Alipay
{
    public partial class qrcode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var api = new viviapi.ETAPI.Alipay.Qrcode();
            api.Notify();
        }
    }
}
