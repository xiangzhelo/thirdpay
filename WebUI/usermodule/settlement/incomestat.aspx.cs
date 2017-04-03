using System;
using System.Collections.Generic;
using System.Data;
using viviapi.WebComponents.Web;
using viviLib.Data;

namespace viviAPI.WebUI7uka.usermodule.settlement
{
    public partial class Incomestat : UserPageBase
    {
        protected string pageordercount = "0";
        protected string pagesumpay = "0";
        protected string totalordercount = "0";
        protected string totalsumpay = "0";

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
            this.sdate.Value = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
            this.edate.Value = DateTime.Today.ToString("yyyy-MM-dd");
        }
        #endregion

        #region LoadData
        /// <summary>
        /// 
        /// </summary>
        private void LoadData()
        {
            #region build where
            var listParam = new List<SearchParam>();
            listParam.Add(new SearchParam("userid", UserId));
            int tempId = 0;
            if (!string.IsNullOrEmpty(ddlChannelType.SelectedValue))
            {
                if (int.TryParse(ddlChannelType.SelectedValue, out tempId))
                {
                    if (tempId > 0)
                    {
                        listParam.Add(new SearchParam("typeId", tempId));
                    }
                }
            }
            DateTime tempdt = DateTime.MinValue;
            if (DateTime.TryParse(this.sdate.Value, out tempdt))
            {
                if (tempdt > DateTime.MinValue)
                {
                    listParam.Add(new SearchParam("stime", tempdt.ToString("yyyy-MM-dd")));
                }
            }

            if (DateTime.TryParse(this.edate.Value, out tempdt))
            {
                if (tempdt > DateTime.MinValue)
                {
                    listParam.Add(new SearchParam("etime", tempdt.ToString("yyyy-MM-dd")));
                }
            }
            #endregion

            string orderby = string.Empty;// orderBy + " " + orderByType;

            DataSet pageData = viviapi.BLL.Order.OrderIncome.Instance.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);

            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.rptOrders.DataSource = pageData.Tables[1];
            this.rptOrders.DataBind();

            DataTable data2 = pageData.Tables[2];
            if (data2 != null && data2.Rows.Count > 0)
            {
                try
                {
                    totalordercount = Convert.ToDecimal(data2.Rows[0]["s_num"]).ToString("f0");
                    totalsumpay = Convert.ToDecimal(data2.Rows[0]["sumpay"]).ToString("f0");
                }
                catch
                { }
            }

            data2 = pageData.Tables[1];
            try
            {
                pageordercount = Convert.ToDecimal(data2.Compute("sum(s_num)", "")).ToString("f0");
                pagesumpay = Convert.ToDecimal(data2.Compute("sum(sumpay)", "")).ToString("f0");
            }
            catch
            { }


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
    }
}
