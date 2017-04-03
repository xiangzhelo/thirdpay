using System.Globalization;

using System;
using System.Text;
using System.Web;
using viviapi.ETAPI.Common;
using viviapi.ETAPI.YeePay.Lib.com.yeepay;
using viviapi.Model.supplier;
using viviapi.SysConfig;
using viviapi.Model.Order;

namespace viviapi.ETAPI.YeePay
{
    /// <summary>
    /// 
    /// </summary>
    public class RMB : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.YeePay;

        public RMB()
            : base(SuppId)
        {

        }

        public static RMB Default
        {
            get
            {
                var instance = new RMB();
                return instance;
            }
        }

        internal string NotifyUrl { get { return this.SiteDomain + "/receive/yeepay/bank.aspx"; } }
        internal string Succflag = "SUCCESS";
        internal string Failflag = "FAIL";

        #region GetPayUrl
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="orderAmt"></param>
        /// <param name="bankcode"></param>
        /// <returns></returns>
        public string GetPayUrl(string orderid, decimal orderAmt, string bankcode)
        {
            string puserid = this.SuppAccount;
            string puserkey = this.SuppKey;

            HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("gb2312");
            Buy.NodeAuthorizationURL = "https://www.yeepay.com/app-merchant-proxy/node";
            if (!string.IsNullOrEmpty(this.PostBankUrl))
            {
                Buy.NodeAuthorizationURL = this.PostBankUrl;
            }

            string p2_Order = orderid;
            string p3_Amt = decimal.Round(orderAmt, 2).ToString(CultureInfo.InvariantCulture);
            string p4_Cur = "CNY";
            string p5_Pid = SysConfig.PaymentSetting.yeepay_pid;
            string p6_Pcat = SysConfig.PaymentSetting.yeepay_pcat;
            string p7_Pdesc = SysConfig.PaymentSetting.yeepay_pdesc;
            /*代理支付页*/
            string p8_Url = NotifyUrl;//Configuration.GetConfig().SiteDomain

            string p9_SAF = "1";
            //if (!string.IsNullOrEmpty(orderInfo.returnurl))
            //p9_SAF = "1";

            string pa_MP = "";
            string pd_FrpId = Bank.GetBankCode(bankcode);
            string pr_NeedResponse = "1";

            return Buy.CreateBuyUrl(puserid, puserkey, p2_Order, p3_Amt, p4_Cur, p5_Pid, p6_Pcat, p7_Pdesc, p8_Url, p9_SAF, pa_MP, pd_FrpId, pr_NeedResponse) + "&noLoadingPage=1";
        }
        #endregion

        #region GetPayForm
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="orderAmt"></param>
        /// <param name="bankcode"></param>
        /// <returns></returns>
        public string GetPayForm(string orderid, decimal orderAmt, string bankcode, bool autosumit)
        {
            string puserid = this.SuppAccount;
            string puserkey = this.SuppKey;

            HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("gb2312");
            Buy.NodeAuthorizationURL = "https://www.yeepay.com/app-merchant-proxy/node";
            if (!string.IsNullOrEmpty(this.PostBankUrl))
                Buy.NodeAuthorizationURL = PostBankUrl;

            if (!string.IsNullOrEmpty(SuppInfo.jumpUrl))
            {
                Buy.NodeAuthorizationURL = this.SuppInfo.jumpUrl + "/switch/yeepay.aspx";
            } 

            string p2Order = orderid;
            string p3Amt = decimal.Round(orderAmt, 2).ToString(CultureInfo.InvariantCulture);
            const string p4Cur = "CNY";
            string p5Pid = PaymentSetting.yeepay_pid;
            string p6Pcat = PaymentSetting.yeepay_pcat;
            string p7Pdesc = PaymentSetting.yeepay_pdesc;
            /*代理支付页*/
            string p8Url = NotifyUrl;//Configuration.GetConfig().SiteDomain

            const string p9Saf = "1";
            //if (!string.IsNullOrEmpty(orderInfo.returnurl))
            //p9_SAF = "1";

            const string paMp = "";
            string pdFrpId = Bank.GetBankCode(bankcode);
            const string prNeedResponse = "1";

            string formHtml = Buy.CreateBuyForm(puserid, puserkey, p2Order, p3Amt, p4Cur, p5Pid, p6Pcat, p7Pdesc, p8Url, p9Saf, paMp, pdFrpId, prNeedResponse, "payform");

            if (autosumit)
            formHtml += ("<script type=\"text/javascript\" language=\"javascript\">function go(){ var _form = document.forms['payform']; _form.submit();};setTimeout(function(){go()},100);</script>");


            return formHtml;

        }
        #endregion
        
        /// <summary>
        /// 
        /// </summary>
        public void Return()
        {
            string opstate = "-1";
            int status = 4;

            BuyCallbackResult result = Buy.VerifyCallback(SuppAccount
                , SuppKey
                , FormatQueryString.GetQueryString("r0_Cmd")
                , FormatQueryString.GetQueryString("r1_Code")
                , FormatQueryString.GetQueryString("r2_TrxId")
                , FormatQueryString.GetQueryString("r3_Amt")
                , FormatQueryString.GetQueryString("r4_Cur")
                , FormatQueryString.GetQueryString("r5_Pid")
                , FormatQueryString.GetQueryString("r6_Order")
                , FormatQueryString.GetQueryString("r7_Uid")
                , FormatQueryString.GetQueryString("r8_MP")
                , FormatQueryString.GetQueryString("r9_BType")
                , FormatQueryString.GetQueryString("rp_PayDate")
                , FormatQueryString.GetQueryString("hmac"));

            if (string.IsNullOrEmpty(result.ErrMsg))
            {
                string msg = "支付失败";

                if (result.R1_Code == "1")
                {
                    msg = "支付成功";
                    opstate = "0";
                    status = 2;
                }
                decimal tranAmt = 0M;
                decimal.TryParse(result.R3_Amt, out tranAmt);

                if (result.R9_BType == "1")
                {
                    OrderBankUtils.SuppPageReturn(SuppId
                       , result.R6_Order
                       , result.R2_TrxId
                       , status
                       , opstate
                       , msg
                       , tranAmt, 0M);
                }
                else if ((result.R9_BType == "2") || (result.R9_BType == "3"))
                {
                    OrderBankUtils.SuppNotify(SuppId
                      , result.R6_Order
                      , result.R2_TrxId
                      , status
                      , opstate
                      , msg
                      , tranAmt, tranAmt
                      , Succflag
                      , Failflag);
                }
            }
        }
    }
}

