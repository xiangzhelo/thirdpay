using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.Model;
using viviapi.WebComponents.Web;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.manage
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Salesman : ManagePageBase
    {
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
            string where = " 1=1 ";
            if(!string.IsNullOrEmpty(this.txtmanageId.Text.Trim()))
            {
                int _manageId = 0;
                if (int.TryParse(this.txtmanageId.Text.Trim(), out _manageId))
                {
                    where += string.Format(" and id = {0}", _manageId);
                }               
            }
            DataTable manageList = viviapi.BLL.ManageFactory.GetList(where).Tables[0];
            manageList.Columns.Add("Commissiontypeview");
            foreach (DataRow row in manageList.Rows)
            {
                if (row["Commissiontype"] != DBNull.Value)
                    row["Commissiontypeview"] = row["Commissiontype"].ToString() == "2" ? "按支付金额%" : "按条固定提成";
            }
            this.rptmanage.DataSource = manageList;
            this.rptmanage.DataBind();
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


        protected void rptUsersItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //// string typeName = string.Empty;
                int _manageid = 0;
                _manageid = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "id"));


                Literal litUserCount = (Literal)e.Item.FindControl("litUserCount");
                Literal litMonthIncome = (Literal)e.Item.FindControl("litMonthIncome");
                Literal litDayIncome = (Literal)e.Item.FindControl("litDayIncome");
                Literal litMonthSetted = (Literal)e.Item.FindControl("litMonthSetted");
                TextBox txtpayAmt = (TextBox)e.Item.FindControl("txtpayAmt");

                litUserCount.Text = viviapi.BLL.ManageFactory.GetManageUsers(_manageid).ToString();

                decimal _income = decimal.Zero;
                decimal _settle = decimal.Zero;
                DateTime btime = FirstDayOfMonth;
                DateTime etime = DateTime.Now;
                _income = decimal.Round(viviapi.BLL.Settled.ManageTrade.GetManageIncome(_manageid, btime, etime), 2);
                _settle = decimal.Round(viviapi.BLL.Settled.ManageTrade.GetSettledAmt(_manageid, btime, etime), 2);
                litMonthIncome.Text = _income.ToString();
                litMonthSetted.Text = _settle.ToString();

                btime = ToDayFirstTime;
                etime = DateTime.Now;
                _income = decimal.Round(viviapi.BLL.Settled.ManageTrade.GetManageIncome(_manageid, btime, etime), 2);
                litDayIncome.Text = _income.ToString();
                txtpayAmt.Attributes["onkeypress"] = "if (event.keyCode < 45 || event.keyCode > 57) event.returnValue = false;";
                object balance = DataBinder.Eval(e.Item.DataItem, "balance");
                if(balance != DBNull.Value)
                    txtpayAmt.Text = decimal.Round(Convert.ToDecimal(balance), 2).ToString();
                else
                    txtpayAmt.Text = "0.00";             
            }

        }

        protected void Pager1_PageChanging(object sender, EventArgs e)
        {
            this.LoadData();
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
                string manageId = e.CommandArgument.ToString();
                int _manageId = int.Parse(manageId);

                viviapi.Model.Manage manageInfo = viviapi.BLL.ManageFactory.GetModel(_manageId);
                if (manageInfo != null)
                {
                    if (!manageInfo.balance.HasValue)
                    {
                        AlertAndRedirect("结算金额大于余额 操作有误");
                        return;
                    }
                    TextBox txtpayAmt = e.Item.FindControl("txtpayAmt") as TextBox;
                    decimal payAmt = decimal.Parse(txtpayAmt.Text.Trim());
                    if (payAmt <= 0M)
                    {
                        AlertAndRedirect("请输入正确的金额");
                        return;
                    }
                    if (payAmt > manageInfo.balance.Value)
                    {
                        AlertAndRedirect("结算金额大于余额 操作有误");
                        return;
                    }

                    int result = viviapi.BLL.Settled.ManageTrade.Add(_manageId, 0, 3, "", DateTime.Now, 0 - payAmt, "提现");
                    if (result > 0)
                    {
                        AlertAndRedirect("结算成功", "Salesman.aspx");
                    }
                    else
                    {
                        AlertAndRedirect("结算失败，请重试!");
                    }

                }
                else
                {
                    AlertAndRedirect("参数错误!");
                }
            }
        }

        protected string GetParm(object userid, object balance, object Freeze, object unpayment)
        {
            try
            {
                decimal temp1, temp2, temp3;
                if (balance == DBNull.Value)
                    temp1 = 0M;
                else
                    temp1 = Convert.ToDecimal(balance);

                if (Freeze == DBNull.Value)
                    temp2 = 0M;
                else
                    temp2 = Convert.ToDecimal(Freeze);

                if (unpayment == DBNull.Value)
                    temp3 = 0M;
                else
                    temp3 = Convert.ToDecimal(unpayment);


                return string.Format("{0}${1}${2}${3}", userid, temp1, temp2, temp3);
            }
            catch
            {
                return string.Format("{0}${1}${2}${3}", "0.00", "0.00", "0.00", "0.00");
            }
        }
    }
}