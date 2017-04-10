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
    public partial class quotapayrate : ManagePageBase
    {
       public int searchuserID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                try
                {
                    searchuserID=int.Parse(Request.Params["userid"]);
                }
                catch {
                    searchuserID = 0;
                }
                if (searchuserID > 0)
                {
                    this.userid.Value = searchuserID.ToString();
                    LoadData();
                }
                else
                {
                    this.rpt_paymode.DataSource = null;
                    this.rpt_paymode.DataBind();
                }
            }
        }
        void LoadData()
        {
            DataSet pageData = viviapi.BLL.Quota.Quotapayrate.getpayrate(this.searchuserID);
            this.rpt_paymode.DataSource = pageData.Tables[0];
            this.rpt_paymode.DataBind();
        }
        protected string toPercent(string payrate)
        {
            decimal rate = decimal.Parse(payrate) * 100M;
            return rate.ToString("f2");
        }

        protected void b_search_Click1(object sender, EventArgs e)
        {
            this.searchuserID = int.Parse(this.userid.Value.ToString());
            LoadData();
        }
    }
}