using System;
using System.Collections.Generic;
using System.Data;
using viviapi.BLL.Supplier;
using viviapi.Model;
using viviapi.WebComponents.Web;
using viviLib.Data;

namespace viviAPI.WebAdmin.Console.Supplier
{
    /// <summary>
    /// 
    /// </summary>
    public partial class List : ManagePageBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //viviapi.BLL.ManageFactory.CheckSecondPwd();
            setPower();

            if (!this.IsPostBack)
            {
                LoadData();
            }
        }

        
        private void LoadData()
        {
            int tempValue = 0;

            var listParam = new List<SearchParam>();
            if (!string.IsNullOrEmpty(ddlused.SelectedValue))
            {
                bool used = ddlused.SelectedValue == "1";

                listParam.Add(new SearchParam("used", used));
            }

            if (!string.IsNullOrEmpty(txtSupplierName.Text))
            {
                listParam.Add(new SearchParam("name", txtSupplierName.Text));
            }

            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                if (int.TryParse(txtCode.Text, out tempValue))
                {
                    listParam.Add(new SearchParam("code", tempValue));
                }
            }

            DataSet pageData = Factory.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, "");
            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);

            this.rptdata.DataSource = pageData.Tables[1];
            this.rptdata.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = viviapi.BLL.ManageFactory.CheckCurrentPermission(false
            , ManageRole.Interfaces);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Edit.aspx",true);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}