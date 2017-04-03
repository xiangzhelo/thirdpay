using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using viviapi.ETAPI.Common;
using viviapi.Model.supplier;
using viviapi.SysConfig;
using viviLib.Security;

namespace viviapi.ETAPI.Alipay
{
    /// <summary>
    /// SuppUserName=>收款支付宝账号
    /// </summary>
    public class AliPayTools : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.AlipayTool;

        public AliPayTools()
            : base(SuppId)
        {
            
        }

        public static AliPayTools Default
        {
            get
            {
                return new AliPayTools();
            }
        }
        private string sendUrl = "https://shenghuo.alipay.com/send/payment/fill.htm";

        internal string NotifyUrl { get { return this.SiteDomain + "/receive/alipay/diytool.aspx"; } }

        public string GetPayForm(string out_trade_no, decimal amount)
        {
            string remark = SuppInfo.desc;
            if (string.IsNullOrEmpty(remark))
            {
                remark = "注意：请勿修改订单信息，否则付款后可能无法领取到商品。";
            }
            var html = new StringBuilder();
            html.AppendFormat("<form id='form1' name='payform' action='{0}' method='POST'>", sendUrl);
            html.AppendFormat("<input type='hidden' name='optEmail' value='{0}'>", SuppUserName);
            html.AppendFormat("<input type='hidden' name='payAmount' value='{0}'>", amount);
            html.AppendFormat("<input type='hidden' name='title' value='{0}'>", out_trade_no);
            html.AppendFormat("<input type='hidden' name='memo' value='{0}'>"
                , remark);
            html.Append("</form>");

            //自动提交该表单到测试网关
            html.Append(
                "<script type=\"text/javascript\" >setTimeout(\"document.getElementById('form1').submit();\",500);</script>");

            // viviLib.Logging.LogHelper.Write(html.ToString());

            return html.ToString();
        }

        public void Notify()
        {
            string tradeNo = GetQueryString("tradeNo", string.Empty);	//支付宝交易号
            string Money = GetQueryString("Money", "0");			        //付款金额
            string title = GetQueryString("title", string.Empty);		//付款说明，一般是网站用户名
            string memo = GetQueryString("memo", string.Empty);			//备注
            string Sign = GetQueryString("Sign", string.Empty);			//签名
            //-----------------------------------------------------------------

            //此处请修改为自己的商户ID (将100修改为您自己的数字ID)
            string WebID = SuppAccount;

            //此处请修改为自己的商户Key (Key = "ABCD" ,修改""号内 ABCD 为您的密钥)
            string Key = SuppKey;

            if (Sign.ToUpper() != Cryptography.MD5(WebID + Key + tradeNo + Money + title + memo).ToUpper())
            {
                HttpContext.Current.Response.Write("Fail");
            }
            else
            {
                /*付款成功
                ********************************************************************
                会员使用支付宝付款时，可以放2个参数，分别是“付款说明”(title)和“备注”(memo)，您可以灵活使用这2个参数进行自动发货
                $UserName	=	$title	'如充值的用户名放在title中
                $remark	=	$memo	'如充值类型放在memo中（付款成功后是开通VIP还是开通其它服务等不同类型）
                *******************************************************************
		
                *******************************************************************
                为了防止用户填错“付款说明”或“备注”导致充值失败，您可以先检查用户名是否存在，再决定自动发货，以解决这个问题
                UserNameIsExist	=	true;	//此处修改为您的检测代码,当然如果您觉得没有必要，也可以不检测
                */

                decimal result = 0M;

                decimal.TryParse(Money, out result);

                OrderBankUtils.SuppNotify(SuppId
                     , title
                     , tradeNo
                     , 2
                     , "0"
                     , "msg"
                     , result
                     , 0M
                     , "Success"
                     , "IncorrectOrder");
            }
        }

        public static string GetQueryString(string QueryString, string defaultValue)
        {
            HttpRequest Request = HttpContext.Current.Request;
            if (Request.QueryString[QueryString] == null)
                return defaultValue;
            if (Request.QueryString[QueryString].Length == 0)
                return defaultValue;
            return Request.QueryString[QueryString].Trim();
        }

        public static string GetFormString(string key, string defaultValue)
        {
            HttpRequest Request = HttpContext.Current.Request;
            if (Request.Form[key] == null)
                return defaultValue;
            if (Request.Form[key].Length == 0)
                return defaultValue;

            return Request.Form[key].Trim();
        }

        public void Notify2()
        {
            
            string key = GetFormString("key", string.Empty);
            string ddh = GetFormString("ddh", string.Empty);
            string cny = GetFormString("cny", "0");		
            string a1 = GetFormString("a1", string.Empty);		
            string a3 = GetFormString("a3", string.Empty);			
            //-----------------------------------------------------------------

            if (a3 != "1")
                return;

            if (key == SuppKey)
            {
                decimal result = 0M;
                if (decimal.TryParse(cny, out result))
                {
                    OrderBankUtils.SuppNotify(SuppId
                    , a1
                    , ddh
                    , 2
                    , "0"
                    , "msg"
                    , result
                    , 0M
                    , "ok"
                    , "fail");
                }

               
            }

            
        }

    }
}

