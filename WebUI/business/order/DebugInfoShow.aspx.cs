using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.Model;
using viviapi.Model.User;
using viviLib;
using viviLib.Web;
using viviLib.Security;
using viviapi.BLL;
using DBAccess;

namespace viviapi.web.business.Order
{
    public partial class Console_Order_DebugInfoShow : viviapi.WebComponents.Web.BusinessPageBase
    {
        public string strid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    strid = Request.Params["id"];
                    int id = (Convert.ToInt32(strid));
                    ShowInfo(id);
                }
            }
        }
        private void ShowInfo(int id)
        {
            Model.Sys.debuginfo model = BLL.Sys.Debuglog.GetModel(id);
            this.lblid.Text = model.id.ToString();
            this.lblbugtype.Text = Enum.GetName(typeof(viviapi.Model.Sys.debugtypeenum), model.bugtype);
            this.lbluserid.Text = model.userid.ToString();
            this.lbluserorder.Text = model.userorder;
            this.lblurl.Text = model.url;
            this.lblerrorcode.Text = model.errorcode;
            this.lblerrorinfo.Text = model.errorinfo;
            this.lbldetail.Text = model.detail;
            if (model.addtime != null) this.lbladdtime.Text = model.addtime.Value.ToString("yyyy-MM-dd HH:ss:mm");
        }
    }
}