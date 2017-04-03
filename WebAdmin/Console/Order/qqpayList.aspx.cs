using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using viviapi.BLL.Order.Bank;
using viviapi.Model;
using viviapi.Model.User;
using viviapi.WebComponents;
using viviapi.WebComponents.Web;
using viviLib.Data;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.Order
{
    /// <summary>
    /// 
    /// </summary>
    public partial class qqpayList : ManagePageBase
    {
        /// <summary>
        /// 交易金额
        /// </summary>
        protected string TotalTranAtm = "0.00";

        /// <summary>
        /// 商户金额
        /// </summary>
        protected string TotalUserAtm = "0.00";

        /// <summary>
        /// 代理总提成
        /// </summary>
        protected string TotalPromATM = "0.00";

        /// <summary>
        /// 平台利润
        /// </summary>
        protected string TotalProfit = "0.00";

        //业务员提成
        protected string TotalCommission = "0.00";

        #region
        protected int UserId
        {
            get
            {
                return WebBase.GetQueryStringInt32("userid", -1);
            }
        }

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

        protected int CurrPage
        {
            get
            {
                return WebBase.GetQueryStringInt32("currpage", -1);
            }
        }

        protected int MID
        {
            get
            {
                return WebBase.GetQueryStringInt32("mid", -1);
            }
        }

        protected int NotifyStatus
        {
            get
            {
                return WebBase.GetQueryStringInt32("ns", -1);
            }
        }

        protected string Kano
        {
            get
            {
                return WebBase.GetQueryStringString("ka", string.Empty);
            }
        }


        protected string Stime
        {
            get
            {
                return WebBase.GetQueryStringString("stime", string.Empty);
            }
        }

        protected string Etime
        {
            get
            {
                return WebBase.GetQueryStringString("etime", string.Empty);
            }
        }

        protected string Sysorderid
        {
            get
            {
                return WebBase.GetQueryStringString("orderid", string.Empty);
            }
        }

        protected string Userorderid
        {
            get
            {
                return WebBase.GetQueryStringString("userorder", string.Empty);
            }
        }

        protected string supporderid
        {
            get
            {
                return WebBase.GetQueryStringString("supporder", string.Empty);
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
                this.StimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.EtimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.StimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.EtimeBox.Attributes.Add("onFocus", "WdatePicker()");

                DataTable list = viviapi.BLL.Supplier.Factory.GetList("").Tables[0];
                ddlSupplier2.Items.Add(new ListItem("--请选择接口商--", ""));
                foreach (DataRow dr in list.Rows)
                {
                    this.ddlSupplier2.Items.Add(new ListItem(dr["name"].ToString(), dr["code"].ToString()));

                }

                InitForm();
                LoadData();
            }
        }

        #region InitForm
        /// <summary>
        /// 
        /// </summary>
        private void InitForm()
        {
            if (this.UserId > -1)
            {
                this.txtuserid.Text = UserId.ToString(CultureInfo.InvariantCulture);
            }
            if (this.Status > -1)
            {
                this.ddlOrderStatus.SelectedValue = Status.ToString(CultureInfo.InvariantCulture);
            }

            if (this.deduct > -1)
            {
                this.ddldeduct.SelectedValue = deduct.ToString();
            }
            if (this.NotifyStatus > -1)
            {
                this.ddlNotifyStatus.SelectedValue = this.NotifyStatus.ToString(CultureInfo.InvariantCulture);
            }
            //if (!string.IsNullOrEmpty(this.kano))
            //{
            //    this.txtCardNo.Text = this.kano;
            //}
            if (!string.IsNullOrEmpty(this.Stime))
            {
                this.StimeBox.Text = this.Stime;
            }
            if (!string.IsNullOrEmpty(this.Etime))
            {
                this.EtimeBox.Text = this.Etime;
            }
            if (!string.IsNullOrEmpty(this.Sysorderid))
            {
                this.txtOrderId.Text = this.Sysorderid;
            }
            if (!string.IsNullOrEmpty(this.Userorderid))
            {
                this.txtUserOrder.Text = this.Userorderid;
            }
            if (!string.IsNullOrEmpty(this.supporderid))
            {
                this.txtSuppOrder.Text = this.supporderid;
            }

            ddlmange.Items.Add("--请选择业务员--");
            DataTable data = viviapi.BLL.ManageFactory.GetList(" status =1").Tables[0];
            foreach (DataRow dr in data.Rows)
            {
                ddlmange.Items.Add(new ListItem(dr["username"].ToString(), dr["id"].ToString()));
            }

            if (this.MID > -1)
                this.ddlmange.SelectedValue = this.MID.ToString(CultureInfo.InvariantCulture);
        }
        #endregion

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

            if (!string.IsNullOrEmpty(this.txtOrderId.Text.Trim()))
            {
                listParam.Add(new SearchParam("orderId_like", txtOrderId.Text));
            }
            if (!string.IsNullOrEmpty(txtuserid.Text.Trim()))
            {
                if (int.TryParse(txtuserid.Text.Trim(), out tempId))
                {
                    listParam.Add(new SearchParam("userid", tempId));
                }
            }
            if (!string.IsNullOrEmpty(ddlSupplier2.SelectedValue))
            {
                listParam.Add(new SearchParam("supplierid", int.Parse(ddlSupplier2.SelectedValue)));
            }

            //if (!string.IsNullOrEmpty(ddlChannelType.SelectedValue))
            //{
            //    if (int.TryParse(ddlChannelType.SelectedValue, out tempId))
            //    {
            //        if (tempId > 0)
            //        {
                        listParam.Add(new SearchParam("typeId", 100));
            //        }
            //    }
            //}

            if (!string.IsNullOrEmpty(ddldeduct.SelectedValue))
            {
                if (int.TryParse(ddldeduct.SelectedValue, out tempId))
                {
                    listParam.Add(new SearchParam("deduct", tempId));
                }
            }

            if (!string.IsNullOrEmpty(txtUserOrder.Text.Trim()))
            {
                listParam.Add(new SearchParam("userorder", txtUserOrder.Text.Trim()));
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

            DateTime tempdt;
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

            if (this.CurrPage > -1)
                this.Pager1.CurrentPageIndex = this.CurrPage;

            DataTable stat = pageData.Tables[2];
            if (stat.Rows.Count >= 0)
            {
                if (stat.Rows[0]["realvalue"] != DBNull.Value)
                {
                    TotalTranAtm = Convert.ToDecimal(stat.Rows[0]["realvalue"]).ToString("f2");
                }
                if (stat.Rows[0]["payAmt"] != DBNull.Value)
                {
                    TotalUserAtm = Convert.ToDecimal(stat.Rows[0]["payAmt"]).ToString("f2");
                }
                if (stat.Rows[0]["commission"] != DBNull.Value)
                {
                    TotalCommission = Convert.ToDecimal(stat.Rows[0]["commission"]).ToString("f2");
                }
                if (stat.Rows[0]["profits"] != DBNull.Value)
                {
                    TotalProfit = Convert.ToDecimal(stat.Rows[0]["profits"]).ToString("f2");
                }
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

        protected void Pager1_PageChanged(object sender, EventArgs e)
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

                var btnReissue = e.Item.FindControl("btnReissue") as Button;
                var btnRest = e.Item.FindControl("btnRest") as Button;
                var btnDeduct = e.Item.FindControl("btnDeduct") as Button;
                var btnReDeduct = e.Item.FindControl("btnReDeduct") as Button;

                int userid = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "userid").ToString());
                string status = DataBinder.Eval(e.Item.DataItem, "status").ToString();
                string deduct = DataBinder.Eval(e.Item.DataItem, "deduct").ToString();

                switch (status)
                {
                    case "1":
                        btnReissue.Visible = false;
                        btnRest.Visible = true;
                        btnDeduct.Visible = false;
                        btnReDeduct.Visible = false;
                        break;

                    case "2":
                        btnReissue.Visible = true;
                        btnRest.Visible = false;
                        btnReDeduct.Visible = false;

                        if (deduct == "0")
                        {
                            #region
                            btnDeduct.Visible = true;
                            btnReDeduct.Visible = false;

                            btnDeduct.OnClientClick = "return confirm('是否确定扣量？')";
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
                            #endregion
                        }
                        else if (deduct == "1")
                        {
                            #region
                            btnDeduct.Visible = false;
                            btnReDeduct.Visible = true;
                            #endregion
                        }
                        break;

                    case "4":
                        btnReissue.Visible = true;
                        btnRest.Visible = false;
                        btnDeduct.Visible = false;
                        btnReDeduct.Visible = false;
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
                    #region 补发
                    string orderId = e.CommandArgument.ToString();
                    if (string.IsNullOrEmpty(orderId))
                        return;

                    string callback = viviapi.SysInterface.Bank.Utility.SynchronousNotify(orderId);

                    ShowMessageBox("返回：" + callback);
                    #endregion
                }
                else if (e.CommandName == "ResetOrder")
                {
                    #region 补单
                    string argument = e.CommandArgument.ToString();

                    Response.Redirect(string.Format("BankResetOrder.aspx?orderid={0}", argument));
                    #endregion
                }
                else if (e.CommandName == "Deduct")//扣量
                {
                    string orderId = e.CommandArgument.ToString();

                    if (Factory.Instance.Deduct(orderId))
                    {
                        ShowMessageBox("扣量成功：");
                    }
                    else
                    {
                        ShowMessageBox("扣量失败,可能是余额不足");
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

    }
}