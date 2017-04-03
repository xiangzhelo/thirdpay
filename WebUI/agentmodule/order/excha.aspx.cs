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

namespace viviAPI.WebUI7uka.agentmodule.order
{
    public partial class excha : viviapi.WebComponents.Web.AgentPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                var listParam = new List<viviLib.Data.SearchParam>();
                listParam.Add(new viviLib.Data.SearchParam("userid", this.UserId));
              // viviapi BLL.OrderCard orderBLL = new BLL.OrderCard();
                DataSet pageData = viviapi.BLL.Order.Card.Factory.Instance.PageSearch(listParam, 8, 1, string.Empty,false);
                DataTable orders = pageData.Tables[1];
                rptorders.DataSource = orders;
                rptorders.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
    }
}
