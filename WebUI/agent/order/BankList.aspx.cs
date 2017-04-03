using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.BLL.Order.Bank;
using viviapi.Model;
using viviapi.Model.User;
using viviapi.WebComponents.Web;
using viviLib.Data;

namespace viviAPI.WebUI7uka.agent.order
{
    /// <summary>
    /// 
    /// </summary>
    public partial class BankList : AgentPageBase
    {
        /// <summary>
        /// 交易金额
        /// </summary>
        protected string TotalTranATM = "0.00";

        /// <summary>
        /// 商户金额
        /// </summary>
        protected string TotalUserATM = "0.00";

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
        protected int pUserId
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringInt32("userid", -1);
            }
        }

        protected int Status
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringInt32("status", -1);
            }
        }

        protected int ctype
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringInt32("ctype", -1);
            }
        }

        protected int currPage
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringInt32("currpage", -1);
            }
        }

        protected int MID
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringInt32("mid", -1);
            }
        }

        protected int NotifyStatus
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringInt32("ns", -1);
            }
        }

        protected string kano
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringString("ka", string.Empty);
            }
        }


        protected string stime
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringString("stime", string.Empty);
            }
        }

        protected string etime
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringString("etime", string.Empty);
            }
        }

        protected string sysorderid
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringString("orderid", string.Empty);
            }
        }

        protected string userorderid
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringString("userorder", string.Empty);
            }
        }

        protected string supporderid
        {
            get
            {
                return viviLib.Web.WebBase.GetQueryStringString("supporder", string.Empty);
            }
        }
        #endregion
        //public UserAccessTimeInfo _userAcceTime = null;
        //public UserAccessTimeInfo userAcceTime 
        //{
        //    get
        //    {
        //        if (_userAcceTime == null && UserId > 0)
        //        {
        //            _userAcceTime =BLL.User.UserAccessTime.GetModel(UserId);
        //        }
        //        return _userAcceTime;
        //    }
        //}




        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
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

        #region InitForm
        /// <summary>
        /// 
        /// </summary>
        private void InitForm()
        {
            if (this.UserId > -1)
            {
                this.txtuserid.Text = UserId.ToString();
            }
            //if (this.Status > -1)
            //{
            //    this.ddlOrderStatus.SelectedValue = Status.ToString();
            //}

            if (this.ctype > -1)
            {
                ddlChannelType.SelectedValue = ctype.ToString();
            }

            //if (!string.IsNullOrEmpty(this.kano))
            //{
            //    this.txtCardNo.Text = this.kano;
            //}
            if (!string.IsNullOrEmpty(this.stime))
            {
                this.StimeBox.Text = this.stime;
            }
            if (!string.IsNullOrEmpty(this.etime))
            {
                this.EtimeBox.Text = this.etime;
            }
            if (!string.IsNullOrEmpty(this.sysorderid))
            {
                this.txtOrderId.Text = this.sysorderid;
            }
            if (!string.IsNullOrEmpty(this.userorderid))
            {
                this.txtUserOrder.Text = this.userorderid;
            }
            if (!string.IsNullOrEmpty(this.supporderid))
            {
                this.txtSuppOrder.Text = this.supporderid;
            }

            //ddlmange.Items.Add("--请选择业务员--");
            //DataTable data = viviapi.BLL.ManageFactory.GetList(" status =1").Tables[0];
            //foreach (DataRow dr in data.Rows)
            //{
            //    ddlmange.Items.Add(new ListItem(dr["username"].ToString(), dr["id"].ToString()));
            //}

            //if (this.MID > -1)
            //    this.ddlmange.SelectedValue = this.MID.ToString();
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
            listParam.Add(new SearchParam("agentid", this.CurrentUser.ID));

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
            if (!string.IsNullOrEmpty(txtSuppOrder.Text.Trim()))
            {
                listParam.Add(new SearchParam("supplierOrder", txtSuppOrder.Text.Trim()));
            }

            listParam.Add(new SearchParam("status", 2));


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

            if (this.currPage > -1)
                this.Pager1.CurrentPageIndex = this.currPage;

            DataTable stat = pageData.Tables[2];
            if (stat.Rows.Count >= 0)
            {
                if (stat.Rows[0]["realvalue"] != DBNull.Value)
                {
                    TotalTranATM = Convert.ToDecimal(stat.Rows[0]["realvalue"]).ToString("f2");
                }
                if (stat.Rows[0]["payAmt"] != DBNull.Value)
                {
                    TotalUserATM = Convert.ToDecimal(stat.Rows[0]["payAmt"]).ToString("f2");
                }
                if (stat.Rows[0]["promAmt"] != DBNull.Value)
                {
                    TotalCommission = Convert.ToDecimal(stat.Rows[0]["promAmt"]).ToString("f2");
                }
                if (stat.Rows[0]["profits"] != DBNull.Value)
                {
                    //TotalPromATM = Convert.ToDecimal(stat.Rows[0]["promAmt"]).ToString("f2");
                    TotalProfit = Convert.ToDecimal(stat.Rows[0]["profits"]).ToString("f2");
                }
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

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {


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

                    string callback = viviapi.SysInterface.Bank.Utility.SynchronousNotify(orderId);

                    ShowMessageBox("返回：" + callback);
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