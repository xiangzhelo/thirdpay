using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using viviapi.Model;
using viviapi.WebComponents.Web;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.Finance
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Freeze : ManagePageBase
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
            List<viviLib.Data.SearchParam> listParam = new List<viviLib.Data.SearchParam>();
            listParam.Add(new viviLib.Data.SearchParam("balance", ">", 0M));
            if (!isSuperAdmin)
            {
                listParam.Add(new viviLib.Data.SearchParam("manageId", ManageId));
            }

            if (proid > 0)
                listParam.Add(new viviLib.Data.SearchParam("proid", proid));
            //if (!string.IsNullOrEmpty(this.StatusList.SelectedValue))
            //    listParam.Add(new viviLib.Data.SearchParam("status", int.Parse(this.StatusList.SelectedValue)));
            string keyword = txtuserId.Text.Trim();

            if (!string.IsNullOrEmpty(keyword))
            {
                int userId = 0;
                int.TryParse(keyword, out userId);
                listParam.Add(new viviLib.Data.SearchParam("id", userId));
            }

            string orderby = orderBy + " " + orderByType;

            DataSet pageData = viviapi.BLL.User.Factory.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);
            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.rptUsers.DataSource = pageData.Tables[1];
            this.rptUsers.DataBind();

        }
        #endregion


        protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {

        }

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
                TextBox txtFreezeMoney = (TextBox)e.Item.FindControl("txtFreezeMoney");
                txtFreezeMoney.Attributes["onkeypress"] = "if (event.keyCode < 45 || event.keyCode > 57) event.returnValue = false;";

            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptUsers_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "freeze")
            {
                string[] arr = e.CommandArgument.ToString().Split('$');
                int userid = Convert.ToInt32(arr[0]);
                decimal _balance = 0M;
                decimal.TryParse(arr[1], out _balance);

                decimal _unpayment = 0M;
                decimal.TryParse(arr[3], out _unpayment);

                decimal _Freeze = 0M;
                decimal.TryParse(arr[2], out _Freeze);

                TextBox txtFreezeMoney = e.Item.FindControl("txtFreezeMoney") as TextBox;
                decimal freezeAmt = decimal.Parse(txtFreezeMoney.Text.Trim());
                if (freezeAmt <= 0M)
                {
                    AlertAndRedirect("请输入正确的金额");
                    return;
                }

                if (freezeAmt > _balance - _unpayment - _Freeze)
                {
                    AlertAndRedirect("冻结的金额大于余额 操作有误");
                    return;
                }
                TextBox txtWhy = (TextBox)e.Item.FindControl("txtWhy");

                viviapi.Model.Settled.UsersAmtFreezeInfo itemInfo = new viviapi.Model.Settled.UsersAmtFreezeInfo();
                itemInfo.userid = userid;
                itemInfo.addtime = DateTime.Now;
                itemInfo.freezeAmt = freezeAmt;
                itemInfo.manageId = ManageId;
                itemInfo.status =  viviapi.Model.Settled.AmtFreezeInfoStatus.否;
                itemInfo.why = txtWhy.Text.Trim();
                itemInfo.unfreezemode = viviapi.Model.Settled.AmtunFreezeMode.未处理;

                if (viviapi.BLL.Settled.UsersAmtFreeze.Freeze(itemInfo))
                {
                    AlertAndRedirect("操作成功");
                    return;
                }
                else
                {
                    AlertAndRedirect("操作失败");
                    return;
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

        protected void Pager1_PageChanging1(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            this.LoadData();
        }
    }
}