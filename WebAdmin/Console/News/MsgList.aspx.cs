using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using viviapi.BLL.News;
using viviapi.Model;
using viviapi.Model.News;
using viviapi.WebComponents.Web;
using viviLib.Data;

namespace viviAPI.WebAdmin.Console.News
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MsgList : ManagePageBase
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
                this.StimeBox.Text = DateTime.Today.AddDays(-7).ToString("yyyy-MM-dd");
                this.EtimeBox.Text = DateTime.Today.ToString("yyyy-MM-dd");

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
           , ManageRole.News);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
        }
        #endregion

        protected void LoadData()
        {
            var listParam = new List<SearchParam>();
            //if (!string.IsNullOrEmpty(this.ddl_type.SelectedValue))
            //    listParam.Add(new SearchParam("newstype", int.Parse(this.ddl_type.SelectedValue)));
            //string keyword = txtTitle.Text.Trim();

            //if (!string.IsNullOrEmpty(keyword))
            //{
            //    listParam.Add(new SearchParam("msgtitle", keyword));
            //}

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
            DataSet pageData = viviapi.BLL.Communication.InternalMessage.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty);


            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.rptMsgs.DataSource = pageData.Tables[1];
            this.rptMsgs.DataBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string ids = Request.Form["msgIds"];
                foreach (string id in ids.Split(','))
                {
                    viviapi.BLL.Communication.InternalMessage.Delete(int.Parse(id));
                }
            }
            catch { }

            LoadData();
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        } 
        
    }
}
