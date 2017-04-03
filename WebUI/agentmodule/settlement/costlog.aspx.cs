using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using viviapi.WebComponents.Web;
using viviLib.Data;

namespace viviAPI.WebUI7uka.agentmodule.settlement
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Costlog : AgentPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InitForm();
                LoadData();
            }
        }

        void InitForm()
        {
            //this.sdate.Value = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            //this.edate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        }

        void LoadData()
        {
            var listParam = new List<SearchParam> {new SearchParam("userid", UserId)};

            if (ddlstatus.Value != "-1")
            {
                listParam.Add(new SearchParam("status", int.Parse(ddlstatus.Value)));
            }

            //if (!string.IsNullOrEmpty(this.okey.Value.Trim()))
            //{
            //    listParam.Add(new SearchParam("account", this.okey.Value));
            //}

            //DateTime tempdt = DateTime.MinValue;
            //if (!string.IsNullOrEmpty(sdate.Value.Trim()))
            //{
            //    if (DateTime.TryParse(builderdate(this.sdate.Value, this.stime1.Value, this.stime2.Value, this.stime3.Value), out tempdt))
            //    {
            //        if (tempdt > DateTime.MinValue)
            //        {
            //            listParam.Add(new SearchParam("saddtime", sdate.Value.Trim()));
            //        }
            //    }
            //}

            //if (!string.IsNullOrEmpty(edate.Value.Trim()))
            //{
            //    if (DateTime.TryParse(builderdate(this.edate.Value, this.etime1.Value, this.etime2.Value, this.etime3.Value), out tempdt))
            //    {
            //        if (tempdt > DateTime.MinValue)
            //        {
            //            listParam.Add(new SearchParam("eaddtime", tempdt.AddDays(1)));
            //        }
            //    }
            //}

            string orderby = string.Empty;


            DataSet pageData = viviapi.BLL.Finance.Withdraw.Instance.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby,false);
            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.rptDetails.DataSource = pageData.Tables[1];
            this.rptDetails.DataBind();
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void rptDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void b_search_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}

