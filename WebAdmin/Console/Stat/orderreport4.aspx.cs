using System;
using System.Data;
using System.Web.UI.WebControls;
using viviapi.Model;

namespace viviAPI.WebAdmin.Console.Stat
{
    /// <summary>
    /// 
    /// </summary>
    public partial class orderreport4 : viviapi.WebComponents.Web.ManagePageBase
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

               

                LoadData();
            }
        }

        #region LoadData
        /// <summary>
        /// 
        /// </summary>
        private void LoadData()
        {

            string orderby = "promAmt DESC";
            if (ViewState["Sort"] != null)
                orderby = ViewState["Sort"].ToString();

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

            DataSet ds = viviapi.BLL.Order.Statistics.AgentStat2(sdt, edt, this.Pager1.CurrentPageIndex - 1, this.Pager1.PageSize, orderby);

            Pager1.RecordCount = Convert.ToInt32(ds.Tables[0].Rows[0]["C"]);

            rep_report.DataSource = ds.Tables[1];
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
            if (e.Item.ItemType == ListItemType.Header)
            {
                if (ViewState["id"] != null)
                {
                    LinkButton lkbtnSort = (LinkButton)e.Item.FindControl("iBtn" + ViewState["id"].ToString().Trim());
                    lkbtnSort.Text = ViewState["text"].ToString();
                }
            }

            if (e.Item.ItemType == ListItemType.Footer)
            {
                
            }
        }
        #endregion

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void rep_report_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                LinkButton lkbtnSort = (LinkButton)e.Item.FindControl("iBtn" + e.CommandName.Trim());
                if (ViewState[e.CommandName.Trim()] == null)
                {
                    ViewState[e.CommandName.Trim()] = "DESC";
                    lkbtnSort.Text = lkbtnSort.Text + "▼";
                }
                else
                {
                    if (ViewState[e.CommandName.Trim()].ToString().Trim() == "DESC")
                    {
                        ViewState[e.CommandName.Trim()] = "ASC";
                        if (lkbtnSort.Text.IndexOf("▼") != -1)
                            lkbtnSort.Text = lkbtnSort.Text.Trim().Replace("▼", "▲");
                        else
                            lkbtnSort.Text = lkbtnSort.Text + "▲";
                    }
                    else
                    {
                        ViewState[e.CommandName.Trim()] = "DESC";
                        if (lkbtnSort.Text.IndexOf("▲") != -1)
                            lkbtnSort.Text = lkbtnSort.Text.Replace("▲", "▼");
                        else
                            lkbtnSort.Text = lkbtnSort.Text + "▼";

                    }
                }
                ViewState["text"] = lkbtnSort.Text;
                ViewState["id"] = e.CommandName.Trim();
                ViewState["Sort"] = e.CommandName.ToString().Trim() + " " + ViewState[e.CommandName.Trim()].ToString().Trim();

                LoadData();
            }
        }

    }
}