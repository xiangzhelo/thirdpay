using System;

namespace viviAPI.WebAdmin.Console.order
{
    public partial class Console_Order_DebugInfoShow : viviapi.WebComponents.Web.ManagePageBase
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
            viviapi.Model.Sys.debuginfo model = viviapi.BLL.Sys.Debuglog.GetModel(id);
            this.lblid.Text = model.id.ToString();
            this.lblbugtype.Text = Enum.GetName(typeof(viviapi.Model.Sys.debugtypeenum), model.bugtype);
            this.lbluserid.Text = model.userid.ToString();
            this.lbluserorder.Text = model.userorder;
            this.lblurl.Text = model.url;
            this.lblerrorcode.Text = model.errorcode;
            this.lblerrorinfo.Text = model.errorinfo;
            this.lbldetail.Text = model.detail;
            this.lbladdtime.Text = model.addtime.Value.ToString("yyyy-MM-dd HH:ss:mm");
        }
    }
}