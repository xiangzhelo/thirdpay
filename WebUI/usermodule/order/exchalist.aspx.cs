using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.Model.Channel;
using DBAccess;
using viviLib.Data;
using System.Data;

namespace viviAPI.WebUI7uka.usermodule.order
{
    public partial class exchaList : viviapi.WebComponents.Web.UserPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //var listParam = new List<viviLib.Data.SearchParam>();
                //listParam.Add(new viviLib.Data.SearchParam("userid", this.UserId));
                //// viviapi BLL.OrderCard orderBLL = new BLL.OrderCard();
                //DataSet pageData = viviapi.BLL.Order.Card.Factory.Instance.PageSearch(listParam, 8, 1, string.Empty, false);
                //DataTable orders = pageData.Tables[1];
                //rptorders.DataSource = orders;
                //rptorders.DataBind();
                LoadData();
            }
        }
        protected void b_search_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        #region LoadData
        /// <summary>
        /// 
        /// </summary>
        private void LoadData()
        {
            #region build where
            var listParam = new List<SearchParam>();
            listParam.Add(new SearchParam("userid", UserId));

            DateTime tempdt = DateTime.MinValue;
            if (DateTime.TryParse(builderdate(this.sdate.Value, "00", "00", "00"), out tempdt))
            {
                if (tempdt > DateTime.MinValue)
                {
                    listParam.Add(new SearchParam("stime", tempdt.ToString()));
                }
            }

            if (DateTime.TryParse(builderdate(this.edate.Value, "23", "59", "59"), out tempdt))
            {
                if (tempdt > DateTime.MinValue)
                {
                    listParam.Add(new SearchParam("etime", tempdt.ToString()));
                }
            }

           
            #endregion

            string orderby = string.Empty;// orderBy + " " + orderByType;



            DataSet pageData = viviapi.BLL.Order.Card.Factory.Instance.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby, true);

            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.rptorders.DataSource = pageData.Tables[1];
            this.rptorders.DataBind();
        }
        public string builderdate(string date, string hour, string m, string s)
        {
            return string.Format("{0} {1}:{2}:{3}", date, hour, m, s);
        }
        #endregion
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
    }
}
