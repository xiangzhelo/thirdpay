using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL.Communication;
using viviapi.Model;
using viviapi.Model.Order;
using viviapi.Model.User;
using viviapi.WebComponents.Web;
using viviLib;
using viviLib.Web;
using viviLib.Security;
using viviapi.BLL;
using DBAccess;
using viviLib.Data;
using System.Data;

namespace viviAPI.WebUI7uka.agentmodule.account
{
    public partial class Feedbacks : AgentPageBase
    {

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
        }
        #endregion

        #region LoadData
        /// <summary>
        /// 
        /// </summary>
        private void LoadData()
        {
            List<SearchParam> listParam = new List<SearchParam>();
            listParam.Add(new SearchParam("userid", UserId));

            feedback dal = new feedback();

            DataSet pageData = dal.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty);
            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.rptfeedback.DataSource = pageData.Tables[1];
            this.rptfeedback.DataBind();



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

        protected void rptfeedback_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Literal lit = (Literal)e.Item.FindControl("litdetail");
                string status = DataBinder.Eval(e.Item.DataItem, "status").ToString();
                if (status == "1")
                {
                    lit.Text = "等待回复";
                }
                else if (status == "2")
                {
                    lit.Text = "已回复";
                }
            }

        }
    }
}
