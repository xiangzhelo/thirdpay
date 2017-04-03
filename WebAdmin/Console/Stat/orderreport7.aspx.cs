using System;
using System.Data;
using System.Web.UI.WebControls;
using viviapi.Model;

namespace viviAPI.WebAdmin.Console.Stat
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Orderreport7 : viviapi.WebComponents.Web.ManagePageBase
    {

        /// <summary>
        /// 
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
                this.EtimeBox.Text = DateTime.Today.ToString("yyyy-MM-dd");
                this.StimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.EtimeBox.Attributes.Add("onFocus", "WdatePicker()");

              

                LoadData();
            }
        }

        #region LoadData
        /// <summary>
        /// 
        /// </summary>
        private void LoadData()
        {
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

            DataSet data = viviapi.BLL.Order.Statistics.BusinessStat7(sdt, edt.AddDays(1));
            if (data != null)
            {
                try
                {
                    TotalProfit = Convert.ToDecimal(data.Tables[0].Compute("sum(amt)", "")).ToString("f2");
                }
                catch { }
            }

            rep_report.DataSource = data;
            rep_report.DataBind();
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