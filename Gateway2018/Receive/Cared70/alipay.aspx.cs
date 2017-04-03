using System;

namespace viviAPI.Gateway2018.Receive.Cared70
{
    public partial class AliPay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            viviapi.ETAPI.Cared70.AliPay.Instance.Notify();
        }
    }
}
