using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL.Order.Card;
using viviapi.Model;
using viviapi.Model.Order;
using viviapi.Model.User;
using viviLib;
using viviLib.Web;
using viviLib.Security;
using viviapi.BLL;
using DBAccess;
using System.Data;

namespace viviAPI.WebUI7uka.usermodule.order
{
    public partial class cardreport : viviapi.WebComponents.Web.UserPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                LoadData();
            }
        }

        protected void b_search_Click(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                LoadData();
            }
        }
        protected void rptDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            
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
            List<viviLib.Data.SearchParam> listParam = new List<viviLib.Data.SearchParam>();
            listParam.Add(new viviLib.Data.SearchParam("userid", UserId));
            listParam.Add(new viviLib.Data.SearchParam("ordertype", "<>", 8));
            int tempId = 0;

            string s_type = select_field.Value;
            if (int.TryParse(s_type, out tempId))
            {
                string searchkey = okey.Value.Trim();
                if (!string.IsNullOrEmpty(searchkey))
                {
                    if (tempId == 1)
                    {
                        listParam.Add(new viviLib.Data.SearchParam("userorder", searchkey));
                    }
                    else if (tempId == 2)
                    {
                        listParam.Add(new viviLib.Data.SearchParam("cardNo", searchkey));
                    }
                }
            }
            if (!string.IsNullOrEmpty(Success.Value))
            {
                if (int.TryParse(Success.Value, out tempId))
                {
                    if (tempId > 0)
                    {
                        listParam.Add(new viviLib.Data.SearchParam("status", tempId));
                    }
                }
            }


            #endregion

            string orderby = string.Empty;// orderBy + " " + orderByType;



            DataSet pageData = CardNotify.Instance.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);

            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.rptOrders.DataSource = pageData.Tables[1];
            this.rptOrders.DataBind();


        }
        public string builderdate(string date, string hour, string m, string s)
        {
            return string.Format("{0} {1}:{2}:{3}", date, hour, m, s);
        }
        #endregion
    }
}
