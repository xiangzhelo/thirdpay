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
using viviLib.ExceptionHandling;
using viviLib.Web;

namespace viviapi.ETAPI.HuiYuan
{
    /// <summary>
    /// 
    /// </summary>
    public class Card : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.HuiSu;

        public Card()
            : base(SuppId)
        {

        }

        public static HuiYuan.Card Default
        {
            get
            {
                var card = new HuiYuan.Card();
                return card;
            }
        }

        public string NotifyUrl
        {
            get
            {
                return SiteDomain + "/receive/huiyuan/card.aspx";
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

            string agentID = SuppAccount;
            string billID = o.SysOrderNo;
            string billTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string cardType = GetCardType(o.CardTypeId);
            string cardData = o.CardNo + "," + o.CardPass + "," + o.FaceValue.ToString(CultureInfo.InvariantCulture);// Encode();
            cardData = card_Encode(cardData);

            string cardAMT = o.FaceValue.ToString(CultureInfo.InvariantCulture);
            string clientIP = viviLib.Web.ServerVariables.TrueIP;

            string desc = string.Empty;
            string extParam = string.Empty;
            string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmss");

            string plain = string.Format("agent_id={0}&bill_id={1}&bill_time={2}&card_type={3}&card_data={4}&card_amt={5}&notify_url={6}&time_stamp={7}|||{8}"
                , agentID
                , billID
                , billTime
                , cardType
                , cardData
                , cardAMT
                , NotifyUrl
                , timeStamp
                , SuppKey);

            string sign = md5sign(plain).ToLower();

            try
            {
                string postUrl = string.Format("{9}?agent_id={0}&bill_id={1}&bill_time={2}&card_type={3}&card_data={4}&card_amt={5}&notify_url={6}&time_stamp={7}&sign={8}"
                , agentID
                , billID
                , billTime
                , cardType
                , cardData
                , cardAMT
                , NotifyUrl
                , timeStamp
                , sign
                , PostCardUrl);

                SynsSummitLogger(postUrl);

                string callBackText = WebClientHelper.GetString(postUrl, null, "get", System.Text.Encoding.GetEncoding("gbk"));

                SynsSummitLogger(callBackText);

                callBack.Success = 1;
                callBack.SuppCallBackText = callBackText;

                if (!string.IsNullOrEmpty(callBackText))
                {
                    string[] results = callBackText.Split('&');
                    if (results.Length > 2)
                    {
                        string retCode = results[0].Replace("ret_code=", "");

                        callBack.SuppErrorCode = retCode;
                        callBack.SuppErrorMsg = results[1];

                        if (retCode == "0")
                        {
                            callBack.SummitStatus = 1;
                        }
                        if (retCode == "99")
                        {
                            callBack.SummitStatus = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);

                callBack.Success = 0;
                callBack.Message = ex.Message;
            }
            return callBack;
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
                string retCode = HttpContext.Current.Request.QueryString["ret_code"];
                string retMsg = HttpContext.Current.Request.QueryString["ret_msg"];
                string agentID = HttpContext.Current.Request.QueryString["agent_id"];
                string billID = HttpContext.Current.Request.QueryString["bill_id"];
                string jnetBillNO = HttpContext.Current.Request.QueryString["jnet_bill_no"];
                string billStatus = HttpContext.Current.Request.QueryString["bill_status"];
                string cardRealAMT = HttpContext.Current.Request.QueryString["card_real_amt"];
                string cardSettleAMT = HttpContext.Current.Request.QueryString["card_settle_amt"];
                string cardDetailData = HttpContext.Current.Request.QueryString["card_detail_data"];
                string extParam = HttpContext.Current.Request.QueryString["ext_param"];

                string sign = HttpContext.Current.Request.QueryString["sign"];

                string plain = string.Format("ret_code={0}&agent_id={1}&bill_id={2}&jnet_bill_no={3}&bill_status={4}&card_real_amt={5}&card_settle_amt={6}&card_detail_data={7}|||{8}"
                    , retCode
                    , agentID
                    , billID
                    , jnetBillNO
                    , billStatus
                    , cardRealAMT
                    , cardSettleAMT
                    , cardDetailData
                    , SuppKey
                    );

                AsynsRetLogger(plain);

                string localsign = md5sign(plain);

                if (localsign.ToLower() == sign.ToLower())
                {
                    string msg = GetMsgInfo(retCode);
                    int status = (retCode == "0" && billStatus == "1") ? 2 : 4;
                    string opstate = "-1";
                    if (status == 2)
                    {
                        opstate = "0";

                        msg = "支付成功";
                    }
                    else
                    {
                        opstate = ConvertCode(retCode);
                    }

                    var response = new CardOrderSupplierResponse()
                    {
                        SupplierId = SuppId,
                        SuppTransNo = billID,
                        SysOrderNo = billID,
                        OrderAmt = decimal.Parse(cardRealAMT),
                        SuppAmt = 0M,
                        OrderStatus = status,
                        SuppErrorCode = retCode,
                        Opstate = opstate,
                        SuppErrorMsg = msg,
                        ViewMsg = msg,
                        Method = 1
                    };

                    OrderCardUtils.SuppNotify(response, Succflag);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                HttpContext.Current.Response.Write("error");
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

            string sign = md5sign(plain).ToLower();
            string callBack;
            try
            {
                string postUrl = string.Format("http://Service.800j.com/Consign/Query.aspx?agent_id={0}&bill_id={1}&time_stamp={2}&sign={3}"
                , agentID
                , billID
                , timeStamp
                , sign);

                AsynsRetLogger(postUrl);

                callBack = WebClientHelper.GetString(postUrl, null, "get", System.Text.Encoding.GetEncoding("gbk"));

                AsynsRetLogger(callBack);

            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                callBack = ex.Message;
            }
            return callBack;
        }
        #endregion

        public bool Finish(string callback)
        {
            bool result = false;

            try
            {
                if (!string.IsNullOrEmpty(callback))
                {
                    string[] arr = callback.Split('&');

                    if (arr.Length == 11)
                    {
                        string retCode = arr[0];
                        string retMsg = arr[1];
                        string agentID = arr[2];
                        string billID = arr[3];
                        string jnetBillNO = arr[4];
                        string billStatus = arr[5];
                        string cardRealAMT = arr[6];
                        string cardSettleAMT = arr[7];
                        string cardDetailData = arr[8];
                        string extParam = arr[9];
                        string sign = arr[10];

                        string plain = string.Format("{0}&{1}&{2}&{3}&{4}&{5}&{6}&{7}|||{8}"
                            , retCode
                            , agentID
                            , billID
                            , jnetBillNO
                            , billStatus
                            , cardRealAMT
                            , cardSettleAMT
                            , cardDetailData
                            , SuppKey);

                        string locationsign = md5sign(plain);

                        if (sign.Replace("sign=", "") == locationsign)
                        {
                            billID = billID.Replace("bill_id=", "");
                            retCode = retCode.Replace("ret_code=", "");
                            billStatus = billStatus.Replace("bill_status=", "");
                            retMsg = retMsg.Replace("ret_msg=", "");
                            jnetBillNO = jnetBillNO.Replace("jnet_bill_no=", "");
                            cardRealAMT = cardRealAMT.Replace("card_real_amt=", "");

                            string opstate = "-1";
                            int status = 4;
                            string msg = GetMsgInfo(retCode);
                            if (retCode == "0")
                            {
                                if (billStatus == "0")
                                {
                                    opstate = string.Empty; //需要再次查询
                                }
                                else if (billStatus == "1")
                                {
                                    opstate = "0";
                                    status = 2;
                                    msg = "支付成功";
                                }
                            }
                            else if (retCode != "99")
                            {
                                opstate = "-1";
                            }

                            if (!string.IsNullOrEmpty(opstate))
                            {
                                decimal realAMT = 0;
                                if (decimal.TryParse(cardRealAMT.Trim(), out realAMT))
                                {
                                    var response = new CardOrderSupplierResponse()
                                    {
                                        SupplierId = SuppId,
                                        SuppTransNo = jnetBillNO,
                                        SysOrderNo = billID,
                                        OrderAmt = realAMT,
                                        SuppAmt = 0M,
                                        OrderStatus = status,
                                        SuppErrorCode = retCode,
                                        Opstate = opstate,
                                        SuppErrorMsg = msg,
                                        ViewMsg = msg,
                                        Method = 1
                                    };
                                    OrderCardUtils.Finish(response);
                                }
                                result = true;
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
                case 103:
                    return "13";//移动神州行

                case 104:
                    return "35";//盛大一卡通

                case 105:
                    return "43";//征途一卡通

                case 106:
                    return "10";//骏网一卡通

                case 108:
                    return "14";//联通一卡充

                case 109:
                    return "47";//久游一卡通

                case 110:
                    return "42";//网易一卡通

                case 111:
                    return "44";//完美一卡通

                case 112:
                    return "46";//搜狐一卡通

                case 113:
                    return "15";//电信国卡
            }
            return type.ToString(CultureInfo.InvariantCulture);
        }
        #endregion

        #region GetMsgInfo
        /// <summary>
        /// 
        /// </summary>
        /// <param name="retCode"></param>
        /// <returns></returns>
        public string GetMsgInfo(string retCode)
        {
            switch (retCode)
            {
                case "0":
                    return "寄售成功";
                case "-1":
                    return "失败";
                case "-2":
                    return "单据受理中";
                case "1":
                    return "传入参数有误";
                case "2":
                    return "未开通该服务";
                case "3":
                    return "IP验证错误";
                case "4":
                    return "签名验证错误";
                case "5":
                    return "重复的订单号";
                case "6":
                    return "卡加密错误";
                case "7":
                    return "卡验证失败";
                case "8":
                    return "单据不存在";
                case "9":
                    return "卡号或密码不正确";
                case "10":
                    return "卡中余额不足";
                case "22":
                    return "卡号卡密格式加密错误";
                case "98":
                    return "接口维中";
                case "99":
                    return "系统错误";
            }
            return retCode;
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
            if (suppcode == "9" || suppcode == "7")
            {
                syscode = "-1";//卡号或密码错误
            }
            else if (suppcode == "10")
            {
                syscode = "-10";//卡中余额不足
            }
            else
            {
                syscode = "-11";//卡号不存在
            }
            return syscode;
        }
        #endregion

        #region Functions
        public string card_Encode(string src)
        {
            byte[] keys = System.Text.Encoding.GetEncoding("utf-8").GetBytes(this.SuppInfo.puserkey1);
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

        public static string md5sign(string str)
        {
            System.Text.Encoding gbkEncoding = System.Text.Encoding.GetEncoding("GBK");
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string a = BitConverter.ToString(md5.ComputeHash(gbkEncoding.GetBytes(str)));
            a = a.Replace("-", "");
            return a.ToLower();
        }
        #endregion
    }
}
