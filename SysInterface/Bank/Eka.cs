using System.Globalization;
using System.Text;
using System.Web;
using viviapi.Model.Order;
using viviLib.Security;

namespace viviapi.SysInterface.Bank
{
    public class Eka
    {
        public static string VbYika = "vbeka";
        public static string VbYikaNotifySuccessflag = "opstate=0";

        public static bool ReceiveVerify(string userid, string bankid, string money, string orderid, string notify_url, string key, string sign)
        {
            string md5Str = string.Format("parter={0}&type={1}&value={2}&orderid={3}&callbackurl={4}{5}", userid, bankid, money, orderid, notify_url, key);
            md5Str = viviLib.Security.Cryptography.MD5(md5Str).ToLower();
            if (md5Str == sign)
            {
                return true;
            }
            return false;
        }

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
            if (string.IsNullOrEmpty(opstate))
            {
                opstate = orderinfo.status == 2 ? "0" : "-1";
            }

            string systime = "";
            if (orderinfo.completetime.HasValue)
                systime = orderinfo.completetime.Value.ToString("yyyy/MM/dd HH:mm:ss");

            string ovalue = "0";

            if (orderinfo.realvalue.HasValue)
                ovalue = decimal.Round(orderinfo.realvalue.Value, 2).ToString(CultureInfo.InvariantCulture);

            string plain = string.Format("orderid={0}&opstate={1}&ovalue={2}{3}"
                            , userorder
                            , opstate
                            , ovalue
                            , apiKey);
            string locationsign = Cryptography.MD5(plain);

            var parms = new StringBuilder();
            parms.AppendFormat("orderid={0}", UrlEncode(userorder));
            parms.AppendFormat("&opstate={0}", UrlEncode(opstate));
            parms.AppendFormat("&ovalue={0}", UrlEncode(ovalue));
            parms.AppendFormat("&sysorderid={0}", UrlEncode(orderinfo.orderid));
            parms.AppendFormat("&ekaorderid={0}", HttpUtility.UrlEncode(orderinfo.orderid));
            parms.AppendFormat("&ekatime={0}", HttpUtility.UrlEncode(systime));
            parms.AppendFormat("&attach={0}", UrlEncode(orderinfo.attach));
            parms.AppendFormat("&msg={0}", UrlEncode(orderinfo.msg));
            parms.AppendFormat("&sign={0}", UrlEncode(locationsign));

            notifyUrl = notifyUrl + "?" + parms.ToString();

            return notifyUrl;
        }
        #endregion

        public static string UrlEncode(string paramValue)
        {
            if (string.IsNullOrEmpty(paramValue))
                return string.Empty;

            return HttpUtility.UrlEncode(paramValue, Encoding.GetEncoding("gb2312"));
        }     
    }
}
