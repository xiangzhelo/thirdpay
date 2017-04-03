

namespace viviapi.ETAPI.YeePay.Lib.com.yeepay
{
    public abstract class BuyOld : FormatQueryString
    {
        private static string nodeAuthorizationURL = "https://www.yeepay.com/app-merchant-proxy/node";

        public static string CreateForm(string p1_MerId, string keyValue, string p2_Order, string p3_Amt, string p4_Cur, string p5_Pid, string p8_Url, string p9_SAF, string pa_MP, string pd_FrpId, string formId)
        {
            return Buy.CreateBuyForm(p1_MerId, keyValue, p2_Order, p3_Amt, "CNY", p5_Pid, "", "", p8_Url, p9_SAF, pa_MP, pd_FrpId, "", "", "1", formId);
        }

        public static string CreateUrl(string p1_MerId, string keyValue, string p2_Order, string p3_Amt, string p4_Cur, string p5_Pid, string p8_Url, string p9_SAF, string pa_MP, string pd_FrpId)
        {
            return Buy.CreateBuyUrl(p1_MerId, keyValue, p2_Order, p3_Amt, "CNY", p5_Pid, "", "", p8_Url, p9_SAF, pa_MP, pd_FrpId, "", "", "1");
        }

        public static QueryOrdResult QueryOrder(string p1_MerId, string keyValue, string p2_Order)
        {
            string aValue = "";
            aValue = (aValue + "QueryOrdDetail") + p1_MerId + p2_Order;
            string para = "";
            para = (((para + "?p0_Cmd=QueryOrdDetail") + "&p1_MerId=" + p1_MerId) + "&p2_Order=" + p2_Order) + "&hmac=" + Digest.HmacSign(aValue, keyValue);
            string strUrl = HttpUtils.SendRequest(nodeAuthorizationURL, para);
            string str4 = FormatQueryString.GetQueryString("r0_Cmd", strUrl, '\n');
            string returnCode = FormatQueryString.GetQueryString("r1_Code", strUrl, '\n');
            string returnTrxId = FormatQueryString.GetQueryString("r2_TrxId", strUrl, '\n');
            string returnAmt = FormatQueryString.GetQueryString("r3_Amt", strUrl, '\n');
            string str8 = FormatQueryString.GetQueryString("r4_Cur", strUrl, '\n');
            string returnPid = FormatQueryString.GetQueryString("r5_Pid", strUrl, '\n');
            string returnOrder = FormatQueryString.GetQueryString("r6_Order", strUrl, '\n');
            string returnAllPara = FormatQueryString.GetQueryString("r8_MP", strUrl, '\n');
            string returnStatus = FormatQueryString.GetQueryString("rb_PayStatus", strUrl, '\n');
            string returnRefundCount = FormatQueryString.GetQueryString("rc_RefundCount", strUrl, '\n');
            string str14 = FormatQueryString.GetQueryString("rd_RefundAmt", strUrl, '\n');
            string str15 = FormatQueryString.GetQueryString("hmac", strUrl, '\n');
            return new QueryOrdResult(returnCode, returnTrxId, returnAmt, returnPid, returnOrder, returnStatus, returnAllPara, returnAmt, returnRefundCount);
        }

        public static string NodeAuthorizationURL
        {
            get
            {
                return nodeAuthorizationURL;
            }
            set
            {
                nodeAuthorizationURL = value;
            }
        }
    }
}

