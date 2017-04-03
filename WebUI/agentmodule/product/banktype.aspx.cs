using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using viviapi.Model;
using viviapi.Model.User;
using viviapi.WebComponents.Web;
using viviLib;
using viviLib.Web;
using viviLib.Security;
using viviapi.BLL;
using DBAccess;

namespace viviAPI.WebUI7uka.agentmodule.product
{
    public partial class Banktype : AgentPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                LoadData();
            }

        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            List<viviLib.Data.SearchParam> listParam = new List<viviLib.Data.SearchParam>();
            int typeId = 0;
            int pageSize = this.Pager1.PageSize;
            int pageIndex = this.Pager1.CurrentPageIndex;

            DataSet pageData = viviapi.BLL.Channel.Channel.GetBankChanels(pageIndex, pageSize, this.UserId, typeId, 0, -1); //PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty);
            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            this.rptcardtypes.DataSource = pageData.Tables[1];
            this.rptcardtypes.DataBind();
        }
    }
}
