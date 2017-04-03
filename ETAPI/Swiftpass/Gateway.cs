using System;
using System.Collections.Specialized;
using System.IO;
using System.Web.Script.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using viviapi.ETAPI.Common;
using viviapi.ETAPI.Swiftpass.Lib;
using viviapi.Model.supplier;
using viviLib.ExceptionHandling;
using viviLib.Web;

namespace viviapi.ETAPI.Swiftpass
{
    public class Gateway : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.Swiftpass;

        private ClientResponseHandler resHandler = new ClientResponseHandler();
        private PayHttpClient pay = new PayHttpClient();
        private RequestHandler reqHandler = null;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        public Gateway()
            : base(SuppId)
        {
            HttpRequest req = HttpContext.Current.Request;
            this.reqHandler = new RequestHandler(HttpContext.Current);

            //初始化数据
            this.reqHandler.setGateUrl(PostBankUrl);
            this.reqHandler.setKey(SuppKey);
        }
        internal string notifyUrl { get { return this.SiteDomain + "/receive/swiftpass/result.aspx"; } }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <param name="total_fee"></param>
        /// <returns></returns>
        public Hashtable submitOrderInfo(string out_trade_no, decimal total_fee)
        {
            //reqHandler.setReqParameters(new string[] { "out_trade_no", "body", "attach", "total_fee", "mch_create_ip", "time_start", "time_expire" });

            reqHandler.setParameter("out_trade_no", out_trade_no);//商户订单号
            reqHandler.setParameter("body", "商城储值");//商品描述
            reqHandler.setParameter("attach", "");//附加信息
            reqHandler.setParameter("total_fee", (total_fee * 100M).ToString("0"));//总金额

            reqHandler.setParameter("mch_create_ip", HttpContext.Current.Request.UserHostAddress);
            reqHandler.setParameter("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));//订单生成时间
            reqHandler.setParameter("time_expire", DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss"));//订单超时间

            reqHandler.setParameter("service", "pay.weixin.scancode");//接口类型：pay.weixin.scancode
            //reqHandler.setParameter("charset", "GB2312");//字符集 默认为UTF-8
            reqHandler.setParameter("mch_id", SuppAccount);//必填项，商户号，由威富通分配
            reqHandler.setParameter("version", SuppUserName);//接口版本号，目前为1.1
            reqHandler.setParameter("notify_url", notifyUrl);//通知地址，必填项，接收威富通通知的URL，需给绝对路径，255字符内格式如:http://wap.tenpay.com/tenpay.asp
            reqHandler.setParameter("nonce_str", Utils.random()); ;//随机字符串，必填项，不长于 32 位
            reqHandler.createSign();//创建签名

            string data = Utils.toXml(reqHandler.getAllParameters());
            Dictionary<string, string> reqContent = new Dictionary<string, string>();
            reqContent.Add("url", reqHandler.getGateUrl());
            reqContent.Add("data", data);
            Dictionary<string, object> resMsg = new Dictionary<string, object>();

            pay.setReqContent(reqContent);

            if (pay.call())
            {
                resHandler.setContent(pay.getResContent());
                resHandler.setKey(SuppKey);
                Hashtable param = resHandler.getAllParameters();
                if (resHandler.isTenpaySign())
                {
                    //当返回状态与业务结果都为0时才返回支付二维码，其它结果请查看接口文档
                    if (int.Parse(param["status"].ToString()) == 0 && int.Parse(param["result_code"].ToString()) == 0)
                    {
                        resMsg.Clear();
                        resMsg.Add("code_img_url", param["code_img_url"]);
                        resMsg.Add("code_url", param["code_url"]);
                        resMsg.Add("code_status", param["code_status"]);

                       // Utils.writeFile("resMsg", param);

                        return param;
                        //param["code_img_url"].ToString() + "&uuid=" + GetUUID(param["code_url"].ToString());
                    }
                    else
                    {
                        resMsg.Clear();
                        resMsg.Add("status", 500);
                        resMsg.Add("msg", "Error Code:" + param["err_code"] + " Error Message:" + param["err_msg"]);

                        LogWrite("Error Code:" + param["err_code"] + " Error Message:" + param["err_msg"]);
                    }

                }
                else
                {
                    resMsg.Clear();
                    resMsg.Add("status", 500);
                    resMsg.Add("msg", "Error Code:" + param["status"] + " Error Message:" + param["message"]);


                    LogWrite("Error Code:" + param["err_code"] + " Error Message:" + param["err_msg"]);
                }
            }
            else
            {
                resMsg.Clear();
                resMsg.Add("status", 500);
                resMsg.Add("msg", "Response Code:" + pay.getResponseCode() + " Error Info:" + pay.getErrInfo());


                LogWrite("Error Code:" + "Response Code:" + pay.getResponseCode() + " Error Info:" + pay.getErrInfo());
            }

            return null;
        }

        //public string GetUUID(string code_url)
        //{
        //    string resMsg = WebClientHelper.GetString(code_url, null, "get",
        //        System.Text.Encoding.Default);

        //    try
        //    {
        //        if (!string.IsNullOrEmpty(resMsg))
        //        {
        //            string split = "uuid = \"";

        //            int index = resMsg.IndexOf(split, System.StringComparison.Ordinal);

        //            string newstr = resMsg.Substring(index + split.Length);

        //            index = newstr.IndexOf("\"", System.StringComparison.Ordinal);

        //            return newstr.Substring(0, index);
        //        }

        //    }
        //    catch(Exception exception)
        //    {
        //        viviLib.ExceptionHandling.ExceptionHandler.HandleException(exception);
        //    }

        //    return string.Empty;
        //}

        public static string FormatRequestData(NameValueCollection list)
        {
            if ((list == null) || (list.Count == 0))
            {
                return string.Empty;
            }
            string[] strArray = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                strArray[i] = string.Format("{0}={1}", list.Keys[i], list[i]);
            }
            return string.Join("\n\r", strArray);
        }

        public void callback()
        {
           // LogWrite("swift pass callback start");

            string logfiles = FormatRequestData(HttpContext.Current.Request.Params);

           // LogWrite("swift pass callback start=>" + logfiles);

            try
            {
                using (StreamReader sr = new StreamReader(HttpContext.Current.Request.InputStream))
                {

                    string contenct = sr.ReadToEnd();
                    this.resHandler.setContent(contenct);
                    this.resHandler.setKey(SuppKey);

                  //  LogWrite("setContent" + contenct);

                    Hashtable resParam = this.resHandler.getAllParameters();
                    if (this.resHandler.isTenpaySign())
                    {
                        string opstate = "-1";
                        int status = 4;
                        string msg = "支付失败";
                        decimal result = 0M;

                        if (int.Parse(resParam["status"].ToString()) == 0 
                            && int.Parse(resParam["result_code"].ToString()) == 0)
                        {
                            Utils.writeFile("接口回调", resParam);
                            //此处可以在添加相关处理业务

                            if (decimal.TryParse(resParam["total_fee"].ToString(), out result))
                            {
                                msg = "支付成功";
                                opstate = "0";
                                status = 2;
                            }
                        }

                        OrderBankUtils.SuppNotify(SuppId
                      , resParam["out_trade_no"].ToString()
                      , resParam["transaction_id"].ToString()
                      , status
                      , opstate
                      , msg
                      , result / 100M
                      , 0M, "success", "failure");

                        //context.Response.Write("success");
                    }
                    else
                    {
                        HttpContext.Current.Response.Write("failure");
                    }
                }
            }
            catch (Exception exception)
            {

                ExceptionHandler.HandleException(exception);
            }

        }

        public void Notify()
        {
            try
            {
                LogWrite("Notify");

                resHandler = new ClientResponseHandler(HttpContext.Current);

                LogWrite("isTenpaySign");

                if (this.resHandler.isTenpaySign())
                {
                    string opstate = "-1";
                    int status = 4;
                    string msg = "支付失败";
                    decimal result = 0M;

                    if (int.Parse(resHandler.getParameter("status").ToString())
                        == 0 && int.Parse(resHandler.getParameter("result_code").ToString()) == 0)
                    {
                        //Utils.writeFile("接口回调", resParam);
                        //此处可以在添加相关处理业务

                        if (decimal.TryParse(resHandler.getParameter("total_fee").ToString(), out result))
                        {
                            msg = "支付成功";
                            opstate = "0";
                            status = 2;
                        }
                    }

                    OrderBankUtils.SuppNotify(SuppId
                  , resHandler.getParameter("out_trade_no").ToString()
                  , resHandler.getParameter("transaction_id").ToString()
                  , status
                  , opstate
                  , msg
                  , result / 100M
                  , 0M, "success", "failure");
                }
            }
            catch (Exception exception)
            {

                ExceptionHandler.HandleException(exception);
            }
        }
    }
}
