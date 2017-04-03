using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aspose.Cells;
using viviapi.Model;
using viviapi.WebComponents.Web;
using viviLib.Data;

namespace viviAPI.WebAdmin.Console.Withdraw
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SuppTransLogs : ManagePageBase
    {
        protected string total_amount = "0.00元";
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
                this.StimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.EtimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.StimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.EtimeBox.Attributes.Add("onFocus", "WdatePicker()");

                this.LoadData();
            }
        }

        #region setPower
        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = viviapi.BLL.ManageFactory.CheckCurrentPermission(false
, ManageRole.Financial);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
        }
        #endregion

        #region LoadData
        /// <summary>
        /// 
        /// </summary>
        private void LoadData()
        {
            DataSet pageData = GetData(false);


            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            DataTable data = pageData.Tables[1];

            this.rptdata.DataSource = data;
            this.rptdata.DataBind();

        }
        #endregion       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

       

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        protected void rptdata_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "query")
            {
               
            }
            else if (e.CommandName == "Reissue")
            {
                Response.Redirect(string.Format("Reissue.aspx?id={0}", e.CommandArgument));
            }
            
        }

        protected void rptdata_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {                
                int status = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "status"));
                Button btnquery = (Button)e.Item.FindControl("btnquery");
                Button btnReissue = (Button)e.Item.FindControl("btnReissue");
               // btnquery.Visible = (status == 1 || status == 8);

                btnReissue.Visible = (status == 255);
            }
        }

        #region GetData
        /// <summary>
        /// 
        /// </summary>
        private DataSet GetData(bool isexport)
        {
            var listParam = new List<SearchParam>();

            if (currentManage.isSuperAdmin <= 0)
            {

            }

            string userId = txtuserId.Text.Trim();
            int tempId = 0;
            if (int.TryParse(userId, out tempId))
            {
                listParam.Add(new SearchParam("userid", tempId));
            }
            if (!string.IsNullOrEmpty(txtbankAccount.Text))
            {
                listParam.Add(new SearchParam("bankAccount", txtbankAccount.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txttrade_no.Text))
            {
                listParam.Add(new SearchParam("trade_no", txttrade_no.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(ddlbankName.Value))
            {
                listParam.Add(new SearchParam("bankcode", ddlbankName.Value));
            }

            DateTime tempdt = DateTime.MinValue;
            if (!string.IsNullOrEmpty(StimeBox.Text.Trim()))
            {
                if (DateTime.TryParse(StimeBox.Text.Trim(), out tempdt))
                {
                    if (tempdt > DateTime.MinValue)
                    {
                        listParam.Add(new SearchParam("stime", StimeBox.Text.Trim()));
                    }
                }
            }

            if (!string.IsNullOrEmpty(EtimeBox.Text.Trim()))
            {
                if (DateTime.TryParse(EtimeBox.Text.Trim(), out tempdt))
                {
                    if (tempdt > DateTime.MinValue)
                    {
                        listParam.Add(new SearchParam("etime", tempdt.AddDays(1)));
                    }
                }
            }
            string orderby = string.Empty;

            DataSet pageData = null;

            if (isexport == false)
            {
                pageData = viviapi.BLL.Finance.WithdrawSuppTranLog.Instance.PageSearch(listParam, this.Pager1.PageSize,
                     this.Pager1.CurrentPageIndex, string.Empty);
            }
            else
            {
                pageData = viviapi.BLL.Finance.WithdrawSuppTranLog.Instance.PageSearch(listParam, 10000,
                     this.Pager1.CurrentPageIndex, string.Empty);
            }

            return pageData;

        }
        #endregion       

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = GetData(true);
                if (ds != null)
                {
                    DataTable data = ds.Tables[1];
                    data.Columns.Add("sName", typeof(string));
                    foreach (DataRow dr in data.Rows)
                    {
                         dr["sName"] = viviapi.BLL.Finance.WithdrawSuppTranLog.GetStatusText(dr["status"]);
                    }
                    data.AcceptChanges();

                    data.TableName = "Rpt";
                    string path = Server.MapPath("~/common/template/xls/distributions.xls");

                    var designer = new WorkbookDesigner();
                    designer.Workbook = new Workbook(path);


                    //数据源 
                    designer.SetDataSource(data);
                    designer.Process();

                    designer.Workbook.Save(this.Response
                        , DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls"
                        , ContentDisposition.Attachment
                        , designer.Workbook.SaveOptions);

                }
            }
            catch (Exception ex)
            {
                AlertAndRedirect(ex.Message);
            }
        }

    }
}