using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aspose.Cells;
using viviapi.BLL.Finance.Agent;
using viviapi.BLL.Supplier;
using viviapi.Model;
using viviapi.WebComponents.Web;
using viviLib.Data;

namespace viviAPI.WebAdmin.Console.AgentWithdraw
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AgentDists : ManagePageBase
    {
        protected string total_amount = "0.00元";
        protected string total_charge = "0.00元";
        protected string total_paymoney = "0.00元";

        protected WithdrawAgent stlAgtBLL = new WithdrawAgent();

        public int audit_status
        {
            get
            {

                return viviLib.Web.WebBase.GetQueryStringInt32("audit_status", -1);
            }
        }

        public int payment_status
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringInt32("payment_status", -1);
            }
        }

        #region Page_Load
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
                if (audit_status > -1)
                {
                    this.ddlaudit_status.SelectedValue = audit_status.ToString();
                }

                if (payment_status > -1)
                {
                    this.ddlpayment_status.SelectedValue = payment_status.ToString();
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

                this.BindData();
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
            #region Param List
            var listParam = new List<SearchParam>();
            int tempId = 0;
            if (!String.IsNullOrEmpty(txtUserId.Text.Trim()))
            {
                if (int.TryParse(this.txtUserId.Text.Trim(), out tempId))
                {
                    listParam.Add(new SearchParam("userid", tempId));
                }
            }

            //系统交易号
            if (!String.IsNullOrEmpty(txttrade_no.Text.Trim()))
            {
                listParam.Add(new SearchParam("trade_no", this.txttrade_no.Text.Trim()));
            }

            //商户订单号
            if (!String.IsNullOrEmpty(txtout_trade_no.Text.Trim()))
            {
                listParam.Add(new SearchParam("out_trade_no", this.txtout_trade_no.Text.Trim()));
            }

            //批号
            if (!String.IsNullOrEmpty(this.txtLotno.Text))
            {
                listParam.Add(new SearchParam("lotno", this.txtLotno.Text));
            }

            //收款银行
            if (!String.IsNullOrEmpty(ddlbankCode.SelectedValue))
            {
                listParam.Add(new SearchParam("bankCode", this.ddlbankCode.SelectedValue));
            }

            //收款账户
            if (!String.IsNullOrEmpty(this.txtbankAccountName.Text))
            {
                listParam.Add(new SearchParam("bankAccountName", this.txtbankAccountName.Text));
            }

            //付款接口
            if (!string.IsNullOrEmpty(ddlSupplier.SelectedValue))
            {
                listParam.Add(new SearchParam("tranapi", int.Parse(ddlSupplier.SelectedValue)));
            }

            //上传模式
            if (!string.IsNullOrEmpty(ddlmode.SelectedValue))
            {
                listParam.Add(new SearchParam("mode", int.Parse(ddlmode.SelectedValue)));
            }

            //审核状态
            if (!string.IsNullOrEmpty(ddlaudit_status.SelectedValue))
            {
                listParam.Add(new SearchParam("audit_status", int.Parse(ddlaudit_status.SelectedValue)));
            }
            //付款状态
            if (!string.IsNullOrEmpty(ddlpayment_status.SelectedValue))
            {
                listParam.Add(new SearchParam("payment_status", int.Parse(ddlpayment_status.SelectedValue)));
            }
            //是否取消
            if (!string.IsNullOrEmpty(ddlis_cancel.SelectedValue))
            {
                bool is_cancel = false;
                if (ddlis_cancel.SelectedValue == "1")
                    is_cancel = true;

                listParam.Add(new SearchParam("is_cancel", is_cancel));
            }

            //通知状态
            if (!string.IsNullOrEmpty(ddlnotifystatus.SelectedValue))
            {
                listParam.Add(new SearchParam("notifystatus", int.Parse(ddlnotifystatus.SelectedValue)));
            }



            #endregion

            DataSet pageData = stlAgtBLL.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty,1);


            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            DataTable data = pageData.Tables[1];


            this.rptList.DataSource = data;
            this.rptList.DataBind();

            try
            {
                DataRow row = pageData.Tables[2].Rows[0];
                total_amount = string.Format("{0:f2}", row["amount"]);
                total_charge = string.Format("{0:f2}", row["charge"]);
                total_paymoney = string.Format("{0:f2}", row["totalpay"]);
            }
            catch { 
            
            }
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

        #region rptList

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                try
                {
                    //用户确认
                    int issure = Convert.ToByte(DataBinder.Eval(e.Item.DataItem, "issure"));
                    int audit_status = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "audit_status"));
                    int payment_status = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "payment_status"));
                    bool is_cancel = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "is_cancel"));


                    if (issure == 2 && is_cancel == false)
                    {
                        Button btnCancel = (Button)e.Item.FindControl("btnCancel");
                        Button btnAudits = (Button)e.Item.FindControl("btnAudits");
                        Button btnRefuse = (Button)e.Item.FindControl("btnRefuse");
                        Button btnpaysuccess = (Button)e.Item.FindControl("btnpaysuccess");
                        Button btnpayfail = (Button)e.Item.FindControl("btnpayfail");

                        btnCancel.Visible = (audit_status == 1);
                        btnAudits.Visible = (audit_status == 1);
                        btnRefuse.Visible = (audit_status == 1);

                        if (audit_status == 2)
                        {
                            #region
                            if (payment_status == 1)
                            {
                                int tranApi = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "tranApi"));
                                if (tranApi > 0)
                                {
                                    int suppstatus = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "suppstatus"));
                                    if (suppstatus == 0)
                                    {
                                        Button btnResendToApi = (Button)e.Item.FindControl("btnResendToApi");
                                        btnResendToApi.Visible = true;
                                    }
                                }
                                else
                                {
                                    btnpaysuccess.Visible = true;
                                    btnpayfail.Visible = true;
                                }
                            }
                            #endregion
                        }
                    }

                    Button btnReissue = (Button)e.Item.FindControl("btnReissue");
                    byte mode = Convert.ToByte(DataBinder.Eval(e.Item.DataItem, "mode"));
                    btnReissue.Visible = (mode == 1);
                }
                catch (Exception)
                {
                }
                
            }
        }

        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string message = string.Empty;
            string trade_no = string.Empty;
            if (e.CommandArgument != null)
                trade_no = e.CommandArgument.ToString();

            if (!string.IsNullOrEmpty(trade_no))
            {
                if (e.CommandName == "Cancel")
                {
                    message = stlAgtBLL.doCancel(trade_no);
                }
                else if (e.CommandName == "Audit")
                {
                    message = stlAgtBLL.doAudit(trade_no, currentManage.id, currentManage.username);
                    if (message == "审核成功")
                    {
                        var _model = stlAgtBLL.GetModel(trade_no);
                        if (_model != null)
                        {
                            //viviapi.ETAPI.Common.Withdrawal.InitDistribution2(_model);
                        }
                    }
                }
                else if (e.CommandName == "Refuse")
                {
                    message = stlAgtBLL.doRefuse(trade_no, currentManage.id, currentManage.username);
                }
                else if (e.CommandName == "paysuccess")
                {
                    message = stlAgtBLL.PaySuccess(trade_no);
                }
                else if (e.CommandName == "payfail")
                {
                    message = stlAgtBLL.PayFail(trade_no);
                }
                else if (e.CommandName == "Reissue")
                {
                    stlAgtBLL.DoNotify(trade_no);
                    message = "请求已提交";
                }
                else if (e.CommandName == "ResendToApi")
                {
                    var _model = stlAgtBLL.GetModel(trade_no);
                    if (_model != null)
                    {
                        //viviapi.ETAPI.Common.Withdraw.InitDistribution2(_model);
                    }
                    message = "请求已提交";
                }
            }

            AlertAndRedirect(message);

            BindData();
        }
        #endregion

        #region GetData
        /// <summary>
        /// 
        /// </summary>
        private DataSet GetData()
        {
            #region Param List
            List<SearchParam> listParam = new List<SearchParam>();
            int tempId = 0;
            if (!String.IsNullOrEmpty(txtUserId.Text.Trim()))
            {
                if (int.TryParse(this.txtUserId.Text.Trim(), out tempId))
                {
                    listParam.Add(new SearchParam("userid", tempId));
                }
            }

            //系统交易号
            if (!String.IsNullOrEmpty(txttrade_no.Text.Trim()))
            {
                listParam.Add(new SearchParam("trade_no", this.txttrade_no.Text.Trim()));
            }

            //商户订单号
            if (!String.IsNullOrEmpty(txtout_trade_no.Text.Trim()))
            {
                listParam.Add(new SearchParam("out_trade_no", this.txtout_trade_no.Text.Trim()));
            }

            //批号
            if (!String.IsNullOrEmpty(this.txtLotno.Text))
            {
                listParam.Add(new SearchParam("lotno", this.txtLotno.Text));
            }

            //收款银行
            if (!String.IsNullOrEmpty(ddlbankCode.SelectedValue))
            {
                listParam.Add(new SearchParam("bankCode", this.ddlbankCode.SelectedValue));
            }

            //收款账户
            if (!String.IsNullOrEmpty(this.txtbankAccountName.Text))
            {
                listParam.Add(new SearchParam("bankAccountName", this.txtbankAccountName.Text));
            }

            //付款接口
            if (!string.IsNullOrEmpty(ddlSupplier.SelectedValue))
            {
                listParam.Add(new SearchParam("tranapi", int.Parse(ddlSupplier.SelectedValue)));
            }

            //上传模式
            if (!string.IsNullOrEmpty(ddlmode.SelectedValue))
            {
                listParam.Add(new SearchParam("mode", int.Parse(ddlmode.SelectedValue)));
            }

            //审核状态
            if (!string.IsNullOrEmpty(ddlaudit_status.SelectedValue))
            {
                listParam.Add(new SearchParam("audit_status", int.Parse(ddlaudit_status.SelectedValue)));
            }
            //付款状态
            if (!string.IsNullOrEmpty(ddlpayment_status.SelectedValue))
            {
                listParam.Add(new SearchParam("payment_status", int.Parse(ddlpayment_status.SelectedValue)));
            }
            //是否取消
            if (!string.IsNullOrEmpty(ddlis_cancel.SelectedValue))
            {
                bool is_cancel = false;
                if (ddlis_cancel.SelectedValue == "1")
                    is_cancel = true;

                listParam.Add(new SearchParam("is_cancel", is_cancel));
            }

            //通知状态
            if (!string.IsNullOrEmpty(ddlnotifystatus.SelectedValue))
            {
                listParam.Add(new SearchParam("notifystatus", int.Parse(ddlnotifystatus.SelectedValue)));
            }



            #endregion

            DataSet pageData = stlAgtBLL.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty, 1);


            return pageData;
        }
        #endregion

        #region btnExport_Click
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = GetData();
                if (ds != null)
                {
                    DataTable data = ds.Tables[1];
                    data.Columns.Add("sName", typeof(string));
                    foreach (DataRow dr in data.Rows)
                    {
                        dr["sName"] = stlAgtBLL.GetPaymentStatusText(dr["payment_status"]);
                    }
                    data.AcceptChanges();

                    data.TableName = "Rpt";
                    string path = Server.MapPath("~/common/template/xls/SettledAgent.xls");

                    Aspose.Cells.WorkbookDesigner designer = new Aspose.Cells.WorkbookDesigner();
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

    }
}