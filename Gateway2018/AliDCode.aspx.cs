using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL.Order.Bank;
using viviapi.ETAPI.Alipay;
using viviLib.ExceptionHandling;
using viviLib.Web;

namespace viviAPI.Gateway2018
{
    public partial class AliDCode : System.Web.UI.Page
    {
        protected string qrcode_img_url = "";
        protected string orderAmt = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            bool success = false;

            string orderid = "";

            string bankcode = "";
            try
            {
                string userid = Request.QueryString["userid"];
                string suppid = Request.QueryString["suppid"];
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

            if (success)
            {
                Getqrcode_img_url(orderid, decimal.Parse(orderAmt), bankcode);
            }
        }


        public void Getqrcode_img_url(string orderid, decimal orderAmt, string bankcode)
        {
            var info = OrderBankCodePay.Instance.GetModel(orderid);
            if (info == null)
            {
                var api = new Qrcode();
                qrcode_img_url = api.Getqrcode_img_url(orderid, orderAmt, bankcode);
                if (string.IsNullOrEmpty(qrcode_img_url))
                {
                    Response.Write("error 1");
                    Response.End();
                }
                info = new viviapi.Model.Order.Bank.OrderBankCodePay
                {
                    addTime = DateTime.Now,
                    channel = 101,
                    codeImgUrl = qrcode_img_url,
                    sysOrderNo = orderid,
                    updateTime = DateTime.Now
                };
                int infoId = OrderBankCodePay.Instance.Add(info);
                if (infoId <= 0)
                {
                    Response.Write("error 2");
                    Response.End();
                }
            }
            else
            {
                qrcode_img_url = info.codeImgUrl;
            }

            if (!string.IsNullOrEmpty(qrcode_img_url))
            {

            }
            //litimg.Text = string.Format("<img src='{0}' />", qrcode_img_url);
        }

        //protected void BtnAlipay_Click(object sender, EventArgs e)
        //{
        //    string message = "";

        //    var info = Factory.Instance.GetModelByOrderId(hforderid.Value);
        //    if (info == null)
        //    {
        //        message = "不存在此订单";
        //    }
        //    else
        //    {
        //        if (info.status == 1)
        //        {
        //            message = "未完成支付";
        //        }
        //        else
        //        {
        //            message = "true";
        //        }
        //    }

        //    if (message == "true")
        //    {
        //        //if (!string.IsNullOrEmpty(info.returnurl))
        //        //{
        //        //    bllBank.BankOrderReturn(info);
        //        //}
        //        //else
        //        //{
        //        //    var parms = new StringBuilder();
        //        //    parms.AppendFormat("o={0}", info.orderid);
        //        //    parms.AppendFormat("&uo={0}", info.userorder);
        //        //    parms.AppendFormat("&c={0}", info.paymodeId);
        //        //    parms.AppendFormat("&t={0}", info.typeId);
        //        //    parms.AppendFormat("&v={0:f2}", info.realvalue);
        //        //    parms.AppendFormat("&e={0}", info.msg);
        //        //    parms.AppendFormat("&u={0}", info.userid);
        //        //    parms.AppendFormat("&s={0}", info.status);

        //        //    Response.Redirect("/PayResult.aspx?" + parms.ToString(), false);
        //        //}
        //    }
        //    else
        //    {
        //        //ShowMessageBox(message);
        //    }
        //}
    }
}
