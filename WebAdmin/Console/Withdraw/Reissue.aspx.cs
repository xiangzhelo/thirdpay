using System;
using System.Data;
using System.Web.UI.WebControls;
using viviapi.BLL.Supplier;
using viviapi.WebComponents.Web;

namespace viviAPI.WebAdmin.Console.Withdraw
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Reissue : ManagePageBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected int id
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringInt32("id", 0);
            }
        }

        private viviapi.Model.Finance.WithdrawSuppTranLog _model = null;
        public viviapi.Model.Finance.WithdrawSuppTranLog model
        {
            get
            {
                if (_model == null)
                {
                    if (id > 0)
                    {
                        _model = viviapi.BLL.Finance.WithdrawSuppTranLog.Instance.GetModel(id);
                    }
                }
                return _model;
            }
        }
       

        protected void Page_Load(object sender, EventArgs e)
        {
            viviapi.BLL.ManageFactory.CheckSecondPwd();

            if (!this.IsPostBack)
            {
                #region
                DataTable list = Factory.GetList("isdistribution=1").Tables[0];
                ddlSupplier.Items.Add(new ListItem("--付款接口--", ""));
                ddlSupplier.Items.Add(new ListItem("不走接口", "0"));
                foreach (DataRow dr in list.Rows)
                {
                    this.ddlSupplier.Items.Add(new ListItem(dr["name"].ToString(), dr["code"].ToString()));
                }
                #endregion

                if (model != null)
                {
                    txttrade_no.Attributes["readonly"] = "true";
                    txttrade_no.Text = model.trade_no;
                    ddlstatus.SelectedValue = model.status.ToString();
                    txtamount.Text = model.amount.ToString();

                    ddlSupplier.SelectedValue = model.suppid.ToString();
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
            string message = "";
            if (model != null)
            {
                string bill_no = string.Empty;

                bool is_cancel = false;
                int status = int.Parse(this.ddlstatus.SelectedValue);
                if (status == 1 ||
                   status == 2 ||
                   status == 4)
                {
                    is_cancel = true;
                }

                int code = viviapi.ETAPI.Common.Withdrawal.Complete(int.Parse(ddlSupplier.SelectedValue)
                           , this.txttrade_no.Text.Trim()
                           , is_cancel
                           , int.Parse(this.ddlstatus.SelectedValue)
                           , this.txtamount.Text.Trim()
                           , this.txttrade_no.Text.Trim()
                           , this.txterror_message.Text.Trim());

                if (code == 0)
                {
                    message = "处理成功";
                }
                else if (code == 1)
                {
                    message = "无效单";
                }
                else if (code == 2)
                {
                    message = "无效接口商";
                }
                else if (code == 3)
                {
                    message = "状态无效";
                }
                else if (code == 99)
                {
                    message = "系统出错";
                }
            }

            AlertAndRedirect(message);
        }
    }
}
