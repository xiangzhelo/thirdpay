using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using viviapi.ETAPI.Common;
using viviapi.Model.Order.Card;
using viviapi.Model.Payment;
using viviapi.BLL.Payment;
using viviapi.Model.Order;
using viviapi.Model.supplier;
using viviapi.SysConfig;
using viviapi.Model;
using viviLib.ExceptionHandling;
using viviLib.Web;
using viviLib.Logging;


namespace viviapi.ETAPI.TaoShang
{
    /// <summary>
    /// 以管理员身份注册: regsvr32 盘符:\路径\encrypts.dll 
    /// 在C#开发环境中 “添加引用", 在com选项页中找到 encrypts注册项,确定引用
    /// 在程序中调用:encrypts.Encrypt objEncrypt=new encrypts.Encrypt() ; strValue = objEncrypt.get_encryptA(data, key);		//加密
    /// </summary>
    public class Card : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.TaoShang;

        public Card()
            : base(SuppId)
        {

        }

        public static Card Default
        {
            get
            {
                var lbpay = new Card();
                return lbpay;
            }
        }

        internal string notify_url { get { return this.SiteDomain + "/receive/taoshang/card.aspx"; } }
        string succflag = "opstate=0";

        #region CardSend
        /// <summary>
        /// </summary>
        /// <param name="_orderid"></param>
        /// <param name="_cardno"></param>
        /// <param name="_cardpwd"></param>
        /// <param name="_typeId"></param>
        /// <param name="cardvalue"></param>
        /// <param name="supporderid"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public string CardSend(string _orderid, string _cardno, string _cardpwd, int _typeId, int cardvalue, out string supporderid,out string supperrorcode, out string errmsg)
        {
            errmsg = string.Empty;
            supporderid = string.Empty;
            supperrorcode = string.Empty;

            string opstate = "-1";
            string puserkey = this.SuppKey;

            if (string.IsNullOrEmpty(this.PostCardUrl)
                || string.IsNullOrEmpty(puserkey))
            {
                return opstate;
            }

            string commitUrl = this.PostCardUrl + "?";
            string cardType = GetCardType(_typeId).ToString();
           // string cardType = GetCardType(_typeId).ToString("0000") + cardvalue.ToString();
            string _restrict = "0";

            string plain = string.Format("type={0}&parter={1}&cardno={2}&cardpwd={3}&value={4}&restrict={5}&orderid={6}&callbackurl={7}"
                , cardType
                , SuppAccount
                , _cardno
                , _cardpwd
                , cardvalue
                , _restrict
                , _orderid
                , notify_url);

            string sign = viviLib.Security.Cryptography.MD5(plain + SuppKey);

            try
            {
                System.Text.StringBuilder postData = new StringBuilder(plain);
                postData.AppendFormat("&sign={0}", sign);

                //LogWrite(commitUrl + postData.ToString());
                //LogWrite(postData.ToString());

                string retCode = viviLib.Web.WebClientHelper.GetString(commitUrl, postData.ToString(), "GET", System.Text.Encoding.GetEncoding("GB2312"), 10000);
                supperrorcode = retCode.Replace("opstate=","");

                errmsg = GetMsgInfo(retCode, string.Empty);
                if (retCode == "opstate=1")
                {
                    opstate = "0";
                }
            }
            catch (Exception ex)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
            }
            return opstate;
        }
        #endregion

        #region CardSend
        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public CardSynchCallBack CardSend(CardOrderSummitArgs o)
        {
            var callBack = new CardSynchCallBack();

            string opstate = "-1";
            string puserkey = this.SuppKey;

            if (string.IsNullOrEmpty(this.PostCardUrl)
                || string.IsNullOrEmpty(puserkey))
            {
                callBack.Success = 0;
                callBack.Message = "未配置网关";

                return callBack;
            }

            string commitUrl = this.PostCardUrl + "?";
            string cardType = GetCardType(o.CardTypeId).ToString();
            // string cardType = GetCardType(_typeId).ToString("0000") + cardvalue.ToString();
            string _restrict = "0";

            string plain = string.Format("type={0}&parter={1}&cardno={2}&cardpwd={3}&value={4}&restrict={5}&orderid={6}&callbackurl={7}"
                , cardType
                , SuppAccount
                , o.CardNo
                , o.CardPass
                , o.FaceValue
                , _restrict
                , o.SysOrderNo
                , notify_url);

            string sign = viviLib.Security.Cryptography.MD5(plain + SuppKey);

            try
            {
                var postData = new StringBuilder(plain);
                postData.AppendFormat("&sign={0}", sign);

                //LogWrite(commitUrl + postData.ToString());
                //LogWrite(postData.ToString());

                string retCode = WebClientHelper.GetString(commitUrl, postData.ToString(), "GET", System.Text.Encoding.GetEncoding("GB2312"), 10000);
                
                callBack.SuppCallBackText = retCode;
                callBack.SuppErrorCode = retCode.Replace("opstate=", "");
                callBack.SuppErrorMsg = GetMsgInfo(retCode, string.Empty);

                if (retCode == "opstate=1")
                {
                    callBack.SummitStatus = 1;
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

        #region GetMsgInfo
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardstatus"></param>
        /// <returns></returns>
        public string GetMsgInfo(string retcode, string _ovalue)
        {
            switch (retcode)
            {
                case "opstate=0":
                    return "支付成功";
                case "opstate=1":
                    return "数据接收成功";
                case "opstate=2":
                    return "不支持该类卡或者该面值的卡";
                case "opstate=3":
                    return "签名验证失败";
                case "opstate=4":
                    return "订单内容重复";
                case "opstate=5":
                    return "该卡密已经有被使用的记录";
                case "opstate=6":
                    return "订单号已经存在";
                case "opstate=7":
                    return "数据非法";
                case "opstate=8":
                    return "非法用户";
                case "opstate=9":
                    return "暂时停止该类卡或者该面值的卡交易";
                case "opstate=10":
                    return "充值卡无效";
                case "opstate=11":
                    return string.Format("支付成功,实际面值{0}元", _ovalue);
                case "opstate=12":
                    return "处理失败卡密未使用";
                case "opstate=13":
                    return "系统繁忙";
                case "opstate=14":
                    return "不存在该笔订单";
                case "opstate=15":
                    return "未知请求";
                case "opstate=16":
                    return "密码错误";
                case "opstate=17":
                    return "匹配订单失败";
                case "opstate=18":
                    return "余额不足";
                case "opstate=19":
                    return "运营商维护";
                case "opstate=20":
                    return "提交次数过多";
                case "opstate=99":
                    return "其他错误";
            }
            return retcode;
        }
        #endregion        

        #region CardNotify
        /// <summary>
        /// 
        /// </summary>
        public void CardNotify()
        {
            HttpRequest req = HttpContext.Current.Request;
            string _orderid = req.QueryString["orderid"];
            string _opstate = req.QueryString["opstate"];
            string _ovalue = req.QueryString["ovalue"];
            string _sign = req.QueryString["sign"];
            string _sysorderid = req.QueryString["sysorderid"];
            string _systime = req.QueryString["systime"];  //

            //'生成MD5签名
            String plain = string.Format("orderid={0}&opstate={1}&ovalue={2}{3}", _orderid, _opstate, _ovalue, SuppKey);
            String localSign = viviLib.Security.Cryptography.MD5(plain);

            try
            {
                if (localSign == _sign)
                {
                    string opstate = "-1";
                    int status = 4;

                    if (_opstate.ToLower() == "0")
                    {
                        opstate = "0";
                        status = 2;
                    }

                    BLL.OrderCard bll = new viviapi.BLL.OrderCard();

                    string suppmsg = GetMsgInfo("opstate=" + opstate, _ovalue);
                    string viewMsg = suppmsg;

                    //bll.ReceiveSuppResult(suppId
                    //    , _orderid
                    //    , _sysorderid
                    //    , status
                    //    , opstate
                    //    , suppmsg
                    //    , viewMsg
                    //    , decimal.Parse(_ovalue)
                    //    , 0M
                    //    , opstate
                    //    , 1);

                    //HttpContext.Current.Response.Write("opstate=0");

                    var response = new CardOrderSupplierResponse()
                    {
                        SupplierId = SuppId,
                        SuppTransNo = _sysorderid,
                        SysOrderNo = _orderid,
                        OrderAmt = decimal.Parse(_ovalue),
                        SuppAmt = 0M,
                        OrderStatus = status,
                        SuppErrorCode = opstate,
                        Opstate = opstate,
                        SuppErrorMsg = suppmsg,
                        ViewMsg = viewMsg,
                        Method = 1
                    };

                    OrderCardUtils.SuppNotify(response, succflag);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
            }
        }
        #endregion

        #region GetCardType
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paytype"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public int GetCardType(int paytype)
        {
            int _cardType = paytype;
            if (paytype == 103)
            {
                _cardType = 13;
            }
            else if (paytype == 104)
            {
                _cardType = 2;
            }
            else if (paytype == 105)
            {
                _cardType = 7;
            }
            else if (paytype == 106)
            {
                _cardType = 3;
            }
            else if (paytype == 107)
            {
                _cardType = 1;
            }
            else if (paytype == 108)
            {
                _cardType = 14;
            }
            else if (paytype == 109)
            {
                _cardType = 8;
            }
            else if (paytype == 110)
            {
                _cardType = 9;
            }
            else if (paytype == 111)
            {
                _cardType = 5;
            }
            else if (paytype == 112)
            {
                _cardType = 6;
            }
            else if (paytype == 113)
            {
                _cardType = 12;
            }
            else if (paytype == 200)
            {
                _cardType = 17;
            }
            else if (paytype == 201)
            {
                _cardType = 18;
            }
            else if (paytype == 202)
            {
                _cardType = 19;
            }
            else if (paytype == 203)
            {
                _cardType = 20;
            }
            else if (paytype == 204)
            {
                _cardType = 10;
            }
            else if (paytype == 205)
            {
                _cardType = 11;
            }
            else if (paytype == 210)
            {
                _cardType = 28;
            }
            return _cardType;
        }
        #endregion


        #region Query
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_cardType"></param>
        /// <param name="_money"></param>
        /// <param name="_cardNo"></param>
        /// <param name="_cardpwd"></param>
        /// <param name="_orderid"></param>
        /// <returns></returns>
        public string Query(string orderid)
        {
            string retText = string.Empty;

            string commitUrl = SuppInfo.queryCardUrl;
            if (string.IsNullOrEmpty(commitUrl))
                return string.Empty;

            orderid = orderid.Trim();
            string plain = String.Format("orderid={0}&parter={1}{2}", orderid, this.SuppAccount,this.SuppKey);
            String sign = viviLib.Security.Cryptography.MD5(plain);

            commitUrl = String.Format("{0}?orderid={1}&parter={2}&sign={3}"
                , commitUrl
                , orderid
                , SuppAccount
                , sign);

             try
            {

                retText = viviLib.Web.WebClientHelper.GetString(commitUrl, null, "GET", System.Text.Encoding.Default);

            }
            catch (Exception ex)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);

                retText = ex.Message;
            }
             return retText;
        }
        #endregion

        #region Finish
       /// <summary>
       /// 
       /// </summary>
       /// <param name="retText"></param>
       /// <returns></returns>
        public bool Finish(string retText)
        {
            bool success = false;

            try
            {
                //orderid=2014040116392194&opstate=&ovalue=0.00&sign=5757b4b2928fd919444a0a55ee90ffb4
                if (!string.IsNullOrEmpty(retText))
                {
                    string[] arr = retText.Split('&');
                    string orderid = arr[0].Replace("orderid=","");
                    string opstate = arr[1].Replace("opstate=", "");
                    string ovalue = arr[2].Replace("ovalue=", "");
                    string sign = arr[3].Replace("sign=", "");

                    string localsign = string.Format("orderid={0}&opstate={0}&ovalue={0}", orderid, opstate, ovalue);
                    localsign = viviLib.Security.Cryptography.MD5(localsign+SuppKey);

                    if (localsign == sign)
                    {
                        string op = string.Empty;

                        int status = 4;
                        if (opstate != "1")
                        {
                            if (opstate == "2")
                            {
                                op = "0";
                            }
                            else
                            {
                                status = 4;
                                op = opstate;
                            }

                            if (!string.IsNullOrEmpty(op))
                            {
                                //BLL.OrderCard bll = new viviapi.BLL.OrderCard();

                                //bll.ReceiveSuppResult(suppId
                                //   , orderid
                                //   , string.Empty
                                //   , status
                                //   , op
                                //   , opstate
                                //   , decimal.Parse(ovalue)
                                //   , 0M
                                //   , opstate);

                                string msg = GetMsgInfo("opstate=" + opstate, ovalue);
                                var response = new CardOrderSupplierResponse()
                                {
                                    SupplierId = SuppId,
                                    SuppTransNo = "",
                                    SysOrderNo = orderid,
                                    OrderAmt = decimal.Parse(ovalue),
                                    SuppAmt = 0M,
                                    OrderStatus = status,
                                    SuppErrorCode = opstate,
                                    Opstate = opstate,
                                    SuppErrorMsg = msg,
                                    ViewMsg = msg,
                                    Method = 1
                                };

                                OrderCardUtils.Finish(response);

                                success = true;
                            }
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
            }

            return success;
        }
        #endregion
    }
}

