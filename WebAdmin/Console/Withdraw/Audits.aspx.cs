using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aspose.Cells;
using viviapi.BLL.Supplier;
using viviapi.Model;
using viviapi.Model.Finance;
using viviapi.WebComponents.Web;
using viviLib.Data;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.Withdraw
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Audits : ManagePageBase
    {
        protected string PageTotalAmount = "0.00";
        protected string TotalAmount = "0.00";

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public int ItemInfoId
        {
            get
            {
                return WebBase.GetQueryStringInt32("ID", 0);
            }
        }

        public int ItemInfoStatus
        {
            get
            {
                return WebBase.GetQueryStringInt32("status", 0);
            }
        }
        #endregion

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
                this.ddlmode.Items.Add(new ListItem("--提现方式--", ""));

                foreach (int num in Enum.GetValues(typeof(viviapi.Model.Finance.WithdrawMode)))
                {
                    this.ddlmode.Items.Add(new ListItem(Enum.GetName(typeof(viviapi.Model.Finance.WithdrawMode), num)
                        , num.ToString()));
                }

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
            DataTable data2 = pageData.Tables[2];


            if (data != null && data.Rows.Count > 0)
            {
                PageTotalAmount = string.Format("{0:f2}", data.Compute("sum(amount)", ""));
            }

            if (data2 != null && data2.Rows.Count > 0)
            {
                TotalAmount = string.Format("{0:f2}", data2.Rows[0]["tapplyAmt"]);
            }
            this.rptdata.DataSource = data;
            this.rptdata.DataBind();
        }
        #endregion

        #region GetData
        /// <summary>
        /// 
        /// </summary>
        private DataSet GetData(bool isexport)
        {
            var listParam = new List<SearchParam>();
            listParam.Add(new SearchParam("status", 2));

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
                     this.Pager1.CurrentPageIndex, string.Empty, true);
            }
            else
            {
                pageData = viviapi.BLL.Finance.Withdraw.Instance.PageSearch(listParam, 10000,
                     this.Pager1.CurrentPageIndex, string.Empty, false);
            }

            return pageData;
        }
        #endregion

        #region 查找
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

        #region 批量审核
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPass_Click(object sender, EventArgs e)
        {
            int success = 0;

            string trannoList = Request.Form["TrannoList"];
            string batchNo = Guid.NewGuid().ToString("N");

            if (!string.IsNullOrEmpty(trannoList))
            {
                foreach (string tranNo in trannoList.Split(','))
                {
                    bool result = Audit(tranNo, batchNo, 1, "");
                    if (result == true)
                    {
                        success++;
                    }
                }

                ShowMessageBox("操作成功" + success.ToString(CultureInfo.InvariantCulture) + "笔");
                BindData();
            }
            else
            {
                ShowMessageBox("请选择要审核的申请!");
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

        #region btnAllPass_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAllPass_Click(object sender, EventArgs e)
        {
            int success = 0;

            string batchNo = Guid.NewGuid().ToString("N");

            DataTable data = viviapi.BLL.Finance.Withdraw.Instance.GetList("status=2").Tables[0];

            if (data != null && data.Rows.Count > 0)
            {
                foreach (DataRow row in data.Rows)
                {
                    bool result = Audit(row["tranno"].ToString(), batchNo, 1, "");
                    if (result == true)
                    {
                        success++;
                    }
                }

                ShowMessageBox("操作成功" + success.ToString(CultureInfo.InvariantCulture) + "笔");
                BindData();
            }
            else
            {
                ShowMessageBox("请选择要审核的申请!");
            }
        }
        #endregion

        #region btnallfail_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnallfail_Click(object sender, EventArgs e)
        {
            int success = 0;

            string batchNo = Guid.NewGuid().ToString("N");

            DataTable data = viviapi.BLL.Finance.Withdraw.Instance.GetList("status=2").Tables[0];

            if (data != null && data.Rows.Count > 0)
            {
                foreach (DataRow row in data.Rows)
                {
                    bool result = Audit(row["tranno"].ToString(), batchNo, 0, "");
                    if (result == true)
                    {
                        success++;
                    }
                }

                ShowMessageBox("操作成功" + success.ToString(CultureInfo.InvariantCulture) + "笔");
                BindData();
            }
            else
            {
                ShowMessageBox("请选择要审核的申请!");
            }
        }
        #endregion

        #region GetTranApiName
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string GetTranApiName(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return "不走接口";

            int id = Convert.ToInt32(obj);
            if (id == 100)
                return "财付通";

            return "";
        }
        #endregion

        #region Pager1_PageChanged
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            BindData();
        }
        #endregion

        #region rptdata_ItemDataBound
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptdata_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

            }
        }
        #endregion

        #region rptdata_ItemCommand
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptdata_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string tranno = e.CommandArgument.ToString();

            if ((e.CommandName == "Pass" || e.CommandName == "Refuse") && !string.IsNullOrEmpty(tranno))
            {
                byte auditResult = 0;

                if (e.CommandName == "Pass")
                {
                    auditResult = 1;
                }
                else if (e.CommandName == "Refuse")
                {
                    auditResult = 0;
                }

                Audit(tranno, "", auditResult, currentManage.relname);
            }

            BindData();
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

        #region Audit
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tranno"></param>
        /// <param name="batchNo"></param>
        /// <param name="auditResult"></param>
        /// <param name="remark"></param>
        public bool Audit(string tranno, string batchNo, byte auditResult, string remark)
        {
            bool result = viviapi.BLL.Finance.Withdraw.Instance.Audit(tranno, batchNo, auditResult,
                        currentManage.relname);

            if (result == true)
            {
                if (auditResult == 1)
                {
                    var info = viviapi.BLL.Finance.Withdraw.Instance.GetModel(tranno);

                    if (info.Suppstatus == 1 && info.SuppId > 0)
                    {
                        //走接口 付款
                        viviapi.ETAPI.Common.Withdrawal.InitDistribution(info);
                    }
                }
            }

            return result;
        }
        #endregion
    }
}