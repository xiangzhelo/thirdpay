using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using viviapi.ETAPI.Common;
using viviapi.Model.supplier;
using viviLib.Security;

namespace viviapi.ETAPI.Card51
{
    public class TenPay : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.Card51;

        public TenPay()
            : base(SuppId)
        {

        }

        public static TenPay Instance
        {
            get
            {
                var instance = new TenPay();
                return instance;
            }
        }


        internal string Returnurl { get { return this.SiteDomain + "/return/card51/tenpay.aspx"; } }
        internal string NotifyUrl { get { return this.SiteDomain + "/receive/card51/tenpay.aspx"; } }


        internal string Succflag = "<result>1</result>";
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
        public string PayBank(string orderid, decimal orderAmt, string bankcode, bool autoSubmit)
        {
            //提交地址
            string form_url = "http://www.51card.cn/gateway/tenpay/tenpay.asp";
            //商户号
            string customerid = SuppAccount;
            string Mer_key = SuppKey;
            //商户订单编号
            string sdcustomno = orderid;
            //订单金额
            string ordermoney = decimal.Round(orderAmt * 100, 0).ToString();
            string cardno = "35";

            string noticeurl = NotifyUrl;
            string backurl = Returnurl;
            string mark = "chongzhi";

            string plain = "customerid={0}&sdcustomno={1}&orderAmount={2}&cardno={3}&noticeurl={4}&backurl={5}{6}";
            plain = string.Format(plain, customerid, sdcustomno, ordermoney, cardno, noticeurl, backurl, Mer_key);
            SynsSummitLogger("plain: " + plain);
            string sign = viviLib.Security.Cryptography.MD5(plain).ToUpper();
            SynsSummitLogger("SignMD5: " + sign);
            string postForm = "<form name=\"frm1\" id=\"frm1\" method=\"get\" action=\"" + form_url + "\">";
            postForm += "<input type=\"hidden\" name=\"customerid\" value=\"" + customerid + "\" />";
            postForm += "<input type=\"hidden\" name=\"sdcustomno\" value=\"" + sdcustomno + "\" />";
            postForm += "<input type=\"hidden\" name=\"ordermoney\" value=\"" + ordermoney + "\" />";
            postForm += "<input type=\"hidden\" name=\"cardno\" value=\"" + cardno + "\" />";

            postForm += "<input type=\"hidden\" name=\"noticeurl\" value=\"" + noticeurl + "\" />";
            postForm += "<input type=\"hidden\" name=\"backurl\" value=\"" + backurl + "\" />";

            postForm += "<input type=\"hidden\" name=\"sign\" value=\"" + sign + "\" />";
            postForm += "<input type=\"hidden\" name=\"mark\" value=\"" + mark + "\" />";
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
            string orderId = HttpContext.Current.Request["sdcustomno"];
            string status = HttpContext.Current.Request["state"];//1:成功 2：失败
            string tradeNo = HttpContext.Current.Request["sd51no"];
            string sign = HttpContext.Current.Request["sign"];

            //签名是否正确
            Boolean verify = false;

            string plain = "sdcustomno={0}&state={1}&sd51no={2}&key={3}";
            plain = string.Format(plain, orderId, status, tradeNo, SuppKey);
            string signature1 = Cryptography.MD5(plain).ToUpper();
            if (signature1 == sign)
            {
                verify = true;
            }


            string info = "支付失败";
            //判断签名验证是否通过
            if (verify == true)
            {
                string opstate = "-1";
                int status1 = 4;
                //判断交易是否成功
                if (status == "1")
                {
                    info = "支付成功";
                    opstate = "0";
                    status1 = 2;
                }
                return;
                OrderBankUtils.SuppPageReturn(
                    SuppId
                    , orderId
                    , tradeNo
                    , status1
                    , opstate
                    , info
                    , 0m, 0M);
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

            string state = HttpContext.Current.Request["state"];
            string customerid = HttpContext.Current.Request["customerid"];
            string sd51no = HttpContext.Current.Request["sd51no"];
            string sdcustomno = HttpContext.Current.Request["sdcustomno"];
            string ordermoney = HttpContext.Current.Request["ordermoney"];
            string cardno = HttpContext.Current.Request["cardno"];
            string mark = HttpContext.Current.Request["mark"];//1:成功 2：失败
            string sign = HttpContext.Current.Request["sign"];
            string resign = HttpContext.Current.Request["resign"];
            string des = HttpContext.Current.Request["des"];
            //签名是否正确
            Boolean verify = false;

            string plain = "customerid={0}&sd51no={1}&sdcustomno={2}&mark=&key={3}";
            plain = string.Format(plain, customerid, sd51no, sdcustomno, SuppKey);
            string sign1 = Cryptography.MD5(plain).ToUpper().ToString();

            if (sign1 == sign)
            {
                verify = true;
            }
            //判断签名验证是否通过
            if (verify == true)
            {
                string opstate = "-1";
                int status = 4;
                //判断交易是否成功
                if (state == "1")
                {
                    opstate = "0";
                    status = 2;
                }

                OrderBankUtils.SuppNotify(SuppId
                  , sdcustomno
                  , sd51no
                  , status
                  , opstate
                  , des
                  , decimal.Parse(ordermoney), decimal.Parse(ordermoney)
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
