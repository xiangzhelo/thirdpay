using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Collections.Generic;
using System.Collections.Specialized;
using viviapi.BLL;
using viviapi.ETAPI.Alipay.Lib;
using viviapi.ETAPI.Common;
using viviapi.Model.Common;
using viviapi.Model.Payment;
using viviapi.BLL.Payment;
using viviapi.Model.Order;
using viviapi.Model.supplier;
using viviapi.SysConfig;
using viviapi.Model;
using viviLib.Web;
using viviLib.Logging;

///
namespace viviapi.ETAPI.Alipay
{
    /// <summary>
    /// 直连
    /// </summary>
    public class AliPayMApi : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.Alipay;

        public AliPayMApi()
            : base(SuppId)
        {

        }

        public static AliPayMApi Default
        {
            get
            {
                var instance = new AliPayMApi();
                return instance;
            }
        }

        private string _input_charset = "gbk";
        private string body = PaymentSetting.alipay_body;
        private string subject = PaymentSetting.alipay_subject;
        private string payment_type = "1";
        private string service = "create_direct_pay_by_user";

        internal string notify_url { get { return this.SiteDomain + "/receive/alipay/mapi.aspx"; } }
        internal string return_url { get { return this.SiteDomain + "/return/alipay/mapi.aspx"; } }
        internal string show_url { get { return this.SiteDomain + "/success.htm"; } }

        internal string Succflag = "success";
        internal string Failflag = "fail";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public string GetPayForm(string out_trade_no, decimal amount, string bankid, bool autosumit)
        {
            string partner = SuppAccount;
            string seller_email = SuppUserName;
            string total_fee = amount.ToString("0");

            //默认支付方式
            string paymethod = "bankPay";
            string defaultbank = GetBankCode(bankid);
            string anti_phishing_key = "";
            string exter_invoke_ip = "";

            var sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", partner);
            sParaTemp.Add("_input_charset", _input_charset);
            sParaTemp.Add("service", service);
            sParaTemp.Add("payment_type", payment_type);
            sParaTemp.Add("notify_url", notify_url);
            sParaTemp.Add("return_url", return_url);
            sParaTemp.Add("seller_email", seller_email);
            sParaTemp.Add("out_trade_no", out_trade_no);
            sParaTemp.Add("subject", subject);
            sParaTemp.Add("total_fee", total_fee);
            sParaTemp.Add("body", body);
            sParaTemp.Add("paymethod", paymethod);
            sParaTemp.Add("defaultbank", defaultbank);
            sParaTemp.Add("show_url", show_url);
            //sParaTemp.Add("anti_phishing_key", anti_phishing_key);
            //sParaTemp.Add("exter_invoke_ip", exter_invoke_ip);

            //建立请求
            string sHtmlText = Submit.BuildRequest(sParaTemp, "get", "确认", autosumit);
            //viviLib.Logging.LogHelper.Write(sHtmlText);

            // LogWrite("formHtml=>" + sHtmlText);
            return sHtmlText;
        }

        /// <summary>
        /// WZCBB2C-DEBIT
        /// </summary>
        /// <param name="paymodeId"></param>
        /// <returns></returns>
        public static string GetBankCode(string paymodeId)
        {
            string code = "";
            switch (paymodeId)
            {
                case "970":
                    code = "CMB"; //招商银行
                    break;
                case "967":
                    code = "ICBCB2C"; //中国工商银行
                    break;
                case "964":
                    code = "ABC"; //中国农业银行
                    break;
                case "965":
                    code = "CCB"; //中国建设银行
                    break;
                case "963":
                    code = "BOCB2C"; //中国银行
                    break;
                case "981":
                    code = "COMM"; //中国交通银行
                    break;
                case "980":
                    code = "CMBC"; //中国民生银行
                    break;
                //case "974":
                //    code = "SDB-NET-B2C"; //深圳发展银行
                //    break;
                case "985":
                    code = "GDB"; //广东发展银行
                    break;
                case "962":
                    code = "CITIC"; //中信银行
                    break;
                //case "982":
                //    code = "HXB-NET-B2C"; //华夏银行
                //    break;
                case "972":
                    code = "CIB"; //兴业银行
                    break;
                case "971":
                    code = "POSTGC"; //中国邮政
                    break;
                case "989":
                    code = "BJBANK"; //北京银行
                    break;
                case "988":
                    code = "CBHB-NET-B2C"; //渤海银行
                    break;
                case "990":
                    code = "BJRCB"; //北京农商银行
                    break;
                case "979":
                    code = "NJCB-NET-B2C"; //南京银行
                    break;
                case "986":
                    code = "CEBBANK"; //中国光大银行
                    break;
                case "987":
                    code = "HKBEA-NET-B2C"; //东亚银行
                    break;
                case "997":
                    code = "NBCB-NET-B2C"; //宁波银行
                    break;
                case "978":
                    code = "SPABANK"; //平安银行
                    break;
                case "968":
                    code = "CZ-NET-B2C"; //浙商银行
                    break;
                case "975":
                    code = "SHBANK"; //上海银行
                    break;
                case "976":
                    code = "SHRCB"; //
                    break;
                case "977":
                    code = "SPDB"; //浦发银行
                    break;
                case "983":
                    code = "HZCBB2C"; //杭州银行
                    break;
                case "998":
                    code = "NBBANK"; //宁波银行
                    break;
            }
            return code;
        }

        #region Return
        /// <summary>
        /// 
        /// </summary>
        public void Return()
        {
            SortedDictionary<string, string> sPara = GetRequestGet();
            if (sPara.Count > 0)//判断是否有带返回参数
            {
                string partner = SuppAccount;
                //string pusername = suppUserName;
                string key = SuppKey;

                Notify aliNotify = new Notify();

                bool verifyResult = aliNotify.Verify(sPara
                    , HttpContext.Current.Request.QueryString["notify_id"]
                    , HttpContext.Current.Request.QueryString["sign"]);

                if (verifyResult)//验证成功
                {
                    string opstate = "-1";
                    int status = 4;
                    string msg = "支付失败";
                    decimal result = 0M;

                    string out_trade_no = HttpContext.Current.Request.QueryString["out_trade_no"];
                    string trade_no = HttpContext.Current.Request.QueryString["trade_no"];
                    string trade_status = HttpContext.Current.Request.QueryString["trade_status"];
                    string total_fee = HttpContext.Current.Request.QueryString["total_fee"];

                    if (HttpContext.Current.Request.QueryString["trade_status"] == "TRADE_FINISHED"
                        || HttpContext.Current.Request.QueryString["trade_status"] == "TRADE_SUCCESS")
                    {
                        if (decimal.TryParse(total_fee, out result))
                        {
                            msg = "成功";
                            opstate = "0";
                            status = 2;
                        }
                    }
                    else
                    {
                        msg = "trade_status=" + HttpContext.Current.Request.QueryString["trade_status"];
                    }
                    OrderBankUtils.SuppPageReturn(SuppId
                       , out_trade_no
                       , trade_no
                       , status
                       , opstate
                       , msg
                       , result, 0M);
                }
            }
        }

        public SortedDictionary<string, string> GetRequestGet()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = HttpContext.Current.Request.QueryString;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], HttpContext.Current.Request.QueryString[requestItem[i]]);
            }

            return sArray;
        }
        #endregion

        #region Notify
        /// <summary>
        /// 
        /// </summary>
        public void Notify()
        {
            string callbackText = "";

            SortedDictionary<string, string> sPara = GetRequestPost();
            if (sPara.Count > 0)//判断是否有带返回参数
            {
                string partner = SuppAccount;
                //string pusername = suppUserName;
                string key = SuppKey;

                Notify aliNotify = new Notify();

                bool verifyResult = aliNotify.Verify(sPara, HttpContext.Current.Request.Form["notify_id"], HttpContext.Current.Request.Form["sign"]);
                if (verifyResult)//验证成功
                {
                    string opstate = "-1";
                    int status = 4;
                    string msg = "支付失败";
                    decimal result = 0M;

                    string out_trade_no = HttpContext.Current.Request.Form["out_trade_no"];
                    string trade_no = HttpContext.Current.Request.Form["trade_no"];
                    string trade_status = HttpContext.Current.Request.Form["trade_status"];
                    string total_fee = HttpContext.Current.Request.Form["total_fee"];

                    if (HttpContext.Current.Request.Form["trade_status"] == "TRADE_FINISHED"
                        || HttpContext.Current.Request.Form["trade_status"] == "TRADE_SUCCESS")
                    {
                        if (decimal.TryParse(total_fee, out result))
                        {
                            msg = "成功";
                            opstate = "0";
                            status = 2;
                        }
                    }
                    else
                    {
                        msg = "trade_status=" + HttpContext.Current.Request.Form["trade_status"];
                    }

                    OrderBankUtils.SuppNotify(SuppId
                      , out_trade_no
                      , trade_no
                      , status
                      , opstate
                      , msg
                      , result, 0M
                      , Succflag
                      , Failflag);
                }
            }
        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = HttpContext.Current.Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], HttpContext.Current.Request.Form[requestItem[i]]);
            }

            return sArray;
        }
        #endregion
    }
}




