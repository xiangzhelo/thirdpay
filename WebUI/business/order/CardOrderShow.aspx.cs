using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.Model;
using viviapi.Model.User;
using viviapi.WebComponents;
using viviLib;
using viviLib.Web;
using viviLib.Security;
using viviapi.BLL;
using DBAccess;

namespace viviapi.web.business.Order
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CardOrderShow : viviapi.WebComponents.Web.BusinessPageBase
    {
        public long Id
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringInt64("id",0L);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            setPower();
            if (!Page.IsPostBack)
            {
                ShowInfo(Id);
            }
        }

        #region setPower
        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = BLL.ManageFactory.CheckCurrentPermission(false
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
            try { return BLL.User.Factory.GetCacheModel(uid).UserName; }
            catch { return string.Empty; }
        }

        private string getChannelTypeName(int id)
        {
            try { return BLL.Channel.ChannelType.GetModelByTypeId(id).modetypename; }
            catch { return string.Empty; }
        }

        private string getChannelName(string code)
        {
            try { return BLL.Channel.Channel.GetModelByCode(code).modeName; }
            catch { return string.Empty; }
        }



        private void ShowInfo(long id)
        {
            if (this.Id <= 0L)
                return;

            BLL.OrderCard bll = new OrderCard();
            Model.Order.OrderCardInfo model = bll.GetModel(Id);

            if (model == null)
                return;

            this.lblid.Text = Id.ToString();
            this.lblorderid.Text = model.orderid;
            this.lblordertype.Text = model.ordertype.ToString();
            lblsmsg.Text = model.msg;

            this.lbluserid.Text = model.userid.ToString() + " (" + getuserName(model.userid)+ ")";
            this.lbltypeId.Text = getChannelTypeName(model.typeId);// model.typeId.ToString();
            this.lblpaymodeId.Text = getChannelName(model.paymodeId);
            this.lbluserorder.Text = model.userorder;
            this.lblrefervalue.Text = model.refervalue.ToString("f2");
            if(model.realvalue.HasValue)
                this.lblrealvalue.Text = model.realvalue.Value.ToString("f2");
            this.lblnotifyurl.Text = model.notifyurl;
            if(!string.IsNullOrEmpty(model.againNotifyUrl))
                litNotify.Text = string.Format("<a target=\"_blank\" href=\"{0}\">{0}</a>", model.againNotifyUrl);
            this.lblnotifycount.Text = model.notifycount.ToString();
            this.lblnotifystat.Text = Enum.GetName(typeof(viviapi.Model.Order.OrderNofityStatusEnum), model.notifystat); //model.notifystat.ToString();
            this.lblnotifycontext.Text = model.notifycontext;
            this.lblreturnurl.Text = model.returnurl;
            this.lblattach.Text = model.attach;
            this.lblpayerip.Text = model.payerip;
            this.lblclientip.Text = model.clientip;
            this.lblreferUrl.Text = model.referUrl;
            this.lbladdtime.Text = viviLib.TimeControl.FormatConvertor.DateTimeToTimeString(model.addtime);
            this.lblsupplierId.Text = WebUtility.GetSupplierName(model.supplierId);
            this.lblsupplierOrder.Text = model.supplierOrder;

            this.lblstatus.Text = Enum.GetName(typeof(viviapi.Model.Order.OrderStatusEnum), model.status);

            if(model.completetime.HasValue)
                this.lblcompletetime.Text = viviLib.TimeControl.FormatConvertor.DateTimeToTimeString(model.completetime.Value);

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

            lblversion.Text = model.version;// SystemApiHelper.GetVersionName(model.version);
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