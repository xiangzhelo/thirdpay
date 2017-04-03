using System.Globalization;
using System.Text;
using viviapi.Model.Order;
using viviLib.Security;

namespace viviapi.SysInterface.Bank
{
    /// <summary>
    /// 
    /// </summary>
    public class HuaQi
    {
        /// <summary>
        /// 花旗
        /// </summary>
        public static string Vbhq10 = "vhq1.00";
        public static string Vbhq10ApiName = "花旗支付-商户支付功能接口规范版本号1.0";
        public static string Vbhq10BankReceiveVerifyStr = "{0}|{1}|{2}|{3}|{4}|{5}|{6}";
        public static string Vbhq10NotifySuccessflag = "errCode=0";     


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

            string refervalue = decimal.Round(orderinfo.refervalue, 2).ToString(CultureInfo.InvariantCulture);
            string realvalue = "0";
            if (orderinfo.realvalue.HasValue)
                realvalue = decimal.Round(orderinfo.realvalue.Value, 2).ToString(CultureInfo.InvariantCulture);
          

            string errcode = "116";
            if (orderinfo.status == 2 || orderinfo.status == 8)
            {
                errcode = "0";
            }
            //P_UserId | P_OrderId | P_CardId | P_CardPass | P_FaceValue | P_ChannelId | SalfStr
            string plain = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}"
                , orderinfo.userid
                , orderinfo.userorder
                , string.Empty
                , string.Empty
                , refervalue
                , orderinfo.cus_field2
                , apiKey);

            string sign = Cryptography.MD5(plain);

            var parms = new StringBuilder();

            parms.AppendFormat("P_UserId={0}", orderinfo.userid);
            parms.AppendFormat("&P_OrderId={0}", orderinfo.userorder);
            parms.AppendFormat("&P_CardId={0}", string.Empty);
            parms.AppendFormat("&P_CardPass={0}", string.Empty);
            parms.AppendFormat("&P_FaceValue={0}", refervalue);
            parms.AppendFormat("&P_ChannelId={0}", orderinfo.cus_field2);//约定为充值类型
            parms.AppendFormat("&P_PayMoney={0}", realvalue);
            parms.AppendFormat("&P_Subject={0}", orderinfo.cus_subject);
            parms.AppendFormat("&P_Price={0}", orderinfo.cus_price);
            parms.AppendFormat("&P_Quantity={0}", orderinfo.cus_quantity);
            parms.AppendFormat("&P_Description={0}", orderinfo.cus_description);
            parms.AppendFormat("&P_Notic={0}", orderinfo.attach);
            parms.AppendFormat("&P_ErrCode={0}", errcode);
            parms.AppendFormat("&P_ErrMsg={0}", orderinfo.msg);
            parms.AppendFormat("&P_PostKey={0}", sign);

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

        #region
        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public static int ConvertChannelCode(string typeid)
        {
            int result = 0;
            switch (typeid)
            {
                case "1":
                    result = 102; //在线银行充值
                    break;
                case "2":
                    result = 101; //支付宝充值
                    break;
                case "3":
                    result = 100; //财付通充值
                    break;

                case "4":
                    result = 107; //腾讯QQ卡
                    break;
                case "5":
                    result = 104; //盛大卡
                    break;
                case "6":
                    result = 106; //骏网一卡通
                    break;
                case "7":
                    result = 111; //完美一卡通
                    break;
                case "8":
                    result = 112; //搜狐一卡通
                    break;
                case "9":
                    result = 105; //征途游戏卡
                    break;
                case "10":
                    result = 109; //久游一卡通
                    break;
                case "11":
                    result = 110; //网易一卡通
                    break;
                case "12":
                    result = 117; //魔兽卡 =》纵游一卡通
                    break;
                case "13":
                    result = 113; //电信充值卡
                    break;
                case "14":
                    result = 103; //神州行充值卡
                    break;
                case "15":
                    result = 108; //联通充值卡
                    break;
                case "16":
                    result = 116; //金山一卡通
                    break;
                case "17":
                    result = 115; //光宇一卡通
                    break;
                case "18":
                    result = 118; //5173卡=》天下一卡通
                    break;
                case "19":
                    result = 119; //热血卡=》天宏一卡通
                    break;
            }
            return result;
        }
        #endregion

        #region ReceiveVerify

        /// <summary>
        /// 接收时 验证商户网银
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static bool ReceiveVerify( string sign, params object[] arg)
        {
            bool result = false;
            string plain = string.Empty;
            string localsign = string.Empty;

            plain = string.Format(Vbhq10BankReceiveVerifyStr, arg);
            localsign = viviLib.Security.Cryptography.MD5(plain).ToLower();
            if (localsign == sign.ToLower())
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region
        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public static int CodeMapping(int typeid)
        {
            int result = 0;
            switch (typeid)
            {
                case 103:
                    result = 13; //神州行充值卡
                    break;
                case 104:
                    result = 2; //盛大一卡通
                    break;
                case 105:
                    result = 7; //征途支付卡
                    break;
                case 106:
                    result = 3; //骏网一卡通
                    break;
                case 107:
                    result = 1; //腾讯Q币卡
                    break;
                case 108:
                    result = 14; //联通充值卡
                    break;
                case 109:
                    result = 8; //久游一卡通
                    break;
                case 110:
                    result = 9; //网易一卡通
                    break;
                case 111:
                    result = 5; //完美一卡通
                    break;
                case 112:
                    result = 6; //搜狐一卡通
                    break;
                case 113:
                    result = 12; //电信充值卡
                    break;
                case 115:
                    result = 16; //电信充值卡
                    break;
                case 116:
                    result = 15; //金山一卡通
                    break;
                case 117:
                    result = 21; //纵游一卡通
                    break;
                case 118:
                    result = 22; //天下一卡通
                    break;
                case 119:
                    result = 23; //天宏一卡通
                    break;
                case 200:
                    result = 17; //神州行浙江卡
                    break;
                case 201:
                    result = 18; //神州行江苏卡
                    break;
                case 202:
                    result = 19; //神州行辽宁卡
                    break;
                case 203:
                    result = 20; //神州行福建卡
                    break;
                case 204:
                    result = 10; //魔兽卡
                    break;
                case 205:
                    result = 11; //联华卡
                    break;
                case 210:
                    result = 28; //盛付通卡
                    break;
            }
            return result;
        }
        #endregion
    }
}
