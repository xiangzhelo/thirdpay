using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using viviapi.BLL.Supplier;
using viviapi.Model.Common;
using viviapi.Model.Order;
using viviapi.SysInterface.Bank;
using viviapi.WebComponents.Web;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.Order
{
    /// <summary>
    /// 
    /// </summary>
    public partial class BankResetOrder : ManagePageBase
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
            viviapi.BLL.ManageFactory.CheckSecondPwd();

            if (!this.IsPostBack)
            {
                txtOrderId.Text = OrderId;

                string where = string.Empty;
                DataTable list = Factory.GetList(where).Tables[0];
                foreach (DataRow dr in list.Rows)
                {
                    this.ddlSupp.Items.Add(new ListItem(dr["name"].ToString(), dr["code"].ToString()));
                }
                if (!string.IsNullOrEmpty(OrderId))
                {
                    OrderBankInfo orderBank = viviapi.BLL.Order.Bank.Factory.Instance.GetModelByOrderId(OrderId);
                    if (orderBank != null)
                    {
                        ddlSupp.SelectedValue = orderBank.supplierId.ToString(CultureInfo.InvariantCulture);
                        this.txtOrderAmt.Text = decimal.Round(orderBank.refervalue).ToString("f2");

                        btnAdd.Enabled = (orderBank.status == 1);
                    }
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
                string orderId = this.txtOrderId.Text.Trim();
                if (string.IsNullOrEmpty(orderId))
                {
                    ShowMessageBox("请输入订单号");
                    return;
                }

                if (string.IsNullOrEmpty(ddlSupp.SelectedValue))
                {
                    ShowMessageBox("请选择接口商");
                    return;
                }

                string orderAmt = this.txtOrderAmt.Text.Trim();
                decimal amt = 0M;
                if (string.IsNullOrEmpty(orderAmt))
                {
                    ShowMessageBox("请输入订单金额");
                    return;
                }

                if (!decimal.TryParse(orderAmt, out amt))
                {
                    ShowMessageBox("订单金额不能为空");
                    return;
                }
                if (amt <= 0M)
                {
                    ShowMessageBox("金额不能为0");
                    return;
                }

                OrderBankInfo orderBank = viviapi.BLL.Order.Bank.Factory.Instance.GetModelByOrderId(orderId);
                if (orderBank == null)
                {
                    ShowMessageBox("不存在此订单");
                    return;
                }

                if (orderBank.status != 1)
                {
                    ShowMessageBox("订单状态不正确");
                    return;
                }

                string supp = this.ddlSupp.SelectedValue;

                if (orderBank.status == 1)
                {
                    FunExecResult result = viviapi.ETAPI.Common.OrderBankUtils.InsertToDb(int.Parse(supp)
                           , orderBank
                           , "Sys" + DateTime.Now.Ticks.ToString()
                           , 2
                           , "0"
                           , "手动补单"
                           , amt, 0M);

                    if (result.ErrCode == 0)
                    {
                        if (orderBank.status != 1)
                        {
                            APINotification.SynchronousNotifyX(orderBank);
                        }

                        ShowMessageBox("操作成功");
                    }
                    else
                    {
                        ShowMessageBox("操作失败");
                    }

                }
                else
                {
                    ShowMessageBox("订单状态不正确");
                }
            }
        }
    }
}
