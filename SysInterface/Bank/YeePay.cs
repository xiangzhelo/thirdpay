using System;
using System.Globalization;
using System.Text;
using System.Web;
using viviapi.Model.Order;
using viviapi.SysInterface.Lib.YeePay;

namespace viviapi.SysInterface.Bank
{
    /// <summary>
    /// 
    /// </summary>
    public class YeePay
    {
        public static string VbYee10 = "vbYee.10";
        public static string VbYee10ApiName = "易宝网银";
        public static string VbYee10BankNotifySuccessflag = "SUCCESS";

        #region CreateNotifyUrl
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderinfo"></param>
        /// <param name="isNotify"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public static string CreateNotifyUrl(OrderBankInfo orderinfo, bool isNotify, string apiKey)
        {
            string notifyUrl = string.Empty;

            if (orderinfo == null || string.IsNullOrEmpty(apiKey))
            {
                return notifyUrl;
            }

            notifyUrl = isNotify ? orderinfo.notifyurl : orderinfo.returnurl;

            string r1Code = "0";//支付结果
            string p1MerId = orderinfo.userid.ToString(CultureInfo.InvariantCulture);
            const string r0Cmd = "Buy";
            if (orderinfo.status == 2 || orderinfo.status == 8)
            {
                r1Code = "1";
            }
            string r2TrxId = orderinfo.orderid;
            string r3Amt = "0";
            if (orderinfo.realvalue.HasValue)
                r3Amt = decimal.Round(orderinfo.realvalue.Value, 2).ToString(CultureInfo.InvariantCulture);
            
            const string r4Cur = "RMB";
            string r5Pid = orderinfo.cus_subject;
            string r6Order = orderinfo.userorder;
            const string r7Uid = "";
            string r8Mp = orderinfo.attach;
            string r9BType = "1";
            if (isNotify)
                r9BType = "2";

            string systime = "";
            if (orderinfo.completetime.HasValue)
                systime = orderinfo.completetime.Value.ToString("yyyy-MM-dd HH:mm:ss");

            string sbOld = "";

            sbOld += p1MerId;
            sbOld += r0Cmd;
            sbOld += r1Code;
            sbOld += r2TrxId;
            sbOld += r3Amt;

            sbOld += r4Cur;
            sbOld += r5Pid;
            sbOld += r6Order;
            sbOld += r7Uid;
            sbOld += r8Mp;
            sbOld += r9BType;

            string nhmac = Digest.HmacSign(sbOld, apiKey);

            var parms = new StringBuilder();
            parms.AppendFormat("p1_MerId={0}", FormatQueryString(p1MerId));
            parms.AppendFormat("&r0_Cmd={0}", FormatQueryString(r0Cmd));
            parms.AppendFormat("&r1_Code={0}", FormatQueryString(r1Code));
            parms.AppendFormat("&r2_TrxId={0}", FormatQueryString(r2TrxId));//易宝支付交易流水号
            parms.AppendFormat("&r3_Amt={0}", FormatQueryString(r3Amt));
            parms.AppendFormat("&r4_Cur={0}", FormatQueryString(r4Cur));
            parms.AppendFormat("&r5_Pid={0}", FormatQueryString(r5Pid));//商品名称
            parms.AppendFormat("&r6_Order={0}", FormatQueryString(r6Order));//商户订单号
            parms.AppendFormat("&r7_Uid={0}", FormatQueryString(r7Uid));
            parms.AppendFormat("&r8_MP={0}", FormatQueryString(r8Mp));
            parms.AppendFormat("&r9_BType={0}", FormatQueryString(r9BType));//为“1”: 浏览器重定向; 为“2”: 服务器点对点通讯.
            parms.AppendFormat("&rb_BankId={0}", FormatQueryString(orderinfo.paymodeId));//支付通道编码
            parms.AppendFormat("&ro_BankOrderId={0}", FormatQueryString(orderinfo.supplierOrder));
            parms.AppendFormat("&rp_PayDate={0}", FormatQueryString(systime));
            parms.AppendFormat("&ro_BankOrderId={0}", FormatQueryString(orderinfo.supplierOrder));
            parms.AppendFormat("&rq_CardNo={0}", FormatQueryString(string.Empty));
            parms.AppendFormat("&ru_Trxtime={0}", FormatQueryString(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            parms.AppendFormat("&hmac={0}", FormatQueryString(nhmac));

            if (notifyUrl.IndexOf("?", System.StringComparison.Ordinal) > 0)
            {
                notifyUrl = notifyUrl + "&" + parms.ToString();
            }
            else
            {
                notifyUrl = notifyUrl + "?" + parms.ToString();
            }

            return notifyUrl;
        }
        #endregion

        /// <summary>
        /// 检查
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="p2Order"></param>
        /// <param name="p3Amt"></param>
        /// <param name="p4Cur"></param>
        /// <param name="p5Pid"></param>
        /// <param name="p6Pcat"></param>
        /// <param name="p7Pdesc"></param>
        /// <param name="p8Url"></param>
        /// <param name="p9Saf"></param>
        /// <param name="paMp"></param>
        /// <param name="pdFrpId"></param>
        /// <param name="prNeedRespone"></param>
        /// <param name="keyValue"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public static bool CheckSign(string merchantId
            , string p2Order
            , string p3Amt
            , string p4Cur
            , string p5Pid
            , string p6Pcat
            , string p7Pdesc
            , string p8Url
            , string p9Saf
            , string paMp
            , string pdFrpId
            , string prNeedRespone
            , string keyValue
            , string sign)
        {
            string sbOld = "";

            sbOld += "Buy";
            sbOld += merchantId;
            sbOld += p2Order;
            sbOld += p3Amt;
            sbOld += p4Cur;

            sbOld += p5Pid;
            sbOld += p6Pcat;
            sbOld += p7Pdesc;
            sbOld += p8Url;
            sbOld += p9Saf;

            sbOld += paMp;
            sbOld += pdFrpId;
            sbOld += prNeedRespone;

            string hmac = YeePayLib.Digest.HmacSign(sbOld, keyValue);
            return sign == hmac;
        }

        static string FormatQueryString(string value)
        {
            return HttpUtility.UrlEncode(value, Encoding.GetEncoding("GB2312"));
        }

        #region ConverBankCode
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bankcode"></param>
        /// <returns></returns>
        public static string ConverBankCode(string bankcode)
        {
            string code = string.Empty;

            switch (bankcode)
            {
                case "CMBCHINA-NET":
                    code = "970"; //招商银行
                    break;
                case "ICBC-NET":
                    code = "967"; //中国工商银行
                    break;
                case "ABC-NET":
                    code = "964"; //中国农业银行
                    break;
                case "CCB-NET":
                    code = "965"; //中国建设银行
                    break;
                case "BOC-NET":
                    code = "963"; //中国银行
                    break;
                case "SPDB-NET":
                    code = "977"; //浦发银行
                    break;
                case "BOCO-NET":
                    code = "981"; //中国交通银行
                    break;
                case "CMBC-NET":
                    code = "980"; //中国民生银行
                    break;
                case "SDB-NET":
                    code = "974"; //深圳发展银行
                    break;
                case "GDB-NET":
                    code = "985"; //广东发展银行
                    break;
                case "ECITIC-NET":
                    code = "962"; //中信银行
                    break;
                case "HXB-NET":
                    code = "982"; //华夏银行
                    break;
                case "CIB-NET":
                    code = "972"; //兴业银行
                    break;
                case "BCCB-NET":
                    code = "989"; //北京银行
                    break;
                case "CEB-NET":
                    code = "986"; //中国光大银行
                    break;
                case "PINGANBANK-NET":
                    code = "978"; //平安银行
                    break;
                case "SHB-NET":
                    code = "975"; //上海银行
                    break;
                case "POST-NET":
                    code = "971"; //中国邮政储蓄银行
                    break;
                //case "984":
                //    code = "00011"; //广州农村商业银行
                //    break;
                //case "1015":
                //    code = "GZCB"; //广州银行
                //    break;
                //case "1016":
                //    code = "CUPS"; //中国银联
                //    break;
                //case "976":
                //    code = "00030"; //上海农村商业银行
                //    break;
                //case "971":
                //    code = "POST"; //中国邮政
                //    break;

                case "CBHB-NET":
                    code = "CBHB"; //渤海银行
                    break;
                case "BJRCB-NET":
                    code = "990"; //北京农商银行
                    break;
                case "NJCB-NET":
                    code = "979"; //南京银行
                    break;
                case "HKBEA-NET":
                    code = "987"; //东亚银行
                    break;
                case "NBCB-NET ":
                    code = "998"; //宁波银行
                    break;
                case "HZBANK-NET":
                    code = "983"; //杭州银行
                    break;

                //case "1028":
                //    code = "HSB"; //徽商银行
                //    break;
                case "CZ-NET":
                    code = "968"; //浙商银行
                    break;

              
                case "ALIPAY":
                    code = "992"; //支付宝
                    break;

             
                case "TENPAY":
                    code = "993"; //财付通
                    break;

                case "ALIPAYDCODE":
                    code = "1003"; //支付宝扫码
                    break;

                //case "1032":
                //    code = "UPOP"; //银联在线支付
                //    break;
                default:
                    code = "967";
                    break;
            }
            return code;
        }
        #endregion

        
    }
}
