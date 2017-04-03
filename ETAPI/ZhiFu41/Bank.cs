using System;
using System.Web;
using viviapi.ETAPI.Common;
using viviapi.Model.supplier;
using viviLib.Security;

namespace viviapi.ETAPI.ZhiFu41
{
    /// <summary>
    /// 41支付接口
    /// </summary>
    public class Bank : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.zhifu41;
        protected readonly string REQ_CUSTOMER_IP = "116.226.209.138";//手动设置请求消费者IP地址，主要是用于测试环境，生产环境请设置为null
        public Bank()
            : base(SuppId)
        {

        }

        public static Bank Instance
        {
            get
            {
                var instance = new Bank();
                return instance;
            }
        }


        internal string Returnurl { get { return this.SiteDomain + "/return/zhifu41/bank.aspx"; } }
        internal string NotifyUrl { get { return this.SiteDomain + "/receive/zhifu41/bank.aspx"; } }
        internal string Succflag = "success";
        internal string Failflag = "fail";
        private string ClientIp
        {
            get
            {
                string ip = null;
                string ipList = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!string.IsNullOrEmpty(ipList))
                {
                    ip = ipList.Split(',')[0];
                }
                else
                {
                    ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                if (ip.IndexOf("::1") != -1)
                {
                    ip = "127.0.0.1";
                }
                if (REQ_CUSTOMER_IP != null)
                    ip = REQ_CUSTOMER_IP;
                return ip;
            }
        }
        #region PayBank
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid">订单ID</param>
        /// <param name="orderAmt">订单金额</param>
        /// <param name="bankcode">银行编号</param>
        /// <param name="autoSubmit">自动提交到测试网关</param>
        /// <returns></returns>
        public string PayBank(string orderid, decimal orderAmt, string bankcode, bool autoSubmit)
        {
            string form_url = PostBankUrl;
            string input_charset = "UTF-8";
            string notify_url = NotifyUrl;
            string return_url = Returnurl;
            string pay_type = "1";
            string bank_code = GetBankCode(bankcode);
            string merchant_code = SuppAccount;
            string order_no = orderid;
            string order_amount = decimal.Round(orderAmt, 2).ToString("0.00");
            string order_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string req_referer = "www.baidu.com";
            string customer_ip = ClientIp;
            string sign = "";


            string plain = "bank_code={0}&customer_ip={1}&input_charset={2}&merchant_code={3}&notify_url={4}&order_amount={5}&order_no={6}&order_time={7}&pay_type={8}&req_referer={9}&return_url={10}&key={11}";
            plain = string.Format(bank_code, customer_ip, input_charset, merchant_code, notify_url, order_amount, order_no, order_no, pay_type, req_referer, return_url, SuppKey);
            SynsSummitLogger("plain: " + plain);
            string SignMD5 = viviLib.Security.Cryptography.MD5(plain, "UTF-8");
            SynsSummitLogger("SignMD5: " + SignMD5);

            //订单支付接口的Md5摘要，原文=订单号+金额+日期+支付币种+商户证书 
            //string SignMD5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Billno + Amount + BillDate + Currency_Type+Mer_key, "MD5").ToLower();

            string postForm = "<form name=\"frm1\" id=\"frm1\" method=\"post\" action=\"" + form_url + "\">";

            postForm += "<input type=\"hidden\" name=\"bank_code\" value=\"" + bank_code + "\" />";
            postForm += "<input type=\"hidden\" name=\"customer_ip\" value=\"" + customer_ip + "\" />";
            postForm += "<input type=\"hidden\" name=\"input_charset\" value=\"" + input_charset + "\" />";
            postForm += "<input type=\"hidden\" name=\"merchant_code\" value=\"" + merchant_code + "\" />";
            postForm += "<input type=\"hidden\" name=\"notify_url\" value=\"" + notify_url + "\" />";
            postForm += "<input type=\"hidden\" name=\"order_amount\" value=\"" + order_amount + "\" />";
            // postForm += "<input type=\"hidden\" name=\"Lang\" value=\"" + Lang + "\" />";
            postForm += "<input type=\"hidden\" name=\"order_no\" value=\"" + order_no + "\" />";
            postForm += "<input type=\"hidden\" name=\"order_time\" value=\"" + order_time + "\" />";
            postForm += "<input type=\"hidden\" name=\"pay_type\" value=\"" + pay_type + "\" />";
            postForm += "<input type=\"hidden\" name=\"req_referer\" value=\"" + req_referer + "\" />";
            postForm += "<input type=\"hidden\" name=\"return_url\" value=\"" + return_url + "\" />";
            postForm += "<input type=\"hidden\" name=\"SuppKey\" value=\"" + SuppKey + "\" />";
            postForm += "<input type=\"hidden\" name=\"sign\" value=\"" + SignMD5 + "\" />";
            postForm += "</form>";

            if (autoSubmit == true)
            {
                //自动提交该表单到测试网关
                postForm +=
                    "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>";
            }

            SynsSummitLogger("SignMD5: " + postForm);
            return postForm;
        }
        #endregion

        #region ReturnBank
        public static readonly string MERCHANT_CODE = "merchant_code";
        public static readonly string NOTIFY_TYPE = "notify_type";
        public static readonly string ORDER_NO = "order_no";
        public static readonly string ORDER_AMOUNT = "order_amount";
        public static readonly string ORDER_TIME = "order_time";
        public static readonly string RETURN_PARAMS = "return_params";
        public static readonly string TRADE_NO = "trade_no";
        public static readonly string TRADE_TIME = "trade_time";
        public static readonly string TRADE_STATUS = "trade_status";
        public static readonly string SIGN = "sign";
        /// <summary>
        /// 
        /// </summary>
        public void ReturnBank()
        {
            string merchantCode = HttpContext.Current.Request.QueryString[MERCHANT_CODE];
            string notifyType = HttpContext.Current.Request.QueryString[NOTIFY_TYPE];
            string orderNo = HttpContext.Current.Request.QueryString[ORDER_NO];
            string orderAmount = HttpContext.Current.Request.QueryString[ORDER_AMOUNT];
            string orderTime = HttpContext.Current.Request.QueryString[ORDER_TIME];
            string returnParams = HttpContext.Current.Request.QueryString[RETURN_PARAMS];
            string tradeNo = HttpContext.Current.Request.QueryString[TRADE_NO];
            string tradeTime = HttpContext.Current.Request.QueryString[TRADE_TIME];
            string tradeStatus = HttpContext.Current.Request.QueryString[TRADE_STATUS];
            string sign = HttpContext.Current.Request.QueryString[SIGN];

            KeyValues kvs = new KeyValues();
            kvs.add(new KeyValue(MERCHANT_CODE, merchantCode));
            kvs.add(new KeyValue(NOTIFY_TYPE, notifyType));
            kvs.add(new KeyValue(ORDER_NO, orderNo));
            kvs.add(new KeyValue(ORDER_AMOUNT, orderAmount));
            kvs.add(new KeyValue(ORDER_TIME, orderTime));
            kvs.add(new KeyValue(RETURN_PARAMS, returnParams));
            kvs.add(new KeyValue(TRADE_NO, tradeNo));
            kvs.add(new KeyValue(TRADE_TIME, tradeTime));
            kvs.add(new KeyValue(TRADE_STATUS, tradeStatus));

            String _sign = kvs.sign(THConfig.merchantKey, THConfig.charset);
            string info = "支付失败";
            if (_sign == sign)
            {
                string opstate = "-1";
                int status = 4;
                if ("success" == tradeStatus)
                {
                    info = "支付成功";
                    opstate = "0";
                    status = 2;
                }
                OrderBankUtils.SuppPageReturn(SuppId
                    , orderNo
                    , tradeNo
                    , status
                    , opstate
                    , info
                    , decimal.Parse(orderAmount), 0M);

                //这个success字符串在支付成功的情况下必须填入，因为交易平台回调商户的后台通知地址后，会通过返回的内容中包含success来判别商户是否收到通知，并成功告知交易平台。
                //这个success字符串只有在商户后台通知时必须填写，页面通知可不填写。

            }
            else
            {
                HttpContext.Current.Response.Write("签名不正确！");
            }
        }
        #endregion

        #region Notify
        /// <summary>
        /// 
        /// </summary>
        public void Notify()
        {
            string merchantCode = HttpContext.Current.Request.QueryString[MERCHANT_CODE];
            string notifyType = HttpContext.Current.Request.QueryString[NOTIFY_TYPE];
            string orderNo = HttpContext.Current.Request.QueryString[ORDER_NO];
            string orderAmount = HttpContext.Current.Request.QueryString[ORDER_AMOUNT];
            string orderTime = HttpContext.Current.Request.QueryString[ORDER_TIME];
            string returnParams = HttpContext.Current.Request.QueryString[RETURN_PARAMS];
            string tradeNo = HttpContext.Current.Request.QueryString[TRADE_NO];
            string tradeTime = HttpContext.Current.Request.QueryString[TRADE_TIME];
            string tradeStatus = HttpContext.Current.Request.QueryString[TRADE_STATUS];
            string sign = HttpContext.Current.Request.QueryString[SIGN];

            KeyValues kvs = new KeyValues();
            kvs.add(new KeyValue(MERCHANT_CODE, merchantCode));
            kvs.add(new KeyValue(NOTIFY_TYPE, notifyType));
            kvs.add(new KeyValue(ORDER_NO, orderNo));
            kvs.add(new KeyValue(ORDER_AMOUNT, orderAmount));
            kvs.add(new KeyValue(ORDER_TIME, orderTime));
            kvs.add(new KeyValue(RETURN_PARAMS, returnParams));
            kvs.add(new KeyValue(TRADE_NO, tradeNo));
            kvs.add(new KeyValue(TRADE_TIME, tradeTime));
            kvs.add(new KeyValue(TRADE_STATUS, tradeStatus));

            String _sign = kvs.sign(THConfig.merchantKey, THConfig.charset);
            if (_sign == sign)
            {
                string opstate = "-1";
                int status = 4;
                if ("success" == tradeStatus)
                {
                    opstate = "0";
                    status = 2;
                }

                OrderBankUtils.SuppNotify(SuppId
                  , orderNo
                  , tradeNo
                  , status
                  , opstate
                  , tradeStatus
                  , decimal.Parse(orderAmount), decimal.Parse(orderAmount)
                  , Succflag
                  , Failflag);
            }
            else
            {
                HttpContext.Current.Response.Write("fail");
            }
        }
        #endregion

        #region GetBankCode
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paymodeId"></param>
        /// <returns></returns>
        public string GetBankCode(string paymodeId)
        {
            string code = string.Empty;
            switch (paymodeId)
            {
                case "970":
                    code = "CMBC";  //招商银行
                    break;
                case "967":
                    code = "ICBC"; //中国工商银行
                    break;
                case "964":
                    code = "ABC"; //中国农业银行
                    break;
                case "965":
                    code = "CCB"; //中国建设银行
                    break;
                case "963":
                    code = "BOC"; //中国银行
                    break;
                case "977":
                    code = "SPDB"; //浦发银行
                    break;
                case "981":
                    code = "BOCOM"; //中国交通银行
                    break;
                case "980":
                    code = "CMBCS"; //中国民生银行
                    break;
                case "974":
                    code = "PINGAN"; //深圳发展银行
                    break;
                case "985":
                    code = "CGB"; //广东发展银行
                    break;
                case "962":
                    code = "ECITIC"; //中信银行
                    break;
                case "982":
                    code = "HXB"; //华夏银行
                    break;
                case "972":
                    code = "CIB"; //兴业银行
                    break;
                //case "984":
                //    code = "00011"; //广州农村商业银行
                //    break;
                //case "1015":
                //    code = "GZCB"; //广州银行
                //    break;
                //case "1016":
                //    code = "CUPS"; //中国银联
                //    break;
                //case "976":
                //    code = "00030"; //上海农村商业银行
                //    break;
                //case "971":
                //    code = "POST"; //中国邮政
                //    break;
                case "989":
                    code = "BCCB"; //北京银行
                    break;
                //case "988":
                //    code = "CBHB"; //渤海银行
                //    break;
                case "990":
                    code = "BRCB"; //北京农商银行
                    break;
                case "979":
                    code = "00055"; //南京银行
                    break;
                case "986":
                    code = "CEBBANK"; //中国光大银行
                    break;
                //case "987":
                //    code = "BEA"; //东亚银行
                //    break;
                //case "1025":
                //    code = "NBCB"; //宁波银行
                //    break;
                case "983":
                    code = "00081"; //杭州银行
                    break;
                case "978":
                    code = "PINGAN"; //平安银行
                    break;
                //case "1028":
                //    code = "HSB"; //徽商银行
                //    break;
                case "968":
                    code = "00086"; //浙商银行
                    break;
                case "975":
                    code = "00084"; //上海银行
                    break;
                case "971":
                    code = "00051"; //中国邮政储蓄银行
                    break;
                //case "1032":
                //    code = "UPOP"; //银联在线支付
                //    break;
            }
            return code;
        }
        #endregion
    }
}
