using System;
using System.Collections.Generic;
using System.Data;
using viviapi.Model;
using viviapi.WebComponents.Web;

namespace viviAPI.WebAdmin.Console.User
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UserLoginLog : ManagePageBase
    {
        protected string wzfmoney = string.Empty;
        protected string yzfmoney = string.Empty;


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
                this.StimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.EtimeBox.Attributes.Add("onFocus", "WdatePicker()");

                this.StimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.EtimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");

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
   , ManageRole.Merchant);

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

            string keyword = KeyWordBox.Text.Trim();
            if (!string.IsNullOrEmpty(this.SeachType.SelectedValue) && !string.IsNullOrEmpty(keyword))
            {
                if (this.SeachType.SelectedValue.ToLower() == "UserId")
                {
                    int userId = 0;
                    int.TryParse(keyword, out userId);
                    listParam.Add(new viviLib.Data.SearchParam("id", userId));
                }
                else if (this.SeachType.SelectedValue == "UserName")
                {
                    listParam.Add(new viviLib.Data.SearchParam("userName", keyword));
                }
            }

            if (!string.IsNullOrEmpty(this.StimeBox.Text))
            {
                listParam.Add(new viviLib.Data.SearchParam("starttime", this.StimeBox.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.EtimeBox.Text))
            {
                listParam.Add(new viviLib.Data.SearchParam("endtime", Convert.ToDateTime(this.EtimeBox.Text.Trim()).AddDays(1)));
            }

            DataSet pageData = viviapi.BLL.User.UserLoginLogFactory.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty);
            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.rptUsers.DataSource = pageData.Tables[1];
            this.rptUsers.DataBind();
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
        
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string selids = viviLib.Web.WebBase.GetFormString("chkItem", "");
            if (!string.IsNullOrEmpty(selids))
            {
                foreach (string id in selids.Split(','))
                {
                    int _id = 0;
                    if (int.TryParse(id, out _id))
                    {
                        viviapi.BLL.User.UserLoginLogFactory.Del(_id);
                    }
                }
            }
            this.LoadData();
        }
}
}