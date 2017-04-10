﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.WebComponents.Web;
using viviLib.Data;

namespace viviAPI.WebAdmin.Console.quota
{
    public partial class quotalist : ManagePageBase
    {
        protected string[] quotaType = new string[8];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InitForm();
                LoadData();
            }
        }
        #region InitForm
        void InitForm()
        {
            this.sdate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.edate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.sdate.Attributes.Add("onFocus", "WdatePicker()");
            this.edate.Attributes.Add("onFocus", "WdatePicker()");
            this.quota_type.Value = "0";
        }
        #endregion
        #region LoadData
        private void LoadData()
        {
            #region build where
            quotaType[0] = "";
            quotaType[1] = "AG额度";
            quotaType[2] = "BBIN额度";
            quotaType[3] = "MG额度";
            quotaType[4] = "OG额度";
            quotaType[5] = "HG额度";
            quotaType[6] = "PT额度";
            quotaType[7] = "EBET额度";
            var listParam = new List<SearchParam>();
            DateTime tempdt = DateTime.MinValue;
            if (DateTime.TryParse(builderdate(this.sdate.Text, "00", "00", "00"), out tempdt))
            {
                if (tempdt > DateTime.MinValue)
                {
                    listParam.Add(new SearchParam("stime", tempdt.ToString()));
                }
            }

            if (DateTime.TryParse(builderdate(this.edate.Text, "23", "59", "59"), out tempdt))
            {
                if (tempdt > DateTime.MinValue)
                {
                    listParam.Add(new SearchParam("etime", tempdt.ToString()));
                }
            }
            if (this.quota_type.Value != null && this.quota_type.Value != "0")
            {
                listParam.Add(new SearchParam("quota_type", int.Parse(this.quota_type.Value)));
            }
            DataSet pageData = viviapi.BLL.Quota.quotaOrder.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, "id desc");
            DataTable totalData = pageData.Tables[0];
            DataTable page1Data = pageData.Tables[1];
            this.Pager1.RecordCount = Convert.ToInt32(totalData.Rows.Count);
            this.rptOrders.DataSource = page1Data;
            this.rptOrders.DataBind();
            #endregion

        }
        #endregion
        protected void b_search_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        protected string toPercent(string payrate)
        {
            decimal rate = decimal.Parse(payrate) * 100M;
            return rate.ToString("f2");
        }
        private string builderdate(string date, string hour, string m, string s)
        {
            return string.Format("{0} {1}:{2}:{3}", date, hour, m, s);
        }
    }
}