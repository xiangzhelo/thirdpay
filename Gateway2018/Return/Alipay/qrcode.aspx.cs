using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace viviAPI.Gateway2018.Return.Alipay
{
    public partial class Qrcode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string goodsID = Request["goods_id"];

            string callbackText = string.Format("{{is_success:\"T\",out_trade_no:\"{0}\"}}", goodsID);

            Response.Write(callbackText);
            Response.End();
        }
    }
}
