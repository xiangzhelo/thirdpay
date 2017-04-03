using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using viviapi.ETAPI.Common;
using viviapi.Model.Payment;
using viviapi.BLL.Payment;
using viviapi.Model.Order;
using viviapi.Model.supplier;
using viviapi.SysConfig;
using viviapi.Model;
using viviLib.ExceptionHandling;
using viviLib.Web;
using viviLib.Logging;

namespace viviapi.ETAPI.Dinpay
{
    /// <summary>
    /// 环讯接口
    /// </summary>
    public class Bank : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.Dinpay;

        public Bank()
            : base(SuppId)
        {

        }

        internal string Returnurl { get { return this.SiteDomain + "/return/dinpay/bank.aspx"; } }
        internal string NotifyUrl { get { return this.SiteDomain + "/receive/dinpay/bank.aspx"; } }

        #region PayBank
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid">订单号</param>
        /// <param name="orderAmt"></param>
        /// <param name="bankcode"></param>
        /// <param name="autoSubmit"></param>
        /// <returns></returns>
        public string PayBank(string orderid, decimal orderAmt, string bankcode, bool autoSubmit)
        {
            string service_type = "direct_pay";
            string merchant_code = SuppAccount;
            string input_charset = "GB2312";
            string notify_url = NotifyUrl;
            string return_url = Returnurl;
            string client_ip = ServerVariables.TrueIP;
            string interface_version = "V3.0";
            string sign_type = "MD5";

            string order_no = orderid;
            string order_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string order_amount = orderAmt.ToString("f2");
            string product_name = orderid; //商品名称
            string show_url = "";//商品展示URL
            string product_code = "";//商品编号
            string product_num = "1";//商品数量
            string product_desc = ""; //商品描述
            string bank_code = GetBankCode(bankcode);
            string extra_return_param = "";
            string extend_param = "";


            string signSrc = "";
            #region
            //组织订单信息
            if (bank_code != "")
            {
                signSrc = signSrc + "bank_code=" + bank_code + "&";
            }
            if (client_ip != "")
            {
                signSrc = signSrc + "client_ip=" + client_ip + "&";
            }
            if (extend_param != "")
            {
                signSrc = signSrc + "extend_param=" + extend_param + "&";
            }
            if (extra_return_param != "")
            {
                signSrc = signSrc + "extra_return_param=" + extra_return_param + "&";
            }
            if (input_charset != "")
            {
                signSrc = signSrc + "input_charset=" + input_charset + "&";
            }
            if (interface_version != "")
            {
                signSrc = signSrc + "interface_version=" + interface_version + "&";
            }
            if (merchant_code != "")
            {
                signSrc = signSrc + "merchant_code=" + merchant_code + "&";
            }
            if (notify_url != "")
            {
                signSrc = signSrc + "notify_url=" + notify_url + "&";
            }
            if (order_amount != "")
            {
                signSrc = signSrc + "order_amount=" + order_amount + "&";
            }
            if (order_no != "")
            {
                signSrc = signSrc + "order_no=" + order_no + "&";
            }
            if (order_time != "")
            {
                signSrc = signSrc + "order_time=" + order_time + "&";
            }
            if (product_code != "")
            {
                signSrc = signSrc + "product_code=" + product_code + "&";
            }
            if (product_desc != "")
            {
                signSrc = signSrc + "product_desc=" + product_desc + "&";
            }
            if (product_name != "")
            {
                signSrc = signSrc + "product_name=" + product_name + "&";
            }
            if (product_num != "")
            {
                signSrc = signSrc + "product_num=" + product_num + "&";
            }
            if (return_url != "")
            {
                signSrc = signSrc + "return_url=" + return_url + "&";
            }
            if (service_type != "")
            {
                signSrc = signSrc + "service_type=" + service_type + "&";
            }
            if (show_url != "")
            {
                signSrc = signSrc + "show_url=" + show_url + "&";
            }
            signSrc = signSrc + "key=" + SuppKey;
            #endregion
            string singInfo = signSrc;
            string sign = viviLib.Security.Cryptography.MD5(singInfo, input_charset);

            string gateway = "https://pay.dinpay.com/gateway?input_charset=" + input_charset;

            if (!string.IsNullOrEmpty(PostBankUrl))
            {
                gateway = PostBankUrl;

                if (PostBankUrl.IndexOf("?input_charset", StringComparison.Ordinal) < 0)
                {
                    gateway += "?input_charset=" + input_charset;
                }
            }

            string postForm = "<form name=\"frm1\" id=\"frm1\" method=\"post\" action=\"" + gateway + "\">\n";
            postForm += "<input type=\"hidden\" name=\"sign\" value=\"" + sign + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"merchant_code\" value=\"" + merchant_code + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"bank_code\" value=\"" + bank_code + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"order_no\" value=\"" + order_no + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"order_amount\" value=\"" + order_amount + "\" />\n";

            postForm += "<input type=\"hidden\" name=\"service_type\" value=\"" + service_type + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"input_charset\" value=\"" + input_charset + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"notify_url\" value=\"" + notify_url + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"interface_version\" value=\"" + interface_version + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"sign_type\" value=\"" + sign_type + "\" />\n";

            postForm += "<input type=\"hidden\" name=\"order_time\" value=\"" + order_time + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"product_name\" value=\"" + product_name + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"client_ip\" value=\"" + client_ip + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"extend_param\" value=\"" + extend_param + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"extra_return_param\" value=\"" + extra_return_param + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"product_code\" value=\"" + product_code + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"product_desc\" value=\"" + product_desc + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"product_num\" value=\"" + product_num + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"return_url\" value=\"" + return_url + "\" />\n";
            postForm += "<input type=\"hidden\" name=\"show_url\" value=\"" + show_url + "\" />\n";
            postForm += "</form>";

            if (autoSubmit)
            {
                postForm +=
                    "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>";
            }

            return postForm;
        }
        #endregion

        #region Notify
        /// <summary>
        /// 
        /// </summary>
        public void Notify()
        {
            try
            {

                //获取智付GET过来反馈信息
                //商号号
                string merchant_code = GetValue("merchant_code");
                //通知类型
                string notify_type = GetValue("notify_type");
                //通知校验ID
                string notify_id = GetValue("notify_id");
                //接口版本
                string interface_version = GetValue("interface_version");
                //签名方式
                string sign_type = GetValue("sign_type");
                //签名
                string dinpaySign = GetValue("sign");
                //商家订单号
                string order_no = GetValue("order_no");
                //商家订单时间
                string order_time = GetValue("order_time");
                //商家订单金额
                string order_amount = GetValue("order_amount");
                //回传参数
                string extra_return_param = GetValue("extra_return_param");
                //智付交易定单号
                string trade_no = GetValue("trade_no");
                //智付交易时间
                string trade_time = GetValue("trade_time");
                //交易状态 SUCCESS 成功  FAILED 失败
                string trade_status = GetValue("trade_status");
                //银行交易流水号
                string bank_seq_no = GetValue("bank_seq_no");
                /**
                 *签名顺序按照参数名a到z的顺序排序，若遇到相同首字母，则看第二个字母，以此类推，
                *同时将商家支付密钥key放在最后参与签名，组成规则如下：
                *参数名1=参数值1&参数名2=参数值2&……&参数名n=参数值n&key=key值
                **/
                //组织订单信息
                string signStr = "";

                if (bank_seq_no != "")
                {
                    signStr = signStr + "bank_seq_no=" + bank_seq_no + "&";
                }

                if (!string.IsNullOrEmpty(extra_return_param))
                {
                    signStr = signStr + "extra_return_param=" + extra_return_param + "&";
                }
                signStr = signStr + "interface_version=V3.0" + "&";
                signStr = signStr + "merchant_code=" + merchant_code + "&";


                if (notify_id != "")
                {
                    signStr = signStr + "notify_id=" + notify_id + "&notify_type=" + notify_type + "&";
                }

                signStr = signStr + "order_amount=" + order_amount + "&";
                signStr = signStr + "order_no=" + order_no + "&";
                signStr = signStr + "order_time=" + order_time + "&";
                signStr = signStr + "trade_no=" + trade_no + "&";
                signStr = signStr + "trade_status=" + trade_status + "&";

                if (trade_time != "")
                {
                    signStr = signStr + "trade_time=" + trade_time + "&";
                }

                string key = SuppKey;

                signStr = signStr + "key=" + key;
                string signInfo = signStr;

                //将组装好的信息MD5签名
                string sign = viviLib.Security.Cryptography.MD5(signInfo).ToLower(); //注意与支付签名不同  此处对String进行加密

                bool page_notify = (notify_type == "page_notify");
                //比较智付返回的签名串与商家这边组装的签名串是否一致

                if (dinpaySign == sign)
                {
                    //验签成功   
                    /**
		
                    此处进行商户业务操作
		
                    业务结束
                    */

                    //bank_seq_no=201404184666177223&interface_version=V3.0&merchant_code=2060010306&notify_id=edc44e7cf77c47d38c26cb823dc05667&notify_type=offline_notify&order_amount=1&order_no=14041817245151020625&order_time=2014-04-18 17:25:09&trade_no=1002006070&trade_status=SUCCESS&trade_time=2014-04-18 17:25:05&key=www_longpay_13128888806





                    int status = 4;
                    string opstate = "1";
                    decimal tranAMT = 0M;
                    if (trade_status == "SUCCESS")
                    {
                        status = 2;
                        opstate = "0";
                        tranAMT = decimal.Parse(order_amount);
                    }


                    OrderBankUtils.SuppNotify(SuppId
                                     , order_no
                                     , trade_no
                                     , status
                                     , opstate
                                     , string.Empty
                                     , tranAMT
                                     , 0M

                                     , "SUCCESS"
                                     , "Fail");


                }
                else
                {
                    //验签失败 业务结束
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }
        string GetValue(string key)
        {
            string val = HttpContext.Current.Request.Form[key];

            if (string.IsNullOrEmpty(val))
            {
                val = HttpContext.Current.Request.QueryString[key];
            }
            if (!string.IsNullOrEmpty(val))
            {
                return val.Trim();
            }
            return "";
        }
        #endregion

        #region ReturnBank
        /// <summary>
        /// 
        /// </summary>
        public void ReturnBank()
        {
            //获取智付GET过来反馈信息
            //商号号
            string merchant_code = GetValue("merchant_code");

            //通知类型
            string notify_type = GetValue("notify_type");

            //通知校验ID
            string notify_id = GetValue("notify_id");

            //接口版本
            string interface_version = GetValue("interface_version");

            //签名方式
            string sign_type = GetValue("sign_type");

            //签名
            string dinpaySign = GetValue("sign");

            //商家订单号
            string order_no = GetValue("order_no");

            //商家订单时间
            string order_time = GetValue("order_time");

            //商家订单金额
            string order_amount = GetValue("order_amount");

            //回传参数
            string extra_return_param = GetValue("extra_return_param");

            //智付交易定单号
            string trade_no = GetValue("trade_no");

            //智付交易时间
            string trade_time = GetValue("trade_time");

            //交易状态 SUCCESS 成功  FAILED 失败
            string trade_status = GetValue("trade_status");

            //银行交易流水号
            string bank_seq_no = GetValue("bank_seq_no");

            /**
             *签名顺序按照参数名a到z的顺序排序，若遇到相同首字母，则看第二个字母，以此类推，
            *同时将商家支付密钥key放在最后参与签名，组成规则如下：
            *参数名1=参数值1&参数名2=参数值2&……&参数名n=参数值n&key=key值
            **/


            //组织订单信息
            string signStr = "";

            if (bank_seq_no != "")
            {
                signStr = signStr + "bank_seq_no=" + bank_seq_no + "&";
            }

            if (extra_return_param != "")
            {
                signStr = signStr + "extra_return_param=" + extra_return_param + "&";
            }
            signStr = signStr + "interface_version=V3.0" + "&";
            signStr = signStr + "merchant_code=" + merchant_code + "&";


            if (notify_id != "")
            {
                signStr = signStr + "notify_id=" + notify_id + "&notify_type=" + notify_type + "&";
            }

            signStr = signStr + "order_amount=" + order_amount + "&";
            signStr = signStr + "order_no=" + order_no + "&";
            signStr = signStr + "order_time=" + order_time + "&";
            signStr = signStr + "trade_no=" + trade_no + "&";
            signStr = signStr + "trade_status=" + trade_status + "&";

            if (trade_time != "")
            {
                signStr = signStr + "trade_time=" + trade_time + "&";
            }

            string key = SuppKey;

            signStr = signStr + "key=" + key;
            string signInfo = signStr;

            //将组装好的信息MD5签名
            string sign = viviLib.Security.Cryptography.MD5(signInfo).ToLower(); //注意与支付签名不同  此处对String进行加密

            //比较智付返回的签名串与商家这边组装的签名串是否一致
            if (dinpaySign == sign)
            {
                //验签成功   
                /**
		
                此处进行商户业务操作
		
                业务结束
                */
                int status = 4;
                string opstate = "1";
                decimal tranAMT = 0M;

                if (trade_status == "SUCCESS")
                {
                    status = 2;
                    opstate = "0";
                    tranAMT = decimal.Parse(order_amount);
                }

                OrderBankUtils.SuppPageReturn(SuppId
                                       , order_no
                                       , trade_no
                                       , status
                                       , opstate
                                       , string.Empty
                                       , tranAMT
                                       , 0M);
            }
            else
            {
                //验签失败 业务结束
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
                    code = "BCOM"; //中国交通银行
                    break;
                case "980":
                    code = "CMBC"; //中国民生银行
                    break;
                case "974":
                    code = "SDB"; //深圳发展银行
                    break;
                case "985":
                    code = "GDB"; //广东发展银行
                    break;
                case "962":
                    code = "ECITIC"; //中信银行
                    break;
                case "982":
                    code = "HXB"; //华夏银行
                    break;
                case "972":
                    code = "CIB"; //兴业银行
                    break;
                case "971":
                    code = "PSBC"; //中国邮政
                    break;
                case "986":
                    code = "CEBB"; //中国光大银行
                    break;
                case "987":
                    code = "BEA"; //东亚银行
                    break;
                case "978":
                    code = "SPABANK"; //平安银行
                    break;
            }
            return code;
        }
        #endregion
    }
}
