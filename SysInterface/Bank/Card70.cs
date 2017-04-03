using System.Globalization;
using System.Text;
using System.Web;
using viviapi.Model.Order;
using viviLib.Security;

namespace viviapi.SysInterface.Bank
{
    /// <summary>
    /// 
    /// </summary>
    public class Card70
    {
        #region 70储值
        public static string Vb7010 = "vb70.10";
        public static string Vb7010ApiName = "70Card平台用户储值接口-版本号1.0";
        public static string Vb7010BankReceiveVerifyStr = "userid={0}&orderid={1}&bankid={2}";
        public static string Vb7010BankNotifyVerifyStr = "returncode={0}&userid={1}&orderid={2}&keyvalue={3}";
        public static string Vb7010BankNotifySuccessflag = "ok";
        #endregion

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

            string userorder = orderinfo.userorder;
            string opstate = orderinfo.opstate;

            string returncode = "11";
            if (orderinfo.status == 2 || orderinfo.status == 8)
            {
                returncode = "1";
            }

            string money = "0";

            if (orderinfo.realvalue.HasValue)
                money = decimal.Round(orderinfo.realvalue.Value, 2).ToString(CultureInfo.InvariantCulture);

            string plain = string.Format(Vb7010BankNotifyVerifyStr
                          , returncode
                          , orderinfo.userid
                          , userorder
                          , apiKey);

            string sign = Cryptography.MD5(plain);

            var parms = new StringBuilder();
            parms.AppendFormat("returncode={0}", HttpUtility.UrlEncode(returncode));
            parms.AppendFormat("&userid={0}", HttpUtility.UrlEncode(orderinfo.userid.ToString(CultureInfo.InvariantCulture)));
            parms.AppendFormat("&orderid={0}", HttpUtility.UrlEncode(userorder));
            parms.AppendFormat("&money={0}", HttpUtility.UrlEncode(money));
            parms.AppendFormat("&sign={0}", HttpUtility.UrlEncode(sign));
            parms.AppendFormat("&ext={0}", HttpUtility.UrlEncode(orderinfo.attach, System.Text.Encoding.GetEncoding("GB2312")));

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

        #region ConverBankCode
        /// <summary>
        /// 
        /// </summary>
        /// <param name="version"></param>
        /// <param name="bankcode"></param>
        /// <returns></returns>
        public static string ConverBankCode(string bankcode)
        {
            string code = string.Empty;

           
                switch (bankcode)
                {
                    case "1001":
                        code = "970"; //招商银行
                        break;
                    case "1002":
                        code = "967"; //中国工商银行
                        break;
                    case "1005":
                        code = "964"; //中国农业银行
                        break;
                    case "1003":
                        code = "965"; //中国建设银行
                        break;
                    case "1052":
                        code = "963"; //中国银行
                        break;
                    case "1004":
                        code = "977"; //浦发银行
                        break;
                    case "1020":
                        code = "981"; //中国交通银行
                        break;
                    case "1006":
                        code = "980"; //中国民生银行
                        break;
                    case "1008":
                        code = "974"; //深圳发展银行
                        break;
                    case "1027":
                        code = "985"; //广东发展银行
                        break;
                    case "1021":
                        code = "962"; //中信银行
                        break;
                    case "1025":
                        code = "982"; //华夏银行
                        break;
                    case "1009":
                        code = "972"; //兴业银行
                        break;
                    case "1032":
                        code = "989"; //北京银行
                        break;
                    case "1022":
                        code = "986"; //中国光大银行
                        break;
                    case "1010":
                        code = "978"; //平安银行
                        break;
                    case "1024":
                        code = "975"; //上海银行
                        break;
                    case "1028":
                        code = "971"; //中国邮政储蓄银行
                        break;

                    case "ALIPAY":
                    case "1101":
                        code = "992"; //支付宝
                        break;

                    case "TENPAY":
                    case "1102":
                        code = "993"; //财付通
                        break;

                    case "ALIPAYDCODE":
                    case "1103":
                        code = "1003"; //支付宝二维码
                        break;
                    
                    case "WeXinPay":
                    case "1104":
                        code = "1004"; //
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

                    //case "988":
                    //    code = "CBHB"; //渤海银行
                    //    break;
                    //case "990":
                    //    code = "00056"; //北京农商银行
                    //    break;
                    //case "979":
                    //    code = "00055"; //南京银行
                    //break;

                    //case "987":
                    //    code = "BEA"; //东亚银行
                    //    break;
                    //case "1025":
                    //    code = "NBCB"; //宁波银行
                    //    break;
                    //case "983":
                    //    code = "00081"; //杭州银行
                    //    break;

                    //case "1028":
                    //    code = "HSB"; //徽商银行
                    //    break;
                    //case "968":
                    //    code = "00086"; //浙商银行
                    //    break;

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

        #region BankReceiveVerify

        /// <summary>
        /// 接收时 验证商户网银
        /// </summary>
        /// <param name="version"></param>
        /// <param name="key"></param>
        /// <param name="sign"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static bool BankReceiveVerify(string sign, params object[] arg)
        {
            bool result = false;
            string plain = string.Empty;
            string localsign = string.Empty;

            plain = string.Format(Vb7010BankReceiveVerifyStr, arg).ToLower() + string.Format("&keyvalue={0}", arg[3]);

            localsign = Cryptography.MD5(plain).ToLower();

            if (localsign == sign.ToLower())
            {
                result = true;
            }

            return result;
        }
        #endregion
    }
}
