using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using viviapi.ETAPI.Common;
using viviapi.Model.Order.Card;
using viviapi.Model.supplier;
using viviapi.SysConfig;
using viviapi.SysInterface.Card;
using viviapi.SysInterface.Card.MyAPI;
using viviLib.ExceptionHandling;
using viviLib.Web;

namespace viviapi.ETAPI.HuiSu
{
    public class Card : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.HuiSu;

        public Card()
            : base(SuppId)
        {

        }

        public static HuiSu.Card Default
        {
            get
            {
                var card = new HuiSu.Card();
                return card;
            }
        }

        public string NotifyUrl
        {
            get
            {
                return SiteDomain + "/receive/huisu/card.aspx"; ;
            }
        }

        internal string Succflag = "ok";

        #region CardSend
        /// <summary>
        /// http://service.800j.com/Consign/HuisuRecycleCardSubmit.aspx
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public CardSynchCallBack CardSend(CardOrderSummitArgs o)
        {
            var callBack = new CardSynchCallBack();

            string postUrl = PostCardUrl;
            if (string.IsNullOrEmpty(postUrl))
                postUrl = "http://service.800j.com/Consign/HuisuRecycleCardSubmit.aspx";

            string agentID = SuppAccount;
            string billID = o.SysOrderNo;
            string billTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string cardType = GetCardType(o.CardTypeId);
            string cardData = o.CardNo + "," + o.CardPass + "," + o.FaceValue.ToString(CultureInfo.InvariantCulture);// Encode();
            cardData = card_Encode(cardData);

            string cardPrice = o.FaceValue.ToString(CultureInfo.InvariantCulture);
            string clientIP = ServerVariables.TrueIP;

            string desc = string.Empty;
            string extParam = string.Empty;
            string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmss");

            string plain = string.Format("agent_id={0}&bill_id={1}&bill_time={2}&card_type={3}&card_data={4}&card_price={5}&notify_url={6}&time_stamp={7}|||{8}"
                , agentID
                , billID
                , billTime
                , cardType
                , cardData
                , cardPrice
                , NotifyUrl
                , timeStamp
                , SuppKey);

            string sign = Md5Sign(plain).ToLower();

            try
            {
                postUrl = string.Format("{9}?agent_id={0}&bill_id={1}&bill_time={2}&card_type={3}&card_data={4}&card_price={5}&notify_url={6}&time_stamp={7}&sign={8}"
                , agentID
                , billID
                , billTime
                , cardType
                , cardData
                , cardPrice
                , NotifyUrl
                , timeStamp
                , sign
                , postUrl);

                SynsSummitLogger(postUrl);

                string callBackText = WebClientHelper.GetString(postUrl, null, "get", Encoding.GetEncoding("gbk"));

                SynsSummitLogger(callBackText);

                callBack.SuppCallBackText = callBackText;

                if (!string.IsNullOrEmpty(callBackText))
                {
                    string[] results = callBackText.Split('&');
                    if (results.Length > 2)
                    {
                        string retCode = GetValue(results[0]);
                        string billStatus = GetValue(results[4]);

                        callBack.SuppErrorCode = retCode + "|" + billStatus;
                        callBack.SuppErrorMsg = GetValue(results[7]);
                        callBack.SuppTransNo = GetValue(results[3]);

                        if (retCode == "0" || retCode == "99")
                        {
                            callBack.Success = 1;

                            if (billStatus == "0" || billStatus == "1")
                            {
                                callBack.SummitStatus = 1;
                            }
                            if (billStatus == "2")
                            {
                                callBack.SummitStatus = 1;
                                callBack.OrderStatus = 2;
                            }
                            if (billStatus == "-1")
                            {
                                callBack.SummitStatus = 0;
                            }
                        }
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

                string retCode = HttpContext.Current.Request.QueryString["ret_code"];
                string retMsg = HttpContext.Current.Request.QueryString["ret_msg"];
                string agentID = HttpContext.Current.Request.QueryString["agent_id"];
                string billID = HttpContext.Current.Request.QueryString["bill_id"];
                string huisuBillID = HttpContext.Current.Request.QueryString["huisu_bill_id"];
                string billStatus = HttpContext.Current.Request.QueryString["bill_status"];
                string cardRealPrice = HttpContext.Current.Request.QueryString["card_real_price"];
                string cardSettleAmt = HttpContext.Current.Request.QueryString["card_settle_amt"];
                string extParam = HttpContext.Current.Request.QueryString["ext_param"];
                string sign = HttpContext.Current.Request.QueryString["sign"];

                AsynsRetLogger(sign);

                string plain = string.Format("ret_code={0}&agent_id={1}&bill_id={2}&huisu_bill_id={3}&bill_status={4}&card_real_price={5}&card_settle_amt={6}|||{7}"
                    , retCode
                    , agentID
                    , billID
                    , huisuBillID
                    , billStatus
                    , cardRealPrice
                    , cardSettleAmt
                    , SuppKey
                    );

                AsynsRetLogger("plain" + plain);

                string localsign = Md5Sign(plain);

                AsynsRetLogger("localsign" + localsign);

                if (String.Equals(localsign, sign, StringComparison.CurrentCultureIgnoreCase))
                {
                    if (retCode != "0") return;

                    switch (billStatus)
                    {
                        case "2":
                            orderAmt = decimal.Parse(cardRealPrice);
                            status = 2;
                            opstate = "0";
                            msg = "支付成功";
                            viewMsg = msg;
                            break;
                        case "-1":
                            status = 4;
                            opstate = ConvertCode(retMsg);
                            msg = GetMsgInfo(retMsg);
                            viewMsg = SysInterface.Card.MyAPI.Utility.GetMessageByCode(opstate);
                            break;
                    }

                    if (status > 0)
                    {
                        var response = new CardOrderSupplierResponse()
                        {
                            SupplierId = SuppId,
                            SuppTransNo = huisuBillID,
                            SysOrderNo = billID,
                            OrderAmt = orderAmt,
                            SuppAmt = 0M,
                            OrderStatus = status,
                            SuppErrorCode = retCode,
                            Opstate = opstate,
                            SuppErrorMsg = msg,
                            ViewMsg = viewMsg,
                            Method = 1
                        };

                        OrderCardUtils.SuppNotify(response, Succflag);
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

        #region Query
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public string Query(string orderid)
        {
            string agentID = SuppAccount;
            string billID = orderid;
            string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmss");

            string plain = string.Format("agent_id={0}&bill_id={1}&time_stamp={2}|||{3}"
                , agentID
                , billID
                , timeStamp
                , SuppKey);

            string sign = Md5Sign(plain).ToLower();
            string responeText;
            try
            {
                string postUrl = string.Format("http://service.800j.com/consign/HuisuRecycleCardQuery.aspx?agent_id={0}&bill_id={1}&time_stamp={2}&sign={3}"
                , agentID
                , billID
                , timeStamp
                , sign);

                responeText = WebClientHelper.GetString(postUrl, null, "GET", Encoding.GetEncoding("gbk"));

            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                responeText = ex.Message;
            }
            return responeText;
        }
        #endregion

        #region Finish
        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public bool Finish(string callback)
        {
            bool result = false;

            try
            {
                if (!string.IsNullOrEmpty(callback))
                {
                    string[] arr = callback.Split('&');

                    if (arr.Length == 10)
                    {
                        string retCode = GetValue(arr[0]);
                        string agentID = GetValue(arr[1]);
                        string billID = GetValue(arr[2]);
                        string huisuBillID = GetValue(arr[3]);
                        string billStatus = GetValue(arr[4]);
                        string cardRealPrice = GetValue(arr[5]);
                        string cardSettleAmt = GetValue(arr[6]);
                        string retMsg = GetValue(arr[7]);
                        string extParam = GetValue(arr[8]);
                        string sign = GetValue(arr[9]);

                        string plain = string.Format("ret_code={0}&agent_id={1}&bill_id={2}&huisu_bill_id={3}&bill_status={4}&card_real_price={5}&card_settle_amt={6}|||{7}"
                            , retCode
                            , agentID
                            , billID
                            , huisuBillID
                            , billStatus
                            , cardRealPrice
                            , cardSettleAmt
                            , SuppKey);

                        string locationsign = Md5Sign(plain);

                        if (sign == locationsign)
                        {
                            if (retCode == "0")
                            {
                                int status = 0;
                                string opstate = "";
                                decimal orderAmt = 0M;
                                string msg = "", viewMsg = "";

                                switch (billStatus)
                                {
                                    case "2":
                                        orderAmt = decimal.Parse(cardRealPrice);
                                        status = 2;
                                        opstate = "0";
                                        msg = "支付成功";
                                        viewMsg = msg;
                                        break;
                                    case "-1":
                                        status = 4;
                                        opstate = ConvertCode(retMsg);
                                        msg = GetMsgInfo(retMsg);
                                        viewMsg = SysInterface.Card.MyAPI.Utility.GetMessageByCode(opstate);
                                        break;
                                }

                                if (status > 0)
                                {
                                    var response = new CardOrderSupplierResponse()
                                    {
                                        SupplierId = SuppId,
                                        SuppTransNo = huisuBillID,
                                        SysOrderNo = billID,
                                        OrderAmt = orderAmt,
                                        SuppAmt = 0M,
                                        OrderStatus = status,
                                        SuppErrorCode = retCode,
                                        Opstate = opstate,
                                        SuppErrorMsg = msg,
                                        ViewMsg = viewMsg,
                                        Method = 1
                                    };

                                    OrderCardUtils.Finish(response);
                                    result = true;
                                }
                            }

                        }
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

        #region GetCardType
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
                    return "10";//骏网一卡通
                case 103:
                    return "13";//移动神州行
                case 108:
                    return "14";//联通一卡充
                case 113:
                    return "15";//电信国卡
                case 104:
                    return "41";//盛大一卡通
                case 110:
                    return "42";//网易一卡通
                case 105:
                    return "43";//征途一卡通
                case 111:
                    return "44";//完美一卡通
                case 112:
                    return "46";//搜狐一卡通
                case 109:
                    return "47";//久游一卡通
                case 117:
                    return "55";//纵游一卡通
                case 107:
                    return "57";//QQ币充值卡
                case 115:
                    return "58";//光宇一卡通
                case 118:
                    return "59";//天下一卡通
                case 119:
                    return "60";//天宏一卡通
                case 210:
                    return "61";//盛付通一卡通

            }
            return type.ToString(CultureInfo.InvariantCulture);
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

        #region Funtions
        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public string card_Encode(string src)
        {
            byte[] keys = Encoding.GetEncoding("utf-8").GetBytes(this.SuppUserName);
            byte[] data = Encoding.GetEncoding("utf-8").GetBytes(src);
            byte[] iv = Encoding.GetEncoding("utf-8").GetBytes("123456");

            byte[] result = viviLib.Security.Des3.Des3EncodeECB(keys, iv, data);
            if (result != null)
                return ToHexString(result);
            return src;

        }

        private readonly char[] _hexDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        public string ToHexString(byte[] bytes)
        {
            int num = bytes.Length;
            char[] chars = new char[num * 2];
            for (int i = 0; i < num; i++)
            {
                int b = bytes[i];
                chars[i * 2] = _hexDigits[b >> 4];
                chars[i * 2 + 1] = _hexDigits[b & 0xF];
            }
            return new string(chars);
        }

        public string Md5Sign(string str)
        {
            Encoding gbkEncoding = Encoding.GetEncoding("GBK");
            var md5 = new MD5CryptoServiceProvider();
            string a = BitConverter.ToString(md5.ComputeHash(gbkEncoding.GetBytes(str)));
            a = a.Replace("-", "");
            return a.ToLower();
        }

        #endregion

    }
}
