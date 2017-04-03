using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using viviapi.ETAPI.Common;
using viviapi.Model.Payment;
using viviapi.BLL.Payment;
using viviapi.Model.Order;
using viviapi.Model.supplier;
using viviapi.SysConfig;
using viviapi.Model;
using viviapi.SysInterface.Bank;
using viviLib.Web;
using viviLib.Logging;

namespace viviapi.ETAPI.TaoShang
{
    /// <summary>
    /// 环讯接口
    /// </summary>
    public class Bank : ETAPIBase
    {
        private const int SuppId = (int)SupplierCode.TaoShang;

        public Bank()
            : base(SuppId)
        {

        }

        internal string Returnurl { get { return this.SiteDomain + "/return/taoshang/bank.aspx"; } }
        internal string NotifyUrl { get { return this.SiteDomain + "/receive/taoshang/bank.aspx"; } }

        #region PayBank
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid">订单号</param>
        /// <param name="money">充值</param>       
        /// <returns></returns>
        public string PayBank(string orderid, decimal orderAmt, string BankID)
        {
            string payUrl = PostBankUrl;
            if (string.IsNullOrEmpty(payUrl))
                return string.Empty;

            orderAmt = Decimal.Round(orderAmt, 2);

            string plain = string.Format("parter={0}&type={1}&value={2}&orderid={3}&callbackurl={4}", SuppAccount, BankID, orderAmt, orderid, NotifyUrl) + SuppKey;
            string sign = viviLib.Security.Cryptography.MD5(plain, "GB2312");

            return string.Format("{0}?parter={1}&type={2}&value={3}&orderid={4}&callbackurl={5}&hrefbackurl={6}&sign={7}"
                , payUrl
                , SuppAccount
                , BankID
                , orderAmt
                , orderid
                , NotifyUrl
                , Returnurl
                , sign);
        }
        #endregion

        #region GetPayForm
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="orderAmt"></param>
        /// <param name="BankID"></param>
        /// <param name="autoSubmit"></param>
        /// <returns></returns>
        public string GetPayForm(string orderid, decimal orderAmt, string BankID, bool autoSubmit)
        {
            string payUrl = PostBankUrl;
            if (string.IsNullOrEmpty(payUrl))
                return string.Empty;

            orderAmt = Decimal.Round(orderAmt, 2);

            string plain = string.Format("parter={0}&type={1}&value={2}&orderid={3}&callbackurl={4}", SuppAccount, BankID, orderAmt, orderid, NotifyUrl) + SuppKey;
            string sign = viviLib.Security.Cryptography.MD5(plain, "GB2312");

            //return string.Format("{0}?parter={1}&type={2}&value={3}&orderid={4}&callbackurl={5}&hrefbackurl={6}&sign={7}"
            //    , payUrl
            //    , SuppAccount
            //    , BankID
            //    , orderAmt
            //    , orderid
            //    , NotifyUrl
            //    , Returnurl
            //    , sign);

            string postForm = "<form name=\"frm1\" id=\"frm1\" method=\"get\" target='_blank' action=\"" + payUrl + "\"> \n";
            postForm += "<input type=\"hidden\" name=\"parter\" value=\"" + SuppAccount + "\" /> \n";
            postForm += "<input type=\"hidden\" name=\"type\" value=\"" + BankID + "\" /> \n";
            postForm += "<input type=\"hidden\" name=\"value\" value=\"" + orderAmt + "\" /> \n";
            postForm += "<input type=\"hidden\" name=\"orderid\" value=\"" + orderid + "\" /> \n";

            postForm += "<input type=\"hidden\" name=\"callbackurl\" value=\"" + NotifyUrl + "\" /> \n";
            postForm += "<input type=\"hidden\" name=\"hrefbackurl\" value=\"" + Returnurl + "\" /> \n";
            postForm += "<input type=\"hidden\" name=\"sign\" value=\"" + sign + "\" /> \n";
            postForm += "</form>";

            if (autoSubmit == true)
            {
                //自动提交该表单到测试网关
                postForm +=
                    "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>";
            }

            return postForm;
        }
        #endregion

        #region ReturnBank
        /// <summary>
        /// 
        /// </summary>
        public void ReturnBank()
        {
            HttpRequest req = HttpContext.Current.Request;

            string _orderid = req.QueryString["orderid"];
            string _opstate = req.QueryString["opstate"];
            string _ovalue = req.QueryString["ovalue"];
            string _sign = req.QueryString["sign"];
            string sysorderid = req.QueryString["sysorderid"];
            string systime = req.QueryString["systime"];

            string plain = string.Format("orderid={0}&opstate={1}&ovalue={2}{3}", _orderid, _opstate, _ovalue, SuppKey);
            String localSign = viviLib.Security.Cryptography.MD5(plain);

            try
            {
                if (localSign == _sign)
                {
                    string opstate = "-1";
                    int status = 4;
                    decimal realAmt = 0M;
                    if (_opstate.ToLower() == "0")
                    {
                        opstate = "0";
                        status = 2;
                        realAmt = decimal.Parse(_ovalue);
                    }

                    OrderBankUtils.SuppPageReturn(SuppId
                        , _orderid
                        , sysorderid
                        , status
                        , opstate
                        , ""
                        , realAmt, 0M);

                    //BankUtils bll = new BankUtils();
                    //bll.DoBankComplete(suppId, _orderid, _ekaorderid, status, opstate, string.Empty, decimal.Parse(_ovalue), 0M, false, true);
                    //HttpContext.Current.Response.Write("opstate=0");
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
            }
        }
        #endregion

        String GetErrorInfo(String ErrorCode)
        {
            String info = ErrorCode;
            switch (ErrorCode)
            {
                case "-1":
                    info = "系统忙";
                    break;
                case "1":
                    info = "商户订单号无效";
                    break;
                case "2":
                    info = "银行编码错误";
                    break;
                case "3":
                    info = "商户不存在";
                    break;
                case "4":
                    info = "验证签名失败";
                    break;
                case "5":
                    info = "商户储值关闭";
                    break;
                case "6":
                    info = "金额超出限额";
                    break;
            }
            return info;
        }

        #region Notify
        /// <summary>
        /// 
        /// </summary>
        public void Notify()
        {
            HttpRequest req = HttpContext.Current.Request;

            string _orderid = req.QueryString["orderid"];
            string _opstate = req.QueryString["opstate"];
            string _ovalue = req.QueryString["ovalue"];
            string _sign = req.QueryString["sign"];
            string sysorderid = req.QueryString["sysorderid"];
            string systime = req.QueryString["systime"];

            string plain = string.Format("orderid={0}&opstate={1}&ovalue={2}{3}", _orderid, _opstate, _ovalue, SuppKey);
            String localSign = viviLib.Security.Cryptography.MD5(plain);

            try
            {
                if (localSign == _sign)
                {
                    string opstate = "-1";
                    int status = 4;
                    decimal realAmt = 0M;
                    if (_opstate.ToLower() == "0")
                    {
                        opstate = "0";
                        status = 2;
                        realAmt = decimal.Parse(_ovalue);
                    }
                    OrderBankUtils.SuppNotify(SuppId
             , _orderid
             , sysorderid
             , status
             , opstate
             , ""
             , realAmt
             , 0M
             , "opstate=0"
             , "opstate=-1");
                    //BankUtils bll = new BankUtils();
                    //bll.DoBankComplete(suppId, _orderid, _ekaorderid, status, opstate, string.Empty, decimal.Parse(_ovalue), 0M, true, false);
                    HttpContext.Current.Response.Write("opstate=0");
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
            }
        }
        #endregion

        #region GetBankCode
        /// <summary>
        /// WZCB	温州银行
        /// HKBCHINA	汉口银行
        /// ZHNX	珠海市农村信用合作联社
        /// SDE	顺德农信社
        /// YDXH	尧都信用合作联社
        /// CZCB	浙江稠州商业银行
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
                    code = "COMM"; //中国交通银行
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
                    code = "CITIC"; //中信银行
                    break;
                case "982":
                    code = "HXB"; //华夏银行
                    break;
                case "972":
                    code = "CIB"; //兴业银行
                    break;
                case "984":
                    code = "GNXS"; //广州农村商业银行
                    break;
                case "1015":
                    code = "GZCB"; //广州银行
                    break;
                //case "1016":
                //    code = "CUPS"; //中国银联
                //    break;
                case "976":
                    code = "SHRCB"; //上海农村商业银行
                    break;
                //case "971":
                //    code = "POST"; //中国邮政
                //    break;
                case "989":
                    code = "BCCB"; //北京银行
                    break;
                case "988":
                    code = "CBHB"; //渤海银行
                    break;
                case "990":
                    code = "BJRCB"; //北京农商银行
                    break;
                case "979":
                    code = "NJCB"; //南京银行
                    break;
                case "986":
                    code = "CEB"; //中国光大银行
                    break;
                case "987":
                    code = "HKBEA"; //东亚银行
                    break;
                case "1025":
                    code = "NBCB"; //宁波银行
                    break;
                case "983":
                    code = "HCCB"; //杭州银行
                    break;
                case "978":
                    code = "SZPAB"; //平安银行
                    break;
                //case "1028":
                //    code = "HSB"; //徽商银行
                //    break;
                //case "968":
                //    code = "1000"; //浙商银行
                //    break;
                case "975":
                    code = "BOS"; //上海银行
                    break;
                case "971":
                    code = "PSBC"; //中国邮政储蓄银行
                    break;
                //case "1032":
                //    code = "UPOP"; //银联在线支付
                //    break;
                default:
                    code = "ICBC";
                    break;
            }
            return code;
        }
        #endregion
    }
}
