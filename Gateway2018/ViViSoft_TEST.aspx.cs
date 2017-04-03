using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.ETAPI.Common;

namespace viviAPI.Gateway2018
{
    public partial class ViViSoft_TEST : System.Web.UI.Page
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

            OrderBankUtils.SuppNotify(SuppId
                     , SysOrderNo
                     , SuppTransNo
                     , 2
                     , "0"
                     , "sucess"
                     , TranAmt
                     ,0M
                     , "OK"
                     , "NO");
        }
    }
}
