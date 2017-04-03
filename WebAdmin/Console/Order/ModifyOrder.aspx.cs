using System;
using System.Data;
using System.Web.UI.WebControls;
using viviapi.BLL.Order.Card;

using viviapi.ETAPI.Common;
using viviapi.Model.Order;
using viviapi.Model.supplier;
using viviapi.WebComponents.Web;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.order
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ModifyOrder : ManagePageBase
    {
        private OrderCardInfo _model = null;
        protected OrderCardInfo Model
        {
            get
            {
                if (!string.IsNullOrEmpty(OrderId) && _model == null)
                {
                    _model = Factory.Instance.GetModelByOrderId(OrderId);
                }
                return _model;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected string OrderId
        {
            get
            {
                return WebBase.GetQueryStringString("orderid","");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            viviapi.BLL.ManageFactory.CheckSecondPwd();

            if (!this.IsPostBack)
            {
                this.txtOrder.Text = OrderId;
                string where = string.Empty;

                DataTable list = viviapi.BLL.Supplier.Factory.GetList(where).Tables[0];
                //ddlSupp.Items.Add(new ListItem("--默认--", "-1"));
                foreach (DataRow dr in list.Rows)
                {
                    this.ddlSupp.Items.Add(new ListItem(dr["name"].ToString(), dr["code"].ToString()));
                }
                if (Model != null)
                {

                    this.txtOrderAmt.Text = Model.refervalue.ToString("f2");
                    ddlSupp.SelectedValue = Model.supplierId.ToString();
                }

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string message = SaveInfo();

            if (string.IsNullOrEmpty(message))
                message = "操作成功";

            ShowMessageBox(message);
        }

        string SaveInfo()
        {
            decimal amt = 0;

            string message = string.Empty;
            if (string.IsNullOrEmpty(OrderId))
                message = "订单号不能为空";
            else if (string.IsNullOrEmpty(ddlSupp.SelectedValue))
            {
                message = "请选择接口商";
            }
            else
            {
                
                if (!decimal.TryParse(this.txtOrderAmt.Text.Trim(), out amt))
                {
                    message = ("金额不能为0");
                }
            }
            if (string.IsNullOrEmpty(message))
            {
                if (Factory.Instance.ResetState(this.OrderId, int.Parse(ddlSupp.SelectedValue), amt) == false)
                {
                    message = "保存失败";
                }
            }
            return message;
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string message = SaveInfo();
            if (string.IsNullOrEmpty(message))
            {
                int supplierId = int.Parse(ddlSupp.SelectedValue);
                if (Model != null)
                {
                    string supporderid = string.Empty;
                    string errorinfo = string.Empty;
                    string errorerrcode = string.Empty;

                    decimal amt = decimal.Parse(this.txtOrderAmt.Text.Trim());

                    var supp = (SupplierCode)supplierId;

                    var callBack = OrderCardUtils.SynchSubmit(supp
                        , OrderId
                        , Model.typeId
                        , Model.cardNo
                        , Model.cardPwd
                        , Convert.ToInt32(decimal.Round(amt, 0))
                        , ""
                        , 1);

                    if (callBack.SummitStatus == 0)
                    {
                        message = "提交失败";
                    }
                    else
                    {
                        message = "提交成功";
                    }
                }
                else
                {
                    message = "操作失败";
                }
            }

            ShowMessageBox(message);
        }
    }
}
