using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.WebComponents.Web;

namespace viviAPI.WebAdmin.Console.quota
{
    public partial class quotatype : ManagePageBase { 

        protected void Page_Load(object sender, EventArgs e)
        {
            /*if (!this.IsPostBack)
            {

            }*/
            LoadData();
        }
        void LoadData()
        {
            DataSet pageData = viviapi.BLL.Quota.Quotatype.getType();
            this.rpt_paymode.DataSource = pageData.Tables[0];
            this.rpt_paymode.DataBind();
        }
        protected string toPercent(string payrate)
        {
            decimal rate = decimal.Parse(payrate) * 100M;
            return rate.ToString("f2");
        }
    }
}