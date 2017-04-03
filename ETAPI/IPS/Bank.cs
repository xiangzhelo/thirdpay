using System;
using System.Web;
using viviapi.ETAPI.Common;
using viviapi.Model.supplier;
using viviLib.Security;

namespace viviapi.ETAPI.IPS
{
    /// <summary>
    /// 环讯接口
    /// </summary>
    public class Bank: ETAPIBase
    {
        private const int SuppId = (int) SupplierCode.IPS;

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


        internal string Returnurl { get { return this.SiteDomain + "/return/ips/bank.aspx"; } }
        internal string NotifyUrl { get { return this.SiteDomain + "/receive/ips/bank.aspx"; } }


        internal string Succflag = "ipscheckok";
        internal string Failflag = "fail";

        #region PayBank
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="orderAmt"></param>
        /// <param name="bankcode"></param>
        /// <param name="autoSubmit"></param>
        /// <returns></returns>
        public string PayBank(string orderid,decimal orderAmt,string bankcode,bool autoSubmit)
        {
            //提交地址
            string form_url = PostBankUrl;

            //商户号
            string Mer_code = SuppAccount; 
            string Mer_key = SuppKey; 
            //商户订单编号
            string Billno = orderid;
            //订单金额(保留2位小数)
            string Amount = decimal.Round(orderAmt, 2).ToString("0.00");            

            //订单日期
            string BillDate = DateTime.Now.ToString("yyyyMMdd");

            //币种 01--借记卡
            //04--IPS账户支付
            //08--IB支付
            //16--电话支付
            //64--储值卡支付
            string Gateway_Type = "01";

            //支付卡种 
            //RMB 人民币
            //HKD 港币
            //MYR 马币
            //USD USD
            string  Currency_Type = "RMB";

            //语言
            //GB--GB中文
            //EN--英语
            //BIG5--BIG中文
            //JP--日文
            //FR--法文
           // string Lang = "GB";

            //支付结果成功返回的商户URL
            string Merchanturl = Returnurl;

            //支付结果失败返回的商户URL
            string FailUrl = string.Empty; 

            //支付结果错误返回的商户URL
            string ErrorUrl = string.Empty; 

            //商户数据包
            string Attach = string.Empty; 

            //显示金额
            string DispAmount = Amount;

            //订单支付接口加密方式
            //0--不加密
            //2--md5摘要
            //9--错误
            string OrderEncodeType = "5"; 

            //交易返回接口加密方式 
              //<option value="10">老接口</option>
              //<option value="11">md5withRsa</option>
              //<option value="12" selected="selected">md5摘要</option>
              //<option value="9">错误</option>
            string RetEncodeType = "17"; //Request.Form["RetEncodeType"];

            //返回方式
          //<option value="0">无Server to Server</option>
          //<option value="1" selected="selected">有Server to Server</option>
          //<option value="9">错误</option>
            //string Rettype = "0"; //Request.Form["Rettype"];

            //Server to Server 返回页面URL
            string ServerUrl = NotifyUrl;// Request.Form["ServerUrl"];
            string RetType = "1";
            string DoCredit = "1";
            string Bankco = GetBankCode(bankcode);

            string plain = "billno{0}currencytype{1}amount{2}date{3}orderencodetype{4}{5}";
            plain = string.Format(plain, Billno, Currency_Type, Amount, BillDate, OrderEncodeType, Mer_key);
            SynsSummitLogger("plain: " + plain);
            string SignMD5 = viviLib.Security.Cryptography.MD5(plain);
            SynsSummitLogger("SignMD5: " + SignMD5);

            //订单支付接口的Md5摘要，原文=订单号+金额+日期+支付币种+商户证书 
            //string SignMD5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Billno + Amount + BillDate + Currency_Type+Mer_key, "MD5").ToLower();

            string postForm = "<form name=\"frm1\" id=\"frm1\" method=\"post\" action=\"" + form_url + "\">";

            postForm += "<input type=\"hidden\" name=\"Mer_code\" value=\"" + Mer_code + "\" />";
            postForm += "<input type=\"hidden\" name=\"Billno\" value=\"" + Billno + "\" />";
            postForm += "<input type=\"hidden\" name=\"Amount\" value=\"" + Amount + "\" />";
            postForm += "<input type=\"hidden\" name=\"Date\" value=\"" + BillDate + "\" />";
            postForm += "<input type=\"hidden\" name=\"Currency_Type\" value=\"" + Currency_Type + "\" />";
            postForm += "<input type=\"hidden\" name=\"Gateway_Type\" value=\"" + Gateway_Type + "\" />";
           // postForm += "<input type=\"hidden\" name=\"Lang\" value=\"" + Lang + "\" />";
            postForm += "<input type=\"hidden\" name=\"Merchanturl\" value=\"" + Merchanturl + "\" />";
            postForm += "<input type=\"hidden\" name=\"FailUrl\" value=\"" + FailUrl + "\" />";
            postForm += "<input type=\"hidden\" name=\"ErrorUrl\" value=\"" + ErrorUrl + "\" />";
            postForm += "<input type=\"hidden\" name=\"Attach\" value=\"" + Attach + "\" />";
            postForm += "<input type=\"hidden\" name=\"DispAmount\" value=\"" + DispAmount + "\" />";
            postForm += "<input type=\"hidden\" name=\"OrderEncodeType\" value=\"" + OrderEncodeType + "\" />";
            postForm += "<input type=\"hidden\" name=\"RetEncodeType\" value=\"" + RetEncodeType + "\" />";            
            postForm += "<input type=\"hidden\" name=\"ServerUrl\" value=\"" + ServerUrl + "\" />";
            postForm += "<input type=\"hidden\" name=\"SignMD5\" value=\"" + SignMD5 + "\" />";
            postForm += "<input type=\"hidden\" name=\"DoCredit\" value=\"" + DoCredit + "\" />";
            postForm += "<input type=\"hidden\" name=\"Bankco\" value=\"" + Bankco + "\" />";
            postForm += "<input type=\"hidden\" name=\"RetType\" value=\"" + RetType + "\" />";
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
        /// <summary>
        /// 
        /// </summary>
        public void ReturnBank()
        {
            //接收数据
            string billno = HttpContext.Current.Request["billno"];
            string amount = HttpContext.Current.Request["amount"];
            string currency_type = HttpContext.Current.Request["Currency_type"];
            string mydate = HttpContext.Current.Request["date"];
            string succ = HttpContext.Current.Request["succ"];
            string msg = HttpContext.Current.Request["msg"];
            string attach = HttpContext.Current.Request["attach"];
            string ipsbillno = HttpContext.Current.Request["ipsbillno"];
            string retEncodeType = HttpContext.Current.Request["retencodetype"];
            string signature = HttpContext.Current.Request["signature"];

            //签名原文
            string content = billno + amount + mydate + succ + ipsbillno + currency_type;

            //签名是否正确
            Boolean verify = false;
            if (retEncodeType == "17")
            {
                string plain = "billno{0}currencytype{1}amount{2}date{3}succ{4}ipsbillno{5}retencodetype{6}{7}";
                plain = string.Format(plain, billno, currency_type, amount, mydate, succ, ipsbillno, retEncodeType, SuppKey);
                string signature1 = Cryptography.MD5(plain);
                if (signature1 == signature)
                {
                    verify = true;
                }
            }

            string info = "支付失败" + msg;
            //判断签名验证是否通过
            if (verify == true)
            {
                string opstate = "-1";
                int status = 4;
                //判断交易是否成功
                if (succ == "Y")
                {
                    info = "支付成功";
                    opstate = "0";
                    status = 2;
                }
               
                OrderBankUtils.SuppPageReturn(SuppId
                    , billno
                    , ipsbillno
                    , status
                    , opstate
                    , info
                    , decimal.Parse(amount),0M);
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
            //接收数据
            string billno = HttpContext.Current.Request["billno"];
            string amount = HttpContext.Current.Request["amount"];
            string currency_type = HttpContext.Current.Request["Currency_type"];
            string mydate = HttpContext.Current.Request["date"];
            string succ = HttpContext.Current.Request["succ"];
            string msg = HttpContext.Current.Request["msg"];
            string attach = HttpContext.Current.Request["attach"];
            string ipsbillno = HttpContext.Current.Request["ipsbillno"];
            string retEncodeType = HttpContext.Current.Request["retencodetype"];
            string signature = HttpContext.Current.Request["signature"];
            string ipsbanktime = HttpContext.Current.Request["ipsbanktime"];
            //签名原文
            string content = billno + amount + mydate + succ + ipsbillno + currency_type;

            //签名是否正确
            Boolean verify = false;

            //验证方式：11-md5withRSA  12-md5
            if (retEncodeType == "17")
            {
                string merchant_key = this.SuppKey; //"GDgLwwdK270Qj1w4xho8lyTpRQZV9Jm5x4NwWOTThUa4fMhEBK9jOXFrKRT6xhlJuU2FEa89ov0ryyjfJuuPkcGzO5CeVx5ZIrkkt1aBlZV36ySvHOMcNv8rncRiy3DQ";

                string plain = "billno{0}currencytype{1}amount{2}date{3}succ{4}ipsbillno{5}retencodetype{6}{7}";
                plain = string.Format(plain, billno, currency_type, amount, mydate, succ, ipsbillno, retEncodeType, SuppKey);

                string signature1 = viviLib.Security.Cryptography.MD5(plain);
                //Md5摘要
                //string signature1 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(content + merchant_key, "MD5").ToLower();

                if (signature1 == signature)
                {
                    verify = true;
                }
            }
            //判断签名验证是否通过
            if (verify == true)
            {
                string opstate = "-1";
                int status = 4;
                //判断交易是否成功
                if (succ == "Y")
                {
                    opstate = "0";
                    status = 2;
                }

                //viviapi.BLL.OrderBank bll = new viviapi.BLL.OrderBank();
                //bll.DoBankComplete(suppId, billno, ipsbillno, status, opstate, string.Empty, decimal.Parse(amount), 0M, true, false);
                //HttpContext.Current.Response.Write("ipscheckok");

                OrderBankUtils.SuppNotify(SuppId
                  , billno
                  , ipsbillno
                  , status
                  , opstate
                  , succ
                  , decimal.Parse(amount),0M
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
                    code = "00042";  //招商银行
                    break;
                case "967":
                    code = "00004"; //中国工商银行
                    break;
                case "964":
                    code = "00017"; //中国农业银行
                    break;
                case "965":
                    code = "00012"; //中国建设银行
                    break;
                case "963":
                    code = "00083"; //中国银行
                    break;
                case "977":
                    code = "00032"; //浦发银行
                    break;
                case "981":
                    code = "00005"; //中国交通银行
                    break;
                case "980":
                    code = "00013"; //中国民生银行
                    break;
                case "974":
                    code = "00023"; //深圳发展银行
                    break;
                case "985":
                    code = "00052"; //广东发展银行
                    break;
                case "962":
                    code = "00092"; //中信银行
                    break;
                case "982":
                    code = "00041"; //华夏银行
                    break;
                case "972":
                    code = "00016"; //兴业银行
                    break;
                case "984":
                    code = "00011"; //广州农村商业银行
                    break;
                //case "1015":
                //    code = "GZCB"; //广州银行
                //    break;
                //case "1016":
                //    code = "CUPS"; //中国银联
                //    break;
                case "976":
                    code = "00030"; //上海农村商业银行
                    break;
                //case "971":
                //    code = "POST"; //中国邮政
                //    break;
                case "989":
                    code = "00050"; //北京银行
                    break;
                //case "988":
                //    code = "CBHB"; //渤海银行
                //    break;
                case "990":
                    code = "00056"; //北京农商银行
                    break;
                case "979":
                    code = "00055"; //南京银行
                    break;
                case "986":
                    code = "00057"; //中国光大银行
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
                    code = "00087"; //平安银行
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
