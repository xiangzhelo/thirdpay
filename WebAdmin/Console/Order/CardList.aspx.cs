using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Aspose.Cells;
using viviapi.BLL;
using viviapi.BLL.Order.Card;
using viviapi.Model;
using viviapi.Model.User;
using viviapi.WebComponents;
using viviapi.WebComponents.Web;
using viviLib.Data;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.Order
{
    public partial class CardOrderList : ManagePageBase
    {
        /// <summary>
        /// 交易金额
        /// </summary>
        protected string TotalTranATM = string.Empty;

        /// <summary>
        /// 商户金额
        /// </summary>
        protected string TotalUserATM = string.Empty;

        /// <summary>
        /// 代理总提成
        /// </summary>
        protected string TotalPromATM = string.Empty;

        /// <summary>
        /// 平台利润
        /// </summary>
        protected string TotalProfit = string.Empty;

        //业务员提成
        protected string TotalCommission = "0.00";


        protected int Status
        {
            get
            {
                return WebBase.GetQueryStringInt32("status", -1);
            }
        }

        protected int deduct
        {
            get
            {
                return WebBase.GetQueryStringInt32("deduct", -1);
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
                this.StimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.EtimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.StimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.EtimeBox.Attributes.Add("onFocus", "WdatePicker()");

                InitForm();
                LoadData();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitForm()
        {

            if (this.Status > -1)
            {
                this.ddlOrderStatus.SelectedValue = Status.ToString();
            }

            if (this.deduct > -1)
            {
                this.ddldeduct.SelectedValue = deduct.ToString();
            }

            DataTable list = viviapi.BLL.Supplier.Factory.GetList("").Tables[0];
            ddlSupplier2.Items.Add(new ListItem("--请选择接口商--", ""));
            foreach (DataRow dr in list.Rows)
            {
                this.ddlSupplier2.Items.Add(new ListItem(dr["name"].ToString(), dr["code"].ToString()));
            }

            ddlmange.Items.Add("--请选择业务员--");
            DataTable data = viviapi.BLL.ManageFactory.GetList(" status =1").Tables[0];
            foreach (DataRow dr in data.Rows)
            {
                ddlmange.Items.Add(new ListItem(dr["username"].ToString(), dr["id"].ToString()));
            }
        }
        #region LoadData
        /// <summary>
        /// 
        /// </summary>
        private void LoadData()
        {
            var listParam = new List<SearchParam>();

            int tempId = 0;
            if (isSuperAdmin == false)
            {
                listParam.Add(new SearchParam("manageId", ManageId));
            }
            else
            {
                if (!string.IsNullOrEmpty(ddlmange.SelectedValue))
                {
                    if (int.TryParse(ddlmange.SelectedValue, out tempId))
                    {
                        listParam.Add(new SearchParam("manageId", tempId));
                    }
                }
            }

            if (!string.IsNullOrEmpty(ddldeduct.SelectedValue))
            {
                if (int.TryParse(ddldeduct.SelectedValue, out tempId))
                {
                    listParam.Add(new SearchParam("deduct", tempId));
                }
            }
            if (!string.IsNullOrEmpty(ddlSupplier2.SelectedValue))
            {
                listParam.Add(new SearchParam("supplierid", int.Parse(ddlSupplier2.SelectedValue)));
            }

            if (!string.IsNullOrEmpty(this.txtOrderId.Text.Trim()))
            {
                listParam.Add(new SearchParam("orderId_like", txtOrderId.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtuserid.Text.Trim()))
            {
                if (int.TryParse(txtuserid.Text.Trim(), out tempId))
                {
                    listParam.Add(new SearchParam("userid", tempId));
                }
            }

            if (!string.IsNullOrEmpty(ddlChannelType.SelectedValue))
            {
                if (int.TryParse(ddlChannelType.SelectedValue, out tempId))
                {
                    if (tempId > 0)
                    {
                        listParam.Add(new SearchParam("typeId", tempId));
                    }
                }
            }

            if (!string.IsNullOrEmpty(txtUserOrder.Text.Trim()))
            {
                listParam.Add(new SearchParam("userorder", txtUserOrder.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtCardNo.Text.Trim()))
            {
                listParam.Add(new SearchParam("cardNo", txtCardNo.Text.Trim()));
            }

            if (!string.IsNullOrEmpty(txtSuppOrder.Text.Trim()))
            {
                listParam.Add(new SearchParam("supplierOrder", txtSuppOrder.Text.Trim()));
            }

            if (!string.IsNullOrEmpty(ddlOrderStatus.SelectedValue))
            {
                if (int.TryParse(ddlOrderStatus.SelectedValue, out tempId))
                {
                    if (tempId > 0)
                    {
                        listParam.Add(new SearchParam("status", tempId));
                    }
                }
            }

            if (!string.IsNullOrEmpty(ddlNotifyStatus.SelectedValue))
            {
                if (int.TryParse(ddlNotifyStatus.SelectedValue, out tempId))
                {
                    if (tempId > 0)
                    {
                        listParam.Add(new SearchParam("notifystat", tempId));
                    }
                }
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


            string orderby = string.Empty;// orderBy + " " + orderByType;

            DataSet pageData = Factory.Instance.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby, true);
            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.rptOrders.DataSource = pageData.Tables[1];
            this.rptOrders.DataBind();


            DataTable stat = pageData.Tables[2];
            TotalTranATM = stat.Rows[0]["realvalue"].ToString();
            TotalUserATM = stat.Rows[0]["payAmt"].ToString();
            TotalPromATM = stat.Rows[0]["promAmt"].ToString();
            TotalProfit = stat.Rows[0]["profits"].ToString();
            if (stat.Rows[0]["commission"] != DBNull.Value)
            {
                TotalCommission = Convert.ToDecimal(stat.Rows[0]["commission"]).ToString("f2");
            }

        }
        #endregion

        #region setPower
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

        #region 分页
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        #endregion

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
                if (this.isSuperAdmin == false)
                {
                    var thProfits = e.Item.FindControl("th_profits") as HtmlTableCell;
                    if (thProfits != null)
                        thProfits.Visible = false;

                    var thSupplier = e.Item.FindControl("th_supplier") as HtmlTableCell;
                    if (thSupplier != null)
                        thSupplier.Visible = false;
                }
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (this.isSuperAdmin == false)
                {
                    var thProfits = e.Item.FindControl("tr_profits") as HtmlTableCell;
                    if (thProfits != null)
                        thProfits.Visible = false;

                    var thSupplier = e.Item.FindControl("tr_supplier") as HtmlTableCell;
                    if (thSupplier != null)
                        thSupplier.Visible = false;
                }

                Button btnReissue = e.Item.FindControl("btnReissue") as Button;
                Button btnRest = e.Item.FindControl("btnRest") as Button;
                Button btnDeduct = e.Item.FindControl("btnDeduct") as Button;
                Button btnReDeduct = e.Item.FindControl("btnReDeduct") as Button;
                Button btnChange = e.Item.FindControl("btnChange") as Button;


               
                int userid = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "userid").ToString());
                string status = DataBinder.Eval(e.Item.DataItem, "status").ToString();
                string deduct = DataBinder.Eval(e.Item.DataItem, "deduct").ToString();

                btnChange.Visible = (status == "4" || status == "1");

                switch (status)
                {
                    case "1":
                        btnChange.Visible = true;
                        btnReissue.Visible = false;
                        btnRest.Visible = true;
                        btnDeduct.Visible = false;
                        btnReDeduct.Visible = false;
                        break;
                    case "2":

                        btnChange.Visible = false;
                        btnReissue.Visible = true;
                        btnRest.Visible = false;
                        btnReDeduct.Visible = false;
                        btnDeduct.OnClientClick = "return confirm('是否确定扣量？')";

                        if (deduct == "0")
                        {
                            btnDeduct.Visible = true;
                            btnReDeduct.Visible = false;
                            object completeTime = DataBinder.Eval(e.Item.DataItem, "processingtime");
                            double difftime = WebUtility.GetDifftime(userid, completeTime);
                            if (difftime > viviapi.SysConfig.RuntimeSetting.DeductSafetyTime)
                            {
                                btnDeduct.Text = "扣";
                            }
                            else if (difftime > 0.0 && difftime <= viviapi.SysConfig.RuntimeSetting.DeductSafetyTime)
                            {
                                btnDeduct.Text = "危险";
                            }
                            else
                            {
                                btnDeduct.Text = "不能";
                            }
                        }
                        else if (deduct == "1")
                        {
                            btnDeduct.Visible = false;
                            btnReDeduct.Visible = true;
                        }
                        break;
                    case "4":
                        btnReissue.Visible = true;
                        btnRest.Visible = false;
                        btnDeduct.Visible = false;
                        btnReDeduct.Visible = false;
                        btnChange.Visible = true;
                        break;
                }
            }
        }
        #endregion

        #region rptOrders_ItemCommand
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptOrders_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Reissue")
                {
                    string orderId = e.CommandArgument.ToString();
                    if (string.IsNullOrEmpty(orderId))
                        return;

                    string callbackText = viviapi.SysInterface.Card.APINotification.SynchronousNotify(orderId);

                    ShowMessageBox("返回：" + callbackText);
                }
                if (e.CommandName == "Change")
                {
                    string orderId = e.CommandArgument.ToString();
                    if (string.IsNullOrEmpty(orderId))
                        return;

                    Response.Redirect(string.Format("ModifyOrder.aspx?orderid={0}", orderId));

                    //string callback = "调整失败";
                    //if (BLL.Order.Dal.UpdateCardOrderStatus(orderId))
                    //{
                    //    callback = "调整成功";
                    //}

                    //AlertAndRedirect(callback, Url);
                }
                else if (e.CommandName == "ResetOrder")
                {
                    Response.Redirect(string.Format("CardResetOrder.aspx?orderid={0}", e.CommandArgument));
                }
                else if (e.CommandName == "Deduct")//扣量
                {
                    string orderId = e.CommandArgument.ToString();

                    if (Factory.Instance.Deduct(orderId))
                    {
                        ShowMessageBox("扣量成功");
                    }
                    else
                    {
                        ShowMessageBox("扣量失败，可能是余额不足");
                    }
                    LoadData();
                }
                else if (e.CommandName == "ReDeduct")//归还订单
                {
                    string orderId = e.CommandArgument.ToString();
                    if (Factory.Instance.ReDeduct(orderId))
                    {
                        ShowMessageBox("还单成功");
                    }
                    else
                    {
                        ShowMessageBox("还单失败");
                    }
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }
        #endregion

        #region btnExport_Click
        #region GetData
        /// <summary>
        /// 
        /// </summary>
        private DataSet GetData()
        {
            #region
            List<SearchParam> listParam = new List<SearchParam>();

            int tempId = 0;
            if (isSuperAdmin == false)
            {
                listParam.Add(new SearchParam("manageId", ManageId));
            }
            else
            {
                if (!string.IsNullOrEmpty(ddlmange.SelectedValue))
                {
                    if (int.TryParse(ddlmange.SelectedValue, out tempId))
                    {
                        listParam.Add(new SearchParam("manageId", tempId));
                    }
                }
            }



            if (!string.IsNullOrEmpty(this.txtOrderId.Text.Trim()))
            {
                listParam.Add(new SearchParam("orderId_like", txtOrderId.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtuserid.Text.Trim()))
            {
                if (int.TryParse(txtuserid.Text.Trim(), out tempId))
                {
                    listParam.Add(new SearchParam("userid", tempId));
                }
            }

            if (!string.IsNullOrEmpty(ddlChannelType.SelectedValue))
            {
                if (int.TryParse(ddlChannelType.SelectedValue, out tempId))
                {
                    if (tempId > 0)
                    {
                        listParam.Add(new SearchParam("typeId", tempId));
                    }
                }
            }

            if (!string.IsNullOrEmpty(txtUserOrder.Text.Trim()))
            {
                listParam.Add(new SearchParam("userorder", txtUserOrder.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(txtCardNo.Text.Trim()))
            {
                listParam.Add(new SearchParam("cardNo", txtCardNo.Text.Trim()));
            }

            if (!string.IsNullOrEmpty(txtSuppOrder.Text.Trim()))
            {
                listParam.Add(new SearchParam("supplierOrder", txtSuppOrder.Text.Trim()));
            }

            if (!string.IsNullOrEmpty(ddlOrderStatus.SelectedValue))
            {
                if (int.TryParse(ddlOrderStatus.SelectedValue, out tempId))
                {
                    if (tempId > 0)
                    {
                        listParam.Add(new SearchParam("status", tempId));
                    }
                }
            }

            if (!string.IsNullOrEmpty(ddlNotifyStatus.SelectedValue))
            {
                if (int.TryParse(ddlNotifyStatus.SelectedValue, out tempId))
                {
                    if (tempId > 0)
                    {
                        listParam.Add(new SearchParam("notifystat", tempId));
                    }
                }
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
            #endregion
            string orderby = string.Empty;// orderBy + " " + orderByType;


            DataSet pageData = viviapi.BLL.Order.Card.Factory.Instance.PageSearch(listParam, 10000, 1, orderby, true);
            return pageData;
        }
        #endregion
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = GetData();
                if (ds != null)
                {
                    DataTable data = ds.Tables[1];
                    //data.Columns.Add("sName", typeof(string));
                    //foreach (DataRow dr in data.Rows)
                    //{
                    //    dr["sName"] = stlAgtBLL.GetPaymentStatusText(dr["payment_status"]);
                    //}
                    //data.AcceptChanges();

                    data.TableName = "Rpt";
                    string path = Server.MapPath("~/common/template/xls/cards.xls");

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
    }
}
