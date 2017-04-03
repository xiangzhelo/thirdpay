using System;
using System.Collections.Generic;
using System.Data;
using viviapi.BLL.Order;
using viviapi.Model;

namespace viviAPI.WebAdmin.Console.Stat
{
    public partial class usersOrderIncomes : viviapi.WebComponents.Web.ManagePageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            setPower();
            if (!this.IsPostBack)
            {
                this.StimeBox.Text = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                this.EtimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.StimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.EtimeBox.Attributes.Add("onFocus", "WdatePicker()");
            }
        }

        #region setPower
        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = viviapi.BLL.ManageFactory.CheckCurrentPermission(false
               , ManageRole.Report);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
        }
        #endregion

        void LoadData()
        {
            List<viviLib.Data.SearchParam> listParam = new List<viviLib.Data.SearchParam>();

            int tempId = 0;
            if (!string.IsNullOrEmpty(txtuserid.Text.Trim()))
            {
                if (int.TryParse(txtuserid.Text.Trim(), out tempId))
                {
                    listParam.Add(new viviLib.Data.SearchParam("userid", tempId));
                }
            }

            if (!string.IsNullOrEmpty(ddlChannelType.SelectedValue))
            {
                if (int.TryParse(ddlChannelType.SelectedValue, out tempId))
                {
                    if (tempId > 0)
                    {
                        listParam.Add(new viviLib.Data.SearchParam("typeid", tempId));
                    }
                }
            }



            DateTime tempdt = DateTime.MinValue;
            if (!string.IsNullOrEmpty(StimeBox.Text.Trim()))
            {
                if (DateTime.TryParse(StimeBox.Text.Trim(), out tempdt))
                {
                    if (tempdt > DateTime.MinValue)
                    {
                        listParam.Add(new viviLib.Data.SearchParam("stime", tempdt.ToString("yyyy-MM-dd")));
                    }
                }
            }

            if (!string.IsNullOrEmpty(EtimeBox.Text.Trim()))
            {
                if (DateTime.TryParse(EtimeBox.Text.Trim(), out tempdt))
                {
                    if (tempdt > DateTime.MinValue)
                    {
                        listParam.Add(new viviLib.Data.SearchParam("etime", tempdt.ToString("yyyy-MM-dd")));
                    }
                }
            }

            decimal tempnum = 0M;
            if (!string.IsNullOrEmpty(txtvaluefrom.Text.Trim()))
            {
                if (decimal.TryParse(txtvaluefrom.Text.Trim(), out tempnum))
                {
                    listParam.Add(new viviLib.Data.SearchParam("fvaluefrom", tempnum));
                }
            }

            if (!string.IsNullOrEmpty(EtimeBox.Text.Trim()))
            {
                if (decimal.TryParse(EtimeBox.Text.Trim(), out tempnum))
                {
                    listParam.Add(new viviLib.Data.SearchParam("fvalueto", tempnum));
                }
            }

            string orderby = string.Empty;// orderBy + " " + orderByType;

            DataSet pageData = OrderIncome.Instance.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);
            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.gv_data.DataSource = pageData.Tables[1];
            this.gv_data.DataBind();    
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            LoadData();     
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}