using System;
using System.Globalization;
using System.Net;
using System.Text;
using System.Web;
using viviapi.BLL;
using viviapi.BLL.Order.Bank;
using viviapi.BLL.Sys;
using viviapi.Model.Order;
using viviapi.SysConfig;
using viviLib.ExceptionHandling;

namespace viviapi.SysInterface.Bank
{
    /// <summary>
    /// 
    /// </summary>
    public class Utility
    {
        #region BankNoticeUrl
        /// <summary>
        /// 取通知地址
        /// </summary>
        /// <param name="orderinfo"></param>
        /// <param name="isNotify"></param>
        /// <returns></returns>
        public static string GetBankBackUrl(OrderBankInfo orderinfo, bool isNotify)
        {
            if (orderinfo == null)
                return string.Empty;

            string notifyUrl = isNotify ? orderinfo.notifyurl : orderinfo.returnurl;

            if (string.IsNullOrEmpty(notifyUrl))
                return string.Empty;

            var userinfo = BLL.User.Factory.GetCacheUserBaseInfo(orderinfo.userid);
            if (userinfo == null)
                return string.Empty;

            string ver = orderinfo.version;
            string apikey = userinfo.APIKey;

            if (ver == MyAPI.Utility.EnName)
            {
                notifyUrl = MyAPI.Utility.CreateNotifyUrl(orderinfo, isNotify, apikey);
            }
            else if (ver == Eka.VbYika)
            {
                notifyUrl = Eka.CreateNotifyUrl(orderinfo, isNotify, apikey);
            }
            else if (ver == Card70.Vb7010)
            {
                notifyUrl = Card70.CreateNotifyUrl(orderinfo, isNotify, apikey);
            }
            else if (ver == YeePay.VbYee10)
            {
                notifyUrl = YeePay.CreateNotifyUrl(orderinfo, isNotify, apikey);
            }
            else if (ver == HuaQi.Vbhq10)
            {
                notifyUrl = HuaQi.CreateNotifyUrl(orderinfo, isNotify, apikey);
            }
            return notifyUrl;
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
        public static bool BankReceiveVerify(string version, string sign, params object[] arg)
        {
            bool result = false;
            string plain = string.Empty;
            string localsign = string.Empty;

            if (version == Card70.Vb7010)
            {
                plain = string.Format(Card70.Vb7010BankNotifyVerifyStr, arg).ToLower() + string.Format("&keyvalue={0}", arg[3]);

                localsign = viviLib.Security.Cryptography.MD5(plain).ToLower();

                if (localsign == sign.ToLower())
                {
                    result = true;
                }
            }
            else if (version == HuaQi.Vbhq10)
            {
                plain = string.Format(HuaQi.Vbhq10BankReceiveVerifyStr, arg);
                localsign = viviLib.Security.Cryptography.MD5(plain).ToLower();
                if (localsign == sign.ToLower())
                {
                    result = true;
                }
            }
            return result;
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
            else if (version == Eka.VbYika)
            {
                flag = Eka.VbYikaNotifySuccessflag;
            }
            else if (version == Card70.Vb7010)
            {
                flag = Card70.Vb7010BankNotifySuccessflag;
            }
            else if (version == YeePay.VbYee10)
            {
                flag = YeePay.VbYee10BankNotifySuccessflag;
            }
            else if (version == HuaQi.Vbhq10)
            {
                flag = HuaQi.Vbhq10NotifySuccessflag;
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

        #region SynchronousNotify
        /// <summary>
        /// 同步补发
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <returns></returns>
        public static string SynchronousNotify(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
                return string.Empty;

            var orderInfo = Factory.Instance.GetModelByOrderId(orderId);
            if (orderInfo == null)
                return string.Empty;

            string successFlag = Successflag(orderInfo.version);

            string notifyUrl = GetBankBackUrl(orderInfo, true);

            string callback = string.Empty;
            string status = string.Empty;
            string message = string.Empty;
            string statusCode = string.Empty;
            string statusDesc = string.Empty;
            bool isOk = false;
            int notifystat = 4;
            try
            {
                if (viviLib.Text.PageValidate.IsUrl(notifyUrl))
                {

                    try
                    {
                        callback = viviLib.Web.WebClientHelper.GetString(notifyUrl
                        , string.Empty
                        , "GET"
                        , Encoding.GetEncoding("GB2312")
                        , 5*1000);

                        isOk = callback.StartsWith(successFlag) || callback.ToLower().StartsWith(successFlag);

                        if (isOk) notifystat = 2;
                    }
                    catch (WebException e)
                    {
                        message = e.Message;
                        status = e.Status.ToString();

                        if (e.Status == WebExceptionStatus.ProtocolError)
                        {
                            statusCode = ((HttpWebResponse)e.Response).StatusCode.ToString();
                            statusDesc = ((HttpWebResponse)e.Response).StatusDescription;
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.HandleException(ex);
                    }

                    if (orderInfo.notifystat != 2)
                    {
                        var notify = new Model.Order.Bank.BankNotify()
                        {
                            orderid = orderInfo.orderid,
                            status = status,
                            message = message,
                            httpStatusCode = statusCode,
                            StatusDescription = statusDesc,
                            againNotifyUrl = notifyUrl,
                            notifystat = notifystat,
                            notifycontext = callback,
                            notifytime = DateTime.Now
                        };

                        BLL.Order.Bank.BankNotify.Instance.Insert(notify);
                    }
                }
                else
                {
                    callback = "返回地址不正确！";
                }
            }
            catch (Exception ex)
            {

            }


            return callback;
        }
        #endregion

        #region AsynchronousNotify
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        public static void AsynchronousNotify(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
                return;

            var orderInfo = Factory.Instance.GetModelByOrderId(orderId);
            if (orderInfo == null || orderInfo.status == 1)
                return;

            string notifyUrl = GetBankBackUrl(orderInfo,true);

            Common.AsynchronousNotify(1, orderInfo, notifyUrl);
        }
        #endregion

        #region ReturnToMerchant
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderInfo"></param>
        public static void ReturnToMerchant(OrderBankInfo orderInfo)
        {
            string returnUrl = GetReturnUrl(orderInfo);

            HttpContext.Current.Response.Redirect(returnUrl, true);
        }

        public static string GetReturnUrl(OrderBankInfo orderInfo)
        {
            string returnUrl = GetBankBackUrl(orderInfo, false);
            if (string.IsNullOrEmpty(returnUrl))
            {
                var parms = new StringBuilder();
                parms.AppendFormat("o={0}", orderInfo.orderid);
                parms.AppendFormat("&uo={0}", orderInfo.userorder);
                parms.AppendFormat("&c={0}", orderInfo.paymodeId);
                parms.AppendFormat("&t={0}", orderInfo.typeId);
                parms.AppendFormat("&v={0:f2}", orderInfo.realvalue);
                parms.AppendFormat("&e={0}", orderInfo.msg);
                parms.AppendFormat("&u={0}", orderInfo.userid);
                parms.AppendFormat("&s={0}", orderInfo.status);

                returnUrl = RuntimeSetting.GatewayUrl + "/PayResult.aspx?" + parms.ToString();
            }

            return returnUrl;
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
            

            return result;
        }
        #endregion

        #region 系统配置
        //最大交易额
        /// <summary>
        /// 
        /// </summary>
        public static decimal MaxChargeAMT = TransactionSettings.MaxTranATM;
        //最小交易额
        /// <summary>
        /// 
        /// </summary>
        public static decimal MinTranAMT = TransactionSettings.MinTranATM;

        /// <summary>
        /// 是否需要验证来路
        /// </summary>
        public static bool RequiredCheckUrlReferrer
        {
            get
            {
                return TransactionSettings.CheckUrlReferrer;
            }
        }

        /// <summary>
        /// 是否需要验证订单号是否重复
        /// </summary>
        protected static bool RequiredCheckUserOrderNo
        {
            get
            {
                return TransactionSettings.CheckUserOrderNo;
            }
        }

        /// <summary>
        /// 订单号前缀
        /// </summary>
        public static string OrderPrefix
        {
            get
            {
                return TransactionSettings.OrderPrefix;
            }
        }

        /// <summary>
        /// 缓存超时时间
        /// </summary>
        public static int ExpiresTime
        {
            get
            {
                return TransactionSettings.ExpiresTime;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        protected static bool DebuglogOpen
        {
            get
            {
                return TransactionSettings.Debuglog;
            }
        }
        #endregion
    }
}
