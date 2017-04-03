using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using Aspose.Cells;
using viviapi.BLL.Supplier;
using viviapi.Model;
using viviapi.WebComponents.Web;
using viviLib.Data;

namespace viviAPI.WebAdmin.Console.Withdraw
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PayingByApi : ManagePageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            setPower();

            if (!this.IsPostBack)
            {
                #region 提现方式
                this.ddlmode.Items.Add(new ListItem("--提现方式--", ""));

                foreach (int num in Enum.GetValues(typeof(viviapi.Model.Finance.WithdrawMode)))
                {
                    this.ddlmode.Items.Add(new ListItem(Enum.GetName(typeof(viviapi.Model.Finance.WithdrawMode), num)
                        , num.ToString()));
                }
                #endregion

                #region
                DataTable list = Factory.GetList("isdistribution=1").Tables[0];
                ddlSupplier.Items.Add(new ListItem("--付款接口--", ""));
                ddlSupplier.Items.Add(new ListItem("不走接口", "0"));
                foreach (DataRow dr in list.Rows)
                {
                    this.ddlSupplier.Items.Add(new ListItem(dr["name"].ToString(), dr["code"].ToString()));
                }
                #endregion

                this.BindData();
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

        #region BindData
        /// <summary>
        /// 
        /// </summary>
        private void BindData()
        {
            DataSet pageData = GetData(false);


            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            DataTable data = pageData.Tables[1];


            this.rptdata.DataSource = data;
            this.rptdata.DataBind();
        }
        #endregion

        #region btnSearch_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        #endregion

        #region GetData
        /// <summary>
        /// 
        /// </summary>
        private DataSet GetData(bool isexport)
        {
            var listParam = new List<SearchParam>();
            listParam.Add(new SearchParam("suppstatus", 1));

            int tempId = 0;
            if (!String.IsNullOrEmpty(txtTranno.Text.Trim()))
            {
                listParam.Add(new SearchParam("tranno", txtTranno.Text.Trim()));
            }
            if (!String.IsNullOrEmpty(txtUserId.Text.Trim()))
            {
                if (int.TryParse(this.txtUserId.Text.Trim(), out tempId))
                {
                    listParam.Add(new SearchParam("userid", tempId));
                }
            }
            if (!string.IsNullOrEmpty(ddlSupplier.SelectedValue))
            {
                listParam.Add(new SearchParam("suppId", int.Parse(ddlSupplier.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(ddlbankName.SelectedValue))
            {
                listParam.Add(new SearchParam("payeebank", ddlbankName.SelectedValue));
            }
            if (!string.IsNullOrEmpty(txtAccount.Text.Trim()))
            {
                listParam.Add(new SearchParam("account", txtAccount.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtpayeeName.Text.Trim()))
            {
                listParam.Add(new SearchParam("payeename", txtpayeeName.Text.Trim()));
            }
            DataSet pageData = null;

            if (isexport == false)
            {
                pageData = viviapi.BLL.Finance.Withdraw.Instance.PageSearch(listParam, this.Pager1.PageSize,
                     this.Pager1.CurrentPageIndex, string.Empty,true);
            }
            else
            {
                pageData = viviapi.BLL.Finance.Withdraw.Instance.PageSearch(listParam, 10000,
                     this.Pager1.CurrentPageIndex, string.Empty, false);
            }
            return pageData;
        }
        #endregion

        #region btnExport_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        dr["sName"] = Enum.GetName(typeof(viviapi.Model.SettledStatus), dr["status"]);
                    }
                    data.AcceptChanges();

                    data.TableName = "Rpt";
                    string path = Server.MapPath("~/common/template/xls/settle.xls");

                    var designer = new Aspose.Cells.WorkbookDesigner();
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
        #endregion

        #region 分页
        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="e"></param>
        protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            BindData();
        }
        #endregion

        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Cancel")
            {
                viviapi.BLL.Finance.Withdraw.Instance.Cancel(e.CommandArgument.ToString());
                this.BindData();
            }
        }
    }
}

