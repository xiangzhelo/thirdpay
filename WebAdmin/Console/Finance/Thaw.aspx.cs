using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.Model;
using viviapi.Model.Settled;
using viviapi.WebComponents.Web;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.Finance
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UnFreeze : ManagePageBase
    {
        protected string wzfmoney = string.Empty;
        protected string yzfmoney = string.Empty;

        public string orderBy
        {
            get
            {
                return WebBase.GetQueryStringString("orderby", "addtime");
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

            string keyword = txtuserId.Text.Trim();
            if (!string.IsNullOrEmpty(keyword))
            {
                int userId = 0;
                int.TryParse(keyword, out userId);
                listParam.Add(new viviLib.Data.SearchParam("userid", userId));
            }

            string orderby = orderBy + " " + orderByType;

            DataSet pageData = viviapi.BLL.Settled.UsersAmtFreeze.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);
            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.rptData.DataSource = pageData.Tables[1];
            this.rptData.DataBind();

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
                string status = DataBinder.Eval(e.Item.DataItem, "status").ToString();

                Button btn_unfreeze1 = (Button)e.Item.FindControl("btn_unfreeze1");
                Button btn_unfreeze2 = (Button)e.Item.FindControl("btn_unfreeze2");
                if (status == "1")
                {
                    btn_unfreeze1.Visible = true;
                    btn_unfreeze2.Visible = true;
                }
                else
                {
                    btn_unfreeze1.Visible = false;
                    btn_unfreeze2.Visible = false;
                }
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptUsers_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                AmtunFreezeMode mode = AmtunFreezeMode.未处理;
                if (e.CommandName == "unfreeze1")
                {
                    mode = AmtunFreezeMode.解冻到余额;
                }
                else if (e.CommandName == "unfreeze2")
                {
                    mode = AmtunFreezeMode.解冻并扣除;
                }
                if (mode != AmtunFreezeMode.未处理)
                {
                    int id = int.Parse(e.CommandArgument.ToString());
                    if (viviapi.BLL.Settled.UsersAmtFreeze.unFreeze(id, mode))
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
            catch (Exception ex)
            {
                AlertAndRedirect(ex.Message);
            }
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }
    }
}