using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using viviapi.BLL.Sys;
using viviapi.Model;
using viviapi.WebComponents.Web;
using viviLib.Data;
using viviLib.Web;

namespace viviAPI.WebAdmin.Console.order
{
    /// <summary>
    /// 
    /// </summary>
    public partial class DebugInfos : ManagePageBase
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
            var listParam = new List<SearchParam>();

            if (currentManage.isSuperAdmin <= 0)
            {

            }

            string userId = txtuserId.Text.Trim();
            int tempId = 0;
            if (int.TryParse(userId, out tempId))
            {
                listParam.Add(new SearchParam("userid", tempId));
            }

            if (!string.IsNullOrEmpty(txtuserOrder.Text.Trim()))
            {
                listParam.Add(new SearchParam("userorder", txtuserOrder.Text.Trim()));
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
            string orderby = string.Empty;

            DataSet pageData = Debuglog.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);
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

        protected void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                string ids = Request.Form["chkItem"];

                if (!string.IsNullOrEmpty(ids))
                {
                    foreach (string itemid in ids.Split(','))
                    {
                        Debuglog.Delete(int.Parse(itemid));
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
            LoadData();
        }
    }
}