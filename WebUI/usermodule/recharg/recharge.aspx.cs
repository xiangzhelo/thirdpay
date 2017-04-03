using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.Model.Channel;
using viviapi.WebComponents.Web;
using viviapi.BLL.Channel;
namespace viviAPI.WebUI7uka.usermodule.recharg
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Recharge : UserPageBase
    {
        //通道是否显示
        
        public string hid = "class=\"hidclass\"";
        public string class992 = "", class993 = "", class1004 = "", class1007 = "", class51 = "", class1008="";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                LoadData();
                
            }
        }
        private void LoadData()
        {
            DataTable data = viviapi.BLL.Channel.ChannelType.GetList(true).Tables[0];

            //通道类别状态
            if (!data.Columns.Contains("type_status"))
                data.Columns.Add("type_status", typeof(string));

            //系统设置
            if (!data.Columns.Contains("sys_setting"))
                data.Columns.Add("sys_setting", typeof(string));

            //用户前台设置
            if (!data.Columns.Contains("user_setting"))
                data.Columns.Add("user_setting", typeof(string));

            if (!data.Columns.Contains("payrate"))
                data.Columns.Add("payrate", typeof(double));

            if (!data.Columns.Contains("suppid"))
                data.Columns.Add("suppid", typeof(int));



            foreach (DataRow dr in data.Rows)
            {
                int typeId = int.Parse(dr["typeId"].ToString());

                bool type_stutas = false;
                bool? sys_setting = null;
                bool? user_setting = null;

                ChannelTypeUserInfo setting = ChannelTypeUsers.GetModel(CurrentUser.ID, typeId);
                ChannelTypeInfo typeInfo = ChannelType.GetModelByTypeId(typeId);
                switch (typeInfo.isOpen)
                {
                    case OpenEnum.Close:
                    case OpenEnum.AllClose:
                        
                        type_stutas = false;
                        break;
                    case OpenEnum.Open:
                    case OpenEnum.AllOpen:
                        type_stutas = true;
                        break;
                }

                dr["type_status"] = type_stutas ? "right" : "wrong";
                dr["sys_setting"] = "Unknown";
                dr["user_setting"] = "Unknown";

                dr["suppid"] = 0;
                if (setting != null)
                {
                    if (setting.sysIsOpen.HasValue)
                    {
                        dr["sys_setting"] = setting.sysIsOpen.Value ? "right" : "wrong";
                        if (!setting.sysIsOpen.Value)//如果端口未开放
                        {
                            if (typeId == 101)//支付宝
                            {
                                class992 = hid;
                            }
                            else if (typeId == 100)//财付通
                            {
                                class993 = hid;
                            }
                            else if (typeId == 207)//微信支付
                            {
                                class1004 = hid;
                            }
                            else if (typeId == 300)//wap微信支付
                            {
                                class1007 = hid;
                            }
                            else if (typeId == 213)//qq支付
                            {
                                class51 = hid;
                            }
                            else if (typeId == 200)//Wap支付宝
                            {
                                class1008 = hid;
                            } lit1.Text += "sysIsOpen" + typeId+",";
                        }

                       
                    }
                    if (setting.userIsOpen.HasValue)
                    {
                        dr["user_setting"] = setting.userIsOpen.Value ? "right" : "wrong";
                        if (!setting.userIsOpen.Value)//如果端口未开放
                        {
                            if (typeId == 101)//支付宝
                            {
                                class992 = hid;
                            }
                            else if (typeId == 100)//财付通
                            {
                                class993 = hid;
                            }
                            else if (typeId == 207)//微信支付
                            {
                                class1004 = hid;
                            }
                            else if (typeId == 300)//wap微信支付
                            {
                                class1007 = hid;
                            }
                            else if (typeId == 213)//qq支付
                            {
                                class51 = hid;
                            }
                            else if (typeId == 200)//Wap支付宝
                            {
                                class1008 = hid;
                            }
                            lit2.Text += "userIsOpen" + typeId + ",";
                        }
                    }
                    lit3.Text += "userIsOpenuserIsOpen" + typeId + ",";

                    if (setting.suppid.HasValue)
                        dr["suppid"] = setting.suppid.Value;
                }
              //dr["payrate"] = 100 * viviapi.BLL.Finance.PayRate.Instance.GetUserPayRate(this.UserID, Convert.ToInt32(dr["typeId"]));

            }
            //rpt_paymode.DataSource = data;
            //rpt_paymode.DataBind();
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
