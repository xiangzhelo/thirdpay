using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using netdemo.com.shengpay.cardpay.payment;
using netdemo.com.shengpay.cardpay.queryOrder;
using netdemo.com.shengpay.cardpay.receive;
using Newtonsoft.Json;
using viviapi.ETAPI.Common;
using viviapi.Model.Order.Card;
using viviapi.Model.supplier;
using viviapi.SysConfig;
using viviapi.SysInterface.Card;
using viviapi.SysInterface.Card.MyAPI;
using viviLib.Security;

using extension = netdemo.com.shengpay.cardpay.payment.extension;
using header = netdemo.com.shengpay.cardpay.payment.header;
using sender = netdemo.com.shengpay.cardpay.payment.sender;
using service = netdemo.com.shengpay.cardpay.payment.service;
using signature = netdemo.com.shengpay.cardpay.payment.signature;

namespace viviapi.ETAPI.Shengpay
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

        public static Shengpay.Card Default
        {
            get
            {
                var card = new Shengpay.Card();
                return card;
            }
        }

        public string _pageUrl
        {
            get
            {
                return SiteDomain + "/receive/shengpay/card.aspx";
            }
        }

        public string _notifyUrl
        {
            get
            {
                return SiteDomain + "/receive/shengpay/card.aspx";
            }
        }

        internal string Succflag = "ok";
        private string SenderId
        {
            get
            {
                return SuppInfo.puserid1;
            }
        }

        private string MerchantKey
        {
            get
            {
                return SuppInfo.puserkey1;
            }
        }

        #region CardSend
        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public CardSynchCallBack CardSend(CardOrderSummitArgs o)
        {
            var callBack = new CardSynchCallBack();
            var channel = GetChannelInfo(o.CardTypeId, o.CardNo);

            if (string.IsNullOrEmpty(channel.PayChannel))
            {
                callBack.SuppErrorCode = "-1";
                callBack.SuppErrorMsg = "不支持的卡种";

                callBack.Message = "不支持的卡种";
                return callBack;
            }

            ReceB2COrderResponse raceResponse = ReceiveB2COrderRep(o.SysOrderNo, o.SysOrderNo, o.FaceValue);
            if (raceResponse == null || raceResponse.transStatus != "00")
            {
                callBack.SuppErrorCode = "-1";
                callBack.SuppErrorMsg = "下单失败";

                callBack.Message = "下单失败";
                return callBack;
            }

            b2CPaymentResponse b2CPayment = Payment(raceResponse.tokenId, raceResponse.sessionId, o.SysOrderNo, o.CardNo,
                o.CardPass, o.CardTypeId, o.FaceValue);

            if (b2CPayment == null)
            {
                callBack.SuppErrorCode = "-1";
                callBack.SuppErrorMsg = "支付失败";

                callBack.Message = "支付失败";
                return callBack;
            }

            callBack.Success = 1;
            callBack.SuppErrorCode = b2CPayment.returnInfo.errorCode;
            callBack.SuppErrorMsg = b2CPayment.returnInfo.errorMsg;
            callBack.SuppTransNo = b2CPayment.transNo;

            //if (b2CPayment.transStatus == "00" 
            //    && string.IsNullOrEmpty(b2CPayment.returnInfo.errorCode))
            //{
            //    callBack.SummitStatus = 1;
            //}

            //if (b2CPayment.transStatus == "01")
            //{
            //    callBack.SummitStatus = 1;

            //    if (o.CardTypeId == 104
            //        || o.CardTypeId == 210)
            //    {
            //        callBack.OrderStatus  = 2;
            //    }
            //}

            if (!string.IsNullOrEmpty(b2CPayment.returnInfo.errorCode))
            {
                callBack.SummitStatus = 0;
                callBack.OrderStatus = 0;
            }
            else
            {
                if (b2CPayment.transStatus == "00")
                {
                    callBack.SummitStatus = 1;
                }
                if (b2CPayment.transStatus == "01")
                {
                    callBack.SummitStatus = 1;

                    if (o.CardTypeId == 104
                        || o.CardTypeId == 210)
                    {
                        decimal paidAmount = 0M;

                        if (decimal.TryParse(b2CPayment.paidAmount, out paidAmount))
                        {
                            callBack.OrderStatus = 2;
                            callBack.SuccAmt = paidAmount;
                        }
                       
                    }
                }
            }

            return callBack;
        }
        #endregion

        #region ReceiveB2COrderRep
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="traceNo"></param>
        /// <param name="cardvalue"></param>
        /// <returns></returns>
        public ReceB2COrderResponse ReceiveB2COrderRep(string orderid, string traceNo, int cardvalue)
        {
            //商户密钥
            // string MerchantKey = suppKey;
            //商户号
            var send = new netdemo.com.shengpay.cardpay.receive.sender();
            send.senderId = SenderId;

            var ser = new netdemo.com.shengpay.cardpay.receive.service();
            ser.serviceCode = "B2CPayment";
            ser.version = "V4.1.1.1.1";

            var head = new netdemo.com.shengpay.cardpay.receive.header();
            head.service = ser;
            head.charset = "UTF-8";
            //网关跟踪号, 保证唯一
            head.traceNo = traceNo;
            head.sender = send;
            head.sendTime = DateTime.Now.ToString("yyyyMMddhhmmss");

            var ext = new netdemo.com.shengpay.cardpay.receive.extension();
            ext.ext1 = "";
            ext.ext2 = "";

            var sig = new netdemo.com.shengpay.cardpay.receive.signature();
            sig.signMsg = "";
            sig.signType = "MD5";

            ReceB2COrderRequest request = new ReceB2COrderRequest();
            request.header = head;
            //商户订单号(商户根据实际情况自行更改), 需保证唯一
            request.orderNo = orderid;
            //订单金额(商户根据实际情况自行更改), 最小单位(元)
            request.orderAmount = cardvalue.ToString();
            //提交订单时间
            request.orderTime = DateTime.Now.ToString("yyyyMMddhhmmss");
            request.currency = "CNY";
            request.language = "zh-CN";
            //页面通知地址(商户根据实际情况自行更改)
            request.pageUrl = _pageUrl;
            //后台通知地址(商户根据实际情况自行更改)
            request.notifyUrl = _notifyUrl;
            request.signature = sig;
            request.extension = ext;
            //以下根据商户自身需求按照文档描述自行添加
            request.buyerContact = "";
            request.buyerId = "";
            request.buyerIp = "";
            request.buyerName = "";
            request.cardPayInfo = "";
            request.cardValue = "";
            request.depositId = "";
            request.depositIdType = "";
            //订单过期时间
            request.expireTime = "";
            request.instCode = "";
            request.payChannel = "";
            request.payType = "";
            request.payeeId = "";
            request.payerAuthTicket = "";
            request.payerId = "";
            request.payerMobileNo = "";
            request.productDesc = "";
            request.productId = "";
            request.productName = "";
            request.productNum = "";
            request.productUrl = "";
            request.sellerId = "";
            request.terminalType = "";
            request.unitPrice = "";

            //签名字符串拼接
            string sign = ser.serviceCode + ser.version
                + head.charset + head.traceNo + send.senderId + head.sendTime
                + request.orderNo + request.orderAmount + request.orderTime
                + request.expireTime + request.currency + request.payType
                + request.payChannel + request.instCode + request.cardValue
                + request.language + request.pageUrl + request.notifyUrl
                + request.terminalType + request.productId + request.productName
                + request.productNum + request.unitPrice + request.productDesc
                + request.productUrl + request.sellerId + request.buyerName
                + request.buyerId + request.buyerContact + request.buyerIp
                + request.payeeId + request.depositId + request.depositIdType
                + request.payerId + request.cardPayInfo + request.payerMobileNo
                + request.payerAuthTicket + ext.ext1 + ext.ext2 + sig.signType + MerchantKey;

            //viviLib.Logging.LogHelper.Write(sign);

            sig.signMsg = MD5(sign);
            request.signature = sig;

            try
            {
                ReceiveOrderAPIExplorterService receiveService = new ReceiveOrderAPIExplorterService();
                ReceB2COrderResponse response = receiveService.receiveB2COrder(request);
                //viviLib.Logging.LogHelper.Write(response.transStatus);
                return response;
            }
            catch (Exception ex)
            {
                //viviLib.Logging.LogHelper.Write(ex.Message);
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
            }

            return null;
        }

        #endregion

        #region Payment
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="sessionId"></param>
        /// <param name="orderid"></param>
        /// <param name="cardno"></param>
        /// <param name="cardpwd"></param>
        /// <param name="typeId"></param>
        /// <param name="cardvalue"></param>
        /// <returns></returns>
        public b2CPaymentResponse Payment(string tokenId, string sessionId, string orderid, string cardno, string cardpwd, int typeId, int cardvalue)
        {
            var channel = GetChannelInfo(typeId, cardno);

            sender send = new sender();
            send.senderId = SenderId;

            service ser = new service();
            ser.serviceCode = "B2CPayment";
            ser.version = "V4.1.1.1.1";

            header head = new header();
            head.service = ser;
            head.charset = "UTF-8";
            //网关跟踪号, 保证唯一
            head.traceNo = orderid;
            head.sender = send;
            head.sendTime = DateTime.Now.ToString("yyyyMMddhhmmss");

            b2COrder ord = new b2COrder();
            ord.orderAmoumt = cardvalue.ToString();
            ord.orderType = "OT001";
            ord.transNo = orderid;

            paymentItem ipitem = new paymentItem();
            ipitem.key = "PAYER_IP";
            ipitem.value = "127.0.0.1";


            /* 
           游戏卡:卡号_密码_面值@@卡号_密码_面值, 多卡用@@分隔
           盛付通卡:卡号_密码@@卡号_密码, 多卡用@@分隔
            */
            paymentItem carditem = new paymentItem();
            carditem.key = "CARD_INFO";
            //carditem.value = "2013091100005018_111111_10@@2013091100005019_111111_10"
            if (channel.PayChannel == "03"
                || channel.PayChannel == "31"
                || channel.PayChannel == "42")
            {
                carditem.value = string.Format("{0}_{1}", cardno, cardpwd);
            }
            else
            {
                carditem.value = string.Format("{0}_{1}_{2}", cardno, cardpwd, cardvalue);
            }


            paymentItem[] paymentItems = new paymentItem[2] { ipitem, carditem };

            /*
            #region 游戏卡
            b2CPayment payment = new b2CPayment();
            //支付渠道, (商户根据实际情况自行更改), 具体代码含义详见文档
            payment.paymentType = "";
            payment.instCode = "";
            payment.payChannel = "61";
            payment.paymentItems = paymentItems;
            #endregion
             * */

            #region 盛付通卡
            b2CPayment payment = new b2CPayment();
            //支付渠道, (商户根据实际情况自行更改), 具体代码含义详见文档
            payment.paymentType = channel.PaymentType;
            payment.instCode = channel.InstCode;
            payment.payChannel = channel.PayChannel;
            payment.paymentItems = paymentItems;
            #endregion

            extension ext = new extension();
            ext.ext1 = "";
            ext.ext2 = "";

            signature sig = new signature();
            sig.signMsg = "";
            sig.signType = "MD5";

            b2CPayer payer = new b2CPayer();
            payer.ptId = "";
            payer.ptIdType = memberIdType.PT_ID;
            payer.sdId = "";
            payer.memberId = "";
            payer.accountId = "";
            payer.accountType = "";
            payer.payableAmount = "";
            payer.payableFee = "";

            b2CPayee payee = new b2CPayee();
            payee.ptId = "";
            payee.sdId = "";
            payee.memberId = "";
            payee.accountId = "";
            payee.accountType = "";
            payee.receivableAmount = "";
            payee.receivableFee = "";

            b2CPaymentRequest request = new b2CPaymentRequest();
            request.header = head;
            request.order = ord;
            request.payer = payer;
            request.payee = payee;
            request.payment = payment;
            request.tokenId = tokenId;
            request.sessionId = sessionId;// Guid.NewGuid().ToString("N");
            request.extension = ext;
            request.signature = sig;

            //签名字符串拼接
            string sign = ser.serviceCode + ser.version
                     + head.charset + head.traceNo + send.senderId + head.sendTime
                     + ord.transNo + ord.orderAmoumt + ord.orderType
                     + payer.ptId + payer.ptIdType + payer.sdId + payer.memberId
                     + payer.accountId + payer.accountType + payer.accountType
                     + payer.payableAmount + payer.payableFee
                     + payee.ptId + payee.sdId + payee.memberId
                     + payee.accountId + payee.accountType + payee.accountType
                     + payee.receivableAmount + payee.receivableFee
                     + payment.paymentType + payment.instCode + payment.payChannel;

            for (int i = 0; i < paymentItems.Length; i++)
            {
                sign += paymentItems[i].key + paymentItems[i].value;
            }
            sign += request.tokenId + request.sessionId
                + ext.ext1 + ext.ext2 + sig.signType + MerchantKey;

            sig.signMsg = Cryptography.MD5(sign);
            request.signature = sig;


            try
            {
                PaymentAPIExporterService service = new PaymentAPIExporterService();
                b2CPaymentResponse response = service.processB2CPay(request);


                //viviLib.Logging.LogHelper.Write("pay" + response.transStatus);
                return response;

            }
            catch (Exception exception)
            {
                //viviLib.Logging.LogHelper.Write("pay exception" + exception.Message);
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(exception);
            }
            return null;
        }
        #endregion

        #region Notify
        /// <summary>
        /// 
        /// </summary>
        public void Notify()
        {
            //string MerchantKey = suppKey;
            string name = GetValue("Name");
            string version = GetValue("Version");
            string charset = GetValue("Charset");
            string traceNo = GetValue("TraceNo");
            string msgSender = GetValue("MsgSender");
            string sendTime = GetValue("SendTime");
            string instCode = GetValue("InstCode");
            string orderNo = GetValue("OrderNo");
            string orderAmount = GetValue("OrderAmount");
            string transNo = GetValue("TransNo");
            string transAmount = GetValue("TransAmount");
            string transStatus = GetValue("TransStatus");
            string transType = GetValue("TransType");
            string transTime = GetValue("TransTime");
            string merchantNo = GetValue("MerchantNo");
            string errorCode = GetValue("ErrorCode");
            string errorMsg = GetValue("ErrorMsg");
            string ext1 = GetValue("Ext1");
            string signType = GetValue("SignType");
            string signMsg = GetValue("SignMsg");

            string sign = name + version + charset + traceNo + msgSender
                          + sendTime + instCode + orderNo + orderAmount + transNo
                          + transAmount + transStatus + transType + transTime
                          + merchantNo + errorCode + errorMsg + ext1 + signType + MerchantKey;
            string localsignMsg = MD5(sign);

            
            if (signMsg == localsignMsg)
            {
                var bll = new viviapi.BLL.OrderCard();

                string opstate = "-1";
                int status = (transStatus == "01") ? 2 : 4;



                string viewMsg = "";

                string errCode = "";
                string errMsg = "";

                if (!string.IsNullOrEmpty(errorMsg))
                {
                    var ja = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(errorMsg);
                    foreach (Newtonsoft.Json.Linq.JObject item in ja)
                    {
                        if (item["errorCode"] != null)
                        {
                            errCode = item["errorCode"].ToString().Replace("\"", "");
                        }
                        if (item["errorMsg"] != null)
                        {
                            errMsg = item["errorMsg"].ToString().Replace("\"", "");
                        }
                    }
                }

                if (status == 2)
                {
                    opstate = "0";
                    errorCode = "0";
                    viewMsg = "支付成功";
                }
                else
                {
                    opstate = ConvertCode(errCode);
                    viewMsg = SysInterface.Card.MyAPI.Utility.GetMessageByCode(opstate);
                }

                var response = new CardOrderSupplierResponse()
                {
                    SupplierId = SuppId,
                    SuppTransNo = transNo,
                    SysOrderNo = orderNo,
                    OrderAmt = decimal.Parse(transAmount),
                    SuppAmt = 0M,
                    OrderStatus = status,
                    SuppErrorCode = errCode,
                    SuppErrorMsg = errMsg,
                    Opstate = opstate,
                    ViewMsg = viewMsg,
                    Method = 1
                };

                OrderCardUtils.SuppNotify(response, "OK");
            }
        }
        #endregion

        #region QueryOrder

        public void Query(string orderNo)
        {
            orderQueryResponse response = QueryOrder(orderNo, "");

            if (response != null)
            {
                //string transStatus = response.transStatus;
                //string errCode = response.returnInfo.errorCode;
                //string viewMsg = response.returnInfo.errorMsg;
                //string transNo = response.transNo;
                //string orderNo1 = response.orderNo;
                //string transAmoumt = response.transAmoumt;
                //string text =
                //    string.Format("盛付通查询：orderNo:{0}transStatus:{1}errCode:{2}viewMsg:{3}transNo:{4}transAmoumt{5}"
                //        , orderNo, transStatus, errCode, viewMsg, transNo, transAmoumt);

                ////viviLib.Logging.LogHelper.Write(text);
                Finish(response);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="transNo"></param>
        /// <returns></returns>
        public orderQueryResponse QueryOrder(string orderNo, string transNo)
        {
            //商户订单号
            //网关订单号
            // string transNo = "C20130923183918278856";
            //商户密钥
            //string MerchantKey = suppKey;

            //商户号
            netdemo.com.shengpay.cardpay.queryOrder.sender send = new netdemo.com.shengpay.cardpay.queryOrder.sender();
            send.senderId = SenderId;

            netdemo.com.shengpay.cardpay.queryOrder.service ser = new netdemo.com.shengpay.cardpay.queryOrder.service();
            ser.serviceCode = "QUERY_ORDER_REQUEST";
            ser.version = "V4.3.1.1.1";

            netdemo.com.shengpay.cardpay.queryOrder.header head = new netdemo.com.shengpay.cardpay.queryOrder.header();
            head.service = ser;
            head.charset = "UTF-8";
            //网关跟踪号, 保证唯一
            head.traceNo = DateTime.Now.ToString("yyyyMMddhhmmss");
            head.sender = send;
            head.sendTime = DateTime.Now.ToString("yyyyMMddhhmmss");

            netdemo.com.shengpay.cardpay.queryOrder.extension ext = new netdemo.com.shengpay.cardpay.queryOrder.extension();
            ext.ext1 = "";
            ext.ext2 = "";

            netdemo.com.shengpay.cardpay.queryOrder.signature sig = new netdemo.com.shengpay.cardpay.queryOrder.signature();
            sig.signMsg = "";
            sig.signType = "MD5";

            netdemo.com.shengpay.cardpay.queryOrder.orderQueryRequest request = new netdemo.com.shengpay.cardpay.queryOrder.orderQueryRequest();
            request.header = head;
            request.merchantNo = "107537";
            //orderNo和transNo必填一个, 优先级transNo大于orderNo
            request.orderNo = orderNo;
            request.transNo = transNo;
            request.extension = ext;
            request.signature = sig;

            //签名字符串拼接
            string sign = ser.serviceCode + ser.version
                    + head.charset + head.traceNo + send.senderId + head.sendTime
                    + request.merchantNo + request.orderNo + request.transNo
                    + ext.ext1 + ext.ext2 + sig.signType + MerchantKey;

            sig.signMsg = MD5(sign);
            request.signature = sig;

            try
            {
                QueryOrderAPIExporterService service = new QueryOrderAPIExporterService();
                orderQueryResponse response = service.queryOrder(request);
                //txtOrderNo.Text = response.orderNo;
                //txtTransNo.Text = response.transNo;
                //txtTransStatus.Text = response.transStatus;
                //txtOrderAmount.Text = response.orderAmount;
                //txtTransAmoumt.Text = response.transAmoumt;
                //txtTransTime.Text = response.transTime;

                return response;

            }
            catch (Exception ex)
            {

            }

            return null;
        }
        #endregion

        #region GetChannelInfo
        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="cardno"></param>
        /// <returns></returns>
        private PayChannelInfo GetChannelInfo(int typeId, string cardno)
        {
            var info = new PayChannelInfo();

            switch (typeId)
            {
                case 104:
                case 210:
                    {
                        #region 盛大卡
                        const string sdykt = "^(CA|CS|S1|S2|S3)";
                        const string sftk = "^(YA|YB)";
                        const string yffk = "^(801331)";
                        const string sdfdylk = "^(YC|YD)";
                        const string sdfylylk = "^(801335|801336|801337|801338|801340)";


                        if (QuickValidate(sdykt, cardno))
                        {
                            #region 盛大一卡通
                            info = new PayChannelInfo()
                            {
                                PaymentType = "PT008",
                                PayChannel = "03",
                                InstCode = "SNDA"
                            };
                            #endregion
                        }
                        else if (QuickValidate(sftk, cardno))
                        {
                            #region 盛付通卡
                            info = new PayChannelInfo()
                            {
                                PaymentType = "PT003",
                                PayChannel = "27",
                                InstCode = "SNDA"
                            };
                            #endregion
                        }
                        else if (QuickValidate(yffk, cardno))
                        {
                            #region 线上预付费卡
                            info = new PayChannelInfo()
                            {
                                PaymentType = "PT013",
                                PayChannel = "29",
                                InstCode = "SNDA"
                            };
                            #endregion
                        }
                        else if (QuickValidate(sdfdylk, cardno))
                        {
                            #region 盛大互动娱乐卡
                            info = new PayChannelInfo()
                            {
                                PaymentType = "PT018",
                                PayChannel = "31",
                                InstCode = "SNDA"
                            };
                            #endregion
                        }
                        else if (QuickValidate(sdfylylk, cardno))
                        {
                            #region 盛大通娱乐一卡通
                            info = new PayChannelInfo()
                            {
                                PaymentType = "PT020",
                                PayChannel = "42",
                                InstCode = "SNDA"
                            };
                            #endregion
                        }
                        #endregion
                    }
                    break;
                case 106://骏网一卡通
                    info = new PayChannelInfo()
                    {
                        PaymentType = "PT030",
                        PayChannel = "60",
                        InstCode = "JWK"
                    };
                    break;
                case 105:
                    info = new PayChannelInfo()
                    {
                        PaymentType = "PT030",
                        PayChannel = "61",
                        InstCode = "ZTK"
                    };
                    break;
                case 111:
                    info = new PayChannelInfo()
                    {
                        PaymentType = "PT030",
                        PayChannel = "62",
                        InstCode = "WMK"
                    };
                    break;
                case 110:
                    info = new PayChannelInfo()
                    {
                        PaymentType = "PT030",
                        PayChannel = "63",
                        InstCode = "WMK"
                    };
                    break;
                case 112:
                    info = new PayChannelInfo()
                    {
                        PaymentType = "PT030",
                        PayChannel = "64",
                        InstCode = "SHK"
                    };
                    break;
                case 117:
                    info = new PayChannelInfo()
                    {
                        PaymentType = "PT030",
                        PayChannel = "65",
                        InstCode = "ZYH"
                    };
                    break;
                case 107:
                    info = new PayChannelInfo()
                    {
                        PaymentType = "PT030",
                        PayChannel = "66",
                        InstCode = "TXK"
                    };
                    break;
                case 119:
                    info = new PayChannelInfo()
                    {
                        PaymentType = "PT030",
                        PayChannel = "67",
                        InstCode = "THK"
                    };
                    break;
                case 109:
                    info = new PayChannelInfo()
                    {
                        PaymentType = "PT030",
                        PayChannel = "68",
                        InstCode = "JYK"
                    };
                    break;
                case 118:
                    info = new PayChannelInfo()
                    {
                        PaymentType = "PT030",
                        PayChannel = "73",
                        InstCode = "TXTK"
                    };
                    break;
                case 103:
                    info = new PayChannelInfo()
                    {
                        PaymentType = "PT030",
                        PayChannel = "75",
                        InstCode = "CM"
                    };
                    break;
                case 108:
                    info = new PayChannelInfo()
                    {
                        PaymentType = "PT030",
                        PayChannel = "76",
                        InstCode = "UC"
                    };
                    break;
                case 113:
                    info = new PayChannelInfo()
                    {
                        PaymentType = "PT030",
                        PayChannel = "77",
                        InstCode = "CT"
                    };
                    break;
            }

            return info;
        }
        #endregion

        #region ConvertCode
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errcode"></param>
        /// <returns></returns>
        public string ConvertCode(string errcode)
        {
            #region Shengpay
            string syscode = "99";
            switch (errcode)
            {
                case "0":
                    syscode = "0";
                    break;
                case "E1004"://充值卡类型有误
                case "F1011"://卡类型有误
                    syscode = "2";
                    break;
                case "F1032"://充值卡处理中
                    syscode = "4";//订单内容重复
                    break;
                case "E1007"://订单参数错误
                case "F0601"://订单参数错误
                case "F1021"://金额为数字
                    syscode = "7";//数据非法
                    break;
                case "F0501"://商户没开通
                case "F1034"://商户没开通
                    syscode = "8";//非法用户
                    break;
                case "F1003"://不支持此种卡
                case "F1023"://不支持此种卡
                    syscode = "9";//暂停该类卡
                    break;
                case "E1001"://系统繁忙，请稍后
                    syscode = "13";
                    break;
                case "B053070":
                case "B053051":
                case "B053050":
                case "B053049":
                case "F1022"://卡失效
                    syscode = "10"; //充值卡无效
                    break;
                //case "2011":
                //    syscode = "11"; //支付成功,实际面值{0}元
                //    break;
                case "F1054":
                case "F1055":
                    syscode = "12";//支付失败卡密未使用
                    break;
                case "F1100"://系统繁忙，请稍后
                    syscode = "13";//系统繁忙
                    break;
                case "F1038"://卡未激活
                    syscode = "15";
                    break;
                case "B053071":
                case "F0304"://卡号或者卡密不对
                case "F0401"://卡号或者卡密不对
                case "F1039"://卡号或者卡密不对
                    syscode = "16";
                    break;
                //case "2017":
                //    syscode = "17";
                //    break;
                case "S0513009":
                case "S0513008":
                case "B0513009":
                case "B053052":
                case "F0201"://余额不足
                    syscode = "18";
                    break;
                case "E1002"://系统维护中
                case "E1003"://系统维护中
                case "E1005"://系统维护中
                case "E1008"://系统维护中
                case "F0101"://系统维护中
                case "F1043"://系统维护中
                    syscode = "19";
                    break;
                case "F0205"://操作过于频繁
                case "F0402"://操作过于频繁
                case "F1040"://请不要重复递交错误信息
                    syscode = "20";
                    break;
                case "F0203"://面值有误
                case "E0001"://IP地址不安全
                case "E1006"://网络通讯故障
                case "F1020"://面值有误
                case "F1050"://金额有误
                    syscode = "99";
                    break;
            }

            return syscode;

            #endregion
        }
        #endregion

        #region Finish
        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback"></param>
        public void Finish(orderQueryResponse callback)
        {
            if (callback != null)
            {
                string opstate = "";
                int status = 4;

                string transStatus = callback.transStatus;
                string errCode = callback.returnInfo.errorCode;
                string errorMsg = callback.returnInfo.errorMsg;
                if (errCode == "B0532006")
                    return;

                errCode = "";
                string errMsg = "";

                if (!string.IsNullOrEmpty(errorMsg))
                {
                    var ja = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(errorMsg);
                    foreach (Newtonsoft.Json.Linq.JObject item in ja)
                    {
                        if (item["errorCode"] != null)
                        {
                            errCode = item["errorCode"].ToString().Replace("\"", "");
                        }
                        if (item["errorMsg"] != null)
                        {
                            errMsg = item["errorMsg"].ToString().Replace("\"", "");
                        }
                    }
                }

                if (errCode == "F1032")
                    return;

                switch (transStatus)
                {
                    case "":
                    case "00":
                        opstate = "";
                        break;
                    case "01":
                        errCode = "0";
                        opstate = "0";
                        status = 2;
                        break;
                    case "02":
                    case "03":
                        opstate = "-1";
                        status = 4;
                        break;
                }

                if (!string.IsNullOrEmpty(opstate))
                {
                    string viewMsg = errMsg;
                    var response = new CardOrderSupplierResponse()
                    {
                        SupplierId = SuppId,
                        SuppTransNo = callback.transNo,
                        SysOrderNo = callback.orderNo,
                        OrderAmt = decimal.Parse(callback.transAmoumt),
                        SuppAmt = 0M,
                        OrderStatus = status,
                        SuppErrorCode = errCode,
                        Opstate = opstate,
                        SuppErrorMsg = callback.returnInfo.errorMsg,
                        ViewMsg = viewMsg,
                        Method = 1
                    };

                    OrderCardUtils.Finish(response);
                }
            }
        }
        #endregion

        #region Funtion
        private string MD5(string input)
        {
            MD5 myMD5 = new MD5CryptoServiceProvider();
            byte[] signed = myMD5.ComputeHash(Encoding.UTF8.GetBytes(input));
            string signResult = byte2mac(signed);
            return signResult.ToUpper();
        }
        private static string byte2mac(byte[] signed)
        {
            StringBuilder EnText = new StringBuilder();
            foreach (byte Byte in signed)
            {
                EnText.AppendFormat("{0:x2}", Byte);
            }

            return EnText.ToString();
        }

        private bool QuickValidate(string express, string value)
        {
            var regex = new Regex(express, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            return regex.IsMatch(value);
        }

        public string GetValue(string key)
        {
            return System.Web.HttpContext.Current.Request.Form[key];
        }

        #endregion

    }
}
