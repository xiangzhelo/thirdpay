using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace viviAPI.WebUI7uka.agentmodule.order
{
    public partial class orderview : viviapi.WebComponents.Web.AgentPageBase
    {
        public string orderid
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringString("orderid", "");
            }
        }
        protected string referUrl = string.Empty;
        protected string notifyurl = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ShowInfo(orderid);
            }
        }

        private void ShowInfo(string orderid)
        {
            if (string.IsNullOrEmpty(orderid))
                return;
            var model = viviapi.BLL.Order.Bank.Factory.Instance.GetModelByOrderId(orderid);

            if (model == null)
                return;



            this.lblorderid.Text = model.orderid;
            this.lblordertype.Text = model.ordertype.ToString();


            this.lbltypeId.Text = getChannelTypeName(model.typeId);// model.typeId.ToString();
            this.lblpaymodeId.Text = getChannelName(model.paymodeId);
            this.lbluserorder.Text = model.userorder;
            this.lblrefervalue.Text = model.refervalue.ToString("f2");
            if (model.realvalue.HasValue)
                this.lblrealvalue.Text = model.realvalue.Value.ToString("f2");
            notifyurl = model.notifyurl;
            if (!string.IsNullOrEmpty(model.againNotifyUrl))
                notifyurl = model.againNotifyUrl;

            //litNotify.Text = string.Format("<a target=\"_blank\" href=\"{0}\">{0}</a>", model.againNotifyUrl);
            this.lblnotifycount.Text = model.notifycount.ToString();
            this.lblnotifystat.Text = Enum.GetName(typeof(viviapi.Model.Order.OrderNofityStatusEnum), model.notifystat); //model.notifystat.ToString();
            this.lblnotifycontext.Text = Server.HtmlEncode(model.notifycontext);
            //this.lblreturnurl.Text = model.returnurl;
            this.lblattach.Text = model.attach;
            this.lblpayerip.Text = model.payerip;
            //this.lblclientip.Text = model.clientip;
            referUrl = model.referUrl;
            this.lbladdtime.Text = viviLib.TimeControl.FormatConvertor.DateTimeToTimeString(model.addtime);
            //this.lblsupplierId.Text = WebUtility.GetsupplierName(model.supplierId);
            //this.lblsupplierOrder.Text = model.supplierOrder;

            this.lblstatus.Text = Enum.GetName(typeof(viviapi.Model.Order.OrderStatusEnum), model.status);

            if (model.completetime.HasValue)
                this.lblcompletetime.Text = viviLib.TimeControl.FormatConvertor.DateTimeToTimeString(model.completetime.Value);

            this.lblpayRate.Text = model.payRate.ToString("p2");
            // this.lblsupplierRate.Text = model.supplierRate.ToString("p2");
            //this.lblpromRate.Text = model.promRate.ToString("p2");
            this.lblpayAmt.Text = model.payAmt.ToString("f2");
            //this.lblpromAmt.Text = model.promAmt.ToString("f2");
            // this.lblsupplierAmt.Text = model.supplierAmt.ToString("f2");
            //this.lblprofits.Text = model.profits.ToString("f2");
            //this.lblserver.Text = model.server.ToString();
            string _vname = viviapi.SysInterface.Bank.Utility.GetVersionName(model.version);
            if (string.IsNullOrEmpty(_vname))
            {
                if (viviapi.BLL.WebInfoFactory.CurrentWebInfo != null)
                {
                    _vname = viviapi.BLL.WebInfoFactory.CurrentWebInfo.apibankname + "[" + viviapi.BLL.WebInfoFactory.CurrentWebInfo.apibankversion + "]";
                }
            }
            lblversion.Text = _vname;
        }

        private string getuserName(int uid)
        {
            try { return viviapi.BLL.User.Factory.GetCacheModel(uid).UserName; }
            catch { return string.Empty; }
        }

        private string getChannelTypeName(int id)
        {
            try { return viviapi.BLL.Channel.ChannelType.GetModelByTypeId(id).modetypename; }
            catch { return string.Empty; }
        }

        private string getChannelName(string code)
        {
            try { return viviapi.BLL.Channel.Channel.GetModelByCode(code).modeName; }
            catch { return string.Empty; }
        }

    }
}
