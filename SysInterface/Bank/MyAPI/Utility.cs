using System;
using System.Globalization;
using System.Text;
using System.Web;
using viviapi.BLL.Channel;
using viviapi.BLL.User;
using viviapi.Model.Order;
using viviLib.ExceptionHandling;
using viviLib.Security;

namespace viviapi.SysInterface.Bank.MyAPI
{
    /// <summary>
    /// 
    /// </summary>
    public class Utility
    {
        public static string EnName = "vb1.0";
        public static string NotifySuccessflag = "opstate=0";
        public static string Successflag = "opstate=0";

        public static string ChineseName
        {
            get
            {
                if (BLL.WebInfoFactory.CurrentWebInfo != null)
                {
                    return BLL.WebInfoFactory.CurrentWebInfo.apibankname + "[" + BLL.WebInfoFactory.CurrentWebInfo.apibankversion + "]";
                }
                return string.Empty;
            }
        }
       

        #region CheckParameter
        /// <summary>
        /// 1 提交成功
        /// 7 数据非法
        /// </summary>
        /// <param name="bankinfo"></param>
        /// <returns></returns>
        public static string CheckParameter(BankInfo bankinfo)
        {
            string errCode = "";

            if (bankinfo == null)
            {
                return "-1";
            }

            errCode = CheckParamsEmpty(bankinfo);
            if (!string.IsNullOrEmpty(errCode))
                return "-1";

            errCode = CheckParamsLength(bankinfo);
            if (!string.IsNullOrEmpty(errCode))
                return "-1";

            errCode = CheckParamsFormat(bankinfo);
            if (!string.IsNullOrEmpty(errCode))
                return "-1";

            #region typeId
            int typeId = ChannelTypeId(bankinfo.type);
            if (typeId == 0)
            {
                bankinfo.ErrCode = "3000";
                bankinfo.Msg = "支付通道不存在";
                return "-1";
            }
            bankinfo.TypeId = typeId;
            #endregion

            #region userId
            int userId = 0;
            if (!int.TryParse(bankinfo.parter, out userId))
            {
                bankinfo.ErrCode = "3001";
                bankinfo.Msg = "商户ID[parter] 格式不正确";

                return "-1";
            }
            bankinfo.UserId = userId;
            #endregion

            #region Value
            decimal value = 0M;
            if (!decimal.TryParse(bankinfo.value, out value))
            {
                bankinfo.ErrCode = "3002";
                bankinfo.Msg = "订单金额[value] 格式不正确";
            }
            else if (value < Bank.Utility.MinTranAMT)
            {
                bankinfo.ErrCode = "3003";
                bankinfo.Msg ="订单金额（value）小于最小允许交易额！";
            }
            else if (value > Bank.Utility.MaxChargeAMT)
            {
                bankinfo.ErrCode = "3004";
                bankinfo.Msg = "订单金额（value）大于最大允许交易额！";
            }
            bankinfo.OrderAmt = value;
            #endregion

            #region userInfo
            var userInfo = BLL.User.Factory.GetCacheUserBaseInfo(userId);
            if (userInfo == null)
            {
                bankinfo.ErrCode = "3003";
                bankinfo.Msg = "商户不存在";
                return "-1";//
            }
            if (userInfo.Status != 2)
            {
                bankinfo.ErrCode = "3004";
                bankinfo.Msg = "商户状态不正常";
                return "-1";//
            }
            bankinfo.APIkey = userInfo.APIKey;
            bankinfo.ManageId = userInfo.manageId;
            #endregion

            if (!CheckSign(bankinfo, userInfo.APIKey))
            {
                bankinfo.ErrCode = "3005";
                bankinfo.Msg = "签名失败";
                return "-1";//
            }

            #region chanelInfo
            var chanelInfo = viviapi.BLL.Channel.Factory.GetModel(typeId, bankinfo.type, userId, true);
            if (chanelInfo == null)
            {
                bankinfo.Msg = bankinfo.type + "通道不存在";
                return "-1";//不支持该类卡或者该面值的卡
            }
            else if (chanelInfo.isOpen != 1)
            {
                bankinfo.Msg = bankinfo.type + "暂停此银行";
                return "-1";//业务状态不可用，未开通此类卡业务
            }
            else if (!chanelInfo.supplier.HasValue)
            {
                bankinfo.Msg = "未设置销卡接口";
                return "-1";//
            }
            bankinfo.ChanelNo = bankinfo.type;
            bankinfo.SupplierId = chanelInfo.supplier.Value;
            #endregion

        //    #region 数据库 检查
        //    var chkresult = BLL.Order.Card.Factory.Instance.CheckCardInfo(userId
        //, cardInfo.orderid
        //, typeId
        //, cardInfo.cardno
        //, cardInfo.cardpwd
        //, cardInfo.OrderAmt);

        //    if (chkresult == null)
        //    {
        //        cardInfo.Msg = "系统故障，服务器忙";
        //        return "13";
        //    }
        //    else
        //    {
        //        switch (chkresult.IsRepeat)
        //        {
        //            case 1:
        //                if (chkresult.Makeup == 1)
        //                {
        //                    cardInfo.SupplierId = chkresult.Supplierid;
        //                    cardInfo.ProcessMode = 2;//自身处理

        //                    #region 补单
        //                    if (String.Equals(chkresult.Cardpwd, cardInfo.CardPwd, StringComparison.CurrentCultureIgnoreCase))
        //                    {
        //                        if (chkresult.Isclose == 0)
        //                        {
        //                            #region 继续补充
        //                            int balance = decimal.ToInt32(chkresult.CardBalance);
        //                            if (balance > 0)
        //                            {
        //                                if (cardInfo.OrderAmt <= balance)
        //                                {
        //                                    cardInfo.ProcessMode = 2;//自身处理
        //                                }
        //                                else if (cardInfo.OrderAmt > balance)
        //                                {
        //                                    cardInfo.Msg = "卡内余额不足";
        //                                    return "10"; //卡余额不足
        //                                }
        //                            }
        //                            else
        //                            {
        //                                cardInfo.Msg = "充值卡无效";
        //                                return "10";//卡余额不足
        //                            }
        //                            #endregion
        //                        }
        //                        else
        //                        {
        //                            cardInfo.Msg = "充值卡无效";
        //                            return "10";//不可以继续 补充了
        //                        }
        //                    }
        //                    else
        //                    {
        //                        cardInfo.Msg = "卡密码不正确";
        //                        return "16";//卡密不对
        //                    }
        //                    #endregion
        //                }
        //                else
        //                {
        //                    cardInfo.ProcessMode = 1;//通过接口处理
        //                }
        //                break;
        //            case 4:
        //            case 5:
        //                if (String.Equals(chkresult.Cardpwd, cardInfo.CardPwd, StringComparison.CurrentCultureIgnoreCase))
        //                {
        //                    cardInfo.Msg = "订单内容重复";
        //                    return "4";
        //                }
        //                break;
        //            case 6:
        //                cardInfo.Msg = "订单号已经存在";
        //                return "6";
        //            case 7:
        //                cardInfo.Msg = "提交次数过多";
        //                return "20";
        //        }
        //    }
        //    #endregion

            return "";
        }
        #endregion

        #region CheckParamsEmpty
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bankinfo"></param>
        /// <returns></returns>
        public static string CheckParamsEmpty(BankInfo bankinfo)
        {
            #region Step1 必要的参数不能为空
            if (string.IsNullOrEmpty(bankinfo.parter))
            {
                bankinfo.ErrCode = "1001";
                bankinfo.Msg = "商户ID（parter）不能空！";
            }
            else if (string.IsNullOrEmpty(bankinfo.type))
            {
                bankinfo.ErrCode = "1002";
                bankinfo.Msg = "银行类型（type）不能空！";
            }
            else if (string.IsNullOrEmpty(bankinfo.value))
            {
                bankinfo.ErrCode = "1003";
                bankinfo.Msg = "订单金额（value）不能空！";
            }
            else if (string.IsNullOrEmpty(bankinfo.orderid))
            {
                bankinfo.ErrCode = "1004";
                bankinfo.Msg = "商户订单号（orderid）不能空！";
            }
            else if (string.IsNullOrEmpty(bankinfo.callbackurl))
            {
                bankinfo.ErrCode = "1005";
                bankinfo.Msg = "下行异步通知地址（callbackurl）不能空！";
            }
            /*else if (string.IsNullOrEmpty(version))
             {
                 error = "error:1006 版本号（version）不能空！";
             }*/
            else if (string.IsNullOrEmpty(bankinfo.sign))
            {
                bankinfo.ErrCode = "1006";
                bankinfo.Msg = "MD5签名（sign）不能空！";
            }
            #endregion

            return bankinfo.ErrCode;
        }
        #endregion

        #region CheckParamsLength
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bankinfo"></param>
        /// <returns></returns>
        public static string CheckParamsLength(BankInfo bankinfo)
        {

            #region Step2 检查参数长度
            if (bankinfo.parter.Length > 5)
            {
                bankinfo.ErrCode = "2000";
                bankinfo.Msg = "商户ID（parter）长度超过5位！";
            }
            else if (bankinfo.type.Length > 4)
            {
                bankinfo.ErrCode = "2001";
                bankinfo.Msg = "银行类型（type）长度超过4位！";
            }
            else if (bankinfo.orderid.Length > 30)
            {
                bankinfo.ErrCode = "2002";
                bankinfo.Msg = "商户订单号（orderid）长度超过30位！";
            }
            else if (bankinfo.value.Length > 8)
            {
                bankinfo.ErrCode = "2003";
                bankinfo.Msg = "订单金额（value）长度超过最长限制！";
            }
            else if (bankinfo.callbackurl.Length > 255)
            {
                bankinfo.ErrCode = "2004";
                bankinfo.Msg = "下行异步通知地址（callbackurl）长度超过255位！";
            }
            else if (bankinfo.hrefbackurl.Length > 255)
            {
                bankinfo.ErrCode = "2005";
                bankinfo.Msg = "下行同步通知地址（hrefbackurl）长度超过255位！";
            }
            else if (bankinfo.sign.Length != 32)
            {
                bankinfo.ErrCode = "2005";
                bankinfo.Msg = "签名（sign）长度不正确！";
            }
            #endregion
            return bankinfo.ErrCode;
        }
        #endregion

        #region CheckParamsLength
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bankinfo"></param>
        /// <returns></returns>
        public static string CheckParamsFormat(BankInfo bankinfo)
        {
            if (!viviLib.Text.Validate.IsNumeric(bankinfo.parter))
            {
                bankinfo.ErrCode = "3000";
                bankinfo.Msg = "商户ID（parter）格式不正确！";
            }
            else if (!viviLib.Text.Validate.IsNumeric(bankinfo.type))
            {
                bankinfo.ErrCode = "3001";
                bankinfo.Msg = "银行类型（type）格式不正确！";
            }
            else if (!IsNotifyUrlOk(bankinfo.callbackurl))
            {
                bankinfo.ErrCode = "3002";
                bankinfo.Msg = "下行异步通知地址（callbackurl）格式不正确！";
            }
            else if (!IsReturnUrlOk(bankinfo.hrefbackurl))
            {
                bankinfo.ErrCode = "3003";
                bankinfo.Msg = "下行同步通知地址（hrefbackurl）格式不正确！";
            }
            else if (!IsClientIpOk(bankinfo.payerIp))
            {
                bankinfo.ErrCode = "3004";
                bankinfo.Msg = "支付用户IP（payerIp）格式不正确！";
            }
            return bankinfo.ErrCode;
        }

        #region IsReturnUrlOk
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hrefbackurl"></param>
        /// <returns></returns>
        static bool IsReturnUrlOk(string hrefbackurl)
        {
            if (string.IsNullOrEmpty(hrefbackurl))
                return true;

            bool isUrl = viviLib.Text.Validate.IsUrl(hrefbackurl);
            if (isUrl)
            {
                return !hrefbackurl.Contains("?") && !hrefbackurl.Contains("&");
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callbackurl"></param>
        /// <returns></returns>
        static bool IsNotifyUrlOk(string callbackurl)
        {
            if (string.IsNullOrEmpty(callbackurl))
                return false;

            var isUrl = viviLib.Text.Validate.IsUrl(callbackurl);
            if (isUrl)
            {
                return !callbackurl.Contains("?") && !callbackurl.Contains("&");
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static bool IsClientIpOk(string payerIp)
        {
            if (string.IsNullOrEmpty(payerIp))
                return true;

            return viviLib.Text.Validate.IsIPSect(payerIp);
        }
        #endregion
        #endregion

        #region CheckSign
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bankInfo"></param>
        /// <param name="apikey"></param>
        /// <returns></returns>
        public static bool CheckSign(BankInfo bankInfo, string apikey)
        {
            try
            {
                string md5Str = string.Format("parter={0}&type={1}&value={2}&orderid={3}&callbackurl={4}{5}"
                    , bankInfo.parter
                    , bankInfo.type
                    , bankInfo.value
                    , bankInfo.orderid
                    , bankInfo.callbackurl
                    , apikey);

                md5Str = Cryptography.MD5(md5Str).ToLower();

                return md5Str == bankInfo.sign;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        #endregion


        static bool CheckUrlReferrer(int uid)
        {
            if (Bank.Utility.RequiredCheckUrlReferrer)
            {
                if (HttpContext.Current.Request.UrlReferrer == null)
                    return false;

                var hostBLL = new UserHost();
                return hostBLL.Exists(uid, HttpContext.Current.Request.UrlReferrer.Host);
            }

            return true;
        }

        #region 通道类型ID
        /// <summary>
        /// 
        /// </summary>
        public static int ChannelTypeId(string type)
        {
            int typeid = 102;
            switch (type)
            {
                case "1003"://支付宝
                case "992"://支付宝
                    typeid = 101;
                    break;

                case "993"://财付通
                    typeid = 100;
                    break;

                default:
                    typeid = 102;
                    break;
            }
            return typeid;
        }
        #endregion

        #region ReceiveVerify
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="bankid"></param>
        /// <param name="money"></param>
        /// <param name="orderid"></param>
        /// <param name="notifyUrl"></param>
        /// <param name="apikey"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public static bool ReceiveVerify(string userid, string bankid, string money, string orderid, string notifyUrl, string apikey, string sign)
        {
            string md5Str = string.Format("parter={0}&type={1}&value={2}&orderid={3}&callbackurl={4}{5}", userid, bankid, money, orderid, notifyUrl, apikey);
            md5Str = Cryptography.MD5(md5Str).ToLower();

            return md5Str == sign;
        }
        #endregion

        #region SignVerification
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="userid"></param>
        /// <param name="cardno"></param>
        /// <param name="cardpwd"></param>
        /// <param name="value"></param>
        /// <param name="orderid"></param>
        /// <param name="restrict"></param>
        /// <param name="callbackurl"></param>
        /// <param name="key"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public static bool SignVerification(string type
            , string userid
            , string cardno
            , string cardpwd
            , string value
            , string orderid
            , string restrict, string callbackurl, string key, string sign)
        {
            try
            {
                string plain = string.Empty;

                plain = string.Format("type={0}&parter={1}&cardno={2}&cardpwd={3}&value={4}&restrict={5}&orderid={6}&callbackurl={7}{8}"
                  , type
                  , userid
                  , cardno
                  , cardpwd
                  , value
                  , restrict
                  , orderid
                  , callbackurl
                  , key);

                string locationsign = viviLib.Security.Cryptography.MD5(plain).ToLower();
                if (locationsign == sign)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
                return false;
            }
        }
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
            parms.AppendFormat("&systime={0}", UrlEncode(systime));
            parms.AppendFormat("&attach={0}", UrlEncode(orderinfo.attach));
            parms.AppendFormat("&msg={0}", UrlEncode(orderinfo.msg));
            parms.AppendFormat("&sign={0}", UrlEncode(locationsign));

            notifyUrl = notifyUrl + "?" + parms.ToString();

            return notifyUrl;
        }
        #endregion

        #region UrlEncode
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramValue"></param>
        /// <returns></returns>
        public static string UrlEncode(string paramValue)
        {
            if (string.IsNullOrEmpty(paramValue))
                return string.Empty;
            return System.Web.HttpUtility.UrlEncode(paramValue, System.Text.Encoding.GetEncoding("gb2312"));
        }
        #endregion

        #region GetQueryString
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="strParaName"></param>
        /// <returns></returns>
        public static string GetQueryString(HttpContext context, string strParaName)
        {
            if (context == null)
                return "";

            string value = context.Request[strParaName];

            if (String.IsNullOrEmpty(value))
                return "";


            return HttpUtility.UrlDecode(value, Encoding.GetEncoding("GB2312"));
        }
        #endregion
    }
}
