using System;
using System.Web;
using viviapi.ETAPI.Common;
using viviapi.Model.supplier;
using viviLib.Security;

namespace viviapi.ETAPI.Cared70
{
    /// <summary>
    ///70卡支付宝接口
    /// </summary>
    public class AliPay : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.Cared70;

        public AliPay()
            : base(SuppId)
        {

        }

        public static AliPay Instance
        {
            get
            {
                var instance = new AliPay();
                return instance;
            }
        }


        internal string Returnurl { get { return this.SiteDomain + "/return/cared70/alipay.aspx"; } }
        internal string NotifyUrl { get { return this.SiteDomain + "/receive/cared70/alipay.aspx"; } }
        //internal string Returnurl { get { return "http://pay.wengpay.com/return/card70/alipay.aspx"; } }
        //internal string NotifyUrl { get { return "http://pay.wengpay.com/receive/card70/alipay.aspx"; } }

        internal string Succflag = "<result>1</result>";
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

                return ip;
            }
        }
        #region PayBank
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="orderAmt"></param>
        /// <param name="bankcode"></param>
        /// <param name="autoSubmit"></param>
        /// <returns></returns>
        public string PayBank(string _orderid, decimal orderAmt, string bankcode, bool autoSubmit)
        {
            ////提交地址
            //string form_url = "http://wx.yzch.net/pay.ashx";
            ////商户号
            //string customerid = SuppAccount;
            //string Mer_key = SuppKey;
            ////商户订单编号
            //string sdcustomno = _orderid;
            ////订单金额(系统接收单位：分)
            //string ordermoney = decimal.Round(orderAmt * 100, 0).ToString();
            //string cardno = "34";

            //string noticeurl = NotifyUrl;
            //string backurl = Returnurl;
            //string mark = "chongzhi";

            //string plain = "customerid={0}&sdcustomno={1}&orderAmount={2}&cardno={3}&noticeurl={4}&backurl={5}{6}";
            //plain = string.Format(plain, customerid, sdcustomno, ordermoney, cardno, noticeurl, backurl, Mer_key);
            //SynsSummitLogger("plain: " + plain);
            //string sign = viviLib.Security.Cryptography.MD5(plain).ToUpper();
            //SynsSummitLogger("SignMD5: " + sign);
            //string postForm = "<form name=\"frm1\" id=\"frm1\" method=\"get\" action=\"" + form_url + "\">";
            //postForm += "<input type=\"hidden\" name=\"customerid\" value=\"" + customerid + "\" />";
            //postForm += "<input type=\"hidden\" name=\"sdcustomno\" value=\"" + sdcustomno + "\" />";
            //postForm += "<input type=\"hidden\" name=\"orderAmount\" value=\"" + ordermoney + "\" />";
            //postForm += "<input type=\"hidden\" name=\"cardno\" value=\"" + cardno + "\" />";

            //postForm += "<input type=\"hidden\" name=\"noticeurl\" value=\"" + noticeurl + "\" />";
            //postForm += "<input type=\"hidden\" name=\"backurl\" value=\"" + backurl + "\" />";

            //postForm += "<input type=\"hidden\" name=\"sign\" value=\"" + sign + "\" />";
            //postForm += "<input type=\"hidden\" name=\"mark\" value=\"" + mark + "\" />";
            //postForm += "</form>";

            //if (autoSubmit == true)
            //{
            //    //自动提交该表单到测试网关
            //    postForm +=
            //        "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>";
            //}

            //SynsSummitLogger("SignMD5: " + postForm);
            //return postForm;
            //提交地址
            string form_url = PostBankUrl;
            //商户号
            string userid = SuppAccount;
            string Mer_key = SuppKey;
            //商户订单编号
            string orderid = _orderid;
            //订单金额(保留2位小数)
            string money = decimal.Round(orderAmt, 2).ToString("0.00");
            string bankid = "2003";
            string url = NotifyUrl;
            string aurl = Returnurl;
            string sign = "";
            string ext = "";

            string plain = "userid={0}&orderid={1}&bankid={2}&keyvalue={3}";
            plain = string.Format(plain, userid, orderid, bankid, Mer_key);
            SynsSummitLogger("plain: " + plain);
            sign = viviLib.Security.Cryptography.MD5(plain).ToLower();
            SynsSummitLogger("SignMD5: " + sign);
            string postForm = "<form name=\"frm1\" id=\"frm1\" method=\"get\" action=\"" + form_url + "\">";
            postForm += "<input type=\"hidden\" name=\"userid\" value=\"" + userid + "\" />";
            postForm += "<input type=\"hidden\" name=\"orderid\" value=\"" + orderid + "\" />";
            postForm += "<input type=\"hidden\" name=\"money\" value=\"" + money + "\" />";
            postForm += "<input type=\"hidden\" name=\"url\" value=\"" + url + "\" />";
            postForm += "<input type=\"hidden\" name=\"aurl\" value=\"" + aurl + "\" />";
            postForm += "<input type=\"hidden\" name=\"bankid\" value=\"" + bankid + "\" />";
            postForm += "<input type=\"hidden\" name=\"backurl\" value=\"" + Returnurl + "\" />";
            postForm += "<input type=\"hidden\" name=\"sign\" value=\"" + sign + "\" />";
            postForm += "<input type=\"hidden\" name=\"ext\" value=\"" + ext + "\" />";
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
        public string GetErrMsg(int code)
        {
            string msg = "";
            switch (code)
            {
                case -1: msg = "系统忙"; break;
                case 1: msg = "商户订单号无效"; break;
                case 2: msg = "银行编码错误"; break;
                case 3: msg = "商户不存在"; break;
                case 4: msg = "验证签名失败"; break;
                case 5: msg = "商户储值关闭"; break;
                case 6: msg = "金额超出限额"; break;
            }
            return msg;
        }
        #region ReturnBank
        /// <summary>
        /// 
        /// </summary>
        public void ReturnBank()
        {
            //    返回值示例：http://pay.wengpay.com/return/cared70/weixin.aspx?
            //state=1&
            //    customerid=5774&
            //    sd51no=E115100900000131&
            //    sdcustomno=B4840362426830361619&
            //    ordermoney=1&
            //    cardno=32&
            //    mark=chongzhi&
            //    sign=D17F0F3DF603F31DCB2EAAAAA98848D4&
            //    resign=2A5D80BF516D050EE14B0B364A4580DF&
            //    Des=success
            //http://pay.wengpay.com/return/cared70/weixin.aspx?state=1&customerid=5774&sd51no=E115100900000196&sdcustomno=B4822333598710201852&ordermoney=1&cardno=32&mark=chongzhi&sign=42A7884F911DB783149E6600DCF1E938&resign=B30611FF86954BBD14A87C432642E356&Des=success
            //接收数据
            //string state = HttpContext.Current.Request["state"];
            //string customerid = HttpContext.Current.Request["customerid"];
            //string sd51no = HttpContext.Current.Request["sd51no"];
            //string sdcustomno = HttpContext.Current.Request["sdcustomno"];
            //string ordermoney = HttpContext.Current.Request["ordermoney"];
            //string cardno = HttpContext.Current.Request["cardno"];
            //string mark = HttpContext.Current.Request["mark"];
            //string sign = HttpContext.Current.Request["sign"];
            //string resign = HttpContext.Current.Request["resign"];
            //string des = HttpContext.Current.Request["des"];
            ////签名是否正确
            //Boolean verify = false;

            //string plain = "customerid={0}&sd51no={1}&sdcustomno={2}&mark={3}&key={4}";
            //plain = string.Format(plain, customerid, sd51no, sdcustomno, mark, SuppKey);
            //string sign1 = Cryptography.MD5(plain).ToUpper().ToString();

            //if (sign1 == sign)
            //{
            //    verify = true;
            //}
            ////判断签名验证是否通过
            //if (verify == true)
            //{
            //    string opstate = "-1";
            //    int status = 4;
            //    //判断交易是否成功
            //    if (state == "1")
            //    {
            //        opstate = "0";
            //        status = 2;
            //    }

            //    OrderBankUtils.SuppPageReturn(SuppId
            //      , sdcustomno
            //      , sd51no
            //      , status
            //      , opstate
            //      , des
            //      , decimal.Parse(ordermoney), decimal.Parse(ordermoney));
            //}
            //else
            //{
            //    HttpContext.Current.Response.Write("签名验证失败");
            //}
            //接收数据
            string returncode = HttpContext.Current.Request["returncode"];
            string userid = HttpContext.Current.Request["userid"];//1:成功 2：失败
            string orderid = HttpContext.Current.Request["orderid"];
            string money = HttpContext.Current.Request["money"];
            string sign = HttpContext.Current.Request["sign"];
            string ext = HttpContext.Current.Request["ext"];
            //签名是否正确
            Boolean verify = false;

            string plain = "returncode={0}&userid={1}&orderid={2}&keyvalue={3}";
            plain = string.Format(plain, returncode, userid, orderid, SuppKey);
            string signature1 = Cryptography.MD5(plain).ToLower();
            if (signature1 == sign)
            {
                verify = true;
            }
            string info = "";
            //判断签名验证是否通过
            if (verify == true)
            {
                string opstate = "-1";
                int status1 = 4;
                //判断交易是否成功
                if (returncode == "1")
                {
                    info = "支付成功";
                    opstate = "0";
                    status1 = 2;
                }

                OrderBankUtils.SuppPageReturn(SuppId
                    , orderid
                    , ""
                    , status1
                    , opstate
                    , GetErrMsg(int.Parse(returncode))
                    , decimal.Parse(money), 0M);
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
            //string state = HttpContext.Current.Request["state"];
            //string customerid = HttpContext.Current.Request["customerid"];
            //string sd51no = HttpContext.Current.Request["sd51no"];
            //string sdcustomno = HttpContext.Current.Request["sdcustomno"];
            //string ordermoney = HttpContext.Current.Request["ordermoney"];
            //string cardno = HttpContext.Current.Request["cardno"];
            //string mark = HttpContext.Current.Request["mark"];
            //string sign = HttpContext.Current.Request["sign"];
            //string resign = HttpContext.Current.Request["resign"];
            //string des = HttpContext.Current.Request["des"];
            ////签名是否正确
            //Boolean verify = false;

            //string plain = "customerid={0}&sd51no={1}&sdcustomno={2}&mark={3}&key={4}";
            //plain = string.Format(plain, customerid, sd51no, sdcustomno, mark, SuppKey);
            //string sign1 = Cryptography.MD5(plain).ToUpper().ToString();

            //if (sign1 == sign)
            //{
            //    verify = true;
            //}
            ////判断签名验证是否通过
            //if (verify == true)
            //{
            //    string opstate = "-1";
            //    int status = 4;
            //    //判断交易是否成功
            //    if (state == "1")
            //    {
            //        opstate = "0";
            //        status = 2;
            //    }

            //    OrderBankUtils.SuppNotify(SuppId
            //      , sdcustomno
            //      , sd51no
            //      , status
            //      , opstate
            //      , des
            //      , decimal.Parse(ordermoney), decimal.Parse(ordermoney)
            //      , Succflag
            //      , Failflag);
            //}
            //else
            //{
            //    HttpContext.Current.Response.Write("fail");
            //}
            //{
            //接收数据
            string returncode = HttpContext.Current.Request["returncode"];
            string userid = HttpContext.Current.Request["userid"];//1:成功 2：失败
            string orderid = HttpContext.Current.Request["orderid"];
            string money = HttpContext.Current.Request["money"];
            string sign = HttpContext.Current.Request["sign"];
            string ext = HttpContext.Current.Request["ext"];
            //签名是否正确
            Boolean verify = false;

            string plain = "returncode={0}&userid={1}&orderid={2}&keyvalue={3}";
            plain = string.Format(plain, returncode, userid, orderid, SuppKey);
            string signature1 = Cryptography.MD5(plain).ToLower();
            if (signature1 == sign)
            {
                verify = true;
            }

            //判断签名验证是否通过
            if (verify == true)
            {
                string opstate = "-1";
                int status1 = 4;
                //判断交易是否成功
                if (returncode == "1")
                {

                    opstate = "0";
                    status1 = 2;
                }

                OrderBankUtils.SuppNotify(SuppId
                    , orderid
                    , ""
                    , status1
                    , opstate
                    , GetErrMsg(int.Parse(returncode))
                    , decimal.Parse(money), 0M, Succflag, Failflag);
            }
            else
            {
                HttpContext.Current.Response.Write("false");
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
                case "101":
                    code = "2003";  //支付宝
                    break;

            }
            return code;
        }
        #endregion
    }
}
