using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Security;
using viviapi.ETAPI.Common;
using viviapi.Model.Order.Card;
using viviapi.Model.supplier;
using viviapi.SysConfig;
using viviLib.ExceptionHandling;
using viviLib.Web;

namespace viviapi.ETAPI.Card60866
{
    public class Card : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.Card60866;

        public Card()
            : base(SuppId)
        {

        }

        public static Card60866.Card Default
        {
            get
            {
                var card = new Card60866.Card();
                return card;
            }
        }

        public string NotifyUrl
        {
            get
            {
                return SiteDomain + "/receive/card60866/card.aspx";
            }
        }

        internal string Succflag = "ok";

        #region CardSend
        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public CardSynchCallBack CardSend(CardOrderSummitArgs o)
        {
            var callBack = new CardSynchCallBack();

            string puserid = this.SuppAccount;
            string puserkey = this.SuppKey;
            string commitUrl = this.PostCardUrl + "?";

            string cardno = o.CardNo;
            string cardpass = o.CardPass;
            string orderId = o.SysOrderNo;

            string cardtype = GetPaycardno(o.CardTypeId);
            string productid = GetCardType(o.CardTypeId, o.FaceValue);
            if (productid == "0")
            {
                productid = cardtype + o.FaceValue.ToString(CultureInfo.InvariantCulture);
            }

            string money = o.FaceValue.ToString(CultureInfo.InvariantCulture);

            string md5Str = MD5(string.Format("userid={0}&orderid={1}&typeid={2}&productid={3}&cardno={4}&cardpwd={5}&money={6}&url={7}&keyvalue={8}",
                puserid, orderId, cardtype, productid, cardno, cardpass, money, NotifyUrl, puserkey).ToLower());

            string postData = string.Format("userid={0}&orderid={1}&typeid={2}&productid={3}&cardno={4}&cardpwd={5}&money={6}&url={7}&sign={8}&ext={9}"
                , puserid, orderId, cardtype, productid, cardno, cardpass, money, NotifyUrl, md5Str, string.Empty);

            try
            {
                string callBackText = WebClientHelper.GetString(commitUrl.ToLower()
                    , postData
                    , "GET"
                    , Encoding.GetEncoding("GB2312")
                    , 5000);

                if (!string.IsNullOrEmpty(callBackText))
                {
                    string[] strings = callBackText.Split('&');

                    callBack.Success = 1;
                    callBack.SuppCallBackText = callBackText;

                    string returncode = strings[0].Replace("returncode=", string.Empty);
                    string returnorderid = strings[1].Replace("returnorderid=", string.Empty);
                    string sign = strings[2].Replace("sign=", string.Empty);

                    callBack.SuppErrorCode = returncode;
                    callBack.SuppTransNo = returnorderid;
                    callBack.SuppErrorMsg = GetMsgInfo(returncode);

                    if (MD5(string.Format("returncode={0}&returnorderid={1}&keyvalue={2}", returncode, returnorderid, puserkey).ToLower()) == sign)
                    {
                        if (!string.IsNullOrEmpty(returncode) && returncode == "1")
                        {
                            callBack.SummitStatus = 1;
                        }
                    }
                    else
                    {
                        callBack.Message = "签名失败";
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                callBack.Success = 0;
                callBack.Message = ex.Message;
            }

            return callBack;

        }
        #endregion

        #region CardNotify
        /// <summary>
        /// 
        /// </summary>
        public void CardNotify()
        {
            try
            {
                string returncode = HttpContext.Current.Request.QueryString["returncode"].ToString().Trim().ToLower();
                string userid = HttpContext.Current.Request.QueryString["userid"].ToString().Trim().ToLower();
                string orderid = HttpContext.Current.Request.QueryString["orderid"].ToString().Trim().ToLower();
                string typeid = HttpContext.Current.Request.QueryString["typeid"].ToString().Trim().ToLower();
                string productid = HttpContext.Current.Request.QueryString["productid"].ToString().Trim().ToLower();
                string cardno = HttpContext.Current.Request.QueryString["cardno"].ToString().Trim().ToLower();
                string cardpwd = HttpContext.Current.Request.QueryString["cardpwd"].ToString().Trim().ToLower();
                string money = HttpContext.Current.Request.QueryString["money"].ToString().Trim().ToLower();
                string realmoney = HttpContext.Current.Request.QueryString["realmoney"].ToString().Trim().ToLower();
                string cardstatus = HttpContext.Current.Request.QueryString["cardstatus"].ToString().Trim().ToLower();
                string sign = HttpContext.Current.Request.QueryString["sign"].ToString().Trim().ToLower();
                string ext = HttpContext.Current.Request.QueryString["ext"].ToString().Trim().ToLower();
                string errtype = HttpContext.Current.Request.QueryString["errtype"].ToString().Trim();
                string keyvalue = this.SuppKey;

                string localunsign = string.Format("returncode={0}&userid={1}&orderid={2}&typeid={3}&productid={4}&cardno={5}&cardpwd={6}&money={7}&realmoney={8}&cardstatus={9}&keyvalue={10}"
                   , returncode
                   , userid
                   , orderid
                   , typeid
                   , productid
                   , cardno
                   , cardpwd
                   , money
                   , realmoney
                   , cardstatus
                   , keyvalue
                   );

                string localsign = MD5(localunsign);//, "utf-8"
                if (localsign == sign)
                {
                    string opstate = "-1";
                    if (returncode == "1")
                        opstate = "0";
                    else
                    {
                        opstate = ConvertCode(opstate);
                    }

                    int status = (returncode == "1") ? 2 : 4;

                    var response = new CardOrderSupplierResponse()
                    {
                        SupplierId = SuppId,
                        SuppTransNo = "",
                        SysOrderNo = orderid,
                        OrderAmt = decimal.Parse(realmoney),
                        SuppAmt = 0M,
                        OrderStatus = status,
                        SuppErrorCode = errtype,
                        Opstate = opstate,
                        SuppErrorMsg = GetMsgInfo2(errtype),
                        ViewMsg = GetMsgInfo2(errtype),
                        Method = 1
                    };

                    OrderCardUtils.SuppNotify(response, Succflag);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }
        #endregion

        #region Query
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public string Query(string orderid)
        {
            string callback = string.Empty;
            try
            {
                var parms = new StringBuilder();

                parms.AppendFormat("userid={0}", SuppAccount);
                parms.AppendFormat("&orderid={0}", orderid);

                string plain = parms.ToString() + string.Format("&keyvalue={0}", this.SuppKey);
                string md5sign = viviLib.Security.Cryptography.MD5(plain).ToLower();

                parms.AppendFormat("&sign={0}", md5sign);


                HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("gb2312");

                callback = viviLib.Web.WebClientHelper.GetString("http://tong.60866.com/query.aspx?" + parms.ToString()
                   , null
                   , "GET"
                   , System.Text.Encoding.GetEncoding("utf-8")
                   , 10000);
            }
            catch (Exception ex)
            {
                callback = ex.Message;
            }

            return callback;
        }
        #endregion

        #region Finish
        /// <summary>
        /// returncode=1
        /// &orderid=14032617162212060589
        /// &realmoney=15.0000
        /// &cardstatus=1
        /// &sign=3eedb92c7cb6bedd6aab483dc35375db
        /// &errtype=
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public bool Finish(string orderid, string callback)
        {
            bool result = false;

            try
            {
                if (!string.IsNullOrEmpty(callback))
                {
                    string opstate = "-1";
                    int status = 4;

                    string returncode = GetVal(callback, "returncode");
                    string realmoney = GetVal(callback, "realmoney");
                    string message = GetVal(callback, "errtype");

                    if (message == "成功")
                        message = "支付成功";

                    if (returncode == "1")
                    {
                        opstate = "0";
                        status = 2;
                    }
                    else if (returncode == "8" || returncode == "-1" || returncode == "2") //“2”代表售卡处理中
                    {
                        opstate = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(opstate))
                    {
                        decimal amount = 0M;
                        if (decimal.TryParse(realmoney, out amount))
                        {
                            var response = new CardOrderSupplierResponse()
                            {
                                SupplierId = SuppId,
                                SuppTransNo = "",
                                SysOrderNo = orderid,
                                OrderAmt = amount,
                                SuppAmt = 0M,
                                OrderStatus = status,
                                SuppErrorCode = returncode,
                                Opstate = opstate,
                                SuppErrorMsg = message,
                                ViewMsg = message,
                                Method = 1
                            };
                            OrderCardUtils.Finish(response);

                            result = true;
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
            }

            return result;
        }

        string GetVal(string retCode, string key)
        {
            string[] list = retCode.Split('&');

            foreach (string item in list)
            {
                string[] arr = item.Split('=');

                if (arr[0] == key)
                    return arr[1];
            }

            return string.Empty;
        }
        #endregion

        #region GetPaycardno
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetPaycardno(int type)
        {
            string str = string.Empty;
            switch (type)
            {
                case 103:
                    return "cm";
                case 104:
                case 210:
                    return "sd";
                case 105:
                    return "zt";
                case 106:
                    return "jw";
                case 107:
                    return "qq";
                case 108:
                    return "cc";
                case 109:
                    return "jy";
                case 110:
                    return "wy";
                case 111:
                    return "wm";
                case 112:
                    return "sh";
                case 113:
                    return "dx";

                case 0x407:
                    return "cd";
                case 115:
                    return "gy";//光宇一卡通
                case 117:
                    return "zy";//纵游一卡通
                case 118:
                    return "tx";//天下一卡通
                case 119:
                    return "th";//天宏一卡通
            }
            return str;
        }
        #endregion

        #region GetCardType
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paytype"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public string GetCardType(int paytype, int money)
        {
            string str = "0";
            switch (paytype)
            {
                case 103:
                    switch (money)
                    {
                        #region 神州行充值卡
                        case 10:
                            return "cm10";
                        case 20:
                            return "cm20";
                        case 30:
                            return "cm30";
                        case 50:
                            return "cm50";
                        case 100:
                            return "cm100";
                        case 300:
                            return "cm300";
                        case 500:
                            return "cm500";
                        #endregion
                    }
                    break;
                case 104:
                case 210:
                    switch (money)
                    {
                        #region 盛大一卡通
                        case 5:
                            return "sd5";
                        case 10:
                            return "sd10";
                        case 25:
                            return "sd25";
                        case 30:
                            return "sd30";
                        case 35:
                            return "sd35";
                        case 45:
                            return "sd45";
                        case 50:
                            return "sd50";
                        case 100:
                            return "sd100";
                        #endregion
                    }
                    break;
                case 105:
                    switch (money)
                    {
                        #region 征途支付卡
                        case 10:
                            return "zt10";
                        case 20:
                            return "zt20";
                        case 30:
                            return "zt30";
                        case 50:
                            return "zt50";
                        case 60:
                            return "zt60";
                        case 100:
                            return "zt100";
                        case 300:
                            return "zt300";
                        #endregion
                    }
                    break;
                case 106:
                    switch (money)
                    {
                        #region 骏网一卡通
                        case 4:
                            return "jw4";
                        case 5:
                            return "jw5";
                        case 6:
                            return "jw6";
                        case 10:
                            return "jw10";
                        case 15:
                            return "jw15";
                        case 30:
                            return "jw30";
                        case 50:
                            return "jw50";
                        case 100:
                            return "jw100";
                        #endregion
                    }
                    break;
                case 107:
                    switch (money)
                    {
                        #region 腾讯Q币卡
                        case 5:
                            return "qq5";
                        case 10:
                            return "qq10";
                        case 15:
                            return "qq15";
                        case 30:
                            return "qq30";
                        case 60:
                            return "qq60";
                        case 100:
                            return "qq100";
                        #endregion
                    }
                    break;
                case 108:
                    switch (money)
                    {
                        #region 联通充值卡
                        case 20:
                            return "cc20";
                        case 30:
                            return "cc30";
                        case 50:
                            return "cc50";
                        case 100:
                            return "cc100";
                        case 300:
                            return "cc300";
                        case 500:
                            return "cc500";
                        #endregion
                    }
                    break;
                case 109:
                    switch (money)
                    {
                        #region 久游一卡通
                        case 5:
                            return "jy5";
                        case 10:
                            return "jy10";
                        case 30:
                            return "jy30";
                        case 50:
                            return "jy50";
                        #endregion
                    }
                    break;
                case 110:
                    switch (money)
                    {
                        #region 网易一卡通
                        case 10:
                            return "wy10";
                        case 15:
                            return "wy15";
                        case 30:
                            return "wy30";
                        #endregion
                    }
                    break;
                case 111:
                    switch (money)
                    {
                        #region 完美一卡通
                        case 15:
                            return "wm15";
                        case 30:
                            return "wm30";
                        case 50:
                            return "wm50";
                        case 100:
                            return "wm100";
                        #endregion
                    }
                    break;
                case 112:
                    switch (money)
                    {
                        #region 搜狐一卡通
                        case 5:
                            return "sh5";
                        case 10:
                            return "sh10";
                        case 15:
                            return "sh15";
                        case 30:
                            return "sh30";
                        case 40:
                            return "sh40";
                        case 100:
                            return "sh100";
                        #endregion
                    }
                    break;
                case 113:
                    switch (money)
                    {
                        case 50:
                            return "dx50";
                        case 100:
                            return "dx100";
                    }
                    break;
                case 115:
                    switch (money)//10,20,30,50,100
                    {
                        #region 光宇一卡通
                        case 10:
                            return "gy10";
                        case 20:
                            return "gy15";
                        case 30:
                            return "gy30";
                        case 50:
                            return "gy50";
                        case 100:
                            return "gy100";
                        #endregion
                    }
                    break;
                case 117:
                    switch (money)//10,15,30,50,100
                    {
                        #region 纵游一卡通
                        case 10:
                            return "zy10";
                        case 15:
                            return "zy15";
                        case 30:
                            return "zy30";
                        case 50:
                            return "zy50";
                        case 100:
                            return "zy100";
                        #endregion
                    }
                    break;
                case 118:
                    switch (money)//10,20,30,40,50,60,70,80,90,100
                    {
                        #region 天下一卡通
                        case 10:
                            return "tx10";
                        case 20:
                            return "tx15";
                        case 30:
                            return "tx30";
                        case 40:
                            return "tx30";
                        case 50:
                            return "tx50";
                        case 60:
                            return "tx50";
                        case 70:
                            return "tx50";
                        case 80:
                            return "tx50";
                        case 90:
                            return "tx50";
                        case 100:
                            return "tx100";
                        #endregion
                    }
                    break;
                case 119:
                    switch (money)//5,10,15,30,50,100
                    {
                        #region 天宏一卡通
                        case 5:
                            return "th10";
                        case 10:
                            return "th15";
                        case 15:
                            return "th30";
                        case 30:
                            return "th50";
                        case 50:
                            return "th100";
                        case 100:
                            return "th100";
                        #endregion
                    }
                    break;
                default:
                    return str;
            }
            return "0";
        }
        #endregion

        #region GetMsgInfo
        /// <summary>
        /// 
        /// </summary>
        /// <param name="returncode"></param>
        /// <returns></returns>
        public string GetMsgInfo(string returncode)
        {
            switch (returncode)
            {
                case "5":
                    return "ip验证错误";
                case "6":
                    return "ip验证错误";
                case "7":
                    return "无效的商户号";
                case "8":
                    return "无效的商户号";
                case "9":
                    return "无效的产品类型ID";
                case "10":
                    return "无效的产品类型ID";
                case "11":
                    return "无效的卡";
                case "12":
                    return "系统错误";
                case "13":
                    return "产品维护";
                case "14":
                    return "卡号以处理成功或正在处理中";
                case "15":
                    return "卡号提交次数过多";
                case "16":
                    return "卡密已提交失败";

                case "1001":
                    return "卡号或密码错误";
                case "1002":
                    return "卡号过期";
                case "1003":
                    return "卡余额不足";
                case "1004":
                    return "卡号不存在";
                case "1005":
                    return "卡已使用过";
                case "1006":
                    return "卡号被冻结";
                case "1007":
                    return "卡未激活";
                case "1008":
                    return "不支持的卡类型或金额";
                case "1009":
                    return "其他游戏专用卡";
            }
            return returncode;
        }
        #endregion

        #region GetMsgInfo2
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardstatus"></param>
        /// <returns></returns>
        public string GetMsgInfo2(string cardstatus)
        {
            switch (cardstatus)
            {
                case "0":
                    return "支付成功";
                case "5":
                    return "ip验证错误";
                case "6":
                    return "ip验证错误";
                case "7":
                    return "无效的商户号";
                case "8":
                    return "无效的商户号";
                case "9":
                    return "无效的产品类型ID";
                case "10":
                    return "无效的产品类型ID";
                case "11":
                    return "无效的卡";
                case "12":
                    return "系统错误";
                case "13":
                    return "产品维护";
                case "14":
                    return "卡号以处理成功或正在处理中";
                case "15":
                    return "卡号提交次数过多";
                case "16":
                    return "卡密已提交失败";

                case "1001":
                    return "卡号或密码错误";
                case "1002":
                    return "卡号过期";
                case "1003":
                    return "卡余额不足";
                case "1004":
                    return "卡号不存在";
                case "1005":
                    return "卡已使用过";
                case "1006":
                    return "卡号被冻结";
                case "1007":
                    return "卡未激活";
                case "1008":
                    return "不支持的卡类型或金额";
                case "1009":
                    return "其他游戏专用卡";
            }
            return cardstatus;
        }
        #endregion

        #region ConvertCode
        /// <summary>
        /// 
        /// </summary>
        /// <param name="suppcode"></param>
        /// <returns></returns>
        public string ConvertCode(string suppcode)
        {
            string syscode = string.Empty;
            if (suppcode == "1001")
            {
                syscode = "-1";//卡号或密码错误
            }
            else if (suppcode == "1002")
            {
                syscode = "-2";//卡号过期
            }
            else if (suppcode == "1003")
            {
                syscode = "-10";//卡余额不足
            }
            else if (suppcode == "1004")
            {
                syscode = "-4";//卡号不存在
            }
            else if (suppcode == "1005")
            {
                syscode = "-5";//卡已使用过
            }
            else if (suppcode == "1006")
            {
                syscode = "-6";//卡号被冻结
            }
            else if (suppcode == "1007")
            {
                syscode = "-7";//卡未激活
            }
            else if (suppcode == "1008")
            {
                syscode = "-8";//不支持的卡类型或金额
            }
            else if (suppcode == "1009")
            {
                syscode = "-9";//其他游戏专用卡
            }
            if (string.IsNullOrEmpty(suppcode))
            {
                syscode = "-1";
            }
            return syscode;
        }
        #endregion

        #region Funtions
        public string MD5(string data)
        {
            var hashPasswordForStoringInConfigFile = FormsAuthentication.HashPasswordForStoringInConfigFile(data, "md5");
            if (hashPasswordForStoringInConfigFile != null)
                return hashPasswordForStoringInConfigFile.ToLower();

            return "";
        }
        public static string SendRequest(string url, string parames, string method)
        {
            string str = "";
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }
            if (string.IsNullOrEmpty(method))
            {
                method = "GET";
            }

            if (method.ToUpper() == "GET")
            {
                try
                {
                    WebRequest request = WebRequest.Create(url + parames);
                    request.Method = "GET";
                    str = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.GetEncoding("gb2312")).ReadToEnd();
                }
                catch (Exception exception)
                {
                    return exception.Message;
                }
            }
            return str;
        }
        #endregion

    }
}
