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
    public partial class IncreaseAmts : ManagePageBase
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
            viviapi.BLL.ManageFactory.CheckSecondPwd();
            setPower();
            if (!this.IsPostBack)
            {
                this.StimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.EtimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.StimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.EtimeBox.Attributes.Add("onFocus", "WdatePicker()");

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
            var listParam = new List<viviLib.Data.SearchParam>();

            if (currentManage.isSuperAdmin <= 0)
            {
                
            }

            string userId = txtuserId.Text.Trim();
            int tempId = 0;
            if(int.TryParse(userId,out tempId))
            {
                listParam.Add(new viviLib.Data.SearchParam("userid", tempId));
            }

            DateTime tempdt = DateTime.MinValue;
            if (!string.IsNullOrEmpty(StimeBox.Text.Trim()))
            {
                if (DateTime.TryParse(StimeBox.Text.Trim(), out tempdt))
                {
                    if (tempdt > DateTime.MinValue)
                    {
                        listParam.Add(new viviLib.Data.SearchParam("stime", StimeBox.Text.Trim()));
                    }
                }
            }

            if (!string.IsNullOrEmpty(EtimeBox.Text.Trim()))
            {
                if (DateTime.TryParse(EtimeBox.Text.Trim(), out tempdt))
                {
                    if (tempdt > DateTime.MinValue)
                    {
                        listParam.Add(new viviLib.Data.SearchParam("etime", tempdt.AddDays(1)));
                    }
                }
            }
            string orderby = string.Empty;

            DataSet pageData = viviapi.BLL.Settled.IncreaseAmt.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);
            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.rptTrades.DataSource = pageData.Tables[1];
            this.rptTrades.DataBind();

        }
        #endregion


        protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            this.LoadData();
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
                //string typeName = string.Empty;
                //string billType = DataBinder.Eval(e.Item.DataItem, "billType").ToString();
                //Literal lit = (Literal)e.Item.FindControl("litbillType");
                //switch (billType)
                //{ 
                //    case "1":
                //        typeName = "订单提成";
                //        break;
                //    case "3":
                //        typeName = "提现结算";
                //        break;
                //}
                //lit.Text = typeName;
            }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("IncreaseAmtEdit.aspx");
        }
}
}