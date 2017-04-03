using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.Security.Cryptography;
using System.Xml;
using viviapi.ETAPI.Common;
using viviapi.Model.Order.Card;
using viviapi.Model.Payment;
using viviapi.BLL.Payment;
using viviapi.Model.Order;
using viviapi.SysConfig;
using viviapi.Model;
using viviLib.ExceptionHandling;
using viviLib.Web;
using viviLib.Logging;
using CardOrderSummitArgs = viviapi.BLL.CardOrderSummitArgs;

////
namespace viviapi.ETAPI.Kamen
{
    /// <summary>
    /// 汇速
    /// </summary>
    public class Card : ETAPIBase
    {
        private const int SuppId = (int)Model.SupplierCode.Kamen;

        public Card()
            : base(SuppId)
        {

        }

        public static Card Default
        {
            get
            {
                var card = new Card();
                return card;
            }
        }

        public string NotifyUrl = RuntimeSetting.SiteDomain + "/notify/kamenwang/card.aspx";
        public const string Succflag = "ok";

        protected bool islog = true;

        private void log(string str)
        {
            if (islog)
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"LogFiles\payLog\Kamen.log");

                LogHelper.Write(path, str);
            }
        }

        #region CardSend
        /// <summary>
        /// http://cscapi.kamenwang.com/consignsalecard/api
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public CardSynchCallBack CardSend(CardOrderSummitArgs o)
        {
            var callBack = new CardSynchCallBack();

            string stockid = GetCardType(o.CardTypeId);
            if (string.IsNullOrEmpty(stockid))
            {
                callBack.Success = 0;
                callBack.Message = "不支持此类型";

                return callBack;
            }

            string method = "supply";
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string format = "xml";
            string userid = suppAccount;
            string v = "1.0";
            string sign = "";


            string parvalue = o.FaceValue.ToString(CultureInfo.InvariantCulture);

            string card = o.CardNo;
            string pwd = o.CardPass;
            string customerorderid = o.SysOrderNo.ToLower();
            string from = "api";
            string notifyurl = NotifyUrl;


            string plain = string.Format("card={0}customerorderid={1}format={2}from={3}method={4}notifyurl={12}parvalue={5}pwd={6}stockid={7}timestamp={8}userid={9}v={10}{11}"
                , card
                , customerorderid
                , format
                , from
                , method
                , parvalue
                , pwd
                , stockid
                , timestamp
                , userid
                , v
                , suppKey
                , notifyurl);

            log("CardSend plain" + plain);

            sign = md5sign(plain).ToLower();

            log("CardSend sign" + sign);

            try
            {
                string postUrl = this.postCardUrl +
                                 string.Format(
                                     "?card={0}&customerorderid={1}&format={2}&from={3}&method={4}&notifyurl={12}&parvalue={5}&pwd={6}&stockid={7}&timestamp={8}&userid={9}&v={10}&sign={11}"
                                     , card
                                     , customerorderid
                                     , format
                                     , from
                                     , method
                                     , parvalue
                                     , pwd
                                     , stockid
                                     , timestamp
                                     , userid
                                     , v
                                     , sign
                                     , HttpUtility.UrlEncode(notifyurl));

                log("CardSend postUrl" + postUrl);

                string callBackText = WebClientHelper.GetString(postUrl, null, "post", Encoding.UTF8);

                log("CardSend callBackText" + callBackText);

                callBack.Success = 1;
                callBack.SuppCallBackText = callBackText;

                if (!string.IsNullOrEmpty(callBackText))
                {
                    var xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(callBackText);

                    var xmlNodeList = xmlDocument.SelectSingleNode("Root/Ret");
                    if (xmlNodeList != null)
                    {
                        foreach (XmlNode list in xmlNodeList)
                        {
                            switch (list.Name.ToLower())
                            {
                                case "retcode":
                                    callBack.SuppErrorCode = list.InnerText;
                                    break;
                                case "retmsg":
                                    callBack.SuppErrorMsg = list.InnerText;
                                    break;
                            }
                        }
                    }
                    xmlNodeList = xmlDocument.SelectSingleNode("Root/Order");
                    if (xmlNodeList != null)
                    {
                        foreach (XmlNode list in xmlNodeList)
                        {
                            switch (list.Name.ToLower())
                            {
                                case "orderid":
                                    callBack.SuppTransNo = list.InnerText;
                                    break;
                            }
                        }
                    }

                    if (callBack.SuppErrorCode == "20005")
                    {
                        callBack.SummitStatus = 1;
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

        private string GetValue(string value)
        {
            string[] arr = value.Split('=');

            if (arr.Length >= 2)
                return arr[1];

            return value;
        }

        #endregion

        #region Query
        /// <summary>
        /// http://cscapi.kamenwang.com/consignsalecard/api
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public string Query(string orderid)
        {
            string method = "search";
            string timeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string format = "xml";
            string userid = suppAccount;
            string v = "1.0";

            string sign = "";
            string customerorderid = orderid;

            string plain = string.Format("customerorderid={0}format={1}method={2}timestamp={3}userid={4}v={5}{6}"
                , customerorderid
                , format
                , method
                , timeStamp
                , userid
                , v
                , suppKey
                );

            sign = md5sign(plain).ToLower();
            string responeText;
            try
            {
                string postUrl = "http://cscapi.kamenwang.com/consignsalecard/api" + string.Format("?customerorderid={0}&format={1}&method={2}&timestamp={3}&userid={4}&v={5}&sign={7}"
                , customerorderid
                , format
                , method
                , timeStamp
                , userid
                , v
                , sign
                );

                responeText = WebClientHelper.GetString(postUrl, null, "POST", Encoding.UTF8);

            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                responeText = ex.Message;
            }
            return responeText;
        }
        #endregion

        #region Notify
        /// <summary>
        /// 
        /// </summary>
        public void Notify()
        {
            try
            {
                int status = 0;
                string msg = "";
                string viewMsg = "";
                string opstate = "";
                decimal orderAmt = 0M;

                string canresubmit = HttpContext.Current.Request.Form["canresubmit"];
                string customerorderid = HttpContext.Current.Request.Form["customerorderid"];
                string remark = HttpContext.Current.Request.Form["remark"];
                string settleparvalue = HttpContext.Current.Request.Form["settleparvalue"];
                string usestatus = HttpContext.Current.Request.Form["usestatus"];
                string sign = HttpContext.Current.Request.Form["sign"];


                string plain = string.Format("canresubmit={5}customerorderid={0}remark={1}settleparvalue={2}usestatus={3}{4}"
                    , customerorderid
                    , remark
                    , settleparvalue
                    , usestatus
                    , suppKey
                    , canresubmit
                    );
                log("Notify plain " + plain);

                string localsign = viviLib.Security.Cryptography.MD5(plain,"utf-8");

                log("Notify localsign " + localsign);

                log("Notify sign " + sign);

                if (String.Equals(localsign, sign, StringComparison.CurrentCultureIgnoreCase))
                {
                    switch (usestatus.ToLower())
                    {
                        case "4":
                            orderAmt = decimal.Parse(settleparvalue);
                            status = 2;
                            opstate = "0";
                            msg = "支付成功";
                            viewMsg = msg;
                            break;
                        case "1":
                        case "5":
                            status = 4;
                            opstate = "99";
                            msg = remark;
                            viewMsg = SellCard20.GetMsgToUserView(opstate);
                            break;
                    }

                    if (status > 0)
                    {
                        var response = new CardOrderSupplierResponse()
                        {
                            SupplierId = SuppId,
                            SuppTransNo = "",
                            SysOrderNo = customerorderid,
                            OrderAmt = orderAmt,
                            SuppAmt = 0M,
                            OrderStatus = status,
                            SuppErrorCode = "99",
                            Opstate = opstate,
                            SuppErrorMsg = msg,
                            ViewMsg = viewMsg,
                            Method = 1
                        };

                        string backXML = string.Format(@"<?xml version=""1.0"" encoding=""utf-8""?>
<root>   
<ret>
<status>{0}</status>
</ret>
</root>", "true");

                        OrderCardUtils.SuppNotify(response, backXML);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);

                HttpContext.Current.Response.Write("error");
                //HttpContext.Current.Response.End();
            }
        }

        #endregion

        #region GetMsgInfo
        /// <summary>
        /// 
        /// </summary>
        /// <param name="suppcode"></param>
        /// <returns></returns>
        public string GetMsgInfo(string suppcode)
        {
            string syscode = suppcode;

            switch (suppcode)
            {
                //case "300"://寄售成功
                //    syscode = "0";//支付成功
                //    break;
                case "-1"://创建订单失败
                    syscode = "创建订单失败";//数据非法
                    break;
                case "2"://代理商ID错误 或未开通
                    syscode = "代理商ID错误 或未开通";//非法用户
                    break;
                case "3"://IP验证错误
                    syscode = "IP验证错误";//其他错误
                    break;
                case "4"://签名验证错误
                    syscode = "签名验证错误";//签名验证错误
                    break;
                case "5"://重复的订单号
                    syscode = "重复的订单号";//订单号存在
                    break;
                case "1"://传入参数有误
                    syscode = "传入参数有误";//数据非法
                    break;
                case "8"://单据不存在
                    syscode = "单据不存在";//不存在订单
                    break;
                case "22"://卡号卡密加密错误
                    syscode = "卡号卡密加密错误";//不存在订单
                    break;
                case "98"://接口维护
                    syscode = "接口维护";//运营商维护
                    break;

                case "201"://卡不符合规则
                    syscode = "卡不符合规则";//
                    break;
                case "202"://不支持此卡种
                    syscode = "不支持此卡种";//
                    break;
                case "203"://暂停此卡种
                    syscode = "暂停此卡种";//
                    break;
                case "204"://不支持此面值
                    syscode = "不支持此面值";//
                    break;
                case "205"://暂停此面值
                    syscode = "暂停此面值";//卡余额不足
                    break;

                case "206"://运营商维护
                    syscode = "运营商维护";//
                    break;
                case "207"://用户订单号重复
                    syscode = "用户订单号重复";//
                    break;
                case "208"://卡有处理记录
                    syscode = "卡有处理记录";//
                    break;
                case "209"://卡频繁提交
                    syscode = "卡频繁提交";//
                    break;
                case "210"://卡频繁提交
                    syscode = "用户配置不完整";//
                    break;
                case "300"://卡频繁提交
                    syscode = "成功";//
                    break;
                case "302"://失败，运营商失败
                    syscode = "运营商失败";//运营商维护
                    break;
                case "303"://失败，卡无效
                    syscode = "卡无效";//充值卡无效
                    break;
                case "304"://失败，卡密错误
                    syscode = "卡密错误";//密码错误
                    break;
                case "305"://失败，卡余额不足
                    syscode = "卡余额不足";//卡余额不足
                    break;
                case "306"://失败，卡状态错误
                    syscode = "卡状态错误";//卡余额不足
                    break;
            }
            return syscode;
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
            string syscode = "99";

            switch (suppcode)
            {
                //case "300"://寄售成功
                //    syscode = "0";//支付成功
                //    break;
                case "-1"://创建订单失败
                    syscode = "7";//数据非法
                    break;
                case "2"://代理商ID错误 或未开通
                    syscode = "8";//非法用户
                    break;
                case "3"://IP验证错误
                    syscode = "99";//其他错误
                    break;
                case "4"://签名验证错误
                    syscode = "3";//签名验证错误
                    break;
                case "5"://重复的订单号
                    syscode = "6";//订单号存在
                    break;
                case "1"://传入参数有误
                    syscode = "7";//数据非法
                    break;
                case "8"://单据不存在
                    syscode = "14";//不存在订单
                    break;
                case "22"://卡号卡密加密错误
                    syscode = "99";//不存在订单
                    break;
                case "98"://接口维护
                    syscode = "19";//运营商维护
                    break;

                case "201"://卡不符合规则
                    syscode = "2";//
                    break;
                case "202"://不支持此卡种
                    syscode = "2";//
                    break;
                case "203"://暂停此卡种
                    syscode = "9";//
                    break;
                case "204"://不支持此面值
                    syscode = "2";//
                    break;
                case "205"://暂停此面值
                    syscode = "9";//卡余额不足
                    break;

                case "206"://运营商维护
                    syscode = "19";//
                    break;
                case "207"://用户订单号重复
                    syscode = "6";//
                    break;
                case "208"://卡有处理记录
                    syscode = "5";//
                    break;
                case "209"://卡频繁提交
                    syscode = "20";//
                    break;

                case "302"://失败，运营商失败
                    syscode = "19";//运营商维护
                    break;
                case "303"://失败，卡无效
                    syscode = "10";//充值卡无效
                    break;
                case "304"://失败，卡密错误
                    syscode = "16";//密码错误
                    break;
                case "305"://失败，卡余额不足
                    syscode = "18";//卡余额不足
                    break;
                case "306"://失败，卡状态错误
                    syscode = "99";//卡余额不足
                    break;
            }
            return syscode;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetCardType(int type)
        {
            switch (type)
            {
                case 106:
                    return "30";//骏网一卡通
                case 103:
                    return "31";//移动神州行
                case 108:
                    return "25";//联通一卡充
                case 113:
                    return "26";//电信国卡
                case 104:
                    return "27";//盛大一卡通
                case 110:
                    return "22";//网易一卡通
                case 105:
                    return "33";//征途一卡通
                case 111:
                    return "24";//完美一卡通
                case 112:
                    return "35";//搜狐一卡通
                case 109:
                    return "28";//久游一卡通
                //case 117:
                //    return "55";//纵游一卡通
                case 107:
                    return "18";//QQ币充值卡
                //case 115:
                //    return "58";//光宇一卡通
                case 118:
                    return "36";//天下一卡通
                case 119:
                    return "32";//天宏一卡通
                case 210:
                    return "45";//盛付通一卡通

            }
            return "";
        }


        public static string md5sign(string str)
        {
            System.Text.Encoding gbkEncoding = System.Text.Encoding.GetEncoding("GBK");
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string a = BitConverter.ToString(md5.ComputeHash(gbkEncoding.GetBytes(str)));
            a = a.Replace("-", "");
            return a.ToLower();
        }


        public string card_Encode(string src)
        {
            byte[] keys = System.Text.Encoding.GetEncoding("utf-8").GetBytes(this._suppInfo.puserkey1);
            byte[] data = System.Text.Encoding.GetEncoding("utf-8").GetBytes(src);
            byte[] iv = System.Text.Encoding.GetEncoding("utf-8").GetBytes("123456");

            byte[] result = viviLib.Security.Des3.Des3EncodeECB(keys, iv, data);
            if (result != null)
                return ToHexString(result);
            return src;

        }

        private char[] hexDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        public string ToHexString(byte[] bytes)
        {
            int num = bytes.Length;
            char[] chars = new char[num * 2];
            for (int i = 0; i < num; i++)
            {
                int b = bytes[i];
                chars[i * 2] = hexDigits[b >> 4];
                chars[i * 2 + 1] = hexDigits[b & 0xF];
            }
            return new string(chars);
        }
        #endregion

        #region Finish
        /// <summary>
        /// 
        /// </summary>
        /// <param name="callBackText"></param>
        /// <returns></returns>
        public bool Finish(string callBackText)
        {
            bool result = false;

            try
            {
                if (!string.IsNullOrEmpty(callBackText))
                {
                    string retcode = "";
                    string retmsg = "";
                    
                    string orderid = "";
                    string cardnumber = "";
                    string cardpwd = "";
                    string storagetime = "";
                    string usetime = "";
                    string usestatus = "";
                    string customerorderid = "";
                    string settleparvalue = "";
                    string settlediscount = "";
                    string settlemoney = "";

                    #region xml
                    var xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(callBackText);

                    var xmlNodeList = xmlDocument.SelectSingleNode("Root/Ret");
                    if (xmlNodeList != null)
                    {
                        foreach (XmlNode list in xmlNodeList)
                        {
                            switch (list.Name.ToLower())
                            {
                                case "retcode":
                                    retcode = list.InnerText;
                                    break;
                                case "retmsg":
                                    retmsg = list.InnerText;
                                    break;
                            }
                        }
                    }

                    xmlNodeList = xmlDocument.SelectSingleNode("Root/Card");
                    if (xmlNodeList != null)
                    {
                        foreach (XmlNode list in xmlNodeList)
                        {
                            switch (list.Name.ToLower())
                            {
                                case "orderid":
                                    orderid = list.InnerText;
                                    break;
                                case "cardnumber":
                                    cardnumber = list.InnerText;
                                    break;
                                case "storagetime":
                                    storagetime = list.InnerText;
                                    break;
                                case "usetime":
                                    usetime = list.InnerText;
                                    break;
                                case "usestatus":
                                    usestatus = list.InnerText;
                                    break;
                                case "customerorderid":
                                    customerorderid = list.InnerText;
                                    break;
                                case "settleparvalue":
                                    settleparvalue = list.InnerText;
                                    break;
                                case "settlediscount":
                                    settlediscount = list.InnerText;
                                    break;
                                case "settlemoney":
                                    settlemoney = list.InnerText;
                                    break;

                            }
                        }
                    }
                    #endregion

                    if (retcode == "30001")
                    {
                        #region 
                        int status = 0;
                        string opstate = "";
                        decimal orderAmt = 0M;
                        string msg = "", viewMsg = "";

                        switch (usestatus)
                        {
                            case "4":
                                orderAmt = decimal.Parse(settleparvalue);
                                status = 2;
                                opstate = "0";
                                msg = "支付成功";
                                viewMsg = msg;
                                break;
                            case "1":
                            case "5":
                                status = 4;
                                opstate = ConvertCode(retcode);
                                msg = retmsg;
                                viewMsg = SellCard20.GetMsgToUserView(opstate);
                                break;
                        }

                        if (status > 0)
                        {
                            var response = new CardOrderSupplierResponse()
                            {
                                SupplierId = SuppId,
                                SuppTransNo = orderid,
                                SysOrderNo = customerorderid,
                                OrderAmt = orderAmt,
                                SuppAmt = 0M,
                                OrderStatus = status,
                                SuppErrorCode = retcode,
                                Opstate = opstate,
                                SuppErrorMsg = msg,
                                ViewMsg = viewMsg,
                                Method = 1
                            };

                            OrderCardUtils.Finish(response);
                            result = true;
                        }
                        #endregion
                    }
                   
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }

            return result;
        }
        #endregion
    }
}
