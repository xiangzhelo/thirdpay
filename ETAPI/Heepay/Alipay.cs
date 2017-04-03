using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using viviapi.ETAPI.Common;
using viviapi.Model.supplier;

namespace viviapi.ETAPI.Heepay
{
    public class Alipay : ETAPI.Common.ETAPIBase
    {
        private static int suppid = (int)SupplierCode.HeePay;
        public Alipay() : base(suppid) { }

        internal string Returnurl { get { return this.SiteDomain + "/return/Heepay/WxMobilePay.aspx"; } }
        internal string NotifyUrl { get { return this.SiteDomain + "/receive/Heepay/WxMobilePay.aspx"; } }

        /// <summary>
        /// 获取支付表单
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderAmt"></param>
        /// <param name="bankCode"></param>
        /// <param name="autoSubmit"></param>
        /// <returns></returns>
        public string GetPayForm(string orderId, decimal orderAmt, string bankCode, bool autoSubmit)
        {
            #region 获取参数值
            int version = 1;                                                            //当前接口版本号 1  
            string user_ip = "127_0_0_1";                                                      //用户所在客户端的真实ip。如 127.127.12.12
            string goods_name = HttpUtility.UrlEncode("充值");                                   //商品名称, 长度最长50字符
            string agent_bill_id = orderId;                              //商户系统内部的定单号（要保证唯一）。长度最长50字符
            string goods_note = HttpUtility.UrlEncode("储值卡储值");                                  //支付说明, 长度50字符
            string remark = HttpUtility.UrlEncode("充值");                                           //商户自定义 原样返回,长度最长50字符
            int is_test = 0; //是否测试 1 为测试
            int pay_type = 22;
            decimal pay_amt = orderAmt;                                   //订单总金额 不可为空，单位：元，小数点后保留两位。12.37
            string goods_num = "1";                                     //产品数量,长度最长20字符
            string agent_bill_time = DateTime.Now.ToString("yyyyMMddHHmmss");              //提交单据的时间yyyyMMddHHmmss 20100225102000
            string agent_id = this.SuppAccount;// "2070494";                                                      //商户编号
            string key = this.SuppKey;// "21E1F04F70F44537899B59A6";                                                          //商户密钥
            string pay_code = "";
            /*
             * is_test = 1
            //如果需要测试，请把取消关于is_test的注释  订单会显示详细信息
            */
            int is_phone = 0;
            int is_frame = 0;
            if (IsMobile)
            {
                //wap支付
                is_frame = 0;
                is_phone = 1;
            }
            else
            {
                is_phone = 1;
                //公众号支付
                is_frame = 1;
            }
            #region //签名
            StringBuilder _StringSign = new StringBuilder();
            _StringSign.Append("version=" + version)
                .Append("&agent_id=" + agent_id)
                .Append("&agent_bill_id=" + agent_bill_id)
                .Append("&agent_bill_time=" + agent_bill_time)
                .Append("&pay_type=" + pay_type)
                .Append("&pay_amt=" + pay_amt)
                .Append("&notify_url=" + this.NotifyUrl)
                .Append("&return_url=" + Returnurl)
                .Append("&user_ip=" + user_ip);
            if (is_test == 1)
            {
                _StringSign.Append("&is_test=" + is_test);
            }
            _StringSign.Append("&key=" + key);

            string sign = FormsAuthentication.HashPasswordForStoringInConfigFile(_StringSign.ToString(), "md5").ToLower();
            #endregion
            #endregion
            string postUrl = "https://pay.heepay.com/Payment/Index.aspx";
            if (string.IsNullOrEmpty(this.PostBankUrl))
            {
                postUrl = this.PostBankUrl;
            }

            #region //构造提交表单
            StringBuilder sbPayForm = new StringBuilder();
            sbPayForm.Append("<form id=\"frmSubmit\" method=\"post\" name=\"frmSubmit\" action='" + postUrl + "'>");
            sbPayForm.Append("<input type='hidden' name='version' value='" + version + "' />");
            sbPayForm.Append("<input type='hidden' name='agent_id' value='" + agent_id + "' />");
            sbPayForm.Append("<input type='hidden' name='agent_bill_id' value='" + agent_bill_id + "' />");
            sbPayForm.Append("<input type='hidden' name='agent_bill_time' value='" + agent_bill_time + "' />");
            sbPayForm.Append("<input type='hidden' name='pay_type' value='" + pay_type + "' />");
            sbPayForm.Append("<input type='hidden' name='pay_code' value='" + pay_code + "' />");
            sbPayForm.Append("<input type='hidden' name='pay_amt' value='" + pay_amt + "' />");
            sbPayForm.Append("<input type='hidden' name='notify_url' value='" + this.NotifyUrl + "' />");
            sbPayForm.Append("<input type='hidden' name='return_url' value='" + this.Returnurl + "' />");
            sbPayForm.Append("<input type='hidden' name='user_ip' value='" + user_ip + "' />");
            sbPayForm.Append("<input type='hidden' name='goods_name' value='" + goods_name + "' />");
            sbPayForm.Append("<input type='hidden' name='goods_num' value='" + goods_num + "' />");
            sbPayForm.Append("<input type='hidden' name='goods_note' value='" + goods_note + "' />");
            sbPayForm.Append("<input type='hidden' name='is_test' value='" + is_test + "' />");
            sbPayForm.Append(" <input type='hidden' name='remark' value='" + remark + "' />");
            sbPayForm.Append("<input type='hidden' name='is_phone' value='" + is_phone + "' />");
            sbPayForm.Append("<input type='hidden' name='is_frame' value='" + is_frame + "' />");
            sbPayForm.Append("<input type='hidden' name='sign' value='" + sign + "' />");
            sbPayForm.Append("</form>");
            if (autoSubmit)
                sbPayForm.Append("<script type='text/javascript'>setTimeout(document.frmSubmit.submit(), 100);</script>");
            #endregion
            LogWrite(sbPayForm.ToString());
            return sbPayForm.ToString();

        }
        /// <summary>
        /// 获取ip
        /// </summary>
        /// <returns></returns>
        private string GetIP()
        {
            IPAddress[] arrIPAddresses = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ip in arrIPAddresses)
            {
                if (ip.AddressFamily.Equals(AddressFamily.InterNetwork))
                {
                    return ip.ToString();
                }
            }
            return "";
        }

        /// <summary>
        /// 是否为移动设备的请求
        /// </summary>
        public static bool IsMobile
        {
            get
            {
                Regex RegexMobile = new Regex(@"(iemobile|iphone|ipod|android|nokia|sonyericsson|blackberry|samsung|sec\-|windows ce|motorola|mot\-|up.b|midp\-)");
                var context = HttpContext.Current;
                if (context != null)
                {
                    var request = context.Request;
                    if (request.Browser.IsMobileDevice)
                    {
                        return true;
                    }

                    if (!string.IsNullOrEmpty(request.UserAgent) && RegexMobile.IsMatch(request.UserAgent))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        #region 通知

        internal string Succflag = "SUCCESS";
        internal string Failflag = "FAIL";
        public void Notify(HttpContext context)
        {
            string opstate = "-1";
            int status = 4;

            HttpRequest Request = context.Request;
            #region 获取参数值
            string result = Request["result"];
            string pay_message = Request["pay_message"];
            string agent_id = Request["agent_id"];
            string jnet_bill_no = Request["jnet_bill_no"];
            string agent_bill_id = Request["agent_bill_id"];
            string pay_type = Request["pay_type"];
            string pay_amt = Request["pay_amt"];
            string remark = Request["remark"];
            string returnSign = Request["sign"];

            //获取签名
            StringBuilder sbSign = new StringBuilder();
            sbSign.Append("result=" + result)
                .Append("&agent_id=" + agent_id)
                .Append("&jnet_bill_no=" + jnet_bill_no)
                .Append("&agent_bill_id=" + agent_bill_id)
                .Append("&pay_type=" + pay_type)
                .Append("&pay_amt=" + pay_amt)
                .Append("&remark=" + remark)
                .Append("&key=" + this.SuppKey);


            string sign = FormsAuthentication.HashPasswordForStoringInConfigFile(sbSign.ToString(), "md5").ToLower();

            #endregion

            if (returnSign.Equals(sign))
            {
                string msg = pay_message;

                if (result == "1")
                {
                    msg = "支付成功";
                    opstate = "0";
                    status = 2;
                }
                decimal tranAmt = 0M;
                decimal.TryParse(pay_amt, out tranAmt);
                OrderBankUtils.SuppNotify(suppid
                      , agent_bill_id
                      , jnet_bill_no
                      , status
                      , opstate
                      , msg
                      , tranAmt, tranAmt
                      , Succflag
                      , Failflag);
            }
        }

        #endregion
    }
}
