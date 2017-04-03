using System;
using System.Web;
using viviapi.ETAPI.Common;
using viviapi.Model.supplier;
using viviLib.Security;
using Demo.Class;
using System.Collections.Specialized;
using System.IO;
using viviLib.ExceptionHandling;

namespace viviapi.ETAPI.Zline
{
    /// <summary>
    /// 中联接口
    /// </summary>
    public class Bank : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.Zline;

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


        internal string Returnurl { get { return this.SiteDomain + "/return/zline/bank.aspx"; } }
        internal string NotifyUrl { get { return this.SiteDomain + "/receive/zline/bank.aspx"; } }
        //internal string NotifyUrl { get { return "http://pay.leshouka.com/receive/zline/bank.aspx"; } }
        //需返回给接口的信息
        internal string Succflag = "{'code':'00'}";
        internal string Failflag = "{'code':'01'}";

        #region PayBank
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="orderAmt"></param>
        /// <param name="bankcode"></param>
        /// <param name="autoSubmit"></param>
        /// <returns></returns>
        public string PayBank(string orderid, decimal orderAmt, string bankcode, bool autoSubmit)
        {
            //        <form id="form1" method="post" action="https://payment.kklpay.com/ebank/pay.do">
            //    <input type="text" name="merchantCode" style=" display:none;" value="1000000183"/>
            //    <input type="text" name="outOrderId" style=" display:none;" value="123123321123yhg"/>
            //    <input type="text" name="totalAmount" style=" display:none;" value="1"/>
            //    <input type="text" name="goodsName" style=" display:none;" value="goodsName"/>
            //    <input type="text" name="goodsExplain" style=" display:none;" value="goodsExplain"/>
            //    <input type="text" name="orderCreateTime" style=" display:none;" value="20150210213410"/>
            //    <input type="text" name="lastPayTime" style=" display:none;" value=""/>
            //    <input type="text" name="merUrl" style=" display:none;" value="http://www.baidu.com/Demo/Rec.aspx"/>
            //    <input type="text" name="noticeUrl" style=" display:none;" value="http://www.baidu.com/Demo/SynNotice.aspx"/>
            //    <input type="text" name="bankCode" style=" display:none;" value="BOC"/>
            //    <input type="text" name="bankCardType" style=" display:none;" value="00"/>
            //<input type="text" name="sign"  style=" display:none;" value="3C72DF74572665A22EA0E190E65869EA"/>
            //</form>
            //<script type="text/javascript"> document.getElementById("form1").submit();</script>


            //提交地址
            string form_url = PostBankUrl;
            //商户号
            string Mer_code = SuppAccount;
            string Mer_key = SuppKey;
            //商户订单编号
            string Billno = orderid;
            //订单金额(分)
            string Amount = decimal.Round(orderAmt * 100, 0).ToString();

            string keyValue = string.Empty;
            string key = string.Empty;
            string sign = string.Empty;
            string signValue = string.Empty;
            RSAOperate Rdaop = new RSAOperate(SuppId);
            NameValueCollection collection = new NameValueCollection();
            collection.Add("merchantCode", Mer_code);
            collection.Add("outOrderId", Billno);
            collection.Add("totalAmount", Amount);
            collection.Add("orderCreateTime", DateTime.Now.ToString("yyyyMMddhhmmss"));
            collection.Add("lastPayTime", DateTime.Now.ToString("yyyyMMddhhmmss"));
            collection.Add("merUrl", Returnurl);
            collection.Add("noticeUrl", NotifyUrl);
            collection.Add("bankCode", GetBankCode(bankcode));
            collection.Add("bankCardType", "00");
            collection.Add("goodsName", "goodsname");
            collection.Add("goodsExplain", "explain");
            signValue = Rdaop.GetUrlParamString(collection, RSASign.GetPayRSAParamSort());
            sign = RSASign.GetMD5RSA(signValue +"&KEY="+ SuppKey);
            collection.Add("sign", sign);
            SynsSummitLogger("plain: " + signValue);
            SynsSummitLogger("SignMD5: " + sign);

            string postForm = "<form name=\"form1\" id=\"form1\" method=\"post\" action=\"" + PostBankUrl + "\">";

            for (int i = 0; i < RSASign.GetPayParamSort().Length; i++)
            {
                key = RSASign.GetPayParamSort()[i];
                if (key == "merchantCode")
                    keyValue = Mer_code;
                else
                    keyValue = collection[RSASign.GetPayParamSort()[i]];

                postForm += "<input type=\"text\" name=\"" + key + "\" style=\" display:none;\" value=\"" + keyValue + "\"/>";

            }

            postForm += "<input type=\"text\" name=\"sign\"  style=\" display:none;\" value=\"" + sign + "\"/></form>";


            if (autoSubmit == true)
            {
                //自动提交该表单到测试网关
                postForm +=
                    "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('form1').submit();\",100);</script>";
            }

            SynsSummitLogger("SignMD5: " + postForm);
            return postForm;
        }
        #endregion

        #region ReturnBank
        /// <summary>
        /// 同步返回信息
        /// </summary>
        public void ReturnBank()
        {
            //<form action="http://pay.wengpay.com/return/zline/bank.aspx" id="form1" method="post">

            //<input type="hidden" name="sign" id="sign" value="CF83653CA3C0CC2DFD79C58E0C718C9D" />//签名

            //<input type="hidden" name="result" id="result" value="" />

            //<input type="hidden" name="transType" id="transType" value="00200" />//交易类型

            //<input type="hidden" name="instructCode" id="instructCode" value="11001455960" />//交易订单号

            //<input type="hidden" name="waitTime" id="waitTime" value="" />

            //<input type="hidden" name="autoJump" id="autoJump" value="1" />

            //<input type="hidden" name="transTime" id="transTime" value="20151007221549" />//交易时间

            //<input type="hidden" name="totalAmount" id="totalAmount" value="100" />//消费金额

            //<input type="hidden" name="merchantCode" id="merchantCode" value="1000000183" />//商户号

            //<input type="hidden" name="outOrderId" id="outOrderId" value="B4687724001572971251" />//商户订单
            try
            {
                RSAOperate Rdaop = new RSAOperate(SuppId);

                NameValueCollection collection = new NameValueCollection();
                collection.Add("sign", HttpContext.Current.Request.Form["sign"]);
                collection.Add("transType", HttpContext.Current.Request.Form["transType"]);
                collection.Add("instructCode", HttpContext.Current.Request.Form["instructCode"]);
                collection.Add("transTime", HttpContext.Current.Request.Form["transTime"]);
                collection.Add("totalAmount", HttpContext.Current.Request.Form["totalAmount"]);
                collection.Add("merchantCode", HttpContext.Current.Request.Form["merchantCode"]);
                collection.Add("outOrderId", HttpContext.Current.Request.Form["outOrderId"]);
                string sign = HttpContext.Current.Request.Form["sign"];
                string RSAChar = Rdaop.GetUrlParamString(collection, Demo.Class.RSASign.GetNoticeRSAParamSort()) + "&KEY=" + SuppKey;
                if (sign == RSASign.GetMD5RSA(RSAChar))//判断是否报文加密后能够匹配
                {

                    string billno = HttpContext.Current.Request.Form["outOrderId"];
                    //中联订单号
                    string zlineBillno = HttpContext.Current.Request.Form["instructCode"];
                    //消费金额
                    string amount = HttpContext.Current.Request.Form["totalAmount"];//
                    string msg = "";
                    string opstate = "-1";
                    int status = 4;
                    //判断交易是否成功
                    if (Convert.ToDecimal(amount) > 0)
                    {
                        opstate = "0";
                        status = 2;
                    }

                    OrderBankUtils.SuppPageReturn(SuppId
                               , billno
                               , zlineBillno
                               , status
                               , opstate
                               , string.Empty
                               , decimal.Parse(amount) / 100m, 0M);

                }
                else
                {

                    HttpContext.Current.Response.Write("返回报文加密信息存在异常");//00表示已经收到报文
                }

            }
            catch (Exception eh)
            {
                HttpContext.Current.Response.Write("报文异常" + eh.Message);//00表示已经收到报文
            }
        }
        #endregion
        protected Object lockobject = new Object();
        #region Notify
        /// <summary>
        /// 
        /// </summary>
        public void Notify()
        {
            try
            {
                RSAOperate Rdaop = new RSAOperate(SuppId);
                //处理传输过来的流
                Stream responseStream = HttpContext.Current.Request.InputStream;
                StreamReader readStream = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                string RequestStream = readStream.ReadToEnd();
                readStream.Close();
                if (!string.IsNullOrEmpty(RequestStream))
                {
                    string RSAChar = Rdaop.GetUrlParamString(RequestStream, Demo.Class.RSASign.GetNoticeRSAParamSort()) + "&KEY=" + SuppKey;
                    if (Rdaop.GetIsSafty(Rdaop.GetjosnValue(RequestStream, "sign"), RSAChar))//判断是否报文加密后能够匹配
                    {
                        lock (lockobject)//此处建议使用lock锁机制，进行并发控制，防止重复数据混乱
                        {
                            //此处由用户自己进行返回单据信息进行处理
                            //可以用Rdaop.GetjosnValue(RequestStream, "sign")类似   获取对应单据信息，进行单据信息在客户端服务器处理
                            //todo:中联信息返回待完成
                            string billno = Rdaop.GetjosnValue(RequestStream, "outOrderId");
                            string zlineBillno = Rdaop.GetjosnValue(RequestStream, "instructCode");
                            string amount = Rdaop.GetjosnValue(RequestStream, "totalAmount");//
                            string msg = "";
                            string opstate = "-1";
                            int status = 4;
                            //判断交易是否成功
                            if (Convert.ToDecimal(amount) > 0)
                            {
                                opstate = "0";
                                status = 2;
                            }
                            OrderBankUtils.SuppNotify(SuppId
                              , billno
                              , zlineBillno
                              , status
                              , opstate
                              , msg
                              , decimal.Parse(amount) / 100m
                              , 0M
                              , Succflag
                              , Failflag);
                        }
                    }
                    else
                    {

                        HttpContext.Current.Response.Write("{\"code\":\"00\",\"msg\":\"返回报文加密信息存在异常\"}");//00表示已经收到报文
                    }
                }
                else
                {
                    HttpContext.Current.Response.Write("{\"code\":\"00\",\"msg\":\"返回报文为空存在异常\"}");//00表示已经收到报文
                }
            }
            catch (Exception eh)
            {
                HttpContext.Current.Response.Write("{\"code\":\"00\",\"msg\":\"报文异常\"}");//00表示已经收到报文
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
                    code = "CMB";  //招商银行
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
                    code = "BCM"; //中国交通银行
                    break;
                case "980":
                    code = "CMBC"; //中国民生银行
                    break;
                case "974":
                    code = "PAB"; //深圳发展银行
                    break;
                case "985":
                    code = "GDB"; //广东发展银行
                    break;
                case "962":
                    code = "CITIC"; //中信银行
                    break;
                case "982":
                    code = "HXB"; //华夏银行
                    break;
                case "972":
                    code = "CIB"; //兴业银行
                    break;
                case "984":
                    code = "GDB"; //广州农村商业银行
                    break;

                case "971":
                    code = "PCSB"; //中国邮政
                    break;
                case "989":
                    code = "BCCB"; //北京银行
                    break;

                //case "990":
                //    code = "00056"; //北京农商银行
                //    break;
                //case "979":
                //    code = "00055"; //南京银行
                //    break;
                case "986":
                    code = "CEB"; //中国光大银行
                    break;

                //case "983":
                //    code = "00081"; //杭州银行
                //    break;
                case "978":
                    code = "PAB"; //平安银行
                    break;

                //case "968":
                //    code = ""; //浙商银行
                //    break;
                case "975":
                    code = "BOS"; //上海银行
                    break;
                default:
                    code = "BOC";
                    break;
            }
            return code;
        }
        #endregion

        public string ReJson = string.Empty;
        public string ReJsonKeyValue = string.Empty;
        public string ReJsonArrayValue = string.Empty;
        #region OrderQuery
        /// <summary>
        /// https://paygate.baofoo.com/Check/OrderQuery.aspx
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public string OrderQuery(string orderid)
        {

            string responseText = "";
            try
            {
                Demo.Class.RSAOperate Rdaop = new Demo.Class.RSAOperate(SuppId);
                string postDataRSA = string.Format("merchantCode={0}&outOrderId={1}&KEY={2}", SuppAccount, orderid, SuppKey);//返回排序后需要签名的报文串
                postDataRSA = Demo.Class.RSASign.GetMD5RSA(postDataRSA);//加密后RSA信息
                NameValueCollection collection = new NameValueCollection();
                collection.Add("outOrderId", orderid);
                collection.Add("merchantCode", SuppAccount);
                collection.Add("sign", postDataRSA);
                string postData = Rdaop.GetPostJson(collection, Demo.Class.RSASign.GetQueryParamSort(), postDataRSA);//返回传输报文串
                ReJson = Rdaop.GetPostWeb(Demo.Class.ProperConst.queryUrl, postData);//此步骤拼写Param传输验证数据，必须加入sign，服务器便于验证安全性
                //if (Rdaop.GetjosnValue(ReJson, "code") == "00")
                //{
                //    string RSAChar = Rdaop.GetParamJosnString(ReJson, Demo.Class.RSASign.GetQueryRetuenMD5RSAParamSort()) + Demo.Class.ProperConst.Key;
                //    if (Rdaop.GetIsSafty(Rdaop.GetjosnValue(ReJson, "sign"), RSAChar))
                //    {
                //        //String[] RsaArray = Demo.Class.RSASign.GetQueryReturnParamSort();
                //        //for (int i = 1; i < Demo.Class.RSASign.GetQueryReturnParamSort().Length - 1; i++)//数组长度-1，排除sign字段显示
                //        //{
                //        //    ReJsonKeyValue = Rdaop.GetjosnValue(ReJson, RsaArray[i].ToString());
                //        //    ReJsonArrayValue = RsaArray[i].ToString();
                //        //}
                //        responseText = ReJson;
                //    }
                //    else
                //    {
                //        HttpContext.Current.Response.Write("返回报文安全验证失败");
                //    }
                //}
                //else
                //{
                //    HttpContext.Current.Response.Write("<strong color=\"red\">" + Rdaop.GetjosnValue(ReJson, "msg") + "</strong>");
                //}
                return responseText;

            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }

            return string.Empty;
        }
        #endregion
    }
}
