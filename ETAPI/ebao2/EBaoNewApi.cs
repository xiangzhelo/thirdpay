using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using viviapi.ETAPI.Common;
using viviapi.Model.supplier;

namespace viviapi.ETAPI.ebao2
{
    public class EBaoNewApi : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.YeePay;

        public EBaoNewApi() : base(SuppId)
        {
        }

        /// <summary>
        /// 原生的请求,只针对已经使用易宝支付的用户
        /// </summary>
        /// <param name="nav"></param>
        /// <returns></returns>
        public string Pay(NameValueCollection nav, HttpContext context)
        {
            //请求移动终端网页收银台支付

            //一键支付URL前缀
            string apiprefix = APIURLConfig.mobilePrefix;

            //网页支付地址
            string mobilepayURI = APIURLConfig.webpayURI;


            //商户账户编号
            string customernumber = this.SuppAccount;
            string hmacKey = this.SuppKey;
            string AesKey = hmacKey.Substring(0, 16);
            string payproducttype = nav.AllKeys.Contains("payproducttype") ? nav.Get("payproducttype") : "WECHATU";
            string requestid = nav.AllKeys.Contains("requestid") ? nav.Get("requestid") : "";
            string amount = nav.AllKeys.Contains("amount") ? nav.Get("amount") : "";
            string assure = nav.AllKeys.Contains("assure") ? nav.Get("assure") : "";
            string productname = nav.AllKeys.Contains("productname") ? nav.Get("productname") : "";
            string productcat = nav.AllKeys.Contains("productcat") ? nav.Get("productcat") : "";
            string productdesc = nav.AllKeys.Contains("productdesc") ? nav.Get("productdesc") : "";
            string divideinfo = nav.AllKeys.Contains("divideinfo") ? nav.Get("divideinfo") : "";
            string callbackurl = nav.AllKeys.Contains("callbackurl") ? nav.Get("callbackurl") : "";
            string webcallbackurl = nav.AllKeys.Contains("webcallbackurl") ? nav.Get("webcallbackurl") : "";
            string bankid = nav.AllKeys.Contains("bankid") ? nav.Get("bankid") : "";
            string period = nav.AllKeys.Contains("period") ? nav.Get("period") : "";
            string memo = nav.AllKeys.Contains("memo") ? nav.Get("memo") : "";

            string hmac = "";

            //签名
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
            sd.Add("payproducttype", "WECHATU");
            sd.Add("hmac", hmac);


            StringBuilder logsb = new StringBuilder();
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
            /*解密
            AESUtil.Decrypt(wpinfo_json, AesKey);*/

            string ybResult = YJPayUtil.payAPIRequest(apiprefix + mobilepayURI, datastring, false);

            logsb.Append("请求支付结果：" + ybResult + "\n");

            //将支付结果json字符串反序列化为对象
            RespondJson respJson = Newtonsoft.Json.JsonConvert.DeserializeObject<RespondJson>(ybResult);

            string yb_data = respJson.data ?? "failure";

            if (yb_data == "failure")
            {
                context.Response.Write(ybResult);
            }
            else
            {
                yb_data = AESUtil.Decrypt(yb_data, Config.merchantKey);
                PayRequestJson result = Newtonsoft.Json.JsonConvert.DeserializeObject<PayRequestJson>(yb_data);
                if (result.code == 1)
                {
                    bool r = Digest.PayRequestVerifyHMAC(result.customernumber, result.requestid, result.code, result.externalid, result.amount, result.payurl, hmacKey, result.hmac);
                    if (r)
                    {
                        //重定向跳转到易宝支付收银台
                        context.Response.Redirect(result.payurl);
                    }
                    else
                    {
                        context.Response.Write("回调验签失败");
                    }
                }
                else
                {
                    context.Response.Write(result.msg);
                }
            }
            return "";
        }
    }
}
