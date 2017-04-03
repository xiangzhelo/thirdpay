using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.ETAPI.Common;
using viviapi.Model.Order.Card;

namespace viviAPI.Gateway2018
{
    public partial class ViViSoft_CardTEST : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int SuppId = int.Parse(this.txtSuppId.Text);
            string SuppTransNo = "test_sup" + DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture);
            string SysOrderNo = txtSysOrderId.Text;
            decimal TranAmt = decimal.Parse(this.txtTranAmt.Text);

            var response = new CardOrderSupplierResponse()
            {
                SupplierId = SuppId,
                SuppTransNo = SuppTransNo,
                SysOrderNo = SysOrderNo,
                OrderAmt = TranAmt,
                SuppAmt = 0M,
                OrderStatus = 2,
                SuppErrorCode = txterrCode.Text.Trim(),
                Opstate = "",
                SuppErrorMsg = txterrCode.Text.Trim(),
                ViewMsg = txterrCode.Text.Trim(),
                Method = 1
            };


            OrderCardUtils.SuppNotify(response,"");
        }
    }
}
