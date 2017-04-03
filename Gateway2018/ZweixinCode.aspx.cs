using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL.Order.Bank;
using viviapi.BLL.Supplier;
using viviapi.Model.supplier;
using viviLib.ExceptionHandling;
using viviLib.Web;

namespace viviAPI.Gateway2018
{
    public partial class ZweixinCode : System.Web.UI.Page
    {
        viviapi.BLL.Supplier.SupplierAccount suppAcctBll = new SupplierAccount();

        protected string qrcode_img_url = "";
        protected string code_url = "";
        protected string orderAmt = "";
        protected string orderid = "";
        protected string userid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            bool success = false;

            string bankcode = "";
            int suppid = 0;
            try
            {
                userid = Request.QueryString["userid"];
                suppid = WebBase.GetQueryStringInt32("suppid", 0);
                orderid = Request.QueryString["orderid"];
                orderAmt = Request.QueryString["orderAmt"];
                bankcode = Request.QueryString["bankcode"];
                string time = Request.QueryString["time"];
                string sign = Request.QueryString["sign"];

                //hforderid.Value = orderid;

                #region

                if (!string.IsNullOrEmpty(userid) && !string.IsNullOrEmpty(time))
                {
                    int intUserid = 0;

                    if (int.TryParse(userid, out intUserid))
                    {
                        var userinfo = viviapi.BLL.User.Factory.GetCacheUserBaseInfo(intUserid);

                        if (userinfo != null)
                        {
                            string thisSign =
                                string.Format(
                                    "userid={0}&suppid={1}&orderid={2}&orderAmt={3}&bankcode={4}&time={5}{6}"
                                    , userid
                                    , suppid
                                    , orderid
                                    , orderAmt
                                    , bankcode
                                    , time
                                    , userinfo.APIKey);

                            thisSign = viviLib.Security.Cryptography.MD5(thisSign);

                            if (thisSign == sign)
                            {
                                success = true;
                            }
                        }
                    }
                }

                #endregion

            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                Response.Write("error");
                Response.End();
            }
            success = true;
            if (success)
            {
                Getqrcode_img_url(suppid, orderid, decimal.Parse(orderAmt), bankcode);
            }
        }

        protected string company_name = string.Empty;
        public string Host
        {
            get
            {
                return HttpContext.Current.Request.Url.Scheme + "://" +
                                      HttpContext.Current.Request.Url.Authority;
            }
        }

        public void Getqrcode_img_url(int suppid, string orderid, decimal orderAmt, string bankcode)
        {
            var info = OrderBankCodePay.Instance.GetModel(orderid);
            if (info == null)
            {
                var SuppInfo = viviapi.BLL.Supplier.Factory.GetCacheModel(suppid);
                if (SuppInfo != null)
                {
                    company_name = SuppInfo.desc;

                    if (SuppInfo.multiacct)
                    {
                        var itemInfo = suppAcctBll.GetCacheModelByDomain((int)SupplierCode.Zweixin
                            , HttpContext.Current.Request.Url.Authority);

                        if (itemInfo != null)
                        {
                            company_name = itemInfo.jumpdomain;
                        }
                    }


                }

                if (suppid == (int)SupplierCode.Swiftpass)
                {
                    var api = new viviapi.ETAPI.Swiftpass.Gateway();
                    Hashtable param = api.submitOrderInfo(orderid, orderAmt);
                    if (param == null)
                    {
                        Response.Write("系统出错，请联系客服处理 ErrCode 2000");
                        Response.End();
                    }
                    else
                    {
                        qrcode_img_url = param["code_img_url"].ToString();
                        code_url = param["code_url"].ToString();
                    }
                }
                else if (suppid == (int)SupplierCode.Zweixin)
                {
                    var zwxPay = new viviapi.ETAPI.Zweixin.ZweixinPay();
                    qrcode_img_url = zwxPay.GetCodeUrl(orderid, orderAmt, string.Empty, HttpContext.Current);
                    if (!string.IsNullOrEmpty(qrcode_img_url))
                    {
                        string urldata = viviLib.Security.Cryptography.EncryptConnString(qrcode_img_url);


                        qrcode_img_url = Host + "/wxqrcode.ashx?url=" +
                                         HttpUtility.UrlEncode(urldata);
                    }
                }
                if (!string.IsNullOrEmpty(qrcode_img_url))
                {
                    info = new viviapi.Model.Order.Bank.OrderBankCodePay
                    {
                        addTime = DateTime.Now,
                        channel = 100,
                        codeImgUrl = qrcode_img_url,
                        sysOrderNo = orderid,
                        updateTime = DateTime.Now
                    };
                    int infoId = OrderBankCodePay.Instance.Add(info);
                    if (infoId <= 0)
                    {
                        Response.Write("系统出错，请联系客服处理 ErrCode 2001");
                        Response.End();
                    }
                }

            }
            else
            {
                qrcode_img_url = info.codeImgUrl;
            }
        }
    }
}