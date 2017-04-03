using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using viviapi.Model;
using viviapi.Model.User;
using viviLib;
using viviLib.Web;
using viviLib.Security;
using viviapi.BLL;
using DBAccess;

namespace viviapi.web.Business.User
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ManageLoginLog : viviapi.WebComponents.Web.ManagePageBase
    {

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
            bool result = BLL.ManageFactory.CheckCurrentPermission(true
      , ManageRole.None);

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
            listParam.Add(new viviLib.Data.SearchParam("manageid", ManageId));

            string keyword = KeyWordBox.Text.Trim();
            if (!string.IsNullOrEmpty(this.SeachType.SelectedValue) && !string.IsNullOrEmpty(keyword))
            {
                //if (this.SeachType.SelectedValue.ToLower() == "userid")
                //{
                //    int userId = 0;
                //    int.TryParse(keyword, out userId);
                //    listParam.Add(new viviLib.Data.SearchParam("id", userId));
                //}
                //else
                if (this.SeachType.SelectedValue == "UserName")
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

            DataSet pageData = BLL.ManageFactory.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty);
            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.rptUsers.DataSource = pageData.Tables[1];
            this.rptUsers.DataBind();
        }
        #endregion

        protected void Pager1_PageChanged(object sender, EventArgs e)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selids = viviLib.Web.WebBase.GetFormString("chkItem", "");
            if (!string.IsNullOrEmpty(selids))
            {
                foreach (string id in selids.Split(','))
                {
                    int _id = 0;
                    if(int.TryParse(id, out _id))
                    {
                        BLL.ManageFactory.LoginLogDel(_id);
                    }
                }
            }
            this.LoadData();
        }
    }
}