using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using viviapi.BLL;
using viviapi.BLL.Sys;
using viviapi.ETAPI.Alipay.Lib;
using viviapi.ETAPI.Common;
using viviLib.ExceptionHandling;

namespace viviapi.ETAPI.Alipay
{
    public class Qrcode : ETAPIBase
    {
        private static int SuppId = 101;

        public Qrcode()
            : base(SuppId)
        {

        }

        internal string Failflag = "fail";

        internal string notify_url { get { return this.SiteDomain + "/receive/alipay/qrcode.aspx"; } }
        internal string return_url { get { return this.SiteDomain + "/return/alipay/qrcode.aspx"; } }

       

        protected string ExpiryDate
        {
            get
            {
                return string.Format("{0:yyyy-MM-dd 00:00:00}|{1:yyyy-MM-dd 59:59:59}", DateTime.Today.AddDays(-7),
                    DateTime.Today.AddMonths(1));
            }
        }

        protected string logo_name = OtherSettings.AliDCode_logo_name;
        protected string goods_info_name = OtherSettings.AliDCode_goods_info_name;
        protected string goods_info_desc = OtherSettings.AliDCode_goods_info_desc;

        public string Getqrcode_img_url(string out_trade_no, decimal amount, string bankid)
        {
            try
            {
                string qrcode_img_url = "";

                //接口调用时间
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //格式为：yyyy-MM-dd HH:mm:ss

                //动作
                string method = "add";
                //创建商品二维码
                //业务类型
                string biz_type = "10";

                //目前只支持1
                //业务数据
                string biz_data = string.Format(@"{{
""memo"": ""备注"",
""ext_info"": {{
""single_limit"": ""1"",
""user_limit"": ""1"",
""logo_name"": ""{0}""
}},
""goods_info"": {{
""id"": ""{1}"",
""name"": ""{6}"",
""price"": ""{2:f2}"",
""expiry_date"": ""{3}"",
""desc"": ""{7}"",
""sku_title"": ""sku_title"",
""sku"": [
{{
""sku_id"": ""001"",
""sku_name"": ""1"",
""sku_price"": ""{2:f2}"",
""sku_inventory"": ""1""
}}]
}},
""need_address"":""F"",
""trade_type"":""1"",
""return_url"":""{4}"",
""notify_url"":""{5}""
}}", logo_name, out_trade_no, amount, ExpiryDate, return_url, notify_url, goods_info_name, goods_info_desc);
                //格式：JSON 大字符串，详见技术文档4.2.1章节

               // viviLib.Logging.LogHelper.Write(biz_data);


                ////////////////////////////////////////////////////////////////////////////////////////////////

                //把请求参数打包成数组
                SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
                sParaTemp.Add("partner", Config.Partner);
                sParaTemp.Add("_input_charset", Config.Input_charset.ToLower());
                sParaTemp.Add("service", "alipay.mobile.qrcode.manage");
                sParaTemp.Add("timestamp", timestamp);
                sParaTemp.Add("method", method);
                sParaTemp.Add("biz_type", biz_type);
                sParaTemp.Add("biz_data", biz_data);

                //建立请求
                string sHtmlText = Submit.BuildRequest(sParaTemp);
                //viviLib.Logging.LogHelper.Write(sHtmlText);
                //请在这里加上商户的业务逻辑程序代码

                //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——

                var xmlDoc = new XmlDocument();
                try
                {
                    xmlDoc.LoadXml(sHtmlText);
                    var xmlNodeList = xmlDoc.SelectSingleNode("/alipay/response/alipay");
                    if (xmlNodeList != null)
                    {
                        foreach (XmlNode list in xmlNodeList)
                        {
                            if (list.Name.ToLower() == "qrcode_img_url")
                            {
                                qrcode_img_url = list.InnerText;
                                break;
                            }
                        }
                    }
                }
                catch (Exception exp)
                {

                }

                return qrcode_img_url;
            }
            catch (Exception exception)
            {
                
               ExceptionHandler.HandleException(exception);

                return "";
            }
           
        }

        public void Notify()
        {
            SortedDictionary<string, string> sPara = GetRequestPost();

            if (sPara.Count > 0) //判断是否有带返回参数
            {
                var aliNotify = new Notify();

                bool verifyResult = aliNotify.Verify(sPara
                    , HttpContext.Current.Request.Form["notify_id"]
                    , HttpContext.Current.Request.Form["sign"]);

                if (verifyResult) //验证成功
                {
                    string opstate = "-1";
                    int status = 4;
                    string msg = "支付失败";
                    decimal result = 0M;

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码


                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中服务器异步通知参数列表

                    //通知业务参数
                    string notify_data = HttpContext.Current.Request.Form["notify_data"];
                    //参数格式请参见技术文档7.2章节
                    string out_trade_no = "";
                    string trade_no = "";
                    if (!string.IsNullOrEmpty(notify_data))
                    {
                        var xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(notify_data);

                         trade_no  = xmlDoc.GetElementsByTagName("trade_no")[0].InnerText;
                        string total_fee = xmlDoc.GetElementsByTagName("total_fee")[0].InnerText;
                         out_trade_no = xmlDoc.GetElementsByTagName("out_trade_no")[0].InnerText;
                        string trade_status = xmlDoc.GetElementsByTagName("trade_status")[0].InnerText;

                        if (trade_status == "TRADE_SUCCESS"
                            || trade_status == "TRADE_FINISHED")
                        {
                            if (decimal.TryParse(total_fee, out result))
                            {
                                msg = "成功";
                                opstate = "0";
                                status = 2;
                            }
                        }
                    }


                    //判断是否在商户网站中已经做过了这次通知返回的处理
                    //如果没有做过处理，那么执行商户的业务程序
                    //如果有做过处理，那么不执行商户的业务程序

                    OrderBankUtils.SuppNotify(SuppId
                     , out_trade_no
                     , trade_no
                     , status
                     , opstate
                     , msg
                     , result, 0M
                     , "success"
                     , Failflag);

                    //请不要修改或删除

                    //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else //验证失败
                {
                    HttpContext.Current.Response.Write("fail");
                }
            }
        }

        public SortedDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll =HttpContext.Current.Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], HttpContext.Current.Request.Form[requestItem[i]]);

                //viviLib.Logging.LogHelper.Write(requestItem[i] + ":" + HttpContext.Current.Request.Form[requestItem[i]]);
            }

            return sArray;
        }
    }
}
