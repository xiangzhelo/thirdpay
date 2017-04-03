using System;
using System.Globalization;
using System.Text;
using System.Web;
using viviapi.BLL.Supplier;
using viviapi.ETAPI.Card51;
using viviapi.ETAPI.Heepay;
using viviapi.ETAPI.Tenpay;
using viviapi.Model;
using viviapi.Model.supplier;

namespace viviapi.ETAPI.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class SellFactory
    {
        #region OnlineBankPay
        /// <summary>
        /// 在线支付
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="userkey">API密钥</param>
        /// <param name="suppid"></param>
        /// <param name="orderid"></param>
        /// <param name="orderAmt">订单金额</param>
        /// <param name="bankcode">银行代号</param>
        /// <param name="jump">是否跳转</param>
        public static void OnlineBankPay(int userid, string userkey, int suppid, string orderid, decimal orderAmt, string bankcode, bool jump)
        {
            SupplierInfo suppInfo = Factory.GetCacheModel(suppid);
            if (suppInfo == null)
            {
                HttpContext.Current.Response.Write("not find supplier object");
                HttpContext.Current.Response.End();
            }

            string payForm = string.Empty;

            if (jump)//
            {
                payForm = GetJumpForm(userid, userkey, suppid, orderid,
                        orderAmt, bankcode);

            }
            else
            {
                bool isdcode = (bankcode == "1003") || (bankcode == "1004");
                if (isdcode == false)
                {
                    payForm = GetPayForm(suppid, orderid,
                       orderAmt, bankcode, true);

                }
                else
                {
                    payForm = GetDCodeForm(userid
                       , userkey
                       , suppid
                       , orderid
                       , orderAmt
                       , bankcode);
                }
            }

            if (string.IsNullOrEmpty(payForm))
                payForm = "error";

            //viviLib.Logging.LogHelper.Write(payForm);

            HttpContext.Current.Response.Write(payForm);

        }

        #endregion

        #region GetPayFrom
        /// <summary>
        /// 获取POST提交表单
        /// </summary>
        /// <param name="suppid">接口供应商</param>
        /// <param name="orderid">订单号</param>
        /// <param name="orderAmt">订单金额</param>
        /// <param name="bankcode">银行代号</param>
        /// <param name="autosumit">自动提交</param>
        /// <returns></returns>
        public static string GetPayForm(int suppid, string orderid, decimal orderAmt, string bankcode, bool autosumit)
        {
            string payForm = string.Empty;

            switch (suppid)
            {
                case (int)SupplierCode.Alipay:
                    if (bankcode == "101")
                    {
                        var alipay = new Alipay.AliPay(); //支付宝直连
                        payForm = alipay.GetPayForm(orderid, orderAmt, autosumit);
                    }
                    else
                    {
                        var malipay = new Alipay.AliPayMApi(); //支付宝银行直连
                        payForm = malipay.GetPayForm(orderid, orderAmt, bankcode, autosumit);
                    }
                    break;
                //todo:添加财付通
                case (int)SupplierCode.Tenpay://财付通
                    {
                        var tenpay = new TenPayRMB();
                        payForm = tenpay.GetPayForm(orderid, orderAmt, bankcode, autosumit, HttpContext.Current);
                    }
                    break;

                case (int)SupplierCode.YeePay://易宝
                    {
                        var yeepay = new YeePay.RMB();
                        payForm = yeepay.GetPayForm(orderid, orderAmt, bankcode, autosumit);
                    }
                    break;
                case (int)SupplierCode.YeePayZGT://易宝掌柜通
                    {
                        var zgt = new YeePay.ZGT.Bank();
                        payForm = zgt.PayBank(orderid, orderAmt, bankcode, autosumit);
                    }
                    break;
                case (int)SupplierCode.IPS://环迅
                    {
                        var api = new IPS.Bank();
                        payForm = api.PayBank(orderid, orderAmt, bankcode, autosumit);
                    }
                    break;

                case (int)SupplierCode.zhifu41://41支付
                    {
                        var api = new ZhiFu41.Bank();
                        payForm = api.PayBank(orderid, orderAmt, bankcode, autosumit);
                    }
                    break;

                case (int)SupplierCode.Card51://51卡
                    {
                        if (bankcode == "992" || bankcode == "101")
                        {
                            var api = new Card51.AliPay();//51卡的支付宝接口
                            payForm = api.PayBank(orderid, orderAmt, bankcode, autosumit);
                        }
                        else if (bankcode == "1004")
                        {
                            var api = new Card51.WeiXin();//51卡的微信接口
                            payForm = api.PayBank(orderid, orderAmt, bankcode, autosumit);
                        }
                        else if (bankcode == "1005")
                        {
                            var api = new Card51.QqPay();//51卡的qq扫码支付
                            payForm = api.PayBank(orderid, orderAmt, bankcode, autosumit);
                        }
                        else if (bankcode == "1007")
                        {
                            var api = new WapWeiXin();
                            payForm= api.PayBank(orderid, orderAmt, bankcode, autosumit);
                        }
                        else
                        {
                            var api = new Card51.Bank();//51卡的网银支付接口
                            payForm = api.PayBank(orderid, orderAmt, bankcode, autosumit);
                        }

                    }
                    break;

                case (int)SupplierCode.qianyifu://仟易付
                    {
                        var api = new QianYiFu.Bank();
                        payForm = api.PayBank(orderid, orderAmt, bankcode, autosumit);
                    }
                    break;
                case (int)SupplierCode.LongBaoPay: //易卡支付
                    {

                        var api = new vivipai.ETAPI.Longbao.Bank();
                        payForm = api.PayBank(orderid, orderAmt, bankcode, autosumit);

                    }
                    break;
                case (int)SupplierCode.Zline:
                    {
                        var api = new ETAPI.Zline.Bank();
                        payForm = api.PayBank(orderid, orderAmt, bankcode, autosumit);
                    }
                    break;
                case (int)SupplierCode.Cared70:
                    {
                        if (bankcode == "101" || bankcode == "992")
                        {
                            var api = new Cared70.AliPay();//70卡的支付宝接口
                            payForm = api.PayBank(orderid, orderAmt, bankcode, autosumit);
                        }
                        else if (bankcode == "1004")
                        {
                            var api = new Cared70.WeiXin();//70卡的微信接口
                            payForm = api.PayBank(orderid, orderAmt, bankcode, autosumit);
                        }
                    }
                    break;
                //case (int)SupplierCode.AlipayTool:
                //    {
                //        var tenpay = new Alipay.AliPayTools();
                //        payForm = tenpay.GetPayForm(orderid, orderAmt);
                //    }
                //    break;
                //case (int)SupplierCode.Baofoo:
                //    {
                //        var api = new Baofoo.Bank();
                //        payForm = api.PayBank(orderid, orderAmt, bankcode, autosumit);
                //    }
                //    break;

                //case (int)SupplierCode.Gopay:
                //    {
                //        var api = new Gopay.Bank();
                //        payForm = api.PayBank(orderid, orderAmt, bankcode, autosumit);
                //    }
                //    break;

                //case (int)SupplierCode.Ebatong:
                //    {
                //        var api = new Ebatong.Bank();
                //        payForm = api.PayBank(orderid, orderAmt, bankcode, autosumit);
                //    }
                //    break;

                //case (int)SupplierCode.Dinpay:
                //    {
                //        var api = new Dinpay.Bank();
                //        payForm = api.PayBank(orderid, orderAmt, bankcode, autosumit);
                //    }
                //    break;



                case (int)SupplierCode.Ecpss:
                    {
                        var api = new Ecpss.Bank();//汇潮支付
                        payForm = api.GetPayForm(orderid, orderAmt, bankcode, autosumit);
                    }
                    break;
                //case (int)SupplierCode.TaoShang:
                //    {
                //        var api = new TaoShang.Bank();
                //        payForm = api.GetPayForm(orderid, orderAmt, bankcode, autosumit);
                //    }
                //    break;
                case (int)SupplierCode.Zweixin:
                    {
                        var api = new viviapi.ETAPI.Zweixin.ZweixinPay();
                        payForm = api.GetPayForm(orderid, orderAmt, bankcode, autosumit, HttpContext.Current);
                    }
                    break;
                case (int)SupplierCode.HeePay:
                    {
                        var api = new WxMobilePay();
                        payForm = api.GetPayForm(orderid, orderAmt, bankcode, autosumit);
                    }
                    break;
                case (int)SupplierCode.ZFuPay:
                    {
                        var api = new viviapi.ETAPI.ZFuPay.PostPay();
                        payForm = api.GetPayForm(orderid, orderAmt, bankcode, autosumit);
                    }
                    break;
            }
            return payForm;
        }

        #endregion

        #region GetJumpForm
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="userkey"></param>
        /// <param name="suppid"></param>
        /// <param name="orderid"></param>
        /// <param name="orderAmt"></param>
        /// <param name="bankcode"></param>
        /// <returns></returns>
        public static string GetJumpForm(int userid, string userkey, int suppid, string orderid, decimal orderAmt, string bankcode)
        {
            string form_url = "/gotopay.aspx";

            DateTime time = DateTime.Now;

            string postForm = "<form name=\"payform\" id=\"payform\" method=\"post\" action=\"" + form_url + "\">";

            string sign = string.Format("userid={0}&suppid={1}&orderid={2}&orderAmt={3}&bankcode={4}&time={5}{6}"
                , userid
                , suppid
                , orderid
                , orderAmt
                , bankcode
                , time
                , userkey);

            sign = viviLib.Security.Cryptography.MD5(sign);

            postForm += "<input type=\"hidden\" name=\"userid\" value=\"" + userid + "\" />";
            postForm += "<input type=\"hidden\" name=\"suppid\" value=\"" + suppid + "\" />";
            postForm += "<input type=\"hidden\" name=\"orderid\" value=\"" + orderid + "\" />";
            postForm += "<input type=\"hidden\" name=\"orderAmt\" value=\"" + orderAmt + "\" />";
            postForm += "<input type=\"hidden\" name=\"bankcode\" value=\"" + bankcode + "\" />";
            postForm += "<input type=\"hidden\" name=\"time\" value=\"" + time + "\" />";
            postForm += "<input type=\"hidden\" name=\"sign\" value=\"" + sign + "\" />";
            postForm += "</form>";

            postForm += ("<script type=\"text/javascript\" language=\"javascript\">function go(){ var _form = document.forms['payform']; _form.submit();};setTimeout(function(){go()},100);</script>");


            return postForm;
        }
        #endregion

        #region GetDCodeForm
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="userkey"></param>
        /// <param name="suppid"></param>
        /// <param name="orderid"></param>
        /// <param name="orderAmt"></param>
        /// <param name="bankcode"></param>
        /// <returns></returns>
        public static string GetDCodeForm(int userid, string userkey, int suppid, string orderid, decimal orderAmt, string bankcode)
        {
            string form_url = "/AliDCode.aspx";
            if (bankcode == "1004")
            {
                if (suppid == (int)SupplierCode.YeePayZGT)
                {
                    form_url = "/WxZGTCode.aspx";
                }
                else
                {
                    form_url = "/WeCode.aspx";
                }
            }

            DateTime time = DateTime.Now;

            string postForm = "<form name=\"payform\" id=\"payform\" method=\"get\" action=\"" + form_url + "\">";

            string sign = string.Format("userid={0}&suppid={1}&orderid={2}&orderAmt={3}&bankcode={4}&time={5}{6}"
                , userid
                , suppid
                , orderid
                , orderAmt
                , bankcode
                , time
                , userkey);

            sign = viviLib.Security.Cryptography.MD5(sign);

            postForm += "<input type=\"hidden\" name=\"userid\" value=\"" + userid + "\" />";
            postForm += "<input type=\"hidden\" name=\"suppid\" value=\"" + suppid + "\" />";
            postForm += "<input type=\"hidden\" name=\"orderid\" value=\"" + orderid + "\" />";
            postForm += "<input type=\"hidden\" name=\"orderAmt\" value=\"" + orderAmt + "\" />";
            postForm += "<input type=\"hidden\" name=\"bankcode\" value=\"" + bankcode + "\" />";
            postForm += "<input type=\"hidden\" name=\"time\" value=\"" + time + "\" />";
            postForm += "<input type=\"hidden\" name=\"sign\" value=\"" + sign + "\" />";
            postForm += "</form>";

            postForm += ("<script type=\"text/javascript\" language=\"javascript\">function go(){ var _form = document.forms['payform']; _form.submit();};setTimeout(function(){go()},100);</script>");


            return postForm;
        }
        #endregion

        #region ReqDistribution
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        public static void ReqDistribution(Model.Finance.WithdrawSuppTranLog info)
        {
            //bool result = false;
            //if (info.suppid == 100)
            //{
            //    tenpay.distribution.gw api = new viviapi.ETAPI.tenpay.distribution.gw();
            //    result = api.DoTrans(info);
            //}
            //else if (info.suppid == 1004)
            //{
            //    //Ebatong.distribution.gw api = new viviapi.ETAPI.Ebatong.distribution.gw();
            //    //result = api.DoTrans(info);
            //}
        }
        #endregion
    }
}
