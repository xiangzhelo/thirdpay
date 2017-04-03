using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.WebComponents.Web;

namespace viviAPI.WebUI7uka.agentmodule.recharg
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Recharge : AgentPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

            }
        }


        //protected void btnsubmit_Click(object sender, EventArgs e)
        //{
        //    string rechargeamt = txtRechargeMoney.Text.Trim();

        //    decimal amt = 0M;
        //    if (string.IsNullOrEmpty(rechargeamt))
        //    {
        //        this.callinfo.InnerText = "请输入充值金额";
        //        return;
        //    }
        //    else if (!decimal.TryParse(rechargeamt.Replace(",", ""), out amt))
        //    {
        //        this.callinfo.InnerText = "充值金额格式不正确。";
        //        return;
        //    }
        //    var systemUser = viviapi.BLL.User.Factory.GetModel(800);
        //    if (systemUser == null)
        //    {
        //        this.callinfo.InnerText = "系统故障，请系统管理员。";
        //        return;
        //    }
        //    String shop_id = "800";
        //    String orderid = new Random().Next(100, 999).ToString(CultureInfo.InvariantCulture) + DateTime.Now.ToString("yyyyMMddHHmmss");
        //    String bank_Type = Request.Form["bank_list"];//银行类型
        //    if (InitData(orderid, bank_Type, amt) > 0)
        //    {
        //        String callBackurl = "http://" + Request.Url.Host + ":" + Request.Url.Port + "/payresult/" + "recharge_notify.aspx";
        //        String hrefbackurl = "http://" + Request.Url.Host + ":" + Request.Url.Port + "/usermodule/recharg/" + "recharge_return.aspx";


        //        if (string.IsNullOrEmpty(bank_Type))
        //        {
        //            bank_Type = "967";
        //        }
        //        String bank_payMoney = amt.ToString();//充值金额

        //        String param = String.Format("parter={0}&type={1}&value={2}&orderid={3}&callbackurl={4}", shop_id, bank_Type, bank_payMoney, orderid, callBackurl);

        //        String PostUrl = String.Format("{0}chargebank.aspx?{1}&hrefbackurl={2}&sign={3}&attach=system_recharge"
        //            , webInfo.PayUrl
        //            , param
        //            , hrefbackurl
        //            , viviLib.Security.Cryptography.MD5(param + systemUser.APIKey).ToLower()
        //            );
        //        Response.Redirect(PostUrl);
        //    }
        //    else
        //    {
        //        this.callinfo.InnerText = "系统故障，请系统管理员。";
        //        return;
        //    }
        //}

        //int InitData(string payno, string bank_list, decimal rechargeAmt)
        //{
        //    int typeid = 102;
        //    if (bank_list == "992")
        //        typeid = 101;
        //    else if (bank_list == "993")
        //        typeid = 100;

        //    var info = new viviapi.Model.APP.Recharge();

        //    info.id = 0;
        //    info.orderid = payno;
        //    info.paytype = typeid;
        //    info.suppid = 1;
        //    info.processstatus = 1;
        //    info.processtime = DateTime.Now;
        //    info.realPayAmt = 0M;
        //    info.rechargeAmt = rechargeAmt;
        //    info.rechtype = 1;
        //    info.remark = string.Empty;
        //    info.smsnotification = false;
        //    info.status = 1;
        //    info.account = this.CurrentUser.UserName;
        //    info.userid = this.UserId;
        //    info.field1 = bank_list;
        //    info.processstatus = 0;

        //    var BLL = new viviapi.BLL.APP.Recharge();
        //    return BLL.Add(info);
        //}
    }
}
