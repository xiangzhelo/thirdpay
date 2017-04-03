using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using viviapi.BLL.Supplier;
using viviapi.ETAPI.Common;
using viviapi.Model.Order.Card;
using viviapi.WebComponents.Web;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.Order
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CardResetOrder : ManagePageBase
    {
        public string OrderId
        {
            get
            {
                return WebBase.GetQueryStringString("orderid", "");
            }
        }

        private viviapi.Model.Order.OrderCardInfo _ordercard = null;
        public viviapi.Model.Order.OrderCardInfo OrderCard
        {
            get
            {
                if (_ordercard == null && !string.IsNullOrEmpty(OrderId))
                {
                    _ordercard = viviapi.BLL.Order.Card.Factory.Instance.GetModelByOrderId(OrderId);
                }
                return _ordercard;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            viviapi.BLL.ManageFactory.CheckSecondPwd();

            if (!this.IsPostBack)
            {
                string where = string.Empty;
                DataTable list = Factory.GetList(where).Tables[0];
                foreach (DataRow dr in list.Rows)
                {
                    this.ddlSupp.Items.Add(new ListItem(dr["name"].ToString(), dr["code"].ToString()));
                }

                if (this.OrderCard != null)
                {
                    this.txtOrder.Text = OrderId;
                    this.txtOrder.Attributes["readonly"] = OrderId;

                    ddlSupp.SelectedValue = OrderCard.supplierId.ToString(CultureInfo.InvariantCulture);
                    this.txtOrderAmt.Text = decimal.Round(OrderCard.refervalue).ToString("f2");

                    btnAdd.Enabled = (OrderCard.status == 1);
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                if (OrderCard.status == 1)
                {
                    bool result = false;
                    int SuppId = Convert.ToInt32(this.ddlSupp.SelectedValue);
                    decimal TranAmt = decimal.Parse(this.txtOrderAmt.Text.Trim());
                    if (TranAmt <= 0M)
                    {
                        ShowMessageBox("金额不能为0");
                        return;
                    }
                    var response = new CardOrderSupplierResponse()
                    {
                        SupplierId = SuppId,
                        SuppTransNo = "Sys" + DateTime.Now.Ticks.ToString(),
                        SysOrderNo = OrderCard.orderid,
                        OrderAmt = TranAmt,
                        SuppAmt = 0M,
                        OrderStatus = int.Parse(rblstatus.SelectedValue),
                        SuppErrorCode = txtCode.Text,
                        Opstate = "",
                        SuppErrorMsg = txtMsg.Text,
                        ViewMsg = "",
                        Method = 1
                    };


                    OrderCardUtils.SuppNotify(response, "");

                    ShowMessageBox("操作成功");
                }
                else
                {
                    ShowMessageBox("订单状态不正确");
                }
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                int supp = int.Parse(this.ddlSupp.SelectedValue);

                viviapi.ETAPI.Common.OrderCardUtils.QueryOrder(supp, decimal.ToInt32(OrderCard.refervalue), OrderCard.orderid);

                ShowMessageBox("操作成功,请查看订单状态");
            }
            catch (Exception exception)
            {

                ShowMessageBox(exception.Message);
            }
           
        }
    }
}
