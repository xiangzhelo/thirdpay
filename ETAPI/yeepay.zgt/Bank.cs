using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using viviapi.ETAPI.Common;
using viviapi.ETAPI.YeePay.ZGT;
using viviapi.Model.supplier;

namespace viviapi.ETAPI.YeePay.ZGT
{
    public class Bank : ETAPIBase
    {
        private static int suppid = (int)SupplierCode.YeePayZGT;
        public Bank() : base(suppid)
        {
        }
        /// <summary>
        /// 支付信息提交
        /// </summary>
        /// <param name="orderid">订单号</param>
        /// <param name="orderAmt">金额（元）</param>
        /// <param name="autoSubmit">是否自动提交</param>
        /// <returns></returns>
        public string PayBank(string orderid, decimal orderAmt, string bankCode, bool autoSubmit)
        {
            //请求移动终端网页收银台支付

            //一键支付URL前缀
            string apiprefix = APIURLConfig.mobilePrefix;

            //网页支付地址
            string mobilepayURI = APIURLConfig.webpayURI;

            //商户账户编号
            string customernumber = this.MerchantAccount;
            string hmacKey = this.MerchantKey;
            string AesKey = this.AesKey;

            //日志字符串
            StringBuilder logsb = new StringBuilder();
            logsb.Append(DateTime.Now.ToString() + "\n");

            Random ra = new Random();
            string payproducttype = "SALES"; // "支付方式";
            string requestid = orderid;//订单号
            string amount = orderAmt.ToString();//金额
            string productcat = "";//商品类别码，商户支持的商品类别码由易宝支付运营人员根据商务协议配置
            string productdesc = "";//商品描述
            string productname = "在线储值";//商品名称
            string assure = "0";//是否需要担保,1是，0否
            string divideinfo = "";//分账信息，格式”ledgerNo:分账比
            string bankid = GetBankCode(bankCode);//银行编码
            string period = "";//担保有效期，单位 ：天；当assure=1 时必填，最大值：30
            string memo = "";//商户备注

            //商户提供的商户后台系统异步支付回调地址
            string callbackurl = CallbackUrl;
            //商户提供的商户前台系统异步支付回调地址
            string webcallbackurl = "";
            string hmac = "";


            hmac = Digest.GetHMAC(customernumber, requestid, amount, assure, productname, productcat, productdesc, divideinfo, callbackurl, webcallbackurl, bankid, period, memo, hmacKey);

            SortedDictionary<string, object> sd = new SortedDictionary<string, object>();
            sd.Add("customernumber", customernumber);
            sd.Add("amount", amount);
            sd.Add("requestid", requestid);
            sd.Add("assure", assure);
            sd.Add("productname", productname);
            sd.Add("productcat", productcat);
            sd.Add("productdesc", productdesc);
            sd.Add("divideinfo", divideinfo);
            sd.Add("callbackurl", callbackurl);
            sd.Add("webcallbackurl", webcallbackurl);
            sd.Add("bankid", bankid);
            sd.Add("period", period);
            sd.Add("memo", memo);
            sd.Add("payproducttype", payproducttype);
            sd.Add("hmac", hmac);



            //将网页支付对象转换为json字符串
            string wpinfo_json = Newtonsoft.Json.JsonConvert.SerializeObject(sd);
            logsb.Append("网银支付明文数据json格式为：" + wpinfo_json + "\n");

            string datastring = AESUtil.Encrypt(wpinfo_json, AesKey);

            logsb.Append("网银支付业务数据经过AES加密后的值为：" + datastring + "\n");



            //打开浏览器访问一键支付网页支付链接地址，请求方式为get
            string postParams = "data=" + HttpUtility.UrlEncode(datastring) + "&customernumber=" + customernumber;
            string url = apiprefix + mobilepayURI + "?" + postParams;

            logsb.Append("网银支付链接地址为：" + url + "\n");

#if DEBUG
            SoftLog.LogStr(logsb.ToString(), "ECPayLog");
#endif
            string ybResult = YJPayUtil.payAPIRequest(apiprefix + mobilepayURI, datastring, false, this.MerchantAccount);

            logsb.Append("请求支付结果：" + ybResult + "\n");
            //返回的PayForm
            string payForm = string.Empty;
            //将支付结果json字符串反序列化为对象
            RespondJson respJson = Newtonsoft.Json.JsonConvert.DeserializeObject<RespondJson>(ybResult);
            string yb_data = respJson.data;
            if (string.IsNullOrEmpty(yb_data))
            {
                payForm = ybResult;
                return payForm;
            }
            yb_data = AESUtil.Decrypt(yb_data, this.MerchantKey);
            PayRequestJson result = Newtonsoft.Json.JsonConvert.DeserializeObject<PayRequestJson>(yb_data);

            if (result.code == 1)
            {
                bool r = Digest.PayRequestVerifyHMAC(result.customernumber, result.requestid, result.code, result.externalid, result.amount, result.payurl, hmacKey, result.hmac);
                if (r)
                {
                    HttpContext.Current.Response.Redirect(result.payurl);
                    //重定向跳转到易宝支付收银台
                    payForm = "<form id='frm1' action='" + result.payurl + "' method='get'></form>";
                    payForm += "<script type='text/javascript' language='javascript'>setTimeout(document.getElementById('frm1').submit(),100);</script>";
                }
                else
                {
                    payForm = "回调验签失败";
                }
            }
            else
            {
                payForm = result.msg;
            }
            return payForm;
        }

        public string WxQRCode(string orderid, decimal orderAmt, string bankCode)
        {
            //请求移动终端网页收银台支付

            //一键支付URL前缀
            string apiprefix = APIURLConfig.mobilePrefix;

            //网页支付地址
            string mobilepayURI = APIURLConfig.webpayURI;

            //商户账户编号
            string customernumber = this.MerchantAccount;
            string hmacKey = this.MerchantKey;
            string AesKey = this.AesKey;

            //日志字符串
            StringBuilder logsb = new StringBuilder();
            logsb.Append(DateTime.Now.ToString() + "\n");

            Random ra = new Random();
            string payproducttype = "WECHATU"; // "支付方式";
            string requestid = orderid;//订单号
            string amount = orderAmt.ToString();//金额
            string productcat = "";//商品类别码，商户支持的商品类别码由易宝支付运营人员根据商务协议配置
            string productdesc = "在线储值";//商品描述
            string productname = "在线储值";//商品名称
            string assure = "0";//是否需要担保,1是，0否
            string divideinfo = "";//分账信息，格式”ledgerNo:分账比
            string bankid = GetBankCode(bankCode);//银行编码
            string period = "";//担保有效期，单位 ：天；当assure=1 时必填，最大值：30
            string memo = "";//商户备注

            //商户提供的商户后台系统异步支付回调地址
            string callbackurl = CallbackUrl;
            //商户提供的商户前台系统异步支付回调地址
            string webcallbackurl = "";
            string hmac = "";


            hmac = Digest.GetHMAC(customernumber, requestid, amount, assure, productname, productcat, productdesc, divideinfo, callbackurl, webcallbackurl, bankid, period, memo, hmacKey);

            SortedDictionary<string, object> sd = new SortedDictionary<string, object>();
            sd.Add("customernumber", customernumber);
            sd.Add("amount", amount);
            sd.Add("requestid", requestid);
            sd.Add("assure", assure);
            sd.Add("productname", productname);
            sd.Add("productcat", productcat);
            sd.Add("productdesc", productdesc);
            sd.Add("divideinfo", divideinfo);
            sd.Add("callbackurl", callbackurl);
            sd.Add("webcallbackurl", webcallbackurl);
            sd.Add("bankid", bankid);
            sd.Add("period", period);
            sd.Add("memo", memo);
            sd.Add("payproducttype", payproducttype);
            sd.Add("hmac", hmac);



            //将网页支付对象转换为json字符串
            string wpinfo_json = Newtonsoft.Json.JsonConvert.SerializeObject(sd);
            logsb.Append("网银支付明文数据json格式为：" + wpinfo_json + "\n");

            string datastring = AESUtil.Encrypt(wpinfo_json, AesKey);

            logsb.Append("网银支付业务数据经过AES加密后的值为：" + datastring + "\n");



            //打开浏览器访问一键支付网页支付链接地址，请求方式为get
            string postParams = "data=" + HttpUtility.UrlEncode(datastring) + "&customernumber=" + customernumber;
            string url = apiprefix + mobilepayURI + "?" + postParams;

            logsb.Append("网银支付链接地址为：" + url + "\n");

#if DEBUG
            SoftLog.LogStr(logsb.ToString(), "ECPayLog");
#endif
            string ybResult = YJPayUtil.payAPIRequest(apiprefix + mobilepayURI, datastring, false, this.MerchantAccount);

            logsb.Append("请求支付结果：" + ybResult + "\n");
            //返回的PayForm
            string payForm = string.Empty;
            //将支付结果json字符串反序列化为对象
            RespondJson respJson = Newtonsoft.Json.JsonConvert.DeserializeObject<RespondJson>(ybResult);
            string yb_data = respJson.data;
            if (string.IsNullOrEmpty(yb_data))
            {
                payForm = ybResult;
                return null;
            }
            yb_data = AESUtil.Decrypt(yb_data, this.MerchantKey);
            PayRequestJson result = Newtonsoft.Json.JsonConvert.DeserializeObject<PayRequestJson>(yb_data);

            if (result.code == 1)
            {
                bool r = Digest.PayRequestVerifyHMAC(result.customernumber, result.requestid, result.code, result.externalid, result.amount, result.payurl, hmacKey, result.hmac);
                if (r)
                {

                    return result.payurl;
                    //byte[] arry = Encoding.UTF8.GetBytes(result.payurl);
                    //MemoryStream stream = new MemoryStream(arry);
                    //return stream;
                    //重定向跳转到易宝支付收银台
                    payForm = "<form id='frm1' action='" + result.payurl + "' method='get'></form>";
                    payForm += "<script type='text/javascript' language='javascript'>setTimeout(document.getElementById('frm1').submit(),100);</script>";
                }
                else
                {
                    payForm = "回调验签失败";
                }
            }
            else
            {
                payForm = result.msg;
            }
            return null;
        }
        /// <summary>
        /// 掌柜通支付通知信息
        /// </summary>
        /// <param name="data"></param>
        public void Notify(string data)
        {
            data = AESUtil.Decrypt(data, MerchantKey);

            PayResultJson result = Newtonsoft.Json.JsonConvert.DeserializeObject<PayResultJson>(data);
            string opstate = "1";
            ///支付结果回调验签
            bool r = Digest.PayResultVerifyHMAC(result.customernumber, result.requestid, result.code, result.notifytype, result.externalid, result.amount, result.cardno, this.MerchantKey, result.hmac);
            if (r)
            {
                if (result.code == 1)
                {
                    decimal amt = 0;
                    string msg = string.Empty;
                    if (decimal.TryParse(result.amount, out amt))
                    {
                        opstate = "0";
                        msg = "成功";
                    }
                    OrderBankUtils.SuppNotify(suppid, "", result.requestid
                       , 2
                       , opstate
                       , msg
                       , amt
                       , amt
                       , "Success"
                       , "Failure");
                }
            }
        }
        /// <summary>
        /// 商户编号
        /// </summary>
        private string MerchantAccount
        {
            get
            {
                return SuppAccount;
            }
        }
        /// <summary>
        /// 商户密钥
        /// </summary>
        private string MerchantKey
        {
            get
            {
                return SuppKey;
            }
        }
        /// <summary>
        /// 商户加密 密钥
        /// </summary>
        private string AesKey
        {
            get
            {
                return SuppKey.Substring(0, 16);
            }
        }

        internal string CallbackUrl
        {
            get
            {
                return this.SiteDomain + "/Receive/YeePayZGT/callback.aspx";
            }
        }
        public string GetBankCode(string paymodeId)
        {
            string code = "";
            switch (paymodeId)
            {
                case "970":
                    code = "CMBCHINA-NET-B2C"; //招商银行
                    break;
                case "967":
                    code = "ICBC-NET-B2C"; //中国工商银行
                    break;
                case "964":
                    code = "ABC-NET-B2C"; //中国农业银行
                    break;
                case "965":
                    code = "CCB-NET-B2C"; //中国建设银行
                    break;
                case "963":
                    code = "BOC-NET-B2C"; //中国银行
                    break;
                case "981":
                    code = "BOCO-NET-B2C"; //中国交通银行
                    break;
                case "980":
                    code = "CMBC-NET-B2C"; //中国民生银行
                    break;
                case "974":
                    code = "SDB-NET-B2C"; //深圳发展银行
                    break;
                case "985":
                    code = "GDB-NET-B2C"; //广东发展银行
                    break;
                case "962":
                    code = "ECITIC-NET-B2C"; //中信银行
                    break;
                case "982":
                    code = "HXB-NET-B2C"; //华夏银行
                    break;
                case "972":
                    code = "CIB-NET-B2C"; //兴业银行
                    break;
                case "971":
                    code = "POST-NET-B2C"; //中国邮政
                    break;
                case "989":
                    code = "BCCB-NET-B2C"; //北京银行
                    break;
                case "988":
                    code = "CBHB-NET-B2C"; //渤海银行
                    break;
                case "990":
                    code = "BJRCB-NET-B2C"; //北京农商银行
                    break;
                case "979":
                    code = "NJCB-NET-B2C"; //南京银行
                    break;
                case "986":
                    code = "CEB-NET-B2C"; //中国光大银行
                    break;
                case "987":
                    code = "HKBEA-NET-B2C"; //东亚银行
                    break;
                case "997":
                    code = "NBCB-NET-B2C"; //宁波银行
                    break;
                case "978":
                    code = "PINGANBANK-NET"; //平安银行
                    break;
                case "968":
                    code = "CZ-NET-B2C"; //浙商银行
                    break;
                case "975":
                    code = "SHB-NET-B2C"; //上海银行
                    break;
                case "977":
                    code = "SPDB-NET-B2C"; //上海银行
                    break;
            }
            return code;
        }
    }
}
