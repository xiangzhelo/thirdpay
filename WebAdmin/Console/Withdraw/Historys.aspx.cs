using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using Aspose.Cells;
using viviapi.BLL.Supplier;
using viviapi.Model;
using viviapi.Model.Finance;
using viviLib.Data;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.Withdraw
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Historys : viviapi.WebComponents.Web.ManagePageBase
    {
        protected string PageTotalAmount = "0.00";
        protected string PageTotalCharges = "0.00";
        protected string PageTotalWithdrawAmt = "0.00";

        protected string TotalAmount = "0.00";
        protected string TotalCharges = "0.00";
        protected string TotalWithdrawAmt = "0.00";



        public string action
        {
            get
            {
                return WebBase.GetQueryStringString("action", "");
            }
        }

        public int userId
        {
            get
            {
                return WebBase.GetQueryStringInt32("userid", 0);
            }
        }

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

                DataTable list = Factory.GetList("isdistribution=1").Tables[0];
                ddlSupplier.Items.Add(new ListItem("--付款接口--", ""));
                ddlSupplier.Items.Add(new ListItem("不走接口", "0"));
                foreach (DataRow dr in list.Rows)
                {
                    this.ddlSupplier.Items.Add(new ListItem(dr["name"].ToString(), dr["code"].ToString()));
                }

                this.txtStimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.txtEtimeBox.Attributes.Add("onFocus", "WdatePicker()");

                this.txtStimeBox.Text = DateTime.Now.ToString("yyyy-MM-01");
                this.txtEtimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");

                this.ddlStatusList.Items.Add(new ListItem("--状态--", ""));
                foreach (int num in Enum.GetValues(typeof(WithdrawStatus)))
                {
                    this.ddlStatusList.Items.Add(new ListItem(Enum.GetName(typeof(WithdrawStatus), num), num.ToString()));
                }
                ddlStatusList.SelectedValue = ((int)WithdrawStatus.Paid).ToString();

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

        protected string TotalMoney = "0.00元";

        #region BindData
        /// <summary>
        /// 
        /// </summary>
        private void BindData()
        {

            DataSet pageData = GetData(false);

            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            DataTable data = pageData.Tables[1];

            #region
            if (data != null)
            {
                data.Columns.Add("StatusText");
                foreach (DataRow row in data.Rows)
                {
                    var status = (WithdrawStatus)row["Status"];
                    if (status == WithdrawStatus.Auditing)
                    {
                        row["StatusText"] = "<font color='#66CC00'>审核中</font>";
                    }
                    else if (status == WithdrawStatus.Paying)
                    {
                        row["StatusText"] = "<a href=\"Pay.aspx?tranno=" + row["tranno"].ToString() + "\">进行支付</a>";
                    }
                    else if (status == WithdrawStatus.Canceled)
                    {
                        row["StatusText"] = "<font color='red'>已取消</font>";
                    }
                    else if (status == WithdrawStatus.Paid)
                    {
                        row["StatusText"] = "<font color='blue'>已支付</font>";
                    }
                }
            }
            #endregion

            if (data != null && data.Rows.Count > 0)
            {
                PageTotalAmount = string.Format("{0:f2}", data.Compute("sum(amount)", ""));
                PageTotalCharges = string.Format("{0:f2}", data.Compute("sum(charges)", ""));
                PageTotalWithdrawAmt = string.Format("{0:f2}", data.Compute("sum(withdraw)", ""));
            }


            DataTable data2 = pageData.Tables[2];
            if (data2 != null && data2.Rows.Count > 0)
            {
                TotalAmount = string.Format("{0:f2}", data2.Rows[0]["tapplyAmt"]);
                TotalCharges = string.Format("{0:f2}", data2.Rows[0]["tCharges"]);
                TotalWithdrawAmt = string.Format("{0:f2}", data2.Rows[0]["trealPay"]);
            }


            this.rptList.DataSource = data;
            this.rptList.DataBind();

            //TotalMoney = Convert.ToString(pageData.Tables[2].Rows[0][0]);
        }
        #endregion



        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            BindData();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        #region GetData
        /// <summary>
        /// 
        /// </summary>
        private DataSet GetData(bool isexport)
        {
            #region Param
            var listParam = new List<SearchParam>();
            if (!string.IsNullOrEmpty(this.ddlStatusList.SelectedValue))
            {
                listParam.Add(new SearchParam("status", int.Parse(this.ddlStatusList.SelectedValue)));
            }

            int tempId = 0;
            if (!String.IsNullOrEmpty(txtItemInfoId.Text.Trim()))
            {
                if (int.TryParse(this.txtItemInfoId.Text.Trim(), out tempId))
                {
                    listParam.Add(new SearchParam("id", tempId));
                }
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
            DateTime tempdt;
            if (!string.IsNullOrEmpty(txtStimeBox.Text.Trim()))
            {
                if (DateTime.TryParse(txtStimeBox.Text.Trim(), out tempdt))
                {
                    if (tempdt > DateTime.MinValue)
                    {
                        listParam.Add(new SearchParam("saddtime", txtStimeBox.Text.Trim()));
                    }
                }
            }

            if (!string.IsNullOrEmpty(txtEtimeBox.Text.Trim()))
            {
                if (DateTime.TryParse(txtEtimeBox.Text.Trim(), out tempdt))
                {
                    if (tempdt > DateTime.MinValue)
                    {
                        listParam.Add(new SearchParam("eaddtime", tempdt.AddDays(1)));
                    }
                }
            }
            #endregion

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
                        dr["sName"] = Enum.GetName(typeof(viviapi.Model.Finance.WithdrawStatus), dr["status"]);
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
    }
}