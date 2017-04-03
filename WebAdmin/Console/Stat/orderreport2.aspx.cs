using System;
using System.Data;
using System.Web.UI.WebControls;
using viviapi.BLL.Supplier;
using viviapi.Model;
using viviapi.WebComponents.Web;

namespace viviAPI.WebAdmin.Console.Stat
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Orderreport2 : ManagePageBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected string TotalRealvalue = "0.00";

        /// <summary>
        /// 
        /// </summary>
        protected string TotalSupplierAmt = "0.00";

        /// <summary>
        /// 
        /// </summary>
        protected string TotalPayAmtATM = "0.00";

        /// <summary>
        /// 平台利润
        /// </summary>
        protected string TotalProfit = "0.00";

       

      




        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            setPower();
            if (!this.IsPostBack)
            {
                this.StimeBox.Text = DateTime.Today.ToString("yyyy-MM-dd");
                this.EtimeBox.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
                this.StimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.EtimeBox.Attributes.Add("onFocus", "WdatePicker()");

                string where = string.Empty;
                DataTable list = Factory.GetList(where).Tables[0];
                ddlSupplier.Items.Add(new ListItem("--请选择--", ""));
                foreach (DataRow dr in list.Rows)
                {
                    this.ddlSupplier.Items.Add(new ListItem(dr["name"].ToString(), dr["code"].ToString()));                  
                }

                LoadData();
            }
        }

        #region LoadData
        /// <summary>
        /// 
        /// </summary>
        private void LoadData()
        {
            int suppid = 0;
            if (!string.IsNullOrEmpty(ddlSupplier.SelectedValue))
            {
                suppid = int.Parse(ddlSupplier.SelectedValue);
            }

             DateTime sdt = DateTime.MinValue;
            if (!string.IsNullOrEmpty(StimeBox.Text.Trim()))
            {
                if (DateTime.TryParse(StimeBox.Text.Trim(), out sdt))
                {
                }
            }

             DateTime edt = DateTime.MinValue;
            if (!string.IsNullOrEmpty(EtimeBox.Text.Trim()))
            {
                if (DateTime.TryParse(EtimeBox.Text.Trim(), out edt))
                {
                    
                }
            }

            DataTable data = viviapi.BLL.Order.Statistics.Stat(suppid, sdt, edt);

            rep_report.DataSource = data;
            rep_report.DataBind();

            if (data != null)
            {
                try {
                    TotalRealvalue = Convert.ToDecimal(data.Compute("sum(realvalue)", "")).ToString("f2");
                    TotalSupplierAmt = Convert.ToDecimal(data.Compute("sum(supplierAmt)", "")).ToString("f2");
                    TotalPayAmtATM = Convert.ToDecimal(data.Compute("sum(payAmt)", "")).ToString("f2");
                    TotalProfit = Convert.ToDecimal(data.Compute("sum(profits)", "")).ToString("f2");
                }
                catch { }
            }
        }
        #endregion

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

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            LoadData();
        }



        #region rptOrders_ItemDataBound
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptOrders_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                
            }
        }
        #endregion

    }
}