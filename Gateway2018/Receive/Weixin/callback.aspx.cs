using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.ETAPI.Common;
using viviapi.ETAPI.tenpay.tenpayLib;
using viviapi.ETAPI.Weixin;
using viviapi.Model.supplier;

namespace viviAPI.Gateway2018.Receive.Weixin
{
    /// <summary>
    /// 
    /// </summary>
    public partial class callback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var resHandler = new ResponseHandlerX(Context);
            resHandler.init();
            resHandler.setKey(TenpayUtil.key, string.Empty);

            bool signOk = resHandler.IsTenpaySign();

            if (signOk)
            {
                string return_code = resHandler.getParameter("return_code");
                string return_msg = resHandler.getParameter("return_msg");
                string result_code = resHandler.getParameter("result_code");

                string total_fee = resHandler.getParameter("total_fee");
                string transaction_id = resHandler.getParameter("transaction_id");
                string out_trade_no = resHandler.getParameter("out_trade_no");
                string attach = resHandler.getParameter("attach");

                string opstate = "-1";
                int status = 4;
                decimal tranAmt = 0M;

                if (return_code == "SUCCESS"
                    && result_code == "SUCCESS")
                {
                    status = 2;
                    opstate = "0";
                    tranAmt = decimal.Parse(total_fee) / 100M;
                }

                string retSuccXml = @"<xml>
<return_code><![CDATA[SUCCESS]]></return_code>
<return_msg></return_msg>
</xml>";
                string retFailXml = @"<xml>
<return_code><![CDATA[FAIL]]></return_code>
<return_msg></return_msg>
</xml>";
                OrderBankUtils.SuppNotify((int)SupplierCode.Weixin
                                        , out_trade_no
                                        , transaction_id
                                        , status
                                        , opstate
                                        , return_msg
                                        , tranAmt, tranAmt
                                        , retSuccXml
                                        , retFailXml);


                //BankUtils.SuppNotify(1006
                //                , out_trade_no
                //                , transaction_id
                //                , status
                //                , opstate
                //                , return_msg
                //                , tranAmt
                //                , retXml);
            }
            else
            {
                Response.Write("fail -md5 failed");
                Response.Write(resHandler.getDebugInfo());
            }
        }
    }
}