using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using viviapi.BLL.Finance;
using viviapi.Model;
using viviapi.Model.Finance;
using viviapi.Model.Settled;
using viviapi.WebComponents.Web;
using viviLib.Data;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.Withdraw
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SysSettle : ManagePageBase
    {
        viviapi.BLL.Withdraw.ChannelWithdraw chnlBLL = new viviapi.BLL.Withdraw.ChannelWithdraw();
        viviapi.BLL.User.SettlementAccount acctBLL = new viviapi.BLL.User.SettlementAccount();
        protected string wzfmoney = string.Empty;
        protected string yzfmoney = string.Empty;

        public string orderBy
        {
            get
            {
                return WebBase.GetQueryStringString("orderby", "balance");
            }
        }

        public string orderByType
        {
            get
            {
                return WebBase.GetQueryStringString("type", "asc");
            }
        }

        public string UserStatus
        {
            get
            {
                return WebBase.GetQueryStringString("UserStatus", "");
            }
        }
        public string cmd
        {
            get
            {
                return WebBase.GetQueryStringString("cmd", "");
            }
        }

        public int UserID
        {
            get
            {
                return WebBase.GetQueryStringInt32("ID", 0);
            }
        }

        public int proid
        {
            get
            {
                return WebBase.GetQueryStringInt32("proid", 0);
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
                txtbalance.Attributes["onkeypress"] = "if (event.keyCode < 45 || event.keyCode > 57) event.returnValue = false;";
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
            Decimal _balance = 0M;
            if (!string.IsNullOrEmpty(this.txtbalance.Text))
                Decimal.TryParse(this.txtbalance.Text, out _balance);

            var listParam = new List<SearchParam>();
            listParam.Add(new SearchParam("enableAmt", ">", _balance));
            if (!isSuperAdmin)
            {
                listParam.Add(new SearchParam("manageId", ManageId));
            }

            if (proid > 0)
                listParam.Add(new SearchParam("proid", proid));
            //if (!string.IsNullOrEmpty(this.StatusList.SelectedValue))
            //    listParam.Add(new SearchParam("status", int.Parse(this.StatusList.SelectedValue)));
            string keyword = txtuserId.Text.Trim();

            if (!string.IsNullOrEmpty(keyword))
            {
                int userId = 0;
                int.TryParse(keyword, out userId);
                listParam.Add(new SearchParam("id", userId));
            }

            string orderby = orderBy + " " + orderByType;

            DataSet pageData = viviapi.BLL.User.Factory.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);
            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.rptUsers.DataSource = pageData.Tables[1];
            this.rptUsers.DataBind();
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

        protected void RptUsersItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var txtpayAmt = (TextBox)e.Item.FindControl("txtpayAmt");
                var litTodayIncome = (Literal)e.Item.FindControl("litTodayIncome");

                txtpayAmt.Attributes["onkeypress"] = "if (event.keyCode < 45 || event.keyCode > 57) event.returnValue = false;";
                txtpayAmt.Text = "0";

                int userid = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "id"));

                decimal userAvailableBalance = viviapi.BLL.User.UsersAmt.GetUserAvailableBalance(userid);
                txtpayAmt.Text = decimal.Round(userAvailableBalance, 2).ToString(CultureInfo.InvariantCulture);

                litTodayIncome.Text = Trade.GetUserTotalDetentionAmt(userid).ToString(CultureInfo.InvariantCulture);

            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptUsers_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Settled")
            {
                int userId = 0;
                decimal settleAmt = 0M;
                TextBox txtpayAmt = e.Item.FindControl("txtpayAmt") as TextBox;
                try
                {
                    userId = Convert.ToInt32(e.CommandArgument);
                    settleAmt = Convert.ToDecimal(txtpayAmt.Text.Trim());
                }
                catch { }

                if (userId <= 0 || settleAmt <= 0M)
                {
                    AlertAndRedirect("参数不正确!");
                    return;
                }
                string result = Settle(userId, settleAmt);
                if (!string.IsNullOrEmpty(result))
                {
                    AlertAndRedirect(result);
                    return;
                }
                else
                {
                    AlertAndRedirect("提现成功", "SysSettle.aspx");
                }
            }
        }

        protected void Pager1_PageChanging(object sender, EventArgs e)
        {
            this.LoadData();
        }

        protected void btnBatchSettle_Click(object sender, EventArgs e)
        {
            int total = 0; int success = 0; decimal totalAmt = 0M;

            foreach (RepeaterItem item in rptUsers.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    HtmlInputCheckBox checkbox = item.FindControl("chkItem") as HtmlInputCheckBox;
                    if (!checkbox.Checked)
                        continue;

                    HiddenField hfuserid = item.FindControl("hfuserid") as HiddenField;
                    total++;

                    int userId = 0;
                    decimal settleAmt = 0M;
                    TextBox txtpayAmt = item.FindControl("txtpayAmt") as TextBox;
                    try
                    {
                        userId = Convert.ToInt32(hfuserid.Value);
                        settleAmt = Convert.ToDecimal(txtpayAmt.Text.Trim());
                    }
                    catch { }
                    if (userId <= 0 || settleAmt <= 0M)
                    {
                        //AlertAndRedirect("参数不正确!");
                        continue;
                    }
                    string result = Settle(userId, settleAmt);
                    if (string.IsNullOrEmpty(result))
                    {
                        success++;
                        totalAmt += settleAmt;
                    }
                }
            }
            AlertAndRedirect(string.Format("总处理提现总条数{0} 其中成功条数{1} 成功金额{2:0.00}", total, success, totalAmt), "SysSettle.aspx");
        }

        #region Settle
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="settleAmt"></param>
        string Settle(int userId, decimal settleAmt)
        {
            string result = "";

            var userInfo = viviapi.BLL.User.UsersAmt.GetModel(userId);
            var scheme = viviapi.BLL.Finance.TocashScheme.GetModelByUser(1, userId);
            var settlementAccount = acctBLL.GetModel(userId);

            if (scheme == null)
            {
                result = "提现方案未设置";
            }
            else if (userInfo == null)
            {
                result = "用户不存在或者余额不够";
            }
            else if (settleAmt <= 0M)
            {
                result = "请输入正确的金额";
            }
            else if (settleAmt > userInfo.Balance - userInfo.Freeze - userInfo.Unpayment)
            {
                result = "结算金额大于余额 操作有误";
            }
            else
            {
                decimal charges = 0M;

                #region 计算手续费
                charges = scheme.chargerate * settleAmt;
                if (scheme.lowerLimit > 0)
                {
                    if (charges < scheme.lowerAmt)
                    {
                        charges = scheme.lowerAmt;
                    }
                }
                if (scheme.upperLimit > 0)
                {
                    if (charges > scheme.upperAmt)
                    {
                        charges = scheme.upperAmt;
                    }
                }
                #endregion 

                if (settleAmt > charges)
                {
                    var itemInfo = new viviapi.Model.Finance.Withdraw()
                    {
                        Tranno = viviapi.BLL.Finance.Withdraw.Instance.GenerateOrderId(),
                        Addtime = DateTime.Now,
                        Amount = settleAmt,
                        Charges = charges,
                        Paytime = DateTime.Now,
                        Status = WithdrawStatus.Auditing,
                        Tax = 0M,
                        Userid = userId,

                        BankCode = settlementAccount.BankCode,
                        PayeeBank = settlementAccount.PayeeBank,
                        
                        ProvinceCode=settlementAccount.ProvinceCode,
                        BankProvince = settlementAccount.BankProvince,

                        CityCode = settlementAccount.CityCode,
                        BankCity = settlementAccount.BankCity,
                        Payeeaddress = settlementAccount.BankAddress,

                        PayeeName = settlementAccount.PayeeName,
                        AccoutType = settlementAccount.AccoutType,
                        Account = settlementAccount.Account,
                        Paytype = settlementAccount.Pmode,
                        Settmode = WithdrawMode.System,
                        Required = DateTime.Now.AddHours(2),
                        Suppstatus = 0
                    };

                    if (DateTime.Now.Hour > 16)
                    {
                        itemInfo.Required = DateTime.Now.AddDays(1);
                    }

                    if (scheme.vaiInterface > 0)
                    {
                        itemInfo.SuppId = chnlBLL.GetSupplier(itemInfo.BankCode);
                        itemInfo.Suppstatus = 1;
                    }


                    int itemid = viviapi.BLL.Finance.Withdraw.Instance.Apply(itemInfo);
                    itemInfo.ID = itemid;
                    if (itemid > 0)
                    {
                        result = "提现成功";
                    }
                    else
                    {
                        result = "提现失败";
                    }
                }
                else
                {
                    result = "金额太小";
                }


            }
            return result;
        }
        #endregion

        #region btnAllSettle_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAllSettle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtPassWord.Text))
            {
                AlertAndRedirect("请输入二级密码");
            }
            else
            {
                if (!viviapi.BLL.ManageFactory.SecPwdVaild(this.txtPassWord.Text.Trim()))
                {
                    AlertAndRedirect("二级密码不正确");
                }
                else
                {
                    Decimal balance = 0M;
                    if (!string.IsNullOrEmpty(this.txtbalance.Text))
                        Decimal.TryParse(this.txtbalance.Text, out balance);
                    if (balance <= 0)
                        balance = 0M;

                    DataTable data = viviapi.BLL.User.UsersAmt.GetList("balance-Freeze-unpayment>" + balance.ToString()).Tables[0];

                    foreach (DataRow row in data.Rows)
                    {
                        int userid = Convert.ToInt32(row["userid"]);
                        decimal enableAmt = Convert.ToDecimal(row["enableAmt"]);

                        Settle(userid, enableAmt);
                    }
                    AlertAndRedirect("操作成功", "Audits.aspx");
                }
            }
        }
        #endregion

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}