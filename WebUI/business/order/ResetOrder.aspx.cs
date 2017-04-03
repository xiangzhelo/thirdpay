using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL.Supplier;

namespace viviapi.web.business.Order
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Console_Order_ResetOrder : viviapi.WebComponents.Web.BusinessPageBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected string OrderId
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringString("orderid","");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected int oclass
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringInt32("oclass", 0);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected int supp
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringInt32("supp", 0);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected decimal amt
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringDecimal("amt", 0M);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            BLL.ManageFactory.CheckSecondPwd();

            if (!this.IsPostBack)
            {
                this.txtOrder.Text = OrderId;
                this.rblOrdClass.SelectedValue = oclass.ToString();
                string where = string.Empty;
                DataTable list = Factory.GetList(where).Tables[0];
                //ddlSupp.Items.Add(new ListItem("--默认--", "-1"));
                foreach (DataRow dr in list.Rows)
                {
                    this.ddlSupp.Items.Add(new ListItem(dr["name"].ToString(), dr["code"].ToString()));
                }
                ddlSupp.SelectedValue = supp.ToString();
                this.txtOrderAmt.Text = decimal.Round(amt).ToString("f2");

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
                bool result = false;
                string supp = this.ddlSupp.SelectedValue;
                decimal amt = decimal.Parse(this.txtOrderAmt.Text.Trim());
                if (amt <= 0M)
                {
                    AlertAndRedirect("金额不能为0");
                    return;
                }
                if (!string.IsNullOrEmpty(supp))
                {
                   // viviapi.ETAPI.ETAPIBase cmd = new viviapi.ETAPI.ETAPIBase(int.Parse(supp));
                    
                    string orderclass = rblOrdClass.SelectedValue;
                    if (orderclass == "1")
                    {
                        BLL.OrderBank bll = new viviapi.BLL.OrderBank();
                        //result = bll.DoBankComplete(int.Parse(supp), this.txtOrder.Text.Trim(), "ResetOrder", 2, "0", "", amt, 0M, true, false, false);
                    }
                    else if (orderclass == "2")
                    {
                        BLL.OrderCard bll = new viviapi.BLL.OrderCard();
                        //result = bll.DoCardComplete(int.Parse(supp), this.txtOrder.Text.Trim(), "ResetOrder", 2, "0", "", amt, 0M, false);
                        //result = bll.RepairOrder(int.Parse(supp), this.txtOrder.Text.Trim(), "ResetOrder", 2, "0", "", "", amt, 0M, string.Empty, 1);
                    }
                    else if (orderclass == "3")
                    { }
                }
                if (result)
                {
                    AlertAndRedirect("操作成功");
                    return;
                }
                else{                
                    AlertAndRedirect("操作失败");
                    return;
                }
            }
        }
    }
}
