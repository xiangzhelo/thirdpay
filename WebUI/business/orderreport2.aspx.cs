using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using viviapi.Model;
using viviapi.Model.Order;
using viviapi.Model.User;
using viviLib;
using viviLib.Web;
using viviLib.Security;
using viviapi.BLL;
using DBAccess;

namespace viviapi.web.business
{
    /// <summary>
    /// 
    /// </summary>
    public partial class orderreport2 : viviapi.WebComponents.Web.BusinessPageBase
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
        protected string TotalCommission = "0.00";








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

            DataTable data = BLL.Order.Statistics.StatForBusiness(this.ManageId, sdt, edt);

            rep_report.DataSource = data;
            rep_report.DataBind();

            if (data != null)
            {
                try
                {
                    TotalRealvalue = Convert.ToDecimal(data.Compute("sum(realvalue)", "")).ToString("f2");                    
                    TotalPayAmtATM = Convert.ToDecimal(data.Compute("sum(payAmt)", "")).ToString("f2");
                    TotalCommission = Convert.ToDecimal(data.Compute("sum(commission)", "")).ToString("f2");
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