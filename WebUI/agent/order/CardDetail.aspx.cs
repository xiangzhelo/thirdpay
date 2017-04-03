using System;
using System.Globalization;
using viviapi.BLL;
using viviapi.BLL.Order.Card;
using viviapi.Model;
using viviapi.WebComponents;
using viviapi.WebComponents.Web;
using viviLib.Web;

namespace viviAPI.WebUI7uka.agent.order
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CardOrderShow :AgentPageBase
    {
        public string OrderId
        {
            get
            {
                return WebBase.GetQueryStringString("orderid", "");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ShowInfo();
            }
        }

        #region setPower
        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = viviapi.BLL.ManageFactory.CheckCurrentPermission(false
            , ManageRole.Orders);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
        }
        #endregion



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



        private void ShowInfo()
        {
            if (string.IsNullOrEmpty(OrderId))
                return;

            var model = Factory.Instance.GetModelByOrderId(OrderId);

            if (model == null)
                return;

            this.lblid.Text = model.id.ToString(CultureInfo.InvariantCulture);
            this.lblorderid.Text = model.orderid;
            this.lblordertype.Text = model.ordertype.ToString(CultureInfo.InvariantCulture);
            lblsmsg.Text = model.msg;

            this.lbluserid.Text = model.userid.ToString(CultureInfo.InvariantCulture) + " (" + getuserName(model.userid) + ")";
            this.lbltypeId.Text = getChannelTypeName(model.typeId);// model.typeId.ToString();
            this.lblpaymodeId.Text = getChannelName(model.paymodeId);
            this.lbluserorder.Text = model.userorder;
            this.lblrefervalue.Text = model.refervalue.ToString("f2");
            if (model.realvalue.HasValue)
                this.lblrealvalue.Text = model.realvalue.Value.ToString("f2");
            //lblcardfaceval.Text = model.faceValue.ToString("f2");

            this.lblnotifyurl.Text = model.notifyurl;
            if (!string.IsNullOrEmpty(model.againNotifyUrl))
                litNotify.Text = string.Format("<a target=\"_blank\" href=\"{0}\">{0}</a>", model.againNotifyUrl);
            this.lblnotifycount.Text = model.notifycount.ToString(CultureInfo.InvariantCulture);
            this.lblnotifystat.Text = Enum.GetName(typeof(viviapi.Model.Order.OrderNofityStatusEnum), model.notifystat); //model.notifystat.ToString();
            this.lblnotifycontext.Text = model.notifycontext;
            this.lblreturnurl.Text = model.returnurl;
            this.lblattach.Text = model.attach;
            this.lblpayerip.Text = model.payerip;
            this.lblclientip.Text = model.clientip;
            this.lblreferUrl.Text = model.referUrl;

            this.lblsupplierId.Text = WebUtility.GetSupplierName(model.supplierId);
            this.lblsupplierOrder.Text = model.supplierOrder;

            this.lblstatus.Text = Enum.GetName(typeof(viviapi.Model.Order.OrderStatusEnum), model.status);

            this.lbladdtime.Text = model.addtime.ToString("yyyy-MM-dd HH:mm:ss:fff");

            if (model.processingtime.HasValue)
            {
                string showTime = model.processingtime.Value.ToString("yyyy-MM-dd HH:mm:ss:fff");

                TimeSpan ts = model.processingtime.Value - model.addtime;

                showTime += " (" + ts.TotalMilliseconds.ToString() + "毫秒)";

                this.lblcompletetime.Text = showTime;
            }


            this.lblpayRate.Text = model.payRate.ToString("p2");
            this.lblsupplierRate.Text = model.supplierRate.ToString("p2");
            this.lblpromRate.Text = model.promRate.ToString("p2");
            this.lblpayAmt.Text = model.payAmt.ToString("f2");
            this.lblpromAmt.Text = model.promAmt.ToString("f2");
            this.lblsupplierAmt.Text = model.supplierAmt.ToString("f2");
            this.lblprofits.Text = model.profits.ToString("f2");
            this.lblserver.Text = model.server.ToString();
            lblcardno.Text = model.cardNo;
            this.lblcardpwd.Text = model.cardPwd;

            //lblwithhold.Text = GetWithholdType(model.withhold_type);
            //lblmakeup.Text = Getmakeup(model.makeup);

            string vname = viviapi.SysInterface.Card.Utility.GetVersionName(model.version);

            lblversion.Text = vname;
            if (model.ismulticard == 1)
            {
                lblversion.Text += "多卡";
            }
            else
            {
                lblversion.Text += "单卡";
            }

            lblcardNum.Text = model.cardnum.ToString();
            litreback.Text = string.Format("opstate:{0} ovalue:{1} ", model.opstate, model.ovalue);
        }


        
    }
}