using System;
using System.Web;
using viviapi.ETAPI.Common;
using viviapi.Model.supplier;
using viviLib.Security;
using System.Web.Security;

namespace viviapi.ETAPI.QianYiFu
{
    /// <summary>
    /// 仟易付接口
    /// </summary>
    public class Bank : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.qianyifu;

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


        internal string Returnurl { get { return this.SiteDomain + "/return/qianyifu/bank.aspx"; } }
        internal string NotifyUrl { get { return this.SiteDomain + "/receive/qianyifu/bank.aspx"; } }


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
            string form_url = PostBankUrl;
            //商户号
            string customerid = SuppAccount;
            string key = SuppKey;
            string banktype =GetBankCode(bankcode);
            string amount = orderAmt.ToString("0.00");
            //银行卡支付
            String param = String.Format("parter={0}&type={1}&value={2}&orderid={3}&callbackurl={4}{5}", customerid, bankcode, amount, orderid, NotifyUrl,key);
            string sign = viviLib.Security.Cryptography.MD5(param, "GB2312").ToLower();
            SynsSummitLogger("plain: " + param);
            
            SynsSummitLogger("SignMD5: " + sign);
            string postForm = "<form name=\"frm1\" id=\"frm1\" method=\"get\" action=\"" + form_url + "\">";
            postForm += "<input type=\"hidden\" name=\"parter\" value=\"" + customerid + "\" />";
            postForm += "<input type=\"hidden\" name=\"type\" value=\"" + bankcode + "\" />";
            postForm += "<input type=\"hidden\" name=\"value\" value=\"" + amount + "\" />";
            postForm += "<input type=\"hidden\" name=\"orderid\" value=\"" + orderid + "\" />";
            postForm += "<input type=\"hidden\" name=\"callbackurl\" value=\"" + NotifyUrl + "\" />";
            postForm += "<input type=\"hidden\" name=\"sign\" value=\"" + sign + "\" />";
            postForm += "<input type=\"hidden\" name=\"agent\" value=\"\" />";
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
            String key = SuppKey;//配置文件密钥
            //返回参数
            String orderid = HttpContext.Current.Request["orderid"];//返回订单号
            String opstate = HttpContext.Current.Request["opstate"];//返回处理结果
            String ovalue = HttpContext.Current.Request["ovalue"];//返回实际充值金额
            String sign = HttpContext.Current.Request["sign"];//返回签名
            String ekaorderID = HttpContext.Current.Request["ekaorderid"];//录入时产生流水号。
            String ekatime = HttpContext.Current.Request["ekatime"];//处理时间。
            String attach = HttpContext.Current.Request["attach"];//上行附加信息
            String msg = HttpContext.Current.Request["msg"];//返回订单处理消息

            String param = String.Format("orderid={0}&opstate={1}&ovalue={2}{3}", orderid, opstate, ovalue, key);//组织参数
            //比对签名是否有效
            if (sign.Equals(FormsAuthentication.HashPasswordForStoringInConfigFile(param, "MD5").ToLower()))
            {
                string _info = "支付失败";
                string opstate1 = "-1";
                int status = 4;


                if (opstate.Equals("0") || opstate.Equals("-3"))
                {
                    _info = "支付成功";
                    opstate1 = "0";
                    status = 2;

                }

                string returnUrl = string.Empty;

                OrderBankUtils.SuppPageReturn(SuppId
                                        , SuppKey
                                        , orderid
                                        , status
                                        , opstate1
                                        , string.Empty
                                        , Convert.ToDecimal(ovalue), 0M);
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

            string plain = "customerid={0}&sd51no={1}&sdcustomno={2}&key={3}";
            plain = string.Format(customerid, sd51no, sdcustomno, SuppKey);
            string sign1 = Cryptography.MD5(plain).ToString();

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
                  , state
                  , decimal.Parse(ordermoney), 0M
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
                    code = "970";  //招商银行
                    break;
                case "967":
                    code = "967"; //中国工商银行
                    break;
                case "964":
                    code = "964"; //中国农业银行
                    break;
                case "965":
                    code = "965"; //中国建设银行
                    break;
                case "963":
                    code = "963"; //中国银行
                    break;
                case "977":
                    code = "977"; //浦发银行
                    break;
                case "981":
                    code = "981"; //中国交通银行
                    break;
                case "980":
                    code = "980"; //中国民生银行
                    break;
                case "974":
                    code = "974"; //深圳发展银行
                    break;
                case "985":
                    code = "985"; //广东发展银行
                    break;
                case "962":
                    code = "962"; //中信银行
                    break;
                case "982":
                    code = "982"; //华夏银行
                    break;
                case "972":
                    code = "972"; //兴业银行
                    break;
                case "984":
                    code = "984"; //广州农村商业银行
                    break;
                //case "1015":
                //    code = "GZCB"; //广州银行
                //    break;
                //case "1016":
                //    code = "CUPS"; //中国银联
                //    break;
                case "976":
                    code = "976"; //上海农村商业银行
                    break;
                //case "971":
                //    code = "POST"; //中国邮政
                //    break;
                case "989":
                    code = "989"; //北京银行
                    break;
                //case "988":
                //    code = "CBHB"; //渤海银行
                //    break;
                case "990":
                    code = "990"; //北京农商银行
                    break;
                case "979":
                    code = "979"; //南京银行
                    break;
                case "986":
                    code = "986"; //中国光大银行
                    break;
                //case "987":
                //    code = "BEA"; //东亚银行
                //    break;
                //case "1025":
                //    code = "NBCB"; //宁波银行
                //    break;
                case "983":
                    code = "983"; //杭州银行
                    break;
                case "978":
                    code = "978"; //平安银行
                    break;
                //case "1028":
                //    code = "HSB"; //徽商银行
                //    break;
                case "968":
                    code = "968"; //浙商银行
                    break;
                case "975":
                    code = "975"; //上海银行
                    break;
                case "971":
                    code = "971"; //中国邮政储蓄银行
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
