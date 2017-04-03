
using viviapi.BLL;
using vivipai.ETAPI;
using viviLib.ExceptionHandling;
using viviLib.Security;
using System;
using System.Web;
using viviapi.ETAPI.Common;
using viviapi.Model.Order.Card;
using viviapi.Model.supplier;
namespace vivipai.ETAPI.Longbao
{
    public class Bank : ETAPIBase
    {
        private static int suppId = (int)SupplierCode.LongBaoPay;

        public Bank()
            : base(suppId)
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
        public string GetBankCode(string paymodeId)
        {
            switch (paymodeId)
            {
                case "970":
                    return "970";

                case "967":
                    return "967";

                case "964":
                    return "964";

                case "965":
                    return "965";

                case "963":
                    return "963";

                case "977":
                    return "977";

                case "981":
                    return "981";

                case "980":
                    return "980";

                case "974":
                    return "974";

                case "985":
                    return "985";

                case "962":
                    return "962";

                case "982":
                    return "982";

                case "972":
                    return "972";

                case "984":
                    return "984";

                case "1015":
                    return "1015";

                case "976":
                    return "976";

                case "989":
                    return "989";

                case "988":
                    return "988";

                case "990":
                    return "990";

                case "979":
                    return "979";

                case "986":
                    return "986";

                case "987":
                    return "987";

                case "1025":
                    return "1025";

                case "983":
                    return "983";

                case "978":
                    return "978";

                case "975":
                    return "975";

                case "971":
                    return "971";

                case "993":
                    return "993";

                case "992":
                    return "992";

                case "1004":
                    return "1004";

                case "1003":
                    return "1003";
            }
            return "ICBC";
        }

        private string GetErrorInfo(string ErrorCode)
        {
            string str = ErrorCode;
            switch (ErrorCode)
            {
                case "-1":
                    return "系统忙";

                case "1":
                    return "商户订单号无效";

                case "2":
                    return "银行编码错误";

                case "3":
                    return "商户不存在";

                case "4":
                    return "验证签名失败";

                case "5":
                    return "商户储值关闭";

                case "6":
                    return "金额超出限额";
            }
            return str;
        }

        public void Notify()
        {
            HttpRequest request = HttpContext.Current.Request;
            string orderId = request.QueryString["orderid"];
            string opstate = request.QueryString["opstate"];
            string ovalue = request.QueryString["ovalue"];
            string sign = request.QueryString["sign"];
            string supplierOrderId = request.QueryString["sysorderid"];
            string str6 = request.QueryString["completiontime"];
            string attach = request.QueryString["attach"];
            string msg = request.QueryString["msg"];
            string str8 = Cryptography.MD5(string.Format("orderid={0}&opstate={1}&ovalue={2}{3}", new object[] { orderId, opstate, ovalue, base.SuppKey }));
            try
            {
                if (str8 == sign)
                {
                    string opstate1 = "-1";
                    int status = 4;
                    if (opstate.ToLower() == "0")
                    {
                        opstate1 = "0";
                        status = 2;
                    }
                    string viewMsg = "成功";

                    var response = new CardOrderSupplierResponse()
                    {
                        SupplierId = suppId,
                        SuppTransNo = supplierOrderId,
                        SysOrderNo = orderId,
                        OrderAmt = decimal.Parse(ovalue),
                        SuppAmt = 0M,
                        OrderStatus = status,
                        SuppErrorCode = opstate,
                        Opstate = opstate1,
                        SuppErrorMsg = viewMsg,
                        ViewMsg = viewMsg,
                        Method = 1
                    };


                    OrderBankUtils.SuppNotify(suppId, orderId, supplierOrderId, status, opstate, "", decimal.Parse(ovalue), 0M, "opstate=0", "opstate=-1");

                    HttpContext.Current.Response.Write("opstate=0");
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
        }

        public string PayBank(string orderid, decimal orderAmt, string BankID, bool autoSubmit)
        {
            string PostBankUrl = base.PostBankUrl;
            if (string.IsNullOrEmpty(PostBankUrl))
            {
                return string.Empty;
            }

            string amount = decimal.Round(orderAmt, 2).ToString("0.00");
            string key = base.SuppKey;
            //string key = "33bf98be6b104eb0a96baf6224abc485";
            string hashstr = string.Format("parter={0}&type={1}&value={2}&orderid={3}&callbackurl={4}{5}", new object[] { base.SuppAccount, GetBankCode(BankID), amount, orderid, this.notifyUrl, key });
            string sign = Cryptography.MD5(hashstr, "GB2312");

            string postForm = "<form name=\"frm1\" id=\"frm1\" method=\"get\" action=\"" + PostBankUrl + "\">\n";
            postForm += "<input type=\"hidden\" name=\"parter\" value=\"" + base.SuppAccount + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"type\" value=\"" + BankID + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"value\" value=\"" + amount + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"orderid\" value=\"" + orderid + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"callbackurl\" value=\"" + this.notifyUrl + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"hrefbackurl\" value=\"" + this.returnurl + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"sign\" value=\"" + sign + "\" />\n";

            //postForm += "<input type=\"submit\" value=\"提交\" />";

            postForm += "</form>";

            if (autoSubmit)
            {
                postForm +=
                    "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>";
            }

            return postForm;
        }

        public void ReturnBank()
        {
            HttpRequest request = HttpContext.Current.Request;
            string orderId = request.QueryString["orderid"];
            string str2 = request.QueryString["opstate"];
            string s = request.QueryString["ovalue"];
            string str4 = request.QueryString["sign"];
            string supplierOrderId = request.QueryString["ekaorderid"];
            string str6 = request.QueryString["ekatime"];
            string str8 = Cryptography.MD5(string.Format("orderid={0}&opstate={1}&ovalue={2}{3}", new object[] { orderId, str2, s, base.SuppKey }));
            try
            {
                if (str8 == str4)
                {
                    string opstate = "-1";
                    int status = 4;
                    if (str2.ToLower() == "0")
                    {
                        opstate = "0";
                        status = 2;
                    }
                    OrderBankUtils.SuppNotify(suppId, orderId, supplierOrderId, status, opstate, "", decimal.Parse(s), 0M, "opstate=0", "opstate=-1");
                    HttpContext.Current.Response.Write("opstate=0");
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
        }
        /// <summary>
        /// 异步
        /// </summary>
        internal string notifyUrl
        {
            get
            {
                return (base.SiteDomain + "/receive/longbao/Bank.aspx");
            }
        }

        internal string returnurl
        {
            get
            {
                return (base.SiteDomain + "/return/longbao/Bank.aspx");
            }
        }
    }
}

