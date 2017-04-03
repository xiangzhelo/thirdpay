using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using viviapi.BLL;
using viviapi.BLL.Order.Card;
using viviapi.Model.Order;
using viviapi.Model.User;
using viviLib.ExceptionHandling;
using viviLib.Text;

namespace viviapi.SysInterface.Card
{
    /// <summary>
    /// 
    /// </summary>
    public class Utility
    {
        #region GetCardNotifyUrl
        /// <summary>
        /// 取通知地址
        /// </summary>
        /// <param name="orderinfo"></param>
        /// <returns></returns>
        public static string GetCardNotifyUrl(OrderCardInfo orderinfo)
        {
            if (orderinfo == null)
                return string.Empty;

            string notifyUrl = orderinfo.notifyurl;

            if (string.IsNullOrEmpty(notifyUrl))
                return string.Empty;

            var userinfo = BLL.User.Factory.GetCacheUserBaseInfo(orderinfo.userid);
            if (userinfo == null)
                return string.Empty;

            string ver = orderinfo.version;
            string apikey = userinfo.APIKey;

            if (ver == MyAPI.Utility.EnName)
            {
                notifyUrl = MyAPI.Utility.CreateNotifyUrl(orderinfo, apikey);
            }
            else if (ver == Eka.Utility.EnName)
            {
                notifyUrl = Eka.Utility.CreateNotifyUrl(orderinfo, apikey);
            }
            else if (ver == Card70.Utility.EnName)
            {
                notifyUrl = Card70.Utility.CreateNotifyUrl(orderinfo, apikey);
            }
            else if (ver == YeePay.AnnulCard.EnName)
            {
                notifyUrl = YeePay.AnnulCard.CreateNotifyUrl(orderinfo, apikey);
            }
            else if (ver == YeePay.ChargeCardDirect.EnName)
            {
                notifyUrl = YeePay.ChargeCardDirect.CreateNotifyUrl(orderinfo, apikey);
            }
            return notifyUrl;
        }
        #endregion

        #region GetCardNotifyUrl
        /// <summary>
        /// 取通知地址
        /// </summary>
        /// <param name="orderinfo"></param>
        /// <returns></returns>
        public static string GetMultiCardNotifyUrl(viviapi.Model.Order.Card.OrderCardTotal orderinfo)
        {
            if (orderinfo == null)
                return string.Empty;

            if (orderinfo.status == 1)
                return string.Empty;

            string notifyUrl = orderinfo.notifyUrl;

            if (string.IsNullOrEmpty(notifyUrl))
                return string.Empty;

            var userinfo = BLL.User.Factory.GetCacheUserBaseInfo(orderinfo.userId);
            if (userinfo == null)
                return string.Empty;

            string ver = orderinfo.version;
            string apikey = userinfo.APIKey;

            if (ver == YeePay.ChargeCardDirect.EnName)
            {
                notifyUrl = YeePay.ChargeCardDirect.CreateMultiNotifyUrl(orderinfo, apikey);
            }

            return notifyUrl;
        }
        #endregion

        #region Successflag
        /// <summary>
        /// 成功返回标志
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public static string Successflag(string version)
        {
            string flag = string.Empty;

            if (version == MyAPI.Utility.EnName)
            {
                flag = MyAPI.Utility.NotifySuccessflag;
            }
            else if (version == Eka.Utility.EnName)
            {
                flag = Eka.Utility.NotifySuccessflag;
            }
            else if (version == Card70.Utility.EnName)
            {
                flag = Card70.Utility.NotifySuccessflag;
            }
            else if (version == YeePay.AnnulCard.EnName)
            {
                flag = YeePay.AnnulCard.NotifySuccessflag;
            }
            else if (version == YeePay.ChargeCardDirect.EnName)
            {
                flag = YeePay.ChargeCardDirect.NotifySuccessflag;
            }
            return flag;
        }
        #endregion

        #region CheckCallBackIsSuccess
        /// <summary>
        /// 
        /// </summary>
        /// <param name="version"></param>
        /// <param name="callbackText"></param>
        /// <returns></returns>
        public static bool CheckCallBackIsSuccess(string version, string callbackText)
        {
            bool result = false;

            if (string.IsNullOrEmpty(callbackText))
            {
                return false;
            }

            string successFlag = Successflag(version);

            if (callbackText.StartsWith(successFlag, true, CultureInfo.CurrentCulture))
            {
                result = true;
            }

            return result;
        }
        #endregion

        #region CheckFormat
        /// <summary>
        /// 检查卡号及卡密的格式
        /// 必须是字母加数字的组合长度的8-16
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static bool CheckFormat(string inputValue)
        {
            return Validate.QuickValidate("^[0-9A-Za-z]{4,32}", inputValue);
        }
        #endregion

        #region GetVersionName
        /// <summary>
        /// 
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public static string GetVersionName(string version)
        {
            string result = string.Empty;
            if (version == MyAPI.Utility.EnName)
            {
                result = MyAPI.Utility.ChineseName;
            }
            if (version == Card70.Utility.EnName)
            {
                result = Card70.Utility.ChineseName;
            }

            return result;
        }
        #endregion

        #region CodeMapping
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

        #region IsShengFuTong
        /// <summary>
        /// 
        /// 是否为盛付通卡
        /// 80133+YA YB YC YD
        /// </summary>
        /// <param name="cardno"></param>
        /// <returns></returns>
        public static bool IsShengFuTong(string cardno)
        {
            if (string.IsNullOrEmpty(cardno))
            {
                return false;
            }

            const string pattern = "^(8013|YA|YB)";//|YC|YD

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);

            return regex.IsMatch(cardno);
        }
        #endregion

        #region 卡类格式检查
        /// <summary>
        /// 必须是字母加数字的组合长度的8-16
        /// </summary>
        /// <param name="cardno"></param>
        /// <returns></returns>
        public static bool VerifyCardNoFormat(string cardno)
        {
            return Validate.QuickValidate("^[0-9A-Za-z]{6,600}", cardno);
        }
        #endregion

        #region 卡类格式检查
        /// <summary>
        /// 必须是字母加数字的组合长度的8-16
        /// </summary>
        /// <param name="cardno"></param>
        /// <returns></returns>
        public static bool VerifyCardPwdFormat(string cardno)
        {
            return Validate.QuickValidate("^[0-9A-Za-z]{6,600}", cardno);
        }
        #endregion
    }
}
