using System;

namespace viviAPI.Gateway2018.Receive.Alipay
{
    public partial class Result : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            viviapi.ETAPI.Alipay.AliPay.Default.Notify();
        }
    }
}
