using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using viviapi.WebComponents;
using viviapi.WebComponents.Web;

namespace viviAPI.WebUI7uka.agentmodule.recharg
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GoToRecharge : AgentPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GoToPay();
        }

        void GoToPay()
        {
            String internalAPIID = viviapi.SysConfig.RuntimeSetting.InternalAPIID;
            String internalAPIKey = viviapi.SysConfig.RuntimeSetting.InternalAPIKey;


            string message = "";
            String orderid = "";
            string rechargeamt = GetValue("rechargeMoney");
            string bankType = GetValue("bank_list");
           
            decimal amt = 0M;
            if (string.IsNullOrEmpty(rechargeamt))
            {
                message = "请输入充值金额" + Request.Form["rechargeMoney"];
            }
            else if (!decimal.TryParse(rechargeamt, out amt))
            {
                message = "充值金额格式不正确。";
            }
            else
            {
                if (string.IsNullOrEmpty(internalAPIID) ||
                    string.IsNullOrEmpty(internalAPIKey)
                    )
                {
                    message = "系统故障，请系统管理员。";
                }
                else
                {
                    orderid = new Random().Next(100, 999).ToString(CultureInfo.InvariantCulture) + DateTime.Now.ToString("yyyyMMddHHmmss");
                    if (InitData(orderid, bankType, amt) > 0)
                    {

                    }
                    else
                    {
                        message = "系统错误。";
                    }
                }
            }

            if (string.IsNullOrEmpty(message))
            {
                String callBackurl = WebUtility.GetCurrentHost() + "/merchant/receiveResult/rechargeCallBack.aspx";
                String hrefbackurl = WebUtility.GetCurrentHost() + "/merchant/receiveResult/rechargeReturn.aspx";


                if (string.IsNullOrEmpty(bankType))
                {
                    bankType = "967";
                }

                String param = String.Format("parter={0}&type={1}&value={2}&orderid={3}&callbackurl={4}", internalAPIID, bankType, rechargeamt, orderid, callBackurl);
                String sign = viviLib.Security.Cryptography.MD5(param + internalAPIKey).ToLower();


                string postForm = "<form name=\"frm1\" id=\"frm1\" method=\"get\" action=\"" + WebUtility.GetGatewayUrl() + "/ChargeBank.aspx\">";

                postForm += "<input type=\"hidden\" name=\"parter\" value=\"" + internalAPIID + "\" />";
                postForm += "<input type=\"hidden\" name=\"type\" value=\"" + bankType + "\" />";
                postForm += "<input type=\"hidden\" name=\"value\" value=\"" + rechargeamt + "\" />";
                postForm += "<input type=\"hidden\" name=\"orderid\" value=\"" + orderid + "\" />";
                postForm += "<input type=\"hidden\" name=\"callbackurl\" value=\"" + callBackurl + "\" />";
                postForm += "<input type=\"hidden\" name=\"hrefbackurl\" value=\"" + hrefbackurl + "\" />";
                postForm += "<input type=\"hidden\" name=\"sign\" value=\"" + sign + "\" />";
                postForm += "<input type=\"hidden\" name=\"attach\" value=\"system_recharge\" />";
                postForm += "</form>";

                postForm += "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>";


                litForm.Text = postForm;
            }
            else
            {
                litForm.Text = message;
            }

        }

        int InitData(string payno, string bankList, decimal rechargeAmt)
        {
            int typeid = 102;
            if (bankList == "992")
                typeid = 101;
            else if (bankList == "993")
                typeid = 100;

            var info = new viviapi.Model.APP.Recharge()
            {
                id = 0,
                orderid = payno,
                paytype = typeid,
                suppid = 1,
                processstatus = 0,
                processtime = DateTime.Now,
                realPayAmt = 0M,
                rechargeAmt = rechargeAmt,
                rechtype = 1,
                remark = string.Empty,
                smsnotification = false,
                status = 1,
                account = this.CurrentUser.UserName,
                userid = this.UserId,
                field1 = bankList
            };

            var bll = new viviapi.BLL.APP.Recharge();
            return bll.Add(info);
        }
    }
}
